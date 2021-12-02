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
    class ActionGroup : DAction
    {
        protected List<DAction> actions;

        public ActionGroup(IEnumerable<DAction> actions) : base(0)
        {
            this.actions = new List<DAction>();
            foreach (var action in actions)
            {
                this.actions.Add(action.copy());
            }

            foreach (var action in actions)
            {
                duration = Math.Max(duration, action.duration);
            }
        }

        internal override DAction copy()
        {
            return new ActionGroup(actions);
        }

        internal override void runOnNode(DNode node)
        {
            foreach (var action in actions)
            {
                action.runOnNode(node);
            }
        }

        internal override void evaluateWithNode(DNode node, float dt)
        {
            elapsed += dt;

            foreach (var action in actions)
            {
                action.evaluateWithNode(node, dt);
            }
        }
    }
}
