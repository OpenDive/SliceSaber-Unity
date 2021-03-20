using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    private float timeRemaining = 100000;
    public TMP_Text timeText;

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
        }
        else
        {
            Debug.Log("Time up!!!!!!!!!!!");
        }

    }

    void DisplayTime(float timeToDisplay)
    {
        float days = Mathf.FloorToInt(timeToDisplay / 86400);
        float hourSeconds = Mathf.FloorToInt(timeToDisplay % 86400);
        float hours = Mathf.FloorToInt(hourSeconds / 3600);
        float minSeconds = Mathf.FloorToInt(hourSeconds % 3600);
        float minutes = Mathf.FloorToInt(minSeconds / 60);
        float seconds = Mathf.FloorToInt(minSeconds % 60);

        timeText.text = string.Format("{0:00} day {1:00} hour {2:00} min {3:00} sec", days, hours, minutes, seconds);
    }
}
