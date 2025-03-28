using UnityEngine;

public class PlayerTrail : MonoBehaviour
{
    private ParticleSystem playerParticleSystem;

    void Start()
    {
        playerParticleSystem = GetComponent<ParticleSystem>();
        playerParticleSystem.transform.position = Player.instance.transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            playerParticleSystem.Play();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            playerParticleSystem.Stop();
        }
    }
}