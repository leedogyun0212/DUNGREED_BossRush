using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstance : MonoBehaviour, IHP
{
    private CharacterManager characterManager = null;

    public MonsterManager _MonsterManager { get; protected set; }

    public PlayerMovement playerMovement { get; private set; }

    public PlayerAnim playerAnim  { get; private set; }

    public PlayerDash playerDash { get; private set; }

    public PlayerAttackCon Attdir { get; private set; }

    public PlayerDead playerDead { get; private set; }

    public float Hp { get; set; } = 150.0f;

    public float MaxHp { get; set; } = 150.0f;

    public bool Die { get; protected set; } = false;

    [SerializeField] public float PlayerAtt = 30.0f;

    [SerializeField] public float Playerdef = 2.0f;

    public bool MonDamage { get; set; } = false;

    public int Coin = 5000;

    public bool isGrounded;

    private void Awake()
    {
        GameObject.Find("PlayerImage");
        characterManager = GameManager.GetManagerClass<CharacterManager>();
        characterManager.Player = this;
        _MonsterManager = GameManager.GetManagerClass<MonsterManager>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAnim = GetComponentInChildren<PlayerAnim>();
        playerDash = GetComponent<PlayerDash>();
        Attdir = GetComponentInChildren<PlayerAttackCon>();
        playerDead = GetComponent<PlayerDead>();
    }

    private void Update()
    {
        PlayerDie();
    }

    public void PlayerDie()
    {
        if(Hp <=0)
        {
            Hp = 0;
            playerAnim._Die = true;
            playerAnim.PlayerDie();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
