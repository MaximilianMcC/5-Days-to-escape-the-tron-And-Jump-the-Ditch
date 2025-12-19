abstract class GameObject
{
	// No ctors allowed
	// private GameObject() { }

	//? High numbers = rendered first
	//? Low numbers = rendered last
	public virtual int RenderPriority3D { get; set; } = 0;
	// public int RenderPriority2D { get; set; }

	public virtual void Start() { }
	public virtual void Update() { }

	public virtual void Draw3D() { }
	public virtual void Draw2D() { }

	public virtual void Draw3DDebug() { }
	public virtual void Draw2DDebug() { }

	public virtual void CleanUp() { }
}