using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using FarseerPhysics;

namespace Hydra
{
    public class SKSpriteNode : SKNode
    {
        internal Texture2D texture2D;

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

        protected Rectangle? sourceRectangle;
        protected Vector2 origin;
        protected SpriteEffects effects;
        protected float layerDepth;

        Vector2 drawScale;
        Vector2 sizeScale;

        Vector2 _size;
        internal Vector2 size
        {
            get
            {
                return _size * base.scale;
            }
            set
            {
                _size = value / base.scale;
                sizeScale = new Vector2(_size.X / texture2D.Width, _size.Y / texture2D.Height);
				drawScale = sizeScale * base.scale;
            }
        }

		internal override Vector2 scale
        {
            get
            {
                return base.scale;
            }
            set
            {
                base.scale = value;
                drawScale = sizeScale * base.scale;
            }
        }

        internal BlendState blendState = BlendState.AlphaBlend;

        public SKSpriteNode(Texture2D texture)
        {
            load(texture, Color.White, texture.Bounds.Size.ToVector2());
        }

        public SKSpriteNode(Texture2D texture, Vector2 size)
        {
            load(texture, Color.White, size);
        }

        public SKSpriteNode(Texture2D texture, Color color, Vector2 size)
        {
            load(texture, color, size);
        }

        void load(Texture2D texture, Color color, Vector2 size)
        {
            texture2D = texture;
            position = Vector2.Zero;
            sourceRectangle = null;
            this.color = color;
            zRotation = 0;
            origin = new Vector2(texture2D.Bounds.Width * 0.5f, texture2D.Bounds.Height * 0.5f);
            scale = Vector2.One;
            this.size = size;
            effects = SpriteEffects.None;
            layerDepth = 0.0f;
        }

        public SKSpriteNode(string assetName)
        {
            Texture2D texture = SKScene.current.Texture2D(assetName);

            load(texture, Color.White, new Vector2(texture.Width, texture.Height));
        }

        internal override void beforeDraw()
        {
            base.beforeDraw();

            if (blendState != Game1.blendState)
            {
                Game1.blendState = blendState;
                Game1.spriteBatch.End();
                Game1.spriteBatch.Begin(Game1.sortMode, Game1.blendState, Game1.samplerState, Game1.depthStencilState, Game1.rasterizerState, Game1.effect, Game1.transformMatrix);
            }
        }

		internal override void draw(Vector2 currentPosition, float currentAlpha, Vector2 currentScale)
        {
			if (isHidden || alpha <= 0.0f)
            {
                return;
            }

            beforeDraw();

			Game1.spriteBatch.Draw(texture2D, currentPosition + position, sourceRectangle, drawColor * alpha * currentAlpha, zRotation, origin, currentScale * drawScale, effects, layerDepth);

			drawChildren(currentPosition, currentAlpha, currentScale);
        }

        internal void setScaleToFit(float width, float height)
        {
            scale = Vector2.One;
            float xScale = width / size.X;
            float yScale = height / size.Y;

            scale = Vector2.One * Math.Min(xScale, yScale);

            if (scale.X > 1.0f)
            {
                scale = Vector2.One;
            }
        }

        internal void setScaleToFit(Vector2 size)
        {
            setScaleToFit(size.X, size.Y);
        }
    }
}
