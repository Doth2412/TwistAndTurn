using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTile : MonoBehaviour, IPlaceable
{	
	static public bool isGoalReached;
	// Start is called before the first frame update
	void Start()
	{
		isGoalReached = false;
	}
	
	public void ApplyEffect(IMoveable moveable)
	{
		isGoalReached = true;
	}
}
