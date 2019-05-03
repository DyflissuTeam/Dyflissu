using System;
using Dyflissu.Physics.Shapes;
using Dyflissu.Primitives;

namespace Dyflissu.Physics
{
    public class CollisionChecker
    {
        public static void Solve(Body a, Body b)
        {
            if (a.IsStatic && b.IsStatic)
            {
                return;
            }

            if (!IsAabbOverlapping(a, b))
            {
                return;
            }

            Manifold manifold;

            if (a.Shape is RectangleShape && b.Shape is RectangleShape)
            {
                manifold = TestRectangles(a, b);
            }
            else
            {
                throw new InvalidOperationException("Unknown pair of shapes.");
            }

            if (manifold.Colliding)
            {
                ResolveCollision(manifold, a, b);
            }
        }

        private static void ResolveCollision(Manifold manifold, Body a, Body b)
        {
            Vector2 relativeVelocity = b.Velocity - a.Velocity;

            float velAlongNormal = PhysicsMath.DotProduct(relativeVelocity, manifold.Normal);
            if (velAlongNormal > 0)
            {
                return;
            }

            float e = Math.Min(a.Restitution, b.Restitution);

            float j = -(1 + e) * velAlongNormal;
            j /= a.InverseMass + b.InverseMass;

            Vector2 impulse = manifold.Normal * j;
            a.Velocity -= impulse * a.InverseMass;
            b.Velocity += impulse * b.InverseMass;
        }

        private static Manifold TestRectangles(Body a, Body b)
        {
            var manifold = new Manifold
            {
                Normal = b.Position - a.Position,
                Colliding = false
            };

            float xExtent = (a.Shape.Box.X + b.Shape.Box.X) / 2 - MathF.Abs(manifold.Normal.X);

            if (xExtent <= 0)
            {
                return manifold;
            }


            float yExtent = (a.Shape.Box.Y + b.Shape.Box.Y) / 2 - MathF.Abs(manifold.Normal.Y);

            if (yExtent <= 0)
            {
                return manifold;
            }

            manifold.Colliding = true;

            if (xExtent > yExtent)
            {
                manifold.Normal = new Vector2(manifold.Normal.X < 0 ? -1 : 1, 0);
                manifold.PenetrationDepth = xExtent;
            }
            else
            {
                manifold.Normal =  new Vector2(0, manifold.Normal.Y < 0 ? -1 : 1);
                manifold.PenetrationDepth = yExtent;
            }

            return manifold;
        }

        private static bool IsAabbOverlapping(Body a, Body b)
        {
            Vector2
                aMax = a.Position + a.Shape.Box / 2,
                aMin = a.Position - a.Shape.Box / 2,
                bMax = b.Position + b.Shape.Box / 2,
                bMin = b.Position - b.Shape.Box / 2;

            return !(aMax.X < bMin.X) && !(aMin.X > bMax.X) && (!(aMax.Y < bMin.Y) && !(aMin.X > bMax.Y));
        }
    }
}