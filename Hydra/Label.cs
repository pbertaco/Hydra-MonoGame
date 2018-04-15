using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace Hydra
{
    public enum SpriteFontName { SpriteFont, KenPixel16, KenPixel18, KenPixel24, KenPixel32 }

    public class Label : SKLabelNode
    {
        Vector2 sketchPosition;

        HorizontalAlignment horizontalAlignment = HorizontalAlignment.left;
        VerticalAlignment verticalAlignment = VerticalAlignment.top;

        public Label(string text, float x = 0, float y = 0, SpriteFontName spriteFontName = SpriteFontName.SpriteFont) : base(spriteFontName.ToString(), text)
        {
            sketchPosition = new Vector2(x, y);
            resetPosition();
            SKScene.current.labelList.Add(this);
        }

        internal void resetPosition()
        {
            position = Control.resetPosition(sketchPosition, horizontalAlignment, verticalAlignment);
        }

        internal void setAlignment(HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            this.horizontalAlignment = horizontalAlignment;
            this.verticalAlignment = verticalAlignment;
            resetPosition();
        }
    }
}
