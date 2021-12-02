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
    class ActionWait : DAction
    {
        internal static Random random = new Random();

        float durationBase;

        public ActionWait(float durationBase, float durationRange) : base(durationBase)
        {
            this.durationBase = durationBase;

            float randomDuration = (float)(random.NextDouble() * durationRange);
            duration = this.durationBase - durationRange / 2 + randomDuration;

            if (duration <= 0)
            {
                duration = 0.001f;
            }
        }

        internal override DAction copy()
        {
            return new ActionWait(duration, 0);
        }

        internal override void evaluateWithNode(DNode node, float dt)
        {
            if (elapsed + dt > duration)
            {
                dt = duration - elapsed;
            }

            elapsed += dt;
        }
    }
}
