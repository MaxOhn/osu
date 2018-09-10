// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Game.Graphics;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Osu.Objects;
using OpenTK;
using osu.Game.Rulesets.Osu.Objects.Drawables;
using osu.Game.Rulesets.Osu.Objects.Drawables.Pieces;

namespace osu.Game.Rulesets.Osu.Mods
{
    internal class OsuModDonut : Mod, IApplicableToDrawableHitObjects
    {
        public override string Name => "Donut";
        public override string ShortenedName => "DN";
        public override FontAwesome Icon => FontAwesome.fa_dot_circle_o;
        public override ModType Type => ModType.Fun;
        public override string Description => "Better not hit bullseye...";
        public override double ScoreMultiplier => 1;

        public void ApplyToDrawableHitObjects(IEnumerable<DrawableHitObject> drawables)
        {
            foreach (var drawable in drawables)
            {
                drawable.CheckCanHit = d =>
                {
                    var hs = (DrawableHitCircle)d;

                    hs.hole = new CirclePiece
                    {
                        Scale = hs.Scale / 3,

                        Alpha = 0.4f
                    };

                    return true;
                };
                drawable.ApplyCustomUpdateState += drawableOnApplyCustomUpdateState;
            }
        }

        private void drawableOnApplyCustomUpdateState(DrawableHitObject drawable, ArmedState state)
        {/*
            if (!(drawable is DrawableHitCircle d))
            {
                return;
            }

            var p = d.Position;

            d = new DrawableHitCircle((HitCircle)d, true);*/
        }
    }
}
