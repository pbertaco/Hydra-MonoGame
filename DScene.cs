using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dragon
{
    public class DScene : DNode
    {
        internal Vector2 size;
        internal Color backgroundColor;

        internal Vector2 currentPosition;
        internal float currentRotation;
        internal Vector2 currentScale;
        internal float currentAlpha;

        public DScene(Vector2 someSize)
        {
            size = someSize;
            backgroundColor = Color.White;
            currentPosition = Vector2.Zero;
            currentRotation = 0;
            currentScale = Vector2.One;
            currentAlpha = 1;
        }

        internal virtual void load()
        {
        }

        internal virtual void update()
        {
        }

        internal void draw(SpriteBatch spriteBatch)
        {
            beforeDraw(currentPosition, currentRotation, currentScale, currentAlpha);
            drawChildren(spriteBatch);
        }
    }
}
