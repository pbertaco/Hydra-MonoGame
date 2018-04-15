using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

using FarseerPhysics.Dynamics;

namespace Hydra
{
    class PhysicsWorld : World
    {
        public PhysicsWorld() : base(new Vector2(0.0f, 9.8f))
        {

        }
    }
}
