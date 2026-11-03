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
        public Vector2 Size;
        public Color BlockColor;
        public Texture2D Texture;

        public Block(Vector2 pos, Vector2 size, Color blockColor, Texture2D texture)
        {
            this.Position = pos;
            this.Size = size;
            this.BlockColor = blockColor;
            this.Texture = texture;
        }
        public Edge GetEdge(Direction side)
        {
            switch (side)
            {
                case Direction.North:
                    return GetNorthEdge();
                case Direction.East:
                    return GetEastEdge();
                case Direction.South:
                    return GetSouthEdge();
                case Direction.West:
                    return GetWestEdge();
                default:
                    Console.WriteLine("[ERROR]: Failed to get side " + side + ", from block!");
                    return GetNorthEdge();
            }
        }

        public Edge GetNorthEdge()
        {
            return new Edge(new Vector2(Position.X, Position.Y), new Vector2(Position.X + Common.BlockSize.X, Position.Y), Direction.North);
        }
        public Edge GetEastEdge()
        {
            return new Edge(new Vector2(Position.X + Common.BlockSize.X, Position.Y), new Vector2(Position.X + Common.BlockSize.X, Position.Y + Common.BlockSize.Y), Direction.East);
        }
        public Edge GetSouthEdge()
        {
            return new Edge(new Vector2(Position.X, Position.Y + Common.BlockSize.Y), new Vector2(Position.X + Common.BlockSize.X, Position.Y + Common.BlockSize.Y), Direction.South);
        }
        public Edge GetWestEdge()
        {
            return new Edge(new Vector2(Position.X, Position.Y), new Vector2(Position.X, Position.Y + Common.BlockSize.Y), Direction.West);
        }

        public Vector2 GetOrigin()
        {
            return new Vector2(Position.X - 0.5f * Size.X, Position.Y + 0.5f * Size.Y);
        }

        public void Draw()
        {
            // Raylib.DrawRectangleLines((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y, BlockColor);
            Raylib.DrawTextureRec(Texture, new Rectangle(Position, Size),Position, BlockColor);
        }

    }
}
