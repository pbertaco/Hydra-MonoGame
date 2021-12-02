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
    class ActionRepeat : DAction
    {
        DAction action;
        int count;

        int executionCount;

        public ActionRepeat(DAction action, int count) : base(0)
        {
            this.action = action;
            this.count = count;

            duration = action.duration * count;
        }

        internal override DAction copy()
        {
            return new ActionRepeat(action, count);
        }

        internal override void runOnNode(DNode node)
        {
            action.runOnNode(node);
        }

        internal override void evaluateWithNode(DNode node, float dt)
        {
            elapsed += dt;

            if (executionCount < count)
            {
                float actionElapsed = action.elapsed;

                action.evaluateWithNode(node, dt);

                if (actionElapsed + dt > action.duration)
                {
                    executionCount += 1;

                    if (executionCount < count)
                    {
                        action = action.copy(); //TODO: performance
                        action.runOnNode(node);
                        dt = actionElapsed + dt - action.duration;
                        elapsed -= dt;
                        evaluateWithNode(node, dt);
                    }
                }
            }
        }
    }
}
