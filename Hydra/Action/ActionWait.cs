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
    public class ActionWait : Action
    {
        float durationRange;

        public ActionWait(float duration, float durationRange)
        {
            this.duration = duration;
            this.durationRange = durationRange;
        }

		internal override Action copy()
		{
            return new ActionWait(duration, durationRange);
		}

		internal override void evaluateWithNode(Node node, float dt)
        {
            if (elapsed + dt > duration)
            {
                dt = duration - elapsed;
            }

            elapsed += dt;
        }
    }
}
