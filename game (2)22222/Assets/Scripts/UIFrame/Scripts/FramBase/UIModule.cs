#region 模块信息
// **********************************************************************
// Copyright (C) 2017 The company name
//
// 文件名(File Name):             UIModule.cs
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
using System;

namespace UIFrame
{
    public class UIModule : MonoBehaviour
    {

        #region Field
        Transform[] allChild;
        #endregion

        #region Mono Callback

        /// <summary>
        /// 获取所有的子对象
        /// </summary>
        protected virtual void Awake()
        {
            allChild = transform.GetComponentsInChildren<Transform>();
        }

        /// <summary>
        /// 给所有的子对象添加行为脚本，并注册本模块
        /// </summary>
        protected virtual void Start()
        {
            //注册本模块
            UIManager.instance.RegisterUIModule(name);
            AddUIBehaviour();
        }

        void Update()
        {

        }
        #endregion

        #region Functions

        /// <summary>
        /// 为子对象添加对应的行为脚本
        /// </summary>
        private void AddUIBehaviour()
        {
            for (int i = 0; i < allChild.Length; i++)
            {
                for (int j = 0; j < UIManager.instance.GetUITokenList().Count; j++)
                {
                    UIToken uIToken = UIManager.instance.GetUITokenList()[j];
                    if (allChild[i].name.EndsWith(uIToken.token))
                    {
                        Type currentType = UIReflection.GetUIBehaviourByName(uIToken.behaviourName, UIConst.UIBEHAVIOUR);
                        if (currentType == null) return;

                        //避免重复添加脚本
                        if (allChild[i].gameObject.GetComponent(currentType) == null)
                        {
                            allChild[i].gameObject.AddComponent(currentType);
                        }
                    }
                }
            }
        }

        #endregion

        #region Find Child Behaviour
        /// <summary>
        /// 通过部件名称查找对应的行为脚本
        /// </summary>
        /// <param name="widgetName"></param>
        /// <returns></returns>
        public UIBehaviour FindChildUIBehaviourByName(string widgetName)
        {
            //return UIManager.instance.FindUIBehaviourByName(name, widgetName);
            return UIManager.instance.FindUIBehaviourByName(widgetName, name);
        }
        #endregion
    }
}