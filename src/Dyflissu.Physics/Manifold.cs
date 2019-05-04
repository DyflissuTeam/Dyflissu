using System;
using Dyflissu.Primitives;

namespace Dyflissu.Physics
{
    public sealed class Manifold
    {
        public Body A { get; }

        public Body B { get; }

        public float E { get; private set; }

        public float PenetrationDepth { get; private set;}

        public Vector2 Normal { get; private set; }

        public bool Colliding { get; private set; }

        public Manifold()
        {
            Colliding = false;
        }

        public Manifold(Body a, Body b)
        {
            A = a;
            B = b;

            E = Math.Abs((b.Velocity - a.Velocity).SquareLength) < 1e-10 
                ? 0 
                : MathF.Min(a.Restitution, b.Restitution);
        }

        public bool Solve()
        {
            Vector2 normal = A.Position - B.Position;
            float aHalfSizeX = A.Shape.Box.X / 2;
            float bHalfSizeX = B.Shape.Box.X / 2;

            float xOverlap = aHalfSizeX + bHalfSizeX - MathF.Abs(normal.X);

            if (xOverlap <= 0) return false;
            
            float aHalfSizeY = A.Shape.Box.Y / 2;
            float bHalfSizeY = B.Shape.Box.Y / 2;

            float yOverlap = aHalfSizeY + bHalfSizeY - MathF.Abs(normal.Y);

            if (yOverlap == 0)
            {
                return false;
            }

            if (xOverlap < yOverlap)
            {
                Normal = new Vector2(-MathF.Sign(normal.X), 0);
                PenetrationDepth = xOverlap;
            }
            else
            {
                Normal = new Vector2(0, -MathF.Sign(normal.Y));
                PenetrationDepth = yOverlap;

            }
                
            return true;

        }

        public void Resolve()
        {
            E = Math.Abs((B.Velocity - A.Velocity).SquareLength) < 1e-10 
                ? 0 
                : MathF.Min(A.Restitution, B.Restitution);
            
            Vector2 relativeVelocity = B.Velocity - A.Velocity;

            float velAlongNormal = PhysicsMath.DotProduct(relativeVelocity, Normal);
            
            if (velAlongNormal > 0)
            {
                return;
            }

            float j = -(1f + E) * velAlongNormal;
            j /= A.InverseMass + B.InverseMass;

            A.ApplyImpulse(Normal * -j);
            B.ApplyImpulse(Normal * j);
            
            // TODO: Friction (e.g. https://gist.github.com/BonsaiDen/6144232).
        }

        public void PositionalCorrection()
        {
            const float 
                percent = 0.7f,
                slop = 0.05f;
            float m = MathF.Max(PenetrationDepth - slop, 0) / (A.InverseMass + B.InverseMass);

            Vector2 correction = Normal * percent * m;
            A.Position -= correction * A.InverseMass;
            B.Position += correction * B.InverseMass;
        }
    }
}