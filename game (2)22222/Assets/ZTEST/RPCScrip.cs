using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RPCScrip : MonoBehaviour
{
    HandOfPlayer[] handOfPlayer;


    PhotonView phv;
    PhotonAnimatorView pva;

    private void Awake()
    {
        phv = PhotonView.Get(GetComponent<PhotonView>());


    }

    public void SwitchMyWeapon(int id, string weapon)
    {
        Debug.Log("= viewid"+id);

        phv.RPC("RPCMethod", RpcTarget.OthersBuffered, id, weapon);
    }



    [PunRPC]
    public void RPCMethod(int viewId,string weapon)
    {
        Debug.Log(viewId+"我的viewid"+gameObject.name);
        GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");

        Debug.Log("玩家数量"+Players.Length);
        GameObject go = null;
        foreach (var item in Players)
        {
            if (item.GetComponent<PhotonView>().ViewID == viewId)
            {
                go = item;
                Debug.Log(go+"-----------------");
          //      Debug.Log(go.name);
            }
        }
        if (go != null)
        {
            GameObject wp;
            handOfPlayer = go.transform.GetComponentsInChildren<HandOfPlayer>();
            for (int i = 0; i < handOfPlayer.Length; i++)
            {
                if (handOfPlayer[i].transform.Find("WeaponPoint").childCount == 0)
                    continue;
                wp = handOfPlayer[i].transform.Find("WeaponPoint").GetChild(0).gameObject;
                Debug.Log("要被销毁的武器名字"+wp.name);
                Destroy(wp);
            }

        }
    }
















}
//GameObject p =  PhotonNetwork.Instantiate(name, pos, Quaternion.identity);
//  p.tag = "Player";
// phv = p.GetComponent<PhotonView>();


//  //GameObject player = ObjectPool.Instance.GetObj(name);
//   p.GetComponent<PhotonView>().ViewID = id;
//  //player.AddComponent<PhotonView>();
//  //phv = player.AddComponent<PhotonView>();

//  //PhotonNetwork.Instantiate()

//  //player.GetComponent<PhotonView>().ObservedComponents.Add(GetComponent<Transform>());
//  //player.GetComponent<PhotonView>().ObservedComponents.Add(GetComponent<Animator>()); GetComponent<PhotonAnimatorView>();
//  pva.SetLayerSynchronized(0, PhotonAnimatorView.SynchronizeType.Continuous);
//  pva.SetLayerSynchronized(1, PhotonAnimatorView.SynchronizeType.Continuous);
//  pva.SetParameterSynchronized("Speed", PhotonAnimatorView.ParameterType.Float, PhotonAnimatorView.SynchronizeType.Continuous);
//  pva.SetParameterSynchronized("AttackMode", PhotonAnimatorView.ParameterType.Float, PhotonAnimatorView.SynchronizeType.Continuous);
//  pva.SetParameterSynchronized("OpenFire", PhotonAnimatorView.ParameterType.Trigger, PhotonAnimatorView.SynchronizeType.Continuous);
