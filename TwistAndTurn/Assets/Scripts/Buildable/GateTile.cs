using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTile : MonoBehaviour, IPlaceable
{
	public List<KeyTile> keyTiles;
	// Start is called before the first frame update
	void Start()
	{
		keyTiles = new List<KeyTile>(FindObjectsOfType<KeyTile>());
	}

	// Update is called once per frame
	void Update()
	{
	}

	public void ApplyEffect(IMoveable moveable)
	{
		if(keyTiles.Count != 0)
		{
			Player.instance.canMove = false;
		}
	}

	public void RemoveKeyTile(KeyTile keyTile)
	{
		keyTiles.Remove(keyTile);
		GridSystem.instance.levelMatrix[(int)keyTile.transform.position.x, (int)keyTile.transform.position.y] = null;
		Destroy(keyTile.gameObject);
		if (keyTiles.Count == 0)
		{
			GridSystem.instance.levelMatrix[(int)transform.position.x, (int)transform.position.y] = null;
			Destroy(gameObject);
		}
	}
}