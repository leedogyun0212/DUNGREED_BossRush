using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Image itemImage;  // 아이템 아이콘
    public Item currentItem { get; set; } // 현재 슬롯에 있는 아이템
    private Transform originalParent;  // 드래그 전 부모
    private CanvasGroup canvasGroup;  // 드래그 중 투명도 조절
    private static InventorySlot draggedSlot;  // 현재 드래그 중인 슬롯

    public bool isWeaponSlot = false;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void AddItem(Item newItem)
    {
        currentItem = newItem;          // 현재 슬롯에 아이템 추가
        itemImage.sprite = newItem.itemIcon; // 아이템 아이콘 표시
        itemImage.color = new Color(255, 255, 255, 255);

        itemImage.enabled = true;       // 이미지 활성화
    }

    public void ClearSlot()
    {
        currentItem = null;
        itemImage.sprite = null;
        itemImage.enabled = false;
    }

    public bool IsEmpty()
    {
        return currentItem == null;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (currentItem == null) return;

        // 드래그 시작 시
        originalParent = transform.parent;
        canvasGroup.alpha = 0.6f; // 투명도 조절
        canvasGroup.blocksRaycasts = false; // 다른 슬롯이 드롭을 감지할 수 있게 설정
        transform.SetParent(originalParent.parent); // 상위 Canvas로 이동
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (currentItem == null) return;

        // 드래그 중 슬롯 따라 움직임
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (currentItem == null) return;

        // 드래그 종료 시 원래 위치로 되돌림
        transform.SetParent(originalParent);
        transform.localPosition = Vector3.zero;
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        // 다른 슬롯에 아이템을 드롭했을 때 교환
        InventorySlot droppedSlot = eventData.pointerDrag.GetComponent<InventorySlot>();

        if (droppedSlot != null && droppedSlot != this)
        {
            InventorySystem inventorySystem = FindObjectOfType<InventorySystem>();
            WeaponSwitch weaponSwitch = FindObjectOfType<WeaponSwitch>();

            // 현재 슬롯이 weaponSlots(0번)인지, weaponSlots2(1번)인지 확인
            bool isWeaponSlot1 = inventorySystem.weaponSlots.Contains(this);
            bool isWeaponSlot2 = inventorySystem.weaponSlots2.Contains(this);

            SwapItems(droppedSlot);

            Debug.Log(droppedSlot.currentItem+"sdad"+this.currentItem);

            // 무기 위치에 따라 activeWeaponIndex 변경
            if (isWeaponSlot1)
            {
                weaponSwitch.activeWeaponIndex = 0;
                weaponSwitch.ChangeWeapon(weaponSwitch.activeWeaponIndex, this.currentItem.Weapon);
            }
            else if (isWeaponSlot2)
            {
                weaponSwitch.activeWeaponIndex = 1;
                weaponSwitch.ChangeWeapon(weaponSwitch.activeWeaponIndex, this.currentItem.Weapon);
            }
        }
    }



    private void SwapItems(InventorySlot targetSlot)
    {
        // 아이템 교환 로직
        Item tempItem = targetSlot.currentItem;
        targetSlot.AddItem(this.currentItem);
        this.AddItem(tempItem);
    }
}
