﻿using System;
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
    public class FadeAlphaTo : FadeAlphaBy
    {
        public FadeAlphaTo(float alpha, float duration) : base(alpha, duration)
        {
        }

        internal override Action copy()
        {
            return new FadeAlphaTo(factor, duration);
        }

        internal override void runOnNode(Node node)
        {
            speed = (factor - node.alpha) / duration;
        }
    }
}