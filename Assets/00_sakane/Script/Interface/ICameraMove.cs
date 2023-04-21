using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraMove
{
	void SetTarget(GameObject target);
	void SetCanMove(bool canMove = true);
}
