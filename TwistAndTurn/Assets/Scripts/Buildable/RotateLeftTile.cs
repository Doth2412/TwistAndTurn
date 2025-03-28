using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLeftTile : MonoBehaviour, IPlaceable
{
	
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void ApplyEffect(IMoveable moveable)
	{
		Player player = (Player)moveable;
		if (player.startPosition + player.moveDirection == transform.position)
		{
			player.rotation = Player.Rotation.Left;
		}
	}
}
