using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float timeValue = 90f;
    public float timeToChangeScene = 8f;
    public TextMeshProUGUI timerText;
    public bool gameStart = true;
    public bool playerDead;
    string scene;

    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        playerDead = false;
    }

    void Update()
    {
        if (timeValue > 0) {
            timeValue -= Time.deltaTime;
        } else {
            timeValue = 0;
        }

        if (playerDead) {
            if (timeToChangeScene > 0) {
                timeToChangeScene -= Time.deltaTime;
            } else {
                scene = "Lobby";
                SceneManager.LoadScene(scene);
            }
        }

        if (gameStart) DisplayTime(timeValue);
    }

    void DisplayTime (float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
            scene = "Lobby";
            SceneManager.LoadScene(scene);
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    
    }
}
