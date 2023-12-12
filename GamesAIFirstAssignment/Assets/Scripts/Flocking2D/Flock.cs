using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    float speed;
    bool turning = false;

    void Start()
    {
        speed = Random.Range(FlockManager.FM.minSpeed, FlockManager.FM.maxSpeed);
    }

    void Update()
    {
        Bounds b = new Bounds(FlockManager.FM.transform.position, FlockManager.FM.goalPosLimits * 2);

        turning = !b.Contains(this.transform.position);

        if (turning && Random.Range(0, 100) < 90)
        {
            Turning();
        }
        
           
        Flocking();
        

        this.transform.Translate(0, speed * Time.deltaTime, 0);
    }

    void Turning()
    {
        Vector2 direction = FlockManager.FM.transform.position - this.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, q, FlockManager.FM.rotationSpeed * Time.deltaTime);
    }

    void Flocking()
    {
        if (Random.Range(0, 100) < 10)
        {
            speed = Random.Range(FlockManager.FM.minSpeed, FlockManager.FM.maxSpeed);
        }

        if (Random.Range(0, 100) < 20)
        {
            ApplyRules();
        }
    }

    void ApplyRules()
    {
        GameObject[] gos;
        gos = FlockManager.FM.allBirds;

        Vector2 vcentre = Vector2.zero;
        Vector2 vavoid = Vector2.zero;
        float gSpeed = 0.1f;

        Vector2 goalPos = FlockManager.FM.goalPos;

        float nDist;
        int groupSize = 0;

        foreach (GameObject go in gos)
        {
            if (go != this.gameObject)
            {
                nDist = Vector2.Distance(go.transform.position, this.transform.position);
                if (nDist <= FlockManager.FM.neighbourDistance)
                {
                    vcentre += (Vector2)go.transform.position;
                    groupSize++;

                    if (nDist < 3.5f)
                    {
                        vavoid = vavoid + ((Vector2)this.transform.position - (Vector2)go.transform.position);
                    }
                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }

        if (groupSize > 0)
        {
            vcentre = vcentre / groupSize + (goalPos - (Vector2)this.transform.position);
            speed = gSpeed / groupSize;

            if (speed > FlockManager.FM.maxSpeed)
            {
                speed = FlockManager.FM.maxSpeed;
            }

            Vector2 direction = (vcentre + vavoid) - (Vector2)transform.position + (goalPos - (Vector2)this.transform.position);
            if (direction != Vector2.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, q, FlockManager.FM.rotationSpeed * Time.deltaTime);
            }
        }
    }
}