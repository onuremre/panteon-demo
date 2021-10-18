using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    GameManager gameManager;
    public Material redWall;
    bool painted = false;

    void Start()
    {
        gameManager = GameManager.Instance;
    }
    void OnMouseOver()
    {
        if(gameManager.game == false && Input.GetMouseButton(0) && painted == false)
        {
            GetComponent<MeshRenderer>().material = redWall;
            gameManager.IncreasProgress();
            painted = true;
        }
    }
}
