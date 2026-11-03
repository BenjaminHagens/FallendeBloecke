using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;

namespace FallendeBloecke.Models
{
    public class ResourceManager
    {
        public static Dictionary<string, Texture2D> Textures = new();

        public static void LoadTexture(string path, string name)
        {
            Textures.Add(name, Raylib.LoadTexture(path));
        }

        public static Texture2D GetTexture(string name)
        {
            Texture2D texture;
            if (Textures.TryGetValue(name, out texture))
            {
                return texture;
            }
            Console.WriteLine("[ERROR]: Failed to get Texture: '" + name + "'");
            return texture;
        }
    }
}
