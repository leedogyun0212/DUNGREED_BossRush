using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHP
{
    float Hp { get; set; }

    float MaxHp { get; set; }

    bool Die { get; }
}
