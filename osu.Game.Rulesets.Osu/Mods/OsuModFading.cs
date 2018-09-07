// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using osu.Framework.Graphics;
using osu.Game.Graphics;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Osu.Objects.Drawables;
using System.Collections.Generic;
using osu.Game.Rulesets.Objects.Types;
using OpenTK;

namespace osu.Game.Rulesets.Osu.Mods
{
    public class OsuModFading : Mod, IApplicableToDrawableHitObjects
    {
        public override string Name => "Fading";
        public override string ShortenedName => "FD";
        public override FontAwesome Icon => FontAwesome.fa_snapchat_ghost;
        public override ModType Type => ModType.Fun;
        public override string Description => "Where are they going???";
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

            //d.ScaleTo(new Vector2(d.Scale.X * amountDrawables / (amountDrawables + d.HitObject.ComboIndex)));

            using (d.BeginAbsoluteSequence(h.StartTime - h.TimePreempt, true))
            {
                d.FadeTo(Math.Max(0.05f, 1 - ((float)d.HitObject.IndexInMap / amountDrawables)), h.TimeFadeIn);
                Console.WriteLine("Combo: " + d.HitObject.IndexInMap + " ---- Alpha: 1-" + d.HitObject.IndexInMap + "/" + amountDrawables + "=1-" + ((float)d.HitObject.IndexInMap / amountDrawables) + "=" + (1 - ((float)d.HitObject.IndexInMap / amountDrawables)));
            }
            
            if(d is DrawableSlider)
                using (d.BeginAbsoluteSequence(((h as IHasEndTime)?.EndTime ?? h.StartTime) - 1, true))
                    d.FadeOut(100);
        }
    }
}
