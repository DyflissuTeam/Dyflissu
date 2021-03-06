using Dyflissu.Physics;
using Microsoft.Xna.Framework;

namespace Dyflissu.Shared
{
    public class GameWorld
    {
        public World PhysicsWorld { get; }

        public GameWorld()
        {
            PhysicsWorld = new World(Vector2.Zero);
        }

        public void Update(float delta)
        {
            PhysicsWorld.Update(delta);
        }
    }
}