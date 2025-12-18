abstract class GameObject
{
	public GameObject() => Start();

	public virtual void Start() { }
	public virtual void Update() { }
	public virtual void Draw3D() { }
	public virtual void Draw2D() { }
	public virtual void CleanUp() { }
}