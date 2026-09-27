using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordAttack : MonoBehaviour
{
    private BoxCollider2D Swing = null;

    private CharacterManager _CharacterManager = null;

    private WeaponManager weaponManager = null;

    private float attackCooldown = 0.5f;

    public bool isAttack { get; set; } = false;

    private SwordSwing _SwordSwing { get; set; }

    private void Awake()
    {
        weaponManager = GameManager.GetManagerClass<WeaponManager>();
        weaponManager.playerSwordAttack = this;

        _CharacterManager = GameManager.GetManagerClass<CharacterManager>();
        _SwordSwing = GetComponentInChildren<SwordSwing>();
    }

    private void Update()
    {
        StartCoroutine(attackSrart());
    }

    IEnumerator attackSrart()
    {
        yield return new WaitUntil(() => weaponManager.AttBtn && !isAttack);
        if (isAttack)
        {;
            yield break; // 이미 공격 중이면 종료
        }

        isAttack = true;
        yield return new WaitForSeconds(0.1f);
        //_SwordSwing.animStart = true;
        _SwordSwing.Swing();
        //Debug.Log(_SwordSwing.animStart);

        yield return new WaitForSecondsRealtime(attackCooldown);
        isAttack = false;
    }
}
