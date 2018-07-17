using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using FarseerPhysics;

namespace Hydra
{
    public class SKScene : SKNode
    {
        internal static SKScene current;
        internal static Color backgroundColor = GameColors.backgroundColor;
        internal static Vector2 translate;
        internal static Vector2 currentSize;
        internal static Vector2 defaultSize = new Vector2(667, 375);

        internal ContentManager contentManager;

        internal List<Control> controlList = new List<Control>();
        internal List<Label> labelList = new List<Label>();
        internal List<Button> buttonList = new List<Button>();
        internal List<SKEmitterNode> emitterNodeList = new List<SKEmitterNode>();

        internal CameraNode camera;

        internal PhysicsWorld physicsWorld;
        internal GameWorld gameWorld;

        internal static float currentTime;
        internal static float elapsedTime;

        internal virtual void load()
        {
            physicsWorld = new PhysicsWorld();
            gameWorld = new GameWorld();

            Control control = new Control("", defaultSize.X / 2, defaultSize.Y / 2);
            control.setAlignment(HorizontalAlignment.center, VerticalAlignment.center);
            addChild(control);

            control.addChild(gameWorld);
        }

        internal Texture2D Texture2D(string assetName)
        {
            Texture2D texture;

            try
            {
                texture = contentManager.Load<Texture2D>("Texture2D/" + assetName);
            }
            catch (ContentLoadException)
            {
                texture = contentManager.Load<Texture2D>("Texture2D/null");
            }

            return texture;
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

        internal virtual void touchDown(Touch touch)
        {
            foreach (Button button in buttonList)
            {
                if (button.parent != null)
                {
                    if (button.contains(touch.locationIn(button.parent)))
                    {
                        button.state = ButtonState.Pressed;
                        button.touchDown();
                    }
                }
            }
        }

        internal virtual void touchMoved(Touch touch)
        {

        }

        internal virtual void touchUp(Touch touch)
        {

        }

        internal void presentScene(SKScene scene)
        {
            contentManager.Unload();
            scene.contentManager = contentManager;
            current = scene;
            scene.load();
            GC.Collect();
        }

        internal void updateSize()
        {
            foreach (Control control in controlList)
            {
                control.resetPosition();
            }

            foreach (Label label in labelList)
            {
                label.resetPosition();
            }
        }

        internal void draw()
        {
            evaluateActions(elapsedTime);

            didEvaluateActions();

            physicsWorld.Step(elapsedTime);

            didSimulatePhysics();

            didFinishUpdate();

            foreach (var emitterNode in emitterNodeList)
            {
                emitterNode.update(currentTime, elapsedTime);
            }

            base.draw(Vector2.Zero, 1.0f, Vector2.One);
        }

        internal void addChild(Box box)
        {
            addChild(box.blackSpriteNode);
            base.addChild(box);
        }
    }
}
