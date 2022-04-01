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

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerControllerObj = GameObject.Find("PlayerController");
        playerController = playerControllerObj.GetComponent<PlayerController>();
        totalScore = playerController.GetScore();

        UpdateTimerText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateTimerText()
    {
        timerText.text = "Timer: ";
    }
}
