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
    public class FadeAlphaBy : Action
    {
        protected float factor;
        protected float speed;

        public FadeAlphaBy(float factor, float duration)
        {
            this.factor = factor;
            this.duration = duration;
            if (this.duration <= 0)
            {
                this.duration = 0.001f;
            }
            speed = factor / duration;
        }

        internal override Action copy()
        {
            return new FadeAlphaBy(factor, duration);
        }

        internal override void evaluateWithNode(Node node, float dt)
        {
            if (elapsed + dt > duration)
            {
                dt = duration - elapsed;
            }

            elapsed += dt;

            node.alpha += speed * dt;
        }
    }
}
