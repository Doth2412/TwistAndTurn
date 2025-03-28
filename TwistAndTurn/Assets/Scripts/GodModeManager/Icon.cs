using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]

public class Icon : MonoBehaviour
{
	public GameObject buidablePrefab;
	private Image image;
	
	void Start()
	{
		image = GetComponent<Image>();
		image.sprite = buidablePrefab.GetComponent<SpriteRenderer>().sprite;
	}
}
