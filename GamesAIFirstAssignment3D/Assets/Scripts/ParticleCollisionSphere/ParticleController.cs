using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public int numberOfParticles = 10;
    public GameObject particlePrefab;
    public float repulsionForceMultiplier = 1f;
    public float initialVelocity = 3f;

    // Start is called before the first frame update
    void Start()
    {
        CreateParticles();
    }


    void CreateParticles()
    {
        for (int i = 0; i < numberOfParticles; i++)
        {
            // Instantiate particles at the center of the imaginary sphere
            GameObject particle = Instantiate(particlePrefab, Vector3.zero, Quaternion.identity);

            particle.transform.parent = transform;

            // Assign unique starting positions (inside the sphere)
            Vector3 randomPosition = Random.onUnitSphere; 

            Rigidbody rb = particle.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = randomPosition.normalized * initialVelocity;

                // Apply force to each particle based on the square of their distance
                float distanceSquared = Vector3.Distance(particle.transform.position, Vector3.zero);
                Vector3 forceDirection = -particle.transform.position.normalized;
                float forceMagnitude = distanceSquared;
                Vector3 force = forceDirection * forceMagnitude;

                rb.AddForce(force, ForceMode.Acceleration);
            }
        }
    }
}
