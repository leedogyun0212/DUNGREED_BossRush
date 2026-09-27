using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneNext : MonoBehaviour
{
    // 로드할 씬 저장
    public static string NextSceneName;

    // 로딩 전에 줄 딜레이
    private float _BeginDelay = 1.0f;

    // 로딩 후에 줄 딜레이
    private float _EndDelay = 1.0f;

    private void Start()
    {
        StartCoroutine(LoadNextScene());
    }
    // 씬을 먼저 LoadingScene으로 전환시키고 로드할 다음 씬을 설정
    public static void LoadScene(string nextScene)
    {
        // 로드할 다음씬 설정
        NextSceneName = nextScene;

        // 로딩씬으로 전환
        UnityEngine.SceneManagement.SceneManager.LoadScene("LoadingScene");
    }

    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(_BeginDelay);

        // asyncoperation : 비동기식 로딩
        AsyncOperation ao = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(NextSceneName);

        // > 다음 씬이 모두 준비되어도 전화시키지 않도록 합니다.
        ao.allowSceneActivation = false;

        // 다음씬이 모두 준비될때까지 대기
        yield return new WaitUntil(() => ao.progress >= 0.9f);

        yield return new WaitForSeconds(_EndDelay);

        ao.allowSceneActivation = true;
    }
}
