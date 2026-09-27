using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

public class SwordSwing : MonoBehaviour
{
    private Animator animator;

    public bool animStart { get; set; } = false;
    public bool bossAtt { get; set; } = false;

    public GameObject PlayerDir;
    public GameObject SwingDir;

    private CharacterManager player = null;

    private MonsterManager boss = null;

    private WeaponManager weapon = null;

    private AudioSource audioSource = null;

    private void Awake()
    {
        player = GameManager.GetManagerClass<CharacterManager>();
        boss = GameManager.GetManagerClass<MonsterManager>();
        animator = GetComponent<Animator>();
        weapon = GameManager.GetManagerClass<WeaponManager>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        attackDir();
    }

    private void attackDir()
    {
        SwingDir.transform.position = PlayerDir.transform.position;
        SwingDir.transform.rotation = PlayerDir.transform.rotation;
    }

    public void Swing()
    {
        if (!animStart) // 중복 실행 방지
        {
            animStart = true;
            bossAtt = true;
            audioSource.Play();
            SetTrigger("Swing");
        }
    }

    public void Endanim()
    {
        if (animStart) // 중복 실행 방지
        {
            animStart = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss")&& bossAtt)
        {
            bossAtt = false;
            boss.Boss.HP -= player.Player.PlayerAtt / boss.Boss.Def;
        }
    }

    public void SetTrigger(string paramName)
    {
        animator.SetTrigger(paramName);
    }
}
