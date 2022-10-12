using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI txtKillCount;

    [SerializeField]
    private TextMeshProUGUI txtTimer;


    // Timer
    private float curFrames = 0;
    private float curSeconds = 0;
    private float curMinutes = 0;

    #region Engine Functions

    private void Awake()
    {
        // Limit Framerate to 60FPS and disable Vsync
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    private void Update()
    {
        SetKillCountText();
        SetTimerText();
    }

    #endregion

    #region My Functions

    private void SetKillCountText()
    {

        txtKillCount.text = Player.killCount.ToString();

    }

    private void SetTimerText()
    {
        // Calculate Time
        if (curFrames < 60)
        {
            curFrames ++;
        }
        else
        {
            curFrames = 0;
            curSeconds++;
            if (curSeconds >= 60)
            {
                curSeconds = 0;
                curMinutes++;
            }
        }

        // Convert Time to Text

        string time = "";

        // Text Minutes
        if (curMinutes < 10)
        {
            time += "0" + ((int)curMinutes).ToString();
        }
        else
        {
            time += ((int)curMinutes).ToString();
        }

        // Text Seconds
        if (curSeconds < 10)
        {
            time += ":0" + ((int)curSeconds).ToString();
        }
        else
        {
            time += ":" + ((int)curSeconds).ToString();
        }

        // Text Frames
        if (curFrames < 10)
        {
            time += ":0" + ((int)curFrames).ToString();
        }
        else
        {
            time += ":" + ((int)curFrames).ToString();
        }


        txtTimer.text = time;

    }

    #endregion
}
