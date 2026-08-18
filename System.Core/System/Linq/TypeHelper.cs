using System;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x02000168 RID: 360
	internal static class TypeHelper
	{
		// Token: 0x06000C65 RID: 3173 RVA: 0x0002DE52 File Offset: 0x0002C052
		internal static bool IsEnumerableType(Type enumerableType)
		{
			return TypeHelper.FindGenericType(typeof(IEnumerable<>), enumerableType) != null;
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x0002DE6A File Offset: 0x0002C06A
		internal static bool IsKindOfGeneric(Type type, Type definition)
		{
			return TypeHelper.FindGenericType(definition, type) != null;
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x0002DE7C File Offset: 0x0002C07C
		internal static Type GetElementType(Type enumerableType)
		{
			Type type = TypeHelper.FindGenericType(typeof(IEnumerable<>), enumerableType);
			if (type != null)
			{
				return type.GetGenericArguments()[0];
			}
			return enumerableType;
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x0002DEB0 File Offset: 0x0002C0B0
		internal static Type FindGenericType(Type definition, Type type)
		{
			while (type != null && type != typeof(object))
			{
				if (type.IsGenericType && type.GetGenericTypeDefinition() == definition)
				{
					return type;
				}
				if (definition.IsInterface)
				{
					foreach (Type type2 in type.GetInterfaces())
					{
						Type type3 = TypeHelper.FindGenericType(definition, type2);
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

		// Token: 0x06000C69 RID: 3177 RVA: 0x0002DF2F File Offset: 0x0002C12F
		internal static bool IsNullableType(Type type)
		{
			return type != null && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0002DF59 File Offset: 0x0002C159
		internal static Type GetNonNullableType(Type type)
		{
			if (TypeHelper.IsNullableType(type))
			{
				return type.GetGenericArguments()[0];
			}
			return type;
		}
	}
}
