using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Hydra
{
    class Box : Button
    {
        public Box(string assetName,
                   float? x = null,
                   float? y = null,
                   HorizontalAlignment horizontalAlignment = HorizontalAlignment.center,
                   VerticalAlignment verticalAlignment = VerticalAlignment.center,
                   bool animated = false)
            : base(assetName,
                   0,
                   0,
                   horizontalAlignment,
                   verticalAlignment)
        {
            alignCenter(x, y);

            if (animated)
            {
                pop();
            }
        }

        void pop()
        {

        }

        void alignCenter(float? x, float? y)
        {
            if (x == null)
            {
                sketchPosition = new Vector2(SKScene.defaultSize.X / 2.0f - size.X / 2.0f, sketchPosition.Y);
            }

            if (y == null)
            {
                sketchPosition = new Vector2(sketchPosition.X, SKScene.defaultSize.Y / 2.0f - size.Y / 2.0f);
            }

            resetPosition();
        }

        internal override void touchDown()
        {
        }

        internal override void touchUp()
        {
        }
    }
}
