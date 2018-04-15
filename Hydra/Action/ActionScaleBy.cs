using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Hydra
{
    class ActionScaleBy : Action
    {
        protected Vector2 scale;
        protected Vector2 speed;

        public ActionScaleBy(Vector2 scale, float duration)
        {
            this.scale = scale;
            this.duration = duration;
            if (this.duration <= 0)
            {
                this.duration = 0.001f;
            }
            speed = scale / duration;
        }

        internal override Action copy()
        {
            return new ActionScaleBy(scale, duration);
        }

        internal override void evaluateWithNode(Node node, float dt)
        {
            if (elapsed + dt > duration)
            {
                dt = duration - elapsed;
            }

            elapsed += dt;

            SpriteNode spriteNode = (SpriteNode)node;
            spriteNode.scale += speed * dt;
        }
    }
}
