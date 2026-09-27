using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour, IManager
{
    public GameManager gameManager { get { return GameManager.gameManager; } }

    public TalkInstance talkInstance { get; set; }

    public bool Setting { get; set; } = false;

    //ui 설정
    public bool SetOn { get; set; } = false;

    public bool Start { get; set; } = false;

    public bool End { get; set; } = false;

    public bool SettingUI { get; set; } = false;

    // 보스 정산

    public bool First { get; set; } = true;

    public bool FirstItem { get; set; } = true;
}
