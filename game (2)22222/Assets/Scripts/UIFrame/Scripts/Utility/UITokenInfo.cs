#region 模块信息
// **********************************************************************
// Copyright (C) 2017 The company name
//
// 文件名(File Name):             UITokenInfo.cs
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
using System.Collections.Generic;

namespace UIFrame
{
    [System.Serializable]
    public class UIToken
    {
        public string token;
        public string behaviourName;
    }

    [CreateAssetMenu(fileName ="UIToken",menuName ="UIFrame/UITokenInfo",order =1000)]
    public class UITokenInfo:ScriptableObject
    {
        public List<UIToken> uITokens;
    }
}
