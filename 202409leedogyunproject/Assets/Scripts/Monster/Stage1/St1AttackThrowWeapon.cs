using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.EventSystems;

public class St1AttackThrowWeapon : MonoBehaviour, IRecyclableGameObject
{
    private CharacterManager characterManager = null;

    private MonsterManager monsterManager = null;

    public float TimeCheck = 0.0f;

    public bool isActive { get; set; }

    private Vector2 PlayerDir;
    private Vector2 PlayerDirtest;

    public int WeaponNum = 0;

    private bool hasHitWall = false;

    public bool On = false;

    public GameObject hitEffectPrefab;
    private GameObject hitEffect;


    private void Awake()
    {
        characterManager = GameManager.GetManagerClass<CharacterManager>();
        monsterManager = GameManager.GetManagerClass<MonsterManager>();
    }

    private void Update()
    {
        StartCoroutine(NumCon());
        if(On)
            MovePlayer();
        else if(!On)
            TimeCheck = Time.time;
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        isActive = false;
    }

    private void MovePlayer()
    {
        if (Time.time - TimeCheck < 2.0f)
        {
            PlayerPos();
        }
        else if (Time.time - TimeCheck > 2.0f&&WeaponNum !=0)
        {
            transform.position += (Vector3)PlayerDirtest * 25.0f*Time.deltaTime;
            //transform.position = Vector2.MoveTowards(transform.position, PlayerDir, 2.0f);
        }
    }

    private void PlayerPos()
    {
        if (hasHitWall) return;

        PlayerDir = characterManager.Player.transform.position;
        PlayerDirtest = (PlayerDir - (Vector2)transform.position).normalized;

        float angle = Mathf.Atan2(PlayerDirtest.y, PlayerDirtest.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle-90);
    }


    public IEnumerator NumCon()
    {
        yield return new WaitUntil(() => WeaponNum == monsterManager.Boss.st1Attack3.AttackOn);
        On = true;
    }

    private void WeaponHit()
    {
        //if (hitEffect != null) return;

        hitEffect = Instantiate(hitEffectPrefab, characterManager.Player.transform.position, Quaternion.Euler(0,0,0));

        hitEffect.transform.localRotation = Quaternion.Euler(0, 0, transform.position.z + 45);

        Destroy(hitEffect, 0.5f);
        Invoke(nameof(Resethit), 0.5f);
    }

    private void Resethit()
    {
        hitEffect = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            characterManager.Player.Hp -= (monsterManager.Boss.Att*0.8f) / characterManager.Player.Playerdef;
            WeaponHit();
        }
        if (collision.CompareTag("Ground"))
        {
            if (WeaponNum == 5)
                monsterManager.Boss.AnimChange = -1;
            transform.position += (Vector3)PlayerDirtest * 1.5f;
            WeaponNum = 0;
            On = false;
            hasHitWall = true;

            monsterManager.Boss.st1Attack3.AttackOn += 1;
        }
    }
}
