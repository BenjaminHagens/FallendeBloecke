using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;

namespace FallendeBloecke.Shapes
{
    public enum Richtung { North, East, South, West };

    public struct Kante
    {
        public Vector2 Start;
        public Vector2 End;
        public Richtung eRichtung;

        public Kante(Vector2 start, Vector2 end, Richtung dir)
        {
            this.Start = start;
            this.End = end;
            this.eRichtung = dir;
        }
        public static string GetDirectionString(Richtung side)
        {
            switch (side)
            {
                case Richtung.North:
                    return "North";
                case Richtung.East:
                    return "East";
                case Richtung.South:
                    return "South";
                case Richtung.West:
                    return "West";
                default:
                    return "Unknown";
            }
        }

    }
}
