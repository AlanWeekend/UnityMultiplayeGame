using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;
using System;
using Photon.Pun;


public class PlayerAnimatorAttackEvent : MonoBehaviour
{

    PhotonView phv;
    PlayerBlood pb;
    GameObject bullet;

    private void Awake()
    {
        phv = GetComponent<PhotonView>();
    }
    private void Start()
    {
    }
    /// <summary>
    /// 帧事件绑定的方法
    /// </summary>
    public void Fire()
    {

        bullet = ObjectPool.Instance.GetObj(DBA.Instance.GetBulletNameByWeaponName(GetComponent<PlayerAttack>().currentWeaponName));
        Debug.Log("我是" + gameObject.name + ",我进行了攻击");


        BulletClassType bulletType = bullet.GetComponent<BulletClassType>();
        if (bulletType != null)
        {
            bulletType.ViewID = phv.ViewID;
        }
        Debug.Log(bullet);
        BulletCSharp bulletCsharp = bullet.GetComponent<BulletCSharp>();

        if (bulletCsharp != null)
        {
            bullet.transform.position = gameObject.transform.position;
            Destroy(bulletCsharp);
        }

        BulletCSharp bulletCSharp = bullet.AddComponent<BulletCSharp>();
        bulletCSharp.BAwake(gameObject.transform);


    }
    private void OnTriggerEnter(Collider other)
    {

        if (!phv.IsMine)
            return;
        BulletClassType bulletClassType = other.GetComponent<BulletClassType>();

        if (bulletClassType != null && bulletClassType.ViewID != phv.ViewID)
        {
            //pb = GetComponent<PlayerBlood>();
            //pb.bloodValue -= 1;
            //发送扣血

            phv.RPC("reduceBlood", RpcTarget.AllBufferedViaServer, 1, phv.ViewID,bulletClassType.ViewID);

        }
    }

    /// <summary>
    /// 同步扣血
    /// </summary>
    /// <param name="bloodNum">扣除血量</param>
    /// <param name="id">viewid</param>
    [PunRPC]
    public void reduceBlood(int bloodNum, int id,int bulletViewID)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        //根据viewID查找目标玩家
        for (int i = 0; i < players.Length; i++)
        {
            //找到目标玩家
            if (players[i].GetComponent<PhotonView>().ViewID == id)
            {
                players[i].GetComponent<PlayerBlood>().SetBlood(bloodNum,bulletViewID);
                break;
            }
        }
        //if (phv.ViewID != id) return;

        //pb = GetComponent<PlayerBlood>();
        //pb.bloodValue -= bloodNum;
        //pb

    }

}











































//LuaState lua = null;
///// <summary>
///// 实例化luastate
///// </summary>
//public void LuaStart()
//{
//    lua = new LuaState();
//    lua.Start();
//}

//private void Start()
//{
//    LuaStart();
//    FindLua();
//}


///// <summary>
///// 找到lua的路径
///// </summary>
//public void FindLua()
//{
//    string fullPath = Application.dataPath + "/LuaTest";
//    lua.AddSearchPath(fullPath);
//}
///// <summary>
///// 找到路径下的lua文件的名字
///// </summary>
//private void DoFile()
//{
//    lua.DoFile(PlayerAttack.Instance.currentWeaponName + ".lua");

//    lua.Collect();
//    lua.CheckTop();
//}
///// <summary>
///// 游戏退出的执行
///// </summary>
//private void OnApplicationQuit()
//{

//    lua.Dispose();
//    lua = null;

//}
//}



