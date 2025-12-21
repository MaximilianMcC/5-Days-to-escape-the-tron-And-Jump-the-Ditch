using Raylib_cs;

class SceneManager
{
	public static Scene CurrentScene = null;

	public static void SetScene(Scene newScene)
	{
		// Unload the previous scene
		if (CurrentScene != null) CleanUpCurrentScene();

		// Load the new scene
		CurrentScene = newScene;
		StartCurrentScene();
	}

	public static void StartCurrentScene()
	{
		for (int i = CurrentScene.GameObjects.Count - 1; i >= 0 ; i--)
		{
			CurrentScene.GameObjects[i].Start();
		}
	}

	public static void UpdateCurrentScene()
	{
		// Check for if we're toggling debug mode
		if (Raylib.IsKeyPressed(KeyboardKey.Grave)) Settings.Debug = !Settings.Debug;

		// Update everything
		foreach (GameObject gameObject in CurrentScene.GameObjects)
		{
			gameObject.Update();
		}

		// Delete anything that needs to go
		for (int i = CurrentScene.GameObjects.Count - 1; i >= 0; i--)
		{
			if (CurrentScene.GameObjects[i].QueuedForDeletion)
			{
				CurrentScene.Remove(CurrentScene.GameObjects[i]);
			}
		}

	}

	public static void DrawCurrentScene()
	{
		// Draw 3D stuff
		Raylib.BeginMode3D(CurrentScene.ActiveCamera);
		for (int i = CurrentScene.GameObjects.Count - 1; i >= 0 ; i--)
		{
			CurrentScene.GameObjects[i].Draw3D();

			// Draw debug stuff if needed
			if (Settings.Debug) CurrentScene.GameObjects[i].Draw3DDebug();
		}
		Raylib.DrawGrid(15, 1);
		Raylib.EndMode3D();

		// Draw 2D stuff
		for (int i = CurrentScene.GameObjects.Count - 1; i >= 0 ; i--)
		{
			CurrentScene.GameObjects[i].Draw2D();

			// Draw debug stuff if needed
			if (Settings.Debug)
			{
				Raylib.DrawText($"FPS: {Raylib.GetFPS()}", 10, 10, 30, Color.White);
				CurrentScene.GameObjects[i].Draw2DDebug();
			}
		}		
	}

	public static void CleanUpCurrentScene()
	{
		// Loop through everything and delete it
		for (int i = CurrentScene.GameObjects.Count - 1; i >= 0 ; i--)
		{
			CurrentScene.Remove(CurrentScene.GameObjects[i]);
		}
	}
}

abstract class Scene
{
	public List<GameObject> GameObjects = [];

	//? We must use a ref since its a struct
	private Camera3D camera;
	public ref Camera3D ActiveCamera => ref camera;

	// TODO: Don't do this like this
	public Scene() => Init();
	public abstract void Init();

	public void Add(GameObject newGameObject)
	{
		// Add it to the scene
		GameObjects.Add(newGameObject);

		// Call its start method
		newGameObject.Start();

		// Resort the rendering order thingy
		GameObjects = GameObjects.OrderByDescending(gameObject => gameObject.RenderPriority3D).ToList();
	}

	public void Remove(GameObject gameObject)
	{
		gameObject.CleanUp();
		GameObjects.Remove(gameObject);
	}
}