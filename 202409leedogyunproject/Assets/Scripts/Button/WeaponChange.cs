using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponChange : MonoBehaviour
{
    private WeaponManager weapon = null;

    private TalkManager talk = null;

    private void Awake()
    {
        weapon = GameManager.GetManagerClass<WeaponManager>();
        talk = GameManager.GetManagerClass<TalkManager>();
    }

    public void SwithButton()
    {
        weapon.Swith = true;
    }

    public void MenuBtn()
    {
        talk.Setting = true;
    }

}
