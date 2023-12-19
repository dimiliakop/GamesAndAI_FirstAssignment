using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public static FlockManager FM;
    public GameObject birdPrefab;
    public int numBirds = 20;
    public GameObject[] allBirds;
    public Vector3 airLimit = new Vector3(20, 20, 20);

    public Vector3 goalPos = Vector3.zero;

    [Header("Bird Settings")]
    [Range(0.0f, 5.0f)]
    public float minSpeed;
    [Range(0.0f, 20.0f)]
    public float maxSpeed;
    [Range(1.0f, 50.0f)]
    public float neighbourDistance;
    [Range(1.0f, 20.0f)]
    public float rotationSpeed;


    // Start is called before the first frame update
    void Start()
    {
        allBirds = new GameObject[numBirds];
        for (int i = 0; i < numBirds; i++)
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-airLimit.x, airLimit.x),
                                                                Random.Range(-airLimit.y, airLimit.y),
                                                                Random.Range(-airLimit.z, airLimit.z));
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
            goalPos = this.transform.position + new Vector3(Random.Range(-airLimit.x * 100, airLimit.x * 100),
                                                            Random.Range(-airLimit.y * 100, airLimit.y * 100),
                                                            Random.Range(-airLimit.z * 100, airLimit.z * 100));
        }

    }
}
