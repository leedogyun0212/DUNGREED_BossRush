using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class St1AttackChoose : MonoBehaviour
{
    private CharacterManager Character = null;

    private Stage1Boss Boss = null;

    public int RandomNum { get; set; } = 1;

    public int LaserAtt { get; set; } = 0;

    public float TargetTimeCheck { get; set; } = 1.0f;

    public bool ChangeNum { get; set; } = false;

    private void Awake()
    {
        Character = GameManager.GetManagerClass<CharacterManager>();
        Boss = GetComponent<Stage1Boss>();
    }

    private void Update()
    {
        StartCoroutine(Choose());
    }

    private IEnumerator Choose()
    {

        yield return new WaitUntil(() => Boss.AnimChange == 0 && Boss.AttAnim == 0&&ChangeNum);
        ChangeNum = false;
        RandomNum += 1;
        if (RandomNum > 4) RandomNum = 1;
        if (RandomNum < 3)
        {
            Boss.AttAnim = 3;
        }
        NumChoose();
    }

    public void NumChoose()
    {
        if (RandomNum < 3)
        {
            Boss.st1Attack1.AttOn = false;
            Boss.AnimChange =-RandomNum-1;
            Boss.st1Attack1.LeftRight();
            ChangeNum = true;
        }
        else if (RandomNum == 3)
        {
            Boss.TestAtt2 = true;
            Boss.AnimChange = RandomNum;
            ChangeNum = true;
        }
        else if (RandomNum == 4)
        {
            Boss.st1Attack3.AttackContest = RandomNum+1;
            Boss.AnimChange = RandomNum;
            ChangeNum = true;
        }
    }

}
