using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndUI : MonoBehaviour
{
    private bool SceneBtn = false;

    private CharacterManager character = null;

    private MonsterManager monster = null;

    public Text PlayerGold;
    public Text GameTime;

    public Sprite DieBoss;

    public Image Success;

    private void Awake()
    {
        character = GameManager.GetManagerClass<CharacterManager>();
        monster = GameManager.GetManagerClass<MonsterManager>();
    }

    private void Start()
    {
        SetText();
    }

    private void SetText()
    {
        GameTime.text = (Time.time + "");
        PlayerGold.text = (character.Player.Coin+"");
    }

    private void OnEnable()
    {
        BossDie();
    }

    private void BossDie()
    {
        if(monster.Boss.HP<=0)
        {
            Success.sprite = DieBoss;
        }
    }

    public void ExitButton()
    {
        SceneNext.LoadScene("VillageScene");
    }
}
