#region 模块信息
// **********************************************************************
// Copyright (C) 2017 The company name
//
// 文件名(File Name):             KillCanvas.cs
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

public class KillCanvas : MonoBehaviour {

    public static KillCanvas Instance { set; get; }

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 设置击杀提示
    /// </summary>
    /// <param name="killerName">击杀者名</param>
    /// <param name="killedName">被击杀者名</param>
    public void SetKillTip(string killerName,string killedName)
    {
        ClearKillTip();
        GameObject go = Instantiate(Resources.Load<GameObject>("KillTip"));
        go.transform.SetParent(transform);
        go.transform.localPosition= Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;
        go.GetComponent<KillTip>().SetKillTipText(killerName, killedName);
        Invoke("ClearKillTip",2f);
    }

    /// <summary>
    /// 清除击杀提示
    /// </summary>
    private void ClearKillTip()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
