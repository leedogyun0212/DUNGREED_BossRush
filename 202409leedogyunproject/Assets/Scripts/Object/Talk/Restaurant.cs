using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Restaurant : MonoBehaviour
{
    private CharacterManager characterManager = null;

    private TalkManager talkManager = null;

    public Food food;

    public int foodcoin;

    public Image FoodImage;

    public Text Textfood;
    public Text TextCoin;
    public Text Textname;

    public bool OrderEnd { get; set; } = false;

    private void Awake()
    {
        characterManager = GameManager.GetManagerClass<CharacterManager>();
        talkManager = GameManager.GetManagerClass<TalkManager>();
    }

    private void Start()
    {
        DataUpdate();
        FoodImage.gameObject.SetActive(false);
    }

    private void Update()
    {
    }

    public void PriceButton()
    {
        if (characterManager.Player.Coin >= foodcoin && !OrderEnd)
        {
            characterManager.Player.PlayerAtt += food.PlusAtt;
            characterManager.Player.Playerdef += food.PlusDef;
            characterManager.Player.Hp += food.PlusHP;
            characterManager.Player.MaxHp += food.PlusHP;
            characterManager.Player.Coin -= foodcoin;
            Debug.Log("플레이어 코인 :" + characterManager.Player.Coin);
            Thank();
        }
        else if (characterManager.Player.Coin < foodcoin && !OrderEnd)
        {
            Debug.Log("돈이 부족합니다.");
        }
        else if (OrderEnd)
            talkManager.talkInstance.restaurantOrder.OrderButton = false;
    }


    public void ChangeImageBtn()
    {
        Debug.Log(talkManager.talkInstance.restaurantOrder);
        talkManager.talkInstance.restaurantOrder.ChangeImage();
    }

    public void Thank()
    {
        TextFalse();
        FoodImage.gameObject.SetActive(true);
        FoodImage.color = new Color(255, 255, 255, 255);
        OrderEnd = true;
        talkManager.talkInstance.restaurantOrder.OrderButton = false;
    }

    public void ExitButton()
    {
        talkManager.talkInstance.NpcOpen = false;
        talkManager.talkInstance.OpenChoose = 0;
        talkManager.talkInstance.GameUI.SetActive(true);
        talkManager.talkInstance.RestaurantUI.SetActive(false);
        
    }

    private void DataUpdate()
    {
        Textname.text = (food.name + "");
        Textfood.text = ("공격력 + "+food.PlusAtt + "\n"+ "방어력 + " + food.PlusDef + "\n"+ "체력 + " + food.PlusHP);
        TextCoin.text = (foodcoin + "");
    }

    private void TextFalse()
    {
        Textfood.gameObject.SetActive(false);
        TextCoin.gameObject.SetActive(false);
        Textname.gameObject.SetActive(false);
    }
}
