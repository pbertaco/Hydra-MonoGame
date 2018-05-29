using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace Hydra
{
    public enum HorizontalAlignment { left, center, right }
    public enum VerticalAlignment { top, center, bottom }

    public class Control : SKSpriteNode
    {
        Vector2 _sketchPosition;
        public Vector2 sketchPosition
        {
            get
            {
                return _sketchPosition;
            }
            set
            {
                _sketchPosition = value;
                resetPosition();
            }
        }

        HorizontalAlignment horizontalAlignment = HorizontalAlignment.left;
        VerticalAlignment verticalAlignment = VerticalAlignment.top;

        public Control(string assetName, float x = 0, float y = 0) : base(assetName)
        {
            origin = Vector2.Zero;

            sketchPosition = new Vector2(x, y);
            SKScene.current.controlList.Add(this);
        }

        public Control(string assetName, float x = 0, float y = 0,
                       HorizontalAlignment horizontalAlignment = HorizontalAlignment.left,
                       VerticalAlignment verticalAlignment = VerticalAlignment.top) : this(assetName, x, y)
        {
            setAlignment(horizontalAlignment, verticalAlignment);
        }

        internal static Vector2 positionWith(Vector2 sketchPosition, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            return new Vector2(
                sketchPosition.X + (SKScene.translate.X * (float)horizontalAlignment),
                sketchPosition.Y + (SKScene.translate.Y * (float)verticalAlignment)
                );
        }

        internal void resetPosition()
        {
            position = positionWith(sketchPosition, horizontalAlignment, verticalAlignment);
        }

        internal Vector2 positionWith(Vector2 sketchPosition)
        {
            return positionWith(sketchPosition, horizontalAlignment, verticalAlignment);
        }

        internal void setAlignment(HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            this.horizontalAlignment = horizontalAlignment;
            this.verticalAlignment = verticalAlignment;
            resetPosition();
        }
    }
}
