#region 模块信息
// **********************************************************************
// Copyright (C) 2017 The company name
//
// 文件名(File Name):             Blood.cs
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
using Photon.Pun;

public class BloodBar : MonoBehaviour {

    //血条背景
    Image blackImg;
    //血条
    Image bloodImg;
    //跟随的玩家
    public Transform player;
    //玩家高度偏移量
    Vector2 offset;
    //玩家名字文本
    Text playerNameText;
    //血量上限
    int maxBlood;
    //玩家的ViewID
    public int selfId;

    // PhotonView photonView;

    private void Awake() {
        blackImg =transform.Find("BloodBack").GetComponent<Image>();
        bloodImg=blackImg.transform.Find("Blood").GetComponent<Image>();
        playerNameText=transform.Find("PlayerName").GetComponent<Text>();
        // photonView = GetComponent<PhotonView>();
    }

    private void FixedUpdate()
    {
        offset = new Vector2(0, 200);
        if(player!=null)
            transform.position = offset + RectTransformUtility.WorldToScreenPoint(Camera.main, player.position);
    }

    /// <summary>
    /// 设置玩家名字
    /// </summary>
    /// <param name="playerName"></param>
    public void SetName(string playerName)
    {
        playerNameText.text = playerName;
    }

    /// <summary>
    /// 设置血量上限
    /// </summary>
    /// <param name="maxBlood">血量上限</param>
    public void SetMaxBlood(int maxBlood)
    {
        this.maxBlood = maxBlood;
        blackImg.rectTransform.sizeDelta = new Vector2(maxBlood * 10,blackImg.rectTransform.sizeDelta.y);
        bloodImg.rectTransform.sizeDelta = blackImg.rectTransform.sizeDelta;  
    }

    /// <summary>
    /// 设置血量
    /// </summary>
    /// <param name="blood">血量</param>
    public void SetBlood(int blood)
    {
        bloodImg.fillAmount=(float)blood/(float)maxBlood;
    }
}
