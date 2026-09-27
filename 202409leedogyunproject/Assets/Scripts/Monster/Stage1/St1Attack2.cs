using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class St1Attack2 : MonoBehaviour
{
    private CharacterManager Character = null;

    [SerializeField] private St1AttackThrowball PrefabThrow;

    private ObjectPool<St1AttackThrowball> ThrowPool1 = null;

    private Stage1Boss Boss = null;

    public Animator animator { get; private set; }

    private float TimeCheck = 0;
    private float CoolTime = 0;

    private bool Attcheck = false;

    private SpriteRenderer BossSprite = null;

    public Sprite Die;

    private AudioSource audioSource = null;


    private void Awake()
    {
        Character = GameManager.GetManagerClass<CharacterManager>();
        BossSprite = GetComponent<SpriteRenderer>();
        ThrowPool1 = new ObjectPool<St1AttackThrowball>();
        Boss = GetComponentInParent<Stage1Boss>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        
        SetInt("AnimChange", Boss.AnimChange);
        if(Attcheck&& Time.time - CoolTime > 0.15)
        {
            CoolTime = Time.time;
            AttackStart(Vector2.right);
            AttackStart(Vector2.left);
            AttackStart(Vector2.up);
            AttackStart(Vector2.down);
        }
    }

    public void AttackStart(Vector2 vec)
    {
        St1AttackThrowball ThrowAtt = ThrowPool1.GetRecyclableObject() ??
            ThrowPool1.RegisterRecyclableObject(Instantiate(PrefabThrow));


        ThrowAtt.dirChange(vec);
        Boss.Test += 2.2f;

        if (ThrowAtt.gameObject.activeSelf)
            ThrowAtt.isActive = true;
        else
            ThrowAtt.gameObject.SetActive(ThrowAtt.isActive = true);

        ThrowAtt.StartBullet();

        ThrowAtt.transform.position = transform.position;
        audioSource.Play();
    }

    public void Attack()
    {
        if (!Attcheck&&Boss.TestAtt2)
        {
            Attcheck = true;
            TimeCheck = Time.time;
        }
    }

    public void AttackTest()
    {
        if (Time.time - TimeCheck > 5.0)
        {
            Boss.TestAtt2 = false;
            Attcheck = false;
            Boss.AnimChange = -1;
        }
    }

    public void DieBoss()
    {
        BossSprite.sprite = Die;
    }

    public int SetInt(string paramName, int value)
    {
        animator.SetInteger(paramName, value);
        return value;
    }
}
