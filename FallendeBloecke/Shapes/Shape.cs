using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using FallendeBloecke.Models;
using Raylib_cs;
using Ray = Raylib_cs;

namespace FallendeBloecke.Shapes
{
    public enum Type { Empty, I, J, L, O, S, T, Z };

    public class Shape
    {
        // private 
        private float DefaultFallTime = Common.DefaultMoveTime;
        // public
        public float FallTime;
        public Type ShapeType;
        public uint ID;
        public Vector2 Velocity;
        public Vector2 Origin;
        public Block BlockProps;
        public List<Vector2> Indices = new();
        public List<Block> Blocks = new();

        public Shape(Type type, uint shapeID, Vector2 origin, Vector2 velocity, Block blockProps, List<Vector2> indices)
        {
            ShapeType = type;
            ID = shapeID;
            Origin = origin;
            Velocity = velocity;
            BlockProps = blockProps;
            Indices = indices;
            FallTime = DefaultFallTime;

            Init();
        }
        public void Init()
        {
            Blocks.Clear();

            foreach (Vector2 index in Indices)
            {
                Block block = new Block(new(Origin.X + index.X * Common.BlockSize.X, Origin.Y + index.Y * Common.BlockSize.Y), BlockProps.Farbton, BlockProps.Textur);
                Blocks.Add(block);
            }
        }


        public bool ShouldFall()
        {
            if (FallTime <= 0.0f)
            {
                ResetFallTime();
                return true;
            }
            return false;
        }

        public void UpdateMoveTime(float dt)
        {
            FallTime -= dt;
        }

        public void Move(Dir_e dir)
        {
            Vector2 factor = new(0, 0);
            switch (dir)
            {
                case Dir_e.North:
                    factor.Y -= 1;
                    break;
                case Dir_e.East:
                    factor.X += 1;
                    break;
                case Dir_e.South:
                    factor.Y += 1;
                    break;
                case Dir_e.West:
                    factor.X -= 1;
                    break;
                default:
                    Log.Error("Unexpected move direction");
                    break;
            }
            UpdatePosition(factor);
            Log.Info("Shape should have moved");
        }

        public Block GetOuterBlock(Dir_e dir)
        {
            Vector2 maxPos = new();
            int idx = 0;
            bool isFirst = true;
            for (int i = 0; i < Blocks.Count; ++i)
            {
                Block block = Blocks[i];
                if (isFirst)
                {
                    maxPos = block.Position;
                    isFirst = false;
                }
                else
                {
                    if (dir == Dir_e.North && block.Position.Y <= maxPos.Y
                        || dir == Dir_e.East && block.Position.X >= maxPos.X
                        || dir == Dir_e.South && block.Position.Y >= maxPos.Y
                        || dir == Dir_e.West && block.Position.X <= maxPos.X)
                    {
                        maxPos = block.Position;
                        idx = i;
                    }
                }
            }

            return Blocks[idx];
        }

        public void UpdatePosition(Vector2 factor)
        {
            foreach (Block block in Blocks)
            {
                block.Position.X += factor.X * Common.BlockSize.X;
                block.Position.Y += factor.Y * Common.BlockSize.Y;
            }
        }


        public void Draw()
        {
            foreach (Block block in this.Blocks)
            {
                block.Draw();
            }

        }

        //  private 
        private void ResetFallTime()
        {
            FallTime = DefaultFallTime;
        }
    }
}
