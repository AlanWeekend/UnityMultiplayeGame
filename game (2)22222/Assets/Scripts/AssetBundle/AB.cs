#region 模块信息
// **********************************************************************
// Copyright (C) 2017 The company name
//
// 文件名(File Name):             AB.cs
// 公司(Company):  		 		  #COMPANY#
// 作者(Author):                  #AuthorName#
// 版本号(Version):		 		  #VERSION#
// Unity版本号(Unity Version):	  #UNITYVERSION#
// 创建时间(CreateTime):          #DATE#
// 修改者列表(modifier):
// 模块描述(Module description):
// **********************************************************************
#endregion
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;
using System.Security.Cryptography;

public class AB : MonoBehaviour
{

    #region SingleTon
    public static AB Instance { get; private set; }
    #endregion

    #region Field
    //服务器地址
    private string path = "game.yongshen.me";
    //下载对象
    private WWW www;
    //状态文本
    private Text statusText;
    //进展文本
    private Text progressText;
    //进度条
    private Slider slider;
    //加载完毕回调
    private Action callback;
    #endregion

    #region Mono Callback

    private void Awake()
    {
        Instance = this;
    }

    private void Update () {
        //更新进度条
        if (www != null && !www.isDone) {
            slider.value = www.progress;
        }
    }

    #endregion

    #region Functions
    /// <summary>
    /// 初始化资源
    /// </summary>
    /// <param name="statusText">状态文本</param>
    /// <param name="progressText">进度文本</param>
    /// <param name="slider">进度条</param>
    /// <param name="action">加载完回调</param>
    public void Init(Text statusText, Text progressText, Slider slider, Action action)
    {
        this.statusText = statusText;
        this.progressText = progressText;
        this.slider = slider;
        this.callback = action;
        //开启协程
        StartCoroutine(CheckVersion());
    }

    /// <summary>
    /// 检测版本
    /// </summary>
    /// <returns></returns>
    private IEnumerator CheckVersion()
    {
        //设置UI
        SetText("获取服务器信息", "");
        //获取服务器版本
        www = new WWW(path + "/ab.json");
        yield return www;

        Abjson abjson = JsonMapper.ToObject<Abjson>(www.text);

        if(abjson == null)
        {
            SetText("服务器连接失败","");
        }

        //验证本地资源文件
        for (int i = 0; i < abjson.ablist.Length; i++)
        {
            ABlist aBlist = abjson.ablist[i];
            string endStr = aBlist.ABName.Substring(aBlist.ABName.IndexOf('.') + 1);
            string dir = ABConst.assetBundlePath;
            if (endStr == "db" || endStr == "lua")
            {
                Type type = typeof(ABConst);
                dir = type.GetField(endStr + "Path").GetValue(null).ToString();
            }
            string localPath = Application.streamingAssetsPath + dir + aBlist.ABName;

            SetText("验证文件", aBlist.ABName);
            //本地存在文件
            if (File.Exists(localPath)&& aBlist.md5.Equals(GetMD5(localPath)))
            {
                yield return 0;
            }
            //本地不存在文件或文件已经更新
            else
            {
                SetText("下载文件", aBlist.ABName);
                www = new WWW(aBlist.url);
                yield return www;
                File.WriteAllBytes(localPath, www.bytes);
            }
            //更新进度条
            this.slider.value = (float)(i + 1) / (float)abjson.ablist.Length;
        }
        SetText("加载完毕", "");
        if (callback != null) callback();
    }

    /// <summary>
    /// 计算文件的md5
    /// </summary>
    /// <param name="path">文件路径</param>
    private string GetMD5(string path)
    {
        if (!File.Exists(path))
            throw new ArgumentException(string.Format("<{0}>,不存在",path));
        int bufferSize = 1024 * 16; //自定义缓冲区大小
        byte[] buffer = new byte[bufferSize];
        Stream inputStream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        HashAlgorithm hashAlgorithm = new MD5CryptoServiceProvider();
        int readLength = 0;//每次读取长度
        var output = new byte[bufferSize];
        while ((readLength = inputStream.Read(buffer, 0, buffer.Length)) > 0)
        {
            //计算MD5
            hashAlgorithm.TransformBlock(buffer, 0, readLength, output, 0);
        }
        hashAlgorithm.TransformFinalBlock(buffer, 0, 0);
        string md5 = BitConverter.ToString(hashAlgorithm.Hash);
        hashAlgorithm.Clear();
        inputStream.Close();
        md5 = md5.Replace("-", "");
        md5 = md5.ToLower();
        Debug.Log(md5);
        return md5;
    }

