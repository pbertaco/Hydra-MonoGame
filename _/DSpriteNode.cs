using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dragon
{
    public class DSpriteNode : DNode
    {
        internal Texture2D texture;
        internal BlendState blendState = BlendState.AlphaBlend;
        internal Color color;

        protected Rectangle? sourceRectangle;
        protected Vector2 anchorPoint;
        protected SpriteEffects spriteEffects;
        protected float layerDepth;

        Vector2 drawOrigin;

        Vector2 textureSize => new Vector2(texture?.Width ?? 128, texture?.Height ?? 128);

        Vector2 _size;
        internal Vector2 size
        {
            get => _size * scale;
            set => _size = value / scale;
        }

        public DSpriteNode(Color? color = null, Vector2? size = null)
        {
            Texture2D texture = DGame.current.contentManager.loadTexture2D("");
            load(texture, color, size);
        }

        public DSpriteNode(Vector2 size)
        {
            Texture2D texture = DGame.current.contentManager.loadTexture2D("");
            load(texture, null, size);
        }

        public DSpriteNode(Texture2D texture, Color? color = null, Vector2? size = null)
        {
            load(texture, color, size);
        }

        public DSpriteNode(Texture2D texture, Vector2 size)
        {
            load(texture, null, size);
        }

        public DSpriteNode(string assetName, Color? color = null, Vector2? size = null)
        {
            Texture2D texture = DGame.current.contentManager.loadTexture2D(assetName);
            load(texture, color, size);
        }

        public DSpriteNode(string assetName, Vector2 size)
        {
            Texture2D texture = DGame.current.contentManager.loadTexture2D(assetName);
            load(texture, null, size);
        }

        void load(Texture2D someTexture, Color? someColor, Vector2? someSize)
        {
            texture = someTexture;
            position = Vector2.Zero;
            sourceRectangle = null;
            color = someColor ?? Color.White;
            rotation = 0;
            anchorPoint = new Vector2(0.5f, 0.5f);
            scale = Vector2.One;
            spriteEffects = SpriteEffects.None;
            layerDepth = 0;
            size = someSize ?? textureSize;
        }

        internal override void beforeDraw(Vector2 currentPosition, float currentRotation, Vector2 currentScale, float currentAlpha)
        {
            base.beforeDraw(currentPosition, currentRotation, currentScale, currentAlpha);

            DGame game = DGame.current;

            if (blendState != game.blendState)
            {
                game.blendState = blendState;
                game.spriteBatch.End();
                game.spriteBatch.Begin(game.sortMode, game.blendState, game.samplerState, game.depthStencilState, game.rasterizerState, game.effect, game.transformMatrix);
            }
        }

        internal override void draw(Vector2 currentPosition, float currentRotation, Vector2 currentScale, float currentAlpha)
        {
            beforeDraw(currentPosition, currentRotation, currentScale, currentAlpha);
            drawOrigin = textureSize * anchorPoint;
            Vector2 drawScale = _size / textureSize * this.drawScale;
            Color drawColor = color * drawAlpha;
            DGame.current.spriteBatch.Draw(texture, drawPosition, sourceRectangle, drawColor, drawRotation, drawOrigin, drawScale, spriteEffects, layerDepth);
            drawChildren();
        }

        internal void setScaleToFit(Vector2 size)
        {

        }
    }
}
