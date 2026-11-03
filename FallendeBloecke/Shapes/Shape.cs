using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;
using FallendeBloecke.Models;

namespace FallendeBloecke.Shapes
{
    public class Shape
    {
        public uint ID;
        public Vector2 Velocity;
        public Vector2 Origin;
        public Block BlockProps;
        public List<Vector2> Indices = new();
        public List<Block> Blocks = new();
        public Shape(uint id, Vector2 origin, Vector2 velocity, Block blockProps, List<Vector2> indices)
        {
            this.ID = id;
            this.Origin = origin;
            this.Velocity = velocity;
            this.BlockProps = blockProps;
            this.Indices = indices;

            this.UpdateShape();
        }

        public void UpdatePosition(Vector2 factor)
        {
            foreach (Block block in Blocks)
            {
                block.Position.X += factor.X * BlockProps.Size.X;
                block.Position.Y += factor.Y * BlockProps.Size.Y;
            }
        }

        public void UpdateShape()
        {
            Blocks.Clear();

            foreach (Vector2 index in Indices)
            {
                Block block = new Block(new(Origin.X + index.X * Common.BlockSize.X, Origin.Y + index.Y * Common.BlockSize.Y), BlockProps.Size, BlockProps.BlockColor, BlockProps.Texture);
                Blocks.Add(block);
            }
        }

        public void Draw()
        {
            foreach (Block block in this.Blocks)
            {
                block.Draw();
            }

        }
    }
}
