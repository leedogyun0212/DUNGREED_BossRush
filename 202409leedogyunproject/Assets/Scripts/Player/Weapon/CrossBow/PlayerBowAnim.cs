using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBowAnim : MonoBehaviour
{
    private WeaponManager weapon = null;

    public Animator animator { get; private set; }

    private void Awake()
    {
        weapon = GameManager.GetManagerClass<WeaponManager>();
        animator = GetComponent<Animator>();
    }

    public void PlayAttackAnimation()
    {
        SetTrigger("_Attack");
    }
    public void Attack()
    {
        weapon.playerAttack.Attack();
    }

    public void AttackEnd()
    {
        weapon.AttBtn = false;
        weapon.playerAttack.isAttacking = false;
    }

    public void SetTrigger(string paramName)
    {
        animator.SetTrigger(paramName);
    }
}
