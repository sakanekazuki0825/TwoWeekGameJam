using System.Collections.Generic;
using UnityEngine;

public interface IPanel
{
	void SetLinkDirection(List<Vector2> directions);
	bool IsOnTrain();
}
