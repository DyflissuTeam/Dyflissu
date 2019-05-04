using Dyflissu.Physics.Shapes;
using Microsoft.Xna.Framework;

// Argh. I'm checking exactly with zero, don't bother me, man.
// ReSharper disable CompareOfFloatsByEqualityOperator

namespace Dyflissu.Physics
{
    public sealed class Body
    {
        public Shape Shape { get; set; }
        public Vector2 Position { get; set; }
        
        public Vector2 Velocity { get; set; }
        
        public Vector2 Force { get; private set; }

        public float Mass { get; set; } = 0.1f;
        public float Restitution { get; set; } = 0;
        
        public float InverseMass => Mass == 0 ? 0 : 1f / Mass;

        public bool IsStatic => Mass == 0;
        
        public Vector2 Max => Position + Shape.Box / 2;
        
        public Vector2 Min => Position - Shape.Box / 2;

        public void ApplyImpulse(Vector2 impulse)
        {
            Velocity += impulse * InverseMass;
        }

        public void IntegrateForces(Vector2 gravity)
        {
            if (IsStatic)
            {
                return;
            }

            Velocity += (Force * InverseMass + gravity) / 2f;
        }

        public void IntegrateVelocity(Vector2 gravity)
        {
            if (IsStatic)
            {
                return;
            }

            Position += Velocity;

            IntegrateForces(gravity);
        }

        public void ApplyForce(Vector2 force)
        {
            Force += force;
        }

        public void ClearForce()
        {
            Force = Vector2.Zero;
        }
    }
}