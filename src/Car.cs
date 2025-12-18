using System.Numerics;
using Raylib_cs;

class Car : GameObject
{
	private Model model;
	private Vector3 position;

	public override void Start()
	{
		model = AssetManager.LoadGlbModel("./assets/car.glb");
	}

	public override void Draw3D()
	{
		Raylib.DrawModel(model, position, 1f, Color.White);
	}

	public override void CleanUp()
	{
		Raylib.UnloadModel(model);
	}
}