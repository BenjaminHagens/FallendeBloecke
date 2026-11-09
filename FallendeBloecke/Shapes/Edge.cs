using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using System.Text;

namespace FallendeBloecke.Shapes
{
    public struct Edge
    {
        public Vector2 Start;
        public Vector2 End;
        public Dir_e Dir;

        public Edge(Vector2 start, Vector2 end, Dir_e dir)
        {
            this.Start = start;
            this.End = end;
            this.Dir = dir;
        }

        public void Draw(Color color)
        {
            Raylib.DrawLineV(Start, End, color);
        }


        public static bool IsAAboveB(Edge a, Edge b)
        {
            switch (b.Dir)
            {
                case Dir_e.East:
                    return (a.Start.X > b.Start.X);
                case Dir_e.South:
                    return (a.Start.Y > b.Start.Y);
                case Dir_e.West:
                    return (a.Start.X < b.Start.X);
                default:
                    return false;
            }
        }

        public static string GetDirectionString(Dir_e side)
        {
            switch (side)
            {
                case Dir_e.East:
                    return "East";
                case Dir_e.South:
                    return "South";
                case Dir_e.West:
                    return "West";
                default:
                    return "Unknown";
            }
        }
    }
}
