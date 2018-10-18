using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Hydra.Scenes;

#if __IOS__ || __ANDROID__
using Microsoft.Xna.Framework.Input.Touch;
#endif

namespace Hydra
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    class Game1 : Game
    {
        internal GraphicsDeviceManager graphicsDeviceManager;
        internal SpriteBatch spriteBatch;

        internal SpriteSortMode sortMode = SpriteSortMode.Deferred;
        internal BlendState blendState = BlendState.AlphaBlend;
        internal SamplerState samplerState = SamplerState.LinearClamp;
        internal DepthStencilState depthStencilState = DepthStencilState.None;
        internal RasterizerState rasterizerState = RasterizerState.CullCounterClockwise;
        internal Effect effect = null;
        internal Matrix transformMatrix;

        Dictionary<int, Touch> touches;
        MouseState lastMouseState;

        public ContentManager songManager;

        internal static Game1 current;

        public Game1()
        {
            new MemoryCard().loadGame();

            graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            songManager = new ContentManager(Content.ServiceProvider, "Content");
            Music.sharedInstance.contentManager = songManager;

            samplerState = SamplerState.LinearClamp;

            touches = new Dictionary<int, Touch>();

            current = this;
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
#endif
            graphicsDeviceManager.ApplyChanges();

            SKScene.current = new GameScene();

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
            Vector2 defaultSize = SKScene.defaultSize;
            Vector2 currentSize = Vector2.Zero;

            graphicsDeviceManager.PreferredBackBufferWidth = Window.ClientBounds.Width;
            graphicsDeviceManager.PreferredBackBufferHeight = Window.ClientBounds.Height;

            float xScale = graphicsDeviceManager.PreferredBackBufferWidth / defaultSize.X;
            float yScale = graphicsDeviceManager.PreferredBackBufferHeight / defaultSize.Y;
            float scale = Math.Min(xScale, yScale);

            currentSize.X = graphicsDeviceManager.PreferredBackBufferWidth / scale;
            currentSize.Y = graphicsDeviceManager.PreferredBackBufferHeight / scale;

            SKScene.translate.X = (currentSize.X - defaultSize.X) / 2.0f;
            SKScene.translate.Y = (currentSize.Y - defaultSize.Y) / 2.0f;

            transformMatrix = Matrix.CreateScale(scale);
            SKScene.currentSize = currentSize;

            if (SKScene.current != null)
            {
                SKScene.current.updateSize();
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

            GraphicsDevice.PresentationParameters.RenderTargetUsage = RenderTargetUsage.PreserveContents;

            SKScene.current.contentManager = Content;
            SKScene.current.load();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

#if __MACOS__
            KeyboardState keyboardState = Keyboard.GetState();
            if ((keyboardState.IsKeyDown(Keys.LeftWindows) || keyboardState.IsKeyDown(Keys.RightWindows)) && keyboardState.IsKeyDown(Keys.Q))
            {
                Exit();
            }
#endif

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
                            SKScene.current.touchDown(touch);
                            touches.Add(touchLocation.Id, touch);
                        }
                        break;
                    case TouchLocationState.Moved:
                        {
                            Touch touch = touches[touchLocation.Id];
                            if (position != touch.lastPosition) {
                                touch.moved(position);
                                SKScene.current.touchMoved(touch);    
                            }
                        }
                        break;
                    case TouchLocationState.Released:
                        {
                            Touch touch = touches[touchLocation.Id];
                            touch.up(position);
                            SKScene.current.touchUp(touch);
                            touchUp(touch);
                            touches.Remove(touchLocation.Id);
                        }
                        break;
                    case TouchLocationState.Invalid:
                        break;
                }
            }
#else

            MouseState mouseState = Mouse.GetState();

            Vector2 lastPosition = new Vector2(
                lastMouseState.X / transformMatrix.M11,
                lastMouseState.Y / transformMatrix.M22);
            Vector2 position = new Vector2(
                mouseState.X / transformMatrix.M11,
                mouseState.Y / transformMatrix.M22);

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (lastMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (position != lastPosition)
                    {
                        Touch touch = touches[0];
                        touch.moved(position);
                        SKScene.current.touchMoved(touch);
                    }
                }
                else
                {
                    Touch touch = new Touch(position);
                    SKScene.current.touchDown(touch);
                    touches.Add(0, touch);
                }
            }
            else
            {
                if (lastMouseState.LeftButton == ButtonState.Pressed)
                {
                    Touch touch = touches[0];
                    touch.up(position);
                    SKScene.current.touchUp(touch);
                    touchUp(touch);
                    touches.Remove(0);
                }
            }

            lastMouseState = mouseState;
#endif

            SKScene.currentTime = (float)gameTime.TotalGameTime.TotalSeconds;
            SKScene.elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            SKScene.current.update();
            SKScene.current.camera?.update();

            base.Update(gameTime);
        }

        void touchUp(Touch touch)
        {
            foreach (Button button in SKScene.current.buttonList)
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
            graphicsDeviceManager.GraphicsDevice.Clear(SKScene.backgroundColor);

            spriteBatch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, transformMatrix);
            SKScene.current.draw();
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
