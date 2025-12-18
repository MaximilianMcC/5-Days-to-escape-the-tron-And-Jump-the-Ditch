using System.Numerics;
using Raylib_cs;

class Player : GameObject
{
	private readonly float height = 1.7f;

	public Camera3D Camera;
	public Vector3 Position;

	public override void Start()
	{
		// Set up the camera
		Camera = new Camera3D(
			Position,
			Position + -Vector3.UnitZ,
			Vector3.UnitY,
			60f,
			CameraProjection.Perspective
		);
	}

	public override void Update()
	{
		Camera.Position = Position + (Vector3.UnitY * height);
		Camera.Target = Camera.Position + -Vector3.UnitZ;
	}
}