using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    GameManager gameManager;
    Platform platform;
    GameObject movingStick, rotator;
    Vector3 platformSize;
    public int direction = 1;
    public float obstacleSpeed = 1;
    float stickSize;

    
    void Start()
    {
        gameManager = GameManager.Instance;
        platform = gameManager.platform;
        platformSize = gameManager.platform.getSize();
        if(tag == "HalfDonutObstacle")
        {
            movingStick = this.gameObject.transform.Find("MovingStick").gameObject;
            //stick = movingStick.transform.Find("Half_Donut_Stick").gameObject;
            //stickSize = stick.GetComponent<Collider>().bounds.size.x;
        }
        if(tag == "RotatorObstacle")
        {
            movingStick = this.gameObject.transform.Find("RotatingStick").gameObject;
            rotator = this.gameObject.transform.Find("Rotator").gameObject;
        }
    }

    
    void Update()
    {
        switch (tag)
        {
            case "HorizontalObstacle":
                HorizontalObstacleMove();
                break;
            case "HalfDonutObstacle":
                HalfDonutObstacleMove();
                break;
            case "RotatorObstacle":
                RotatorObstacleMove();
                break;
        }
    }


    void HorizontalObstacleMove()
    {
        if(transform.position.z  > platform.transform.position.z + (platformSize.z/2))
        {
            direction = -1;
        }
        if(transform.position.z < platform.transform.position.z - (platformSize.z / 2))
        {
            direction = 1;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, (float)(transform.position.z + 1 * obstacleSpeed * direction * gameManager.gameSpeed * Time.deltaTime));
    }

    void HalfDonutObstacleMove()
    {
        
        /*if(movingStick.transform.localPosition.x > stickSize / 2)
        {
            direction = -1;
        }
        if(movingStick.transform.localPosition.x  < (stickSize / 2) * -1)
        {
            direction = 1;
        }*/
        if(movingStick.transform.localPosition.x > 0.03)
        {
            direction = -1;
        }
        if (movingStick.transform.localPosition.x < - 0.5)
        {
            direction = 1;
        }
        movingStick.transform.localPosition = new Vector3((float)(movingStick.transform.localPosition.x + 0.1 * obstacleSpeed * direction * gameManager.gameSpeed * Time.deltaTime), movingStick.transform.localPosition.y, movingStick.transform.localPosition.z);
        
    }

    void RotatorObstacleMove()
    {
        movingStick.transform.RotateAround(rotator.transform.position, rotator.transform.up, obstacleSpeed * gameManager.gameSpeed * Time.deltaTime);
        if(movingStick.transform.position.z > rotator.transform.position.z)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
    }
}
