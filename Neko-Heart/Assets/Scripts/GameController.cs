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
        FindObjectOfType<PlayerController>().SetFruitToZero();
        FindObjectOfType<PlayerController>().GotoLevel2();
    }

    void LoseGame()
    {
        messageText.text = "That's too bad :(" +
            "\nPress 'R' to restart this round";
        RestartGame();
    }

    void RestartGame()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
