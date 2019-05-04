using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIFrame;

public class btntest : MonoBehaviour {

    string heroName = "忍者";
    int heroHp = 100;
    string heroWeapon = "123";
    private GameObject heroShowPoint;
    Button button;


    private void Awake()
    {
        button = GetComponent<Button>();
        heroShowPoint = GameObject.Find("HeroShowPoint");

    }
    void Start ()
    {
        button.onClick.AddListener(Onclick);
	}


    public void Onclick()
    {
        //heroShowPoint.transform.GetChild(i)

        for (int i = 0; i < heroShowPoint.transform.childCount; i++)
        {
            //heroShowPoint.transform.GetChild(i)
            if (heroShowPoint.transform.GetChild(i).gameObject.activeSelf==true)
            Destroy(heroShowPoint.transform.GetChild(i).gameObject);

        }

        //生成职业第一的模型

        GameObject heroModule = ObjectPool.Instance.GetObj(name.Substring(gameObject.name.IndexOf('_') + 1) + "01");
        PlayerAttack pa = heroModule.GetComponent<PlayerAttack>();
        PlayerMove pm = heroModule.GetComponent<PlayerMove>();
        Destroy(pa);
        Destroy(pm);
        heroModule.AddComponent<ShowHeroInit>();
        SelectHero.heroName = name.Substring(gameObject.name.IndexOf('_') + 1) + "01";


        //获取当前按钮对应的职业名
        string career = gameObject.name.Substring(gameObject.name.IndexOf('_') + 1);

        //显示职业属性 血量 名称 武器名
        string[] attribute = DBA.Instance.GetCareerAttributeByName(career);
        HeroView.instence.heroModule.HeroHP= attribute[0];
        HeroView.instence.heroModule.HeroName = attribute[1];
        HeroView.instence.heroModule.WeaponName = attribute[2];

        //HeroView.instence.heroModule.HeroExperience


        //获取当前职业的所有图标名
        string[] icons = DBA.Instance.GetCareerIcos(career);

        //清理之前的图标
        UIManager.instance.FindUIBehaviourByName("Content@", "SelectHeroPanel%").ClearGridLayoutGroupChirld();

        //遍历添加新图标
        for (int i = 0; i < icons.Length; i++)
        {
            //GameObject icon = ObjectPool.Instance.GetObj(icons[i]);
            GameObject icon = AB.Instance.LoadGameObjectByFilePathAndInstantiate(icons[i]);

            UIManager.instance.FindUIBehaviourByName("Content@", "SelectHeroPanel%").AddGridLayoutGroupChirld(icon);
        }

        ToggleGroupChild.intence.FindAndSetToggle();

    }
}
