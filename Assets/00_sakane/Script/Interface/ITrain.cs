using UnityEngine;

public interface ITrain
{
	void Go();
	void Stop();
	void AddSpeed(float speed);
	void Curve(Vector3 curvePos, Vector3 targePos);
	void GameClear(Vector3 goalPos);
	void GoTitle();
	bool IsScreenOut();
}