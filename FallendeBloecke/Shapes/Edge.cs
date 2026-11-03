using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;

namespace FallendeBloecke.Shapes
{
    public enum Direction { North, East, South, West };

    public struct Edge
    {
        public Vector2 Start;
        public Vector2 End;
        public Direction Dir;

        public Edge(Vector2 start, Vector2 end, Direction dir)
        {
            this.Start = start;
            this.End = end;
            this.Dir = dir;
        }
        public static string GetDirectionString(Direction side)
        {
            switch (side)
            {
                case Direction.North:
                    return "North";
                case Direction.East:
                    return "East";
                case Direction.South:
                    return "South";
                case Direction.West:
                    return "West";
                default:
                    return "Unknown";
            }
        }

    }
}
