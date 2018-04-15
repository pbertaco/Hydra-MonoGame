using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Hydra.Scenes;
using Microsoft.Xna.Framework.Input.Touch;

namespace Hydra
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphicsDeviceManager;
        internal static SpriteBatch spriteBatch;

        internal static SpriteSortMode sortMode = SpriteSortMode.Deferred;
        internal static BlendState blendState = BlendState.AlphaBlend;
        internal static SamplerState samplerState = SamplerState.LinearClamp;
        internal static DepthStencilState depthStencilState = DepthStencilState.None;
        internal static RasterizerState rasterizerState = RasterizerState.CullCounterClockwise;
        internal static Effect effect = null;
        internal static Matrix transformMatrix;

        Dictionary<int, Touch> touches;
        MouseState lastMouseState;

        public Game1()
        {
            new MemoryCard().loadGame();

            graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            samplerState = SamplerState.PointClamp;

            touches = new Dictionary<int, Touch>();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphicsDeviceManager.SupportedOrientations =
                DisplayOrientation.LandscapeLeft |
                DisplayOrientation.LandscapeRight |
                DisplayOrientation.Portrait |
                DisplayOrientation.PortraitDown;

#if __IOS__ || __ANDROID__
            graphicsDeviceManager.IsFullScreen = true;
            graphicsDeviceManager.PreferredBackBufferWidth = graphicsDeviceManager.GraphicsDevice.DisplayMode.Width;
            graphicsDeviceManager.PreferredBackBufferHeight = graphicsDeviceManager.GraphicsDevice.DisplayMode.Height;
#else
            graphicsDeviceManager.PreferredBackBufferWidth = (int)(graphicsDeviceManager.GraphicsDevice.DisplayMode.Width * 0.9);
            graphicsDeviceManager.PreferredBackBufferHeight = (int)(graphicsDeviceManager.GraphicsDevice.DisplayMode.Height * 0.9);
            IsMouseVisible = true;
            Window.AllowUserResizing = true;

            //graphics.IsFullScreen = true;
            //graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            //graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            //IsMouseVisible = true;
#endif
            graphicsDeviceManager.ApplyChanges();

            GameScene.current = new LoadScene();

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

        void updateSize()
        {
            Vector2 defaultSize = GameScene.defaultSize;

            Vector2 currentSize = Vector2.Zero;

#if __IOS__ || __ANDROID__
            float xScale = graphicsDeviceManager.GraphicsDevice.DisplayMode.Width / defaultSize.X;
            float yScale = graphicsDeviceManager.GraphicsDevice.DisplayMode.Height / defaultSize.Y;
            float scale = Math.Min(xScale, yScale);

            currentSize.X = graphicsDeviceManager.GraphicsDevice.DisplayMode.Width / scale;
            currentSize.Y = graphicsDeviceManager.GraphicsDevice.DisplayMode.Height / scale;
#else
            graphicsDeviceManager.PreferredBackBufferWidth = Window.ClientBounds.Width;
            graphicsDeviceManager.PreferredBackBufferHeight = Window.ClientBounds.Height;

            float xScale = graphicsDeviceManager.PreferredBackBufferWidth / defaultSize.X;
            float yScale = graphicsDeviceManager.PreferredBackBufferHeight / defaultSize.Y;
            float scale = Math.Min(xScale, yScale);

            currentSize.X = graphicsDeviceManager.PreferredBackBufferWidth / scale;
            currentSize.Y = graphicsDeviceManager.PreferredBackBufferHeight / scale;
#endif

            GameScene.translate.X = (currentSize.X - defaultSize.X) / 2.0f;
            GameScene.translate.Y = (currentSize.Y - defaultSize.Y) / 2.0f;

            transformMatrix = Matrix.CreateScale(scale);
            GameScene.currentSize = currentSize;

            if (GameScene.current != null)
            {
                GameScene.current.updateSize();
            }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            GameScene.current.contentManager = Content;
            GameScene.current.load();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
#if __IOS__ || __ANDROID__
            TouchCollection touchCollection = TouchPanel.GetState();
            foreach (TouchLocation touchLocation in touchCollection)
            {
                Vector2 position = new Vector2(
                    touchLocation.Position.X / transformMatrix.M11,
                    touchLocation.Position.Y / transformMatrix.M22);
                
                switch (touchLocation.State)
                {
                    case TouchLocationState.Pressed: 
                        {
                            Touch touch = new Touch(position);
                            GameScene.current.touchDown(touch);
                            touches.Add(touchLocation.Id, touch);
                        }
                        break;
                    case TouchLocationState.Moved:
                        {
                            Touch touch = touches[touchLocation.Id];
                            touch.moved(position);
                            GameScene.current.touchMoved(touch);
                        }
                        break;
                    case TouchLocationState.Released:
                        {
                            Touch touch = touches[touchLocation.Id];
                            touch.up(position);
                            GameScene.current.touchUp(touch);
                            touchUp(touch);
                            touches.Remove(touchLocation.Id);
                        }
                        break;
                    case TouchLocationState.Invalid:
                        break;
                }
            }
#else
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            MouseState mouseState = Mouse.GetState();

            Vector2 position = new Vector2(
                mouseState.X / transformMatrix.M11,
                mouseState.Y / transformMatrix.M22);

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (lastMouseState.LeftButton == ButtonState.Pressed)
                {
                    Touch touch = touches[0];
                    touch.moved(position);
                    GameScene.current.touchMoved(touch);
                }
                else
                {
                    Touch touch = new Touch(position);
                    GameScene.current.touchDown(touch);
                    touches.Add(0, touch);
                }
            }
            else
            {
                if (lastMouseState.LeftButton == ButtonState.Pressed)
                {
                    Touch touch = touches[0];
                    touch.up(position);
                    GameScene.current.touchUp(touch);
                    touchUp(touch);
                    touches.Remove(0);
                }
            }

            lastMouseState = mouseState;
#endif

            GameScene.currentTime = (float)gameTime.TotalGameTime.TotalSeconds;
            GameScene.elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            GameScene.current.update();
            GameScene.current.camera?.update();

            base.Update(gameTime);
        }

        void touchUp(Touch touch)
        {
            foreach (Button button in GameScene.current.buttonList)
            {
                if (button.state == ButtonState.Pressed)
                {
                    button.touchUp();
                    if (button.parent != null && button.touchUpEvent != null)
                    {
                        if (button.contains(touch.locationIn(button.parent)))
                        {
                            button.touchUpInside();
                        }
                    }
                    button.state = ButtonState.Released;
                }
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphicsDeviceManager.GraphicsDevice.Clear(GameScene.backgroundColor);

            spriteBatch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, transformMatrix);
            GameScene.current.draw();
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
