using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.Practices.Unity.Utility
{
	// Token: 0x02000007 RID: 7
	public static class TypeReflectionExtensions
	{
		// Token: 0x0600000C RID: 12 RVA: 0x0000237C File Offset: 0x0000057C
		public static ConstructorInfo GetConstructor(this Type type, params Type[] constructorParameters)
		{
			return type.GetTypeInfo().DeclaredConstructors.Single((ConstructorInfo c) => !c.IsStatic && TypeReflectionExtensions.ParametersMatch(c.GetParameters(), constructorParameters));
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000023C8 File Offset: 0x000005C8
		public static IEnumerable<MethodInfo> GetMethodsHierarchical(this Type type)
		{
			if (type == null)
			{
				return Enumerable.Empty<MethodInfo>();
			}
			if (type.Equals(typeof(object)))
			{
				return from m in type.GetTypeInfo().DeclaredMethods
				where !m.IsStatic
				select m;
			}
			return (from m in type.GetTypeInfo().DeclaredMethods
			where !m.IsStatic
			select m).Concat(type.GetTypeInfo().BaseType.GetMethodsHierarchical());
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002490 File Offset: 0x00000690
		public static MethodInfo GetMethodHierarchical(this Type type, string methodName, Type[] closedParameters)
		{
			return type.GetMethodsHierarchical().Single((MethodInfo m) => m.Name.Equals(methodName) && TypeReflectionExtensions.ParametersMatch(m.GetParameters(), closedParameters));
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000024C8 File Offset: 0x000006C8
		public static IEnumerable<PropertyInfo> GetPropertiesHierarchical(this Type type)
		{
			if (type == null)
			{
				return Enumerable.Empty<PropertyInfo>();
			}
			if (type.Equals(typeof(object)))
			{
				return type.GetTypeInfo().DeclaredProperties;
			}
			return type.GetTypeInfo().DeclaredProperties.Concat(type.GetTypeInfo().BaseType.GetPropertiesHierarchical());
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000251C File Offset: 0x0000071C
		public static bool ParametersMatch(ParameterInfo[] parameters, Type[] closedConstructorParameterTypes)
		{
			Guard.ArgumentNotNull(parameters, "parameters");
			Guard.ArgumentNotNull(closedConstructorParameterTypes, "closedConstructorParameterTypes");
			if (parameters.Length != closedConstructorParameterTypes.Length)
			{
				return false;
			}
			for (int i = 0; i < parameters.Length; i++)
			{
				if (!parameters[i].ParameterType.Equals(closedConstructorParameterTypes[i]))
				{
					return false;
				}
			}
			return true;
		}
	}
}
