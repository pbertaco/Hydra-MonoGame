using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace Hydra
{
    enum HorizontalAlignment { left, center, right }
    enum VerticalAlignment { top, center, bottom }

    public class Control : SpriteNode
    {
        Vector2 sketchPosition;

        HorizontalAlignment horizontalAlignment = HorizontalAlignment.left;
        VerticalAlignment verticalAlignment = VerticalAlignment.top;

        public Control(string assetName, float x = 0, float y = 0) : base(assetName)
        {
            origin = Vector2.Zero;

            sketchPosition = new Vector2(x, y);
            resetPosition();
            GameScene.current.controlList.Add(this);
        }

        internal static Vector2 resetPosition(Vector2 sketchPosition, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            return new Vector2(
                sketchPosition.X + (GameScene.translate.X * (float)horizontalAlignment),
                sketchPosition.Y + (GameScene.translate.Y * (float)verticalAlignment)
                );
        }

        internal void resetPosition()
        {
            position = resetPosition(sketchPosition, horizontalAlignment, verticalAlignment);
        }

        internal void setAlignment(HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            this.horizontalAlignment = horizontalAlignment;
            this.verticalAlignment = verticalAlignment;
            resetPosition();
        }
    }
}
