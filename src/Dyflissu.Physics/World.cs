using System;
using System.Collections.Generic;
using Dyflissu.Primitives;

namespace Dyflissu.Physics
{
    public class World
    {
        private readonly List<Body> _bodies;

        public World()
        {
            _bodies = new List<Body>();
        }

        public void AddBody(Body body)
        {
            _bodies.Add(body);
        }

        public void Update(float delta)
        {
            // 1 because we skip the first with the second loop
            for (var i = 1; i < _bodies.Count; ++i)
            {
                for (var j = 0; j < _bodies.Count; ++j)
                {
                    CollisionChecker.Solve(_bodies[i], _bodies[j]);
                }
            }

            foreach (Body body in _bodies)
            {
                body.Position += body.Velocity * delta;
            }
        }

        public IEnumerable<Body> Bodies => _bodies;
    }
}