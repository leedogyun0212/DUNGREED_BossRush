using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class St1Attack3 : MonoBehaviour
{
    private Stage1Boss Boss = null;

    [SerializeField] private St1AttackThrowWeapon PrefabWeapon;

    private ObjectPool<St1AttackThrowWeapon> ThrowWeapon = null;

    public int Attnum = 1;

    public int AttackOn = 1;

    private float StartPos = -4.0f;

    public int AttackContest { get; set; } = 5;

    private void Awake()
    {
        ThrowWeapon = new ObjectPool<St1AttackThrowWeapon>();
        Boss = GetComponent<Stage1Boss>();
    }

    private void Update()
    {
        if (Boss.AnimChange == 4 && AttackContest > 0)
        {
            Attack();
        }
    }

    public void Attack()
    {
        St1AttackThrowWeapon WeaponAtt = ThrowWeapon.GetRecyclableObject() ??
            ThrowWeapon.RegisterRecyclableObject(Instantiate(PrefabWeapon));

        AttackContest -= 1;

        if (WeaponAtt.WeaponNum == 0&&Attnum<6)
        {
            WeaponAtt.WeaponNum = Attnum;
            Attnum++;
        }

        if (WeaponAtt.gameObject.activeSelf)
            WeaponAtt.isActive = true;
        else
            WeaponAtt.gameObject.SetActive(WeaponAtt.isActive = true);

        WeaponAtt.transform.position = new Vector2(transform.position.x+ StartPos,transform.position.y);

        StartPos += 2.0f;
        if (AttackContest == 0) StartPos = -4.0f;
    }
}
