using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateHolder : MonoBehaviour
{
    private static int currentCoinCount = 0;
    private static List<string> boolState = new List<string>{"False","True"};

    public static string rated
    {
        get
        {
            if (PlayerPrefs.HasKey("rated"))
                return PlayerPrefs.GetString("rated");
            return "False";
        }
        set
        {
            PlayerPrefs.SetString("rated", value);
        }
    }

    public static int numberOfGamesPlayed
    {
        get
        {
            if (PlayerPrefs.HasKey("numberOfGamesPlayed"))
                return PlayerPrefs.GetInt("numberOfGamesPlayed");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("numberOfGamesPlayed", value);
        }
    }

    public static string vibrate
    {
        get
        {
            string _object = "vibrate";
            if (PlayerPrefs.HasKey(_object))
            {
                return PlayerPrefs.GetString(_object);
            }
            return "True";
        }
        set
        {
            PlayerPrefs.SetString("vibrate", value);
        }
    }

    public static int currentGamePoint
    {
        get
        {
            return currentCoinCount;
        }
        set
        {
            currentCoinCount = value;
        }
    }

    public static int highestGamePoint
    {
        get
        {
            if (PlayerPrefs.HasKey("HighestCoinGained"))
                return PlayerPrefs.GetInt("HighestCoinGained");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("HighestCoinGained", value);
        }
    }

    public static bool restartClicked
    {
        get
        {
            if (PlayerPrefs.HasKey("RestartClicked"))
                return PlayerPrefs.GetString("RestartClicked").Equals(boolState[0]) ? false : true;
            return false;
        }
        set
        {
            PlayerPrefs.SetString("RestartClicked", value.ToString());
        }
    }

    public static bool audioEnable
    {
        get
        {
            if (PlayerPrefs.HasKey("AudioEnabled"))
            {
                Debug.Log(PlayerPrefs.GetString("AudioEnabled"));
                return PlayerPrefs.GetString("AudioEnabled").Equals(boolState[0]) ? false : true;
            }
            return true;
        }
        set
        {
            PlayerPrefs.SetString("AudioEnabled", value.ToString());
        }
    }
}
