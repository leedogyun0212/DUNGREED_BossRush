using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBossUI : MonoBehaviour
{
    public GameObject Boss;

    private MonsterManager monster = null;


    private void Start()
    {
        monster = GameManager.GetManagerClass<MonsterManager>();
        //Boss.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            monster.BossOn = true;
            Boss.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
