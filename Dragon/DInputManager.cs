using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Dragon
{
    public class DInputManager
    {
        Dictionary<int, DTouch> touches;
        MouseState lastMouseState;

        public DInputManager()
        {
            touches = new Dictionary<int, DTouch>();
        }

#if macOS
        internal void update()
        {
            updateMouse();
        }

        void updateMouse()
        {
            MouseState mouseState = Mouse.GetState();

            Vector2 lastPosition = new Vector2(lastMouseState.X, lastMouseState.Y);
            Vector2 position = new Vector2(mouseState.X, mouseState.Y);

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (lastMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (position != lastPosition)
                    {
                        DTouch touch = touches[0];
                        touch.moved(position);
                        DScene.current.touchMoved(touch);
                    }
                }
                else
                {
                    DTouch touch = new DTouch(position);
                    DScene.current.touchDown(touch);
                    touches.Add(0, touch);
                }
            }
            else
            {
                if (lastMouseState.LeftButton == ButtonState.Pressed)
                {
                    DTouch touch = touches[0];
                    touch.up(position);
                    DScene.current.touchUp(touch);
                    touches.Remove(0);
                }
            }

            lastMouseState = mouseState;
        }
#endif

#if iOS || Android
        internal void update()
        {
            updateTouchPanel();
        }

        void updateTouchPanel()
        {
            TouchCollection touchCollection = TouchPanel.GetState();

            foreach (TouchLocation touchLocation in touchCollection)
            {
                Vector2 position = touchLocation.Position;

                switch (touchLocation.State)
                {
                    case TouchLocationState.Pressed:
                        {
                            DTouch touch = new DTouch(position);
                            DScene.current.touchDown(touch);
                            touches.Add(touchLocation.Id, touch);
                        }
                        break;
                    case TouchLocationState.Moved:
                        {
                            DTouch touch = touches[touchLocation.Id];
                            if (position != touch.lastPosition)
                            {
                                touch.moved(position);
                                DScene.current.touchMoved(touch);
                            }
                        }
                        break;
                    case TouchLocationState.Released:
                        {
                            DTouch touch = touches[touchLocation.Id];
                            touch.up(position);
                            DScene.current.touchUp(touch);
                            touches.Remove(touchLocation.Id);
                        }
                        break;
                    case TouchLocationState.Invalid:
                        touches = new Dictionary<int, DTouch>();
                        break;
                }
            }
        }

#endif
    }
}
