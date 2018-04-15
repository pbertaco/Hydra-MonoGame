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

        public ActionResizeBy(Vector2 size, float duration)
        {
            this.size = size;
            this.duration = duration;
            if (this.duration <= 0)
            {
                this.duration = 0.001f;
            }
            speed = size / duration;
        }

        internal override SKAction copy()
        {
            return new ActionResizeBy(size, duration);
        }

        internal override void evaluateWithNode(SKNode node, float dt)
        {
            if (elapsed + dt > duration)
            {
                dt = duration - elapsed;
            }

            elapsed += dt;

            SKSpriteNode spriteNode = (SKSpriteNode)node;
            spriteNode.size += speed * dt;
        }
    }
}
