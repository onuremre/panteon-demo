using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float rotationSpeed = 5;
    public bool rotatingPlatform = false;

    void Update()
    {
        if(rotatingPlatform == true)
        {
            rotatePlatform();
        }    
    }

    public Vector3 getSize()
    {
        Vector3 size = GetComponent<Renderer>().bounds.size;
        return size;
    }

    public void rotatePlatform()
    {
        Quaternion rotationZ = Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, new Vector3(1, 0, 0));
        transform.rotation = rotationZ * transform.rotation;
    }

    void OnTriggerEnter(Collider other)
    {
        if (rotatingPlatform && other.name == "Boy")
        {
            other.GetComponent<PlayerController>().touchPosZ -= rotationSpeed * Time.deltaTime;
        }
    }
}
