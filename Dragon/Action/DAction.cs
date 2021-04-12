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

namespace Dragon
{
    using DActionTimingFunction = Func<float, float, float, float, float>;

    class DAction
    {
        /// <summary>
        /// The duration required to complete an action, in seconds.
        /// </summary>
        internal float duration;

        /// <summary>
        /// The timing function.
        /// </summary>
        internal DActionTimingFunction timingFunction = Easing.Linear;

        internal float elapsed;

        internal float t0;

        public DAction(float duration)
        {
            if (duration <= 0)
            {
                this.duration = 0.001f;
            }
            else
            {
                this.duration = duration;
            }
        }

        internal DAction with(DActionTimingFunction someTimingFunction)
        {
            timingFunction = someTimingFunction;
            return this;
        }

        /// <summary>
        /// Creates an action that moves a node relative to its current position
        /// </summary>
        /// <param name="delta">A vector that describes the change to apply to the node’s position</param>
        /// <param name="duration">The duration of the animation, in seconds</param>
        internal static DAction moveBy(Vector2 delta, float duration)
        {
            return new ActionMoveBy(delta, duration);
        }

        internal static DAction moveBy(float deltaX, float deltaY, float duration)
        {
            return new ActionMoveBy(new Vector2(deltaX, deltaY), duration);
        }

        /// <summary>
        /// Creates an action that moves a node to a new position
        /// </summary>
        /// <param name="location">The coordinates for the node’s new position</param>
        /// <param name="duration">The duration of the animation, in seconds</param>
        internal static DAction moveTo(Vector2 location, float duration)
        {
            return new ActionMoveTo(location, duration);
        }

        internal static DAction moveTo(float locationX, float locationY, float duration)
        {
            return new ActionMoveTo(new Vector2(locationX, locationY), duration);
        }

        /// <summary>
        /// Creates an action that rotates the node by a relative value
        /// </summary>
        /// <param name="radians">The amount to rotate the node, in radians</param>
        /// <param name="duration">The duration of the animation, in seconds</param>
        internal static DAction rotateBy(float radians, float duration)
        {
            return new ActionRotateBy(radians, duration);
        }

        internal static DAction rotateTo(float radians, float duration)
        {
            return new ActionRotateTo(radians, duration);
        }

        /// <summary>
        /// Creates an action that adjusts the size of a sprite
        /// </summary>
        /// <param name="size">Size.</param>
        /// <param name="duration">Duration.</param>
        internal static DAction resizeBy(Vector2 size, float duration)
        {
            return new ActionResizeBy(size, duration);
        }

        internal static DAction resizeBy(float sizeX, float sizeY, float duration)
        {
            return new ActionResizeBy(new Vector2(sizeX, sizeY), duration);
        }

        internal static DAction resizeTo(Vector2 size, float duration)
        {
            return new ActionResizeTo(size, duration);
        }

        internal static DAction resizeTo(float sizeX, float sizeY, float duration)
        {
            return new ActionResizeTo(new Vector2(sizeX, sizeY), duration);
        }


        internal static DAction scaleBy(Vector2 scale, float duration)
        {
            return new ActionScaleBy(scale, duration);
        }

        internal static DAction scaleBy(float scaleX, float scaleY, float duration)
        {
            return new ActionScaleBy(new Vector2(scaleX, scaleY), duration);
        }

        internal static DAction scaleBy(float scale, float duration)
        {
            return new ActionScaleBy(new Vector2(scale, scale), duration);
        }

        internal static DAction scaleTo(Vector2 scale, float duration)
        {
            return new ActionScaleTo(scale, duration);
        }

        internal static DAction scaleTo(float scaleX, float scaleY, float duration)
        {
            return new ActionScaleTo(new Vector2(scaleX, scaleY), duration);
        }

        internal static DAction scaleTo(float scale, float duration)
        {
            return new ActionScaleTo(new Vector2(scale, scale), duration);
        }


        internal static DAction sequence(IEnumerable<DAction> actions)
        {
            return new ActionSequence(actions);
        }

        internal static DAction group(IEnumerable<DAction> actions)
        {
            return new ActionGroup(actions);
        }

        internal static DAction repeat(DAction action, int count)
        {
            return new ActionRepeat(action, count);
        }

        internal static DAction repeatForever(DAction action)
        {
            return new ActionRepeat(action, int.MaxValue);
        }

        internal static DAction fadeIn(float duration)
        {
            return new FadeAlphaTo(1.0f, duration);
        }

        internal static DAction fadeOut(float duration)
        {
            return new FadeAlphaTo(0.0f, duration);
        }

        internal static DAction fadeAlphaBy(float factor, float duration)
        {
            return new FadeAlphaBy(factor, duration);
        }

        internal static DAction fadeAlphaTo(float alpha, float duration)
        {
            return new FadeAlphaTo(alpha, duration);
        }

        internal static DAction hide()
        {
            return new ActionHide();
        }

        internal static DAction unhide()
        {
            return new ActionUnhide();
        }

        internal static DAction setTexture(Texture2D texture2D)
        {
            return new ActionSetTexture(texture2D, false);
        }

        internal static DAction setTexture(Texture2D texture2D, bool resize)
        {
            return new ActionSetTexture(texture2D, resize);
        }

        internal static DAction animate(IEnumerable<Texture2D> textures, float timePerFrame)
        {
            throw new NotImplementedException();
        }

        internal static DAction animate(IEnumerable<Texture2D> textures, float timePerFrame, bool resize, bool restore)
        {
            throw new NotImplementedException();
        }

        internal static DAction playSoundFileNamed(string soundFile, bool waitForCompletion)
        {
            throw new NotImplementedException();
        }

        internal static DAction colorize(Color color, float colorBlendFactor, float duration)
        {
            throw new NotImplementedException();
        }

        internal static DAction colorize(float colorBlendFactor, float duration)
        {
            throw new NotImplementedException();
        }

        internal static DAction speedBy(float speed, float duration)
        {
            throw new NotImplementedException();
        }

        internal static DAction speedTo(float speed, float duration)
        {
            throw new NotImplementedException();
        }

        internal static DAction waitForDuration(float duration)
        {
            return new ActionWait(duration, 0);
        }

        internal static DAction waitForDuration(float duration, float durationRange)
        {
            return new ActionWait(duration, durationRange);
        }

        internal static DAction removeFromParent()
        {
            return new ActionRemoveFromParent();
        }

        internal static DAction run(Action block)
        {
            return new ActionRunBlock(block);
        }

        #region DUtils

        /// <summary>
        /// Performs an action after the specified delay.
        /// </summary>
        internal static DAction afterDelay(float delay, DAction action)
        {
            return sequence(new[] { waitForDuration(delay), action });
        }

        /// <summary>
        /// Performs a block after the specified delay.
        /// </summary>
        internal static DAction afterDelay(float delay, Action block)
        {
            return afterDelay(delay, new ActionRunBlock(block));
        }

        /// <summary>
        /// Removes the node from its parent after the specified delay.
        /// </summary>
        internal static DAction removeFromParentAfterDelay(float delay)
        {
            return afterDelay(delay, new ActionRemoveFromParent());
        }

        #endregion

        internal virtual DAction copy()
        {
            return this;
        }

        internal virtual void runOnNode(DNode node)
        {
        }

        internal virtual void evaluateWithNode(DNode node, float dt)
        {
        }

        internal virtual DAction reversed()
        {
            return this;
        }
    }
}
