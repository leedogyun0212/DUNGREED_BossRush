using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class St1Attack1 : MonoBehaviour
{
    private CharacterManager Character = null;

    [SerializeField] private St1AttackPlayer PrefabHandAtt; 

    private ObjectPool<St1AttackPlayer> AttackPool = null;

    private Stage1Boss Boss = null;

    private Vector3 BossHandVec;

    public Animator animator { get; private set; }

    public float TargetTimeCheck { get; set; } = 0.0f;

    public bool AttOn { get; set; } = false;

    public int HandNum { get; set; } = 0;

    private void Awake()
    {
        Character = GameManager.GetManagerClass<CharacterManager>();
        Boss = GetComponentInParent<Stage1Boss>();
        animator = GetComponent<Animator>();
        AttackPool = new ObjectPool<St1AttackPlayer>();
    }

    private void Start()
    {
        BossHandVec = transform.position;
    }

    private void Update()
    {
        BossHandVec.y = Character.Player.transform.position.y;
        SetInt("AnimChange",Boss.AnimChange);
        if(HandNum>0)
            TargetMove();
    }


    public void MoveHand()
    {
        transform.position = Vector2.MoveTowards(transform.position, BossHandVec, 3.0f);
    }

    public void Attack()
    {
        St1AttackPlayer handAtt = AttackPool.GetRecyclableObject() ??
            AttackPool.RegisterRecyclableObject(Instantiate(PrefabHandAtt));


        if (this.gameObject.CompareTag("Right"))
            handAtt.transform.rotation = Quaternion.Euler(0, 180, 0);

        if (handAtt.gameObject.activeSelf)
            handAtt.isActive = true;
        else
            handAtt.gameObject.SetActive(handAtt.isActive = true);

        handAtt.Damage = true;

        handAtt.transform.position = transform.position;
    }

    public void AttackStart()
    {
        if (AttOn && HandNum == Boss.AnimChange)
        {
            Attack();
        }
    }

    public void AttEnd()
    {

        if (AttOn && HandNum == Boss.AnimChange)
        {
            Boss.AttAnim -= 1;
            EndAnim();
        }
    }

    private void EndAnim()
    {
        if(Boss.AttAnim>0)
        {
            Boss.attackChoose.RandomNum = Random.Range(1, 3);
            AttOn = false;
            TargetTimeCheck = Time.time;
            Boss.AnimChange = -Boss.attackChoose.RandomNum-1;
            LeftRight();
        }
        if(Boss.AttAnim == 0)
        {
            Boss.AnimChange = -1;
        }
    }

    public void TargetMove()
    {
        Debug.Log(AttOn + "sds");
        if (!AttOn && Time.time - TargetTimeCheck < 1.5f && HandNum == Boss.attackChoose.RandomNum)
        {
            MoveHand();
        }
        else if (!AttOn && Time.time - TargetTimeCheck > 1.5f && HandNum == Boss.attackChoose.RandomNum)
        {
            Boss.AnimChange = Boss.attackChoose.RandomNum;
            Debug.Log(Boss.AnimChange+ "TargetMove//else");
            AttOn = true;
        }
    }

    public void LeftRight()
    {
        if (this.gameObject.CompareTag("Right")&& Boss.AnimChange == -Boss.attackChoose.RandomNum-1 && Boss.AnimChange == -3)
        {
            TargetTimeCheck = Time.time;
            if (AttOn) { AttOn = false; }
            HandNum = Boss.attackChoose.RandomNum;
            Debug.Log(HandNum + "Right" + Boss.attackChoose.RandomNum);
            Boss.AnimChange -= 1;
        }
        else if (Boss.AnimChange == -Boss.attackChoose.RandomNum-1&&this.gameObject.CompareTag("BossWeapon")&&Boss.AnimChange == -2)
        {
            TargetTimeCheck = Time.time;
            if (AttOn) { AttOn = false; }
            HandNum = Boss.attackChoose.RandomNum;
            Debug.Log(HandNum + "BossWeapon" + Boss.attackChoose.RandomNum);
            Boss.AnimChange -= 1;
        }
    }

    public int SetInt(string paramName, int value)
    {
        animator.SetInteger(paramName, value);
        return value;
    }
}
