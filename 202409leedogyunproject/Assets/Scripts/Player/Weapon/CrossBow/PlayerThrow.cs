using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour, IRecyclableGameObject
{
    public ProjectileMovement projectile { get; private set; }

    private WeaponManager weapon = null;

    private CharacterManager player = null;

    private MonsterManager boss = null;

    public bool isActive { get; set; }

    // 플레이어와의 거리 검사후 오브젝트 자동 비활성화 코루틴
    private IEnumerator AutoDeactivate()
    {
        Debug.Log(weapon.playerAttack+"Throw");
        // 플레이어와의 거리가 7만큼 벌어질때까지 대기
        yield return new WaitUntil(() => (
        Vector3.Distance(weapon.playerAttack.transform.position, transform.position) >= 17.0f));

        // 오브젝트 비활성
        gameObject.SetActive(false);
    }

    private void Update()
    {
        StartCoroutine(AutoDeactivate());
    }

    private void OnEnable()
    {
    }
    private void OnDisable()
    {
        isActive = false;
    }

    public void SetArrow()
    {
        player = player ?? GameManager.GetManagerClass<CharacterManager>();
        boss = boss ?? GameManager.GetManagerClass<MonsterManager>();
        projectile = projectile ?? GetComponent<ProjectileMovement>();
        weapon = weapon ?? GameManager.GetManagerClass<WeaponManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            Debug.Log("123");
            boss.Boss.HP -= player.Player.PlayerAtt / boss.Boss.Def;
        }
    }
}
