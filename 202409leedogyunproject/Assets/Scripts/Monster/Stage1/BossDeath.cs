using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossDeath : MonoBehaviour
{
    private MonsterManager monster = null;

    public SpriteRenderer bossSprite; // 보스 스프라이트 렌더러
    public Sprite deadSprite; // 사망 후 보일 스프라이트

    private Collider2D bossCollider; // 보스 콜라이더
    public Animator bossAnimator; // 보스 애니메이터

    private Rigidbody2D rigidbody = null;

    private bool isDead = false;

    public GameObject BossBox;

    private void Awake()
    {
        monster = GameManager.GetManagerClass<MonsterManager>();
        rigidbody = GetComponent<Rigidbody2D>();
        bossCollider = GetComponent<Collider2D>();
    }

    public void StartDeathEffect()
    {
        StartCoroutine(DeathSequence());
    }

    IEnumerator DeathSequence()
    {
        yield return new WaitUntil(() => !isDead);
        isDead = true;

        //2깜빡이는 효과 (밝아졌다가 원래대로)
        for (int i = 0; i < 10; i++)
        {
            bossSprite.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            bossSprite.color = Color.black;
            yield return new WaitForSeconds(0.1f);
        }

        bossSprite.color = Color.white;

        //3모든 기능 정지
        if (bossAnimator != null) bossAnimator.enabled = false; // 애니메이션 정지
        // (bossCollider != null) bossCollider.isTrigger = false;// 충돌 비활성화

        DestroyObjects();

        rigidbody.bodyType = RigidbodyType2D.Dynamic;

        //4사망한 모습으로 스프라이트 변경
        bossSprite.sprite = deadSprite;

        BossBox.SetActive(true);
        monster.BossDead = true;
    }

    public void DestroyObjects()
    {
        // "BossWeapon" 태그와 "Right" 태그를 가진 오브젝트 찾기
        GameObject[] bossWeapons = GameObject.FindGameObjectsWithTag("BossWeapon");
        GameObject[] rightObjects = GameObject.FindGameObjectsWithTag("Right");

        // 두 배열을 합치기
        GameObject[] allTargets = bossWeapons.Concat(rightObjects).ToArray();

        // 모든 오브젝트 삭제
        foreach (GameObject obj in allTargets)
        {
            Destroy(obj);
        }
    }

}
