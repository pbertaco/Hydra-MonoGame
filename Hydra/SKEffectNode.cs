using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using FarseerPhysics;

namespace Hydra
{
    class SKEffectNode : SKSpriteNode
    {
        internal static RenderTarget2D renderTarget;

        public SKEffectNode(Color someColor, Vector2 someSize) : base("")
        {
            color = Color.White;
            blendState = Game1.current.blendState;
            renderTarget = new RenderTarget2D(Game1.current.GraphicsDevice, (int)someSize.X, (int)someSize.Y);
            origin = someSize * 0.5f;
        }

        public SKEffectNode(Vector2 someSize) : this(Color.White, someSize)
        {

        }

        internal override void beforeDraw()
        {
            base.beforeDraw();

            Game1 Game1 = Game1.current;

            if (blendState != Game1.blendState)
            {
                Game1.blendState = blendState;
            }

            Game1.spriteBatch.Begin(Game1.sortMode, Game1.blendState, Game1.samplerState, Game1.depthStencilState, Game1.rasterizerState, Game1.effect, Game1.transformMatrix);
        }

        internal override void draw(Vector2 currentPosition, float currentAlpha, Vector2 currentScale)
        {
            if (isHidden || alpha <= 0.0f)
            {
                return;
            }

            Game1 Game1 = Game1.current;

            Game1.spriteBatch.End();

            Game1.graphicsDeviceManager.GraphicsDevice.SetRenderTarget(renderTarget);
            Game1.graphicsDeviceManager.GraphicsDevice.Clear(Color.Transparent);
            Game1.spriteBatch.Begin();

            drawChildren(origin, 1.0f, Vector2.One);

            Game1.spriteBatch.End();
            Game1.graphicsDeviceManager.GraphicsDevice.SetRenderTarget(null);

            beforeDraw();

            Game1.spriteBatch.Draw(renderTarget,
                                   currentPosition + position * currentScale,
                                   sourceRectangle,
                                   drawColor * alpha * currentAlpha,
                                   zRotation,
                                   origin,
                                   currentScale * drawScale,
                                   effects,
                                   layerDepth);
        }
    }
}
