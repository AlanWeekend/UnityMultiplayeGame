using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UIFrame;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Photon.Pun;
using UnityEngine.UI;

public class LobbyPanelUIFunctions
{
    public LobbyPanelUIModule lobbyPanelUIModule;

    public LobbyPanelUIFunctions(LobbyPanelUIModule module)
    {
        lobbyPanelUIModule = module;
        LobbyPanelUIInit();
    }
    private void LobbyPanelUIInit()
    {
        lobbyPanelUIModule.GetChildButton("MeleeButton$", EnterGame);
        lobbyPanelUIModule.GetChildButton("SelectHeroButton$", SwitchSelectHeroPanel);
    }


    /// <summary>
    /// 切换到选择人物界面
    /// </summary>
    public void SwitchSelectHeroPanel()
    {
        lobbyPanelUIModule.FindChildUIBehaviourByName("MeleeButton$").GetComponent<Button>().interactable = true;
        UIManager.instance.FindUIBehaviourByName("SelectHeroPanel%", "SelectHeroPanel%").gameObject.SetActive(true);
        UIManager.instance.FindUIBehaviourByName("LobbyPanel%", "LobbyPanel%").gameObject.SetActive(false);
        
    }

    /// <summary>
    /// 随机进入房间
    /// </summary>
    private void EnterGame()
    {
        SetNickName();
        Debug.Log(PhotonNetwork.NickName.Trim().Equals(""));
        if (SelectHero.heroName == null || PhotonNetwork.NickName.Trim().Equals(""))
        {
            return;
        }
        lobbyPanelUIModule.FindChildUIBehaviourByName("MeleeButton$").GetComponent<Button>().interactable = false;
        MyNetworkManager.Instance.joinRandomFailed += JoinRandomRoomFailed;
        MyNetworkManager.Instance.joinRoom += JoinRoom;
        PhotonNetwork.JoinRandomRoom();

    }

    /// <summary>
    /// 输入昵称
    /// </summary>
    public void SetNickName()
    {
        //lobbyPanelUIModule.SetInputText("Text$", PhotonNetwork.NickName = lobbyPanelUIModule.InputFieldText("Text$"));
        PhotonNetwork.NickName = lobbyPanelUIModule.InputFieldText("Text$");
        Debug.Log(PhotonNetwork.NickName);
    }

    /// <summary>
    /// 随机进入房间失败的话，创建房间
    /// </summary>
    public void JoinRandomRoomFailed()
    {
        PhotonNetwork.CreateRoom(("房间"+Random.Range(1,100)).ToString());
        Debug.Log("建房间");
    }
    /// <summary>
    /// 加入房间成功的回调
    /// </summary>
    public void JoinRoom()
    {
        SceneManager.LoadScene("sence1");
        Debug.Log(PhotonNetwork.CurrentRoom.Name);
        Debug.Log("进入游戏");
    }
}

public class LobbyPanelUIModule : UIModule
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        LobbyPanelUIFunctions lobbyPanelUIFunctions = new LobbyPanelUIFunctions(this); 
    }

    public void GetChildButton(string widgetName,UnityAction unityAction)
    {
        FindChildUIBehaviourByName(widgetName).SetButtonOnClick(unityAction);
    }

    //public void GetInputFieldText(string widgetName, UnityAction<string> unityAction)
    //{
    //    FindChildUIBehaviourByName(widgetName).SetInputFieldOnValueChange(unityAction);
    //}


    /// <summary>
    /// 设置输入框的文字
    /// </summary>
    /// <param name="widgetName"></param>
    /// <param name="text"></param>
    public void SetInputText(string widgetName, string text)
    {
        FindChildUIBehaviourByName(widgetName).SetTextText(text);
    }


    /// <summary>
    /// 获得输入框中的文字
    /// </summary>
    /// <param name="widgetName"></param>
    /// <returns></returns>
    public string InputFieldText(string widgetName)
    {
        return FindChildUIBehaviourByName(widgetName).GetTextText();
    }
}
