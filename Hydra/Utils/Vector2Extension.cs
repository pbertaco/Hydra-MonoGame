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
    static class Vector2Extension
    {
        public static float distanceTo(this Vector2 vector2, Vector2 position)
        {
            return Vector2.Distance(vector2, position);
        }
    }
}
