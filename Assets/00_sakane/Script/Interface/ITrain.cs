using MyEnum;

public interface ITrain
{
	public void Go();
	public void Stop();
	public void AddSpeed(float speed);
	public void Curve(Direction dir);
}
