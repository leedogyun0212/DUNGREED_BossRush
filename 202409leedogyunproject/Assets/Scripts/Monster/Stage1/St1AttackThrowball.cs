using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class St1AttackThrowball : MonoBehaviour, IRecyclableGameObject
{
    private CharacterManager characterManager = null;

    private MonsterManager monsterManager = null;

    private Stage1Boss stage1Boss = null;

    public bool isActive { get; set; }

    public ProjectileMovement projectile { get; private set; }

    private Vector2 dir = Vector2.zero;

    private float Speed = 4.0f;

    private bool PlayerHit = false;

    public Animator animator { get; set; }

    private void Awake()
    {
        characterManager = GameManager.GetManagerClass<CharacterManager>();
        monsterManager = GameManager.GetManagerClass<MonsterManager>();
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, monsterManager.Boss.Test);
        if (PlayerHit) return;
        transform.Translate(dir * Speed * Time.deltaTime, Space.Self);
    }

    private void OnEnable()
    {
        projectile = projectile ?? GetComponent<ProjectileMovement>();
        stage1Boss = stage1Boss ?? GameManager.GetManagerClass<MonsterManager>().Boss;

        transform.rotation = Quaternion.Euler(0, 0, monsterManager.Boss.Test);
        Speed = 8.0f;
    }

    public void StartBullet()
    {
        projectile = projectile ?? GetComponent<ProjectileMovement>();
        stage1Boss = stage1Boss ?? GameManager.GetManagerClass<MonsterManager>().Boss;
        animator = GetComponent<Animator>();

        transform.rotation = Quaternion.Euler(0, 0, monsterManager.Boss.Test);
        Speed = 8.0f;
    }

    private void OnDisable()
    {
        isActive = false;
    }

    public void dirChange(Vector2 dirs)
    {
        dir = dirs;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            characterManager.Player.Hp -= ((monsterManager.Boss.Att*0.5f) / characterManager.Player.Playerdef);
            SetTrigger("Hit");
            PlayerHit = true;
            transform.position = characterManager.Player.transform.position;
        }
        if(collision.CompareTag("Ground"))
        {
            gameObject.SetActive(false);
        }
    }

    public void BallDead()
    {
        PlayerHit = false;
        gameObject.SetActive(false);
    }

    public void SetTrigger(string paramName)
    {
        animator.SetTrigger(paramName);
    }

}
