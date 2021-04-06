using System;

using Microsoft.Xna.Framework;

namespace Dragon
{
    static class Vector2Extension
    {
        public static float distanceTo(this Vector2 vector2, Vector2 position)
        {
            return Vector2.Distance(vector2, position);
        }

        public static Vector2 rotateBy(this Vector2 position, float rotation)
        {
            float sin = (float)Math.Sin(rotation);
            float cos = (float)Math.Cos(rotation);
            float x = position.X * cos - position.Y * sin;
            float y = position.X * sin + position.Y * cos;
            return new Vector2(x, y);
        }
    }
}
