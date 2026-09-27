using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRushAudio : MonoBehaviour
{
    private AudioSource audioSource = null;

    public AudioClip BossStart;

    private MonsterManager monster = null;

    private void Awake()
    {
        monster = GameManager.GetManagerClass<MonsterManager>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        ChangeAudio();
    }

    private void ChangeAudio()
    {
        if (monster.BossStart)
        {
            monster.BossStart = false;
            audioSource.clip = BossStart;
            audioSource.Play();
        }
        audioSource.playOnAwake = true;
    }
}
