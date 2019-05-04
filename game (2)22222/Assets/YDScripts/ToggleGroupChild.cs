using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleGroupChild : MonoBehaviour
{
    public static ToggleGroupChild intence;
    private GameObject heroShowPoint;
    private ToggleGroup toggleGroup;
    private Toggle[] toggleChild;

    private void Awake()
    {
        intence = this;
        toggleGroup = GetComponent<ToggleGroup>();
        heroShowPoint = GameObject.Find("HeroShowPoint");
    }

    private void Start()
    {
        toggleChild = transform.GetComponentsInChildren<Toggle>();
    }

    /// <summary>
    /// 讲子物体加入到togglegroup
    /// </summary>
    public void FindAndSetToggle()
    {

        //string[] skins = DBA.Instance.GetCareerIcos(skinName);
        toggleChild = transform.GetComponentsInChildren<Toggle>();
        
        for (int i = 0; i < toggleChild.Length; i++)
        {
            toggleChild[i].gameObject.AddComponent<SkinToggle>();
            toggleChild[i].group = toggleGroup;
        }
    }
}
