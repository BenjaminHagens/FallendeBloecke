using System;
using Raylib_cs;
using System.Numerics;
using System.Collections.Generic;
using System.Text;
using FallendeBloecke.Shapes;

namespace FallendeBloecke.Models
{
    public class ShapeManager
    {
        // Private
        private List<ShapeType> ShapeTypes = new() { ShapeType.I, ShapeType.J, ShapeType.L, ShapeType.O, ShapeType.S, ShapeType.T, ShapeType.Z };
        // Public
        public List<Shape> Shapes = new();
        private uint LastCreatedID = 0;
        public uint CurrentShapeID = 0;
        public void Draw()
        {
            foreach (Shape shape in this.Shapes)
            {
                shape.Draw();
            }
        }

        /* Creates a new Shape ID, updates the last one.
         * Returns unsigned int. */
        public uint CreateShapeID()
        {
            uint id = ++LastCreatedID;
            LastCreatedID = id;

            return id;
        }

        public uint CreateShape(ShapeType type)
        {
            uint id = CreateShapeID();
            Color colorTint = GetDefaultColorTint(type);
            Block blockProps = new(new(0), colorTint, ResourceManager.GetTexture("block"));
            List<Vector2> indices = GetDefaultIndices(type);
            Shape shape = new Shape(type, id, Common.ShapeOrigin, Common.ShapeVelocity, blockProps, indices);
            Shapes.Add(shape);
            return shape.ID;
        }

        public bool MoveShape(uint id, Vector2 factor)
        {
            foreach (Shape shape in this.Shapes)
            {
                if (shape.ID == id)
                {
                    shape.UpdatePosition(factor);
                    return true;
                }
            }

            return false;
        }

        // Private
        private List<Vector2> GetDefaultIndices(ShapeType type)
        {
            switch (type)
            {
                case ShapeType.I:
                    return new() {
                        new(0,-2), new(0,-1),
                        new(0, 0), new(0, 1)
                    };
                case ShapeType.J:
                    return new() {
                        new(0,-2),
                        new(0,-1),
             new(0, 0), new(-1, 0)
                    };
                case ShapeType.L:
                    return new() {
                        new(0,-2), 
                        new(0,-1),
                        new(0, 0), new(1, 0)
                    };
                case ShapeType.O:
                    return new() {
                        new(0,-1), new(1,-1),
                        new(0, 0), new(1, 0)
                    };
                case ShapeType.S:
                    return new() {
                                    new(0,-1), new(1,-1),
                         new(0, 0), new(-1, 0)
                    };
                case ShapeType.T:
                    return new() {
                        new(-1,-1), new(0,-1),new(1, -1),
                                    new(0, 0)
                    };
                case ShapeType.Z:
                    return new() {
                        new(-1,-1), new(0,-1),
                                   new(0, 0), new(1, 0)
                    };
                default:
                    Console.WriteLine("[ERROR] at GetDefaultIndices: Failed to get default indices from shape type");
                    return new();
            }
        }
        private Color GetDefaultColorTint(ShapeType type)
        {
            switch (type)
            {
                case ShapeType.I:
                    return Color.Blue;
                case ShapeType.J:
                    return Color.DarkBlue;
                case ShapeType.L:
                    return Color.Orange;
                case ShapeType.O:
                    return Color.Yellow;
                case ShapeType.S:
                    return Color.Green;
                case ShapeType.T:
                    return Color.Purple;
                case ShapeType.Z:
                    return Color.Red;
                default:
                    Console.WriteLine("[ERROR] at GetDefaultColorTint: Failed to get color tint from shape type");
                    return Color.White;
            }

        }
    }
}
