using System;
using System.Linq;

namespace System.Web.ModelBinding
{
	// Token: 0x0200068D RID: 1677
	internal static class TypeHelpers
	{
		// Token: 0x06005118 RID: 20760 RVA: 0x00117708 File Offset: 0x00115908
		public static Type ExtractGenericInterface(Type queryType, Type interfaceType)
		{
			Func<Type, bool> func = (Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == interfaceType;
			if (!func(queryType))
			{
				return queryType.GetInterfaces().FirstOrDefault(func);
			}
			return queryType;
		}

		// Token: 0x06005119 RID: 20761 RVA: 0x00117748 File Offset: 0x00115948
		public static Type[] GetTypeArgumentsIfMatch(Type closedType, Type matchingOpenType)
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

		// Token: 0x0600511A RID: 20762 RVA: 0x00117777 File Offset: 0x00115977
		public static bool IsCompatibleObject(Type type, object value)
		{
			return (value == null && TypeHelpers.TypeAllowsNullValue(type)) || type.IsInstanceOfType(value);
		}

		// Token: 0x0600511B RID: 20763 RVA: 0x0011778D File Offset: 0x0011598D
		public static bool IsNullableValueType(Type type)
		{
			return Nullable.GetUnderlyingType(type) != null;
		}

		// Token: 0x0600511C RID: 20764 RVA: 0x0011779B File Offset: 0x0011599B
		public static bool TypeAllowsNullValue(Type type)
		{
			return !type.IsValueType || TypeHelpers.IsNullableValueType(type);
		}
	}
}
