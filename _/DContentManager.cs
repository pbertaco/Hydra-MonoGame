using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Dragon
{
    public class DContentManager
    {
        ContentManager contentManager;
        GraphicsDevice graphicsDevice;

        Dictionary<string, Texture2D> loadedTexture2D;
        Dictionary<string, SpriteFont> loadedSpriteFont;

        public DContentManager(ContentManager someContentManager, GraphicsDevice someGraphicsDevice)
        {
            contentManager = someContentManager;
            graphicsDevice = someGraphicsDevice;
        }

        internal SpriteFont loadSpriteFont(string assetName)
        {
            if (assetName == null || assetName == "")
            {
                return newSpriteFont();
            }

            return newSpriteFont(assetName);
        }

        internal Texture2D loadTexture2D(string assetName)
        {
            if (assetName == null || assetName == "")
            {
                return newTexture2D(1, 1, new Color[] { Color.White });
            }

            return newTexture2D(assetName);
        }

        SpriteFont newSpriteFont(string assetName, bool handleException = true)
        {
            SpriteFont spriteFont = loadedSpriteFont.ContainsKey(assetName) ? loadedSpriteFont[assetName] : null;

            if (spriteFont != null)
            {
                return spriteFont;
            }

            try
            {
                spriteFont = contentManager.Load<SpriteFont>(assetName);
                loadedSpriteFont[assetName] = spriteFont;
            }
            catch (Exception)
            {
                if (handleException)
                {
                    spriteFont = newSpriteFont();
                }
            }

            return spriteFont;
        }

        SpriteFont newSpriteFont()
        {
            return newSpriteFont("MissingSpriteFont", false);
        }

        Texture2D newTexture2D(string assetName, bool handleException = true)
        {
            Texture2D texture = loadedTexture2D.ContainsKey(assetName) ? loadedTexture2D[assetName] : null;

            if (texture != null)
            {
                return texture;
            }

            try
            {
                texture = contentManager.Load<Texture2D>(assetName);
                loadedTexture2D[assetName] = texture;
            }
            catch (Exception)
            {
                if (handleException)
                {
                    texture = newTexture2D("MissingTexture2D", false);
                }
            }

            return texture;
        }

        internal Texture2D newTexture2D<T>(int width, int height, T[] data) where T : struct
        {
            Texture2D texture = new Texture2D(graphicsDevice, width, height);
            texture.SetData(data);
            return texture;
        }

        internal void unload()
        {
            loadedTexture2D = new Dictionary<string, Texture2D>();
            loadedSpriteFont = new Dictionary<string, SpriteFont>();
            contentManager.Unload();
        }
    }
}
