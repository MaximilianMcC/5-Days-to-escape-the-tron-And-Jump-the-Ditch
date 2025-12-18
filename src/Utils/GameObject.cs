abstract class GameObject
{
	public GameObject() => Start();

	public virtual void Start() { }
	public virtual void Update() { }
	public virtual void Render() { }
	public virtual void CleanUp() { }
}