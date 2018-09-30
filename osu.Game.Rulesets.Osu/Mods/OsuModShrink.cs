// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics;
using osu.Game.Graphics;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Osu.Objects.Drawables;
using System.Collections.Generic;
using osu.Game.Rulesets.Osu.Objects.Drawables.Pieces;
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
                drawable.ApplyCustomUpdateState += shrinkObject;
            }
            amountDrawables = counter;
        }

        protected void shrinkObject(DrawableHitObject drawable, ArmedState state)
        {
            if (drawable is DrawableSlider s)
            {
                var h = s.HitObject;
                using (s.BeginAbsoluteSequence(h.StartTime - h.TimePreempt))
                {
                    s.Body.PathWidth = s.Body.PathWidth * amountDrawables / (amountDrawables + s.HitObject.IndexInMap);
                    s.Ball.ScaleTo(new Vector2(s.Ball.Scale.X * amountDrawables / (amountDrawables + s.HitObject.IndexInMap)));
                    foreach (var nestedObj in s.NestedHitObjects)
                        nestedObj.ScaleTo(new Vector2(nestedObj.Scale.X * amountDrawables / (amountDrawables + s.HitObject.IndexInMap)));
                }
            }
            else if (drawable is DrawableOsuHitObject d)
            {
                var h = d.HitObject;
                using (d.BeginAbsoluteSequence(h.StartTime - h.TimePreempt))
                    d.ScaleTo(new Vector2(d.Scale.X * amountDrawables / (amountDrawables + d.HitObject.IndexInMap)));
            }
        }
    }
}
