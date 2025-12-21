class Hamilton : Scene
{
	// TODO: Don't do this like this
	public override void Init()
	{
		GameObjects.Add(new Skybox());
		GameObjects.Add(new Player());
		GameObjects.Add(new Car());

		foreach (GameObject game in GameObjects)
		{
			Console.WriteLine(game.ToString());
		}
	}
}