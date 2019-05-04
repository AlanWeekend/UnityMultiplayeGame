using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HeroModule{

    //人物等级
    private int heroLevel;
    //人物血量
    private string heroHP;
    //人物经验条
    private Sprite heroExperience;
    //人物名字
    private string heroName;
    //武器名字
    private string weaponName;

    public string HeroName
    {
        get
        {
            return heroName;
        }
        set
        {
            heroName = value;
            if (OnNameValueChange != null)
            {
                OnNameValueChange(value);
            }
        }
    }

    public int HeroLevel
    {
        get
        {
            return heroLevel;
        }

        set
        {
            heroLevel = value;
            if (OnLevelValueChange != null)
            {
                OnLevelValueChange(heroLevel);
            }
        }
    }

    public string HeroHP
    {
        get
        {
            return heroHP;
        }

        set
        {
            heroHP = value;
            if (OnHPValueChange != null)
            {
                OnHPValueChange(heroHP);
            }
            else
            {
                Debug.Log("事件为空");
            }
        }
    }

    public Sprite HeroExperience
    {
        get
        {
            return heroExperience;
        }

        set
        {
            heroExperience = value;
            if (OnExperienceValueChange != null)
            {
                OnExperienceValueChange(heroExperience);
            }
        }
    }

    public string WeaponName
    {
        get
        {
            return weaponName;
        }

        set
        {
            weaponName = value;
            if (ShowWeaponName!=null)
            {
                ShowWeaponName(weaponName);
            }
        }
    }

    public event Action<int> OnLevelValueChange;

    public event Action<Sprite> OnExperienceValueChange;
    //选择人物时显示初始血量
    public event Action<string> OnHPValueChange;
    //选择人物时显示初始名称
    public event Action<string> OnNameValueChange;
    //选择人物时显示初始武器
    public event Action<string> ShowWeaponName;

    public void Init(int level, string hp)
    {
        HeroLevel = level;
        heroHP = hp;
    }

}
