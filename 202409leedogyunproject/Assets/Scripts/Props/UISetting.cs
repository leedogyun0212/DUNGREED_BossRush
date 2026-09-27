using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISetting : MonoBehaviour
{
    private TalkManager talk = null;

    public GameObject gameUI; // GameUI 전체

    private bool isEditing = false;

    private void Awake()
    {
        talk = GameManager.GetManagerClass<TalkManager>();
    }

    private void Update()
    {
        if(talk.Start)
        {
            ToggleEditMode();
        }
    }

    public void SettingButton()
    {
        talk.SettingUI = true;
        ToggleEditMode();
    }

    public void ToggleEditMode()
    {
        isEditing = !isEditing;
        
        foreach (Transform child in gameUI.transform)
        {
            var dragHandler = child.GetComponent<UIDragHandler>();

            if (dragHandler == null)
            {
                dragHandler = child.gameObject.AddComponent<UIDragHandler>(); // 처음 실행 시 한 번만 추가
            }

            dragHandler.enabled = talk.End;
            if (!talk.End)
                talk.Start = false;
            Debug.Log(dragHandler.enabled+ "enabled");
        }
    }
}
