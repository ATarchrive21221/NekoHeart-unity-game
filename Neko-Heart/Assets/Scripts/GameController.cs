using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public PlayerController playerController;

    public Text timerText;
    private int totalScore;

    private float currentTime;
    private bool stopTimer;
    private bool winGame;
    private bool loseGame;

    // Start is called before the first frame update
    void Start()
    {
        timerText.text = "Timer: ";
        totalScore = FindObjectOfType<PlayerController>().GetScore();

        currentTime = 45;
        stopTimer = false;

        UpdateTimerText();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = currentTime - Time.deltaTime;

        // the game will run until timer hits 0
        if (currentTime <= 0)
        {
            stopTimer = true;
        }

        UpdateTimerText();

    }

    void WinGame()
    {

    }

    void LoseGame()
    {

    }

    void UpdateTimerText()
    {
        if (!stopTimer)
        {
            timerText.text = "Timer: " + ((int)currentTime).ToString();
        }
        
    }
}
