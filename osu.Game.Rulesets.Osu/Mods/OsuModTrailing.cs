// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using osu.Game.Graphics;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Osu.Objects;
using osu.Game.Rulesets.UI;
using osu.Game.Rulesets.Osu.UI.Cursor;

namespace osu.Game.Rulesets.Osu.Mods
{
    public class OsuModTrailing : Mod, IApplicableToRulesetContainer<OsuHitObject>
    {
        public override string Name => "Trailing";
        public override string ShortenedName => "TL";
        public override FontAwesome Icon => FontAwesome.fa_magic;
        public override ModType Type => ModType.Fun;
        public override string Description => "How about a longer trail?";
        public override double ScoreMultiplier => 1;

        public void ApplyToRulesetContainer(RulesetContainer<OsuHitObject> rulesetContainer)
        {
            var cursor = (GameplayCursor)rulesetContainer.Cursor;
            var cursorChildren = cursor.fadeContainer.Children;
            CursorTrail trail = null;
            foreach (var child in cursorChildren)
            {
                if (child is CursorTrail)
                {
                    trail = (CursorTrail)child;
                    break;
                }
            }
            if (trail != null)
                trail.updateShader = false;
        }
    }
}
