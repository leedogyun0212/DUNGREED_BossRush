using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNpc : MonoBehaviour
{
    private TalkInstance talkInstance;

    public GameObject GameUI;

    public string Name;

    public string Data;

    public string PlayerGo;

    public bool Shopbool { get; set; } = false;

    private void Awake()
    {
        talkInstance = GetComponentInParent<TalkInstance>();
        talkInstance.ShopNpcActive = true;
    }

    private void Start()
    {
    }

    private void Update()
    {
        NpcTalkopen();
    }


    public void NpcTalkopen()
    {
        if (talkInstance.NpcOpen&& talkInstance.OpenChoose == 1)
        {
            Debug.Log(talkInstance.npcTalk + "ShopNpc");
            talkInstance.Talk = false;
            Shopbool = true;
            talkInstance.NpcOpen = false;
        }
    }

    public void OpenShop()
    {
        Debug.Log("inventoryUI is null: " + (talkInstance.Inventory == null));
        talkInstance.Shop.SetActive(true);
        GameUI.SetActive(false);
        talkInstance.Inventory.SetActive(true);
        Debug.Log(talkInstance.Inventory.activeSelf + "adasd");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            talkInstance.Talk = true;
            talkInstance.OpenChoose = 1;
        }
        else 
        {
            talkInstance.Talk = false;
        }
    }

    public void shopExitButton()
    {
        talkInstance.NpcOpen = false;
        talkInstance.OpenChoose = 0;
        talkInstance.Shop.SetActive(false);
    }

}
