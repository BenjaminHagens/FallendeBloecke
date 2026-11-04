using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;

namespace FallendeBloecke.Models
{
    public class ResourceManager
    {
        public static Dictionary<string, Texture2D> Texturen = new();

        public static void LoadTexture(string pfad, string name)
        {
            Texturen.Add(name, Raylib.LoadTexture(pfad));
        }

        public static Texture2D GetTexture(string name)
        {
            Texture2D textur;
            if (Texturen.TryGetValue(name, out textur))
            {
                return textur;
            }
            Console.WriteLine("[ERROR]: Textur '"  + name + "' wurde nicht gefunden");

            return textur;
        }
    }
}
