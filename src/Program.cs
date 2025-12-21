using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

class Program
{
	public static void Main(string[] args)
	{
		Raylib.SetTraceLogLevel(TraceLogLevel.Warning);
		Raylib.SetConfigFlags(ConfigFlags.ResizableWindow);
		Raylib.InitWindow(720, 480, "5 days to escape the tron and jump the ditch");
		Raylib.SetExitKey(KeyboardKey.Null);

		// Load the starting scene
		SceneManager.SetScene(new Hamilton());
		
		while (!Raylib.WindowShouldClose())
		{
			SceneManager.UpdateCurrentScene();

			Raylib.BeginDrawing();
			Raylib.ClearBackground(Color.Magenta);
			SceneManager.DrawCurrentScene();
			Raylib.EndDrawing();
		}

		SceneManager.CleanUpCurrentScene();
		Raylib.CloseWindow();
	}
}