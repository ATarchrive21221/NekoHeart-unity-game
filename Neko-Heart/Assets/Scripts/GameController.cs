using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public Text timerText;
    public Text messageText;
    public Text fruitText;
    public Text instructionText;

    private float currentTime;
    private bool stopTimer;
    private bool winGame;
    private bool loseGame;

    float timerWait = 6;
    bool timerReached = false;

    private int totalFruit;

    // Start is called before the first frame update
    void Start()
    {
        totalFruit = FindObjectOfType<PlayerController>().GetFruit();

        currentTime = 30;
        stopTimer = false;
        loseGame = false;
        winGame = false;

        UpdateTimerText();
        UpdateFruitText();
        messageText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = currentTime - Time.deltaTime;

        // instructions disappear after 8 seconds
        if (currentTime <= 22)
        {
            instructionText.text = "";
        }

        // the game will run until timer hits 0
        if (currentTime <= 0 && !winGame)
        {
            stopTimer = true;
            loseGame = true;
        }

        if (totalFruit >= 20 && !loseGame)
        {
            stopTimer = true;
            winGame = true;
        }

        if (loseGame)
        {
            LoseGame();
        }
        else if (winGame)
        {
            WinGame();
        }

        UpdateTimerText();
        totalFruit = FindObjectOfType<PlayerController>().GetFruit();
        UpdateFruitText();
    }

    void WinGame()
    {
        if (!timerReached)
        {
            timerWait -= Time.deltaTime;
            messageText.text = "You Win! :3" +
                "\nTeleport to the next room in " + ((int)timerWait).ToString() + " seconds.";
        }

        if (!timerReached && timerWait <= 0)
        {
            messageText.text = "";
            timerWait = 0;
            FindObjectOfType<PlayerController>().SetFruitToZero();
            timerReached = false;
            FindObjectOfType<PlayerController>().GotoLevel2();
        }
            
    }

    void LoseGame()
    {
        messageText.text = "That's too bad :(" +
            "\nPress 'R' to restart this round";
        RestartGame();
    }

    void RestartGame()
    {
        FindObjectOfType<PlayerController>().SetFruitToZero();

        if (Input.GetKeyDown(KeyCode.R))
        {
            FindObjectOfType<PlayerController>().Restart();
        }  
    }

    void UpdateTimerText()
    {
        if (!stopTimer)
        {
            timerText.text = "Timer: " + ((int)currentTime).ToString();
        }
        
    }

    void UpdateFruitText()
    {
        fruitText.text = "Fruits: " + totalFruit;
    }
}
