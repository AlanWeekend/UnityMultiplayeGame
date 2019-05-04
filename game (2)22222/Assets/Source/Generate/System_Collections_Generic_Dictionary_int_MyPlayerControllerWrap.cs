﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class System_Collections_Generic_Dictionary_int_MyPlayerControllerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(System.Collections.Generic.Dictionary<int,MyPlayerController>), typeof(System.Object), "PlayerControllerMap");
		L.RegFunction(".geti", get_Item);
		L.RegFunction("get_Item", get_Item);
		L.RegFunction(".seti", set_Item);
		L.RegFunction("set_Item", set_Item);
		L.RegFunction("Add", Add);
		L.RegFunction("Clear", Clear);
		L.RegFunction("ContainsKey", ContainsKey);
		L.RegFunction("ContainsValue", ContainsValue);
		L.RegFunction("GetObjectData", GetObjectData);
		L.RegFunction("OnDeserialization", OnDeserialization);
		L.RegFunction("Remove", Remove);
		L.RegFunction("TryGetValue", TryGetValue);
		L.RegFunction("GetEnumerator", GetEnumerator);
		L.RegFunction("New", _CreateSystem_Collections_Generic_Dictionary_int_MyPlayerController);
		L.RegVar("this", _this, null);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("Count", get_Count, null);
		L.RegVar("Comparer", get_Comparer, null);
		L.RegVar("Keys", get_Keys, null);
		L.RegVar("Values", get_Values, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateSystem_Collections_Generic_Dictionary_int_MyPlayerController(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				System.Collections.Generic.Dictionary<int,MyPlayerController> obj = new System.Collections.Generic.Dictionary<int,MyPlayerController>();
				ToLua.PushSealed(L, obj);
				return 1;
			}
			else if (count == 1 && TypeChecker.CheckTypes<int>(L, 1))
			{
				int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
				System.Collections.Generic.Dictionary<int,MyPlayerController> obj = new System.Collections.Generic.Dictionary<int,MyPlayerController>(arg0);
				ToLua.PushSealed(L, obj);
				return 1;
			}
			else if (count == 1 && TypeChecker.CheckTypes<System.Collections.Generic.IDictionary<int,MyPlayerController>>(L, 1))
			{
				System.Collections.Generic.IDictionary<int,MyPlayerController> arg0 = (System.Collections.Generic.IDictionary<int,MyPlayerController>)ToLua.ToObject(L, 1);
				System.Collections.Generic.Dictionary<int,MyPlayerController> obj = new System.Collections.Generic.Dictionary<int,MyPlayerController>(arg0);
				ToLua.PushSealed(L, obj);
				return 1;
			}
			else if (count == 1 && TypeChecker.CheckTypes<System.Collections.Generic.IEqualityComparer<int>>(L, 1))
			{
				System.Collections.Generic.IEqualityComparer<int> arg0 = (System.Collections.Generic.IEqualityComparer<int>)ToLua.ToObject(L, 1);
				System.Collections.Generic.Dictionary<int,MyPlayerController> obj = new System.Collections.Generic.Dictionary<int,MyPlayerController>(arg0);
				ToLua.PushSealed(L, obj);
				return 1;
			}
			else if (count == 2 && TypeChecker.CheckTypes<int, System.Collections.Generic.IEqualityComparer<int>>(L, 1))
			{
				int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
				System.Collections.Generic.IEqualityComparer<int> arg1 = (System.Collections.Generic.IEqualityComparer<int>)ToLua.ToObject(L, 2);
				System.Collections.Generic.Dictionary<int,MyPlayerController> obj = new System.Collections.Generic.Dictionary<int,MyPlayerController>(arg0, arg1);
				ToLua.PushSealed(L, obj);
				return 1;
			}
			else if (count == 2 && TypeChecker.CheckTypes<System.Collections.Generic.IDictionary<int,MyPlayerController>, System.Collections.Generic.IEqualityComparer<int>>(L, 1))
			{
				System.Collections.Generic.IDictionary<int,MyPlayerController> arg0 = (System.Collections.Generic.IDictionary<int,MyPlayerController>)ToLua.ToObject(L, 1);
				System.Collections.Generic.IEqualityComparer<int> arg1 = (System.Collections.Generic.IEqualityComparer<int>)ToLua.ToObject(L, 2);
				System.Collections.Generic.Dictionary<int,MyPlayerController> obj = new System.Collections.Generic.Dictionary<int,MyPlayerController>(arg0, arg1);
				ToLua.PushSealed(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: System.Collections.Generic.Dictionary<int,MyPlayerController>.New");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _get_this(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			System.Collections.Generic.Dictionary<int,MyPlayerController> obj = (System.Collections.Generic.Dictionary<int,MyPlayerController>)ToLua.CheckObject(L, 1, typeof(System.Collections.Generic.Dictionary<int,MyPlayerController>));
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			MyPlayerController o = obj[arg0];
			ToLua.PushObject(L, o);
			return 1;

		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _set_this(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			System.Collections.Generic.Dictionary<int,MyPlayerController> obj = (System.Collections.Generic.Dictionary<int,MyPlayerController>)ToLua.CheckObject(L, 1, typeof(System.Collections.Generic.Dictionary<int,MyPlayerController>));
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			MyPlayerController arg1 = (MyPlayerController)ToLua.CheckObject<MyPlayerController>(L, 3);
			obj[arg0] = arg1;
			return 0;

		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _this(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushvalue(L, 1);
			LuaDLL.tolua_bindthis(L, _get_this, _set_this);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Item(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			System.Collections.Generic.Dictionary<int,MyPlayerController> obj = (System.Collections.Generic.Dictionary<int,MyPlayerController>)ToLua.CheckObject(L, 1, typeof(System.Collections.Generic.Dictionary<int,MyPlayerController>));
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			MyPlayerController o = obj[arg0];
			ToLua.PushObject(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Item(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			System.Collections.Generic.Dictionary<int,MyPlayerController> obj = (System.Collections.Generic.Dictionary<int,MyPlayerController>)ToLua.CheckObject(L, 1, typeof(System.Collections.Generic.Dictionary<int,MyPlayerController>));
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			MyPlayerController arg1 = (MyPlayerController)ToLua.CheckObject<MyPlayerController>(L, 3);
			obj[arg0] = arg1;
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Add(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			System.Collections.Generic.Dictionary<int,MyPlayerController> obj = (System.Collections.Generic.Dictionary<int,MyPlayerController>)ToLua.CheckObject(L, 1, typeof(System.Collections.Generic.Dictionary<int,MyPlayerController>));
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			MyPlayerController arg1 = (MyPlayerController)ToLua.CheckObject<MyPlayerController>(L, 3);
			obj.Add(arg0, arg1);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Clear(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			System.Collections.Generic.Dictionary<int,MyPlayerController> obj = (System.Collections.Generic.Dictionary<int,MyPlayerController>)ToLua.CheckObject(L, 1, typeof(System.Collections.Generic.Dictionary<int,MyPlayerController>));
			obj.Clear();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ContainsKey(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			System.Collections.Generic.Dictionary<int,MyPlayerController> obj = (System.Collections.Generic.Dictionary<int,MyPlayerController>)ToLua.CheckObject(L, 1, typeof(System.Collections.Generic.Dictionary<int,MyPlayerController>));
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			bool o = obj.ContainsKey(arg0);
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ContainsValue(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			System.Collections.Generic.Dictionary<int,MyPlayerController> obj = (System.Collections.Generic.Dictionary<int,MyPlayerController>)ToLua.CheckObject(L, 1, typeof(System.Collections.Generic.Dictionary<int,MyPlayerController>));
			MyPlayerController arg0 = (MyPlayerController)ToLua.CheckObject<MyPlayerController>(L, 2);
			bool o = obj.ContainsValue(arg0);
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetObjectData(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			System.Collections.Generic.Dictionary<int,MyPlayerController> obj = (System.Collections.Generic.Dictionary<int,MyPlayerController>)ToLua.CheckObject(L, 1, typeof(System.Collections.Generic.Dictionary<int,MyPlayerController>));
			System.Runtime.Serialization.SerializationInfo arg0 = (System.Runtime.Serialization.SerializationInfo)ToLua.CheckObject(L, 2, typeof(System.Runtime.Serialization.SerializationInfo));
			System.Runtime.Serialization.StreamingContext arg1 = StackTraits<System.Runtime.Serialization.StreamingContext>.Check(L, 3);
			obj.GetObjectData(arg0, arg1);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDeserialization(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			System.Collections.Generic.Dictionary<int,MyPlayerController> obj = (System.Collections.Generic.Dictionary<int,MyPlayerController>)ToLua.CheckObject(L, 1, typeof(System.Collections.Generic.Dictionary<int,MyPlayerController>));
			object arg0 = ToLua.ToVarObject(L, 2);
			obj.OnDeserialization(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Remove(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			System.Collections.Generic.Dictionary<int,MyPlayerController> obj = (System.Collections.Generic.Dictionary<int,MyPlayerController>)ToLua.CheckObject(L, 1, typeof(System.Collections.Generic.Dictionary<int,MyPlayerController>));
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			bool o = obj.Remove(arg0);
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int TryGetValue(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			System.Collections.Generic.Dictionary<int,MyPlayerController> obj = (System.Collections.Generic.Dictionary<int,MyPlayerController>)ToLua.CheckObject(L, 1, typeof(System.Collections.Generic.Dictionary<int,MyPlayerController>));
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			MyPlayerController arg1 = null;
			bool o = obj.TryGetValue(arg0, out arg1);
			LuaDLL.lua_pushboolean(L, o);
			ToLua.PushObject(L, arg1);
			return 2;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetEnumerator(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			System.Collections.Generic.Dictionary<int,MyPlayerController> obj = (System.Collections.Generic.Dictionary<int,MyPlayerController>)ToLua.CheckObject(L, 1, typeof(System.Collections.Generic.Dictionary<int,MyPlayerController>));
			System.Collections.IEnumerator o = obj.GetEnumerator();
			ToLua.Push(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Count(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			System.Collections.Generic.Dictionary<int,MyPlayerController> obj = (System.Collections.Generic.Dictionary<int,MyPlayerController>)o;
			int ret = obj.Count;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index Count on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Comparer(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			System.Collections.Generic.Dictionary<int,MyPlayerController> obj = (System.Collections.Generic.Dictionary<int,MyPlayerController>)o;
			System.Collections.Generic.IEqualityComparer<int> ret = obj.Comparer;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index Comparer on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Keys(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			System.Collections.Generic.Dictionary<int,MyPlayerController> obj = (System.Collections.Generic.Dictionary<int,MyPlayerController>)o;
			System.Collections.Generic.Dictionary<int,MyPlayerController>.KeyCollection ret = obj.Keys;
			ToLua.PushSealed(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index Keys on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Values(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			System.Collections.Generic.Dictionary<int,MyPlayerController> obj = (System.Collections.Generic.Dictionary<int,MyPlayerController>)o;
			System.Collections.Generic.Dictionary<int,MyPlayerController>.ValueCollection ret = obj.Values;
			ToLua.PushSealed(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index Values on a nil value");
		}
	}
}
