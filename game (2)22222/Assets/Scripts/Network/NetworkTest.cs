#region 模块信息
// **********************************************************************
// Copyright (C) 2017 The company name
//
// 文件名(File Name):             NetworkTest.cs
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
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.Collections.Generic;

public class NetworkTest : MonoBehaviourPunCallbacks, IOnEventCallback
{

    //自己
    GameObject player;
    /// <summary>
    /// 其他玩家
    /// </summary>
    List<GameObject> players = new List<GameObject>();

    void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    #region 测试用
    // void Awake()
    // {
    //     PhotonNetwork.ConnectUsingSettings();
    // }
    // void OnGUI()
    // {
    //     GUILayout.TextField(PhotonNetwork.NetworkClientState.ToString());
    // }

    // public override void OnConnectedToMaster()
    // {
    //     Debug.Log("连接成功");
    //     PhotonNetwork.JoinRandomRoom();
    // }

    // public override void OnJoinRandomFailed(short returnCode, string message)
    // {
    //     Debug.Log("加入房间失败");
    //     PhotonNetwork.CreateRoom("11");
    // }
    #endregion

    public override void OnLeftRoom()
    {
       // DestoryPlayer();
    }

    public void DestoryPlayer()
    {
        if (PhotonNetwork.AllocateViewID(photonView))
        {
            object[] data = new object[] {
                photonView.ViewID
            };

            RaiseEventOptions raiseEventOptions = new RaiseEventOptions
            {
                Receivers = ReceiverGroup.Others,
                CachingOption = EventCaching.AddToRoomCache
            };

            SendOptions sendOptions = new SendOptions
            {
                Reliability = true
            };

            PhotonNetwork.RaiseEvent(1, data, raiseEventOptions, sendOptions);
        }
        else
        {
            Debug.LogErrorFormat("Failed to allocate a ViewId.");
            Destroy(player);
        }
    }

    public void SpawnPlayer(string abName,Vector3 position)
    {
        Debug.Log("进入房间");
        player = ObjectPool.Instance.GetObj(abName);
        player.transform.position = position;
        //GameObject blood= Instantiate(Resources.Load<GameObject>("Blood"));
        //blood.transform.SetParent(player.transform);
        PhotonView photonView = player.GetComponent<PhotonView>();
        // Debug.Log(photonView.ViewID);
        // if (photonView.IsMine)
        // {
        Debug.Log(Camera.main.gameObject.name);
        Camera.main.transform.root.GetComponent<CameraFollow>().player1 = player.transform;
        // }

        if (PhotonNetwork.AllocateViewID(photonView))
        {
            object[] data = new object[] {
                player.transform.position,
                player.transform.rotation,
                photonView.ViewID,
                abName
            };

            RaiseEventOptions raiseEventOptions = new RaiseEventOptions
            {
                Receivers = ReceiverGroup.Others,
                CachingOption = EventCaching.AddToRoomCache
            };

            SendOptions sendOptions = new SendOptions
            {
                Reliability = true
            };

            PhotonNetwork.RaiseEvent(0, data, raiseEventOptions, sendOptions);
        }
        else
        {
            Debug.LogErrorFormat("Failed to allocate a ViewId.");
            Destroy(player);
        }
    }

    public void OnEvent(EventData photoEvent)
    {
        if (photoEvent.Code == 0)
        {
            GameObject player;
            // 读取信息
            object[] data = (object[])photoEvent.CustomData;

            player = ObjectPool.Instance.GetObj((string)data[3]);
            player.transform.position = (Vector3)data[0];
            player.transform.rotation = (Quaternion)data[1];

            player.GetComponent<PhotonView>().ViewID = (int)data[2];
            //保存玩家
            players.Add(player);
        }
        else if (photoEvent.Code == 1)
        {
            object[] data = (object[])photoEvent.CustomData;
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].GetComponent<PhotonView>().ViewID == (int)data[0])
                {
                    Destroy(player);
                    return;
                }
            }
        }
    }
}