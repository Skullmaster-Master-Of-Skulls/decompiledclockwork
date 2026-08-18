using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TechnoPro.Common.Public.Adapters
{
	// Token: 0x020005F4 RID: 1524
	public static class TypeAdapter
	{
		// Token: 0x060030E8 RID: 12520 RVA: 0x00043724 File Offset: 0x00041924
		public static T[] GetCustomAttributes<T>(this string typeName) where T : Attribute
		{
			Type typeFromHandle = typeof(T);
			Type type = Type.GetType(typeName);
			return (T[])((type != null) ? type.GetCustomAttributes(typeFromHandle, true) : null);
		}

		// Token: 0x060030E9 RID: 12521 RVA: 0x0004375C File Offset: 0x0004195C
		public static T[] GetCustomAttributes<T>(this Type type) where T : Attribute
		{
			Type typeFromHandle = typeof(T);
			return (T[])((type != null) ? type.GetCustomAttributes(typeFromHandle, true) : null);
		}

		// Token: 0x060030EA RID: 12522 RVA: 0x0004378C File Offset: 0x0004198C
		public static Type[] FindTypesByNamespace(this string nameSpace, Type anyTypeInNamespace)
		{
			return nameSpace.FindTypesByNamespace(anyTypeInNamespace.Assembly);
		}

		// Token: 0x060030EB RID: 12523 RVA: 0x000437AC File Offset: 0x000419AC
		public static Type[] FindTypesByNamespace(this string nameSpace, Assembly assembly)
		{
			return (from g in assembly.GetTypes()
			where g.Namespace == nameSpace
			select g).ToArray<Type>();
		}

		// Token: 0x060030EC RID: 12524 RVA: 0x000437E8 File Offset: 0x000419E8
		public static IList<T> GetAttributesOfType<T>(this Type type) where T : Attribute
		{
			return type.GetCustomAttributes<T>().ToList<T>();
		}

		// Token: 0x060030ED RID: 12525 RVA: 0x00043808 File Offset: 0x00041A08
		public static bool HasAttributeOfType<T>(this Type type) where T : Attribute
		{
			return type.GetCustomAttribute<T>() != null;
		}

		// Token: 0x060030EE RID: 12526 RVA: 0x00043828 File Offset: 0x00041A28
		public static bool TypeEquals(Type typeA, Type typeB)
		{
			bool flag = typeA == typeB;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				Type underlyingType = Nullable.GetUnderlyingType(typeA);
				Type underlyingType2 = Nullable.GetUnderlyingType(typeB);
				bool flag2 = underlyingType != null && underlyingType == typeB;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = underlyingType2 != null && underlyingType2 == typeA;
					if (flag3)
					{
						result = true;
					}
					else
					{
						bool flag4 = underlyingType != null && underlyingType2 != null && underlyingType == underlyingType2;
						result = flag4;
					}
				}
			}
			return result;
		}
	}
}
