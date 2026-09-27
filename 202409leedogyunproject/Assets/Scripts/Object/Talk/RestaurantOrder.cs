using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestaurantOrder : MonoBehaviour
{
    private CharacterManager characterManager = null;

    private TalkManager talkManager = null;

    private Image OrderImage = null;

    public bool OrderButton { get; set; } = false;

    private void Awake()
    {
        OrderImage = GetComponent<Image>();
        characterManager = GameManager.GetManagerClass<CharacterManager>();
        talkManager = GameManager.GetManagerClass<TalkManager>();
    }

    private void Update()
    {
        ImageColor();
    }

    public void FoodOrderButton()
    {
        if (OrderButton)
        {
            talkManager.talkInstance.restaurant.PriceButton();
            OrderButton = false;
        }
    }

    public void ChangeImage()
    {
        OrderImage.sprite = talkManager.talkInstance.restaurant.food.FoodIcon;
        OrderButton = true;
    }

    private void ImageColor()
    {
        if(OrderButton)
            OrderImage.color = new Color(255, 255, 255, 255);
        else if (!OrderButton)
            OrderImage.color = new Color(255, 255, 255, 0);
    }
}
