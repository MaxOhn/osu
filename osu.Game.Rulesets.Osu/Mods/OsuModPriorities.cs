// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Linq;
using osu.Game.Rulesets.Mods;
using System.Collections.Generic;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Osu.Objects;
using osu.Game.Rulesets.Osu.Objects.Drawables;
using osu.Game.Rulesets.Scoring;
using osu.Game.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Osu.Mods
{
    public class OsuModPriorities : Mod, IApplicableToDrawableHitObjects, IApplicableToScoreProcessor
    {
        public override string Name => "Priorities";
        public override string ShortenedName => "PT";
        public override FontAwesome Icon => FontAwesome.fa_search;
        public override ModType Type => ModType.Fun;
        public override string Description => "Some are more important than others...";
        public override double ScoreMultiplier => 1;

        private const int timeTillRecolor = 100;

        private bool immediateFail = false;

        public void ApplyToScoreProcessor(ScoreProcessor scoreProcessor)
        {
            scoreProcessor.FailConditions += FailCondition;
        }
        protected bool FailCondition(ScoreProcessor scoreProcessor)
        {
            return scoreProcessor.Combo.Value == 0 && immediateFail;
        }

        public virtual void ApplyToDrawableHitObjects(IEnumerable<DrawableHitObject> drawables)
        {
            foreach (var d in drawables.Where(x => x is DrawableHitCircle || x is DrawableSlider).Where((x, i) => i % 3 == 0))
                d.ApplyCustomUpdateState += ApplyRecoloring;
            foreach (var d in drawables.Where(x => x is DrawableHitCircle || x is DrawableSlider).Where((x, i) => i % 3 != 0))
                d.ApplyCustomUpdateState += UpdateImmediateFail;
        }
        
        protected void ApplyRecoloring(DrawableHitObject drawable, ArmedState state)
        {
            var osuObject = (OsuHitObject)drawable.HitObject;
            int amountRecolors = (int)osuObject.TimePreempt / timeTillRecolor;

            using (drawable.BeginAbsoluteSequence(osuObject.StartTime, true))
                immediateFail = true;
            
            for (int i = 0; i < amountRecolors; i++)
            {
                using (drawable.BeginAbsoluteSequence(osuObject.StartTime - osuObject.TimePreempt + i * timeTillRecolor, true))
                {
                    if (i % 2 == 0)
                        drawable.FadeColour(Color4.Green, timeTillRecolor);
                    else
                        drawable.FadeColour(Color4.LightBlue, timeTillRecolor);
                }
            }
        }

        protected void UpdateImmediateFail(DrawableHitObject drawable, ArmedState state)
        {
            var osuObject = (OsuHitObject)drawable.HitObject;
            using (drawable.BeginAbsoluteSequence(osuObject.StartTime, true))
                immediateFail = false;
        }
    }
}
