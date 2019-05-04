#region 模块信息
// **********************************************************************
// Copyright (C) 2017 The company name
//
// 文件名(File Name):             KillTip.cs
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

public class KillTip : MonoBehaviour {

    Text killer;
    Text killed;

    private void Awake()
    {
        killer = transform.Find("KillerText").GetComponent<Text>();
        killed = transform.Find("KilledText").GetComponent<Text>();
    }

    /// <summary>
    /// 设置击杀提示文字
    /// </summary>
    /// <param name="killerName"></param>
    /// <param name=""></param>
    public void SetKillTipText(string killerName,string killedName)
    {
        killer.text = killerName;
        killed.text = killedName;
    }
}
