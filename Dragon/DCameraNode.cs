using System;

using Microsoft.Xna.Framework;

namespace Dragon
{
    public class DCameraNode : DNode
    {
        internal void update(DScene scene, Vector2 somePosition)
        {
            if (parent == null)
            {
                return;
            }

            parent.position = -somePosition + scene.size / 2.0f;
        }
    }
}
