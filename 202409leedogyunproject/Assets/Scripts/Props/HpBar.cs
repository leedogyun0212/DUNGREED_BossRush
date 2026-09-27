using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] private Image HPImage = null;

    private CharacterManager _CharacterManager = null;

    public Text HPText;

    private void Awake()
    {
        _CharacterManager = GameManager.GetManagerClass<CharacterManager>();
        HPImage = GetComponent<Image>();
    }

    private void Update()
    {
        HPImage.fillAmount = ((int)_CharacterManager.Player.Hp /  _CharacterManager.Player.MaxHp);
        HPText.text = (_CharacterManager.Player.Hp + "/" + _CharacterManager.Player.MaxHp);
    }
}
