using System;
using Raylib_cs;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using FallendeBloecke.Shapes;

namespace FallendeBloecke.Models
{

    public class Block
    {
        public Vector2 Position;
        public Vector2 Größe = Common.BlockSize;
        public Color Farbton;
        public Texture2D Textur;

        public Block(Vector2 pos, Color colorTint, Texture2D textur)
        {
            this.Position = pos;
            this.Farbton = colorTint;
            this.Textur = textur;
        }
        public Kante HolKante(Richtung side)
        {
            switch (side)
            {
                case Richtung.North:
                    return GetNorthEdge();
                case Richtung.East:
                    return GetEastEdge();
                case Richtung.South:
                    return GetSouthEdge();
                case Richtung.West:
                    return GetWestEdge();
                default:
                    Console.WriteLine("[ERROR]: Failed to get side " + side + ", from block!");
                    return GetNorthEdge();
            }
        }

        public Kante GetNorthEdge()
        {
            return new Kante(new Vector2(Position.X, Position.Y), new Vector2(Position.X + Common.BlockSize.X, Position.Y), Richtung.North);
        }
        public Kante GetEastEdge()
        {
            return new Kante(new Vector2(Position.X + Common.BlockSize.X, Position.Y), new Vector2(Position.X + Common.BlockSize.X, Position.Y + Common.BlockSize.Y), Richtung.East);
        }
        public Kante GetSouthEdge()
        {
            return new Kante(new Vector2(Position.X, Position.Y + Common.BlockSize.Y), new Vector2(Position.X + Common.BlockSize.X, Position.Y + Common.BlockSize.Y), Richtung.South);
        }
        public Kante GetWestEdge()
        {
            return new Kante(new Vector2(Position.X, Position.Y), new Vector2(Position.X, Position.Y + Common.BlockSize.Y), Richtung.West);
        }

        public Vector2 GetOrigin()
        {
            return new Vector2(Position.X - 0.5f * Größe.X, Position.Y + 0.5f * Größe.Y);
        }

        public void Draw()
        {
            // Raylib.DrawRectangleLines((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y, BlockColor);
            Raylib.DrawTextureRec(Textur, new Rectangle(Position, Größe),Position, Farbton);
        }

    }
}
