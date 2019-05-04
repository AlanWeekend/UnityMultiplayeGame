using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;
//此脚本是用于单例的lua调用武器方法
public class BulletController : MonoBehaviour
{


    Transform player;
    string Name;

    bool StartUpdate;
    
    /// <summary>
    /// 初始
    /// </summary>
    /// <param name="go">玩家</param>
    public void Init(Transform go)
    {
        player = go;
        string luaName = DBA.Instance.GetBulletNameByWeaponName(player.GetComponent<PlayerAttack>().currentWeaponName);

        Name = luaName;
        LUATEST.Instance.ExecLua(luaName, luaName + ".Awake", player, gameObject);
        LuaStart(luaName);
       // player.GetComponent<Rigidbody>().AddForce()
       
    }

    void LuaStart(string luaName)
    {
        LUATEST.Instance.ExecLua(luaName, luaName + ".Start");
        LuaUpdateStart();
            }

    void LuaUpdateStart()
    {
        StartUpdate = true;
        //StartCoroutine(Rec());

    }


    private void FixedUpdate()
    {


        if (!StartUpdate)
            return;
        LUATEST.Instance.ExecLua(Name, Name + ".Update");
        //gameObject.AddComponent(
        //gameObject.GetType();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("zhixing");
        LUATEST.Instance.ExecLua(Name, Name + ".OntriggerEnter",other.gameObject);
        //gameObject.
        //gameObject.GetType()

    }












    //IEnumerator Rec()
    //{
    //    yield return new WaitForSecondsRealtime(1000f);
    //    ObjectPool.Instance.RecyleObj(gameObject);
    //}






}
//public void Init(Transform go)
//{
//    player = go;
//    Debug.Log(DBA.Instance.GetBulletNameByWeaponName(player.GetComponent<PlayerAttack>().currentWeaponName) + ".lua");
//    new LuaResLoader();
//    lua = new LuaState();
//    lua.Start();
//    LuaBinder.Bind(lua);
//    GetLuaPath();
//    lua.Require("prefab_arrow");
//    lua.DoFile(DBA.Instance.GetBulletNameByWeaponName(player.GetComponent<PlayerAttack>().currentWeaponName) + ".lua");
//    //  CallFunc("prefab_arrow.Awake", player);
//    Debug.Log(player);
//}



//void SendBullet()
//{

//}

//void SendPlayer()
//{

//}

//void CallFunc(string func, Transform go)
//{
//    Debug.Log(go);
//    luaFunc = lua.GetFunction(func);
//    Debug.Log(luaFunc.ToString() + "123123123123123");

//    luaFunc.Call(this.gameObject, go);
//   StartCoroutine( Rec());
//}





//void GetLuaPath()
//{
//    string Path = Application.dataPath + "/lybBulletLua";
//    lua.AddSearchPath(Path);
//}
