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
        public Vector2 Size = Common.BlockSize;
        public Color Farbton;
        public Texture2D Textur;

        public Block(Vector2 pos, Color colorTint, Texture2D textur)
        {
            this.Position = pos;
            this.Farbton = colorTint;
            this.Textur = textur;
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle(Position, Size);
        }

        public Vector2 GetOrigin()
        {
            return new Vector2(Position.X - 0.5f * Size.X, Position.Y + 0.5f * Size.Y);
        }

        public void Draw()
        {
            // Raylib.DrawRectangleLines((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y, BlockColor);
            Raylib.DrawTextureRec(Textur, GetRectangle(), Position, Farbton);
        }

    }
}
