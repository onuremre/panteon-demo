using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    GameManager gameManager;
    bool first = true;

    void Start()
    {
        gameManager = GameManager.Instance;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Character")
        {
            if(other.name == "Boy")
            {
                gameManager.game = false;
                other.GetComponent<PlayerController>().moveCameratoPlayer();
                if(first == true)
                {
                    other.GetComponent<PlayerController>().Victory();
                }
                else
                {
                    other.GetComponent<PlayerController>().Defeat();
                }
            }
            else
            {
                other.GetComponent<EnemyControl>().moveForward = false;
                if (first == true)
                {
                    other.GetComponent<EnemyControl>().Victory();
                }
                else
                {
                    other.GetComponent<EnemyControl>().Defeat();
                }
                
            }

            first = false;
        }
    }
}
