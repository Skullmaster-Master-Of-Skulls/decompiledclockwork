using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace System.Web.Http.Internal
{
	// Token: 0x0200011D RID: 285
	internal static class TypeHelper
	{
		// Token: 0x060006E0 RID: 1760 RVA: 0x00016FC0 File Offset: 0x000151C0
		internal static Type GetTaskInnerTypeOrNull(Type type)
		{
			if (type.IsGenericType && !type.IsGenericTypeDefinition)
			{
				Type genericTypeDefinition = type.GetGenericTypeDefinition();
				if (TypeHelper.TaskGenericType == genericTypeDefinition)
				{
					return type.GetGenericArguments()[0];
				}
			}
			return null;
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00016FFC File Offset: 0x000151FC
		internal static Type[] GetTypeArgumentsIfMatch(Type closedType, Type matchingOpenType)
		{
			if (!closedType.IsGenericType)
			{
				return null;
			}
			Type genericTypeDefinition = closedType.GetGenericTypeDefinition();
			if (!(matchingOpenType == genericTypeDefinition))
			{
				return null;
			}
			return closedType.GetGenericArguments();
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0001702B File Offset: 0x0001522B
		internal static bool IsCompatibleObject(Type type, object value)
		{
			return (value == null && TypeHelper.TypeAllowsNullValue(type)) || type.IsInstanceOfType(value);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00017041 File Offset: 0x00015241
		internal static bool IsNullableValueType(Type type)
		{
			return Nullable.GetUnderlyingType(type) != null;
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0001704F File Offset: 0x0001524F
		internal static bool TypeAllowsNullValue(Type type)
		{
			return !type.IsValueType || TypeHelper.IsNullableValueType(type);
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00017064 File Offset: 0x00015264
		internal static bool IsSimpleType(Type type)
		{
			return type.IsPrimitive || type.Equals(typeof(string)) || type.Equals(typeof(DateTime)) || type.Equals(typeof(decimal)) || type.Equals(typeof(Guid)) || type.Equals(typeof(DateTimeOffset)) || type.Equals(typeof(TimeSpan));
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x000170E8 File Offset: 0x000152E8
		internal static bool IsSimpleUnderlyingType(Type type)
		{
			Type underlyingType = Nullable.GetUnderlyingType(type);
			if (underlyingType != null)
			{
				type = underlyingType;
			}
			return TypeHelper.IsSimpleType(type);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0001710E File Offset: 0x0001530E
		internal static bool CanConvertFromString(Type type)
		{
			return TypeHelper.IsSimpleUnderlyingType(type) || TypeHelper.HasStringConverter(type);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00017120 File Offset: 0x00015320
		internal static bool HasStringConverter(Type type)
		{
			return TypeDescriptor.GetConverter(type).CanConvertFrom(typeof(string));
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00017138 File Offset: 0x00015338
		internal static ReadOnlyCollection<T> OfType<T>(object[] objects) where T : class
		{
			int num = objects.Length;
			List<T> list = new List<T>(num);
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				T t = objects[i] as T;
				if (t != null)
				{
					list.Add(t);
					num2++;
				}
			}
			list.Capacity = num2;
			return new ReadOnlyCollection<T>(list);
		}

		// Token: 0x040001F9 RID: 505
		private static readonly Type TaskGenericType = typeof(Task<>);

		// Token: 0x040001FA RID: 506
		internal static readonly Type ApiControllerType = typeof(ApiController);
	}
}
