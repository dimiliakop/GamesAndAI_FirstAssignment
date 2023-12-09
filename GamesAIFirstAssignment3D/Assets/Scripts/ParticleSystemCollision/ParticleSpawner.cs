using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public GameObject particlePrefab; 
    public float spawnInterval = 0.2f;
    public float particleLifespan = 5f;
    private float timer = 0f; 

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnParticle(); 
            timer = 0f; 
        }
    }

    void SpawnParticle()
    {
        GameObject newParticle = Instantiate(particlePrefab, transform.position, Quaternion.identity);

        DestroyParticleAfterDelay(newParticle, particleLifespan);
    }

    void DestroyParticleAfterDelay(GameObject particle, float delay)
    {
        if (particle != null)
        {
            Destroy(particle, delay);
        }
    }
}
