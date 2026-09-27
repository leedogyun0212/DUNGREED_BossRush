using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour, IManager
{
    public GameManager gameManager { get { return GameManager.gameManager; } }

    public bool AttBtn = false;

    public PlayerSwordAttack playerSwordAttack { get; set;}

    public PlayerAttack playerAttack { get; set;}

    public WeaponFollow follow { get; set;}

    public bool Swith { get; set; } = false;
}
