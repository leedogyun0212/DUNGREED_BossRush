using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour, IManager
{
    public GameManager gameManager { get { return GameManager.gameManager; } }

    private List<Stage1Boss> _StageMonsters = new List<Stage1Boss>();

    public List<Stage1Boss> stageMonsters { get { return _StageMonsters; } }

    public Stage1Boss Boss { get; set; }

    public bool BossDead { get; set; } = false;

    public bool BossStart { get; set; }  = false;

    public bool BossOn { get; set; } = false;


    private void Awake()
    {
        // 몬스터와 플레이어와 거리가 가까운 순서대로 정렬
        IEnumerator SortStageMonsters()
        {
            yield return new WaitUntil(()
                => GameManager.GetManagerClass<CharacterManager>().Player);

            while (true)
            {
                // 정렬
                if (_StageMonsters != null)
                {
                    _StageMonsters.Sort((A, B) =>
                    {
                        float ADistance = Vector3.Distance(
                            A.transform.position, A.characterManager.Player.transform.position);
                        float BDistance = Vector3.Distance(
                            B.transform.position, B.characterManager.Player.transform.position);

                        if (ADistance < BDistance) return -1;
                        else if (ADistance > BDistance) return 1;
                        return 0;
                    });
                }
                yield return null;
            }
        }
        StartCoroutine(SortStageMonsters());
    }

    // _StageMonster의 모든 요소를 비우는 메서드
    public void ClearStageMonsters()
    {
        _StageMonsters.Clear();
    }
}
