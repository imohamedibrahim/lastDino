using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject MainCanvas;
    [SerializeField]
    private GameObject gamePlayGameObject;
    public static UIManager uIManager;
    private UIGamePlayScreenManager uIGamePlayScreenController;
    private UIMainScreenManager uIMainScreenController;
    private GameObject uIGamePlayGameObject;
    private GameObject uIMainScreenGameObject;

    public static UIManager Instance()
    {
        if (uIManager == null)
        {
            uIManager = new UIManager();
        }
        return uIManager;
    }

    public void Awake()
    {
        if (uIManager != null && uIManager != this)
        {
            uIManager = null;
        }
        else
        {
            uIManager = this;
        }
    }

    private void Start()
    {
        uIGamePlayScreenController = UIGamePlayScreenManager.Instance();
        uIMainScreenController = UIMainScreenManager.Instance();
        uIGamePlayGameObject = UtilFunctions.GetChildGameObjectWithTag(MainCanvas, TagHolder.UI_GAME_PLAY_SCREEN);
        uIMainScreenGameObject = UtilFunctions.GetChildGameObjectWithTag(MainCanvas, TagHolder.UI_MAIN_SCREEN);
        uIMainScreenController.AssignGameObject(uIMainScreenGameObject);
        Time.timeScale = 1f;
        GameStateHolder.numberOfGamesPlayed = GameStateHolder.numberOfGamesPlayed + 1;
        Initiate();
    }

    // Start is called before the first frame update
    public void Initiate()
    {
        uIGamePlayGameObject.SetActive(false);
        gamePlayGameObject.SetActive(false);
        uIMainScreenGameObject.SetActive(true);
        uIMainScreenController.Initiate();
    }

    internal void LoadGameScreen()
    {
        uIGamePlayGameObject.SetActive(true);
        uIMainScreenGameObject.SetActive(false);
        uIGamePlayScreenController.AssignGameObject(uIGamePlayGameObject);
        uIGamePlayScreenController.Initiate();
        gamePlayGameObject.SetActive(true);
    }
}
