﻿using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace Hydra
{
    public class Touch
    {
        Vector2 lastPosition;
        Vector2 position;

        internal Vector2 delta;

        public Touch(Vector2 position)
        {
            lastPosition = position;
            this.position = position;
        }

        internal void moved(Vector2 position)
        {
            lastPosition = this.position;
            this.position = position;
            delta = lastPosition - position;
        }

        internal void up(Vector2 position)
        {
            lastPosition = this.position;
            this.position = position;
            delta = lastPosition - position;
        }

        internal Vector2 locationIn(Node node)
        {
            return position - node.positionInNode(GameScene.current);
        }
    }
}