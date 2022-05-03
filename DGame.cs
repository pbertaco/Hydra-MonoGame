using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dragon
{
    public class DGame : Game
    {
        DScene scene;

        GraphicsDeviceManager graphicsDeviceManager;
        SpriteBatch spriteBatch;

        internal SpriteSortMode sortMode = SpriteSortMode.Deferred;
        internal BlendState blendState = null;
        internal SamplerState samplerState = null;
        internal DepthStencilState depthStencilState = null;
        internal RasterizerState rasterizerState = null;
        internal Effect effect = null;
        internal Matrix? transformMatrix = null;

        internal static DGame current;

        public DGame(DScene someScene)
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            scene = someScene;
            current = this;

            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }

        protected override void Initialize()
        {
            updateSize();

            Window.OrientationChanged += (sender, e) =>
            {
                updateSize();
            };

            Window.ClientSizeChanged += (sender, e) =>
            {
                updateSize();
            };

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            scene.load();
        }

        void updateSize()
        {
            Vector2 viewSize = new Vector2(Window.ClientBounds.Width, Window.ClientBounds.Height);

            Console.WriteLine(viewSize);

            graphicsDeviceManager.PreferredBackBufferWidth = (int)viewSize.X;
            graphicsDeviceManager.PreferredBackBufferHeight = (int)viewSize.Y;

            Vector2 sceneSize = scene.size;

            float xScale = viewSize.X / sceneSize.X;
            float yScale = viewSize.Y / sceneSize.Y;
            float scale = Math.Min(xScale, yScale);
            float x = viewSize.X / 2.0f;
            float y = viewSize.Y / 2.0f;

            scene.currentPosition = new Vector2(x, y);
            scene.currentScale = new Vector2(scale);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(scene.backgroundColor);
            spriteBatch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, transformMatrix);
            scene.draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        internal Texture2D loadTexture2D(string assetName)
        {
            Texture2D texture;

            try
            {
                texture = Content.Load<Texture2D>($"Texture2D/{assetName}");
            }
            catch (Exception)
            {
                texture = Content.Load<Texture2D>("Texture2D/MissingResource");
            }

            return texture;
        }

        internal SpriteFont loadSpriteFont(string assetName)
        {
            SpriteFont spriteFont;

            try
            {
                spriteFont = Content.Load<SpriteFont>($"SpriteFont/{assetName}");
            }
            catch (Exception)
            {
                spriteFont = Content.Load<SpriteFont>("SpriteFont/MissingResource");
            }

            return spriteFont;
        }
    }
}
