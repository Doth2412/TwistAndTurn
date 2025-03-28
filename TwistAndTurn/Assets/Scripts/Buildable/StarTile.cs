using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarTile : MonoBehaviour, IPlaceable
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
		GridSystem.instance.starCollected++;
        GridSystem.instance.levelMatrix[(int)transform.position.x, (int)transform.position.y] = null;
        Destroy(gameObject);
    }

	
}
