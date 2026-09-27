using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private CharacterManager characterManager = null;

    private PlatformEffector2D effector;
    public float waitTime = 0.5f; // 발판 통과 후 다시 활성화될 시간

    private void Awake()
    {
        characterManager = GameManager.GetManagerClass<CharacterManager>();
        effector = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        if (characterManager.inputVector.y>=0.7f || characterManager.Player.playerMovement.JumpButton)
        {
            StartCoroutine(DisablePlatform());
        }
    }

    private IEnumerator DisablePlatform()
    {
        effector.rotationalOffset = 180f; // 발판을 아래쪽으로 통과할 수 있도록 설정
        yield return new WaitForSeconds(waitTime);
        effector.rotationalOffset = 0f;   // 원래 상태로 복구
    }
}

