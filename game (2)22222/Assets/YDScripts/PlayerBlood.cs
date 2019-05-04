#region 模块信息
// **********************************************************************
// Copyright (C) 2017 The company name
//
// 文件名(File Name):             PlayerBlood.cs
// 公司(Company):  		 		  #COMPANY#
// 作者(Author):                  #AuthorName#
// 版本号(Version):		 		  #VERSION#
// Unity版本号(Unity Version):	  #UNITYVERSION#
// 创建时间(CreateTime):          #DATE#
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************
#endregion
using UnityEngine;
using System.Collections;
using Photon.Pun;


public class PlayerBlood : MonoBehaviour
{

    //血条
    public GameObject bBarGo;
    //血条脚本
    BloodBar bloodBar;

    //当前血量
    public int bloodValue;
    PhotonView pv;
    //血条画布
    GameObject canvas;
    //血条不显示的父物体
    GameObject notShowBloodFather;

    GameObject[] bloodBars;

    //是否开始计算血量
    bool isCalcu;
    //wan jia suo zai de cao shu liang
    public int grass = 0;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        //通知所有人给我生成一个血条
        if (pv.IsMine)
        {
            //从数据库获取最大血量
            bloodValue = int.Parse(DBA.Instance.GetCareerAttributeByName((gameObject.name.Remove(gameObject.name.IndexOf('0'))).ToLower())[0]);
            //发送RPC实例化血条并初始化
            pv.RPC("InstiateBloodBar", RpcTarget.AllBufferedViaServer, pv.ViewID, bloodValue, bloodValue, PhotonNetwork.NickName);
            //pv.RPC("SetMaxBloodValue", RpcTarget.AllBufferedViaServer, pv.ViewID,
            //    int.Parse(DBA.Instance.GetCareerAttributeByName((gameObject.name.Remove(gameObject.name.IndexOf('0'))).ToLower())[0]));

        }

    }

    private void FixedUpdate()
    {
        if (!isCalcu) return;
        //SetBlood(bloodValue);
        //SetMaxBlood(bloodNum);

    }

    /// <summary>
    /// 设置玩家名字
    /// </summary>
    /// <param name="name">名字</param>
    public void SetPlayerName(string name)
    {
        Debug.Log(bloodBar + "BloodBar");
        bloodBar.SetName(name);
    }

    /// <summary>
    /// 设置血量
    /// </summary>
    /// <param name="blood">血量</param>
    public void SetBlood(int blood,int bulletViewID)
    {
        StartCoroutine(FindBloodBar(blood, bulletViewID));
    }
    IEnumerator FindBloodBar(int blood,int bulletViewID)
    {
        while (bBarGo==null)
        {
            yield return 0;
        }
        bloodValue -= blood;
        bBarGo.GetComponent<BloodBar>().SetBlood(bloodValue);

        if (bloodValue <= 0 && pv.IsMine)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < players.Length; i++)
            {
                PhotonView photonView = players[i].GetComponent<PhotonView>();
                Debug.Log("子弹ViewID" + bulletViewID+" PV" +photonView.ViewID);
                if (photonView.ViewID == bulletViewID)
                {
                    pv.RPC("SetKillTip", RpcTarget.Others, photonView.Owner.NickName, PhotonNetwork.NickName);
                }
            }

            pv.RPC("DestroyBloodBar",RpcTarget.AllBufferedViaServer,pv.ViewID);
            PhotonNetwork.LeaveRoom();
        }
    }

    /// <summary>
    /// 生成击杀提示
    /// </summary>
    /// <param name="killerName"></param>
    /// <param name="killedName"></param>
    [PunRPC]
    public void SetKillTip(string killerName,string killedName)
    {
        KillCanvas.Instance.SetKillTip(killerName, killedName);
    }

    /// <summary>
    /// 销毁血条
    /// </summary>
    /// <param name="id"></param>
    [PunRPC]
    public void DestroyBloodBar(int id)
    {
        //Destroy();
        GameObject[] selfBbar = GameObject.FindGameObjectsWithTag("BloodBar");
        for (int i = 0; i < selfBbar.Length; i++)
        {
            if(selfBbar[i].GetComponent<BloodBar>().selfId == id)
            {
                Debug.Log(id+"--------------");
                Destroy(selfBbar[i]);
                break;
            }

        }
    }

    /// <summary>
    /// 设置最大血量
    /// </summary>
    /// <param name="maxBlood">最大血量</param>
    public void SetMaxBlood(int maxBlood)
    {
        bloodBar.SetMaxBlood(maxBlood);
    }



    /// <summary>
    /// 生成血条
    /// </summary>
    /// <param name="player">跟随的玩家</param>
    [PunRPC]
    public void InstiateBloodBar(int viewID, int maxBlood, int blood, string name)
    {
        StartCoroutine(GetBlood(viewID,
            maxBlood, blood,
            name));

    }

    /// <summary>
    /// 设置血量
    /// </summary>
    /// <param name="viewID"></param>
    /// <param name="name"></param>
    [PunRPC]
    public void SetMaxBloodValue(int id, int maxBlood)
    {

        bBarGo.GetComponent<BloodBar>().SetMaxBlood(maxBlood);

    }

    IEnumerator GetBlood(int viewID, int crtBlood, int maxBlood, string name)
    {
        while (GameObject.Find("BloodCanvas") == null)
        {
            yield return 0;
        }

        Debug.Log("收到" + viewID);
        //实例化血条
        GameObject go = Instantiate(Resources.Load<GameObject>("Blood"));

        //查找所有玩家
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        //根据viewID查找目标玩家
        for (int i = 0; i < players.Length; i++)
        {
            //找到目标玩家
            if (players[i].GetComponent<PhotonView>().ViewID == viewID)
            { 
                //设置血条跟随的玩家
                go.GetComponent<BloodBar>().player = players[i].transform;
                players[i].GetComponent<PlayerBlood>().bBarGo = go;
                //查找画布
                canvas = GameObject.Find("BloodCanvas");
                //把血条放在画布下
                go.transform.SetParent(canvas.transform);
                BloodBar bBar = go.GetComponent<BloodBar>();
                bBar.SetMaxBlood(maxBlood);
                bBar.SetBlood(crtBlood);
                bBar.SetName(name);
                bBar.selfId = viewID;
                players[i].GetComponent<PlayerBlood>().bloodValue = maxBlood;
                break;
            }
        }

        //实例化血条
        // bloodBarGo = Instantiate(Resources.Load<GameObject>("Blood"));
        //bloodBarGo = PhotonNetwork.Instantiate("Blood", Vector3.zero, Quaternion.identity);


        //pv.RPC("SetBloodBar",RpcTarget.AllBufferedViaServer, bloodBarGo.GetComponent<PhotonView>().ViewID);

    }

    //[PunRPC]
    //public void setNickName(string name,int id)
    //{
    //    if (pv.ViewID != id) return;
    //    SetPlayerName(name);
    //}



    //[PunRPC]
    //public void SetBloodBar(int id)
    //{
    //    StartCoroutine(SetBloodBarDelay(id));
    //}

    //IEnumerator SetBloodBarDelay(int id)
    //{
    //    while (canvas == null)
    //    {
    //        canvas = GameObject.Find("BloodCanvas");
    //        yield return 0;
    //    }

    //    bloodBars = GameObject.FindGameObjectsWithTag("BloodBar");
    //    for (int i = 0; i < bloodBars.Length; i++)
    //    {
    //        Debug.Log(bloodBars[i].GetComponent<PhotonView>().ViewID + "*************" + id);
    //        if (bloodBars[i].GetComponent<PhotonView>().ViewID == id)
    //        {
    //            bloodBar = bloodBars[i].GetComponent<BloodBar>();
    //            break;
    //        }
    //    }
    //    bloodBar.player = this.transform;
    //    bloodBar.transform.SetParent(canvas.transform);
    //}

    /// <summary>
    /// 发送RPC关闭我的血条
    /// </summary>
    /// <param name="isShow">是否显示血条</param>
    public void ShowBlood(bool isShow)
    {
        pv.RPC("RPCShowBlood",RpcTarget.AllBufferedViaServer,pv.ViewID,isShow);
    }

    /// <summary>
    /// 是否显示血条
    /// </summary>
    /// <param name="viewID">viewID</param>
    /// <param name="isShow">是否显示</param>
    [PunRPC]
    public void RPCShowBlood(int viewID, bool isShow)
    {
        //Not Close Self Blood
        //if (pv.IsMine && pv.ViewID == viewID)
        //{
        //    return;
        //}
        StartCoroutine(IEShowBlood(viewID, isShow));
    }

    IEnumerator IEShowBlood(int id,bool isShow)
    {
        //查找不显示时的父物体
        GameObject notShowBloodFather= GameObject.Find("BloodNotShow");
        while (notShowBloodFather == null)
        {
            notShowBloodFather = GameObject.Find("BloodNotShow");
            yield return 0;
        }
        GameObject canvas = GameObject.Find("BloodCanvas");
        //查找显示时的父物体
        while (canvas == null)
        {
            canvas = GameObject.Find("BloodCanvas");
            yield return 0;
        }
        //查找所有玩家
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        //遍历玩家
        for (int i = 0; i < players.Length; i++)
        {
            //找到指定的玩家
            if (players[i].GetComponent<PhotonView>().ViewID == id)
            {
                //找到指定玩家的血条
                GameObject b = players[i].GetComponent<PlayerBlood>().bBarGo;
                //设置血条的父物体
                Debug.Log("shoudaoblood" + isShow);
                b.transform.SetParent(isShow ? canvas.transform : notShowBloodFather.transform);
            }
        }
    }
}
