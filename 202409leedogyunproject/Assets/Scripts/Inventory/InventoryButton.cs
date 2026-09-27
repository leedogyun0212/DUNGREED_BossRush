using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    public GameObject Inventory;

    public GameObject GameUI;

    private TalkManager talkManager = null;

    private void Awake()
    {
        talkManager = GameManager.GetManagerClass<TalkManager>();
    }

    private void Start()
    {
        //Inventory.SetActive(false);
        GameUI.SetActive(true);
    }

    private void Update()
    {
        //if (!talkManager.talkInstance.shopNpc.ShopOpen)
        //    GameUI.SetActive(true);
    }

    public void InvenButton()
    {
        GameUI.SetActive(false);
        Inventory.SetActive(true);

    }

    public void ExitButton()
    {
        GameUI.SetActive(true);
        Inventory.SetActive(false);
        if (!talkManager.talkInstance.ShopNpcActive) return;
        talkManager.talkInstance.shopNpc.shopExitButton();
    }
}
