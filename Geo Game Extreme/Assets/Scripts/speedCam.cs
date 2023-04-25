using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedCam : MonoBehaviour
{
    public Animation anim;
    public GameObject plane;
    public int state = 0;
    public float speedSlow;
    public float speedBoost;
    public float speedBoostBoost;
    public float rSpeedSlow;
    public float rSpeedBoost;
    
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
            plane.GetComponent<movement_gravity>().speed = speedSlow;
            plane.GetComponent<movement_gravity>().rotation_speed = rSpeedSlow;
        }

        if(Input.GetKeyDown(KeyCode.W) && state == 1)
        {
            state = 0;
            anim.Play("camera 1");
            plane.GetComponent<movement_gravity>().speed = speedBoost;
            plane.GetComponent<movement_gravity>().rotation_speed = rSpeedBoost;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && state == 0)
        {
            plane.GetComponent<movement_gravity>().speed = speedBoostBoost;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || state == 1)
        {
            if (state == 1)
            {
                plane.GetComponent<movement_gravity>().speed = speedSlow;
            }
            else if (state == 0)
            {
                plane.GetComponent<movement_gravity>().speed = speedBoost;
            }
        }
    }
}
