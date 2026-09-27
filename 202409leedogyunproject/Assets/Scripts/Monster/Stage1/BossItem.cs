using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossItem : MonoBehaviour
{

    private TalkManager talk = null;

    public InventorySystem InventorySystem;

    public Item ShopItem;

    public int Itemcoin;

    public GameObject EndActive;

    private void Awake()
    {
        talk = GameManager.GetManagerClass<TalkManager>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        ItemAdd();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")&& talk.FirstItem)
        {
            talk.talkInstance.Talk = true;
        }
    }

    public void ItemAdd()
    {
        if(talk.talkInstance.NpcOpen&& talk.FirstItem)
        {
            talk.FirstItem = false;
            InventorySystem.AddItemToFirstEmptySlot(ShopItem, InventorySystem.storedSlots);
            EndActive.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
