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
    class FadeAlphaTo : FadeAlphaBy
    {
        public FadeAlphaTo(float factor, float duration) : base(factor, duration)
        {
        }

        internal override DAction copy()
        {
            return new FadeAlphaTo(factor, duration)
            {
                timingFunction = this.timingFunction
            };
        }

        internal override void runOnNode(DNode node)
        {
            speed = (factor - node.alpha) / duration;
        }
    }
}
