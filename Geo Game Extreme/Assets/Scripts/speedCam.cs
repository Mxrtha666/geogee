using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedCam : MonoBehaviour
{
    public Animation anim;
    public GameObject plane;
    public int state = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S) && state == 0)
        {
            state = 1;
            anim.Play("camera");
            plane.GetComponent<movement_gravity>().speed = 2f;
        }

        if(Input.GetKeyDown(KeyCode.W) && state == 1)
        {
            state = 0;
            anim.Play("camera 1");
            plane.GetComponent<movement_gravity>().speed = 4f;
        }
    }
}
