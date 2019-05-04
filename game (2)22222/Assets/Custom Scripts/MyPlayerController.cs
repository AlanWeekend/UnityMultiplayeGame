#region 模块信息
// **********************************************************************
// Copyright (C) 2019 QIANFENG EDUCATION
//
// 文件名(File Name):MyPlayerController.cs
// 公司(Company):#COMPANY#
// 作者(Author):#AUTHOR#
// 版本号(Version):#VERSION#
// Unity版本	(Unity Version):#UNITYVERSION#
// 创建时间(CreateTime):#DATE#
// 修改者列表(modifier):无
// 模块描述(Module description):MyPlayerController
// **********************************************************************
#endregion
using UnityEngine;
using System.Collections;

public class MyPlayerController {

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="playerName">Player name.</param>
    public MyPlayerController(string playerName)
    {
        //给玩家命名
        this.playerName = playerName;
        //生成玩家对象
        currentPlayer = new GameObject(playerName);
    }

    public string playerName;

    public GameObject currentPlayer;
    /// <summary>
    /// 获取当前玩家对象
    /// </summary>
    /// <returns>The current player.</returns>
    public GameObject GetCurrentPlayer()
    {
        return currentPlayer;
    }

    /// <summary>
    /// 给当前玩家对象添加一个组件
    /// </summary>
    /// <returns>The component for player.</returns>
    /// <param name="type">Type.</param>
    public Component AddComponentForPlayer(System.Type type)
    {
        return currentPlayer.AddComponent(type);
    }
}