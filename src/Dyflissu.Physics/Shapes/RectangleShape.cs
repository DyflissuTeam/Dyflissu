using Dyflissu.Primitives;

namespace Dyflissu.Physics.Shapes
{
    public class RectangleShape : Shape
    {
        public RectangleShape(float size) : this(size, size)
        {
        }
        
        public RectangleShape(float width, float height) : base(new Vector2(width, height))
        {
        }
    }
}