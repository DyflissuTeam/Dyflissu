using C3.XNA;
using Dyflissu.Physics;
using Dyflissu.Physics.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DyflissuVector2 = Dyflissu.Primitives.Vector2;

namespace Dyflissu.DesktopGL
{
    public static class PhysicsExtensions
    {
        public static void Draw(this World world, SpriteBatch spriteBatch)
        {
            foreach (Body body in world.Bodies)
            {
                switch (body.Shape)
                {
                    case RectangleShape rectangleShape:
                        Vector2 position = (body.Position - body.Shape.Box / 2).ToXna();
                        spriteBatch.DrawRectangle(position, rectangleShape.Box.ToXna(), Color.Green);
                        break;
                }
            }
        }

        public static Vector2 ToXna(this DyflissuVector2 v) => new Vector2(v.X, v.Y);
    }
}