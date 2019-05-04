#region 模块信息
// **********************************************************************
// Copyright (C) 2017 The company name
//
// 文件名(File Name):             UIInterface.cs
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
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

namespace UIFrame
{
    #region UIInterface

    public interface IUtility
    {
        void SetEventTrigger(EventTriggerType eventTriggerType, UnityAction<Behaviour> unityAction);
        void AddComponent(Type type);
    }

    public interface IRectTransform
    {
        void SetSizeDelta(Vector3 vector3);
        void SetPosition(Vector3 vector3);
        void SetParent(Transform parent);
        void SetPovit(Vector2 vector2);
        Transform GetTransform();
    }

    public interface IText
    {
        void SetTextText(string text);
        string GetTextText();
        void SetTextColor(Color color);
    }

    public interface IImage
    {
        void SetIamgeSprite(Sprite sprite);
        Sprite GetImageSprite(Sprite sprite);
        void SetImageFillAmount(float value);
    }

    public interface IRawImage
    {
        void SetRawIamgeTexture(Texture texture);
        Texture GetRawImageTexture();
    }

    public interface IButton
    {
        void SetButtonOnClick(UnityAction unityAction);
    }

    public interface IInputField
    {
        string GetInputField();
        void SetInputFieldOnValueChange(UnityAction<string> unityAction);
    }

    public interface IToggle
    {
        bool GetToggleValue();
        void SetToggleOnValueChange(UnityAction<bool> unityAction);
    }

    public interface IVerticalLayoutGroup
    {
        void AddVerticalLayoutGroupChirld(GameObject gameObject);
    }

    public interface IGridLayoutGroup
    {
        void AddGridLayoutGroupChirld(GameObject gameObject);
        void ClearGridLayoutGroupChirld();
    }

    #endregion

    public class UIMonoBehaviourCallbacks : MonoBehaviour, IUtility, IRectTransform, IText, IImage, IRawImage, IButton, IInputField, IToggle, IVerticalLayoutGroup,IGridLayoutGroup
    {
        public virtual void AddComponent(Type type)
        {
            throw new NotImplementedException();
        }

        public virtual void AddGridLayoutGroupChirld(GameObject gameObject)
        {
            throw new NotImplementedException();
        }

        public virtual void AddVerticalLayoutGroupChirld(GameObject gameObject)
        {
            throw new NotImplementedException();
        }

        public virtual void ClearGridLayoutGroupChirld()
        {
            throw new NotImplementedException();
        }

        public virtual Sprite GetImageSprite(Sprite sprite)
        {
            throw new NotImplementedException();
        }

        public virtual string GetInputField()
        {
            throw new NotImplementedException();
        }

        public virtual Texture GetRawImageTexture()
        {
            throw new NotImplementedException();
        }

        public virtual string GetTextText()
        {
            throw new NotImplementedException();
        }

        public virtual bool GetToggleValue()
        {
            throw new NotImplementedException();
        }

        public virtual Transform GetTransform()
        {
            throw new NotImplementedException();
        }

        public virtual void SetButtonOnClick(UnityAction unityAction)
        {
            throw new NotImplementedException();
        }

        public virtual void SetEventTrigger(EventTriggerType eventTriggerType, UnityAction<Behaviour> unityAction)
        {
            throw new NotImplementedException();
        }

        public virtual void SetImageFillAmount(float value)
        {
            throw new NotImplementedException();
        }

        public virtual void SetIamgeSprite(Sprite sprite)
        {
            throw new NotImplementedException();
        }

        public virtual void SetInputFieldOnValueChange(UnityAction<string> unityAction)
        {
            throw new NotImplementedException();
        }

        public virtual void SetParent(Transform parent)
        {
            throw new NotImplementedException();
        }

        public virtual void SetPosition(Vector3 vector3)
        {
            throw new NotImplementedException();
        }

        public virtual void SetPovit(Vector2 vector2)
        {
            throw new NotImplementedException();
        }

        public virtual void SetRawIamgeTexture(Texture texture)
        {
            throw new NotImplementedException();
        }

        public virtual void SetSizeDelta(Vector3 vector3)
        {
            throw new NotImplementedException();
        }

        public virtual void SetTextColor(Color color)
        {
            throw new NotImplementedException();
        }

        public virtual void SetTextText(string text)
        {
            throw new NotImplementedException();
        }

        public virtual void SetToggleOnValueChange(UnityAction<bool> unityAction)
        {
            throw new NotImplementedException();
        }
    }
}