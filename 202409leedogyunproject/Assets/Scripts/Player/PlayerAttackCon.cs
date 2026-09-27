using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCon : MonoBehaviour
{
    private CharacterManager _CharacterManager = null;

    private PlayerInstance Player = null;

    private Vector2 aimDirection = Vector2.zero;

    private float PlusPos = 1.5f;

    private void Awake()
    {
        _CharacterManager = GameManager.GetManagerClass<CharacterManager>();
        Player = GetComponentInParent<PlayerInstance>();
    }

    private void Update()
    {
        Controller();
    }

    private void Controller()
    {
        if (_CharacterManager.inputVector.magnitude > 0.1)
        {
            aimDirection = _CharacterManager.Player.transform.position;
            aimDirection.x += _CharacterManager.inputVector.x * 1.5f;
            aimDirection.y += _CharacterManager.inputVector.y * 1.5f;
            transform.position = aimDirection;
            RotatePlayer();
        }
        else
        {
            return;
        }
    }

    private void RotatePlayer()
    {
        float angle = Mathf.Atan2(_CharacterManager.inputVector.y, _CharacterManager.inputVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

}
