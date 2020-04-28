using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace Hydra
{
    enum HorizontalAlignment { left, center, right }
    enum VerticalAlignment { top, center, bottom }

    class Control : SKSpriteNode
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

        public Control(string assetName, float x = 0, float y = 0,
                       HorizontalAlignment horizontalAlignment = HorizontalAlignment.left,
                       VerticalAlignment verticalAlignment = VerticalAlignment.top) : base(assetName)
        {
            anchorPoint = Vector2.Zero;
            sketchPosition = new Vector2(x, y);

            setAlignment(horizontalAlignment, verticalAlignment);

            SKScene.current.controlList.Add(this);
        }

        internal static Vector2 positionWith(Vector2 sketchPosition, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            return new Vector2(sketchPosition.X + (SKScene.position.X * (float)horizontalAlignment), sketchPosition.Y + (SKScene.position.Y * (float)verticalAlignment));
        }

        internal virtual void resetPosition()
        {
            position = positionWith(sketchPosition, horizontalAlignment, verticalAlignment);
        }

        internal Vector2 positionWith(Vector2 someSketchPosition)
        {
            return positionWith(someSketchPosition, horizontalAlignment, verticalAlignment);
        }

        internal void setAlignment(HorizontalAlignment someHorizontalAlignment, VerticalAlignment someVerticalAlignment)
        {
            horizontalAlignment = someHorizontalAlignment;
            verticalAlignment = someVerticalAlignment;
            resetPosition();
        }
    }
}
