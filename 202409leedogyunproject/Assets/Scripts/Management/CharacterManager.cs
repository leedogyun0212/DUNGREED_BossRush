using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour, IManager
{
    public GameManager gameManager { get { return GameManager.gameManager; } }

    // ¡∂¿ÃΩ∫∆Ω πÊ«‚∫§≈Õ
    public Vector2 inputVector { get; set; }

    public PlayerInstance Player { get; set; }
}
