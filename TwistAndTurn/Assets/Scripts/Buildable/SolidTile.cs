using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidTile : MonoBehaviour, IPlaceable
{
	void Start()
	{
		
	}
	
	void Update()
	{
		
	}
	
	public void ApplyEffect(IMoveable moveable)
	{
		Player.instance.canMove = false;
	}
}
