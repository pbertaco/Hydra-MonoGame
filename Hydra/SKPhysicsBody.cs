using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics;

namespace Hydra
{
    class SKPhysicsBody : Body
    {
        public SKPhysicsBody(Vertices vertices, ShapeType shapeType = ShapeType.Polygon) : base(SKScene.current.physicsWorld)
        {
            switch (shapeType)
            {
                case ShapeType.Unknown:
                    throw new NotImplementedException();
                case ShapeType.Circle:
                    throw new NotImplementedException();
                case ShapeType.Edge:
                    throw new NotImplementedException();
                case ShapeType.Polygon:
                    break;
                case ShapeType.Chain:
                    throw new NotImplementedException();
                case ShapeType.TypeCount:
                    throw new NotImplementedException();
            }

            load(vertices);
        }

        public SKPhysicsBody(Vector2 size, ShapeType shapeType = ShapeType.Unknown, Vertices vertices = null) : base(SKScene.current.physicsWorld)
        {
            switch (shapeType)
            {
                case ShapeType.Unknown:
                    vertices = PolygonTools.CreateRectangle(size.X / 2, size.Y / 2);
                    break;
                case ShapeType.Circle:
                    vertices = PolygonTools.CreateEllipse(size.X / 2, size.Y / 2, Settings.MaxPolygonVertices);
                    break;
                case ShapeType.Edge:
                    throw new NotImplementedException();
                case ShapeType.Polygon:
                    throw new NotImplementedException();
                case ShapeType.Chain:
                    throw new NotImplementedException();
                case ShapeType.TypeCount:
                    throw new NotImplementedException();
            }

            load(vertices);
        }

        private void load(Vertices vertices)
        {
            vertices.Scale(ConvertUnits.ToSimUnits(Vector2.One));
            PolygonShape polygonShape = new PolygonShape(vertices, 1.0f);
            CreateFixture(polygonShape);

            BodyType = BodyType.Dynamic;

            CollisionCategories = Category.All;
            CollidesWith = Category.All;

            LinearDamping = 0.2f;
            AngularDamping = 1.0f;
            Restitution = 0.25f;
            Friction = 0.125f;
        }
    }
}
