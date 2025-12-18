using System.Numerics;
using Raylib_cs;

class Player : GameObject
{
	private readonly float height = 1.7f;

	private readonly float sensitivity = 0.001f;
	private readonly float speed = 10f;

	public Camera3D Camera;
	public Vector3 Position;

	//? Values in radians
	public float Yaw;
	public float Pitch;
	private Vector3 ForwardsDirection => new Vector3(
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
		Vector2 mouseMovement = Raylib.GetMouseDelta() * sensitivity;

		// Add to the yaw (left/right)
		Yaw += mouseMovement.X;

		// Add to the pitch (up/down)
		Pitch -= mouseMovement.Y;
		Pitch = Math.Clamp(Pitch, -1.55f, 1.55f);

		// Update the cameras target direction
		Camera.Target = Camera.Position + ForwardsDirection;
	}

	private void Locomote()
	{
		// Get the input
		Vector3 movementInput = Vector3.Zero;
        if (Raylib.IsKeyDown(KeyboardKey.A)) movementInput.X--;
        if (Raylib.IsKeyDown(KeyboardKey.D)) movementInput.X++;
        if (Raylib.IsKeyDown(KeyboardKey.W)) movementInput.Z++;
        if (Raylib.IsKeyDown(KeyboardKey.S)) movementInput.Z--;

		// Normalise the input directions
		if (movementInput != Vector3.Zero) movementInput = Vector3.Normalize(movementInput);

		// Get the forwards and right directions
		Vector3 forwardInputDirection = Vector3.Normalize(ForwardsDirection * new Vector3(1, 0, 1));
		Vector3 rightInputDirection = Vector3.Normalize(Vector3.Cross(forwardInputDirection, Vector3.UnitY));

		// Create the movement
		Vector3 forwardMovement = forwardInputDirection * movementInput.Z;
		Vector3 rightMovement = rightInputDirection * movementInput.X;
		Vector3 movement = ((forwardMovement + rightMovement) * speed) * Raylib.GetFrameTime();

		// Move the player
		Position += movement;
	}

	public override void Update()
	{
		LookAround();
		Locomote();

		// Make the camera follow the player
		Camera.Position = Position + (Vector3.UnitY * height);
	}

	public override void Draw2D()
	{
		Raylib.DrawText($"Yaw: {Yaw}\nPitch: {Pitch}", 10, 10, 30, Color.White);
	}
}