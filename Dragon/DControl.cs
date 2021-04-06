using System;
using Microsoft.Xna.Framework;

namespace Dragon
{
    public enum HorizontalAlignment { left, center, right }
    public enum VerticalAlignment { top, center, bottom }

    public class DControl : DSpriteNode
    {
        Vector2 _sketchPosition;
        public Vector2 sketchPosition
        {
            get => _sketchPosition;
            set
            {
                _sketchPosition = value;
                resetPosition();
            }
        }

        HorizontalAlignment horizontalAlignment = HorizontalAlignment.left;
        VerticalAlignment verticalAlignment = VerticalAlignment.top;

        public DControl(string assetName, float x = 0, float y = 0, HorizontalAlignment horizontalAlignment = HorizontalAlignment.left, VerticalAlignment verticalAlignment = VerticalAlignment.top) : base(assetName)
        {
            anchorPoint = Vector2.Zero;
            sketchPosition = new Vector2(x, y);
            setAlignment(horizontalAlignment, verticalAlignment);
            DScene.current.controlList.Add(this);
        }

        internal static Vector2 position(Vector2 sketchPosition, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            float x = sketchPosition.X + (DScene.current.position.X * (float)horizontalAlignment);
            float y = sketchPosition.Y + (DScene.current.position.Y * (float)verticalAlignment);
            return new Vector2(x, y);
        }

        internal void resetPosition()
        {
            if (parent == DScene.current)
            {
                position = position(sketchPosition, horizontalAlignment, verticalAlignment);
            }
        }

        internal Vector2 position(Vector2 someSketchPosition)
        {
            return position(someSketchPosition, horizontalAlignment, verticalAlignment);
        }

        internal void setAlignment(HorizontalAlignment someHorizontalAlignment, VerticalAlignment someVerticalAlignment)
        {
            horizontalAlignment = someHorizontalAlignment;
            verticalAlignment = someVerticalAlignment;
            resetPosition();
        }
    }
}
