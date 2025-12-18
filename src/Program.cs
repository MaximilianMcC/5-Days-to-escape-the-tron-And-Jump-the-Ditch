using System.Numerics;
using Raylib_cs;

class Program
{
	public static void Main(string[] args)
	{
		Raylib.SetTraceLogLevel(TraceLogLevel.Warning);
		Raylib.SetConfigFlags(ConfigFlags.ResizableWindow);
		Raylib.InitWindow(720, 480, "5 days to escape the tron and jump the ditch");

		List<GameObject> gameObjects = new List<GameObject>()
		{
			new Skybox(),

			new Player(),
			new Car()
		};
		
		while (!Raylib.WindowShouldClose())
		{
			for (int i = gameObjects.Count - 1; i >= 0 ; i--)
			{
				gameObjects[i].Update();
			}

			Raylib.BeginDrawing();
			Raylib.ClearBackground(Color.Magenta);
			Raylib.BeginMode3D(gameObjects.OfType<Player>().First().Camera);
				for (int i = gameObjects.Count - 1; i >= 0 ; i--)
				{
					gameObjects[i].Draw3D();
				}
				Raylib.DrawGrid(15, 1);
			Raylib.EndMode3D();
				for (int i = gameObjects.Count - 1; i >= 0 ; i--)
				{
					gameObjects[i].Draw2D();
				}
				Raylib.DrawText("5 days to escape the tron and jump the ditch", 10, 430, 30, Color.White);
			Raylib.EndDrawing();
		}

		for (int i = gameObjects.Count - 1; i >= 0 ; i--)
		{
			gameObjects[i].CleanUp();
		}


		Raylib.CloseWindow();
	}
}