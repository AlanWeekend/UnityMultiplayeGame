using UnityEngine;
using System.Collections.Generic;
using LuaInterface;

public class AccessingLuaVariables : MonoBehaviour 
{
    private string script =
        @"
            print('Objs2Spawn is: '..Objs2Spawn)
            var2read = 42
            varTable = {1,2,3,4,5}
            varTable.default = 1
            varTable.map = {}
            varTable.map.name = 'map'
            
            meta = {name = 'meta'}
            setmetatable(varTable, meta)

            func = function ()
                print('variable function')
            end
            
            function TestFunc(strs)
                print('get func by variable')
            end
        ";

	void Start () 
    {
#if UNITY_5 || UNITY_2017 || UNITY_2018
        Application.logMessageReceived += ShowTips;
#else
        Application.RegisterLogCallback(ShowTips);
#endif
        new LuaResLoader();
        LuaState lua = new LuaState();
        lua.Start();
        //--------用C#给lua变量赋值---------
        //使用luaState定义一个变量并赋值
        lua["Objs2Spawn"] = 5;//"Objs2Spawn = 5"
        //此时执行代码是在定义变量并赋值的基础之上来执行
        lua.DoString(script);

        //通过LuaState访问
        //通过C#去访问lua中变量
        Debugger.Log("Read var from lua: {0}", lua["var2read"]);
        Debugger.Log("Read table var from lua: {0}", lua["varTable.default"]);  //LuaState 拆串式table
        //用luafunction对象获取lua代码中的函数
        LuaFunction func = lua["TestFunc"] as LuaFunction;
        //调用函数
        func.Call();
        func.Dispose();

        //cache成LuaTable进行访问
        //获取lua代码中表格
        LuaTable table = lua.GetTable("varTable");
        //在访问表格字段的值时，只能访问单个字段，不能访问字段的字段
        Debugger.Log("Read varTable from lua, default: {0} name: {1}", table["default"], table["map.name"]);
        //但是可以给字段的字段赋值
        table["map.name"] = "new";  //table 字符串只能是key
        //赋值之后就可以访问了
        Debugger.Log("Modify varTable name: {0}", table["map.name"]);

        table.AddTable("newmap");//--varTable.newmap={}
        //获取到表格中的该字段，该字段是一个表格
        LuaTable table1 = (LuaTable)table["newmap"];
        //给newmap表格设置的一个字段name:table1
        table1["name"] = "table1";
        Debugger.Log("varTable.newmap name: {0}", table1["name"]);
        //table1释放了
        table1.Dispose();
        //获取表格的元表格，此时table1是元表格
        table1 = table.GetMetaTable();

        if (table1 != null)
        {
            Debugger.Log("varTable metatable name: {0}", table1["name"]);
        }
        //将表格转换为C#中的数组
        object[] list = table.ToArray();

        for (int i = 0; i < list.Length; i++)
        {
            Debugger.Log("varTable[{0}], is {1}", i, list[i]);
        }

        table.Dispose();                        
        lua.CheckTop();
        lua.Dispose();
	}

    private void OnApplicationQuit()
    {
#if UNITY_5 || UNITY_2017 || UNITY_2018
        Application.logMessageReceived -= ShowTips;
#else
        Application.RegisterLogCallback(null);
#endif
    }

    string tips = null;

    void ShowTips(string msg, string stackTrace, LogType type)
    {
        tips += msg;
        tips += "\r\n";
    }

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 - 300, Screen.height / 2 - 200, 600, 400), tips);
    }
}
