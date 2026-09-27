using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcTalk : MonoBehaviour
{
    private TalkManager talkManager = null;

    public Text Name;
    public Text Data;
    public Text Go;

    private bool ShNpc = false;
    private bool ReNpc = false;
    public bool Exit = false;

    private IEnumerator Start()
    {
        talkManager = GameManager.GetManagerClass<TalkManager>();
        yield return new WaitForSeconds(0.1f);  
    }

    private void OnEnable()
    {
        
    }

    private void Update()
    {
        //Debug.Log(talkManager.talkInstance);
    }

    public void Open()
    {
        if(talkManager.talkInstance.shopNpc.Shopbool)
        {
            Name.text = talkManager.talkInstance.shopNpc.Name;
            Data.text = talkManager.talkInstance.shopNpc.Data;
            Go.text = talkManager.talkInstance.shopNpc.PlayerGo;
            talkManager.talkInstance.shopNpc.Shopbool = false;
            ShNpc = true;
            Debug.Log(talkManager.talkInstance.shopNpc.Shopbool + "NpcTalk");
        }
        else if (talkManager.talkInstance.restNpc.Restbool)
        {
            Name.text = talkManager.talkInstance.restNpc.Name;
            Data.text = talkManager.talkInstance.restNpc.Data;
            Go.text = talkManager.talkInstance.restNpc.PlayerGo;
            talkManager.talkInstance.restNpc.Restbool = false;
            ReNpc = true;
        }
    }

    public void GoButton()
    {
        if (ShNpc)
        {
            ShNpc = false;
            talkManager.talkInstance.shopNpc.OpenShop();
        }
        else if (ReNpc)
        {
            ReNpc = false;
            talkManager.talkInstance.restNpc.RestaurantOpen();
        }

        Invoke("DisableObject", 0.1f);

    }

    public void ExitButton()
    {
        Exit = true;
        ShNpc = false;
        ReNpc = false;
        gameObject.SetActive(false);
        if (gameObject.activeSelf) gameObject.SetActive(false);
        Debug.Log(this.gameObject.activeSelf + "¿€µø");
    }

    private void DisableObject()
    {
        gameObject.SetActive(false);
    }

}
