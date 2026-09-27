using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement
{
    // 이동 방향 벡터
    Vector2 lookdirection { get; }

    void Movement();
}
