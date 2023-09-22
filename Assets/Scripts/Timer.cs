using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public Text timeText;

    public static GameObject loseText;

    private void Start()
    {
        loseText = GameObject.Find("Losing");
        // Starts the timer automatically
        timerIsRunning = true;
        loseText.SetActive(false);
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time has run out!");
                Time.timeScale = 0;
                timeRemaining = 0;
                loseText.SetActive(true);
                timerIsRunning = false;
            }
        }
        DisplayTime(timeRemaining);

        if(Card.pairsFound == 7)
        {
            timerIsRunning = false;
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}