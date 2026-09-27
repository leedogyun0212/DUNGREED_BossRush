using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFollow : MonoBehaviour
{
    private CharacterManager _CharacterManager = null;

    private WeaponManager weapon = null;

    private Vector2 aimDirection = Vector2.zero;

    private float angle = 0.0f;

    private Vector2 LookDirection = Vector2.zero;

    private void Awake()
    {
        weapon = GameManager.GetManagerClass<WeaponManager>();
        weapon.follow = this;

        _CharacterManager = GameManager.GetManagerClass<CharacterManager>();
    }

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (_CharacterManager.inputVector.magnitude > 0.1)
        {
            aimDirection = _CharacterManager.Player.transform.position;
            aimDirection.x += _CharacterManager.inputVector.x * 0.5f;
            aimDirection.y += _CharacterManager.inputVector.y * 0.5f;
            transform.position = aimDirection;
            rotateWeapon();
        }
        else
        {
            transform.position = _CharacterManager.Player.transform.position;
        }
    }

    public void rotateWeapon()
    {
        HandleWeaponRotation();
        if (_CharacterManager.inputVector.x < 0)
        {
            this.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else if (_CharacterManager.inputVector.x > 0)
        {
            this.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void HandleWeaponRotation()
    {
        LookDirection = ((Vector2)_CharacterManager.Player.Attdir.transform.position - (Vector2)_CharacterManager.Player.transform.position).normalized;
        angle = Mathf.Atan2(LookDirection.y, LookDirection.x) * Mathf.Rad2Deg;
    }
}
