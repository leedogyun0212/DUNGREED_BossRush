using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    private CharacterManager _CharacterManager = null;

    public InventorySystem InventorySystem;

    public Item ShopItem;

    public int Itemcoin;

    public Text Textcoin;

    public Image ItemImage;

    public Text ItemName;

    private void Awake()
    {
        _CharacterManager = GameManager.GetManagerClass<CharacterManager>();
    }

    private void Start()
    {
        ImageUpdate();
    }

    public void PriceButton()
    {
        if (_CharacterManager.Player.Coin>=Itemcoin)
        {
            InventorySystem.AddItemToFirstEmptySlot(ShopItem,InventorySystem.storedSlots);
            _CharacterManager.Player.Coin -= Itemcoin;
        }
        else if(_CharacterManager.Player.Coin < Itemcoin)
        {
            Debug.Log("돈이 부족합니다.");
        }
    }

    private void ImageUpdate()
    {
        ItemImage.sprite = ShopItem.itemIcon;
        Textcoin.text = (Itemcoin+"");
        ItemName.text = (ShopItem.name + "");
    }
}
