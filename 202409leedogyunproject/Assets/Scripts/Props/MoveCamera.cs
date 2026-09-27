using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private CharacterManager _CharacterManager = null;

    private Vector3 CamVec = Vector3.zero;

    private float CamSpeed = 3.0f;

    public Vector2 minBounds;  // 카메라 최소 이동 제한
    public Vector2 maxBounds;  // 카메라 최대 이동 제한

    public CapsuleCollider2D WallChange;

    private bool NoMoving = false;

    private void Awake()
    {
        _CharacterManager = GameManager.GetManagerClass<CharacterManager>();
    }

    private void Update()
    {
        if(transform.position.x>40)
            Stop();
    }

    private void Move()
    {
        transform.position = Vector3.Lerp(transform.position,CamVec , CamSpeed * Time.deltaTime);
        NoMoving = false;
    }

    private void MoveCamSet()
    {
        CamVec = _CharacterManager.Player.transform.position;
        CamVec.x = Mathf.Clamp(CamVec.x, minBounds.x, maxBounds.x);
        CamVec.y = Mathf.Clamp(CamVec.y, minBounds.y, maxBounds.y);

        CamVec.z = transform.position.z;
        CamVec.z = transform.position.z;
    }

    private void Stop()
    {
        MoveCamSet();

        NoMoving = false;

        bool isAtMinX = Mathf.Abs(transform.position.x - minBounds.x) < 0.1f;
        bool isAtMaxX = Mathf.Abs(transform.position.x - maxBounds.x) < 0.1f;
        bool isAtMinY = Mathf.Abs(transform.position.y - minBounds.y) < 0.1f;
        bool isAtMaxY = Mathf.Abs(transform.position.y - maxBounds.y) < 0.1f;

        bool moveX = !(isAtMinX && CamVec.x < transform.position.x) && !(isAtMaxX && CamVec.x > transform.position.x);
        bool moveY = !(isAtMinY && CamVec.y < transform.position.y) && !(isAtMaxY && CamVec.y > transform.position.y);

        if (moveX || moveY)
        {
            Move();
        }

        if (!NoMoving)
            Move();
    }

    private void PosX()
    {
        //  x 축 제한 확인
        if (Mathf.Abs(transform.position.x - minBounds.x) < 0.1f)
        {
            if (CamVec.x - transform.position.x > 0) Move();
            else NoMoving = true;
        }
        if (Mathf.Abs(transform.position.x - maxBounds.x) < 0.1f)
        {
            if (CamVec.x - transform.position.x < 0) Move();
            else NoMoving = true;
        }
    }

    private void PosY()
    {
        //  y 축 제한 확인
        if (Mathf.Abs(transform.position.y - minBounds.y) < 0.1f)
        {
            if (_CharacterManager.Player.transform.position.y - transform.position.y > 0) Move();
            else NoMoving = true;
        }
        if (Mathf.Abs(transform.position.y - maxBounds.y) < 0.1f)
        {
            if (_CharacterManager.Player.transform.position.y - transform.position.y < 0) Move();
            else NoMoving = true;
        }
    }


    public void SetBounds(Vector2 min, Vector2 max)
    {
        minBounds = min;
        maxBounds = max;
    }
}
