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
    class ActionMoveBy : SKAction
    {
        protected Vector2 delta;
        protected Vector2 speed;

        public ActionMoveBy(Vector2 delta, float duration)
        {
            this.delta = delta;
            this.duration = duration;
            if (this.duration <= 0)
            {
                this.duration = 0.001f;
            }
            speed = delta / duration;
        }

        internal override SKAction copy()
        {
            return new ActionMoveBy(delta, duration);
        }

        internal override void evaluateWithNode(SKNode node, float dt)
        {
            if (elapsed + dt > duration)
            {
                dt = duration - elapsed;
            }

            elapsed += dt;
            node.position += speed * dt;
        }
    }
}
