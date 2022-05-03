using System;
using Dragon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Untitled
{
    public class GameScene : DScene
    {
        static Vector2 defaultSize = new Vector2(320, 200);

        public GameScene() : base(defaultSize)
        {
            backgroundColor = Color.White;
        }

        internal override void load()
        {
            DGame game = DGame.current;
            game.samplerState = SamplerState.PointClamp;
            game.blendState = BlendState.AlphaBlend;


            DSpriteNode hud = new DSpriteNode("background")
            {
                anchorPoint = new Vector2(0, 0),
                position = new Vector2(-size.X / 2.0f, -size.Y / 2.0f),
                color = new Color(1.0f, 0.0f, 0.0f, 0.5f),
                alpha = 0.1f
            };
            addChild(hud);

            DSpriteNode spriteNode = new DSpriteNode("")
            {
                anchorPoint = new Vector2(0, 0),
                color = Color.Green
            };
            hud.addChild(spriteNode);
        }
    }
}

