using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Animator anim;
    int IdleSimple;
    int FlyingFWD;
 

    private float zAxisInput;
    private float xAxisInput;
    public float speed = 20.0f;
    public float turnSpeed = 150f;
    public float rotationSpeed = 100.0f;
    // Start is called before the first frame update
    void Start()
    {
        
        anim = GetComponent<Animator>();
        IdleSimple = Animator.StringToHash("IdleSimple");
        FlyingFWD = Animator.StringToHash("FlyingFWD");

    }

    // Update is called once per frame
    void Update()
    {
        PlayerDirection();
        if (!Input.anyKey)
        {
            anim.SetBool(IdleSimple, true);
            anim.SetBool(FlyingFWD, false);
        }
        
    }


    private void PlayerDirection()
    {
        float zAxisInput = Input.GetAxis("Horizontal");
        float xAxisInput = Input.GetAxis("Vertical");

        // Rotate around Y-axis for horizontal input
        transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * zAxisInput, Space.World);

        // Rotate around X-axis for vertical input
        transform.Rotate(Vector3.right * Time.deltaTime * turnSpeed * xAxisInput, Space.World);

        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            anim.SetBool(IdleSimple, false);
            anim.SetBool(FlyingFWD, true);


        }
    }
}