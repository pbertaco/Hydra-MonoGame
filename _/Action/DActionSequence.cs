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
    class ActionSequence : ActionGroup
    {
        IEnumerator<DAction> enumerator;

        public ActionSequence(IEnumerable<DAction> actions) : base(actions)
        {
            duration = 0;

            foreach (DAction action in actions)
            {
                duration += action.duration;
            }
        }

        internal override DAction copy()
        {
            return new ActionSequence(actions);
        }

        internal override void runOnNode(DNode node)
        {
            enumerator = actions.GetEnumerator();
            if (enumerator.MoveNext())
            {
                enumerator.Current.runOnNode(node);
            }
        }

        internal override void evaluateWithNode(DNode node, float dt)
        {
            elapsed += dt;

            DAction action = enumerator.Current;

            if (action != null)
            {
                float actionElapsed = action.elapsed;

                action.evaluateWithNode(node, dt);

                if (actionElapsed + dt > action.duration)
                {
                    if (enumerator.MoveNext())
                    {
                        enumerator.Current.runOnNode(node);
                        dt = actionElapsed + dt - action.duration;
                        elapsed -= dt;
                        evaluateWithNode(node, dt);
                    }
                }
            }
        }
    }
}
