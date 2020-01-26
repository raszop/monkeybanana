using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities
{
    public const string highestScoreKey = "highestScore";
    public const string autoFireKey = "autoFire";

    public const int isOn = 1;
    public const int isOff = 0;

    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt(highestScoreKey, 0);
    }

    public static void SetNewHighScore(int score)
    {
        PlayerPrefs.SetInt(highestScoreKey, score);
    }

    public static void ChangeAutoFireSetting(bool state)
    {
        PlayerPrefs.SetInt(autoFireKey, state == true ? isOn : isOff);
    }

    public static bool CheckAutoFireSetting()
    {
        return PlayerPrefs.GetInt(autoFireKey, 0) == 1 ? true : false;
    }
}
