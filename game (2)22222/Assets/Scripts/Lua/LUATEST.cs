#region 模块信息
// **********************************************************************
// Copyright (C) 2017 The company name
//
// 文件名(File Name):             LUATEST.cs
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
using LuaInterface;

public class LUATEST {

    #region SingleTon
    private static LUATEST instance;
    public static LUATEST Instance{
        get{
            if(instance==null) instance=new LUATEST();
            return instance;
        }
    }
    #endregion

    LuaState lua = null;

    private LUATEST()
    {
        new LuaResLoader();
        lua = new LuaState();
        lua.Start();
        LuaBinder.Bind(lua);
        lua.AddSearchPath(Application.streamingAssetsPath+"\\Lua");
    }

    /// <summary>
    /// 执行lua方法
    /// </summary>
    /// <param name="luaFile">lua 文件名</param>
    /// <param name="luaFunc">lua 方法名</param>
    /// <param name="pars">参数</param>
    public void ExecLua(string luaFile,string luaFunc,params object[] pars)
    {
        lua.Require(luaFile);
        LuaFunction luafun = lua.GetFunction(luaFunc);
        try{
            luafun.Call(pars);
        }catch{
            Debug.LogErrorFormat("{0}脚本找不到{1}方法",luaFile,luaFunc);
        }
    }

}
