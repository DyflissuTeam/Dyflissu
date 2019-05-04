using System;
using Dyflissu.Physics.Shapes;
using Dyflissu.Primitives;

namespace Dyflissu.Physics
{
    public class CollisionChecker
    {
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

        public static bool IsAabbOverlapping(Body a, Body b)
        {
            if (MathF.Abs(a.Position.X - b.Position.X) * 2 >= a.Shape.Box.X + b.Shape.Box.X)
            {
                return false;
            }

            if (MathF.Abs(a.Position.Y - b.Position.Y) * 2 >= a.Shape.Box.Y + b.Shape.Box.Y)
            {
                return false;
            }

            return true;
        }
    }
}