using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dragon
{
    public class DNode
    {
        internal static Random random = new Random();

-       internal string name;
        internal Vector2 position;
        internal float rotation;
        internal Vector2 scale;
+       internal DNode parent;
+       internal List<DNode> children;
        internal Vector2 drawPosition;
        internal float drawRotation;
        internal Vector2 drawScale;
        internal float drawAlpha;
-       internal bool isHidden;
-       internal float alpha;
-       internal Dictionary<string, DAction> actions;
-       internal List<string> actionsToRemove;
-       internal DPhysicsBody physicsBody;

        public DNode()
        {
            position = Vector2.Zero;
            rotation = 0;
            scale = Vector2.One;
-           actions = new Dictionary<string, DAction>();
-           actionsToRemove = new List<string>();
+           alpha = 1;
+           isHidden = false;
+           children = new List<DNode>();
        }

        internal virtual void update()
        {
            foreach (DNode node in children)
            {
                node.update();
            }
        }

        internal virtual void beforeDraw(Vector2 currentPosition, float currentRotation, Vector2 currentScale, float currentAlpha)
        {
            drawPosition = currentPosition + position.rotateBy(currentRotation) * currentScale;
            drawRotation = currentRotation + rotation;
            drawScale = currentScale * scale;
            drawAlpha = currentAlpha * alpha;
        }

        internal void drawChildren()
        {
            foreach (DNode node in children)
            {
                node.draw(drawPosition, drawRotation, drawScale, drawAlpha);
            }
        }

        internal virtual void draw(Vector2 currentPosition, float currentRotation, Vector2 currentScale, float currentAlpha)
        {
            if (isHidden || alpha <= 0.0f)
            {
                return;
            }

            beforeDraw(currentPosition, currentRotation, currentScale, currentAlpha);
            drawChildren();
        }

        internal void addChild(DNode node)
        {
            children.Add(node);
            node.parent = this;
        }

        internal void insertChild(DNode node, int index)
        {
            children.Insert(index, node);
            node.parent = this;
        }

        internal void removeChildrenIn(IEnumerable<DNode> nodeList)
        {
            foreach (DNode node in nodeList)
            {
                if (node.parent == this)
                {
                    node.removeFromParent();
                }
            }
        }

        internal void removeAllChildren()
        {
            for (int i = children.Count - 1; i >= 0; i--)
            {
                children[i].removeFromParent();
            }
        }

        internal virtual void removeFromParent()
        {
            if (parent != null)
            {
                parent.children.Remove(this);
                parent = null;
            }
        }

        internal void moveToParent(DNode node)
        {
            removeFromParent();
            node.addChild(this);
        }

        internal void run(DAction action)
        {
            run(action, $"{DGame.current.currentTime}{random.NextDouble()}");
        }

        internal void run(DAction action, string key)
        {
            DAction copy = action.copy();
            copy.runOnNode(this);

            if (actions.ContainsKey(key))
            {
                actions[key] = copy;
            }
            else
            {
                actions.Add(key, copy);
            }
        }

        internal void run(DAction action, Action completionBlock)
        {
            run(action, $"{DScene.current}{random.NextDouble()}");
        }

        internal bool hasActions()
        {
            return actions.Count > 0;
        }

        internal DAction actionForKey(string key)
        {
            return actions[key];
        }

        internal void removeActionForKey(string key)
        {
            actions.Remove(key);
        }

        internal void removeAllActions()
        {
            actions.Clear();
        }

        internal void evaluateActions(float dt)
        {
            foreach (KeyValuePair<string, DAction> keyValuePair in actions)
            {
                DAction action = keyValuePair.Value;
                action.evaluateWithNode(this, dt);

                if (action.elapsed > action.duration)
                {
                    actionsToRemove.Add(keyValuePair.Key);
                }
            }

            if (actionsToRemove.Count > 0)
            {
                foreach (string key in actionsToRemove)
                {
                    actions.Remove(key);
                }
                actionsToRemove.Clear();
            }

            for (int i = children.Count - 1; i >= 0; i--)
            {
                children[i].evaluateActions(dt);
            }
        }

        internal Texture2D loadTexture2D(string assetName)
        {
            return DGame.current.loadTexture2D(assetName);
        }

        internal SpriteFont loadSpriteFont(string assetName)
        {
            return DGame.current.loadSpriteFont(assetName);
        }
    }
}
