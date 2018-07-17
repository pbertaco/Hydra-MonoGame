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
    class Button : Control
    {
        internal List<Action> touchUpEvent;

        Rectangle bounds;

        internal ButtonState state;

        SKSpriteNode icon;

        public Button(string assetName, float x = 0, float y = 0,
                       HorizontalAlignment horizontalAlignment = HorizontalAlignment.left,
                      VerticalAlignment verticalAlignment = VerticalAlignment.top) : base(assetName, x, y, horizontalAlignment, verticalAlignment)
        {
            state = ButtonState.Released;
            bounds = texture2D.Bounds;

            setAlignment(horizontalAlignment, verticalAlignment);

            SKScene.current.buttonList.Add(this);
        }

        internal virtual void touchDown()
        {
            resetPosition();
            scale = Vector2.One;

            float duration = 0.125f;
            float x = position.X + (size.X / 2) * 0.05f;
            float y = position.Y + (size.Y / 2) * 0.05f;

            run(SKAction.group(new[] {
                SKAction.scaleTo(0.95f, duration),
                SKAction.moveTo(new Vector2(x, y), duration)
            }), "Button.touchDown");
        }

        internal virtual void touchUp()
        {
            float duration = 0.125f;

            run(SKAction.group(new[] {
                SKAction.scaleTo(1.0f, duration),
                SKAction.moveTo(positionWith(sketchPosition), duration)
            }), "Button.touchUp");
        }

        internal void touchUpInside()
        {
            foreach (Action handler in touchUpEvent)
            {
                handler();
            }
        }

        internal bool contains(Vector2 somePosition)
        {
            bounds.X = (int)position.X;
            bounds.Y = (int)position.Y;
            return bounds.Contains(somePosition);
        }

        internal void setIcon(string assetName)
        {
            Texture2D texture = SKScene.current.Texture2D(assetName);
            SKSpriteNode newIcon = new SKSpriteNode(texture, color, texture.Bounds.Size.ToVector2());

            newIcon.setScaleToFit(size);

            newIcon.position = new Vector2(size.X / 2, size.Y / 2);

            addChild(newIcon);
            icon?.removeFromParent();
            icon = newIcon;

        }

        internal void addHandler(Action action)
        {
            if (touchUpEvent == null)
            {
                touchUpEvent = new List<Action>();
            }

            touchUpEvent.Add(action);
        }

        internal void set(Color someColor, BlendState someBlendState)
        {
            color = someColor;
            blendState = someBlendState;

            if (icon != null)
            {
                icon.color = someColor;
                icon.blendState = someBlendState;
            }
        }
    }
}
