using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public static FlockManager FM;
    public GameObject birdPrefab;
    public int numBirds = 20;
    public GameObject[] allBirds;
    public Vector2 spawnLimits = new Vector2(20, 20);

    public Vector2 goalPosLimits = new Vector2(20, 20);

    public Vector2 goalPos = Vector2.zero;

    [Header("Bird Settings")]
    [Range(0.0f, 15.0f)]
    public float minSpeed;
    [Range(0.0f, 10.0f)]
    public float maxSpeed;
    [Range(1.0f, 20.0f)]
    public float neighbourDistance;
    [Range(1.0f, 15.0f)]
    public float rotationSpeed;


    // Start is called before the first frame update
    void Start()
    {
        allBirds = new GameObject[numBirds];
        for (int i = 0; i < numBirds; i++)
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-spawnLimits.x, spawnLimits.x),
                                                                Random.Range(-spawnLimits.y, spawnLimits.y),
                                                                0);

            allBirds[i] = Instantiate(birdPrefab, pos, Quaternion.identity);
        }

        FM = this;
        goalPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Random.Range(0, 100) < 1)
        {
            goalPos = this.transform.position + new Vector3(Random.Range(-goalPosLimits.x, goalPosLimits.x),
                                                            Random.Range(-goalPosLimits.y, goalPosLimits.y),
                                                            0);
        
        }

    }
}