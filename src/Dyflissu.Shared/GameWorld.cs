using Dyflissu.Physics;
using Dyflissu.Primitives;

namespace Dyflissu.Shared
{
    public class GameWorld
    {
        public World PhysicsWorld { get; }

        public GameWorld()
        {
            PhysicsWorld = new World(Vector2.Zero);
        }
    }
}