using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBoss : MonoBehaviour
{
    public GameObject Boss;
    public GameObject BossHP;

    private MonsterManager monster = null;

    private int OpenNum = 100;

    private void Awake()
    {
        monster = GameManager.GetManagerClass<MonsterManager>();
        Boss.SetActive(false);
        BossHP.SetActive(false);
        gameObject.SetActive(false);
    }

    private void Start()
    {
    }

    private void OnEnable()
    {
        if(monster.BossOn)
        {
            Boss.SetActive(true);
            BossHP.SetActive(true);
            monster.BossStart = true;
        }
    }

    private void Update()
    {
        UIOn();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
        }
    }

    private void UIOn()
    {
        if(OpenNum>0)
        {
            OpenNum -= 1;
            return;
        }
        monster.Boss.attackChoose.ChangeNum = true;
        Destroy(this.gameObject);

    }
}
