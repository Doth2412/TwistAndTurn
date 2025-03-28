using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmTile : MonoBehaviour, IPlaceable
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
		if (Player.instance.children.Count > 0)
		{
			// Remove the last Transform from the children list
			int lastIndex = Player.instance.children.Count - 1;
			Transform lastChild = Player.instance.children[lastIndex];
			GridSystem.instance.moveables[(int)lastChild.position.x, (int)transform.position.y] = null;
			Player.instance.children.RemoveAt(lastIndex);
			Destroy(lastChild.gameObject);
		}
		else
		{
			((MonoBehaviour)moveable).gameObject.SetActive(false);
		}
		GridSystem.instance.levelMatrix[(int)transform.position.x, (int)transform.position.y] = null;
		Destroy(gameObject);
	}

	
}
