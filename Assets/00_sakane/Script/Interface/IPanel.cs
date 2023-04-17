using System.Collections.Generic;
using UnityEngine;

public interface IPanel
{
	void SetLinkDirection(List<Vector2> directions);
	bool IsOnTrain();
	void SetSprite(Sprite sprite);
	void SetChangeSpeedValue(float speed);
}