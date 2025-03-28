using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningEffect : MonoBehaviour
{
    public float spinSpeed = 100f; // Speed of the spin
    public bool clockwise = true; // Direction of the spin
    
    void Start()
    {
        spinSpeed = 100f; // Speed of the spin
        clockwise = true; // Direction of the spin
    }

// Update is called once per frame
void Update()
    {
        // Determine the direction of the spin
        float direction = clockwise ? -1f : 1f;

        // Rotate the GameObject around its Z-axis
        transform.Rotate(0f, 0f, direction * spinSpeed * Time.deltaTime);
    }
}