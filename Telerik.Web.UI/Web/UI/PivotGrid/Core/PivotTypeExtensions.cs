using System;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000CE7 RID: 3303
	internal static class PivotTypeExtensions
	{
		// Token: 0x06007B53 RID: 31571 RVA: 0x001C4FD0 File Offset: 0x001C31D0
		public static bool CanSort(Type source)
		{
			if (source == null)
			{
				return false;
			}
			Type source2 = Nullable.GetUnderlyingType(source) ?? source;
			return PivotTypeExtensions.IsIComparable(source2);
		}

		// Token: 0x06007B54 RID: 31572 RVA: 0x001C4FFC File Offset: 0x001C31FC
		internal static bool IsIComparable(Type source)
		{
			return source != null && (typeof(IComparable).IsAssignableFrom(source) || typeof(IComparable<>).MakeGenericType(new Type[]
			{
				source
			}).IsAssignableFrom(source));
		}

		// Token: 0x06007B55 RID: 31573 RVA: 0x001C504C File Offset: 0x001C324C
		internal static Type FindGenericType(this Type type, Type genericType)
		{
			while (type != null && type != typeof(object))
			{
				if (type.IsGenericType && type.GetGenericTypeDefinition() == genericType)
				{
					return type;
				}
				if (genericType.IsInterface)
				{
					foreach (Type type2 in type.GetInterfaces())
					{
						Type type3 = type2.FindGenericType(genericType);
						if (type3 != null)
						{
							return type3;
						}
					}
				}
				type = type.BaseType;
			}
			return null;
		}

		// Token: 0x06007B56 RID: 31574 RVA: 0x001C50D4 File Offset: 0x001C32D4
		internal static object DefaultValue(Type type)
		{
			if (type != null && type.IsValueType)
			{
				return Activator.CreateInstance(type);
			}
			return null;
		}

		// Token: 0x06007B57 RID: 31575 RVA: 0x001C50EF File Offset: 0x001C32EF
		internal static bool IsNullableType(Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x06007B58 RID: 31576 RVA: 0x001C5110 File Offset: 0x001C3310
		internal static Type GetNonNullableType(Type type)
		{
			if (type == null)
			{
				return null;
			}
			if (!PivotTypeExtensions.IsNullableType(type))
			{
				return type;
			}
			return type.GetGenericArguments()[0];
		}

		// Token: 0x040021C4 RID: 8644
		internal static readonly Type[] PredefinedTypes = new Type[]
		{
			typeof(object),
			typeof(bool),
			typeof(char),
			typeof(string),
			typeof(sbyte),
			typeof(byte),
			typeof(short),
			typeof(ushort),
			typeof(int),
			typeof(uint),
			typeof(long),
			typeof(ulong),
			typeof(float),
			typeof(double),
			typeof(decimal),
			typeof(DateTime),
			typeof(TimeSpan),
			typeof(Guid),
			typeof(Math),
			typeof(Convert)
		};
	}
}
