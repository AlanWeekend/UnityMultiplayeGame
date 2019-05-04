using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class MyNetworkManager : MonoBehaviourPunCallbacks{

    //加入房间失败委托
    public Action joinRandomFailed;
    //加入房间成功的委托
    public Action joinRoom;
    //单例
    public static MyNetworkManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 加入房间失败回调
    /// </summary>
    /// <param name="returnCode"></param>
    /// <param name="message"></param>
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        if (joinRandomFailed != null) joinRandomFailed();
    }
    /// <summary>
    /// 加入房间成功的回调
    /// </summary>
    public override void OnJoinedRoom()
    {
        if (joinRoom != null)
        {
            joinRoom();
        }
    }



}
