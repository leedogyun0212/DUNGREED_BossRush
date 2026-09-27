using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotalLoading : MonoBehaviour
{
    private Image LoadingImage = null;

    private int Count = 1;
    private float GameTime = 1f;

    private Potal Potal = null;

    private void Awake()
    {
        LoadingImage = GetComponent<Image>();
        Potal = GetComponent<Potal>();
    }

    private void Update()
    {
    }
}
