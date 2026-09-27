using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아이템 데이터를 정의하는 클래스
[CreateAssetMenu(fileName = "New Food", menuName = "Restaurant/Food")]
public class Food : ScriptableObject
{
    public string FoodName;           // 아이템 이름
    public Sprite FoodIcon;           // 아이템 아이콘
    public string description;        // 아이템 설명
    [Range(0.0f, 30.0f)] public float PlusAtt; 
    [Range(0.0f, 30.0f)] public float PlusDef; 
    [Range(0.0f, 30.0f)] public float PlusHP; 
}
