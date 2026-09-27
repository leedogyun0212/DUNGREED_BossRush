using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public Transform equippedWeaponsParent1; // 무기 슬롯 부모
    public Transform equippedWeaponsParent2; // 무기 슬롯 부모
    public Transform equippedAccessoriesParent; // 액세서리 슬롯 부모
    public Transform storedItemsParent; // 보관 아이템 슬롯 부모
    public GameObject slotPrefab; // 슬롯 프리팹

    public List<InventorySlot> weaponSlots { get; set; } = new List<InventorySlot>();
    public List<InventorySlot> weaponSlots2 { get; set; } = new List<InventorySlot>();
    public List<InventorySlot> accessorySlots { get; set; } = new List<InventorySlot>();
    public List<InventorySlot> storedSlots { get; set; } = new List<InventorySlot>();

    public Item testItem1;
    public Item testItem2;

    private void Awake()
    {
        // 무기 슬롯 초기화
        InitializeSlots(equippedWeaponsParent1, 2, weaponSlots, slotPrefab);
        InitializeSlots(equippedWeaponsParent2, 2, weaponSlots2, slotPrefab);

        // 액세서리 슬롯 초기화
        InitializeSlots(equippedAccessoriesParent, 4, accessorySlots, slotPrefab);

        // 보관 슬롯 초기화
        InitializeSlots(storedItemsParent, 15, storedSlots, slotPrefab);

        weaponSlots[0].AddItem(testItem1);
        weaponSlots2[0].AddItem(testItem2);
    }

    public void InitializeSlots(Transform parent, int count, List<InventorySlot> slotList, GameObject slotPrefab)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject newSlot = Instantiate(slotPrefab, parent);
            InventorySlot slotComponent = newSlot.GetComponent<InventorySlot>();
            slotList.Add(slotComponent);
        }
    }

    public void AddItemToStored(Item item)
    {
        foreach (var slot in storedSlots)
        {
            if (slot.IsEmpty())
            {
                slot.AddItem(item);
                return;
            }
        }
        Debug.Log("Inventory is full!");
    }

    public void AddItemToFirstEmptySlot(Item item, List<InventorySlot> slotList)
    {
        foreach (InventorySlot slot in slotList)
        {
            if (slot.currentItem == null) // 빈 슬롯 찾기
            {
                slot.AddItem(item);       // 아이템 추가
                Debug.Log(item.itemName + "이(가) 추가되었습니다!");
                return;
            }
        }
        Debug.Log("인벤토리에 빈 슬롯이 없습니다!");
    }
}
