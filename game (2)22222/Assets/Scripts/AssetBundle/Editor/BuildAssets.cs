#region 模块信息
// **********************************************************************
// Copyright (C) 2017 The company name
//
// 文件名(File Name):             BuildAssets.cs
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
using UnityEditor;
using System.IO;

public class BuildAssets {

    //AB资源输出路径
    public static string assetBundleOutPath = Application.streamingAssetsPath + "/AssetBundle";
    public static string assetBundleInputPath = Application.dataPath + "/ABSource";

    [MenuItem("AssetBundle/BuildCurrentPlatform")]
    static void BuildCurrentPlatformAssetBundle()
    {
        //先清理所有的ABName
        ClearAllAssetBundleNames();
        //设置当前根目录下的所有资源的AssetBundleName
        SetRootPathAssetBundleNames(assetBundleInputPath);
        //打包AB
        BuildPipeline.BuildAssetBundles(assetBundleOutPath, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
        //刷新资源
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 清理所有的ABName
    /// </summary>
    static void ClearAllAssetBundleNames()
    {
        //先获取所有资源的路径
        string[] assetsPath = AssetDatabase.GetAllAssetPaths();

        for (int i = 0; i < assetsPath.Length; i++)
        {
            //清理所有的资源的AssetBundleName
            AssetDatabase.RemoveAssetBundleName(assetsPath[i], true);
        }
    }

    static void SetRootPathAssetBundleNames(string rootPath)
    {
        //获取当前目录要打包的文件夹的对象
        DirectoryInfo directoryInfo = new DirectoryInfo(rootPath);
        //遍历该文件夹的所有子对象
        foreach (var item in directoryInfo.GetFileSystemInfos())
        {
            if (item is DirectoryInfo)
            {
                SetRootPathAssetBundleNames(item.FullName);
            }
            else if (!item.FullName.Contains(".meta"))
            {
                SetAssetBundleName(item.FullName);
            }
        }
    }

    static void SetAssetBundleName(string path)
    {
        //将path这个绝对路径改为相对路径
        string relativePath = path.Substring(Application.dataPath.Length);
        //获取资源入口对象
        AssetImporter asset = AssetImporter.GetAtPath("Assets" + relativePath);
        //获取文件名字
        string fileName = relativePath.Substring(relativePath.LastIndexOf("\\") + 1);
        //设置AssetBundleName
        if (fileName.Remove(fileName.LastIndexOf(".")) != "")
        {
            //设置AssetBundle名字
            asset.assetBundleName = fileName.Remove(fileName.LastIndexOf("."));
        }
    }
}
