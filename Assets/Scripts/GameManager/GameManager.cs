using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Platform platform;
    public GameObject player, finished;
    GameObject[] characters;
    Animator animator;
    EnemyControl enemyControl;
    public Slider progressBar;
    public Text score, finalScore;
    public int gameSpeed = 1;
    public bool game = false;
    int progressBarValue = 0, playerPlacement = 1, tempPlacement = 1;
    float playerTransformX; 

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        characters = GameObject.FindGameObjectsWithTag("Character");
    }

    void Update()
    {
        playerTransformX = player.transform.position.x;
        for (int i = 0; i < characters.Length; i++)
        {
            if(characters[i].name != "Boy" && playerTransformX < characters[i].transform.position.x)
            {
                tempPlacement++;
            }
        }
        if(tempPlacement != playerPlacement)
        {
            changePlayerPlacement();
        }

        tempPlacement = 1;
    }

    public void IncreasProgress()
    {
        progressBar.value += 0.01f;
        progressBarValue++;
        progressBar.GetComponentInChildren<Text>().text = "% " + progressBarValue.ToString();

        if(progressBarValue == 100)
        {
            finishGame();
        }
    }

    void changePlayerPlacement()
    {
        if(game == true)
        {
            playerPlacement = tempPlacement;
            score.text = playerPlacement.ToString() + "st";
        }
        
    }

    public void playGame()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            animator = characters[i].GetComponent<Animator>();
            animator.SetBool("Game", true);
            if(characters[i].name != "Boy")
            {
                enemyControl = characters[i].GetComponent<EnemyControl>();
                enemyControl.moveForward = true;
            }
            game = true;
        }
    }

    void finishGame()
    {
        finalScore.text = "You Finished the Game " + playerPlacement.ToString() + "st";
        finished.SetActive(true);
    }

    public void playAgain()
    {
        SceneManager.LoadScene("Game");
    }

}
