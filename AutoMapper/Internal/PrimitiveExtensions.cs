using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AutoMapper.Internal
{
	// Token: 0x020000B2 RID: 178
	public static class PrimitiveExtensions
	{
		// Token: 0x06000544 RID: 1348 RVA: 0x00013D50 File Offset: 0x00011F50
		public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
		{
			TValue result;
			dictionary.TryGetValue(key, out result);
			return result;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00013D68 File Offset: 0x00011F68
		public static bool IsNullableType(this Type type)
		{
			return type.IsGenericType() && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00013D89 File Offset: 0x00011F89
		public static Type GetTypeOfNullable(this Type type)
		{
			return type.GetTypeInfo().GenericTypeArguments[0];
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00013D98 File Offset: 0x00011F98
		public static bool IsCollectionType(this Type type)
		{
			if (type.IsGenericType() && type.GetGenericTypeDefinition() == typeof(ICollection<>))
			{
				return true;
			}
			return (from t in type.GetTypeInfo().ImplementedInterfaces
			where t.IsGenericType()
			select t.GetGenericTypeDefinition()).Any((Type t) => t == typeof(ICollection<>));
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00013E3D File Offset: 0x0001203D
		public static bool IsEnumerableType(this Type type)
		{
			return type.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IEnumerable));
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00013E59 File Offset: 0x00012059
		public static bool IsQueryableType(this Type type)
		{
			return type.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IQueryable));
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00013E75 File Offset: 0x00012075
		public static bool IsListType(this Type type)
		{
			return type.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IList));
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00013E91 File Offset: 0x00012091
		public static bool IsListOrDictionaryType(this Type type)
		{
			return type.IsListType() || type.IsDictionaryType();
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00013EA4 File Offset: 0x000120A4
		public static bool IsDictionaryType(this Type type)
		{
			if (type.IsGenericType() && type.GetGenericTypeDefinition() == typeof(IDictionary<, >))
			{
				return true;
			}
			return (from t in type.GetTypeInfo().ImplementedInterfaces
			where t.IsGenericType()
			select t.GetGenericTypeDefinition()).Any((Type t) => t == typeof(IDictionary<, >));
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00013F4C File Offset: 0x0001214C
		public static Type GetDictionaryType(this Type type)
		{
			if (type.IsGenericType() && type.GetGenericTypeDefinition() == typeof(IDictionary<, >))
			{
				return type;
			}
			return (from t in type.GetTypeInfo().ImplementedInterfaces
			where t.IsGenericType() && t.GetGenericTypeDefinition() == typeof(IDictionary<, >)
			select t).FirstOrDefault<Type>();
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00013FAE File Offset: 0x000121AE
		public static Type GetGenericElementType(this Type type)
		{
			if (type.HasElementType)
			{
				return type.GetElementType();
			}
			return type.GetTypeInfo().GenericTypeArguments[0];
		}
	}
}
