using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아이템 데이터를 정의하는 클래스
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;           // 아이템 이름
    public Sprite itemIcon;           // 아이템 아이콘
    public string description;        // 아이템 설명
    public GameObject Weapon;
}
