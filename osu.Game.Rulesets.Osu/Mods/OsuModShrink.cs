// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics;
using osu.Game.Graphics;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Osu.Objects.Drawables;
using System.Collections.Generic;
using OpenTK;

namespace osu.Game.Rulesets.Osu.Mods
{
    public class OsuModShrink : Mod, IApplicableToDrawableHitObjects
    {
        public override string Name => "Shrink";
        public override string ShortenedName => "SK";
        public override FontAwesome Icon => FontAwesome.fa_bullseye;
        public override ModType Type => ModType.Fun;
        public override string Description => "Is it me or are they getting smaller?";
        public override double ScoreMultiplier => 1;

        private int amountDrawables;

        public void ApplyToDrawableHitObjects(IEnumerable<DrawableHitObject> drawables)
        {
            int counter = 0;
            foreach (var drawable in drawables)
            {
                counter++;
                drawable.ApplyCustomUpdateState += drawableOnApplyCustomUpdateState;
            }
            amountDrawables = counter;
        }

        protected void drawableOnApplyCustomUpdateState(DrawableHitObject drawable, ArmedState state)
        {
            if (!(drawable is DrawableOsuHitObject d))
                return;

            var h = d.HitObject;
            using (d.BeginAbsoluteSequence(h.StartTime - h.TimePreempt))
                d.ScaleTo(new Vector2(d.Scale.X * amountDrawables / (amountDrawables + d.HitObject.IndexInMap)));
        }
    }
}
