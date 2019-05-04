using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;
using UIBehaviour = UIFrame.UIBehaviour;
using System;

public class GameUIBehaviour : UIBehaviour {

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnDestory()
    {
        base.OnDestory();
    }

    public override void SetTextText(string text)
    {
        if (!uiComponent_Text)
        {
            Debug.LogError("没有该组件");
        }

        uiComponent_Text.text=text;
    }

    public override void SetImageFillAmount(float value)
    {
        if (!uiComponent_Image)
        {
            Debug.LogError("没有该组件");
        }
        uiComponent_Image.fillAmount = value;
    }
}
