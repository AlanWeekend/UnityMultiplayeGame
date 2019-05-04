using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;
using UIBehaviour = UIFrame.UIBehaviour;
using System;
using Photon;

public class LobbyPanelUIBehavior : UIBehaviour {

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnDestory()
    {
        base.OnDestory();
    }


    //Component_InputField中的text
    public override void SetTextText(string text)
    {
        if (!uiComponent_Text)
        {
            Debug.LogError("没有该组件");
        }

        uiComponent_Text.text = text;
    }
    public override string GetTextText()
    {
        return uiComponent_Text.text;
    }

    //public override void SetInputFieldOnValueChange(UnityAction<string> unityAction)
    //{
    //    if (!uiComponent_InputField)
    //    {
    //        Debug.LogError("没有该组件");
    //    }
    //    uiComponent_InputField.onValueChanged
    //}

    public override void SetButtonOnClick(UnityAction unityAction)
    {
        if (!uiComponent_Button)
        {
            Debug.LogError("没有该组件");
        }

        uiComponent_Button.onClick.AddListener(unityAction);
    }

}
