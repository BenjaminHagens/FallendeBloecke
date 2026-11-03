using System;
using System.Numerics;
using Raylib_cs;
using FallendeBloecke.Models;

namespace FallendeBloecke;

internal class Program
{

    // STAThread is required if you deploy using NativeAOT on Windows
    // See https://github.com/raylib-cs/raylib-cs/issues/301
    [System.STAThread]
    public static void Main()
    {
        Raylib.InitWindow(Common.ScreenWidth, Common.ScreenHeight, "Hello, World");
        int monitor = Raylib.GetCurrentMonitor();
        int monitorWidth = Raylib.GetMonitorWidth(monitor);
        int monitorHeight = Raylib.GetMonitorHeight(monitor);
        Raylib.SetWindowPosition((int)Math.Round(0.5f * monitorWidth - 0.5f * Common.ScreenWidth), (int)Math.Round(0.5f * monitorHeight - 0.51f * Common.ScreenHeight));

        Game game = new Game();

        game.Init();

        float deltaTime = 0f;
        float lastFrame = 0f;

        while (!Raylib.WindowShouldClose())
        {
            float currentFrame = Raylib.GetFrameTime();
            deltaTime = currentFrame - lastFrame;
            lastFrame = currentFrame;

            game.ProcessInput(deltaTime);

            game.Update(deltaTime);
        }

        Raylib.CloseWindow();
    }

}
