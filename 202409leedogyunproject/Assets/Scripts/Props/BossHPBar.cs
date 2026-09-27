using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBar : MonoBehaviour
{
    [SerializeField] private Image HPImage = null;

    private MonsterManager boss = null;

    private void Awake()
    {
        boss = GameManager.GetManagerClass<MonsterManager>();
        HPImage = GetComponent<Image>();
    }

    private void Update()
    {
        //Debug.Log(boss.Boss.HP);
        HPImage.fillAmount = (boss.Boss.HP / boss.Boss.MaxHP);
    }
}
