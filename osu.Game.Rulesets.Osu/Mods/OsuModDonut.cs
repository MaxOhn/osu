// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Game.Graphics;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Osu.Objects.Drawables;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Osu.Mods
{
    internal class OsuModDonut : Mod, IApplicableToDrawableHitObjects
    {
        public override string Name => "Donut";
        public override string ShortenedName => "DN";
        public override FontAwesome Icon => FontAwesome.fa_dot_circle_o;
        public override string Description => "Better not hit bullseye...";
        public override double ScoreMultiplier => 1;

        public void ApplyToDrawableHitObjects(IEnumerable<DrawableHitObject> drawables)
        {
            foreach (var drawable in drawables)
            {
                if (drawable is DrawableHitCircle d)
                    d.ChangeToTorus(4f);
                else if (drawable is DrawableSlider s)
                    s.ChangeToTorus(4f);
            }
        }
    }
}
