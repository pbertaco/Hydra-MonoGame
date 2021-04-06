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

namespace Dragon
{
    class ActionScaleBy : DAction
    {
        protected Vector2 scale;
        protected Vector2 speed;

        public ActionScaleBy(Vector2 scale, float duration) : base(duration)
        {
            this.scale = scale;
            speed = scale / this.duration;
        }

        internal override DAction copy()
        {
            return new ActionScaleBy(scale, duration)
            {
                timingFunction = this.timingFunction
            };
        }

        internal override void evaluateWithNode(DNode node, float dt)
        {
            if (elapsed + dt > duration)
            {
                dt = duration - elapsed;
            }

            elapsed += dt;

            float t1 = timingFunction(elapsed / duration, 0, 1, 1) * duration;

            node.scale += speed * (t1 - t0);

            t0 = t1;
        }
    }
}
