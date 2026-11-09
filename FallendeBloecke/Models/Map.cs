using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Raylib_cs;
using FallendeBloecke.Shapes;

namespace FallendeBloecke.Models
{
    public class Map
    {
        public static Vector2 Size = new(10f * Common.BlockSize.X, 18f * Common.BlockSize.Y);
        public static Vector2 Origin = new(0, 0);
        public static Rectangle Rect = new(Origin, Size);
        public static Rectangle[] BoundaryArr = {
            new (new (Rect.X + 0.5f * Size.X, Rect.Y - 0.5f * Size.Y), new (Size.X, Size.Y)), // East 
            new (new (Rect.X - 0.5f * Size.X, Rect.Y + 0.5f * Size.Y), new (Size.X, 0.5f* Size.Y)), // South 
            new (new (Rect.X - 1.5f * Size.X, Rect.Y - 0.5f* Size.Y ), new (Size.X, Size.Y)), // West
        };

        public static Vector2 GetShapeOrigin()
        {
            return new Vector2(Origin.X, Origin.Y - 0.5f * Size.Y - Common.BlockSize.Y);
        }

        public static void DrawBoundaryRecs()
        {
            Color[] colors = new Color[] { Color.Green, Color.Red, Color.Blue, Color.Yellow };
            for (int i = 0; i < 3; ++i)
            {
                Raylib.DrawRectangleRec(BoundaryArr[i], colors[i]);
            }
        }

        public static bool CheckBoundaryCollision(Dir_e dir, Rectangle rect)
        {
            int idx = (int)dir;
            bool res = Raylib.CheckCollisionRecs(BoundaryArr[idx], rect);
            Log.Info($"Is collision: {res}");

            return res;
        }
    }
}
