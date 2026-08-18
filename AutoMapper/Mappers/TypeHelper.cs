using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper.Internal;

namespace AutoMapper.Mappers
{
	// Token: 0x02000092 RID: 146
	public static class TypeHelper
	{
		// Token: 0x0600045A RID: 1114 RVA: 0x00011E5F File Offset: 0x0001005F
		public static Type GetElementType(Type enumerableType)
		{
			return TypeHelper.GetElementType(enumerableType, null);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00011E68 File Offset: 0x00010068
		public static Type GetElementType(Type enumerableType, IEnumerable enumerable)
		{
			if (enumerableType.HasElementType)
			{
				return enumerableType.GetElementType();
			}
			if (enumerableType.IsGenericType() && enumerableType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
			{
				return enumerableType.GetTypeInfo().GenericTypeArguments[0];
			}
			Type ienumerableType = TypeHelper.GetIEnumerableType(enumerableType);
			if (ienumerableType != null)
			{
				return ienumerableType.GetTypeInfo().GenericTypeArguments[0];
			}
			if (typeof(IEnumerable).IsAssignableFrom(enumerableType))
			{
				object obj = (enumerable != null) ? enumerable.Cast<object>().FirstOrDefault<object>() : null;
				return ((obj != null) ? obj.GetType() : null) ?? typeof(object);
			}
			throw new ArgumentException(string.Format("Unable to find the element type for type '{0}'.", enumerableType), "enumerableType");
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00011F22 File Offset: 0x00010122
		public static Type GetEnumerationType(Type enumType)
		{
			if (enumType.IsNullableType())
			{
				enumType = enumType.GetTypeInfo().GenericTypeArguments[0];
			}
			if (!enumType.IsEnum())
			{
				return null;
			}
			return enumType;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00011F48 File Offset: 0x00010148
		private static Type GetIEnumerableType(Type enumerableType)
		{
			Type result;
			try
			{
				result = enumerableType.GetTypeInfo().ImplementedInterfaces.FirstOrDefault((Type t) => t.Name == "IEnumerable`1");
			}
			catch (AmbiguousMatchException)
			{
				if (enumerableType.BaseType() != typeof(object))
				{
					result = TypeHelper.GetIEnumerableType(enumerableType.BaseType());
				}
				else
				{
					result = null;
				}
			}
			return result;
		}
	}
}
