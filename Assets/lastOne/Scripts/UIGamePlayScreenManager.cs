using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class UIGamePlayScreenManager : MonoBehaviour
{
    public static UIGamePlayScreenManager uIGamePlayScreenController;
    private SoundManager soundManager;
    private UIManager uIManager;
    internal GameObject gameObject;
    private GameObject gamePlayGameObject;
    private GameObject pauseButtonGameObject;
    private GameObject gamePlayObjects;
    private GameObject gameOverGameObject;
    private GameObject textField;
    private bool isInitialized;
    private bool paused;
    private TextMeshProUGUI highScoreText;
    private TextMeshProUGUI currentScoreText;
    private GameObject rateImage;

    public static UIGamePlayScreenManager Instance()
    {
        if (uIGamePlayScreenController == null)
        {
            uIGamePlayScreenController = new UIGamePlayScreenManager();
        }
        return uIGamePlayScreenController;
    }

    public void Awake()
    {
        if (uIGamePlayScreenController != null && uIGamePlayScreenController != this)
        {
            uIGamePlayScreenController = null;
        }
        else
        {
            uIGamePlayScreenController = this;
        }
    }

    internal void SetText(string v)
    {
        textField.GetComponent<TextMeshProUGUI>().text = v;
    }

    internal void Initiate()
    {
        paused = false;
        uIManager = UIManager.Instance();
        soundManager = SoundManager.Instance();
        isInitialized = true;
        gamePlayGameObject = UtilFunctions.GetChildGameObjectWithTag(gameObject, TagHolder.GAME_PLAY_IMAGE);
        pauseButtonGameObject = UtilFunctions.GetChildGameObjectWithTag(gamePlayGameObject, TagHolder.PAUSE_BUTTON);
        pauseButtonGameObject.GetComponent<Button>().onClick.AddListener(OnPauseClicked);
        textField = UtilFunctions.GetGameObjectWithTagRecursive(gamePlayGameObject, TagHolder.TEXT_FIELD);
        gameOverGameObject = UtilFunctions.GetChildGameObjectWithTag(gameObject, TagHolder.GAME_OVER_IMAGE);
        rateImage = UtilFunctions.GetChildGameObjectWithTag(gameOverGameObject,TagHolder.RATE_IT_IMAGE);
        gameOverGameObject.SetActive(false);
        gamePlayGameObject.SetActive(true);
        if (GameStateHolder.numberOfGamesPlayed % 2 == 0 && GameStateHolder.rated == "False")
        {
            rateImage.SetActive(true);
        }
        else
        {
            rateImage.SetActive(false);
        }
        UtilFunctions.GetChildGameObjectWithTag(gameOverGameObject, TagHolder.RETRY_BUTTON).GetComponent<Button>().onClick.AddListener(ReloadGame);
        UtilFunctions.GetChildGameObjectWithTag(gameOverGameObject, TagHolder.HOME_BUTTON).GetComponent<Button>().onClick.AddListener(GoHome);
        highScoreText = UtilFunctions.GetChildGameObjectWithTag(gameOverGameObject, TagHolder.HIGH_SCORE_TEXT).GetComponent<TextMeshProUGUI>();
        currentScoreText = UtilFunctions.GetChildGameObjectWithTag(gameOverGameObject, TagHolder.CURRENT_SCORE_TEXT).GetComponent<TextMeshProUGUI>();
        UtilFunctions.GetChildGameObjectWithTag(rateImage,TagHolder.RATE_IT_BUTTON).GetComponent<Button>().onClick.AddListener(rateIt);
        UtilFunctions.GetChildGameObjectWithTag(rateImage,TagHolder.RATE_LATER_BUTTON).GetComponent<Button>().onClick.AddListener(closeRateIt);
    }

    private void GoHome()
    {
        soundManager.PlayAudio(SoundManager.AUDIO_LIST.BUTTON_AUDIO);
        SceneManager.LoadScene(0);
    }

    private void ReloadGame()
    {
        soundManager.PlayAudio(SoundManager.AUDIO_LIST.BUTTON_AUDIO);
        GameStateHolder.restartClicked = true;
        SceneManager.LoadScene(0);
    }

    internal void AssignGameObject(GameObject tmp)
    {
        gameObject = tmp;
    }

    void closeRateIt()
    {
        rateImage.SetActive(false);
    }

    void rateIt()
    {
        Application.OpenURL("http://play.google.com/store/apps/details?id=" + Application.identifier);
        GameStateHolder.rated = "done";
    }

    private void OnPauseClicked()
    {
        paused = !paused;
        if (paused) Time.timeScale = 0; else Time.timeScale = 1f;
    }

    internal void GameOver(float tmp)
    {
        if (tmp > GameStateHolder.highestGamePoint)
        {
            GameStateHolder.highestGamePoint = Convert.ToInt32(tmp);
        }
        gamePlayGameObject.SetActive(false);
        gameOverGameObject.SetActive(true);
        highScoreText.text = GameStateHolder.highestGamePoint.ToString() + " SECS";
        currentScoreText.text = tmp.ToString()+ " SECS :(" ;
        
    }
}
