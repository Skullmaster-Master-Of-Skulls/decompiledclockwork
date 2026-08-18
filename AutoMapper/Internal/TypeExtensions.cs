using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace AutoMapper.Internal
{
	// Token: 0x020000BD RID: 189
	internal static class TypeExtensions
	{
		// Token: 0x06000584 RID: 1412 RVA: 0x00014F4F File Offset: 0x0001314F
		public static Type[] GetGenericParameters(this Type type)
		{
			return type.GetGenericTypeDefinition().GetTypeInfo().GenericTypeParameters;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00014F61 File Offset: 0x00013161
		public static IEnumerable<ConstructorInfo> GetDeclaredConstructors(this Type type)
		{
			return type.GetTypeInfo().DeclaredConstructors;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00014F6E File Offset: 0x0001316E
		public static Type CreateType(this TypeBuilder type)
		{
			return type.CreateTypeInfo().AsType();
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00014F7B File Offset: 0x0001317B
		public static IEnumerable<MemberInfo> GetDeclaredMembers(this Type type)
		{
			return type.GetTypeInfo().DeclaredMembers;
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00014F88 File Offset: 0x00013188
		public static IEnumerable<MethodInfo> GetDeclaredMethods(this Type type)
		{
			return type.GetTypeInfo().DeclaredMethods;
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00014F95 File Offset: 0x00013195
		public static IEnumerable<MethodInfo> GetAllMethods(this Type type)
		{
			return type.GetRuntimeMethods();
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00014F9D File Offset: 0x0001319D
		public static IEnumerable<PropertyInfo> GetDeclaredProperties(this Type type)
		{
			return type.GetTypeInfo().DeclaredProperties;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00014FAA File Offset: 0x000131AA
		public static object[] GetCustomAttributes(this Type type, Type attributeType, bool inherit)
		{
			return type.GetTypeInfo().GetCustomAttributes(attributeType, inherit).ToArray<object>();
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00014FBE File Offset: 0x000131BE
		public static bool IsStatic(this FieldInfo fieldInfo)
		{
			return fieldInfo != null && fieldInfo.IsStatic;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00014FCC File Offset: 0x000131CC
		public static bool IsStatic(this PropertyInfo propertyInfo)
		{
			bool? flag;
			if (propertyInfo == null)
			{
				flag = null;
			}
			else
			{
				MethodInfo getMethod = propertyInfo.GetGetMethod(true);
				flag = ((getMethod != null) ? new bool?(getMethod.IsStatic) : null);
			}
			bool? flag2 = flag;
			if (flag2 == null)
			{
				bool? flag3;
				if (propertyInfo == null)
				{
					flag3 = null;
				}
				else
				{
					MethodInfo setMethod = propertyInfo.GetSetMethod(true);
					flag3 = ((setMethod != null) ? new bool?(setMethod.IsStatic) : null);
				}
				return flag3 ?? false;
			}
			return flag2.GetValueOrDefault();
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0001505A File Offset: 0x0001325A
		public static bool IsStatic(this MemberInfo memberInfo)
		{
			return (memberInfo as FieldInfo).IsStatic() || (memberInfo as PropertyInfo).IsStatic();
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00015078 File Offset: 0x00013278
		public static bool IsPublic(this PropertyInfo propertyInfo)
		{
			bool? flag;
			if (propertyInfo == null)
			{
				flag = null;
			}
			else
			{
				MethodInfo getMethod = propertyInfo.GetGetMethod(true);
				flag = ((getMethod != null) ? new bool?(getMethod.IsPublic) : null);
			}
			if (!(flag ?? false))
			{
				bool? flag2;
				if (propertyInfo == null)
				{
					flag2 = null;
				}
				else
				{
					MethodInfo setMethod = propertyInfo.GetSetMethod(true);
					flag2 = ((setMethod != null) ? new bool?(setMethod.IsPublic) : null);
				}
				return flag2 ?? false;
			}
			return true;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0001510C File Offset: 0x0001330C
		public static bool IsPublic(this MemberInfo memberInfo)
		{
			FieldInfo fieldInfo = memberInfo as FieldInfo;
			if (fieldInfo == null)
			{
				return (memberInfo as PropertyInfo).IsPublic();
			}
			return fieldInfo.IsPublic;
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00015129 File Offset: 0x00013329
		public static bool IsNotPublic(this ConstructorInfo constructorInfo)
		{
			return constructorInfo.IsPrivate || constructorInfo.IsFamilyAndAssembly || constructorInfo.IsFamilyOrAssembly || constructorInfo.IsFamily;
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0001514B File Offset: 0x0001334B
		public static Assembly Assembly(this Type type)
		{
			return type.GetTypeInfo().Assembly;
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00015158 File Offset: 0x00013358
		public static Type BaseType(this Type type)
		{
			return type.GetTypeInfo().BaseType;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00015165 File Offset: 0x00013365
		public static bool IsAbstract(this Type type)
		{
			return type.GetTypeInfo().IsAbstract;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00015172 File Offset: 0x00013372
		public static bool IsClass(this Type type)
		{
			return type.GetTypeInfo().IsClass;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0001517F File Offset: 0x0001337F
		public static bool IsEnum(this Type type)
		{
			return type.GetTypeInfo().IsEnum;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0001518C File Offset: 0x0001338C
		public static bool IsGenericType(this Type type)
		{
			return type.GetTypeInfo().IsGenericType;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00015199 File Offset: 0x00013399
		public static bool IsGenericTypeDefinition(this Type type)
		{
			return type.GetTypeInfo().IsGenericTypeDefinition;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x000151A6 File Offset: 0x000133A6
		public static bool IsInterface(this Type type)
		{
			return type.GetTypeInfo().IsInterface;
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x000151B3 File Offset: 0x000133B3
		public static bool IsPrimitive(this Type type)
		{
			return type.GetTypeInfo().IsPrimitive;
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x000151C0 File Offset: 0x000133C0
		public static bool IsSealed(this Type type)
		{
			return type.GetTypeInfo().IsSealed;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x000151CD File Offset: 0x000133CD
		public static bool IsValueType(this Type type)
		{
			return type.GetTypeInfo().IsValueType;
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x000151DA File Offset: 0x000133DA
		public static bool IsInstanceOfType(this Type type, object o)
		{
			return o != null && type.IsAssignableFrom(o.GetType());
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x000151ED File Offset: 0x000133ED
		public static ConstructorInfo[] GetConstructors(this Type type)
		{
			return type.GetTypeInfo().DeclaredConstructors.ToArray<ConstructorInfo>();
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x000151FF File Offset: 0x000133FF
		public static MethodInfo GetGetMethod(this PropertyInfo propertyInfo, bool ignored)
		{
			return propertyInfo.GetMethod;
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00015207 File Offset: 0x00013407
		public static MethodInfo GetSetMethod(this PropertyInfo propertyInfo, bool ignored)
		{
			return propertyInfo.SetMethod;
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0001520F File Offset: 0x0001340F
		public static FieldInfo GetField(this Type type, string name)
		{
			return type.GetRuntimeField(name);
		}
	}
}
