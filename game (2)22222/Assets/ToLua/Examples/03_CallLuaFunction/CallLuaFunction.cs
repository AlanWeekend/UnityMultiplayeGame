using UnityEngine;
using System.Collections;
using LuaInterface;
using System;

public class CallLuaFunction : MonoBehaviour 
{
    //lua 代码
    private string script =
        @"  function luaFunc(num)                        
                return num + 1
            end

            test = {}
            test.luaFunc = luaFunc
        ";

    LuaFunction luaFunc = null;
    LuaState lua = null;
    string tips = null;
	
	void Start () 
    {
#if UNITY_5 || UNITY_2017 || UNITY_2018
        Application.logMessageReceived += ShowTips;
#else
        Application.RegisterLogCallback(ShowTips);
#endif
        new LuaResLoader();
        lua = new LuaState();
        lua.Start();
        //委托工厂对象初始化
        DelegateFactory.Init(); 
        //先执行lua代码       
        lua.DoString(script);

        //Get the function object
        luaFunc = lua.GetFunction("test.luaFunc");

        if (luaFunc != null)
        {

            //通过方法对象封装的Invoke执行方法
            int num = luaFunc.Invoke<int, int>(123456);
            Debugger.Log("generic call return: {0}", num);

            //通过方法对象原生的执行模式执行方法
            num = CallFunc();
            Debugger.Log("expansion call return: {0}", num);

            //通过委托工厂执行函数(需要提前执行工厂初始化)
            //将lua中的function转换为C#委托对象
            Func<int, int> Func = luaFunc.ToDelegate<Func<int, int>>();
            num = Func(123456);
            Debugger.Log("Delegate call return: {0}", num);

            //通过luaState对象的Invoke执行方法
            num = lua.Invoke<int, int>("test.luaFunc", 123456, true);
            Debugger.Log("luastate call return: {0}", num);
        }

        lua.CheckTop();
	}

    void ShowTips(string msg, string stackTrace, LogType type)
    {
        tips += msg;
        tips += "\r\n";
    }

#if !TEST_GC
    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 150, 400, 300), tips);
    }
#endif

    void OnDestroy()
    {
        if (luaFunc != null)
        {
            luaFunc.Dispose();
            luaFunc = null;
        }

        lua.Dispose();
        lua = null;

#if UNITY_5 || UNITY_2017 || UNITY_2018
        Application.logMessageReceived -= ShowTips;
#else
        Application.RegisterLogCallback(null);
#endif
    }

    int CallFunc()
    {        
        //lua方法对象开始准备
        luaFunc.BeginPCall();
        //lua方法对象push一个参数                
        luaFunc.Push(123456);
        //lua方法对象调用
        luaFunc.PCall();       
        //lua方法对象通过Check(数据类型)返回方法执行的结果
        int num = (int)luaFunc.CheckNumber();
        //lua方法对象结束(收尾工作)
        luaFunc.EndPCall();
        return num;                
    }
}
