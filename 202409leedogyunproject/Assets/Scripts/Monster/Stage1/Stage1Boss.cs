using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Boss : MonoBehaviour
{
    public CharacterManager characterManager { get; set; }

    private MonsterManager monsterManager = null;

    public St1Attack1 st1Attack1 { get; set; }

    public St1AttackChoose attackChoose { get; set; }

    public St1Attack2 st1Attack2 { get; set; }

    public St1Attack3 st1Attack3 { get; set; }

    public BossDeath bossDeath { get; set; }

    public float HP;
    public float MaxHP { get; set; }
    public float Att { get; set; }
    public float Def { get; set; }

    public int AnimChange { get; set; } = -1;

    public bool CoolTimeStart { get; set; } = true;

    private float TimeCheck = 0;

    public int AttAnim { get; set; } = 0;

    public float Test { get; set; } =  -2.5f;
    public bool TestAtt2 { get; set; } =  false;

    //private GameObject[] bossWeapons;

    private AudioSource audioSource = null;


    private void Awake()
    {
        monsterManager = GameManager.GetManagerClass<MonsterManager>();
        monsterManager.Boss = this;

        characterManager = GameManager.GetManagerClass<CharacterManager>();
        attackChoose = GetComponent<St1AttackChoose>();
        st1Attack1 = GetComponentInChildren<St1Attack1>();
        st1Attack2 = GetComponentInChildren<St1Attack2>();
        st1Attack3 = GetComponent<St1Attack3>();
        bossDeath = GetComponent<BossDeath>();

        audioSource = GetComponent<AudioSource>();

        Att = 20.0f;
        Def = 5.0f;
        HP = 200.0f;
        MaxHP = 200.0f;
    }

    private void OnEnable()
    {
        audioSource.Play();
    }

    private void Update()
    {
        BossDie();
        CoolTimeCheck();
        StartCoroutine(Change());
    }

    private IEnumerator Change()
    {
        yield return new WaitUntil(() => AnimChange < 0 && CoolTimeStart);
            
        yield return new WaitUntil(() => Time.time - TimeCheck > 3.5f);
        if (AnimChange == -1)
        {
            TimeCheck = Time.time;

            AnimChange = 0;

            CoolTimeStart = false;

            AttAnim = 0;

        }
    }

    private void CoolTimeCheck()
    {
        if (AnimChange == -1)
        {
            CoolTimeStart = true;
        }
        else if(AnimChange != -1)
            TimeCheck = Time.time;
    }

    private void BossDie()
    {
        if (HP <= 0)
        {
            HP = 0;
            AnimChange = -10;
            bossDeath.StartDeathEffect();
        }
    }
}
