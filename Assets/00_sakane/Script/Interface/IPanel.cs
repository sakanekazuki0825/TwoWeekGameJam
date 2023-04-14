using System.Collections.Generic;
using MyEnum;

public interface IPanel
{
	void SetLinkDirection(List<Direction> directions);
	bool IsOnTrain();
}
