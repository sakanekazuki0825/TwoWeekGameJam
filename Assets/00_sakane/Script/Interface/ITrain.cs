using UnityEngine;

public interface ITrain
{
	public void Go();
	public void Stop();
	public void AddSpeed(float speed);
	public void Curve(Vector3 curvePos, Vector3 targePos);
}