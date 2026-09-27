using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgChange : MonoBehaviour
{
    public SpriteRenderer _SpriteRenderer = null;

    public Sprite _Sprite;

    private BoxCollider2D box = null;

    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
    }


    private void Open()
    {
        _SpriteRenderer.sprite = _Sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            box.isTrigger = false;
            Open();
            Destroy(box);
        }
    }
}
