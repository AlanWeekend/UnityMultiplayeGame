using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHeroInit : MonoBehaviour {

    private void Awake()
    {
        //ObjectPool.Instance.Init = Init;
        Init();
    }

    public void Init()
    {
        if (GetComponent<Rigidbody>()!=null)
        {
            GetComponent<Rigidbody>().useGravity = false;
        }

        transform.SetParent(GameObject.Find("HeroShowPoint").transform);
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
        transform.localScale = Vector3.one;
    }

}
