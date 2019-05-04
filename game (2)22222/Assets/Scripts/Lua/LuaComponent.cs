#region 模块信息
// **********************************************************************
// Copyright (C) 2017 The company name
//
// 文件名(File Name):             LuaComponent.cs
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
using LuaInterface;

public class LuaComponent : MonoBehaviour {

    //lua环境，需要在使用前给其赋值
    public LuaState s_luaState;

    protected static class FuncName{
        public const string Awake = "Awake";
        public const string OnEnable = "OnEnable";
        public const string Start = "Start";
        public const string Update = "Update";
        public const string OnDisable = "OnDisable";
        public const string OnDestroy = "OnDestroy";
    }

    //lua路径，不用填名，可以是bundle
    [Tooltip("Script path")]
    public string LuaPath = "Bullet.lua";

    //预存函数提高效率
    protected Dictionary<string,LuaFunction> mDicFunc = new Dictionary<string, LuaFunction>();

    //lua表，当gameObject销毁时需要释放
    public LuaTable mSelfTable = null;

    //初始化函数，可以被重写，已添加其他
    protected bool Init()
    {
        // s_luaState = LuaTest.Instance.GetLuaState();
        new LuaResLoader();
        s_luaState = new LuaState();
        s_luaState.Start();
        LuaBinder.Bind(s_luaState);
        s_luaState.AddSearchPath(Application.streamingAssetsPath+"\\Lua");


        if(string.IsNullOrEmpty(LuaPath))
        {
            Debug.Log(LuaPath);
            return false;
        }
        s_luaState.DoFile(LuaPath);
        // mSelfTable = s_luaState.GetTable(0);
        // mSelfTable = s_luaState.GetTable(LuaPath.Replace(".lua",""));
        // mSelfTable = s_luaState.GetTable("Bullet");
        LuaFunction func = s_luaState.GetFunction("GetTable");
        func.BeginPCall();
        func.PCall();
        mSelfTable=func.CheckLuaTable();
        func.EndPCall();

        if(null==mSelfTable)
        {
            Debug.LogError("null == luaTable "+LuaPath);
            return false;
        }
        AddFunc(FuncName.Awake);
        AddFunc(FuncName.OnEnable);
        AddFunc(FuncName.Start);
        AddFunc(FuncName.Update);
        AddFunc(FuncName.OnDisable);
        AddFunc(FuncName.OnDestroy);

        foreach (var item in mDicFunc)
        {
            Debug.Log("------------------" + item);
        }
        return true;
    }

    //保存函数
    protected bool AddFunc(string name)
    {
        // string funcName = LuaPath.Replace(".lua",":")+name;
        var func = mSelfTable.GetLuaFunction(name);
        if(null==func) 
        {
            return false;
        }
        mDicFunc.Add(name,func);
        return true;
    }

    //调用函数
    protected void CallLuaFunction(string name,params object[] args)
    {
        LuaFunction func=null;
        if(mDicFunc.TryGetValue(name,out func))
        {
            func.BeginPCall();
            func.Push(mSelfTable);
            foreach (var item in args)
            {
                func.Push(item);
            }
            func.PCall();
            func.EndPCall();
        }else
        {
            // Debug.Log("没有找到方法");
        }
    }

    public void LuaInit()
    {
        Init();
        CallLuaFunction(FuncName.Awake, mSelfTable, gameObject);
    }

    private void OnEnable() {
        CallLuaFunction(FuncName.OnEnable,mSelfTable,gameObject);
    }

    void Start () {
        CallLuaFunction(FuncName.Start,mSelfTable,gameObject);
    }

    void Update () {
        CallLuaFunction(FuncName.Update,gameObject,mSelfTable);
    }

    private void OnDisable() {
        CallLuaFunction(FuncName.OnDisable,mSelfTable,gameObject);
    }

    private void OnDestroy() {
        CallLuaFunction(FuncName.OnDestroy,mSelfTable,gameObject);
        //记得释放资源
        foreach (var item in mDicFunc)
        {
            item.Value.Dispose();
        }
        mDicFunc.Clear();
        if(null!=mSelfTable)
        {
            mSelfTable.Dispose();
            mSelfTable=null;
        }
    }
}