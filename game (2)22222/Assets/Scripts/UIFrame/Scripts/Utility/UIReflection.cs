#region 模块信息
// **********************************************************************
// Copyright (C) 2017 The company name
//
// 文件名(File Name):             UIReflection.cs
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
using System.Reflection;
using System;

namespace UIFrame
{
    public static class UIReflection
    {
        /// <summary>
        /// 查找类型中的某个常量字段值
        /// </summary>
        /// <param name="constName">常量名</param>
        /// <param name="constClassType">类型名</param>
        /// <returns></returns>
        public static object GetConstField(string constName, Type constClassType)
        {
            FieldInfo info = constClassType.GetField(constName);
            if (info == null) return null;
            return info.GetValue(null);
        }

        /// <summary>
        /// 通过名字获取行为脚本
        /// </summary>
        /// <param name="behaviourName">行为脚本名</param>
        /// <param name="inhertBehaviourName">行为脚本父类名</param>
        /// <returns></returns>
        public static Type GetUIBehaviourByName(string behaviourName, string inhertBehaviourName)
        {
            //如果行为脚本和其父类名一致
            if (behaviourName == inhertBehaviourName)
            {
                //直接返回当前命名空间下的行为脚本
                return Type.GetType(MethodBase.GetCurrentMethod().DeclaringType.Namespace + "." + inhertBehaviourName);
            }

            //获取当前行为脚本的命名空间
            string bhNameSpace = UIConst.UINAMESPACE;
            //当前要获取的类型
            Type currentType = null;
            //判断是否有命名空间
            if (bhNameSpace.Equals(""))
            {
                //没有命名空间，直接返回当前命名空间下的行为脚本
                currentType = Type.GetType(behaviourName);
            }
            else
            {
                //有命名空间时，返回命名空间下的行为脚本
                currentType = Type.GetType(bhNameSpace + "." + behaviourName);
            }

            if (currentType == null)
            {
                Debug.LogError("无法通过反射获取到行为脚本类型:" + behaviourName + "," + inhertBehaviourName);
                return null;
            }

            if (currentType.Name == inhertBehaviourName || currentType.BaseType.Name == inhertBehaviourName)
            {
                return currentType;
            }

            Debug.LogError("配置文件名称有误");
            return null;
        }
    }
}
