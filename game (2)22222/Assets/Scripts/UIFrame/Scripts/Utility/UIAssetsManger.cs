#region 模块信息
// **********************************************************************
// Copyright (C) 2017 The company name
//
// 文件名(File Name):             UIAssetsManger.cs
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

namespace UIFrame
{
    public class UIAssetsManger 
    {
        /// <summary>
        /// 加载资源文件
        /// </summary>
        /// <typeparam name="T">文件类型</typeparam>
        /// <param name="assetName">文件名</param>
        /// <returns></returns>
        public static T GetAsset<T>(string assetName) where T : Object
        {
            //通过反射获取常量类中的文件名
            string assetsPath = UIReflection.GetConstField(assetName + UIConst.ASSETPATH, typeof(UIConst)).ToString();
            return Resources.Load<T>(assetsPath);
        }
    }
}