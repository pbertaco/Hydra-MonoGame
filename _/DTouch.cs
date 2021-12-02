using System;

using Microsoft.Xna.Framework;

namespace Dragon
{
    public class DTouch
    {
        internal Vector2 lastPosition;
        Vector2 position;

        internal Vector2 delta;

        public DTouch(Vector2 somePosition)
        {
            lastPosition = somePosition;
            position = somePosition;
        }

        internal void moved(Vector2 somePosition)
        {
            lastPosition = position;
            position = somePosition;
            delta = lastPosition - position;
        }

        internal void up(Vector2 somePosition)
        {
            lastPosition = position;
            position = somePosition;
            delta = lastPosition - position;
        }

        internal Vector2 locationIn(DNode node)
        {
            return (position - node.drawPosition).rotateBy(-node.drawRotation) / node.drawScale;
        }
    }
}
