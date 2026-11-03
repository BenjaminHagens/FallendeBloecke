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
        public Camera2D camera = new Camera2D(new Vector2(0.5f * Common.ScreenWidth, 0.5f * Common.ScreenHeight), new Vector2(0, 0), 0f, 1f);
        public Block block_1;
        public Shape shapeO;

        public void Init()
        {
            // Load Textures
            ResourceManager.LoadTexture("Textures/BlockSprite.png", "block");

            // Init Models
            block_1 = new Block(new Vector2(0f, 0f), Common.BlockSize, Color.Green, ResourceManager.GetTexture("block"));

            shapeO = new(new Vector2(0, 0), new(1, 1), new Block(new Vector2(0, 0), Common.BlockSize, Color.Blue, ResourceManager.GetTexture("block")), new List<Vector2> { new(0, 1), new(1, 1), new(1, 2), new(0, 2) });


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

            Raylib.BeginMode2D(camera);

            DrawGrid();

            shapeO.Draw();

            Raylib.EndMode2D();

            Raylib.DrawText("Fallende Blöcke", 12, 12, 20, Color.Black);

            Raylib.EndDrawing();
        }
    }
}
