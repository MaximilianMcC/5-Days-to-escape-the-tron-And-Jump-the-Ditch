using System.Numerics;
using Raylib_cs;

class Player : GameObject
{
	private readonly float height = 1.7f;

	public Camera3D Camera;
	public Vector3 Position;

	public float Yaw;
	public float Pitch;
	private Vector3 forwardsDirection => new(
        MathF.Cos(Pitch) * MathF.Sin(Yaw),
        MathF.Sin(Pitch),
       -MathF.Cos(Pitch) * MathF.Cos(Yaw)
    );

	public override void Start()
	{
		// Lock the players cursor to the centre of the screen
		Raylib.HideCursor();
		Raylib.DisableCursor();

		// Set up the camera
		Camera = new Camera3D(
			Position,
			Position + -Vector3.UnitZ,
			Vector3.UnitY,
			60f,
			CameraProjection.Perspective
		);
	}

	private void LookAround()
	{
		// Get mouse input
		float sensitivity = 0.001f;
		Vector2 mouseMovement = Raylib.GetMouseDelta() * sensitivity;

		// Add to the yaw (left/right)
		Yaw = (Yaw + mouseMovement.X) % 360;

		// Add to the pitch (up/down)
		Pitch -= mouseMovement.Y;
		Pitch = Math.Clamp(Pitch, -1.55f, 1.55f);

		// Update the cameras target direction
		Camera.Target = Camera.Position + forwardsDirection;
	}

	public override void Update()
	{
		LookAround();

		// Make the camera follow the player
		Camera.Position = Position + (Vector3.UnitY * height);
	}

	public override void Draw2D()
	{
		Raylib.DrawText($"Yaw: {Yaw}\nPitch: {Pitch}", 10, 10, 30, Color.White);
	}
}