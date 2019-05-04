using Microsoft.Xna.Framework;

namespace Dyflissu.Physics.Shapes
{
    /// <summary>
    /// Represents shape of an object.
    /// </summary>
    public abstract class Shape
    {
        protected Shape(Vector2 box)
        {
            Box = box;
        }

        /// <summary>
        /// We assume that shape is symmetrical on both axes and its center is in (0; 0).
        /// </summary>
        public Vector2 Box { get; }
        
        /// <summary>
        /// Some meta object (e.g. GameObject).
        /// </summary>
        public object Meta { get; set; }
    }
}