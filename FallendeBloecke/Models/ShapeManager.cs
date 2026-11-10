using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;
using FallendeBloecke.Shapes;
using Type = FallendeBloecke.Shapes.Type;
using Raylib_cs;
using System.Diagnostics;

namespace FallendeBloecke.Models
{
    public class ShapeManager
    {
        // PRIVATE 
        private SpawnSystem SpawnSys = new(Common.DefaultSpawnTime);

        // PUBLIC 
        public Block[,] Blocks = new Block[(int)Map.Height, (int)Map.Width];
        public Shape? CurrentShape = null;


        // #################### 
        //      PUBLIC
        // ####################

        public void Init()
        {
            Vector2 pos = Map.TopLeftPosition;

            for (int y = 0; y < Map.Height; ++y)
            {
                for (int x = 0; x < Map.Width; ++x)
                {
                    Blocks[y, x] = new(new Vector2(pos.X + x * Common.BlockSize.X, pos.Y + y * Common.BlockSize.Y), Color.Red, ResourceManager.GetTexture("block")); ;
                }
            }
        }

        public void Draw()
        {
            for (int y = 0; y < Blocks.GetLength(0); ++y)
            {
                for (int x = 0; x < Blocks.GetLength(1); ++x)
                {
                    Block block = Blocks[y, x];
                    if (!block.Empty)
                    {
                        block.Draw();
                    }
                }
            }
            if (CurrentShape != null)
            {
                CurrentShape.Draw();
            }
        }

        public void Update(float dt)
        {
            HandleShapeSpawn(dt);

            if (CurrentShape != null)
            {
                CheckGroundCollision();

                CurrentShape.UpdateMoveTime(dt);

                if (CurrentShape.ShouldFall())
                {

                    if (CanMoveAtDirection(Dir_e.South))
                    {
                        CurrentShape.Move(Dir_e.South);
                    }
                }
            }
        }

        public void MoveCurrentShape(Dir_e dir)
        {
            if (CurrentShape != null)
            {
                if (!CanMoveAtDirection(dir))
                {
                    Log.Info($"Shape can't move at direction '{Shape.GetDirectionString(dir)}'");
                    return;
                }
                CurrentShape.Move(dir);
            }
        }

        public void CreateRandomShape()
        {
            int rng = RNG.GenerateRNG((int)Type.Empty + 1, (int)Type.Z);
            Type type = (Type)rng;
            CreateShape(type);
        }

        public void CreateShape(Type type)
        {
            Color colorTint = GetDefaultColorTint(type);
            Block blockProps = new(new(0), colorTint, ResourceManager.GetTexture("block"));
            List<Vector2> indices = GetDefaultIndices(type);
            CurrentShape = new Shape(type, Map.ShapeOrigin, Common.ShapeVelocity, blockProps, indices);
        }

        // #################### 
        //      PRIVATE
        // ####################
        private (int, int) GetIndexFromBlockPosition(Vector2 pos)
        {
            Vector2 topLeftPos = Map.TopLeftPosition;
            pos.X -= topLeftPos.X;
            pos.Y -= topLeftPos.Y;
            int x = (int)Math.Round(pos.X / Common.BlockSize.X);
            int y = (int)Math.Round(pos.Y / Common.BlockSize.Y);
            return new(x, y);
        }

        private void ResetCurrentShape()
        {
            if (CurrentShape != null)
            {
                foreach (Block block in CurrentShape.Blocks)
                {
                    (int, int) index = GetIndexFromBlockPosition(block.Position);
                    Log.Info($"Block index (x, y): '({index.Item1}, {index.Item2})'");
                    Blocks[index.Item2, index.Item1].ColorTint = block.ColorTint;
                    Blocks[index.Item2, index.Item1].Empty = false;
                }

                CurrentShape = null;
            }
        }

        private void HandleShapeSpawn(float dt)
        {
            if (CurrentShape == null)
            {
                SpawnSys.Update(dt);
            }

            if (SpawnSys.CanSpawn())
            {
                CreateRandomShape();
            }
        }

