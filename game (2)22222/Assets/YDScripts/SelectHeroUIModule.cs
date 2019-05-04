using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFrame;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public class SelectHeroFunctions
{
    public SelectHeroUIModule SelectHeroUIModule;
    private GameObject heroShowPoint;

    
    public SelectHeroFunctions(SelectHeroUIModule module)
    {
        SelectHeroUIModule = module;
        heroShowPoint = GameObject.Find("HeroShowPoint");
        SelectHeroUIInit();
    }

    private void SelectHeroUIInit()
    {
        SelectHeroUIModule.gameObject.SetActive(false);
        SelectHeroUIModule.GetChildButton("BackButton@", SwitchPanelUIPanel);
    }

    public void SwitchPanelUIPanel()
    {
        //返回到登陆界面
        UIManager.instance.FindUIBehaviourByName("SelectHeroPanel%", "SelectHeroPanel%").gameObject.SetActive(false);
        UIManager.instance.FindUIBehaviourByName("LobbyPanel%", "LobbyPanel%").gameObject.SetActive(true);

        //回收选择人物界面中的模型
        for (int i = 0; i < heroShowPoint.transform.childCount; i++)
        {
            if (heroShowPoint.transform.GetChild(i).gameObject.activeSelf == true)
                //ObjectPool.Instance.RecyleObj(heroShowPoint.transform.GetChild(i).gameObject);
                SelectHeroUIModule.DestroyHeroMoudle(heroShowPoint.transform.GetChild(i).gameObject);
        }
    }

}

public class SelectHeroUIModule : UIModule {

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        SelectHeroFunctions loginFunctions = new SelectHeroFunctions(this);
    }

    public void GetChildText(string widgetName, string text)
    {
        FindChildUIBehaviourByName(widgetName).SetTextText(text);
    }

    /// <summary>
    /// 选择英雄显示信息 血量 名字 武器
    /// </summary>
    /// <param name="widgetName"></param>
    /// <param name="text"></param>
    public void GetHeroMessageText(string widgetName, string text)
    {
        FindChildUIBehaviourByName(widgetName).SetTextText(text);
    }

    public void GetHeroImage(string widgetName, string text)
    {
        FindChildUIBehaviourByName(widgetName).SetTextText(text);
    }

    public void GetChildButton(string widgetName, UnityAction unityAction)
    {
        FindChildUIBehaviourByName(widgetName).SetButtonOnClick(unityAction);
    }

    public void DestroyHeroMoudle( GameObject hsp)
    {
        Destroy(hsp);
    }

}
