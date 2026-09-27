using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkInstance : MonoBehaviour
{
    private TalkManager talkManager = null;

    public ShopNpc shopNpc { get; set; }

    public bool Talk { get; set; } = false;

    public bool NpcOpen { get; set; } = false;

    public int OpenChoose { get; set; } = 0;


    public Restaurant restaurant { get; set; }

    public RestaurantOrder restaurantOrder { get; set; }

    public RestNpc restNpc { get; set; }

    public bool ShopNpcActive = false;

    public NpcTalk npcTalk { get; set; }

    public GameObject Shop;
    public GameObject Inventory;
    public GameObject RestaurantUI;

    public GameObject GameUI;

    private void Start()
    {
        talkManager = GameManager.GetManagerClass<TalkManager>();
        talkManager.talkInstance = this;

        shopNpc = GetComponentInChildren<ShopNpc>();
        restaurant = GetComponentInChildren<Restaurant>();
        restaurantOrder = GetComponentInChildren<RestaurantOrder>();
        restNpc = GetComponentInChildren<RestNpc>();
        npcTalk = GetComponentInChildren<NpcTalk>();
        npcTalk.gameObject.SetActive(false);

        Shop.SetActive(false);
        Inventory.SetActive(false);
        RestaurantUI.SetActive(false);
    }

    private void Update()
    {
        NpcTalkOpen();
    }

    public void RestaurantOpen()
    {
        RestaurantUI.SetActive(true);
    }

    private void NpcTalkOpen()
    {
        if (shopNpc.Shopbool || restNpc.Restbool)
        {
            Debug.Log(restNpc.Restbool + "¿Â" + shopNpc.Shopbool);
            npcTalk.gameObject.SetActive(true);
            GameUI.SetActive(false);
            npcTalk.Open();
        }
        else if (npcTalk.Exit)
        {
            npcTalk.Exit = false;
            GameUI.SetActive(true);
        }
    }
}
