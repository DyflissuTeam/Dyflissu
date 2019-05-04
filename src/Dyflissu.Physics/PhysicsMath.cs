using Microsoft.Xna.Framework;

namespace Dyflissu.Physics
{
    public class PhysicsMath
    {
        public static float DotProduct(Vector2 a, Vector2 b)
        {
            return a.X * b.X + a.Y * b.Y;
        }
    }
}