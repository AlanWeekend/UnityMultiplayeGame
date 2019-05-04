using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UIFrame;

public class HeroView : MonoBehaviour {

    public static HeroView instence;
    public HeroModule heroModule;
    public HeroController heroController;

    //public event UnityAction addSelectNinjaClick;



    private void Awake()
    {
        instence = this;
    }

    private void Start()
    {
        heroModule = new HeroModule();
        heroController = new HeroController();
        BindModuleEvent();
    }

    private void BindModuleEvent()
    {
        heroModule.OnExperienceValueChange += OnExpValueChange;
        heroModule.OnHPValueChange += OnHPValueChange;
        heroModule.OnNameValueChange += OnNameValueChange;
        heroModule.ShowWeaponName += HeroWeaponName;
    }

    public void OnHPValueChange(string hp)
    {
        UIManager.instance.FindUIBehaviourByName("HealthNum@", "SelectHeroPanel%").SetTextText(hp.ToString());
    }

    public void OnNameValueChange(string name)
    {
        UIManager.instance.FindUIBehaviourByName("HeroName@", "SelectHeroPanel%").SetTextText(name);
    }
    public void HeroWeaponName(string name)
    {
        UIManager.instance.FindUIBehaviourByName("WeaponName@", "SelectHeroPanel%").SetTextText(name);
    }
    public void OnExpValueChange(Sprite expSprite)
    {
        UIManager.instance.FindUIBehaviourByName("ExpValue@", "SelectHeroPanel%").GetImageSprite(expSprite);
    }

}
