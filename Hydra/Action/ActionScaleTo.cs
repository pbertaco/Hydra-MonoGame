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
    class ActionScaleTo : ActionScaleBy
    {
        public ActionScaleTo(Vector2 scale, float duration) : base(scale, duration)
        {
        }

        internal override SKAction copy()
        {
            return new ActionScaleTo(scale, duration)
            {
                timingFunction = this.timingFunction
            };
        }

        internal override void runOnNode(SKNode node)
        {
            speed = (scale - node.scale) / duration;
        }
    }
}
