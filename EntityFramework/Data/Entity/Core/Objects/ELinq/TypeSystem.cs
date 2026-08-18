using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Core.Objects.ELinq
{
	// Token: 0x02000569 RID: 1385
	internal static class TypeSystem
	{
		// Token: 0x06003575 RID: 13685 RVA: 0x000FDC7C File Offset: 0x000FBE7C
		private static T GetDefault<T>()
		{
			return default(T);
		}

		// Token: 0x06003576 RID: 13686 RVA: 0x000FDC94 File Offset: 0x000FBE94
		internal static object GetDefaultValue(Type type)
		{
			if (!type.IsValueType() || (type.IsGenericType() && typeof(Nullable<>) == type.GetGenericTypeDefinition()))
			{
				return null;
			}
			MethodInfo methodInfo = TypeSystem.GetDefaultMethod.MakeGenericMethod(new Type[]
			{
				type
			});
			return methodInfo.Invoke(null, new object[0]);
		}

		// Token: 0x06003577 RID: 13687 RVA: 0x000FDCF0 File Offset: 0x000FBEF0
		internal static bool IsSequenceType(Type seqType)
		{
			return TypeSystem.FindIEnumerable(seqType) != null;
		}

		// Token: 0x06003578 RID: 13688 RVA: 0x000FDD00 File Offset: 0x000FBF00
		internal static Type GetDelegateType(IEnumerable<Type> inputTypes, Type returnType)
		{
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

		// Token: 0x06003579 RID: 13689 RVA: 0x000FDEC4 File Offset: 0x000FC0C4
		internal static Expression EnsureType(Expression expression, Type requiredType)
		{
			if (expression.Type != requiredType)
			{
				expression = Expression.Convert(expression, requiredType);
			}
			return expression;
		}

		// Token: 0x0600357A RID: 13690 RVA: 0x000FDEE0 File Offset: 0x000FC0E0
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
						foreach (PropertyInfo propertyInfo in methodInfo.DeclaringType.GetRuntimeProperties())
						{
							if (propertyInfo.CanRead && propertyInfo.Getter() == methodInfo)
							{
								return TypeSystem.PropertyOrField(propertyInfo, out name, out type);
							}
						}
					}
				}
				throw new NotSupportedException(Strings.ELinq_NotPropertyOrField(member.Name));
			}
			PropertyInfo propertyInfo2 = (PropertyInfo)member;
			if (propertyInfo2.GetIndexParameters().Length != 0)
			{
				throw new NotSupportedException(Strings.ELinq_PropertyIndexNotSupported);
			}
			name = propertyInfo2.Name;
			type = propertyInfo2.PropertyType;
			return propertyInfo2;
		}

		// Token: 0x0600357B RID: 13691 RVA: 0x000FDFE0 File Offset: 0x000FC1E0
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
			if (seqType.IsGenericType())
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
			if (interfaces != null && interfaces.Length > 0)
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
			if (seqType.BaseType() != null && seqType.BaseType() != typeof(object))
			{
				return TypeSystem.FindIEnumerable(seqType.BaseType());
			}
			return null;
		}

		// Token: 0x0600357C RID: 13692 RVA: 0x000FE11C File Offset: 0x000FC31C
		internal static Type GetElementType(Type seqType)
		{
			Type type = TypeSystem.FindIEnumerable(seqType);
			if (type == null)
			{
				return seqType;
			}
			return type.GetGenericArguments()[0];
		}

		// Token: 0x0600357D RID: 13693 RVA: 0x000FE143 File Offset: 0x000FC343
		internal static Type GetNonNullableType(Type type)
		{
			if (type != null)
			{
				return Nullable.GetUnderlyingType(type) ?? type;
			}
			return null;
		}

		// Token: 0x0600357E RID: 13694 RVA: 0x000FE15C File Offset: 0x000FC35C
		internal static bool IsImplementationOfGenericInterfaceMethod(this MethodInfo test, Type match, out Type[] genericTypeArguments)
		{
			genericTypeArguments = null;
			if (null == test || null == match || !match.IsInterface() || !match.IsGenericTypeDefinition() || null == test.DeclaringType)
			{
				return false;
			}
			if (test.DeclaringType.IsInterface() && test.DeclaringType.IsGenericType() && test.DeclaringType.GetGenericTypeDefinition() == match)
			{
				return true;
			}
			foreach (Type type in test.DeclaringType.GetInterfaces())
			{
				if (type.IsGenericType() && type.GetGenericTypeDefinition() == match && test.DeclaringType.GetInterfaceMap(type).TargetMethods.Contains(test))
				{
					genericTypeArguments = type.GetGenericArguments();
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600357F RID: 13695 RVA: 0x000FE234 File Offset: 0x000FC434
		internal static bool IsImplementationOf(this PropertyInfo propertyInfo, Type interfaceType)
		{
			PropertyInfo declaredProperty = interfaceType.GetDeclaredProperty(propertyInfo.Name);
			if (null == declaredProperty)
			{
				return false;
			}
			if (propertyInfo.DeclaringType.IsInterface())
			{
				return declaredProperty.Equals(propertyInfo);
			}
			bool result = false;
			MethodInfo value = declaredProperty.Getter();
			InterfaceMapping interfaceMap = propertyInfo.DeclaringType.GetInterfaceMap(interfaceType);
			int num = Array.IndexOf<MethodInfo>(interfaceMap.InterfaceMethods, value);
			MethodInfo[] targetMethods = interfaceMap.TargetMethods;
			if (num > -1 && num < targetMethods.Length)
			{
				MethodInfo methodInfo = propertyInfo.Getter();
				if (methodInfo != null)
				{
					result = methodInfo.Equals(targetMethods[num]);
				}
			}
			return result;
		}

		// Token: 0x040014AF RID: 5295
		internal static readonly MethodInfo GetDefaultMethod = typeof(TypeSystem).GetOnlyDeclaredMethod("GetDefault");
	}
}
