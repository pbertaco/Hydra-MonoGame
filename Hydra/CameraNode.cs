using System;
using System.Collections.Generic;
using System.Text;

namespace Hydra
{
    class CameraNode : Node
    {
        internal void update()
        {
            if (parent != null)
            {
                parent.position = -position;
            }
        }
    }
}
