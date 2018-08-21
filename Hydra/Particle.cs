using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace Hydra
{
    class Particle
    {
        internal Vector2 position;

        internal float speedX;
        internal float speedY;

        internal float alpha;
        internal float scale;

        internal float birthTime;
        internal float lifetime;

        internal void update(float dt, float xAcceleration, float yAcceleration, float particleAlphaSpeed, float particleScaleSpeed)
        {
            speedX += (xAcceleration * dt);
            speedY += (yAcceleration * dt);

            position.X += (speedX * dt);
            position.Y += (speedY * dt);

            alpha += particleAlphaSpeed * dt;

            scale += particleScaleSpeed * dt;
        }
    }
}
