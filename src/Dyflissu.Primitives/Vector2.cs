namespace Dyflissu.Primitives
{
    public struct Vector2
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector2(float v) : this(v, v)
        {
            
        }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float SquareLength => X * X + Y * Y;
        
        public static readonly Vector2 Zero = new Vector2(0);

        public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.X - b.X, a.Y - b.Y);

        public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.X + b.X, a.Y + b.Y);

        public static Vector2 operator *(Vector2 a, float x) => new Vector2(a.X * x, a.Y * x);

        public static Vector2 operator /(Vector2 a, float x) => new Vector2(a.X / x, a.Y / x);

        public override string ToString() => $"({X}; {Y})";
    }
}