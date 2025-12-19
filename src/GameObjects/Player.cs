using System.Numerics;
using Raylib_cs;

class Player : GameObject
{
	private readonly float height = 1.7f;
	private readonly float speed = 10f;

	public bool CanMove { get; set; } = true;
	public bool CanLookAround { get; set; } = true;

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
		SceneManager.CurrentScene.ActiveCamera = new Camera3D(
			Vector3.Zero,
			Vector3.Zero,
			Vector3.UnitY,
			60f,
			CameraProjection.Perspective
		);
		LookAround();
		Locomote();
	}

	private void LookAround()
	{
		// Get mouse input
		Vector2 mouseMovement = Raylib.GetMouseDelta() * Settings.Sensitivity;

		// Add to the yaw (left/right)
		Yaw += mouseMovement.X;

		// Add to the pitch (up/down)
		Pitch -= mouseMovement.Y;
		Pitch = Math.Clamp(Pitch, -1.55f, 1.55f);

		// Update the cameras target direction
		ref Camera3D camera = ref SceneManager.CurrentScene.ActiveCamera;
		camera.Target = camera.Position + ForwardsDirection;
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
		// Movement
		if (CanLookAround) LookAround();
		if (CanMove) Locomote();

		// Make the camera follow the player
		ref Camera3D camera = ref SceneManager.CurrentScene.ActiveCamera;
		camera.Position = Position + (Vector3.UnitY * height);
	}

	public override void Draw2DDebug()
	{
		ref Camera3D camera = ref SceneManager.CurrentScene.ActiveCamera;
		Raylib.DrawText($"{Position}\n{camera.Position}", 100, 100, 20, Color.White);
	}
}