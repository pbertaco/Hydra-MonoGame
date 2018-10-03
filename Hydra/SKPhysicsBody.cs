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
                    vertices = PolygonTools.CreateRectangle(size.X / 2, size.Y / 2);
                    break;
                case ShapeType.TypeCount:
                    throw new NotImplementedException();
            }

            load(vertices, shapeType);
        }

        void load(Vertices vertices, ShapeType shapeType = ShapeType.Polygon)
        {
        	vertices.Scale(ConvertUnits.ToSimUnits(Vector2.One));
        	
            switch (shapeType)
            {
                case ShapeType.Unknown:
                    loadPolygonShape(vertices);
                    break;
                case ShapeType.Circle:
                    loadPolygonShape(vertices);
                    break;
                case ShapeType.Edge:
                    loadEdgeShape(vertices);
                    return;
                case ShapeType.Polygon:
                    loadPolygonShape(vertices);
                    break;
                case ShapeType.Chain:
                    loadChainShape(vertices);
                    break;
                case ShapeType.TypeCount:
                    throw new NotImplementedException();
            }

            BodyType = BodyType.Dynamic;

            CollisionCategories = Category.All;
            CollidesWith = Category.All;

            LinearDamping = 0.2f;
            AngularDamping = 1.0f;
            Restitution = 0.25f;
            Friction = 0.125f;
        }

        void loadEdgeShape(Vertices vertices)
        {
            Vector2 start = vertices[0];
            Vector2 end = vertices[1];
            EdgeShape shape = new EdgeShape(start, end);
            CreateFixture(shape);
        }

        void loadPolygonShape(Vertices vertices)
        {
            PolygonShape shape = new PolygonShape(vertices, 1.0f);
            CreateFixture(shape);
        }

        void loadChainShape(Vertices vertices)
        {
            ChainShape shape = new ChainShape(vertices, true);
            CreateFixture(shape);
        }
    }
}
