using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    private bool GameStart = false;

    private void Update()
    {
        GameStartOn();
    }

    public void StartButton()
    {
        GameStart = true;
    }

    private void GameStartOn()
    {
        if (GameStart)
        {
            GameStart = false;
            SceneNext.LoadScene("VillageScene");
        }
    }
}
