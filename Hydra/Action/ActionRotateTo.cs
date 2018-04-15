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
    class ActionRotateTo : ActionRotateBy
    {
        public ActionRotateTo(float radians, float duration) : base(radians, duration)
        {
        }

        internal override Action copy()
        {
            return new ActionRotateTo(radians, duration);
        }

        internal override void runOnNode(Node node)
        {
            speed = (radians - node.zRotation) / duration;
        }
    }
}
