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
        internal EventHandler touchUpEvent;

        Rectangle bounds;

        internal ButtonState state;

        SKSpriteNode icon;

        public Button(string assetName, float x, float y) : base(assetName, x, y)
        {
            state = ButtonState.Released;
            bounds = texture2D.Bounds;
            SKScene.current.buttonList.Add(this);
        }

        public Button(string assetName, float x = 0, float y = 0,
                       HorizontalAlignment horizontalAlignment = HorizontalAlignment.left,
                       VerticalAlignment verticalAlignment = VerticalAlignment.top) : this(assetName, x, y)
        {
            setAlignment(horizontalAlignment, verticalAlignment);
        }

		internal void touchDown()
        {
            scale = new Vector2(0.95f, 0.95f);
            position = new Vector2(position.X + (bounds.Width / 2) * 0.05f, position.Y + (bounds.Height / 2) * 0.05f);
        }

        internal void touchUp()
        {
            scale = new Vector2(1, 1);
            resetPosition();
        }

        internal void touchUpInside()
        {
            touchUpEvent.Invoke(this, EventArgs.Empty);
        }

        internal bool contains(Vector2 position)
        {
            bounds.X = (int)this.position.X;
            bounds.Y = (int)this.position.Y;
            return bounds.Contains(position);
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

        internal void set(Color color, BlendState blendState)
        {
            this.color = color;
            this.blendState = blendState;

            if (icon != null)
            {
                icon.color = color;
                icon.blendState = blendState;
            }
        }
    }
}
