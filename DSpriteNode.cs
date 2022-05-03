using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dragon
{
    public class DSpriteNode : DNode
    {
        Vector2 origin;
        SpriteEffects effects;
        float layerDepth;

        Vector2 textureSize;
        Vector2 textureScale;

        internal Color color;

        Vector2 _size;
        internal Vector2 size
        {
            get => _size * scale;
            set
            {
                _size = value / scale;
                updateTextureScale();
            }
        }

        Vector2 _anchorPoint;
        internal Vector2 anchorPoint
        {
            get => _anchorPoint;
            set
            {
                _anchorPoint = value;
                updateOrigin();
            }
        }

        Texture2D _texture;
        public Texture2D texture
        {
            get => _texture;
            set
            {
                _texture = value;
                updateTextureSize();
            }
        }

        Rectangle? _sourceRectangle;
        public Rectangle? sourceRectangle
        {
            get => _sourceRectangle;
            set
            {
                _sourceRectangle = value;
                updateTextureScale();
            }
        }

        public DSpriteNode(Texture2D texture)
        {
            load(texture);
        }

        public DSpriteNode(string assetName)
        {
            Texture2D texture = loadTexture2D(assetName);
            load(texture);
        }

        void load(Texture2D someTexture)
        {
            texture = someTexture;
            size = textureSize;
            color = Color.White;
            anchorPoint = new Vector2(0.5f, 0.5f);
        }

        internal override void draw(SpriteBatch spriteBatch, Vector2 currentPosition, float currentRotation, Vector2 currentScale, float currentAlpha)
        {
            beforeDraw(currentPosition, rotation, currentScale, currentAlpha);

            spriteBatch.Draw(
                _texture,
                drawPosition,
                _sourceRectangle,
                color * drawAlpha,
                drawRotation,
                origin,
                drawScale * textureScale,
                effects,
                layerDepth);

            drawChildren(spriteBatch);
        }

        void updateOrigin()
        {
            origin = new Vector2(textureSize.X * _anchorPoint.X, textureSize.Y * _anchorPoint.Y);
        }

        void updateTextureSize()
        {
            textureSize = new Vector2(_texture.Width, _texture.Height);
            updateOrigin();

            if (_sourceRectangle == null)
            {
                updateTextureScale();
            }
        }

        void updateTextureScale()
        {
            if (_sourceRectangle != null)
            {
                textureScale = new Vector2(_size.X / _sourceRectangle.Value.Width, _size.Y / _sourceRectangle.Value.Height);
            }
            else
            {
                textureScale = _size / textureSize;
            }
        }
    }
}
