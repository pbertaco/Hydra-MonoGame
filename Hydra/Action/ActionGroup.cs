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
    class ActionGroup : Action
    {
        protected List<Action> actions;

        public ActionGroup(IEnumerable<Action> actions)
        {
            this.actions = new List<Action>();
            foreach (var action in actions)
            {
                this.actions.Add(action.copy());
            }

            foreach (var action in actions)
            {
                duration = Math.Max(duration, action.duration);
            }
        }

        internal override Action copy()
        {
            return new ActionGroup(actions);
        }

        internal override void runOnNode(Node node)
        {
            foreach (var action in actions)
            {
                action.runOnNode(node);
            }
        }

        internal override void evaluateWithNode(Node node, float dt)
        {
            elapsed += dt;

            foreach (var action in actions)
            {
                action.evaluateWithNode(node, dt);
            }
        }
    }
}
