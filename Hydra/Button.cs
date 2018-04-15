using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hydra
{
    class Button : Control
    {
        internal EventHandler touchUpEvent;

        Rectangle bounds;

        internal ButtonState state;

        public Button(string assetName, float x, float y) : base(assetName, x, y)
        {
            state = ButtonState.Released;
            bounds = texture2D.Bounds;
            Scene.current.buttonList.Add(this);
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
    }
}
