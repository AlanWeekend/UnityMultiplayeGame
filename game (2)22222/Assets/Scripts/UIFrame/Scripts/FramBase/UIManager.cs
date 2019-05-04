#region 模块信息
// **********************************************************************
// Copyright (C) 2017 The company name
//
// 文件名(File Name):             UIManager.cs
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
    public class UIManager : MonoBehaviour
    {
        #region SingleTon
        public static UIManager instance;
        #endregion

        #region Field
        //ui行为脚本字典
        private Dictionary<string, Dictionary<string, UIBehaviour>> uiDics;
        private UITokenInfo uiTokenInfo;
        //private 
        #endregion

        #region Mono Callback
        private void Awake()
        {
            instance = this;
            uiDics = new Dictionary<string, Dictionary<string, UIBehaviour>>();
            uiTokenInfo = UIAssetsManger.GetAsset<UITokenInfo>(UIConst.UIToken);
        }
        #endregion

        #region Functions
        /// <summary>
        /// 获取UIToken列表
        /// </summary>
        /// <returns></returns>
        public List<UIToken> GetUITokenList()
        {
            if (uiTokenInfo == null) return null;
            return uiTokenInfo.uITokens;
        }
        #endregion

        #region (Un) Register UIElement
        /// <summary>
        /// 注册UI模块
        /// </summary>
        /// <param name="moduleName"></param>
        public void RegisterUIModule(string moduleName)
        {
            if (!uiDics.ContainsKey(moduleName)) uiDics.Add(moduleName, new Dictionary<string, UIBehaviour>());
        }

        /// <summary>
        /// 解除UI模块
        /// </summary>
        /// <param name="moduleName"></param>
        public void UnRegisterUIModule(string moduleName)
        {
            if (uiDics.ContainsKey(moduleName)) uiDics.Remove(moduleName);
        }
        #endregion

        #region (Un) Register UIWidget

        /// <summary>
        /// 注册UI部件
        /// </summary>
        /// <param name="widgetName">UI部件名</param>
        /// <param name="moduleName">所属模块名</param>
        /// <param name="uIBehaviour">UI部件的行为脚本</param>
        public void RegisterWidget(string widgetName, string moduleName, UIBehaviour uIBehaviour)
        {
            //检测模块是否添加
            RegisterUIModule(moduleName);
            if (uiDics[moduleName].ContainsKey(widgetName)) uiDics[moduleName][widgetName] = uIBehaviour;
            else uiDics[moduleName].Add(widgetName, uIBehaviour);
        }

        /// <summary>
        /// 解除UI部件
        /// </summary>
        /// <param name="widgetName">UI部件名</param>
        /// <param name="moduleName">所属模块名</param>
        public void UnRegisterWidget(string widgetName, string moduleName)
        {
            if (!uiDics.ContainsKey(moduleName)) return;
            if (uiDics[moduleName].ContainsKey(widgetName)) uiDics[moduleName].Remove(widgetName);
        }

        #endregion

        #region Find Widget Functions
        /// <summary>
        /// 通过名称查找对应的行为脚本
        /// </summary>
        /// <param name="widgetName">部件名</param>
        /// <param name="moduleName">模块名</param>
        /// <returns></returns>
        public UIBehaviour FindUIBehaviourByName(string widgetName,string moduleName)
        {
            if (!uiDics.ContainsKey(moduleName))
            { Debug.Log("模块不存在" + moduleName);return null; }
            if (!uiDics[moduleName].ContainsKey(widgetName))
            { Debug.Log("部件不存在" + widgetName);return null; }
            return uiDics[moduleName][widgetName];
        }
        #endregion
    }
}