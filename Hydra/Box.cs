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

namespace Hydra
{
    class Box : Button
    {
        internal Control blackSpriteNode;

        public Box(string assetName,
                   float? x = null,
                   float? y = null,
                   HorizontalAlignment horizontalAlignment = HorizontalAlignment.center,
                   VerticalAlignment verticalAlignment = VerticalAlignment.center,
                   bool animated = true)
            : base(assetName,
                   x ?? 0,
                   y ?? 0,
                   horizontalAlignment,
                   verticalAlignment)
        {
            alignCenter(x, y);

            blackSpriteNode = new Control("")
            {
                color = Color.Black,
                alpha = 0.5f,
                size = SKScene.scale
            };

            if (animated)
            {
                pop();
            }
        }

        void pop()
        {
            resetPosition();

            float duration = 1.0f;

            float x = position.X + size.X / 2.0f;
            float y = position.Y + size.Y / 2.0f;

            position = new Vector2(x, y);
            scale = new Vector2(0.01f, 0.01f);

            run(SKAction.group(new[] {
                SKAction.scaleTo(1.0f, duration).with(Easing.ElasticEaseOut),
                SKAction.moveTo(positionWith(sketchPosition), duration).with(Easing.ElasticEaseOut)
            }));
        }

        void alignCenter(float? x, float? y)
        {
            if (x == null)
            {
                sketchPosition = new Vector2(SKScene.defaultSize.X / 2.0f - size.X / 2.0f, sketchPosition.Y);
            }

            if (y == null)
            {
                sketchPosition = new Vector2(sketchPosition.X, SKScene.defaultSize.Y / 2.0f - size.Y / 2.0f);
            }
        }

        internal override void touchDown()
        {
        }

        internal override void touchUp()
        {
        }

        internal override void resetPosition()
        {
            base.resetPosition();

            if (blackSpriteNode != null)
            {
                blackSpriteNode.size = SKScene.scale;
                blackSpriteNode.scale = SKScene.scale;
            }
        }

        internal override void removeFromParent()
        {
            base.removeFromParent();
            blackSpriteNode.removeFromParent();
        }
    }
}
