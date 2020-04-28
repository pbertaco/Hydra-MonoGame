using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

using FarseerPhysics;

namespace Hydra
{
    class SKNode
    {
        internal static Random random = new Random();

        Vector2 _position;
        internal Vector2 position
        {
            get => _position;
            set
            {
                if (physicsBody != null)
                {
                    physicsBody.Position = ConvertUnits.ToSimUnits(value);
                }
                _position = value;
            }
        }

        float _zRotation;
        internal float zRotation
        {
            get => _zRotation;
            set
            {
                _zRotation = value;
                if (physicsBody != null)
                {
                    physicsBody.Rotation = value;
                }
            }
        }

        Vector2 _scale;
        internal virtual Vector2 scale { get => _scale; set => _scale = value; }

        internal bool isHidden;

        internal object userData;

        internal string name;

        internal SKNode parent;

        internal List<SKNode> children;

        SKPhysicsBody _physicsBody;
        internal SKPhysicsBody physicsBody
        {
            get => _physicsBody;
            set
            {
                _physicsBody = value;
                position = _position;
                zRotation = _zRotation;
            }
        }

        internal Dictionary<string, SKAction> actions;
        internal List<string> actionsToRemove;

        internal float alpha = 1.0f;

        public SKNode()
        {
            name = "";
            children = new List<SKNode>();
            actions = new Dictionary<string, SKAction>();
            actionsToRemove = new List<string>();
            _scale = Vector2.One;
        }

        internal void run(SKAction action)
        {
            run(action, $"{SKScene.currentTime}{random.NextDouble()}");
        }

        internal void run(SKAction action, string key)
        {
            SKAction copy = action.copy();
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

        internal void run(SKAction action, Action completionBlock)
        {
            run(action, $"{SKScene.currentTime}{random.NextDouble()}");
        }

        internal bool hasActions()
        {
            return actions.Count > 0;
        }

        internal SKAction actionForKey(string key)
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
            foreach (KeyValuePair<string, SKAction> keyValuePair in actions)
            {
                SKAction action = keyValuePair.Value;
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

        internal virtual void addChild(SKNode node)
        {
            children.Add(node);
            node.parent = this;
        }

        internal void insertChild(SKNode node, int index)
        {
            children.Insert(index, node);
            node.parent = this;
        }

        internal void removeChildren(IEnumerable<SKNode> nodes)
        {
            foreach (SKNode node in nodes)
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
                SKScene.current.physicsWorld.RemoveBody(physicsBody);
            }
        }

        internal void moveToParent(SKNode node)
        {
            removeFromParent();
            node.addChild(this);
        }

        internal SKNode childNodeWithName(string someName, bool recursive = true)
        {
            foreach (SKNode node in children)
            {
                if (node.name == someName)
                {
                    return node;
                }

                if (recursive)
                {
                    SKNode childNode = node.childNodeWithName(someName, recursive);

                    if (childNode != null)
                    {
                        return childNode;
                    }
                }
            }

            return null;
        }

        internal void enumerateChildNodesWithName(string someName, Action<SKNode> action, bool recursive = true)
        {
            foreach (SKNode node in children)
            {
                if (node.name == someName)
                {
                    action(node);
                }

                if (recursive)
                {
                    node.enumerateChildNodesWithName(someName, action, recursive);
                }
            }
        }

        internal virtual void beforeDraw()
        {
            if (physicsBody != null)
            {
                position = ConvertUnits.ToDisplayUnits(physicsBody.Position);
                zRotation = physicsBody.Rotation;
            }
        }

        internal virtual void draw(Vector2 parentPosition, float parentAlpha, Vector2 parentScale)
        {
            Vector2 position = currentPosition(parentPosition, parentScale);
            float alpha = currentAlpha(parentAlpha);
            Vector2 scale = currentScale(parentScale);

            if (isHidden || alpha <= 0.0f)
            {
                return;
            }

            beforeDraw();

            drawChildren(position, alpha, scale);
        }

        protected void drawChildren(Vector2 position, float alpha, Vector2 scale)
        {
            foreach (SKNode node in children)
            {
                node.draw(position, alpha, scale);
            }
        }

        internal Vector2 currentPosition()
        {
            if (parent != null)
            {
                return parent.position + position * parent.scale;
            }
            else
            {
                return position;
            }
        }

        internal Vector2 currentPosition(Vector2 parentPosition, Vector2 parentScale)
        {
            return parentPosition + position * parentScale;
        }

        internal float currentAlpha(float parentAlpha)
        {
            return parentAlpha * alpha;
        }

        internal virtual Vector2 currentScale(Vector2 parentScale)
        {
            return parentScale * scale;
        }

        internal virtual bool contains(Vector2 somePosition)
        {
            return false;
        }
    }
}