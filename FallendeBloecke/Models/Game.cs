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
        ShapeManager Shapemanager = new();
        public void Init()
        {
            // Load Textures
            ResourceManager.LoadTexture("Textures/BlockSprite.png", "block");

            Shapemanager.Init();

        }
        public void DrawGrid()
        {
            Vector2 topLeftPos = Map.TopLeftPosition;
            Vector2 startPos = topLeftPos;
            Vector2 endPos = new Vector2(0);
            int height = (int)(Math.Round(Map.Size.Y / Common.BlockSize.Y) + 1);
            for (int y = 0; y < height; ++y)
            {
                startPos = new(topLeftPos.X, topLeftPos.Y + y * Common.BlockSize.Y);
                endPos = new(startPos.X + Map.Size.X, startPos.Y);
                Raylib.DrawLineV(startPos, endPos, Color.White);
            }
            startPos = topLeftPos;
            int width = (int)(Math.Round(Map.Size.X / Common.BlockSize.X) + 1);
            for (int x = 0; x < width; ++x)
            {
                startPos = new(topLeftPos.X + x * Common.BlockSize.X, topLeftPos.Y);
                endPos = new(startPos.X, startPos.Y + Map.Size.Y);
                Raylib.DrawLineV(startPos, endPos, Color.White);
            }
        }

        public void ProcessInput(float dt)
        {
            // Player controls an shape
            if (Shapemanager.CurrentShape != null)
            {
                if (Raylib.IsKeyPressed(KeyboardKey.D) || Raylib.IsKeyPressed(KeyboardKey.Right))
                {
                    Shapemanager.MoveCurrentShape(Dir_e.East);
                }
                if (Raylib.IsKeyPressed(KeyboardKey.S) || Raylib.IsKeyPressed(KeyboardKey.Down))
                {
                    Shapemanager.MoveCurrentShape(Dir_e.South);
                }
                if (Raylib.IsKeyPressed(KeyboardKey.A) || Raylib.IsKeyPressed(KeyboardKey.Left))
                {
                    Shapemanager.MoveCurrentShape(Dir_e.West);
                }
            }
        }

        public void Update(float dt)
        {
            Shapemanager.Update(dt);
        }

        public void Draw()
        {
            DrawGrid();

            Shapemanager.Draw();

            // Map.DrawBoundaryRecs();
        }
    }
}
