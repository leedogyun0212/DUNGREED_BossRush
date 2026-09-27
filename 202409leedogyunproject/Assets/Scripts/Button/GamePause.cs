using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour
{
    private TalkManager talk = null;

    public GameObject settingsPanel; // 설정 UI 패널

    private GameObject[] allUIElements;

    public GameObject GameUI;

    private bool isPaused = false;

    private void Awake()
    {
        talk = GameManager.GetManagerClass<TalkManager>();
    }

    private void Start()
    {
        allUIElements = GameObject.FindGameObjectsWithTag("UI");
    }

    private void Update()
    {
        if (talk.Setting)
        {
            talk.Setting = false;
            TogglePause();
        }
        if(talk.SetOn)
            SettingUI();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0; // 게임 정지
            HideAllUIExceptSettings();
        }
        else
        {
            Time.timeScale = 1; // 게임 재개
            ShowAllUI();
        }
    }

    private void HideAllUIExceptSettings()
    {
        foreach (GameObject uiElement in allUIElements)
        {
            if (uiElement != settingsPanel) // 설정 패널이 아니라면 숨기기
            {
                uiElement.SetActive(false);
            }
        }

        talk.SetOn = true;
        settingsPanel.SetActive(true); // 설정 패널만 켜두기
    }

    private void ShowAllUI()
    {
        foreach (GameObject uiElement in allUIElements)
        {
            uiElement.SetActive(true); // 모든 UI 다시 보이게
        }

        talk.SetOn = false;

        settingsPanel.SetActive(false);
    }

    public void SettingUI()
    {
        if (talk.SettingUI)
        {
            //settingsPanel.SetActive(false);
            GameUI.SetActive(true);
        }
        else if (!talk.SettingUI)
        {
            Debug.Log("WASD");
            //settingsPanel.SetActive(true);
            GameUI.SetActive(false);
        }
    }
}
