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
    class ActionSetTexture : DAction
    {
        Texture2D texture2D;
        bool resize;

        public ActionSetTexture(Texture2D texture2D, bool resize) : base(0)
        {
            this.texture2D = texture2D;
            this.resize = resize;
        }

        internal override DAction copy()
        {
            return new ActionSetTexture(texture2D, resize);
        }

        internal override void runOnNode(DNode node)
        {
            if (resize)
            {
                DSpriteNode spriteNode = (DSpriteNode)node;
                //spriteNode.size = new Vector2(texture2D.Width, texture2D.Height);
            }
        }

        internal override void evaluateWithNode(DNode node, float dt)
        {
            if (elapsed + dt > duration)
            {
                dt = duration - elapsed;
            }

            elapsed += dt;

            DSpriteNode spriteNode = (DSpriteNode)node;
            spriteNode.texture = texture2D;
        }
    }
}
