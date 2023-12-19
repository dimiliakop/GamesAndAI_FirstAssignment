using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{

    float speed;


    bool turning = false;

    private GameObject predatorPrefab = null;

    private float distance_threshold_to_flee = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(FlockManager.FM.minSpeed, FlockManager.FM.maxSpeed);
        if (predatorPrefab == null)
        {
            predatorPrefab = GameObject.FindWithTag("Player");
        }

    }

    // Update is called once per frame
    void Update()
    {


        Bounds b = new Bounds(FlockManager.FM.transform.position, FlockManager.FM.airLimit * 100);

        turning = isTurning(b);


        if (turning)
        {
            Turning();
        }
        else
        {
            Flocking();
        }
        this.transform.Translate(0, 0, speed * Time.deltaTime);


    }

    bool isTurning(Bounds b)
    {
        if (!b.Contains(this.transform.position))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Turning()
    {
        Vector3 direction = FlockManager.FM.transform.position - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
        Quaternion.LookRotation(direction),
        FlockManager.FM.rotationSpeed * Time.deltaTime);
    }

    void Flocking()
    {
        if (Random.Range(0, 100) < 10)
        {
            speed = Random.Range(FlockManager.FM.minSpeed, FlockManager.FM.maxSpeed);
        }

        if (Random.Range(0, 100) < 20)
        {
            applyRules();
        }

    }

    void applyRules()
    {
        GameObject[] gos;
        gos = FlockManager.FM.allBirds; // get all birds

        Vector3 vcentre = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        float gSpeed = 0.1f;

        Vector3 goalPos = FlockManager.FM.goalPos;

        float nDist;

        int groupSize = 0;

        foreach (GameObject go in gos)
        {
            if (go != this.gameObject)
            {
                nDist = Vector3.Distance(go.transform.position, this.transform.position);

                if (nDist <= FlockManager.FM.neighbourDistance) // if neighbour
                {
                    vcentre += go.transform.position;
                    groupSize++;

                    if (nDist < 3.5f) // if too close
                    {
                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }

                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }

        if (groupSize > 0)
        {
            vcentre = vcentre / groupSize + (goalPos - this.transform.position); // average position
            speed = gSpeed / groupSize;
            if (speed > FlockManager.FM.maxSpeed)
            {
                speed = FlockManager.FM.maxSpeed;
            }


            if (Vector3.Distance(transform.position, predatorPrefab.transform.position) <= distance_threshold_to_flee)
            {
                FleePredator();
                speed = speed * 2;

            }
            else
            {
                Vector3 direction = (vcentre + vavoid) - transform.position;

                if (direction != Vector3.zero)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation,
                        Quaternion.LookRotation(direction),
                        FlockManager.FM.rotationSpeed * Time.deltaTime);


                }

            }


        }
    }


    private void FleePredator()
    {
        Vector3 direction = predatorPrefab.transform.position - this.transform.position; // Notice the swapped positions
        direction = -direction; // Negate the direction to make the flock turn away from the predator
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
        Quaternion.LookRotation(direction),
        FlockManager.FM.rotationSpeed * Time.deltaTime);
    }
}
