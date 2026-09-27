using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxOpen : MonoBehaviour
{
    private SpriteRenderer _SpriteRenderer = null;

    public Sprite _Sprite;

    private BoxCollider2D box = null;

    private CharacterManager characterManager = null;

    private TalkManager talk = null;

    private void Awake()
    {
        _SpriteRenderer = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
        characterManager = GameManager.GetManagerClass<CharacterManager>();
        talk = GameManager.GetManagerClass<TalkManager>();
    }


    private void Open()
    {
        _SpriteRenderer.sprite = _Sprite;
        characterManager.Player.Coin += 5000; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            box.isTrigger = false;
            Open();
            Destroy(box);
        }
    }

}
