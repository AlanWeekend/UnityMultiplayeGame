using UnityEngine;
using System.Collections;
using LuaInterface;

public class TestGameObject: MonoBehaviour
{
    private string myluaScript =
        @"
            --获取游戏对象类型
            local GameObject = UnityEngine.GameObject
            local Input = UnityEngine.Input
            local Time = UnityEngine.Time
            local player = GameObject.CreatePrimitive(UnityEngine.PrimitiveType.Cube)
            player.name = 'Player'
            player:AddComponent(typeof(UnityEngine.Light))
            local meshrenderer = player:GetComponent(typeof(UnityEngine.MeshRenderer))
            meshrenderer.material.color = UnityEngine.Color.red

            function MoveCube()
                local hor = Input.GetAxis('Horizontal') 
                local ver = Input.GetAxis('Vertical') 

                player.transform.position = player.transform.position + Vector3(hor,ver,0) * Time.deltaTime * 3
            end
        ";

    private string script =
        @"                      
            -- 获取Color类
            local Color = UnityEngine.Color
            -- 获取GameObject类
            local GameObject = UnityEngine.GameObject
            -- 获取ParticleSystem类
            local ParticleSystem = UnityEngine.ParticleSystem 

            function OnComplete()
                print('OnComplete CallBack')
            end                       
            -- 相当于C# new GameObject('go')
            local go = GameObject('go')
            go:AddComponent(typeof(UnityEngine.MeshRenderer))
            local node = go.transform
            node.position = Vector3.one                  
            print('gameObject is: '..tostring(go))                 
            --go.transform:DOPath({Vector3.zero, Vector3.one * 10}, 1, DG.Tweening.PathType.Linear, DG.Tweening.PathMode.Full3D, 10, nil)
            --go.transform:DORotate(Vector3(0,0,360), 2, DG.Tweening.RotateMode.FastBeyond360):OnComplete(OnComplete)            
            GameObject.Destroy(go, 2)                  
            go.name = '123'
            --print('delay destroy gameobject is: '..go.name)                                           
        ";

    LuaState lua = null;
    LuaFunction luaFunction = null;

    void Start()
    {
#if UNITY_5 || UNITY_2017 || UNITY_2018
        Application.logMessageReceived += ShowTips;
#else
        Application.RegisterLogCallback(ShowTips);
#endif
        new LuaResLoader();
        lua = new LuaState();
        lua.LogGC = true;
        lua.Start();
        LuaBinder.Bind(lua);
        lua.DoString(myluaScript, "TestGameObject.cs");

        luaFunction = lua["MoveCube"] as LuaFunction;
    }

    void Update()
    {
        luaFunction.Call();

        lua.CheckTop();
        lua.Collect();        
    }

    string tips = "";
        
    void ShowTips(string msg, string stackTrace, LogType type)
    {
        tips += msg;
        tips += "\r\n";
    }

    void OnApplicationQuit()
    {        
        lua.Dispose();
        lua = null;
#if UNITY_5 || UNITY_2017 || UNITY_2018
        Application.logMessageReceived -= ShowTips;
#else
        Application.RegisterLogCallback(null);
#endif    
    }

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 - 300, Screen.height / 2 - 300, 600, 600), tips);
    }
}
