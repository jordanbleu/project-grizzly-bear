using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Source.Math
{
    /// <summary>
    /// Used to represent a 2D square in 2D space
    /// </summary>
    [Serializable]
    public struct Square
    {
        public Vector2 Center;
        public float Width;
        public float Height;

        public Square(Vector2 center, Vector2 size) {
            Center = center;
            Width = size.x;
            Height = size.y;
        }

        public float Left { get => Center.x - (Width / 2); }
        public float Right { get => Center.x + (Width / 2); }
        public float Top { get => Center.y + (Height / 2); }
        public float Bottom { get => Center.y - (Height / 2); }


        public Vector2 BoundsY { get => new Vector2(Center.y - (Height / 2), Center.y + (Height / 2)); }

        /// <summary>
        /// Returns whether or not the specified point in world space resides within the boundaries of this square
        /// </summary>
        public bool SurroundsPoint(Vector2 point) => (point.x > Left &&
                                                      point.x < Right &&
                                                      point.y > Bottom &&
                                                      point.y < Top);

    }
}
