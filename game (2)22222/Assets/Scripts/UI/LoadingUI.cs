#region 模块信息
// **********************************************************************
// Copyright (C) 2017 The company name
//
// 文件名(File Name):             LoadingUI.cs
// 公司(Company):  		 		  #COMPANY#
// 作者(Author):                  #AuthorName#
// 版本号(Version):		 		  #VERSION#
// Unity版本号(Unity Version):	  #UNITYVERSION#
// 创建时间(CreateTime):          #DATE#
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************
#endregion
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class LoadingUI : MonoBehaviourPunCallbacks {

    Text status;
    Text progress;
    Slider slider;

    private void Awake() {
        slider =transform.Find("LoadingPanel/Slider").GetComponent<Slider>();
        status=transform.Find("LoadingPanel/VersionsText").GetComponent<Text>();
        progress=transform.Find("LoadingPanel/UpdateText").GetComponent<Text>();
    }

    private void Start() {        
        Debug.Log(AB.Instance);
        AB.Instance.Init(status,progress,slider,Init);
    }
    //连接服务器
    public void Init()
    {
        progress.text="连接服务器...";
        PhotonNetwork.ConnectUsingSettings();
    }

    /// <summary>
    /// 连接服务器成功回调
    /// </summary>
    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("UIScenc01");
    }
}