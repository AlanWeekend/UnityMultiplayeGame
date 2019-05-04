using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinToggle : MonoBehaviour {

    Toggle toggle;
    private GameObject heroShowPoint;
    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        heroShowPoint = GameObject.Find("HeroShowPoint");
    }
    private void Start()
    {
        toggle.onValueChanged.AddListener(delegate { FindAndSetModule(); });
    }

    public void FindAndSetModule()
    {
        
        if (toggle.isOn==true)
        {
            for (int i = 0; i < heroShowPoint.transform.childCount; i++)
            {
                //heroShowPoint.transform.GetChild(i)
                if (heroShowPoint.transform.GetChild(i).gameObject.activeSelf == true)
                   Destroy(heroShowPoint.transform.GetChild(i).gameObject);
            }
            GameObject heroModule = ObjectPool.Instance.GetObj(name.Substring(gameObject.name.IndexOf('_') + 1));
            PlayerAttack pa = heroModule.GetComponent<PlayerAttack>();
            PlayerMove pm = heroModule.GetComponent<PlayerMove>();
            Destroy(pa);
            Destroy(pm);
            heroModule.AddComponent<ShowHeroInit>();
            Debug.Log(name.Substring(gameObject.name.IndexOf('_') + 1));
            SelectHero.heroName = name.Substring(gameObject.name.IndexOf('_') + 1);
            Debug.Log(SelectHero.heroName);
        }
    }
}
