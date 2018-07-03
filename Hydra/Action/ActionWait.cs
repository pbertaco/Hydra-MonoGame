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
    public class ActionWait : SKAction
    {
        float durationBase;
        float durationRange;

        public ActionWait(float durationBase, float durationRange) : base(durationBase)
        {
            this.durationBase = durationBase;
            this.durationRange = durationRange;

            float randomDuration = (float)(SKNode.random.NextDouble() * durationRange);
            duration = this.durationBase - durationRange / 2 + randomDuration;

            if (duration <= 0)
            {
                duration = 0.001f;
            }
        }

        internal override SKAction copy()
        {
            return new ActionWait(duration, 0);
        }

        internal override void evaluateWithNode(SKNode node, float dt)
        {
            if (elapsed + dt > duration)
            {
                dt = duration - elapsed;
            }

            elapsed += dt;
        }
    }
}
