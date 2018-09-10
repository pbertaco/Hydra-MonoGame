using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Hydra
{
    class SKShapeNode : SKSpriteNode
    {
        public SKShapeNode(float radius) : base("")
        {
            float diameter = radius * 2.0f;
            float radiusSquared = radius * radius;

            float auxRadiusSquared = (radius - 2.0f) * (radius - 2.0f);

            Color[] colors = new Color[(int)(diameter * diameter)];

            for (int x = 0; x < diameter; x++)
            {
                for (int y = 0; y < diameter; y++)
                {
                    float i = x * diameter + y;

                    Vector2 pixelPosition = new Vector2(x - radius, y - radius);
                    float lengthSquared = pixelPosition.LengthSquared();

                    if (lengthSquared > auxRadiusSquared && lengthSquared < radiusSquared)
                    {
                        colors[(int)i] = Color.White;
                    }
                }
            }

            texture2D = SKScene.current.Texture2D(new Vector2(diameter, diameter));
            texture2D.SetData(colors);

            size = texture2D.Bounds.Size.ToVector2();
        }
    }
}
