using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISettingButton : MonoBehaviour
{
    private TalkManager talk = null;

    public GameObject Exit;

    private void Awake()
    {
        talk = GameManager.GetManagerClass<TalkManager>();
    }

    private void OnEnable()
    {
        SetExitOn();
    }

    private void SetExitOn()
    {
        if (talk.SettingUI)
            Exit.SetActive(true);
        else if (!talk.SettingUI)
            Exit.SetActive(false);
    }

    public void SetExitButton()
    {
        talk.SettingUI = false;
    }

    public void SetStart()
    {
        talk.Start = true;
        talk.End = true;
    }

    public void SetEnd()
    {
        talk.End = false;
    }
}
