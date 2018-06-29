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
    class ActionResizeBy : SKAction
    {
        protected Vector2 size;
        protected Vector2 speed;

        public ActionResizeBy(Vector2 size, float duration) : base(duration)
        {
            this.size = size;
            speed = size / duration;
        }

        internal override SKAction copy()
        {
            return new ActionResizeBy(size, duration)
            {
                timingFunction = this.timingFunction
            };
        }

        internal override void evaluateWithNode(SKNode node, float dt)
        {
            if (elapsed + dt > duration)
            {
                dt = duration - elapsed;
            }

            elapsed += dt;

            float t1 = timingFunction(elapsed / duration, 0, 1, 1) * duration;

            SKSpriteNode spriteNode = (SKSpriteNode)node;
            spriteNode.size += speed * (t1 - t0);

            t0 = t1;
        }
    }
}
