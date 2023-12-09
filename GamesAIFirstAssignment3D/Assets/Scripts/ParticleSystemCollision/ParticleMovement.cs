using UnityEngine;

public class ParticleMovement: MonoBehaviour
{
    private float gravity = 9.81f;
    private float resistance = 0.1f;
    private float damping = 0.5f;
    private Vector3 velocity;

    void Start()
    {
        Vector3 randomDirection = Random.insideUnitSphere.normalized;
        velocity = (randomDirection + Vector3.up).normalized * Random.Range(8f, 10f);
    }

    void Update()
    {
            velocity.y -= gravity * Time.deltaTime;

            velocity -= resistance * velocity * Time.deltaTime;

            transform.position += velocity * Time.deltaTime;

            CheckCollisionWithCube();
    }

    void CheckCollisionWithCube()
    {
        float cubeSize = 3f; 
        float halfCubeSize = cubeSize * 0.5f;

        Vector3 position = transform.position;

        if (position.x < -halfCubeSize || position.x > halfCubeSize)
        {
            velocity.x *= -damping;
            position.x = Mathf.Clamp(position.x, -halfCubeSize, halfCubeSize);
        }
        if (position.y < -halfCubeSize || position.y > halfCubeSize)
        {
            velocity.y *= -damping;
            position.y = Mathf.Clamp(position.y, -halfCubeSize, halfCubeSize);
        }
        if (position.z < -halfCubeSize || position.z > halfCubeSize)
        {
            velocity.z *= -damping;
            position.z = Mathf.Clamp(position.z, -halfCubeSize, halfCubeSize);
        }

        transform.position = position;
    }
}
