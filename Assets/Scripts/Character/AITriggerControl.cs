using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITriggerControl : MonoBehaviour
{
    GameManager gameManager;
    Vector3 obstacleSize;
    EnemyControl enemyControl;
    GameObject parent;
    float escapePosition, parentSize;
    void Start()
    {
        gameManager = GameManager.Instance;
        parent = transform.parent.gameObject;
        parentSize = parent.GetComponent<Collider>().bounds.size.z;
        enemyControl = parent.GetComponent<EnemyControl>();
    }

    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Character" && other.tag != "Platform" && other.tag != "Invisible")
        {
            if (other.tag == "HalfDonutMovingStick")
            {
                other = other.transform.parent.GetComponent<Collider>();
                if(parent.transform.position.z < gameManager.platform.transform.position.z)
                {
                    escapePosition = (gameManager.platform.getSize().z / 2 + gameManager.platform.transform.position.z) + parent.transform.position.z;
                    Debug.Log("Hi");
                }
                else
                {
                    escapePosition = (gameManager.platform.transform.position.z - gameManager.platform.getSize().z / 2) + parent.transform.position.z;
                    Debug.Log("Ho");
                }
                escapePosition = escapePosition + parentSize;
            }
            else
            {
                obstacleSize = other.GetComponent<Collider>().bounds.size;
                if (parent.transform.position.z > other.transform.position.z)
                {
                    escapePosition = obstacleSize.z / 2 + (other.transform.position.z + parentSize);
                }
                else
                {
                    escapePosition = obstacleSize.z / 2 - (other.transform.position.z - parentSize);
                }
            }
            enemyControl.moveRightLeft = true;
            enemyControl.changePosition = escapePosition;
        }
    }
}
