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
        public ShapeManager ShapeManag = new ShapeManager();

        public void Init()
        {
            // Load Textures
            ResourceManager.LoadTexture("Textures/BlockSprite.png", "block");

        }
        public void DrawGrid()
        {
            Vector2 topLeftPos = new Vector2(-0.5f * Map.Size.X, -0.5f * Map.Size.Y + Common.BlockSize.Y - Common.BlockSize.Y);
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
            // Add outer edges to Map class
            for (int i = 0; i < Common.Dir_n; ++i)
            {
                Edge edge = new();
                if (i == 0)
                {
                    // North
                    edge = new(topLeftPos,
                        new(topLeftPos.X + Map.Size.X, topLeftPos.Y), (Dir_e)i);
                }
                else if (i == 1)
                {
                    // East
                    edge = new(new(topLeftPos.X + Map.Size.X, topLeftPos.Y),
                        new(topLeftPos.X + Map.Size.X, topLeftPos.Y + Map.Size.Y), (Dir_e)i);
                }
                else if (i == 2)
                {
                    // South 
                    edge = new(new(topLeftPos.X, topLeftPos.Y + Map.Size.Y),
                        new(topLeftPos.X + Map.Size.X, topLeftPos.Y + Map.Size.Y), (Dir_e)i);
                }
                else
                {
                    // West 
                    edge = new(topLeftPos,
                        new(topLeftPos.X, topLeftPos.Y + Map.Size.Y), (Dir_e)i);
                }
            }
        }

        public void ProcessInput(float dt)
        {
            // Player controls an shape
            if (ShapeManag.CurrentShapeID > 0)
            {
                if (Raylib.IsKeyPressed(KeyboardKey.D) || Raylib.IsKeyPressed(KeyboardKey.Right))
                {
                    ShapeManag.MoveCurrentShape(Dir_e.East);
                }
                if (Raylib.IsKeyPressed(KeyboardKey.S) || Raylib.IsKeyPressed(KeyboardKey.Down))
                {
                    ShapeManag.MoveCurrentShape(Dir_e.South);
                }
                if (Raylib.IsKeyPressed(KeyboardKey.A) || Raylib.IsKeyPressed(KeyboardKey.Left))
                {
                    ShapeManag.MoveCurrentShape(Dir_e.West);
                }
            }
        }

        public void Update(float dt)
        {
            ShapeManag.Update(dt);
        }

        public void Draw()
        {
            DrawGrid();

            ShapeManag.Draw();

           // Map.DrawBoundaryRecs();
        }
    }
}
