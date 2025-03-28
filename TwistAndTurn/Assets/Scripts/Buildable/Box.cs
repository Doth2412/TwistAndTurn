using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, IPlaceable, IMoveable
{
	GridSystem gridSystem;
	// Start is called before the first frame update
	void Start()
	{
		gridSystem = GridSystem.instance;
	}

	// Update is called once per frame
	void Update()
	{
	}
	
	public void ApplyEffect(IMoveable moveable)
	{
		Player player = moveable as Player;
		if(!player.children.Contains(transform))
		{
			player.children.Add(transform);
		}
	}
	
	public void MoveWithPlayer(float elapsedTime, float moveSpeed, Vector3 direction, Vector3 startPosition)
	{
		Vector3 endposition = startPosition + direction;
		transform.position = Vector3.Lerp(startPosition, endposition, elapsedTime / moveSpeed);
	}
}