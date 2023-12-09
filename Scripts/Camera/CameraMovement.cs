using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    float speed = .075f;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            speed = speed + .05f;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift)){
            speed = speed - .05f;
        }

        if(Input.GetKey(KeyCode.W)){
            transform.position += new Vector3(0, 0, speed);
        }
        if(Input.GetKey(KeyCode.S)){
            transform.position += new Vector3(0, 0, -speed);
        }
        if(Input.GetKey(KeyCode.A)){
            transform.position += new Vector3(-speed, 0, 0);
        }
        if(Input.GetKey(KeyCode.D)){
            transform.position += new Vector3(speed, 0, 0);
        }
        if(Input.GetKey(KeyCode.UpArrow)){
            transform.position += new Vector3(0, speed, 0);
        }
        if(Input.GetKey(KeyCode.DownArrow)){
            transform.position += new Vector3(0, -speed, 0);
        }

    }
}
