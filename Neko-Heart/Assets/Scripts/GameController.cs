using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject tigerBalm;

    public Text timerText;
    public Text messageText;
    private int totalScore;

    private float currentTime;
    private bool stopTimer;
    private bool winGame;
    private bool loseGame;

    private static int apples = 0;
    private static int bananas = 0;

    // Start is called before the first frame update
    void Start()
    {
        timerText.text = "Timer: ";
        totalScore = FindObjectOfType<PlayerController>().GetScore();

        currentTime = 30;
        stopTimer = false;
        loseGame = false;
        winGame = false;

        // tigerBalm.SetActive(false);

        UpdateTimerText();
        messageText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = currentTime - Time.deltaTime;

        // the game will run until timer hits 0
        if (currentTime <= 0 && !winGame)
        {
            stopTimer = true;
            loseGame = true;
        }

        if (totalScore >= 250 && !loseGame)
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

        totalScore = FindObjectOfType<PlayerController>().GetScore();
        UpdateTimerText();

    }

    void WinGame()
    {
        messageText.text = "You Win This Round!" +
            "\nGo collect the tiger balm on the ground to go to the next round!";

        tigerBalm.SetActive(true);

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
    
    public void AddBananas(int bananasIn) 
    {
        bananas += bananasIn;
    }

    public void AddApples(int applesIn)
    {
        apples += applesIn;
    }

void UpdateTimerText()
    {
        if (!stopTimer)
        {
            timerText.text = "Timer: " + ((int)currentTime).ToString();
        }
        
    }
}
