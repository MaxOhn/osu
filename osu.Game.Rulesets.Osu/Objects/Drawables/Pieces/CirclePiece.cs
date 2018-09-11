// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Bindings;
using osu.Game.Skinning;
using OpenTK;

namespace osu.Game.Rulesets.Osu.Objects.Drawables.Pieces
{
    public class CirclePiece : Container, IKeyBindingHandler<OsuAction>
    {
        public Func<bool> Hit;
        public CirclePiece hole;

        public CirclePiece()
        {
            Size = new Vector2((float)OsuHitObject.OBJECT_RADIUS * 2);
            Masking = true;
            CornerRadius = Size.X / 2;

            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

            InternalChild = new SkinnableDrawable("Play/osu/hitcircle", _ => new DefaultCirclePiece());
        }

        public bool OnPressed(OsuAction action)
        {
            switch (action)
            {
                case OsuAction.LeftButton:
                case OsuAction.RightButton:
                    var saveH = IsHovered;
                    var saveHI = (Hit?.Invoke() ?? false);
                    var saveHole = !(hole?.IsHovered ?? false);
                    //Console.WriteLine("total onPressed: " + (saveH && saveHI) + "; is hovered: " + saveH + "; hit invoke: " + saveHI + "; hole hovered: " + saveHole);
                    //Console.WriteLine("hole: " + hole + "; is hovered: " + hole?.IsHovered);
                    return saveH && saveHI && saveHole;
                    return IsHovered && (Hit?.Invoke() ?? false) && !(hole?.IsHovered ?? false);
            }

            return false;
        }

        public bool OnReleased(OsuAction action) => false;
    }
}
