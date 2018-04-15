using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Hydra
{
    public class SKLabelNode : SKNode
    {
        SpriteFont spriteFont;
        string text;
        Color color;
        Vector2 origin;
        float scale;
        SpriteEffects effects;
        float layerDepth;


        public SKLabelNode(string assetName, string text)
        {
            load(assetName, text);
        }

        void load(string assetName, string text)
        {
            spriteFont = SKScene.current.contentManager.Load<SpriteFont>("SpriteFont/" + assetName);
            this.text = text;
            position = Vector2.Zero;
            color = Color.White;
            zRotation = 0;
            Vector2 size = spriteFont.MeasureString(text);
            origin = new Vector2(size.X * 0.5f, size.Y * 0.5f);
            scale = 0.7419354839f;
            effects = SpriteEffects.None;
            layerDepth = 0;
        }

        internal override void draw(Vector2 position, float alpha)
        {
            if (isHidden || alpha <= 0.0f)
            {
                return;
            }
            Game1.spriteBatch.DrawString(spriteFont, text, position + this.position, color * this.alpha * alpha, zRotation, origin, scale, effects, layerDepth);
            base.draw(position, alpha);
        }
    }
}
