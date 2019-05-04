#region 模块信息
// **********************************************************************
// Copyright (C) 2017 The company name
//
// 文件名(File Name):             ObjectPool.cs
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
using System.Collections.Generic;
using System;

/// <summary>
/// 对象池
/// </summary>
public class ObjectPool {

    public Action Init;

    //对象池
    private Dictionary<string,Queue<GameObject>> pool;
    //预制体字典
    private Dictionary<string,GameObject> prefabDic;

    #region SingleTon Lazyload
    private static ObjectPool instance;
    public static ObjectPool Instance{
        get{
            if(instance==null) instance=new ObjectPool();
            return instance;
        }
    }
    #endregion

    #region Constructor
    private ObjectPool()
    {
        pool = new Dictionary<string, Queue<GameObject>>();
        prefabDic =new Dictionary<string, GameObject>();
    }
    #endregion

    #region Functions
    /// <summary>
    /// 从对象池获取游戏对象
    /// </summary>
    /// <param name="objName">对象名</param>
    /// <returns></returns>
    public GameObject GetObj(string objName)
    {
        GameObject obj;
        //没有加载该预设体
        if(!prefabDic.ContainsKey(objName))
        {
            //prefabDic.Add(objName, Resources.Load<GameObject>("Prefabs/" + objName

            prefabDic.Add(objName, AB.Instance.LoadGameObjectByFilePath(objName));
        }
        //没有改对象池
        if(!pool.ContainsKey(objName))
        {
            pool.Add(objName,new Queue<GameObject>());
        }
        //对象池没有该对象
        if(pool[objName].Count==0)
        {
            obj = GameObject.Instantiate(prefabDic[objName]);
        }
        else
        {
            obj = pool[objName].Dequeue();
            obj.SetActive(true);
        }
        //初始化
        obj.name = obj.name.Replace("(Clone)", "");
		if(Init!=null)Init();
        return obj;
    }

    /// <summary>
    /// 回收对象到对象池
    /// </summary>
    /// <param name="go">对象</param>
    public void RecyleObj(GameObject go)
    {
        go.SetActive(false);
        //if(!go.name.Contains("(Clone)")) return;
        //获取对象池名称
        //string poolName = go.name.Replace("(Clone)","");
        string poolName = go.name;
        poolName = poolName.ToLower();
        //如果还没有对象池，就创建一个对象池
        if (!pool.ContainsKey(poolName)) pool.Add(poolName, new Queue<GameObject>());
        Debug.Log("-----"+poolName+"--------" + go);
        pool[poolName].Enqueue(go);
    }

    /// <summary>
    /// 清空对象池子
    /// </summary>
    public void ClearObj()
    {
        foreach (var item in pool.Keys)
        {
            pool[item].Clear();
        }
    }
    #endregion
}