using System;
using System.Collections.Generic;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace Dragon
{
    public class DScene : DNode
    {
        internal DCameraNode cameraNode;
        internal static DScene current;
        internal static Color backgroundColor;
        internal Vector2 viewSize;
        internal World physicsWorld;
        internal List<DControl> controlList = new List<DControl>();

        Vector2 viewScale;
        Vector2 viewPosition;

        Vector2 _size;

        internal Vector2 size
        {
            get => _size;
            set
            {
                _size = value;
                updateSize(viewSize);
            }
        }

        public DScene(Vector2? someSize = null)
        {
            size = someSize ?? Vector2.One;
            backgroundColor = new Color(0.15f, 0.15f, 0.15f, 1.0f);
            physicsWorld = new World(new Vector2(0.0f, 9.8f));
        }

        internal virtual void load()
        {
        }

        internal virtual void update()
        {
        }

        internal virtual void didFinishUpdate()
        {
        }

        internal virtual void didSimulatePhysics()
        {
        }

        internal virtual void didEvaluateActions()
        {
        }

        internal void draw()
        {
            evaluateActions(DGame.current.elapsedTime);
            didEvaluateActions();
            physicsWorld.Step(DGame.current.elapsedTime);
            didSimulatePhysics();
            didFinishUpdate();
            draw(viewPosition, 0.0f, viewScale, 1.0f);
        }

        internal void updateSize(Vector2 someViewSize)
        {
            if (someViewSize == Vector2.Zero)
            {
                return;
            }

            viewSize = someViewSize;
            viewScale = new Vector2(Math.Min(viewSize.X / size.X, viewSize.Y / size.Y));
            viewPosition = (viewSize / viewScale - size) * viewScale / 2f;

            foreach (DControl control in controlList)
            {
                control.resetPosition();
            }
        }

        internal virtual void touchDown(DTouch touch)
        {
        }

        internal virtual void touchMoved(DTouch touch)
        {
        }

        internal virtual void touchUp(DTouch touch)
        {
        }

        internal void presentScene(DScene scene)
        {
            DGame.current.presentScene(scene);
        }
    }
}
