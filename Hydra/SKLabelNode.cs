using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Hydra
{
    class SKLabelNode : SKNode
    {
        SpriteFont spriteFont;

        Vector2 origin;

        Vector2 _anchorPoint;
        internal Vector2 anchorPoint
        {
            get
            {
                return _anchorPoint;
            }
            set
            {
                _anchorPoint = value;
                Vector2 size = spriteFont.MeasureString(_text);
                origin = new Vector2(size.X * anchorPoint.X, size.Y * anchorPoint.Y);
            }
        }

        SpriteEffects effects;
        float layerDepth;

        string _text;
        internal string text {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                Vector2 size = spriteFont.MeasureString(value);
                origin = new Vector2(size.X * 0.5f, size.Y * 0.5f);
            }
        }

        Color drawColor;

        Color _color;
        internal Color color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                drawColor = _color * (1.0f - colorBlendFactor);
            }
        }

        float _colorBlendFactor;
        internal float colorBlendFactor
        {
            get
            {
                return _colorBlendFactor;
            }
            set
            {
                _colorBlendFactor = value;
                drawColor = color * (1.0f - _colorBlendFactor);
            }
        }

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

            Game1.current.spriteBatch.DrawString(spriteFont,
                                         text,
                                         currentPosition + position * currentScale,
                                         drawColor * alpha * currentAlpha,
                                         zRotation,
                                         origin,
                                         currentScale * scale * 0.370967742f, // TODO: currentScale * drawScale
                                         effects,
                                         layerDepth);
            
			base.draw(currentPosition, currentAlpha, currentScale);
        }
    }
}
