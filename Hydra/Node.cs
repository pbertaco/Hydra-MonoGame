using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

using FarseerPhysics;

namespace Hydra
{
    public class Node
    {
        internal static Random random = new Random();

        internal Vector2 position;

        float _zRotation;
        internal float zRotation
        {
            get
            {
                return _zRotation;
            }
            set
            {
                _zRotation = value;
                if (physicsBody != null)
                {
                    physicsBody.Rotation = value;
                }
            }
        }

        internal bool isHidden;

        internal object userData;

        internal string name;

        internal Node parent;

        internal List<Node> children;

        internal PhysicsBody physicsBody;

        internal Dictionary<string, Action> actions;

        internal float alpha = 1.0f;

        public Node()
        {
            name = "";
            children = new List<Node>();
            actions = new Dictionary<string, Action>();
        }

        internal void run(Action action, string key)
        {
            Action copy = action.copy();
            copy.runOnNode(this);
            actions.Add(key, copy);
        }

        internal void run(Action action)
        {
            run(action, $"{GameScene.currentTime}{random.NextDouble()}");
        }

        internal void run(Action action, string key, Func<object> completionBlock)
        {
            run(action, key);
        }

        internal bool hasActions()
        {
            return actions.Count > 0;
        }

        internal Action actionForKey(string key)
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
            foreach (KeyValuePair<string, Action> keyValuePair in actions)
            {
                keyValuePair.Value.evaluateWithNode(this, dt);
            }

            foreach (var node in children)
            {
                node.evaluateActions(dt);
            }
        }

        internal void addChild(Node node)
        {
            children.Add(node);
            node.parent = this;
        }

        internal void insertChild(Node node, int index)
        {
            children.Insert(index, node);
            node.parent = this;
        }

        internal void removeChildren(IEnumerable<Node> nodes)
        {
            foreach (Node node in nodes)
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

            if (physicsBody != null)
            {
                GameScene.current.physicsWorld.RemoveBody(physicsBody);
            }
        }

        internal void moveToParent(Node node)
        {
            removeFromParent();
            node.addChild(this);
        }

        internal Node childNodeWithName(string name, bool recursive = true)
        {
            foreach (Node node in children)
            {
                if (node.name == name)
                {
                    return node;
                }

                if (recursive)
                {
                    Node childNode = node.childNodeWithName(name, recursive);

                    if (childNode != null)
                    {
                        return childNode;
                    }
                }
            }

            return null;
        }

        internal void enumerateChildNodesWithName(string name, Action<Node> action, bool recursive = true)
        {
            foreach (Node node in children)
            {
                if (node.name == name)
                {
                    action(node);
                }

                if (recursive)
                {
                    node.enumerateChildNodesWithName(name, action, recursive);
                }
            }
        }

        internal virtual void beforeDraw()
        {
            if (physicsBody != null)
            {
                position = ConvertUnits.ToDisplayUnits(physicsBody.Position) - parent.positionInNode(GameScene.current.gameWorld);
                zRotation = physicsBody.Rotation;
            }
        }

        internal virtual void draw(Vector2 position, float alpha)
        {
            if (isHidden || alpha <= 0.0f)
            {
                return;
            }

            beforeDraw();

            drawChildren(position, alpha);
        }

        protected void drawChildren(Vector2 position, float alpha)
        {
            foreach (Node node in children)
            {
                node.draw(position + this.position, alpha * this.alpha);
            }
        }

        internal Vector2 positionInNode(Node node)
        {
            return drawPosition(Vector2.Zero) - node.drawPosition(Vector2.Zero);
        }

        Vector2 drawPosition(Vector2 position)
        {
            if (parent != null)
            {
                return parent.drawPosition(position + this.position);
            }

            return position + this.position;
        }
    }
}