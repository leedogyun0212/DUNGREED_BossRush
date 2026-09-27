using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRushEnd : MonoBehaviour
{
    private TalkManager talk = null;

    public GameObject EndUI;
    public GameObject GameUI;

    private void Awake()
    {
        talk = GameManager.GetManagerClass<TalkManager>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameUI.SetActive(false);
            EndUI.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
