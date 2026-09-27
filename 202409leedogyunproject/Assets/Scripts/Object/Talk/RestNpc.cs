using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestNpc : MonoBehaviour
{
    TalkManager talkManager = null;

    public string Name;

    public string Data;

    public string PlayerGo;

    public bool Restbool { get; set; } = false;

    private void Awake()
    {
        talkManager = GameManager.GetManagerClass<TalkManager>();
    }

    private void Update()
    {
        NpcTalkopen();
    }

    public void RestaurantOpen()
    {
        talkManager.talkInstance.RestaurantUI.SetActive(true);
    }

    public void NpcTalkopen()
    {
        if (talkManager.talkInstance.NpcOpen && talkManager.talkInstance.OpenChoose == 2)
        {
            if (Restbool) return;
            Restbool = true;
            talkManager.talkInstance.NpcOpen = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            talkManager.talkInstance.Talk = true;
            talkManager.talkInstance.OpenChoose = 2;
        }
        else
        {
            talkManager.talkInstance.Talk = false;
        }
    }
}
