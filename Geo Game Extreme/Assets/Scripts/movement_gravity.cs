using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement_gravity : MonoBehaviour
{
    public Transform pivot;

    Rigidbody rb;

    void Start()
    {
        rb=GetComponent<Rigidbody>();
    }

    public float speed = 2f;
    public float rotation_speed = 100f;
    void Update()
    {
        

        pivot.Rotate(transform.forward * speed, Space.World);
        
        if (Input.GetKey("d"))
        {
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * rotation_speed);
        }

        if (Input.GetKey("a"))
        {
            transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * rotation_speed);
        }

    }
}
