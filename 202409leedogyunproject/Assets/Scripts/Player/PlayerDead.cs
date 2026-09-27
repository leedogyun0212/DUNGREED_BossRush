using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    public GameObject DeadUI;

    public GameObject GameUI;

    private CharacterManager characterManager = null;

    private MonsterManager monster = null;

    private void Awake()
    {
        characterManager = GameManager.GetManagerClass<CharacterManager>();
        monster = GameManager.GetManagerClass<MonsterManager>();
    }

    private void Update()
    {
        Dead();
    }

    public void Dead()
    {
        if (characterManager.Player.Hp > 0&& monster.Boss.HP>0)
            DeadUI.SetActive(false);
        else if (characterManager.Player.Hp <= 0)
        {
            DeadUI.SetActive(true);
            GameUI.SetActive(false);
        }
    }
}
