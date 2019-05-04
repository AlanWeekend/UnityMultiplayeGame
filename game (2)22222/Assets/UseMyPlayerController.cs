#region 模块信息
// **********************************************************************
// Copyright (C) 2019 QIANFENG EDUCATION
//
// 文件名(File Name):UseMyPlayerController.cs
// 公司(Company):#COMPANY#
// 作者(Author):#AUTHOR#
// 版本号(Version):#VERSION#
// Unity版本	(Unity Version):#UNITYVERSION#
// 创建时间(CreateTime):#DATE#
// 修改者列表(modifier):无
// 模块描述(Module description):UseMyPlayerController
// **********************************************************************
#endregion
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;

public class UseMyPlayerController : MonoBehaviour {

    Dictionary<int, MyPlayerController> map = new Dictionary<int, MyPlayerController>();
    
    void Test()
    {
        var result = map.GetEnumerator();
        while (result.MoveNext())
        {
            Debug.Log(result.Current.Value.playerName);
        }

    }

    string script =
        @"        
            -- lua func 参数是一个字典
            function TestDict(map)                        
                local iter = map:GetEnumerator() 
                
                while iter:MoveNext() do
                    local v = iter.Current.Value
                    print(v.playerName)                                
                end
            end";

            //    local flag, account = map:TryGetValue(1, nil)

            //    if flag then
            //        print('TryGetValue result ok: '..account.playerName)
            //    end

            //    local keys = map.Keys
            //    iter = keys:GetEnumerator()
            //    print('------------print dictionary keys---------------')
            //    while iter:MoveNext() do
            //        print(iter.Current)
            //    end
            //    print('----------------------over----------------------')

            //    local values = map.Values
            //    iter = values:GetEnumerator()
            //    print('------------print dictionary values---------------')
            //    while iter:MoveNext() do
            //        print(iter.Current.playerName)
            //    end
            //    print('----------------------over----------------------')                

            //    print('kick '..map[2].playerName)
            //    map:Remove(2)
            //    iter = map:GetEnumerator() 

            //    while iter:MoveNext() do
            //        local v = iter.Current.Value
            //        print('' name: '..v.playerName)                                
            //    end
            //end                        
        //";

    void Awake()
    {
#if UNITY_5 || UNITY_2017 || UNITY_2018
        Application.logMessageReceived += ShowTips;
#else
        Application.RegisterLogCallback(ShowTips);
#endif
        new LuaResLoader();
        map.Add(1, new MyPlayerController("Tom"));
        map.Add(2, new MyPlayerController("Mary"));
        map.Add(3, new MyPlayerController("Albert"));
        //Test();
        LuaState luaState = new LuaState();
        luaState.Start();
        BindMap(luaState);

        luaState.DoString(script, "UseMyPlayerController.cs");
        LuaFunction func = luaState.GetFunction("TestDict");
        func.BeginPCall();
        func.Push(map);
        func.PCall();
        func.EndPCall();

        func.Dispose();
        func = null;
        luaState.CheckTop();
        luaState.Dispose();
        luaState = null;
    }

    void OnApplicationQuit()
    {
#if UNITY_5 || UNITY_2017 || UNITY_2018
        Application.logMessageReceived -= ShowTips;
#else
        Application.RegisterLogCallback(null);
#endif        
    }

    string tips = "";

    void ShowTips(string msg, string stackTrace, LogType type)
    {
        tips += msg;
        tips += "\r\n";
    }

    enum enen
    {
        AAA, BBB, CCC
    }

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 - 300, Screen.height / 2 - 200, 600, 400), tips);
    }

    //示例方式，方便删除，正常导出无需手写下面代码
    void BindMap(LuaState L)
    {
        L.BeginModule(null);
        MyPlayerControllerWrap.Register(L);
        L.BeginModule("System");
        L.BeginModule("Collections");
        L.BeginModule("Generic");
        System_Collections_Generic_Dictionary_int_MyPlayerControllerWrap.Register(L);
        System_Collections_Generic_KeyValuePair_int_MyPlayerControllerWrap.Register(L);
        L.BeginModule("Dictionary");
        System_Collections_Generic_Dictionary_int_MyPlayerController_KeyCollectionWrap.Register(L);
        System_Collections_Generic_Dictionary_int_MyPlayerController_ValueCollectionWrap.Register(L);
        L.EndModule();
        L.EndModule();
        L.EndModule();
        L.EndModule();
        L.EndModule();
    }

}