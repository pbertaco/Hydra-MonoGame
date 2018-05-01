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
    using SKActionTimingFunction = Func<float, float, float, float, float>;

    public class SKAction
    {
        /// <summary>
        /// The duration required to complete an action, in seconds.
        /// </summary>
        internal float duration;

        /// <summary>
        /// The timing function.
        /// </summary>
        internal SKActionTimingFunction timingFunction = Easing.Linear;

        internal float elapsed;

        internal float t0;

        internal SKAction with(SKActionTimingFunction timingFunction) {
            this.timingFunction = timingFunction;
            return this;
        }

        /// <summary>
        /// Creates an action that moves a node relative to its current position
        /// </summary>
        /// <param name="delta">A vector that describes the change to apply to the node’s position</param>
        /// <param name="duration">The duration of the animation, in seconds</param>
        internal static SKAction moveBy(Vector2 delta, float duration)
        {
            return new ActionMoveBy(delta, duration);
        }

        internal static SKAction moveBy(float deltaX, float deltaY, float duration)
        {
            return new ActionMoveBy(new Vector2(deltaX, deltaY), duration);
        }

        /// <summary>
        /// Creates an action that moves a node to a new position
        /// </summary>
        /// <param name="location">The coordinates for the node’s new position</param>
        /// <param name="duration">The duration of the animation, in seconds</param>
        internal static SKAction moveTo(Vector2 location, float duration)
        {
            return new ActionMoveTo(location, duration);
        }

        internal static SKAction moveTo(float locationX, float locationY, float duration)
        {
            return new ActionMoveTo(new Vector2(locationX, locationY), duration);
        }

        /// <summary>
        /// Creates an action that rotates the node by a relative value
        /// </summary>
        /// <param name="radians">The amount to rotate the node, in radians</param>
        /// <param name="duration">The duration of the animation, in seconds</param>
        internal static SKAction rotateBy(float radians, float duration)
        {
            return new ActionRotateBy(radians, duration);
        }

        internal static SKAction rotateTo(float radians, float duration)
        {
            return new ActionRotateTo(radians, duration);
        }

        /// <summary>
        /// Creates an action that adjusts the size of a sprite
        /// </summary>
        /// <param name="size">Size.</param>
        /// <param name="duration">Duration.</param>
        internal static SKAction resizeBy(Vector2 size, float duration)
        {
            return new ActionResizeBy(size, duration);
        }

        internal static SKAction resizeBy(float sizeX, float sizeY, float duration)
        {
            return new ActionResizeBy(new Vector2(sizeX, sizeY), duration);
        }

        internal static SKAction resizeTo(Vector2 size, float duration)
        {
            return new ActionResizeTo(size, duration);
        }

        internal static SKAction resizeTo(float sizeX, float sizeY, float duration)
        {
            return new ActionResizeTo(new Vector2(sizeX, sizeY), duration);
        }


        internal static SKAction scaleBy(Vector2 scale, float duration)
        {
            return new ActionScaleBy(scale, duration);
        }

        internal static SKAction scaleBy(float scaleX, float scaleY, float duration)
        {
            return new ActionScaleBy(new Vector2(scaleX, scaleY), duration);
        }

        internal static SKAction scaleBy(float scale, float duration)
        {
            return new ActionScaleBy(new Vector2(scale, scale), duration);
        }

        internal static SKAction scaleTo(Vector2 scale, float duration)
        {
            return new ActionScaleTo(scale, duration);
        }

        internal static SKAction scaleTo(float scaleX, float scaleY, float duration)
        {
            return new ActionScaleTo(new Vector2(scaleX, scaleY), duration);
        }

        internal static SKAction scaleTo(float scale, float duration)
        {
            return new ActionScaleTo(new Vector2(scale, scale), duration);
        }


        internal static SKAction sequence(IEnumerable<SKAction> actions)
        {
            return new ActionSequence(actions);
        }

        internal static SKAction group(IEnumerable<SKAction> actions)
        {
            return new ActionGroup(actions);
        }

        internal static SKAction repeat(SKAction action, int count)
        {
            return new ActionRepeat(action, count);
        }

        internal static SKAction repeatForever(SKAction action)
        {
            return new ActionRepeat(action, int.MaxValue);
        }

        internal static SKAction fadeIn(float duration)
        {
            return new FadeAlphaTo(1.0f, duration);
        }

        internal static SKAction fadeOut(float duration)
        {
            return new FadeAlphaTo(0.0f, duration);
        }

        internal static SKAction fadeAlphaBy(float factor, float duration)
        {
            return new FadeAlphaBy(factor, duration);
        }

        internal static SKAction fadeAlphaTo(float alpha, float duration)
        {
            return new FadeAlphaTo(alpha, duration);
        }

        internal static SKAction hide()
        {
            return new ActionHide();
        }

        internal static SKAction unhide()
        {
            return new ActionUnhide();
        }

        internal static SKAction setTexture(Texture2D texture2D)
        {
            return new ActionSetTexture(texture2D, false);
        }

        internal static SKAction setTexture(Texture2D texture2D, bool resize)
        {
            return new ActionSetTexture(texture2D, resize);
        }

        internal static SKAction animate(IEnumerable<Texture2D> textures, float timePerFrame)
        {
            throw new NotImplementedException();
        }

        internal static SKAction animate(IEnumerable<Texture2D> textures, float timePerFrame, bool resize, bool restore)
        {
            throw new NotImplementedException();
        }

        internal static SKAction playSoundFileNamed(string soundFile, bool waitForCompletion)
        {
            throw new NotImplementedException();
        }

        internal static SKAction colorize(Color color, float colorBlendFactor, float duration)
        {
            throw new NotImplementedException();
        }

        internal static SKAction colorize(float colorBlendFactor, float duration)
        {
            throw new NotImplementedException();
        }

        internal static SKAction speedBy(float speed, float duration)
        {
            throw new NotImplementedException();
        }

        internal static SKAction speedTo(float speed, float duration)
        {
            throw new NotImplementedException();
        }

        internal static SKAction waitForDuration(float duration)
        {
            return new ActionWait(duration, 0);
        }

        internal static SKAction waitForDuration(float duration, float durationRange)
        {
            return new ActionWait(duration, durationRange);
        }

        internal static SKAction removeFromParent()
        {
            return new ActionRemoveFromParent();
        }


        internal virtual SKAction copy()
        {
            return this;
        }

        internal virtual void runOnNode(SKNode node)
        {
        }

        internal virtual void evaluateWithNode(SKNode node, float dt)
        {
        }

        internal virtual SKAction reversed()
        {
            return this;
        }
    }
}
