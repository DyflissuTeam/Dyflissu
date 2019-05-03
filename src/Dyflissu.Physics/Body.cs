using Dyflissu.Physics.Shapes;
using Dyflissu.Primitives;
// Argh. I'm checking exactly with zero, don't bother me, man.
// ReSharper disable CompareOfFloatsByEqualityOperator

namespace Dyflissu.Physics
{
    public class Body
    {
        public Shape Shape { get; set; }
        public Vector2 Position { get; set; }
        
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }

        public float Mass { get; set; } = 1f;
        public float Restitution { get; set; } = 20f;
        
        public float InverseMass => Mass == 0 ? 0 : 1f / Mass;

        public bool IsStatic => Mass == 0;
    }
}