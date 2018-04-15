using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Hydra
{
    public class Action
    {
        /// <summary>
        /// The duration required to complete an action, in seconds.
        /// </summary>
        internal float duration;

        internal float elapsed;

        /// <summary>
        /// Creates an action that moves a node relative to its current position
        /// </summary>
        /// <param name="delta">A vector that describes the change to apply to the node’s position</param>
        /// <param name="duration">The duration of the animation, in seconds</param>
        internal static Action moveBy(Vector2 delta, float duration)
        {
            return new ActionMoveBy(delta, duration);
        }

        internal static Action moveBy(float deltaX, float deltaY, float duration)
        {
            return new ActionMoveBy(new Vector2(deltaX, deltaY), duration);
        }

        /// <summary>
        /// Creates an action that moves a node to a new position
        /// </summary>
        /// <param name="location">The coordinates for the node’s new position</param>
        /// <param name="duration">The duration of the animation, in seconds</param>
        internal static Action moveTo(Vector2 location, float duration)
        {
            return new ActionMoveTo(location, duration);
        }

        internal static Action moveTo(float locationX, float locationY, float duration)
        {
            return new ActionMoveTo(new Vector2(locationX, locationY), duration);
        }

        /// <summary>
        /// Creates an action that rotates the node by a relative value
        /// </summary>
        /// <param name="radians">The amount to rotate the node, in radians</param>
        /// <param name="duration">The duration of the animation, in seconds</param>
        internal static Action rotateBy(float radians, float duration)
        {
            return new ActionRotateBy(radians, duration);
        }

        internal static Action rotateTo(float radians, float duration)
        {
            return new ActionRotateTo(radians, duration);
        }

        /// <summary>
        /// Creates an action that adjusts the size of a sprite
        /// </summary>
        /// <param name="size">Size.</param>
        /// <param name="duration">Duration.</param>
        internal static Action resizeBy(Vector2 size, float duration)
        {
            return new ActionResizeBy(size, duration);
        }

        internal static Action resizeBy(float sizeX, float sizeY, float duration)
        {
            return new ActionResizeBy(new Vector2(sizeX, sizeY), duration);
        }

        internal static Action resizeTo(Vector2 size, float duration)
        {
            return new ActionResizeTo(size, duration);
        }

        internal static Action resizeTo(float sizeX, float sizeY, float duration)
        {
            return new ActionResizeTo(new Vector2(sizeX, sizeY), duration);
        }


        internal static Action scaleBy(Vector2 scale, float duration)
        {
            return new ActionScaleBy(scale, duration);
        }

        internal static Action scaleBy(float scaleX, float scaleY, float duration)
        {
            return new ActionScaleBy(new Vector2(scaleX, scaleY), duration);
        }

        internal static Action scaleBy(float scale, float duration)
        {
            return new ActionScaleBy(new Vector2(scale, scale), duration);
        }

        internal static Action scaleTo(Vector2 scale, float duration)
        {
            return new ActionScaleTo(scale, duration);
        }

        internal static Action scaleTo(float scaleX, float scaleY, float duration)
        {
            return new ActionScaleTo(new Vector2(scaleX, scaleY), duration);
        }

        internal static Action scaleTo(float scale, float duration)
        {
            return new ActionScaleTo(new Vector2(scale, scale), duration);
        }


        internal static Action sequence(IEnumerable<Action> actions)
        {
            return new ActionSequence(actions);
        }

        internal static Action group(IEnumerable<Action> actions)
        {
            return new ActionGroup(actions);
        }

        internal static Action repeat(Action action, int count)
        {
            return new ActionRepeat(action, count);
        }

        internal static Action repeatForever(Action action)
        {
            return new ActionRepeat(action, int.MaxValue);
        }

        internal static Action fadeIn(float duration)
        {
            return new FadeAlphaTo(1.0f, duration);
        }

        internal static Action fadeOut(float duration)
        {
            return new FadeAlphaTo(0.0f, duration);
        }

        internal static Action fadeAlphaBy(float factor, float duration)
        {
            return new FadeAlphaBy(factor, duration);
        }

        internal static Action fadeAlphaTo(float alpha, float duration)
        {
            return new FadeAlphaTo(alpha, duration);
        }

        internal static Action hide()
        {
            return new ActionHide();
        }

        internal static Action unhide()
        {
            return new ActionUnhide();
        }

        internal static Action setTexture(Texture2D texture2D)
        {
            return new ActionSetTexture(texture2D, false);
        }

        internal static Action setTexture(Texture2D texture2D, bool resize)
        {
            return new ActionSetTexture(texture2D, resize);
        }

        internal static Action animate(IEnumerable<Texture2D> textures, float timePerFrame)
        {
            throw new NotImplementedException();
        }

        internal static Action animate(IEnumerable<Texture2D> textures, float timePerFrame, bool resize, bool restore)
        {
            throw new NotImplementedException();
        }

        internal static Action playSoundFileNamed(string soundFile, bool waitForCompletion)
        {
            throw new NotImplementedException();
        }

        internal static Action colorize(Color color, float colorBlendFactor, float duration)
        {
            throw new NotImplementedException();
        }

        internal static Action colorize(float colorBlendFactor, float duration)
        {
            throw new NotImplementedException();
        }

        internal static Action speedBy(float speed, float duration)
        {
            throw new NotImplementedException();
        }

        internal static Action speedTo(float speed, float duration)
        {
            throw new NotImplementedException();
        }

        internal static Action waitForDuration(float duration)
        {
            return new ActionWait(duration, 0);
        }

        internal static Action waitForDuration(float duration, float durationRange)
        {
            return new ActionWait(duration, durationRange);
        }

        internal static Action removeFromParent()
        {
            return new ActionRemoveFromParent();
        }


        internal virtual Action copy()
        {
            return this;
        }

        internal virtual void runOnNode(Node node)
        {
        }

        internal virtual void evaluateWithNode(Node node, float dt)
        {
        }

        internal virtual Action reversed()
        {
            return this;
        }
    }
}
