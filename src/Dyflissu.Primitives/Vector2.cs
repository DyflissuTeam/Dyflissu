using System;

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
        
        /// <summary>
        /// Length of the vector.
        /// </summary>
        public float Length => MathF.Sqrt(SquareLength);

        /// <summary>
        /// Length of the vector in square.
        /// </summary>
        /// <remarks>
        /// You not always need to take square root of the length, so this will be a bit better.
        /// </remarks>
        public float SquareLength => X * X + Y * Y;

        /// <summary>
        /// Unit vector which is collinear to original.
        /// </summary>
        public Vector2 Normalized => SquareLength > 0 ? this / Length : Zero;

        public Vector2 Absolute => new Vector2(MathF.Abs(X), MathF.Abs(Y));
        
        public static readonly Vector2 Zero = new Vector2(0);

        public static Vector2 operator -(Vector2 v) => new Vector2(-v.X, -v.Y);

        public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.X - b.X, a.Y - b.Y);

        public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.X + b.X, a.Y + b.Y);

        public static Vector2 operator *(Vector2 a, float x) => new Vector2(a.X * x, a.Y * x);

        public static Vector2 operator /(Vector2 a, float x) => new Vector2(a.X / x, a.Y / x);

        public override string ToString() => $"({X}; {Y})";
    }
}