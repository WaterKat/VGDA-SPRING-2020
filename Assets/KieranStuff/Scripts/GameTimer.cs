using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float timeGiven = 180f;
    private float curTimeLeft = 180f;
    public float curTimeLeftDisplay;
    public bool countingDown = true;
    private bool gameRunning = true;

    public PlayerHealth playerHealth;
    public TextMeshProUGUI timerText;

    private void Start()
    {
        curTimeLeft = timeGiven;
        countingDown = true;
        Time.timeScale = 1f;
    }
    private void Update()
    {
        if(countingDown && gameRunning)
        {
            curTimeLeft -= Time.deltaTime;
            curTimeLeftDisplay = Mathf.Round(curTimeLeft * 10f) * 0.1f;
            timerText.text = "Consciousness Disconnect in " + curTimeLeftDisplay + "s";
            if (curTimeLeft <= 0f)
            {
                countingDown = false;
                curTimeLeft = 0f;
            }
        }
        if(!countingDown)
        {
            playerHealth.LoseGame();
        }
    }

    public void TimeStop()
    {
        gameRunning = false;
    }
}
