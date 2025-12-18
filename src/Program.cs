using Raylib_cs;

class Program
{
	public static void Main(string[] args)
	{
		Raylib.SetTraceLogLevel(TraceLogLevel.Warning);
		Raylib.InitWindow(720, 480, "5 days to escape the tron and jump the ditch");

		while (!Raylib.WindowShouldClose())
		{
			Raylib.BeginDrawing();
			Raylib.ClearBackground(Color.Magenta);
			Raylib.DrawText("5 days to escape the tron and jump the ditch", 10, 10, 30, Color.White);
			Raylib.EndDrawing();
		}

		Raylib.CloseWindow();
	}
}