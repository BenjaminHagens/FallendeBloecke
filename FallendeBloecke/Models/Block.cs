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
        public Texture2D Texture;
        public Color ColorTint;
        public bool Empty = true;

        public Block(Vector2 pos, Color colorTint, Texture2D textur)
        {
            Position = pos;
            ColorTint = colorTint;
            Texture = textur;
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle(Position, Size);
        }

        public Vector2 GetOrigin()
        {
            return new Vector2(Position.X - 0.5f * Size.X, Position.Y + 0.5f * Size.Y);
        }

        public void Draw(Color? colorTint = null)
        {
            if (colorTint != null)
            {
                ColorTint = (Color)colorTint;
            }
            Raylib.DrawTextureRec(Texture, GetRectangle(), Position, ColorTint);
        }

    }
}
