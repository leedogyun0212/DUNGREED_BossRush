using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePotal : MonoBehaviour
{
    TalkManager talk = null;

    private void Awake()
    {
        talk = GameManager.GetManagerClass<TalkManager>();
    }

    private void Update()
    {
        NextPotal();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            talk.talkInstance.Talk = true;
        }
    }

    private void NextPotal()
    {
        if(talk.talkInstance.NpcOpen)
        {
            SceneNext.LoadScene("gameScene");
        }
    }
}
