using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAfterImage : MonoBehaviour
{
    public GameObject afterImagePrefab; // 잔상 프리팹 (SpriteRenderer 포함)
    private float afterImageLifetime = 0.5f;
    private float spawnInterval = 0.1f;

    private bool isDashing = false;

    public void StartDashEffect(float duration)
    {
        if (!isDashing)
            StartCoroutine(SpawnAfterImages(duration));
    }

    IEnumerator SpawnAfterImages(float duration)
    {
        isDashing = true;
        float time = 0;

        while (time < duration)
        {
            SpawnAfterImage();
            yield return new WaitForSeconds(spawnInterval);
            time += spawnInterval;
        }

        isDashing = false;
    }

    void SpawnAfterImage()
    {
        GameObject afterImage = Instantiate(afterImagePrefab, transform.position, transform.rotation);
        SpriteRenderer sr = afterImage.GetComponent<SpriteRenderer>();
        sr.sprite = GetComponent<SpriteRenderer>().sprite; // 현재 스프라이트 복사
        Destroy(afterImage, afterImageLifetime); // 일정 시간 후 잔상 제거
    }
}
