using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    private CharacterManager characterManager = null;

    private WeaponManager weaponManager = null;

    private TalkManager talkManager = null; 

    public bool TalkButton { get; set; } = false;
    public bool TalkButton2 { get; set; } = false;

    public Image AttackBtn;

    public Sprite ChangeAtt;

    public Sprite AttImage;

    public Text Coin;

    private void Awake()
    {
        characterManager = GameManager.GetManagerClass<CharacterManager>();
        weaponManager = GameManager.GetManagerClass<WeaponManager>();
        talkManager = GameManager.GetManagerClass<TalkManager>();
    }

    private void Update()
    {
        Change();
        PlayerCoin();
    }

    private void Change()
    {
        if (talkManager.talkInstance.Talk)
        {
            TalkButton = true;
            AttackBtn.sprite = ChangeAtt;
        }
        else if (!talkManager.talkInstance.Talk)
        {
            TalkButton = false;
            AttackBtn.sprite = AttImage;
        }
    }

    public void AttButtondown()
    {
        if (!TalkButton)
        {
            weaponManager.AttBtn = true;
            Debug.Log(weaponManager.playerSwordAttack);
        }
        else if (TalkButton)
        {
            talkManager.talkInstance.NpcOpen = true;
        }
    }

    public void AttButtonup()
    {
        weaponManager.AttBtn = false;
    }

    public void JumpButtondown()
    {
        characterManager.Player.playerMovement.JumpButton = true;
    }

    public void JumpButtonup()
    {
        characterManager.Player.playerMovement.JumpButton = false;
    }

    public void DashButtondown()
    {
        characterManager.Player.playerDash.DashBtn = true;
        Debug.Log(characterManager.Player.playerDash.DashBtn);
    }

    public void PlayerCoin()
    {
        Coin.text = (characterManager.Player.Coin + "");
    }

}
