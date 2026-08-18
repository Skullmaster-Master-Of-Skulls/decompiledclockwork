using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Telerik.Web.Data
{
	// Token: 0x02001BA9 RID: 7081
	internal static class TypeExtensions
	{
		// Token: 0x060111F6 RID: 70134 RVA: 0x003C69A0 File Offset: 0x003C4BA0
		internal static bool IsPredefinedType(this Type type)
		{
			foreach (Type left in TypeExtensions.PredefinedTypes)
			{
				if (left == type)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060111F7 RID: 70135 RVA: 0x003C69E4 File Offset: 0x003C4BE4
		internal static string FirstSortablePropertyName(this Type type)
		{
			PropertyInfo propertyInfo = (from property in type.GetProperties()
			where property.PropertyType.IsPredefinedType()
			select property).FirstOrDefault<PropertyInfo>();
			if (propertyInfo == null)
			{
				throw new NotSupportedException("CannotFindPropertyToSortBy");
			}
			return propertyInfo.Name;
		}

		// Token: 0x060111F8 RID: 70136 RVA: 0x003C6A39 File Offset: 0x003C4C39
		internal static bool IsNullableType(this Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x060111F9 RID: 70137 RVA: 0x003C6A5A File Offset: 0x003C4C5A
		internal static Type GetNonNullableType(this Type type)
		{
			if (!type.IsNullableType())
			{
				return type;
			}
			return type.GetGenericArguments()[0];
		}

		// Token: 0x060111FA RID: 70138 RVA: 0x003C6A70 File Offset: 0x003C4C70
		internal static string GetTypeName(this Type type)
		{
			Type nonNullableType = type.GetNonNullableType();
			string text = nonNullableType.Name;
			if (type != nonNullableType)
			{
				text += '?';
			}
			return text;
		}

		// Token: 0x060111FB RID: 70139 RVA: 0x003C6AA4 File Offset: 0x003C4CA4
		internal static bool InheritsFrom(this Type type, string typeName)
		{
			Type type2 = type;
			while (type2 != null)
			{
				if (type2.GetTypeName() == typeName)
				{
					return true;
				}
				type2 = type2.BaseType;
			}
			return false;
		}

		// Token: 0x060111FC RID: 70140 RVA: 0x003C6AD6 File Offset: 0x003C4CD6
		internal static bool IsNumericType(this Type type)
		{
			return type.GetNumericTypeKind() != 0;
		}

		// Token: 0x060111FD RID: 70141 RVA: 0x003C6AE4 File Offset: 0x003C4CE4
		internal static bool IsSignedIntegralType(this Type type)
		{
			return type.GetNumericTypeKind() == 2;
		}

		// Token: 0x060111FE RID: 70142 RVA: 0x003C6AEF File Offset: 0x003C4CEF
		internal static bool IsUnsignedIntegralType(this Type type)
		{
			return type.GetNumericTypeKind() == 3;
		}

		// Token: 0x060111FF RID: 70143 RVA: 0x003C6AFC File Offset: 0x003C4CFC
		internal static int GetNumericTypeKind(this Type type)
		{
			type = type.GetNonNullableType();
			if (type.IsEnum)
			{
				return 0;
			}
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Char:
			case TypeCode.Single:
			case TypeCode.Double:
			case TypeCode.Decimal:
				return 1;
			case TypeCode.SByte:
			case TypeCode.Int16:
			case TypeCode.Int32:
			case TypeCode.Int64:
				return 2;
			case TypeCode.Byte:
			case TypeCode.UInt16:
			case TypeCode.UInt32:
			case TypeCode.UInt64:
				return 3;
			default:
				return 0;
			}
		}

		// Token: 0x06011200 RID: 70144 RVA: 0x003C6B80 File Offset: 0x003C4D80
		internal static PropertyInfo GetIndexerPropertyInfo(this Type type, params Type[] indexerArguments)
		{
			return (from p in type.GetProperties()
			where TypeExtensions.AreArgumentsApplicable(indexerArguments, p.GetIndexParameters())
			select p).FirstOrDefault<PropertyInfo>();
		}

		// Token: 0x06011201 RID: 70145 RVA: 0x003C6BB8 File Offset: 0x003C4DB8
		private static bool AreArgumentsApplicable(IEnumerable<Type> arguments, IEnumerable<ParameterInfo> parameters)
		{
			List<Type> list = arguments.ToList<Type>();
			List<ParameterInfo> list2 = parameters.ToList<ParameterInfo>();
			if (list.Count != list2.Count)
			{
				return false;
			}
			for (int i = 0; i < list.Count; i++)
			{
				if (list2[i].ParameterType != list[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06011202 RID: 70146 RVA: 0x003C6C11 File Offset: 0x003C4E11
		internal static bool IsEnumType(this Type type)
		{
			return type.GetNonNullableType().IsEnum;
		}

		// Token: 0x06011203 RID: 70147 RVA: 0x003C6C20 File Offset: 0x003C4E20
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		internal static bool IsCompatibleWith(this Type source, Type target)
		{
			if (source == target)
			{
				return true;
			}
			if (!target.IsValueType)
			{
				return target.IsAssignableFrom(source);
			}
			Type nonNullableType = source.GetNonNullableType();
			Type nonNullableType2 = target.GetNonNullableType();
			if (nonNullableType != source && nonNullableType2 == target)
			{
				return false;
			}
			TypeCode typeCode = nonNullableType.IsEnum ? TypeCode.Object : Type.GetTypeCode(nonNullableType);
			TypeCode typeCode2 = nonNullableType2.IsEnum ? TypeCode.Object : Type.GetTypeCode(nonNullableType2);
			switch (typeCode)
			{
			case TypeCode.SByte:
				switch (typeCode2)
				{
				case TypeCode.SByte:
				case TypeCode.Int16:
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					return true;
				}
				break;
			case TypeCode.Byte:
				switch (typeCode2)
				{
				case TypeCode.Byte:
				case TypeCode.Int16:
				case TypeCode.UInt16:
				case TypeCode.Int32:
				case TypeCode.UInt32:
				case TypeCode.Int64:
				case TypeCode.UInt64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					return true;
				}
				break;
			case TypeCode.Int16:
				switch (typeCode2)
				{
				case TypeCode.Int16:
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					return true;
				}
				break;
			case TypeCode.UInt16:
				switch (typeCode2)
				{
				case TypeCode.UInt16:
				case TypeCode.Int32:
				case TypeCode.UInt32:
				case TypeCode.Int64:
				case TypeCode.UInt64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					return true;
				}
				break;
			case TypeCode.Int32:
				switch (typeCode2)
				{
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					return true;
				}
				break;
			case TypeCode.UInt32:
				switch (typeCode2)
				{
				case TypeCode.UInt32:
				case TypeCode.Int64:
				case TypeCode.UInt64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					return true;
				}
				break;
			case TypeCode.Int64:
				switch (typeCode2)
				{
				case TypeCode.Int64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					return true;
				}
				break;
			case TypeCode.UInt64:
				switch (typeCode2)
				{
				case TypeCode.UInt64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					return true;
				}
				break;
			case TypeCode.Single:
				switch (typeCode2)
				{
				case TypeCode.Single:
				case TypeCode.Double:
					return true;
				}
				break;
			default:
				if (nonNullableType == nonNullableType2)
				{
					return true;
				}
				break;
			}
			return false;
		}

		// Token: 0x06011204 RID: 70148 RVA: 0x003C6E6C File Offset: 0x003C506C
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

		// Token: 0x06011205 RID: 70149 RVA: 0x003C6EF4 File Offset: 0x003C50F4
		internal static object DefaultValue(this Type type)
		{
			if (type.IsValueType)
			{
				return Activator.CreateInstance(type);
			}
			return null;
		}

		// Token: 0x06011206 RID: 70150 RVA: 0x003C6F08 File Offset: 0x003C5108
		internal static MemberInfo FindPropertyOrField(this Type type, string memberName)
		{
			MemberInfo memberInfo = type.FindPropertyOrField(memberName, false);
			if (memberInfo == null)
			{
				memberInfo = type.FindPropertyOrField(memberName, true);
			}
			return memberInfo;
		}

		// Token: 0x06011207 RID: 70151 RVA: 0x003C6F34 File Offset: 0x003C5134
		internal static MemberInfo FindPropertyOrField(this Type type, string memberName, bool staticAccess)
		{
			BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | (staticAccess ? BindingFlags.Static : BindingFlags.Instance);
			foreach (Type type2 in type.SelfAndBaseTypes())
			{
				MemberInfo[] array = type2.FindMembers(MemberTypes.Field | MemberTypes.Property, bindingAttr, Type.FilterNameIgnoreCase, memberName);
				if (array.Length != 0)
				{
					return array[0];
				}
			}
			return null;
		}

		// Token: 0x06011208 RID: 70152 RVA: 0x003C6FA8 File Offset: 0x003C51A8
		internal static IEnumerable<Type> SelfAndBaseTypes(this Type type)
		{
			if (type.IsInterface)
			{
				List<Type> list = new List<Type>();
				TypeExtensions.AddInterface(list, type);
				return list;
			}
			return type.SelfAndBaseClasses();
		}

		// Token: 0x06011209 RID: 70153 RVA: 0x003C70BC File Offset: 0x003C52BC
		internal static IEnumerable<Type> SelfAndBaseClasses(this Type type)
		{
			while (type != null)
			{
				yield return type;
				type = type.BaseType;
			}
			yield break;
		}

		// Token: 0x0601120A RID: 70154 RVA: 0x003C70DC File Offset: 0x003C52DC
		private static void AddInterface(List<Type> types, Type type)
		{
			if (!types.Contains(type))
			{
				types.Add(type);
				foreach (Type type2 in type.GetInterfaces())
				{
					TypeExtensions.AddInterface(types, type2);
				}
			}
		}

		// Token: 0x0601120B RID: 70155 RVA: 0x003C711C File Offset: 0x003C531C
		internal static bool CanSort(this Type source)
		{
			if (source == null)
			{
				return false;
			}
			bool result = false;
			if (typeof(IComparable).IsAssignableFrom(source))
			{
				result = true;
			}
			else
			{
				Type underlyingType = Nullable.GetUnderlyingType(source);
				if (underlyingType != null && typeof(IComparable).IsAssignableFrom(underlyingType))
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0601120C RID: 70156 RVA: 0x003C7174 File Offset: 0x003C5374
		internal static bool CanGroup(this Type source)
		{
			if (source == null)
			{
				return false;
			}
			Type source2 = Nullable.GetUnderlyingType(source) ?? source;
			return TypeExtensions.ImplementsIEquatable(source2) || TypeExtensions.ImplementsIComparable(source2);
		}

		// Token: 0x0601120D RID: 70157 RVA: 0x003C71A8 File Offset: 0x003C53A8
		internal static bool CanFilter(this Type source)
		{
			if (source == null)
			{
				return false;
			}
			Type source2 = Nullable.GetUnderlyingType(source) ?? source;
			return TypeExtensions.ImplementsIEquatable(source2) || TypeExtensions.ImplementsIComparable(source2);
		}

		// Token: 0x0601120E RID: 70158 RVA: 0x003C71DC File Offset: 0x003C53DC
		private static bool ImplementsIEquatable(Type source)
		{
			if (source == null)
			{
				return false;
			}
			Type typeFromHandle = typeof(IEquatable<>);
			Type type = typeFromHandle.MakeGenericType(new Type[]
			{
				source
			});
			return type.IsAssignableFrom(source);
		}

		// Token: 0x0601120F RID: 70159 RVA: 0x003C721C File Offset: 0x003C541C
		private static bool ImplementsIComparable(Type source)
		{
			if (source == null)
			{
				return false;
			}
			foreach (Type left in source.GetInterfaces())
			{
				if (left == typeof(IComparable))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04004CAF RID: 19631
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
