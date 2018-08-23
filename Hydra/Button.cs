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

        internal ButtonState state;

        SKSpriteNode icon;
        Label label;

        public Button(string assetName, float x = 0.0f, float y = 0.0f,
                       HorizontalAlignment horizontalAlignment = HorizontalAlignment.left,
                      VerticalAlignment verticalAlignment = VerticalAlignment.top) : base(assetName, x, y, horizontalAlignment, verticalAlignment)
        {
            state = ButtonState.Released;

            setAlignment(horizontalAlignment, verticalAlignment);

            SKScene.current.buttonList.Add(this);
        }

        internal virtual void touchDown()
        {
            resetPosition();
            scale = Vector2.One;

            float duration = 0.125f;
            float x = position.X + (size.X / 2.0f) * 0.05f;
            float y = position.Y + (size.Y / 2.0f) * 0.05f;

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

        internal void setIcon(string assetName)
        {
            Texture2D texture = SKScene.current.Texture2D(assetName);
            SKSpriteNode newIcon = new SKSpriteNode(texture, color, texture.Bounds.Size.ToVector2());

            newIcon.setScaleToFit(size);

            newIcon.position = new Vector2(size.X / 2.0f, size.Y / 2.0f);

            addChild(newIcon);
            icon?.removeFromParent();
            icon = newIcon;

        }

        internal void setLabel(Label someLabel) {
            label = someLabel;
            label.sketchPosition += size / 2.0f;
            label.resetPosition();
            addChild(label);
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
