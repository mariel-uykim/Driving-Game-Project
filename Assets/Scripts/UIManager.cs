using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public Text stopwatchText;
    public GameObject gameoverPanel;
    public Text resultText;
    public Text [] cpTxt;
    private String currentTime;

    //returns time
    public String CurrentTime()
    {
        return currentTime;
    }

    //displays stopwatch
    public void UpdateTime(TimeSpan time)
    {
        currentTime = time.Minutes.ToString() + ":" + time.Seconds.ToString() + ":" + time.Milliseconds.ToString();
        stopwatchText.text = currentTime;
    }

    //activates game over panel and displays corresponding text
    public void GameOverScreen(bool win, String [] cpTime)
    {
        gameoverPanel.gameObject.SetActive(true);

        if(win)
        {
            resultText.text = "YOU WIN!";
        }

        else
        {
            resultText.text = "YOU DIED!";
        }


        for(int i = 0; i < cpTxt.Length; i++)
        {   
            if(cpTime[i] == "")
            {
                cpTxt[i].text = "checkpoint" + (i + 1) + ": INCOMPLETE";
            }
            else if(cpTime[i] != null)
            {
                cpTxt[i].text = "checkpoint" + (i + 1) + ": " + cpTime[i];
            }
        }

    }

    //restart scene
    public void RestartScene()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
