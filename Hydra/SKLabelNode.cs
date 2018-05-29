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
        SpriteEffects effects;
        float layerDepth;


        public SKLabelNode(string assetName, string text)
        {
            load(assetName, text);
        }

        void load(string assetName, string someText)
        {
            spriteFont = SKScene.current.contentManager.Load<SpriteFont>("SpriteFont/" + assetName);
			text = someText;
            position = Vector2.Zero;
            color = Color.White;
            zRotation = 0;
            Vector2 size = spriteFont.MeasureString(someText);
            origin = new Vector2(size.X * 0.5f, size.Y * 0.5f);
            scale = Vector2.One;
            effects = SpriteEffects.None;
            layerDepth = 0;
        }

		internal override void draw(Vector2 currentPosition, float currentAlpha, Vector2 currentScale)
        {
			if (isHidden || alpha <= 0.0f)
            {
                return;
            }

            Game1.spriteBatch.DrawString(spriteFont, text, currentPosition + position * currentScale, color * currentAlpha * alpha, zRotation, origin, currentScale * scale, effects, layerDepth);
			base.draw(currentPosition, currentAlpha, currentScale);
        }
    }
}
