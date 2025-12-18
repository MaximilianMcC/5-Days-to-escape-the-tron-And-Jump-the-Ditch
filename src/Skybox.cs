using System.Numerics;
using Raylib_cs;

class Skybox : GameObject
{
	private Model skybox;
	private Shader skyboxShader;
	private Texture2D cubemap;

	public override void Start()
	{
		// Create the skybox model (normal cube)
		Mesh mesh = Raylib.GenMeshCube(1f, 1f, 1f);
		skybox = Raylib.LoadModelFromMesh(mesh);

		// Load the shader
		//? stolen from raylib examples website thingy
		skyboxShader = AssetManager.LoadShader("./assets/shaders/skybox.vs", "./assets/shaders/skybox.fs");

		// Set the shaders settings, then apply it to the model
		Raylib.SetShaderValue(skyboxShader, Raylib.GetShaderLocation(skyboxShader, "environmentMap"), MaterialMapIndex.Cubemap, ShaderUniformDataType.Int);
		Raylib.SetMaterialShader(ref skybox, 0, ref skyboxShader);

		// Load the skybox texture, then put it on the cube
		//? stolen from raylib examples website thingy
		// TODO: UNLOAD
		Image skyboxImage = AssetManager.LoadImage("./assets/textures/skybox.png");
		cubemap = Raylib.LoadTextureCubemap(skyboxImage, CubemapLayout.AutoDetect);
		Raylib.UnloadImage(skyboxImage);
		Raylib.SetMaterialTexture(ref skybox, 0, MaterialMapIndex.Cubemap, ref cubemap);
	}

	public override void Draw3D()
	{
		// Let us see inside the cube and
		// remove all 'volume' from it so it
		// looks like its endless/super far away
		Rlgl.DisableBackfaceCulling();
		Rlgl.DisableDepthMask();

		// Draw the actual skybox
		Raylib.DrawModel(skybox, Vector3.Zero, 1f, Color.White);

		// Turn everything back on
		Rlgl.EnableBackfaceCulling();
		Rlgl.EnableDepthMask();
	}

	public override void CleanUp()
	{
		Raylib.UnloadModel(skybox);
		Raylib.UnloadTexture(cubemap);
		Raylib.UnloadShader(skyboxShader);
	}
}