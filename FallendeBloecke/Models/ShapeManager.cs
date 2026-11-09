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
        // Private
        private List<Type> ShapeTypes = new() { Type.I, Type.J, Type.L, Type.O, Type.S, Type.T, Type.Z };
        private uint LastCreatedID = 0;
        private SpawnSystem SpawnSys = new(Common.DefaultSpawnTime);
        // Public
        public List<Shape> Shapes = new();
        public uint CurrentShapeID = 0;


        // #################### 
        //      PUBLIC
        // ####################

        public void Draw()
        {
            foreach (Shape shape in Shapes)
            {
                shape.Draw();
            }
        }

        public void Update(float dt)
        {
            HandleShapeSpawn(dt);

            foreach (Shape shape in Shapes)
            {
                if (shape.ID == CurrentShapeID)
                {
                    shape.UpdateMoveTime(dt);
                    if (shape.ShouldFall())
                    {
                        Log.Info("Shape should fall");
                        if (CanMoveAtDirection(Dir_e.South))
                        {
                            Log.Info("Shape CanMove");
                            shape.Move(Dir_e.South);
                        }
                    }
                }
            }
        }

        public void MoveCurrentShape(Dir_e dir)
        {
            int idx = GetShapeIndexByID(CurrentShapeID);
            if (idx < 0)
            {
                Log.Error("Failed to get current shape");
                return;
            }
            Shape shape = Shapes[idx];
            if (!CanMoveAtDirection(dir))
            {
                Log.Info($"Shape can't move at direction '{Edge.GetDirectionString(dir)}'");
                return;
            }
            shape.Move(dir);
        }

        public void CreateRandomShape()
        {
            int rng = RNG.GenerateRNG((int)Type.Empty + 1, (int)Type.Z);
            Type type = (Type)rng;
            CurrentShapeID = CreateShape(type);
        }

        public uint CreateShape(Type type)
        {
            uint id = CreateShapeID();
            Color colorTint = GetDefaultColorTint(type);
            Block blockProps = new(new(0), colorTint, ResourceManager.GetTexture("block"));
            List<Vector2> indices = GetDefaultIndices(type);
            Shape shape = new Shape(type, id, Common.ShapeOrigin, Common.ShapeVelocity, blockProps, indices);
            Shapes.Add(shape);
            Debug.Assert(Shapes.Count != 0, "Failed to create a new shape");
            return shape.ID;
        }

        /* Creates a new Shape ID, updates the last one.
         * Returns unsigned int. */
        public uint CreateShapeID()
        {
            uint id = ++LastCreatedID;
            Debug.Assert((id != 0), "Created shapeID can't be '0'");
            LastCreatedID = id;

            return id;
        }

        public int GetShapeIndexByID(uint shapeID)
        {
            for (int i = 0; i < Shapes.Count; ++i)
            {
                Shape current = Shapes[i];
                if (current.ID == shapeID)
                {
                    return i;
                }
            }

            return -1;
        }

        // #################### 
        //      PRIVATE
        // ####################
        private void ResetCurrentShape()
        {
            CurrentShapeID = 0;
        }

        private bool IsMovePositionFree(Dir_e dir, Rectangle rect)
        {
            foreach (Shape shape in Shapes)
            {
                if (shape.ID != CurrentShapeID)
                {
                    Block block = shape.GetOuterBlock(dir);
                    if (Raylib.CheckCollisionRecs(rect, block.GetRectangle()))
                    {
                        Log.Info("Move position is NOT free");
                        return false;
                    }
                }
            }

            return true;
        }

        private bool CanMoveAtDirection(Dir_e dir)
        {
            int idx = GetShapeIndexByID(CurrentShapeID);
            if (idx < 0)
            {
                Log.Error("Failed to get current shape");
                return false;
            }
            Shape shape = Shapes[idx];
            Block block = shape.GetOuterBlock(dir);
            Rectangle rect = block.GetRectangle();
            bool isCollision = false;
            switch (dir)
            {
                case Dir_e.North:
                    rect.Y -= Common.BlockSize.Y;
                    Log.Info("Not allowed to move up");
                    return false;
                case Dir_e.East:
                    rect.X += Common.BlockSize.X;
                    isCollision = (!Map.CheckBoundaryCollision(dir, rect) && IsMovePositionFree(dir, rect));
                    return isCollision;
                case Dir_e.South:
                    rect.Y += Common.BlockSize.Y;
                    isCollision = (!Map.CheckBoundaryCollision(dir, rect) && IsMovePositionFree(dir, rect));
                    if (isCollision)
                    {
                        ResetCurrentShape();
                    }
                    return isCollision;
                case Dir_e.West:
                    rect.X -= Common.BlockSize.X;
                    isCollision = (!Map.CheckBoundaryCollision(dir, rect) && IsMovePositionFree(dir, rect));
                    return isCollision;
                default:
                    Log.Error("Failed: Unexpected direction");
                    return false;
            }
        }

        private List<Vector2> GetDefaultIndices(Type type)
        {
            switch (type)
            {
                case FallendeBloecke.Shapes.Type.I:
                    return new() {
                        new(0,-2), new(0,-1),
                        new(0, 0), new(0, 1)
                    };
                case FallendeBloecke.Shapes.Type.J:
                    return new() {
                        new(0,-2),
                        new(0,-1),
             new(0, 0), new(-1, 0)
                    };
                case FallendeBloecke.Shapes.Type.L:
                    return new() {
                        new(0,-2),
                        new(0,-1),
                        new(0, 0), new(1, 0)
                    };
                case FallendeBloecke.Shapes.Type.O:
                    return new() {
                        new(0,-1), new(1,-1),
                        new(0, 0), new(1, 0)
                    };
                case FallendeBloecke.Shapes.Type.S:
                    return new() {
                                    new(0,-1), new(1,-1),
                         new(0, 0), new(-1, 0)
                    };
                case FallendeBloecke.Shapes.Type.T:
                    return new() {
                        new(-1,-1), new(0,-1),new(1, -1),
                                    new(0, 0)
                    };
                case FallendeBloecke.Shapes.Type.Z:
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
        private void HandleShapeSpawn(float dt)
        {
            if (CurrentShapeID == 0)
            {
                SpawnSys.Update(dt);

                if (SpawnSys.CanSpawn())
                {
                    CreateRandomShape();
                }
            }
        }
    }
}
