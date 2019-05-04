using UnityEngine;
using LuaInterface;
using System;

public class HelloWorld : MonoBehaviour
{
    void Awake()
    {
        //实例化LuaState对象
        LuaState lua = new LuaState();
        //初始化LuaState
        lua.Start();
        string hello =
            @"                
                print('hello tolua#')

            ";
        //执行一个字符串类型的lua代码
        lua.DoString(hello, "HelloWorld.cs");
        lua.CheckTop();
        //释放内存
        lua.Dispose();
        //释放luaState对象
        lua = null;
    }
}
