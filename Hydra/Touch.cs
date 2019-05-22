using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace Hydra
{
    class Touch
    {
        internal Vector2 lastPosition;
        Vector2 position;

        internal Vector2 delta;

        public Touch(Vector2 somePosition)
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

        internal Vector2 locationIn(SKNode node)
        {
            return position - node.positionInNode(SKScene.current);
        }
    }
}
