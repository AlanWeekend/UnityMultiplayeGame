using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFrame;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class GameUIFunctions
{
    public GamePanelUIMoudule GameUIModule;

    public float value;
    public string nickName = PhotonNetwork.NickName;

    public GameUIFunctions(GamePanelUIMoudule module)
    {
        GameUIModule = module;
        GameUIInit();
    }

    private void GameUIInit()
    {
        GameUIModule.GetChildText("Text#", "1");
        //GameUIModule.GetChildText("", "");
        GameUIModule.SetExperienceBarValue("ExpValue#", value);
        //显示昵称
        GameUIModule.GetChildText("Name#", nickName);
    }
}

public class GamePanelUIMoudule : UIModule
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        GameUIFunctions loginFunctions = new GameUIFunctions(this);
    }

    //protected override void Start()
    //{
    //    LoginFunctions loginFunctions = new LoginFunctions(this);
    //}

    public void GetChildText(string widgetName,string text)
    {
         FindChildUIBehaviourByName(widgetName).SetTextText(text);
    }

    public void SetExperienceBarValue(string widgetName,float value)
    {
        FindChildUIBehaviourByName(widgetName).SetImageFillAmount(value);
    }

}
