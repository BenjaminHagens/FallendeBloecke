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


       Camera2D Camera = new Camera2D(new Vector2(0.5f * Common.ScreenWidth, 0.5f * Common.ScreenHeight), new Vector2(0, 0), 0f, 1f);

        Game game = new Game();

        game.Init();


        while (!Raylib.WindowShouldClose())
        {
            float deltaTime= Raylib.GetFrameTime();

            game.ProcessInput(deltaTime);

            game.Update(deltaTime);

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            Raylib.BeginMode2D(Camera);

            game.Draw();

            Raylib.EndMode2D();

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }

}
