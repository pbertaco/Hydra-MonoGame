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
    class ActionResizeTo : ActionResizeBy
    {
        public ActionResizeTo(Vector2 size, float duration) : base(size, duration)
        {
        }

		internal override SKAction copy()
		{
            return new ActionResizeTo(size, duration)
            {
                timingFunction = this.timingFunction
            };
		}

		internal override void runOnNode(SKNode node)
        {
            SKSpriteNode spriteNode = (SKSpriteNode)node;
            speed = (size - spriteNode.size) / duration;
		}
	}
}
