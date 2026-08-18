using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Data.Objects.ELinq
{
	// Token: 0x020001A5 RID: 421
	internal static class TypeSystem
	{
		// Token: 0x06001E69 RID: 7785 RVA: 0x00069EEC File Offset: 0x000680EC
		private static T GetDefault<T>()
		{
			return default(T);
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x00069F04 File Offset: 0x00068104
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		internal static object GetDefaultValue(Type type)
		{
			if (!type.IsValueType || (type.IsGenericType && typeof(Nullable<>) == type.GetGenericTypeDefinition()))
			{
				return null;
			}
			MethodInfo methodInfo = TypeSystem.s_getDefaultMethod.MakeGenericMethod(new Type[]
			{
				type
			});
			return methodInfo.Invoke(null, new object[0]);
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x00069F5E File Offset: 0x0006815E
		internal static bool IsSequenceType(Type seqType)
		{
			return TypeSystem.FindIEnumerable(seqType) != null;
		}

		// Token: 0x06001E6C RID: 7788 RVA: 0x00069F6C File Offset: 0x0006816C
		internal static Type GetDelegateType(IEnumerable<Type> inputTypes, Type returnType)
		{
			EntityUtil.CheckArgumentNull<Type>(returnType, "returnType");
			inputTypes = (inputTypes ?? Enumerable.Empty<Type>());
			int num = inputTypes.Count<Type>();
			Type[] array = new Type[num + 1];
			int num2 = 0;
			foreach (Type type in inputTypes)
			{
				array[num2++] = type;
			}
			array[num2] = returnType;
			Type type2;
			switch (num)
			{
			case 0:
				type2 = typeof(Func<>);
				break;
			case 1:
				type2 = typeof(Func<, >);
				break;
			case 2:
				type2 = typeof(Func<, , >);
				break;
			case 3:
				type2 = typeof(Func<, , , >);
				break;
			case 4:
				type2 = typeof(Func<, , , , >);
				break;
			case 5:
				type2 = typeof(Func<, , , , , >);
				break;
			case 6:
				type2 = typeof(Func<, , , , , , >);
				break;
			case 7:
				type2 = typeof(Func<, , , , , , , >);
				break;
			case 8:
				type2 = typeof(Func<, , , , , , , , >);
				break;
			case 9:
				type2 = typeof(Func<, , , , , , , , , >);
				break;
			case 10:
				type2 = typeof(Func<, , , , , , , , , , >);
				break;
			case 11:
				type2 = typeof(Func<, , , , , , , , , , , >);
				break;
			case 12:
				type2 = typeof(Func<, , , , , , , , , , , , >);
				break;
			case 13:
				type2 = typeof(Func<, , , , , , , , , , , , , >);
				break;
			case 14:
				type2 = typeof(Func<, , , , , , , , , , , , , , >);
				break;
			case 15:
				type2 = typeof(Func<, , , , , , , , , , , , , , , >);
				break;
			default:
				type2 = null;
				break;
			}
			return type2.MakeGenericType(array);
		}

		// Token: 0x06001E6D RID: 7789 RVA: 0x0006A124 File Offset: 0x00068324
		internal static Expression EnsureType(Expression expression, Type requiredType)
		{
			if (expression.Type != requiredType)
			{
				expression = Expression.Convert(expression, requiredType);
			}
			return expression;
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x0006A140 File Offset: 0x00068340
		internal static MemberInfo PropertyOrField(MemberInfo member, out string name, out Type type)
		{
			name = null;
			type = null;
			if (member.MemberType == MemberTypes.Field)
			{
				FieldInfo fieldInfo = (FieldInfo)member;
				name = fieldInfo.Name;
				type = fieldInfo.FieldType;
				return fieldInfo;
			}
			if (member.MemberType != MemberTypes.Property)
			{
				if (member.MemberType == MemberTypes.Method)
				{
					MethodInfo methodInfo = (MethodInfo)member;
					if (methodInfo.IsSpecialName)
					{
						foreach (PropertyInfo propertyInfo in methodInfo.DeclaringType.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
						{
							if (propertyInfo.CanRead && propertyInfo.GetGetMethod(true) == methodInfo)
							{
								return TypeSystem.PropertyOrField(propertyInfo, out name, out type);
							}
						}
					}
				}
				throw EntityUtil.NotSupported(Strings.ELinq_NotPropertyOrField(member.Name));
			}
			PropertyInfo propertyInfo2 = (PropertyInfo)member;
			if (propertyInfo2.GetIndexParameters().Length != 0)
			{
				throw EntityUtil.NotSupported(Strings.ELinq_PropertyIndexNotSupported);
			}
			name = propertyInfo2.Name;
			type = propertyInfo2.PropertyType;
			return propertyInfo2;
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x0006A220 File Offset: 0x00068420
		private static Type FindIEnumerable(Type seqType)
		{
			if (seqType == null || seqType == typeof(string) || seqType == typeof(byte[]))
			{
				return null;
			}
			if (seqType.IsArray)
			{
				return typeof(IEnumerable<>).MakeGenericType(new Type[]
				{
					seqType.GetElementType()
				});
			}
			if (seqType.IsGenericType)
			{
				foreach (Type type in seqType.GetGenericArguments())
				{
					Type type2 = typeof(IEnumerable<>).MakeGenericType(new Type[]
					{
						type
					});
					if (type2.IsAssignableFrom(seqType))
					{
						return type2;
					}
				}
			}
			Type[] interfaces = seqType.GetInterfaces();
			if (interfaces != null && interfaces.Length != 0)
			{
				foreach (Type seqType2 in interfaces)
				{
					Type type3 = TypeSystem.FindIEnumerable(seqType2);
					if (type3 != null)
					{
						return type3;
					}
				}
			}
			if (seqType.BaseType != null && seqType.BaseType != typeof(object))
			{
				return TypeSystem.FindIEnumerable(seqType.BaseType);
			}
			return null;
		}

		// Token: 0x06001E70 RID: 7792 RVA: 0x0006A340 File Offset: 0x00068540
		internal static Type GetElementType(Type seqType)
		{
			Type type = TypeSystem.FindIEnumerable(seqType);
			if (type == null)
			{
				return seqType;
			}
			return type.GetGenericArguments()[0];
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x0006A368 File Offset: 0x00068568
		internal static bool IsNullableType(Type type)
		{
			Type nonNullableType = TypeSystem.GetNonNullableType(type);
			return nonNullableType != null && nonNullableType != type;
		}

		// Token: 0x06001E72 RID: 7794 RVA: 0x0006A38E File Offset: 0x0006858E
		internal static Type GetNonNullableType(Type type)
		{
			if (type != null)
			{
				return Nullable.GetUnderlyingType(type) ?? type;
			}
			return null;
		}

		// Token: 0x06001E73 RID: 7795 RVA: 0x0006A3A8 File Offset: 0x000685A8
		internal static bool IsImplementationOfGenericInterfaceMethod(this MethodInfo test, Type match, out Type[] genericTypeArguments)
		{
			genericTypeArguments = null;
			if (null == test || null == match || !match.IsInterface || !match.IsGenericTypeDefinition || null == test.DeclaringType)
			{
				return false;
			}
			if (test.DeclaringType.IsInterface && test.DeclaringType.IsGenericType && test.DeclaringType.GetGenericTypeDefinition() == match)
			{
				return true;
			}
			foreach (Type type in test.DeclaringType.GetInterfaces())
			{
				if (type.IsGenericType && type.GetGenericTypeDefinition() == match)
				{
					InterfaceMapping interfaceMap = test.DeclaringType.GetInterfaceMap(type);
					if (interfaceMap.TargetMethods.Contains(test))
					{
						genericTypeArguments = type.GetGenericArguments();
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001E74 RID: 7796 RVA: 0x0006A474 File Offset: 0x00068674
		internal static bool IsImplementationOf(this PropertyInfo propertyInfo, Type interfaceType)
		{
			PropertyInfo property = interfaceType.GetProperty(propertyInfo.Name, BindingFlags.Instance | BindingFlags.Public);
			if (null == property)
			{
				return false;
			}
			if (propertyInfo.DeclaringType.IsInterface)
			{
				return property.Equals(propertyInfo);
			}
			bool result = false;
			MethodInfo getMethod = property.GetGetMethod();
			InterfaceMapping interfaceMap = propertyInfo.DeclaringType.GetInterfaceMap(interfaceType);
			int num = Array.IndexOf<MethodInfo>(interfaceMap.InterfaceMethods, getMethod);
			MethodInfo[] targetMethods = interfaceMap.TargetMethods;
			if (num > -1 && num < targetMethods.Length)
			{
				MethodInfo getMethod2 = propertyInfo.GetGetMethod();
				if (getMethod2 != null)
				{
					result = getMethod2.Equals(targetMethods[num]);
				}
			}
			return result;
		}

		// Token: 0x04000CCB RID: 3275
		private static readonly MethodInfo s_getDefaultMethod = typeof(TypeSystem).GetMethod("GetDefault", BindingFlags.Static | BindingFlags.NonPublic);
	}
}
