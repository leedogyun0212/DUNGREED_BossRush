using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour, IAnimInstance
{
    public Animator animator { get; private set; }

    private float _Speed = 0.0f;

    public bool _Jumping = false;

    public bool _Hurt = false;

    public bool _Die = false;

    // 캐릭터 매니저 변수 참조
    private CharacterManager characterManager = null;

    private SpriteRenderer Player = null;

    public Sprite Die;

    public GameObject DustPrefab;
    public GameObject JumpEffectPrefab;

    public Transform DustDir;

    private GameObject currentDust;
    private GameObject currentJumpEffect;

    private void Awake()
    {
        characterManager = GameManager.GetManagerClass<CharacterManager>();
        animator = GetComponent<Animator>();
        Player = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        _Speed = characterManager.inputVector.magnitude;
        SetBool("_Die", _Die);
        SetFloat("_Speed", _Speed);
        DustEffect();
        SetBool("_Jump", _Jumping);
    }

    public void PlayerDie()
    {

        Player.sprite = Die;
    }

    public void DustEffect()
    {
        if(Mathf.Abs(characterManager.inputVector.x)>0.1f&& !DustPrefab.activeInHierarchy)
        {
            ShowDustEffect();
        }
    }

    public void ShowDustEffect()
    {
        if (!characterManager.Player.isGrounded)
        {
            ShowJumpEffect();
            return;
        }

        if (currentDust != null) return;

        currentDust = Instantiate(DustPrefab, DustDir.position, Quaternion.identity);

        Destroy(currentDust, 0.5f);
        Invoke(nameof(ResetDust), 0.5f);
    }

    public void ShowJumpEffect()
    {
        if (currentJumpEffect != null) return;

        currentJumpEffect = Instantiate(JumpEffectPrefab, characterManager.Player.transform.position, Quaternion.Euler(0,0,45));

        currentJumpEffect.transform.SetParent(characterManager.Player.transform);

        Destroy(currentJumpEffect, 0.5f);
        Invoke(nameof(Resetjump), 0.5f);
    }

    public void ResetDust()
    {
        currentDust = null;
    }

    public void Resetjump()
    {
        currentJumpEffect = null;
    }

    #region Implemented IAnimInstace

    public bool SetBool(string paramName, bool value)
    {
        animator.SetBool(paramName, value);
        return value;
    }

    public float SetFloat(string paramName, float value)
    {
        animator.SetFloat(paramName, value);
        return value;
    }

    public int SetInt(string paramName, int value)
    {
        animator.SetInteger(paramName, value);
        return value;
    }

    public void SetTrigger(string paramName)
    {
        animator.SetTrigger(paramName);
    }
    #endregion
}
