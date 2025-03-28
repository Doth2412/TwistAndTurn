using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTile : MonoBehaviour, IPlaceable
{
	private List<GateTile> gates;

	void Start()
	{
		gates = new List<GateTile>(FindObjectsOfType<GateTile>());
	}

	public void ApplyEffect(IMoveable moveable)
	{
		foreach (GateTile gate in gates)
		{
			if (gate != null)
			{
				gate.RemoveKeyTile(this);
			}
		}
	}
}