    /// <summary>
    /// 设置UI文本
    /// </summary>
    /// <param name="status">状态</param>
    /// <param name="progress">进度</param>
    private void SetText(string status, string progress)
    {
        statusText.text = status;
        progressText.text = progress;
    }

    /// <summary>
    /// 根据名字加载AB中的资源文件
    /// </summary>
    /// <returns></returns>
    public GameObject LoadGameObjectByFilePath(string bundleName)
    {
        //AssetBundle目录名
        string bundelDir = "AssetBundle";
        //AssetBundle路径
        string bundlePath = Application.streamingAssetsPath + "/" + bundelDir + "/";
        //获取Manifest文件的AB
        AssetBundle manifest = AssetBundle.LoadFromFile(bundlePath + bundelDir);
        //创建AssetManifest对象
        AssetBundleManifest assetBundleManifest = manifest.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        //清理Manifest的Bundle
        manifest.Unload(false);
        //获取要加载的资源的所有依赖文件列表
        string[] dependenciesStr = assetBundleManifest.GetAllDependencies(bundleName);
        //声明所有要加载的依赖Bundle
        AssetBundle[] dependenciesBundle = new AssetBundle[dependenciesStr.Length];
        //获取所有的依赖bundle
        for (int i = 0; i < dependenciesBundle.Length; i++)
        {   
            
            dependenciesBundle[i] = AssetBundle.LoadFromFile(bundlePath + dependenciesStr[i]);
        }
        //获取真正的Bundle
        AssetBundle realBundle = AssetBundle.LoadFromFile(bundlePath + bundleName);
        //加载bundle中的对象
        GameObject go = realBundle.LoadAsset<GameObject>(bundleName);
        // 清理realBundle
        realBundle.Unload(false);
        for (int i = 0; i < dependenciesBundle.Length; i++)
        {
            dependenciesBundle[i].Unload(false);
        }
        return go;
    }

        /// <summary>
    /// 根据名字加载AB中的资源文件并实例化
    /// </summary>
    /// <returns></returns>
    public GameObject LoadGameObjectByFilePathAndInstantiate(string bundleName)
    {
        //AssetBundle目录名
        string bundelDir = "AssetBundle";
        //AssetBundle路径
        string bundlePath = Application.streamingAssetsPath + "/" + bundelDir + "/";
        //获取Manifest文件的AB
        AssetBundle manifest = AssetBundle.LoadFromFile(bundlePath + bundelDir);
        //创建AssetManifest对象
        AssetBundleManifest assetBundleManifest = manifest.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        //清理Manifest的Bundle
        manifest.Unload(false);
        //获取要加载的资源的所有依赖文件列表
        string[] dependenciesStr = assetBundleManifest.GetAllDependencies(bundleName);
        //声明所有要加载的依赖Bundle
        AssetBundle[] dependenciesBundle = new AssetBundle[dependenciesStr.Length];
        //获取所有的依赖bundle
        for (int i = 0; i < dependenciesBundle.Length; i++)
        {   
            
            dependenciesBundle[i] = AssetBundle.LoadFromFile(bundlePath + dependenciesStr[i]);
        }
        //获取真正的Bundle
        AssetBundle realBundle = AssetBundle.LoadFromFile(bundlePath + bundleName);
        //记载bundle中的对象
        GameObject go = realBundle.LoadAsset<GameObject>(bundleName);
        GameObject go1 = Instantiate(go);
        //清理realBundle
        realBundle.Unload(false);
        for (int i = 0; i < dependenciesBundle.Length; i++)
        {
            dependenciesBundle[i].Unload(false);
        }
        go1.name = go1.name.Replace("(Clone)", "");
        return go1;
    }
    #endregion

    #region PhotonCallbcks
    #endregion
}