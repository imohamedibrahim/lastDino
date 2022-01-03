using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIMainScreenManager : MonoBehaviour
{

    public static UIMainScreenManager uIMainScreenController;
    private SoundManager soundManager;
    private UIManager uIManager;
    private GameObject gameObject;
    private GameObject startButtonGameobject;
    private GameObject audioButtonGameobject;
    
    public static UIMainScreenManager Instance()
    {
        if (uIMainScreenController == null)
        {
            uIMainScreenController = new UIMainScreenManager();
        }
        return uIMainScreenController;
    }



    public void Awake()
    {
        if (uIMainScreenController != null && uIMainScreenController != this)
        {
            uIMainScreenController = null;
        }
        else
        {
            uIMainScreenController = this;
        }
    }

    void Start()
    {
        uIManager = UIManager.Instance();
        soundManager = SoundManager.Instance();
    }

    internal void AssignGameObject(GameObject tmp)
    {
        gameObject = tmp;
    }

    void Update()
    {
        
    }

    internal void Initiate()
    {
        startButtonGameobject = UtilFunctions.GetChildGameObjectWithTag(gameObject, TagHolder.START_BUTTON);
        startButtonGameobject.GetComponent<Button>().onClick.AddListener(StartClicked);
        audioButtonGameobject = UtilFunctions.GetChildGameObjectWithTag(gameObject, TagHolder.AUDIO_BUTTON);
        SetAudioState();
        audioButtonGameobject.GetComponent<Button>().onClick.AddListener(AudioClicked);
        if (GameStateHolder.restartClicked)
        {
            StartClicked();
            GameStateHolder.restartClicked = false;
        }
    }

    internal void SetAudioState()
    {
        UtilFunctions.GetChildGameObjectWithTag(audioButtonGameobject,TagHolder.NO_AUDIO_BUTTON).SetActive(!GameStateHolder.audioEnable);
        soundManager.SetMuteState(!GameStateHolder.audioEnable);
    }

    private void AudioClicked()
    {
        GameStateHolder.audioEnable = !GameStateHolder.audioEnable;
        Debug.Log(GameStateHolder.audioEnable);
        SetAudioState();
        soundManager.PlayAudio(SoundManager.AUDIO_LIST.BUTTON_AUDIO);
    }

    private void StartClicked()
    {
        soundManager.PlayAudio(SoundManager.AUDIO_LIST.BUTTON_AUDIO);
        uIManager.LoadGameScreen();
    }
}

