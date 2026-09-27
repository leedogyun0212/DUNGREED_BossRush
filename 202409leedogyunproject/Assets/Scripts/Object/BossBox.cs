using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBox : MonoBehaviour
{
    private SpriteRenderer _SpriteRenderer = null;

    public Sprite _Sprite;

    private BoxCollider2D box = null;

    private CharacterManager characterManager = null;

    private TalkManager talk = null;

    public GameObject UI;

    private void Awake()
    {
        _SpriteRenderer = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
        characterManager = GameManager.GetManagerClass<CharacterManager>();
        talk = GameManager.GetManagerClass<TalkManager>();
    }

    private void Update()
    {
        OpenBox();
    }

    private void Open()
    {
        _SpriteRenderer.sprite = _Sprite;
        characterManager.Player.Coin += 5000;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&& talk.First)
        {
            talk.talkInstance.Talk = true;

        }
    }

    private void OpenBox()
    {
        if (talk.talkInstance.NpcOpen&&talk.First)
        {
            talk.First = false;
            Open();
            UI.SetActive(true);
        }
    }
}
