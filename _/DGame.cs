using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Dragon
{
    public class DGame : Game
    {
        internal static DGame current;

        internal SpriteBatch spriteBatch;
        internal DContentManager contentManager;
        internal DInputManager inputManager;
        internal float currentTime;
        internal float elapsedTime;

        internal SpriteSortMode sortMode = SpriteSortMode.Deferred;
        internal BlendState blendState = null;
        internal SamplerState samplerState = null;
        internal DepthStencilState depthStencilState = null;
        internal RasterizerState rasterizerState = null;
        internal Effect effect = null;
        internal Matrix? transformMatrix = null;

        GraphicsDeviceManager graphicsDeviceManager;
        DScene scene;

        internal Vector2 size => new Vector2(Window.ClientBounds.Width, Window.ClientBounds.Height);

        public DGame(DScene someScene)
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            scene = someScene;
            inputManager = new DInputManager();
            current = this;
        }

        protected override void Initialize()
        {
            initializeWindowEventHandlers();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            contentManager = new DContentManager(Content, GraphicsDevice);
            presentScene(scene);
        }

        protected override void Update(GameTime gameTime)
        {
            inputManager.update();
            currentTime = (float)gameTime.TotalGameTime.TotalSeconds;
            elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            scene.update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(DScene.backgroundColor);
            spriteBatch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, transformMatrix);
            scene.draw();
            spriteBatch.End();
            base.Draw(gameTime);
        }

        internal void presentScene(DScene scene)
        {
            DGame.current.contentManager.unload();
            this.scene = scene;
            DScene.current = scene;
            scene.load();
            scene.updateSize(size);
            GC.Collect();
        }

        void initializeWindowEventHandlers()
        {
            EventHandler<EventArgs> eventHandler = (sender, e) =>
            {
                graphicsDeviceManager.PreferredBackBufferWidth = (int)size.X;
                graphicsDeviceManager.PreferredBackBufferHeight = (int)size.Y;
                graphicsDeviceManager.ApplyChanges();
                scene.updateSize(size);
            };

            Window.OrientationChanged += eventHandler;
            Window.ClientSizeChanged += eventHandler;
        }
    }
}
