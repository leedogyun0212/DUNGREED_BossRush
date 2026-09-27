using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    private TalkManager talk = null;

    private void Awake()
    {
        talk = GameManager.GetManagerClass<TalkManager>();
    }
    public void ExitBtn()
    {
        talk.Setting = true;
    }

    public void Setting()
    {
        talk.SettingUI = true;
    }

}
