#region 模块信息
// **********************************************************************
// Copyright (C) 2017 The company name
//
// 文件名(File Name):             DBA.cs
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
using UnityEngine.UI;
using System.Collections.Generic;

public class DBA {

    #region SingleTon

    private static DBA instace;
    public static DBA Instance
    {
        get
        {
            if (instace == null) instace = new DBA();
            return instace;
        }
    }
    private DBA()
    {
        sqlBase = new SQLBase();
    }

    #endregion

    #region Field
    //数据库操作对象
    private SQLBase sqlBase;
    #endregion

    #region Functions
    /// <summary>
    /// 插入新的武器
    /// </summary>
    /// <param name="sql">SQL语句</param>
    /// <returns></returns>
    public bool InsertWeapon(string sql)
    {
        return sqlBase.ExecuteNonQuery(sql) > 0 ? true : false;
    }

    /// <summary>
    /// 获取本地版本号
    /// </summary>
    /// <returns></returns>
    public float GetLocalVersionID()
    {
        string sql = "SELECT ID FROM Version;";
        object ob = sqlBase.ExecuteScalar(sql);
        if (ob == null) return 0f;
        return float.Parse(ob.ToString());
    }

    /// <summary>
    /// 根据武器名字获取武器的ID
    /// </summary>
    /// <param name="weaponName">武器名字</param>
    /// <returns></returns>
    public int GetWeaponTypeByName(string weaponName)
    {
        string sql = string.Format("SELECT Type FROM Weapon WHERE name = '{0}'", weaponName);
        object ob = sqlBase.ExecuteScalar(sql);
        if (ob == null) return 0;
        return int.Parse(ob.ToString());
    }

    /// <summary>
    /// 根据职业名字获取其所有皮肤的名字
    /// </summary>
    /// <param name="career">职业名字</param>
    /// <returns></returns>
    public string[] GetCareerSkins(string career)
    {
        string sql = string.Format("SELECT Skin FROM Skin WHERE career = '{0}';", career);
        List<ArrayList> arrayLists = sqlBase.ExecuteReader(sql);
        string[] skins = new string[arrayLists.Count];
        for (int i = 0; i < skins.Length; i++)
        {
            skins[i] = arrayLists[i][0].ToString();
        }
        return skins;
    }

    /// <summary>
    /// 根据职业名获取其所有图标的名字
    /// </summary>
    /// <param name="career">职业名</param>
    /// <returns></returns>
    public string[] GetCareerIcos(string career)
    {
        string sql = string.Format("SELECT Icon FROM Icon WHERE career = '{0}';", career);
        List<ArrayList> arrayLists = sqlBase.ExecuteReader(sql);
        string[] icons = new string[arrayLists.Count];
        for (int i = 0; i < icons.Length; i++)
        {
            icons[i] = arrayLists[i][0].ToString();
        }
        return icons;
    }

    /// <summary>
    /// 根据武器名获取子弹名
    /// </summary>
    /// <param name="weaponName">武器名</param>
    /// <returns></returns>
    public string GetBulletNameByWeaponName(string weaponName)
    {
        string sql = string.Format("SELECT BulletName FROM Bullet WHERE Name = '{0}';",weaponName);
        object ob = sqlBase.ExecuteScalar(sql);
        if(ob == null) return default(string);
        return ob.ToString();
    }

    /// <summary>
    /// 根据武器名获取武器属性，0 攻击范围 ,1 攻击伤害
    /// </summary>
    /// <param name="weaponName"></param>
    /// <returns></returns>
    public byte[] GetWeaponAttributeByName(string weaponName)
    {
        string sql =string.Format("SELECT AttackRange,AttackValue FROM WeaponAttribute WHERE Weapon = '{0}';",weaponName);
        List<ArrayList> arrayLists = sqlBase.ExecuteReader(sql);
        byte[] attr = new byte[arrayLists[0].Count];
        for (int i = 0; i < attr.Length; i++)
        {
            attr[i]=byte.Parse(arrayLists[0][i].ToString());
        }
        return attr;
    }

    /// <summary>
    /// 根据职业名获取职业属性，0 血量，1 名字，2 武器名字，3 攻速，4 移速，5 攻击范围，5 经验倍率
    /// </summary>
    /// <param name="career"></param>
    /// <returns></returns>
    public string[] GetCareerAttributeByName(string career)
    {
        string sql = string.Format("SELECT blood,Name,Weapon,attackspeed,movespeed,AttackRange,EXPproportion FROM CareerAttribute WHERE career = '{0}';",career);
        List<ArrayList> arrayLists = sqlBase.ExecuteReader(sql);
        string[] attr = new string[arrayLists[0].Count];
        for (int i = 0; i < attr.Length; i++)
        {
            attr[i]=arrayLists[0][i].ToString();
        }
        return attr;
    }

    #endregion
}