        private bool IsMovePositionFree(Dir_e dir, Rectangle rect)
        {
            if (CurrentShape != null)
            {
                for (int y = 0; y < Blocks.GetLength(0); ++y)
                {
                    for (int x = 0; x < Blocks.GetLength(1); ++x)
                    {
                        Block block = Blocks[y, x];

                        if (!block.Empty && Raylib.CheckCollisionRecs(rect, block.GetRectangle()))
                        {
                            Log.Info("Move position is NOT free");
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private bool CheckGroundCollision()
        {
            if (CurrentShape != null)
            {
                List<Block> bottomBlocks = CurrentShape.GetBottomBlocks();
                foreach (Block block in bottomBlocks)
                {
                    Rectangle rect = block.GetRectangle();
                    Rectangle bottomRect = new(rect.X, rect.Y + Common.BlockSize.Y, Common.BlockSize);
                    bool onGround = Map.CheckBoundaryCollision(Dir_e.South, bottomRect) ||
                                          !IsMovePositionFree(Dir_e.South, bottomRect);
                    if (onGround)
                    {
                        Log.Info("Shape reset: Shape on ground");
                        ResetCurrentShape();
                        return false;
                    }
                }
            }
            return false;
        }

        private bool CanMoveAtDirection(Dir_e dir)
        {
            if (CurrentShape != null)
            {
                foreach (Block block in CurrentShape.Blocks)
                {
                    Rectangle rect = block.GetRectangle();

                    bool movePosFree = false;
                    bool insideMap = false;
                    switch (dir)
                    {
                        case Dir_e.East:
                            rect.X += Common.BlockSize.X;
                            break;
                        case Dir_e.South:
                            rect.Y += Common.BlockSize.Y;
                            break;
                        case Dir_e.West:
                            rect.X -= Common.BlockSize.X;
                            break;
                        default:
                            Log.Error("Failed: Unexpected direction");
                            return false;
                    }
                    insideMap = !Map.CheckBoundaryCollision(dir, rect);
                    movePosFree = IsMovePositionFree(dir, rect);
                    bool canMove = insideMap && movePosFree;

                    return canMove;
                }
            }
            Log.Error("Failed to get CurrentShape (null)");
            return false;
        }

        private List<Vector2> GetDefaultIndices(Type type)
        {
            switch (type)
            {
                case Type.I:
                    return new() {
                        new(0,-2), new(0,-1),
                        new(0, 0), new(0, 1)
                    };
                case Type.J:
                    return new() {
                        new(0,-2),
                        new(0,-1),
             new(0, 0), new(-1, 0)
                    };
                case Type.L:
                    return new() {
                        new(0,-2),
                        new(0,-1),
                        new(0, 0), new(1, 0)
                    };
                case Type.O:
                    return new() {
                        new(0,-1), new(1,-1),
                        new(0, 0), new(1, 0)
                    };
                case Type.S:
                    return new() {
                                    new(0,-1), new(1,-1),
                         new(0, 0), new(-1, 0)
                    };
                case Type.T:
                    return new() {
                        new(-1,-1), new(0,-1),new(1, -1),
                                    new(0, 0)
                    };
                case Type.Z:
                    return new() {
                        new(-1,-1), new(0,-1),
                                   new(0, 0), new(1, 0)
                    };
                default:
                    Console.WriteLine("[ERROR] at GetDefaultIndices: Failed to get default indices from shape type");
                    return new();
            }
        }
        private Color GetDefaultColorTint(Shapes.Type type)
        {
            switch (type)
            {
                case Type.I:
                    return Color.Blue;
                case Type.J:
                    return Color.DarkBlue;
                case Type.L:
                    return Color.Orange;
                case Type.O:
                    return Color.Yellow;
                case Type.S:
                    return Color.Green;
                case Type.T:
                    return Color.Purple;
                case Type.Z:
                    return Color.Red;
                default:
                    Console.WriteLine("[ERROR] at GetDefaultColorTint: Failed to get color tint from shape type");
                    return Color.White;
            }

        }
    }
}
