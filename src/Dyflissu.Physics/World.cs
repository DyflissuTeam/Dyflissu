using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Dyflissu.Physics
{
    public class World
    {
        public Vector2 Gravity { get; }
        public int CollisionResolvingIterations { get; }
        
        private readonly List<Body> _bodies;

        public World(Vector2 gravity)
        {
            Gravity = gravity;
            CollisionResolvingIterations = 10;
            
            _bodies = new List<Body>();
        }

        public void AddBody(Body body)
        {
            _bodies.Add(body);
        }

        public void Update(float delta)
        {
            List<Manifold> contacts = FindCollisions().ToList();
            IntegrateForces();
            SolveCollisions(contacts);
            IntegrateVelocities();
            CorrectPositions(contacts);
            

            foreach (Body body in _bodies)
            {
                body.Position += body.Velocity * delta;
            }
        }

        private void CorrectPositions(IEnumerable<Manifold> contacts)
        {
            foreach (Manifold contact in contacts)
            {
                contact.PositionalCorrection();
            }
        }

        private void IntegrateVelocities()
        {
            foreach (Body body in _bodies)
            {
                body.IntegrateVelocity(Gravity);
                body.ClearForce();
            }
        }

        private void SolveCollisions(IEnumerable<Manifold> contacts)
        {
            for (var i = 0; i < CollisionResolvingIterations; i++)
            {
                foreach (Manifold contact in contacts)
                {
                    contact.Resolve();
                }
            }
        }

        private void IntegrateForces()
        {
            foreach (Body body in _bodies)
            {
                body.IntegrateForces(this.Gravity);
            }
        }

        private IEnumerable<Manifold> FindCollisions()
        {
            // 1 because we skip the first with the second loop
            for (var i = 0; i < _bodies.Count; ++i)
            {
                for (int j = i + 1; j < _bodies.Count; ++j)
                {
                    if (_bodies[i].IsStatic && _bodies[j].IsStatic)
                    {
                        continue;
                    }

                    if (!CollisionChecker.IsAabbOverlapping(_bodies[i], _bodies[j]))
                    {
                        continue;
                    }
                    
                    var contact = new Manifold(_bodies[i], _bodies[j]);
                    if (contact.Solve())
                    {
                        yield return contact;
                    }
                }
            }
        }

        public IEnumerable<Body> Bodies => _bodies;
    }
}