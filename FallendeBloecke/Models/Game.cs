using System;
using System.Numerics;
using FallendeBloecke.Shapes;
using Raylib_cs;
using System.Collections.Generic;
using System.Text;

namespace FallendeBloecke.Models
{
    public class Game
    {
        public Camera2D Kamera = new Camera2D(new Vector2(0.5f * Common.ScreenWidth, 0.5f * Common.ScreenHeight), new Vector2(0, 0), 0f, 1f);
        public ShapeManager ShapeManag = new ShapeManager();

        public void Init()
        {
            // Load Textures
            ResourceManager.LoadTexture("Textures/BlockSprite.png", "block");

            uint id = 0;
            bool wasMoved = false;
            id = ShapeManag.CreateShape(ShapeType.I);
            wasMoved = ShapeManag.MoveShape(id, new(-5, -7));
            id = ShapeManag.CreateShape(ShapeType.J);
            wasMoved = ShapeManag.MoveShape(id, new(-2, -7));
            id = ShapeManag.CreateShape(ShapeType.L);
            wasMoved = ShapeManag.MoveShape(id, new(0, -7));
            id = ShapeManag.CreateShape(ShapeType.O);
            wasMoved = ShapeManag.MoveShape(id, new(-4, 0));
            id = ShapeManag.CreateShape(ShapeType.S);
            wasMoved = ShapeManag.MoveShape(id, new(0, 0));
            id = ShapeManag.CreateShape(ShapeType.T);
            wasMoved = ShapeManag.MoveShape(id, new(-2, 5));
            id = ShapeManag.CreateShape(ShapeType.Z);
            wasMoved = ShapeManag.MoveShape(id, new(3, 5));
            if (!wasMoved)
            {
                Console.WriteLine("[ERROR] Failed to move shape!");
            }
        }
        public void DrawGrid()
        {
            Vector2 topLeftPos = new Vector2(-0.5f * Common.MapSize.X, -0.5f * Common.MapSize.Y + Common.BlockSize.Y - Common.BlockSize.Y);
            Vector2 startPos = topLeftPos;
            Vector2 endPos = new Vector2(0);
            int height = (int)(Math.Round(Common.MapSize.Y / Common.BlockSize.Y) + 1);
            for (int y = 0; y < height; ++y)
            {
                startPos = new(topLeftPos.X, topLeftPos.Y + y * Common.BlockSize.Y);
                endPos = new(startPos.X + Common.MapSize.X, startPos.Y);
                Raylib.DrawLineV(startPos, endPos, Color.White);
            }
            startPos = topLeftPos;
            int width = (int)(Math.Round(Common.MapSize.X / Common.BlockSize.X) + 1);
            for (int x = 0; x < width; ++x)
            {
                startPos = new(topLeftPos.X + x * Common.BlockSize.X, topLeftPos.Y);
                endPos = new(startPos.X, startPos.Y + Common.MapSize.Y);
                Raylib.DrawLineV(startPos, endPos, Color.White);
            }

        }

        public void ProcessInput(float dt)
        {
            if (Raylib.IsKeyDown(KeyboardKey.D) || Raylib.IsKeyDown(KeyboardKey.Right))
            {

            }
        }

        public void Update(float dt)
        {

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            Raylib.BeginMode2D(Kamera);

            DrawGrid();

            ShapeManag.Draw();

            Raylib.EndMode2D();

            Raylib.DrawText("Fallende Blöcke", 12, 12, 20, Color.Black);

            Raylib.EndDrawing();
        }
    }
}
