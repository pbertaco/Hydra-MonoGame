using System;
using System.Collections.Generic;
using System.Text;

namespace Hydra
{
    class CameraNode : SKNode
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
