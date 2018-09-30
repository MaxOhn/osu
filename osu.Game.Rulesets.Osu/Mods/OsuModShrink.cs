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
        public override FontAwesome Icon => FontAwesome.fa_bullseye;    // TODO: Find better icon
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
                drawable.ApplyCustomUpdateState += ShrinkObject;
            }
            amountDrawables = counter;
        }

        protected void ShrinkObject(DrawableHitObject drawable, ArmedState state)
        {
            if (drawable is DrawableSlider s)
            {
                var h = s.HitObject;
                float rescale = (float)amountDrawables / (amountDrawables + s.HitObject.IndexInMap);
                using (s.BeginAbsoluteSequence(h.StartTime - h.TimePreempt))
                {
                    s.Body.PathWidth = s.Body.PathWidth * rescale;
                    s.Ball.ScaleTo(new Vector2(s.Ball.Scale.X * rescale));
                    foreach (var nestedObj in s.NestedHitObjects)
                        nestedObj.ScaleTo(new Vector2(nestedObj.Scale.X * rescale));
                }
            }
            else if (drawable is DrawableHitCircle d)
            {
                var h = d.HitObject;
                float rescale = (float)amountDrawables / (amountDrawables + d.HitObject.IndexInMap);
                using (d.BeginAbsoluteSequence(h.StartTime - h.TimePreempt))
                    d.ScaleTo(new Vector2(d.Scale.X * rescale))
                        .Delay(h.TimePreempt)
                        .FadeOut(800)
                        .ScaleTo(d.Scale.X * rescale * 1.5f, 400, Easing.OutQuad);  // reapply overwritten ScaleTo
            }
        }
    }
}
