using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Hydra
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphicsDeviceManager;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
#if !__IOS__ && !__TVOS__
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
#endif
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            graphicsDeviceManager.GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}
