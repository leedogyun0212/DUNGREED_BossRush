using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    private WeaponManager weaponManager = null;

    public List<GameObject> weapons = new List<GameObject>(); // 2개의 무기만 저장
    public int activeWeaponIndex = 0; // 현재 사용 중인 무기의 인덱스 (0 또는 1)

    private void Awake()
    {
        weaponManager = GameManager.GetManagerClass<WeaponManager>();
    }

    private void Start()
    {
        if (weapons.Count < 2)
        {
            Debug.LogError("무기 리스트에는 정확히 2개의 무기가 필요합니다!");
            return;
        }

        UpdateWeapon();
    }

    private void Update()
    {
        if (weaponManager.Swith)
        {
            SwitchWeapon();
            weaponManager.Swith = false;
        }
    }

    public void SwitchWeapon()
    {
        activeWeaponIndex = 1 - activeWeaponIndex; // 0 -> 1, 1 -> 0 으로 변경
        UpdateWeapon();
    }

    private void UpdateWeapon()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].SetActive(i == activeWeaponIndex);
        }
    }

    public void ChangeWeapon(int slot, GameObject newWeapon)
    {
        if (slot < 0 || slot >= weapons.Count)
        {
            Debug.LogError("잘못된 슬롯 번호입니다! (0 또는 1만 가능)");
            return;
        }

        if (weapons[slot] != null)
        {
            Destroy(weapons[slot]);  // 기존 무기 오브젝트 삭제
            weapons[slot] = null;     // 리스트에서도 제거
        }

        // 프리팹을 인스턴스화하여 새로운 무기 생성
        GameObject newWeaponInstance = Instantiate(newWeapon, transform.position, Quaternion.Euler(0,0,0));
        newWeaponInstance.transform.SetParent(transform); // 부모 설정 (WeaponSwitch가 있는 오브젝트)

        newWeaponInstance.transform.localRotation = Quaternion.Euler(0, 0, 0);

        weapons[slot] = newWeaponInstance; // 새로운 무기로 교체
        UpdateWeapon(); // 현재 활성 무기 갱신
    }
}
