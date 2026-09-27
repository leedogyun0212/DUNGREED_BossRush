using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private CharacterManager characterManager = null;

    private WeaponManager weaponManager = null;

    public PlayerBowAnim BowAnim { get; set; } = null;

    [SerializeField] private PlayerThrow _ThrowPrefab;

    [SerializeField] private Transform _ThrowShotPoint = null;

    private ObjectPool<PlayerThrow> _ThrowPool = null;

    public bool isAttacking { get; set; } = false;

    public GameObject Bowlook;

    public Vector2 lookDir = Vector2.zero;

    private AudioSource audioSource = null;

    private void Awake()
    {
        weaponManager = GameManager.GetManagerClass<WeaponManager>();
        weaponManager.playerAttack = this;

        characterManager = GameManager.GetManagerClass<CharacterManager>();
        BowAnim = GetComponentInChildren<PlayerBowAnim>();
        _ThrowPool = new ObjectPool<PlayerThrow>();
        audioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
        StartCoroutine(AttackStart());
    }

    private IEnumerator AttackStart()
    {
        while (true)
        {
            yield return new WaitUntil(() =>
            weaponManager.AttBtn&&!isAttacking);

            isAttacking = true;


            if(isAttacking)
                BowAnim.PlayAttackAnimation();
        }
    }

    public void Attack()
    {
        PlayerThrow Throw = _ThrowPool.GetRecyclableObject() ??
            _ThrowPool.RegisterRecyclableObject(Instantiate(_ThrowPrefab));

        // 화살 활성화
        if (Throw.gameObject.activeSelf)
            Throw.isActive = true;
        else
            Throw.gameObject.SetActive(Throw.isActive = true);

        Throw.SetArrow();

        // 발사 위치
        lookDir = ((Vector2)Bowlook.transform.position - (Vector2)this.transform.position).normalized;
        Throw.transform.position = _ThrowShotPoint.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Throw.transform.rotation = Quaternion.Euler(0, 0, angle-90);
        Throw.projectile.Initialize(lookDir);
        audioSource.Play();
    }
}
