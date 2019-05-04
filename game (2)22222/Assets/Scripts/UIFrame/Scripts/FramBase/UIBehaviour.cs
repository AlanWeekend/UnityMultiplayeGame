#region 模块信息
// **********************************************************************
// Copyright (C) 2017 The company name
//
// 文件名(File Name):             UIBehaviour.cs
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
using System;

namespace UIFrame
{
    public class UIBehaviour : UIMonoBehaviourCallbacks
    {
        #region About Module

        private string moduleName;

        #endregion

        #region UI Component

        protected RectTransform uiComponent_RectTransform;
        protected Text uiComponent_Text;
        protected Image uiComponent_Image;
        protected RawImage uiComponent_RawImage;
        protected Button uiComponent_Button;
        protected InputField uiComponent_InputField;
        protected Slider uiComponent_Slider;
        protected Toggle uiComponent_Toggle;
        protected Scrollbar uiComponent_Scrollbar;
        protected ScrollRect uiComponent_ScrollRect;
        protected Dropdown uiComponent_Dropdown;
        protected GridLayoutGroup uiComponent_GridLayoutGroup;

        #endregion

        #region Mono Callbacks

        /// <summary>
        /// 获取UI部件上的所有的组件，并在所属的模块中注册本部件
        /// </summary>
        protected virtual void Awake()
        {
            GetAllComponent();
            try
            {
                moduleName = transform.GetComponentInParent<UIModule>().name;
                Debug.Log(moduleName+" "+name);
            }
            catch (NullReferenceException ex)
            {
                try
                {
                    moduleName = transform.GetComponent<UIModule>().name;
                }
                catch(Exception e)
                {
                    Debug.LogError(e.Message);
                }
            }
            UIManager.instance.RegisterWidget(name, moduleName, this);
        }

        protected virtual void OnDestory()
        {
            UIManager.instance.UnRegisterWidget(name, moduleName);
        }

        #endregion

        #region Custom Functions

        /// <summary>
        /// 获取所有的UI组件
        /// </summary>
        public void GetAllComponent()
        {
            uiComponent_RectTransform = GetComponent<RectTransform>();
            uiComponent_Text= GetComponent<Text>();
            uiComponent_Image= GetComponent<Image>();
            uiComponent_RawImage= GetComponent<RawImage>();
            uiComponent_Button= GetComponent<Button>();
            uiComponent_InputField= GetComponent<InputField>();
            uiComponent_Slider = GetComponent<Slider>();
            uiComponent_Toggle = GetComponent<Toggle>();
            uiComponent_Scrollbar = GetComponent<Scrollbar>();
            uiComponent_ScrollRect = GetComponent<ScrollRect>();
            uiComponent_Dropdown = GetComponent<Dropdown>();
            uiComponent_GridLayoutGroup = GetComponent<GridLayoutGroup>();
        }

        #endregion
    }
}