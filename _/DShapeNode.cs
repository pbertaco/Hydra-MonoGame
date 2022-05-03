﻿using System;
using System.Collections.Generic;
using System.Text;

using Dragon;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Hydra
{
    class SKShapeNode : DSpriteNode
    {
        public SKShapeNode(int radius) : base("")
        {
            color = Color.White;

            int diameter = radius * 2;

            Color[] colors = new Color[(diameter * diameter)];

            int x = radius - 1;
            int y = 0;
            int dx = 1;
            int dy = 1;
            int decisionOver2 = dx - diameter;

            while (x >= y)
            {
                colors[(x + radius) * diameter + (y + radius)] = Color.White;
                colors[(y + radius) * diameter + (x + radius)] = Color.White;

                colors[(-x + radius) * diameter + (y + radius)] = Color.White;
                colors[(-y + radius) * diameter + (x + radius)] = Color.White;

                colors[(-x + radius) * diameter + (-y + radius)] = Color.White;
                colors[(-y + radius) * diameter + (-x + radius)] = Color.White;

                colors[(x + radius) * diameter + (-y + radius)] = Color.White;
                colors[(y + radius) * diameter + (-x + radius)] = Color.White;

                if (decisionOver2 <= 0)
                {
                    y++;
                    decisionOver2 += dy;
                    dy += 2;
                }
                if (decisionOver2 > 0)
                {
                    x--;
                    dx += 2;
                    decisionOver2 += (-diameter) + dx;
                }
            }

            texture = DGame.current.contentManager.newTexture2D(diameter, diameter, colors);

            size = texture.Bounds.Size.ToVector2();
        }
    }
}
