using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class St1AttackPlayer : MonoBehaviour, IRecyclableGameObject
{
    private CharacterManager characterManager = null;

    private MonsterManager monsterManager = null;

    private Stage1Boss stage1Boss = null;

    public bool isActive { get; set; }

    public bool Damage = false;

    private void Awake()
    {
        characterManager = GameManager.GetManagerClass<CharacterManager>();
        monsterManager = GameManager.GetManagerClass<MonsterManager>();
    }

    private IEnumerator AutoDeactivate()
    {
        yield return new WaitUntil(() => monsterManager.Boss);
        //.Log("UntilPass");
        yield return new WaitForSecondsRealtime(01.0f);
        //Debug.Log("TimePass");
        gameObject.SetActive(false);
    }

    private void Update()
    {
        StartCoroutine(AutoDeactivate());
    }

    private void OnEnable()
    {
        stage1Boss = stage1Boss ?? GameManager.GetManagerClass<MonsterManager>().Boss;
        
    }
    private void OnDisable()
    {
        isActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(characterManager.Player.Hp + "//" + monsterManager.Boss.Att);
        if (collision.CompareTag("Player")&&Damage)
        {
            Damage = false;
            characterManager.Player.Hp -= monsterManager.Boss.Att / characterManager.Player.Playerdef;
        }
    }
}
