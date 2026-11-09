using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;

namespace FallendeBloecke.Models
{
    internal class Log
    {
        public static void Error(string message, [CallerMemberName] string callerName = "")
        {
            Console.WriteLine($"[ERROR] at {callerName}: {message}");
        }

        public static void Info(string message)
        {
            Console.WriteLine($"[INFO]: {message}");
        }
    }
}
