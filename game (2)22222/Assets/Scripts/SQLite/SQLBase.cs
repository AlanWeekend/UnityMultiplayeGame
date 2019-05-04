#region 模块信息
// **********************************************************************
// Copyright (C) 2017 The company name
//
// 文件名(File Name):             SQLBase.cs
// 公司(Company):  		 		  #COMPANY#
// 作者(Author):                  #AuthorName#
// 版本号(Version):		 		  #VERSION#
// Unity版本号(Unity Version):	  #UNITYVERSION#
// 创建时间(CreateTime):          #DATE#
// 修改者列表(modifier):
// 模块描述(Module description):    数据库底层框架
// **********************************************************************
#endregion
using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System.Data.Common;
using System;
using System.Collections.Generic;

public class SQLBase {

    #region Field
    //数据库名
    private string dbName ="game.db";
    //数据库连接对象
    private SqliteConnection sqliteConnection;
    //数据库事务对象
    private DbTransaction dbTransaction;
    //数据库指令对象
    private SqliteCommand sqliteCommand;
    //数据库读取对象
    private SqliteDataReader reader;
    #endregion

    #region Constructor
    public SQLBase()
    {
        //创建数据库连接对象
        sqliteConnection = new SqliteConnection("Data Source = " + Application.streamingAssetsPath + ABConst.dbPath + dbName);
        sqliteConnection.Open();
        //创建数据库指令对象
        sqliteCommand = sqliteConnection.CreateCommand();
    }

    ~SQLBase()
    {
        //关闭数据库连接
        sqliteConnection.Close();
    }
    #endregion

    #region Functions
    /// <summary>
    /// 执行SQL语句返回影响的行数
    /// </summary>
    /// <param name="sql">SQL语句</param>
    /// <returns></returns>
    public int ExecuteNonQuery(string sql)
    {
        //影响行数
        int i = 0;
        //赋值SQL语句
        sqliteCommand.CommandText = sql;
        //开始事务
        dbTransaction = sqliteConnection.BeginTransaction();
        try
        {
            //执行SQL语句
            i = sqliteCommand.ExecuteNonQuery();
            //提交事务
            dbTransaction.Commit();
        }
        catch (SqliteException e)
        {
            Debug.LogWarning("SQL语句执行出错:"+sql+",原因："+e.Message);
            //回退
            dbTransaction.Rollback();
        }
        return i;
    }

    /// <summary>
    /// 执行SQL语句，返回查询出的(1,1)的数据
    /// </summary>
    /// <param name="sql">SQL语句</param>
    /// <returns></returns>
    public object ExecuteScalar(string sql)
    {
        //SQL语句赋值
        sqliteCommand.CommandText = sql;
        try
        {
            return sqliteCommand.ExecuteScalar();
        }
        catch (SqliteException e)
        {
            Debug.LogWarning("SQL语句执行出错:" + sql + ",原因：" + e.Message);
            return null;
        }
    }

    /// <summary>
    /// 执行SQL语句，返回多个查询结果
    /// </summary>
    /// <param name="sql">SQL语句</param>
    /// <returns></returns>
    public List<ArrayList> ExecuteReader(string sql)
    {
        //SQL语句赋值
        sqliteCommand.CommandText = sql;
        List<ArrayList> arrays = new List<ArrayList>();
        try
        {
            reader = sqliteCommand.ExecuteReader();
            while (reader.Read())
            {
                ArrayList arrayList = new ArrayList();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    arrayList.Add(reader[i]);
                }
                arrays.Add(arrayList);
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("SQL语句执行出错:" + sql + ",原因：" + e.Message);
        }
        finally
        {
            if(reader != null)
                reader.Close();
        }

        return arrays;
    }
    #endregion
}
