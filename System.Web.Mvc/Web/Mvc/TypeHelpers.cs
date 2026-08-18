using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x020001EC RID: 492
	internal static class TypeHelpers
	{
		// Token: 0x06000EDE RID: 3806 RVA: 0x00027270 File Offset: 0x00025470
		public static TDelegate CreateDelegate<TDelegate>(Assembly assembly, string typeName, string methodName, object thisParameter) where TDelegate : class
		{
			Type type = assembly.GetType(typeName, false);
			if (type == null)
			{
				return default(TDelegate);
			}
			return TypeHelpers.CreateDelegate<TDelegate>(type, methodName, thisParameter);
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x000272AC File Offset: 0x000254AC
		public static TDelegate CreateDelegate<TDelegate>(Type targetType, string methodName, object thisParameter) where TDelegate : class
		{
			ParameterInfo[] parameters = typeof(TDelegate).GetMethod("Invoke").GetParameters();
			Type[] types = Array.ConvertAll<ParameterInfo, Type>(parameters, (ParameterInfo pInfo) => pInfo.ParameterType);
			MethodInfo method = targetType.GetMethod(methodName, types);
			if (method == null)
			{
				return default(TDelegate);
			}
			return Delegate.CreateDelegate(typeof(TDelegate), thisParameter, method, false) as TDelegate;
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x00027324 File Offset: 0x00025524
		public static TryGetValueDelegate CreateTryGetValueDelegate(Type targetType)
		{
			TypeHelpers._tryGetValueDelegateCacheLock.EnterReadLock();
			TryGetValueDelegate tryGetValueDelegate;
			try
			{
				if (TypeHelpers._tryGetValueDelegateCache.TryGetValue(targetType, out tryGetValueDelegate))
				{
					return tryGetValueDelegate;
				}
			}
			finally
			{
				TypeHelpers._tryGetValueDelegateCacheLock.ExitReadLock();
			}
			Type type = TypeHelpers.ExtractGenericInterface(targetType, typeof(IDictionary<, >));
			if (type != null)
			{
				Type[] genericArguments = type.GetGenericArguments();
				Type type2 = genericArguments[0];
				Type type3 = genericArguments[1];
				if (type2.IsAssignableFrom(typeof(string)))
				{
					MethodInfo method = TypeHelpers._strongTryGetValueImplInfo.MakeGenericMethod(new Type[]
					{
						type2,
						type3
					});
					tryGetValueDelegate = (TryGetValueDelegate)Delegate.CreateDelegate(typeof(TryGetValueDelegate), method);
				}
			}
			if (tryGetValueDelegate == null && typeof(IDictionary).IsAssignableFrom(targetType))
			{
				tryGetValueDelegate = new TryGetValueDelegate(TypeHelpers.TryGetValueFromNonGenericDictionary);
			}
			TypeHelpers._tryGetValueDelegateCacheLock.EnterWriteLock();
			try
			{
				TypeHelpers._tryGetValueDelegateCache[targetType] = tryGetValueDelegate;
			}
			finally
			{
				TypeHelpers._tryGetValueDelegateCacheLock.ExitWriteLock();
			}
			return tryGetValueDelegate;
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x00027438 File Offset: 0x00025638
		public static Type ExtractGenericInterface(Type queryType, Type interfaceType)
		{
			if (TypeHelpers.MatchesGenericType(queryType, interfaceType))
			{
				return queryType;
			}
			Type[] interfaces = queryType.GetInterfaces();
			return TypeHelpers.MatchGenericTypeFirstOrDefault(interfaces, interfaceType);
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0002745E File Offset: 0x0002565E
		public static object GetDefaultValue(Type type)
		{
			if (!TypeHelpers.TypeAllowsNullValue(type))
			{
				return Activator.CreateInstance(type);
			}
			return null;
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x00027470 File Offset: 0x00025670
		public static bool IsCompatibleObject<T>(object value)
		{
			return value is T || (value == null && TypeHelpers.TypeAllowsNullValue(typeof(T)));
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x00027490 File Offset: 0x00025690
		public static bool IsNullableValueType(Type type)
		{
			return Nullable.GetUnderlyingType(type) != null;
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x000274A0 File Offset: 0x000256A0
		public static MissingMethodException EnsureDebuggableException(MissingMethodException originalException, string fullTypeName)
		{
			MissingMethodException result = null;
			if (!originalException.Message.Contains(fullTypeName))
			{
				string message = string.Format(CultureInfo.CurrentCulture, MvcResources.TypeHelpers_CannotCreateInstance, new object[]
				{
					originalException.Message,
					fullTypeName
				});
				result = new MissingMethodException(message, originalException);
			}
			return result;
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x000274EB File Offset: 0x000256EB
		private static bool MatchesGenericType(Type type, Type matchType)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == matchType;
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x00027504 File Offset: 0x00025704
		private static Type MatchGenericTypeFirstOrDefault(Type[] types, Type matchType)
		{
			foreach (Type type in types)
			{
				if (TypeHelpers.MatchesGenericType(type, matchType))
				{
					return type;
				}
			}
			return null;
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x00027530 File Offset: 0x00025730
		private static bool StrongTryGetValueImpl<TKey, TValue>(object dictionary, string key, out object value)
		{
			IDictionary<TKey, TValue> dictionary2 = (IDictionary<TKey, TValue>)dictionary;
			TValue tvalue;
			bool result = dictionary2.TryGetValue((TKey)((object)key), out tvalue);
			value = tvalue;
			return result;
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0002755C File Offset: 0x0002575C
		private static bool TryGetValueFromNonGenericDictionary(object dictionary, string key, out object value)
		{
			IDictionary dictionary2 = (IDictionary)dictionary;
			bool flag = dictionary2.Contains(key);
			value = (flag ? dictionary2[key] : null);
			return flag;
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x00027588 File Offset: 0x00025788
		public static bool TypeAllowsNullValue(Type type)
		{
			return !type.IsValueType || TypeHelpers.IsNullableValueType(type);
		}

		// Token: 0x040003E4 RID: 996
		private static readonly Dictionary<Type, TryGetValueDelegate> _tryGetValueDelegateCache = new Dictionary<Type, TryGetValueDelegate>();

		// Token: 0x040003E5 RID: 997
		private static readonly ReaderWriterLockSlim _tryGetValueDelegateCacheLock = new ReaderWriterLockSlim();

		// Token: 0x040003E6 RID: 998
		private static readonly MethodInfo _strongTryGetValueImplInfo = typeof(TypeHelpers).GetMethod("StrongTryGetValueImpl", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x040003E7 RID: 999
		public static readonly Assembly MsCorLibAssembly = typeof(string).Assembly;

		// Token: 0x040003E8 RID: 1000
		public static readonly Assembly MvcAssembly = typeof(Controller).Assembly;

		// Token: 0x040003E9 RID: 1001
		public static readonly Assembly SystemWebAssembly = typeof(HttpContext).Assembly;
	}
}
