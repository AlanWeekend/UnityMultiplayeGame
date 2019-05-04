using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;
using UIBehaviour = UIFrame.UIBehaviour;
using System;

public class SelectHeroUIBehavior : UIBehaviour
{

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

        uiComponent_Text.text = text;
    }

    public override void SetIamgeSprite(Sprite sprite)
    {
        if (!uiComponent_Image)
        {
            Debug.LogError("没有该组件");
        }

        uiComponent_Image.sprite = sprite;
    }

    public override void SetButtonOnClick(UnityAction unityAction)
    {
        if (!uiComponent_Button)
        {
            Debug.LogError("没有该组件");
        }

        uiComponent_Button.onClick.AddListener(unityAction);
    }

    public override void AddGridLayoutGroupChirld(GameObject gameObject)
    {
        gameObject.transform.SetParent(transform);
        gameObject.transform.localScale = Vector3.one;
        gameObject.transform.localEulerAngles = Vector3.zero;
        gameObject.transform.localPosition = Vector3.zero;
    }

    public override void ClearGridLayoutGroupChirld()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
