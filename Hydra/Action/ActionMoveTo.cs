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
    class ActionMoveTo : ActionMoveBy
    {
        Vector2 position;

        public ActionMoveTo(Vector2 position, float duration) : base(position, duration)
        {
            this.position = position;
        }

        internal override SKAction copy()
        {
            return new ActionMoveTo(position, duration);
        }

        internal override void runOnNode(SKNode node)
        {
            speed = (position - node.position) / duration;
        }
    }
}
