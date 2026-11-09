using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Ray = Raylib_cs;

namespace FallendeBloecke.Models
{
    public static class Common
    {
        // Screen
        public const int ScreenWidth = 1200;
        public const int ScreenHeight = 1000;
        // Block
        public static Vector2 BlockSize = new(50f, 50f);
        // Shape
        public static Vector2 ShapeOrigin = new(0 * BlockSize.X, -7 * BlockSize.Y);
        public static Vector2 ShapeVelocity = new(1, 1);
        // Direction
        public static int Dir_n = 4;
        // Time defaults
        public static float DefaultMoveTime = 1.0f;
        public static float DefaultSpawnTime = 1.0f;
    }
}
