using Dyflissu.Primitives;

namespace Dyflissu.Physics
{
    public struct Manifold
    {
        public float PenetrationDepth { get; set; }
        
        public Vector2 Normal { get; set; }
        
        public bool Colliding { get; set; }
    }
}