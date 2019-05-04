using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WeaponAtrribute : MonoBehaviour
{

    PhotonView pv;

    public int WeaponType;
    public string WeaponName;
    public HandType Weaponhandtype;


    private void Awake()
    {
        if (GetComponent<PhotonView>() != null)
        {
            pv = GetComponent<PhotonView>();
        }
        name = gameObject.name.Replace("(Clone)","");
        WeaponType = DBA.Instance.GetWeaponTypeByName(gameObject.name);
        Debug.Log(WeaponType + "武器类型");
        if (WeaponType == 1 || WeaponType == 2)
        {
            Weaponhandtype = HandType.Right;
        }
        if (WeaponType == 3 || WeaponType == 4)
        {
            Weaponhandtype = HandType.Left;
        }
    }






    /// <summary>
    /// 别的玩家生成的武器设置为他爹
    /// </summary>
    /// <param name="id"></param>
    public void SetFather(int id)
    {
        pv.RPC("WeaponAtrributeRPC", RpcTarget.AllBuffered, id);
    }


    [PunRPC]
    public void WeaponAtrributeRPC(int id)
    {
        GameObject go;
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < player.Length; i++)
        {
            if (player[i].GetComponent<PhotonView>().ViewID == id)
            {
                go = player[i];
                HandOfPlayer[] hand = go.transform.GetComponentsInChildren<HandOfPlayer>();
                foreach (var item in hand)
                {
                    //if (Weaponhandtype == HandType.Left && item.handType == HandType.Left)
                    //{
                    //    transform.SetParent(item.transform);
                    //    transform.localPosition = Vector3.zero;
                    //    transform.localEulerAngles = Vector3.zero;

                    //}
                    //else if (Weaponhandtype == HandType.Right && item.handType == HandType.Right)
                    //{
                    //    transform.SetParent(item.transform);
                    //    transform.localPosition = Vector3.zero;
                    //    transform.localEulerAngles = Vector3.zero;

                    //}

                    if (item.handType == HandType.Right)
                    {
                        if (Weaponhandtype == HandType.Right)
                        {
                            Debug.Log("我的武器类型是：" + WeaponType + ",我放在了右手上");
                            transform.SetParent(item.transform);
                            transform.localPosition = Vector3.zero;
                            transform.localEulerAngles = Vector3.zero;

                        }
                    }
                    else if (item.handType == HandType.Left)
                    {
                        if (Weaponhandtype == HandType.Left)
                        {
                            Debug.Log("我的武器类型是：" + WeaponType + ",我放在了左手上");
                            transform.SetParent(item.transform);
                            transform.localPosition = Vector3.zero;
                            transform.localEulerAngles = Vector3.zero;
                        }

                    }
                }
                // transform.SetParent(go.transform.GetComponentsInChildren<HandOfPlayer>());
            }
        }
        PhotonTransformView ptv = GetComponent<PhotonTransformView>();


    }










}
