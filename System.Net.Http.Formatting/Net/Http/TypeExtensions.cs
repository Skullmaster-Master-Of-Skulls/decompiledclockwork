using System;
using System.Linq;

namespace System.Net.Http
{
	// Token: 0x0200001C RID: 28
	internal static class TypeExtensions
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x00004D3C File Offset: 0x00002F3C
		public static Type ExtractGenericInterface(this Type queryType, Type interfaceType)
		{
			Func<Type, bool> func = (Type t) => t.IsGenericType() && t.GetGenericTypeDefinition() == interfaceType;
			if (!func(queryType))
			{
				return queryType.GetInterfaces().FirstOrDefault(func);
			}
			return queryType;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004D7A File Offset: 0x00002F7A
		public static bool IsGenericType(this Type type)
		{
			return type.IsGenericType;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004D82 File Offset: 0x00002F82
		public static bool IsInterface(this Type type)
		{
			return type.IsInterface;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004D8A File Offset: 0x00002F8A
		public static bool IsValueType(this Type type)
		{
			return type.IsValueType;
		}
	}
}
