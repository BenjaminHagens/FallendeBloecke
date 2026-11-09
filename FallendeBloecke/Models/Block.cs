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
        public Color ColorTint;
        public Texture2D Textur;

        public Block(Vector2 pos, Color colorTint, Texture2D textur)
        {
            this.Position = pos;
            this.ColorTint = colorTint;
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

        public void Draw(Color? colorTint = null )
        {
            // Raylib.DrawRectangleLines((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y, BlockColor);
            if (colorTint != null)
            {
                ColorTint = (Color)colorTint;
            }
            Raylib.DrawTextureRec(Textur, GetRectangle(), Position, ColorTint);
        }

    }
}
