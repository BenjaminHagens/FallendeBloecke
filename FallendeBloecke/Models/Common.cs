using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace FallendeBloecke.Models
{
    public static class Common
    {
        public const int ScreenWidth = 1200;
        public const int ScreenHeight = 1000;

        public static Vector2 BlockSize = new(50f, 50f);

        public static Vector2 MapSize = new(10f * BlockSize.X, 18f * BlockSize.Y);

    }
}
