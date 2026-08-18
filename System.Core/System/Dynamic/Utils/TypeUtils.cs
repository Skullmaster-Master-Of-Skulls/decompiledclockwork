using System;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Dynamic.Utils
{
	// Token: 0x020000D2 RID: 210
	internal static class TypeUtils
	{
		// Token: 0x0600065A RID: 1626 RVA: 0x00014AB9 File Offset: 0x00012CB9
		internal static Type GetNonNullableType(this Type type)
		{
			if (type.IsNullableType())
			{
				return type.GetGenericArguments()[0];
			}
			return type;
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00014ACD File Offset: 0x00012CCD
		internal static Type GetNullableType(Type type)
		{
			if (type.IsValueType && !type.IsNullableType())
			{
				return typeof(Nullable<>).MakeGenericType(new Type[]
				{
					type
				});
			}
			return type;
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00014AFA File Offset: 0x00012CFA
		internal static bool IsNullableType(this Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00014B1B File Offset: 0x00012D1B
		internal static bool IsBool(Type type)
		{
			return type.GetNonNullableType() == typeof(bool);
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00014B34 File Offset: 0x00012D34
		internal static bool IsNumeric(Type type)
		{
			type = type.GetNonNullableType();
			if (!type.IsEnum)
			{
				TypeCode typeCode = Type.GetTypeCode(type);
				if (typeCode - TypeCode.Char <= 10)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00014B64 File Offset: 0x00012D64
		internal static bool IsInteger(Type type)
		{
			type = type.GetNonNullableType();
			if (type.IsEnum)
			{
				return false;
			}
			TypeCode typeCode = Type.GetTypeCode(type);
			return typeCode - TypeCode.SByte <= 7;
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00014B94 File Offset: 0x00012D94
		internal static bool IsArithmetic(Type type)
		{
			type = type.GetNonNullableType();
			if (!type.IsEnum)
			{
				TypeCode typeCode = Type.GetTypeCode(type);
				if (typeCode - TypeCode.Int16 <= 7)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00014BC4 File Offset: 0x00012DC4
		internal static bool IsUnsignedInt(Type type)
		{
			type = type.GetNonNullableType();
			if (!type.IsEnum)
			{
				switch (Type.GetTypeCode(type))
				{
				case TypeCode.UInt16:
				case TypeCode.UInt32:
				case TypeCode.UInt64:
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00014C0C File Offset: 0x00012E0C
		internal static bool IsIntegerOrBool(Type type)
		{
			type = type.GetNonNullableType();
			if (!type.IsEnum)
			{
				TypeCode typeCode = Type.GetTypeCode(type);
				if (typeCode == TypeCode.Boolean || typeCode - TypeCode.SByte <= 7)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00014C3D File Offset: 0x00012E3D
		internal static bool AreEquivalent(Type t1, Type t2)
		{
			return t1 == t2 || t1.IsEquivalentTo(t2);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00014C51 File Offset: 0x00012E51
		internal static bool AreReferenceAssignable(Type dest, Type src)
		{
			return TypeUtils.AreEquivalent(dest, src) || (!dest.IsValueType && !src.IsValueType && dest.IsAssignableFrom(src));
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00014C7C File Offset: 0x00012E7C
		internal static bool IsValidInstanceType(MemberInfo member, Type instanceType)
		{
			Type declaringType = member.DeclaringType;
			if (TypeUtils.AreReferenceAssignable(declaringType, instanceType))
			{
				return true;
			}
			if (instanceType.IsValueType)
			{
				if (TypeUtils.AreReferenceAssignable(declaringType, typeof(object)))
				{
					return true;
				}
				if (TypeUtils.AreReferenceAssignable(declaringType, typeof(ValueType)))
				{
					return true;
				}
				if (instanceType.IsEnum && TypeUtils.AreReferenceAssignable(declaringType, typeof(Enum)))
				{
					return true;
				}
				if (declaringType.IsInterface)
				{
					foreach (Type src in instanceType.GetInterfaces())
					{
						if (TypeUtils.AreReferenceAssignable(declaringType, src))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00014D14 File Offset: 0x00012F14
		internal static bool HasIdentityPrimitiveOrNullableConversion(Type source, Type dest)
		{
			return TypeUtils.AreEquivalent(source, dest) || (source.IsNullableType() && TypeUtils.AreEquivalent(dest, source.GetNonNullableType())) || (dest.IsNullableType() && TypeUtils.AreEquivalent(source, dest.GetNonNullableType())) || (TypeUtils.IsConvertible(source) && TypeUtils.IsConvertible(dest) && dest.GetNonNullableType() != typeof(bool));
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00014D88 File Offset: 0x00012F88
		internal static bool HasReferenceConversion(Type source, Type dest)
		{
			if (source == typeof(void) || dest == typeof(void))
			{
				return false;
			}
			Type nonNullableType = source.GetNonNullableType();
			Type nonNullableType2 = dest.GetNonNullableType();
			return nonNullableType.IsAssignableFrom(nonNullableType2) || nonNullableType2.IsAssignableFrom(nonNullableType) || (source.IsInterface || dest.IsInterface) || TypeUtils.IsLegalExplicitVariantDelegateConversion(source, dest) || (source == typeof(object) || dest == typeof(object));
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00014E23 File Offset: 0x00013023
		private static bool IsCovariant(Type t)
		{
			return (t.GenericParameterAttributes & GenericParameterAttributes.Covariant) > GenericParameterAttributes.None;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00014E30 File Offset: 0x00013030
		private static bool IsContravariant(Type t)
		{
			return (t.GenericParameterAttributes & GenericParameterAttributes.Contravariant) > GenericParameterAttributes.None;
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00014E3D File Offset: 0x0001303D
		private static bool IsInvariant(Type t)
		{
			return (t.GenericParameterAttributes & GenericParameterAttributes.VarianceMask) == GenericParameterAttributes.None;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00014E4A File Offset: 0x0001304A
		private static bool IsDelegate(Type t)
		{
			return t.IsSubclassOf(typeof(MulticastDelegate));
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00014E5C File Offset: 0x0001305C
		internal static bool IsLegalExplicitVariantDelegateConversion(Type source, Type dest)
		{
			if (!TypeUtils.IsDelegate(source) || !TypeUtils.IsDelegate(dest) || !source.IsGenericType || !dest.IsGenericType)
			{
				return false;
			}
			Type genericTypeDefinition = source.GetGenericTypeDefinition();
			if (dest.GetGenericTypeDefinition() != genericTypeDefinition)
			{
				return false;
			}
			Type[] genericArguments = genericTypeDefinition.GetGenericArguments();
			Type[] genericArguments2 = source.GetGenericArguments();
			Type[] genericArguments3 = dest.GetGenericArguments();
			for (int i = 0; i < genericArguments.Length; i++)
			{
				Type type = genericArguments2[i];
				Type type2 = genericArguments3[i];
				if (!TypeUtils.AreEquivalent(type, type2))
				{
					Type t = genericArguments[i];
					if (TypeUtils.IsInvariant(t))
					{
						return false;
					}
					if (TypeUtils.IsCovariant(t))
					{
						if (!TypeUtils.HasReferenceConversion(type, type2))
						{
							return false;
						}
					}
					else if (TypeUtils.IsContravariant(t) && (type.IsValueType || type2.IsValueType))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00014F28 File Offset: 0x00013128
		internal static bool IsConvertible(Type type)
		{
			type = type.GetNonNullableType();
			if (type.IsEnum)
			{
				return true;
			}
			TypeCode typeCode = Type.GetTypeCode(type);
			return typeCode - TypeCode.Boolean <= 11;
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00014F58 File Offset: 0x00013158
		internal static bool HasReferenceEquality(Type left, Type right)
		{
			return !left.IsValueType && !right.IsValueType && (left.IsInterface || right.IsInterface || TypeUtils.AreReferenceAssignable(left, right) || TypeUtils.AreReferenceAssignable(right, left));
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00014F90 File Offset: 0x00013190
		internal static bool HasBuiltInEqualityOperator(Type left, Type right)
		{
			if (left.IsInterface && !right.IsValueType)
			{
				return true;
			}
			if (right.IsInterface && !left.IsValueType)
			{
				return true;
			}
			if (!left.IsValueType && !right.IsValueType && (TypeUtils.AreReferenceAssignable(left, right) || TypeUtils.AreReferenceAssignable(right, left)))
			{
				return true;
			}
			if (!TypeUtils.AreEquivalent(left, right))
			{
				return false;
			}
			Type nonNullableType = left.GetNonNullableType();
			return nonNullableType == typeof(bool) || TypeUtils.IsNumeric(nonNullableType) || nonNullableType.IsEnum;
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0001501C File Offset: 0x0001321C
		internal static bool IsImplicitlyConvertible(Type source, Type destination)
		{
			return TypeUtils.AreEquivalent(source, destination) || TypeUtils.IsImplicitNumericConversion(source, destination) || TypeUtils.IsImplicitReferenceConversion(source, destination) || TypeUtils.IsImplicitBoxingConversion(source, destination) || TypeUtils.IsImplicitNullableConversion(source, destination);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0001504C File Offset: 0x0001324C
		internal static MethodInfo GetUserDefinedCoercionMethod(Type convertFrom, Type convertToType, bool implicitOnly)
		{
			Type nonNullableType = convertFrom.GetNonNullableType();
			Type nonNullableType2 = convertToType.GetNonNullableType();
			MethodInfo[] methods = nonNullableType.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			MethodInfo methodInfo = TypeUtils.FindConversionOperator(methods, convertFrom, convertToType, implicitOnly);
			if (methodInfo != null)
			{
				return methodInfo;
			}
			MethodInfo[] methods2 = nonNullableType2.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			methodInfo = TypeUtils.FindConversionOperator(methods2, convertFrom, convertToType, implicitOnly);
			if (methodInfo != null)
			{
				return methodInfo;
			}
			if (!TypeUtils.AreEquivalent(nonNullableType, convertFrom) || !TypeUtils.AreEquivalent(nonNullableType2, convertToType))
			{
				methodInfo = TypeUtils.FindConversionOperator(methods, nonNullableType, nonNullableType2, implicitOnly);
				if (methodInfo == null)
				{
					methodInfo = TypeUtils.FindConversionOperator(methods2, nonNullableType, nonNullableType2, implicitOnly);
				}
				if (methodInfo != null)
				{
					return methodInfo;
				}
			}
			return null;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x000150E4 File Offset: 0x000132E4
		internal static MethodInfo FindConversionOperator(MethodInfo[] methods, Type typeFrom, Type typeTo, bool implicitOnly)
		{
			foreach (MethodInfo methodInfo in methods)
			{
				if ((!(methodInfo.Name != "op_Implicit") || (!implicitOnly && !(methodInfo.Name != "op_Explicit"))) && TypeUtils.AreEquivalent(methodInfo.ReturnType, typeTo))
				{
					ParameterInfo[] parametersCached = methodInfo.GetParametersCached();
					if (TypeUtils.AreEquivalent(parametersCached[0].ParameterType, typeFrom))
					{
						return methodInfo;
					}
				}
			}
			return null;
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00015154 File Offset: 0x00013354
		private static bool IsImplicitNumericConversion(Type source, Type destination)
		{
			TypeCode typeCode = Type.GetTypeCode(source);
			TypeCode typeCode2 = Type.GetTypeCode(destination);
			switch (typeCode)
			{
			case TypeCode.Char:
				return typeCode2 - TypeCode.UInt16 <= 7;
			case TypeCode.SByte:
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
				return false;
			case TypeCode.Byte:
				return typeCode2 - TypeCode.Int16 <= 8;
			case TypeCode.Int16:
				switch (typeCode2)
				{
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					return true;
				}
				return false;
			case TypeCode.UInt16:
				return typeCode2 - TypeCode.Int32 <= 6;
			case TypeCode.Int32:
				return typeCode2 == TypeCode.Int64 || typeCode2 - TypeCode.Single <= 2;
			case TypeCode.UInt32:
				return typeCode2 == TypeCode.UInt32 || typeCode2 - TypeCode.UInt64 <= 3;
			case TypeCode.Int64:
			case TypeCode.UInt64:
				return typeCode2 - TypeCode.Single <= 2;
			case TypeCode.Single:
				return typeCode2 == TypeCode.Double;
			default:
				return false;
			}
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00015252 File Offset: 0x00013452
		private static bool IsImplicitReferenceConversion(Type source, Type destination)
		{
			return destination.IsAssignableFrom(source);
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0001525C File Offset: 0x0001345C
		private static bool IsImplicitBoxingConversion(Type source, Type destination)
		{
			return (source.IsValueType && (destination == typeof(object) || destination == typeof(ValueType))) || (source.IsEnum && destination == typeof(Enum));
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x000152B4 File Offset: 0x000134B4
		private static bool IsImplicitNullableConversion(Type source, Type destination)
		{
			return destination.IsNullableType() && TypeUtils.IsImplicitlyConvertible(source.GetNonNullableType(), destination.GetNonNullableType());
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x000152D1 File Offset: 0x000134D1
		internal static bool IsSameOrSubclass(Type type, Type subType)
		{
			return TypeUtils.AreEquivalent(type, subType) || subType.IsSubclassOf(type);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x000152E5 File Offset: 0x000134E5
		internal static void ValidateType(Type type)
		{
			if (type.IsGenericTypeDefinition)
			{
				throw Error.TypeIsGeneric(type);
			}
			if (type.ContainsGenericParameters)
			{
				throw Error.TypeContainsGenericParameters(type);
			}
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00015308 File Offset: 0x00013508
		internal static Type FindGenericType(Type definition, Type type)
		{
			while (type != null && type != typeof(object))
			{
				if (type.IsGenericType && TypeUtils.AreEquivalent(type.GetGenericTypeDefinition(), definition))
				{
					return type;
				}
				if (definition.IsInterface)
				{
					foreach (Type type2 in type.GetInterfaces())
					{
						Type type3 = TypeUtils.FindGenericType(definition, type2);
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

		// Token: 0x0600067A RID: 1658 RVA: 0x00015388 File Offset: 0x00013588
		internal static bool IsUnsigned(Type type)
		{
			type = type.GetNonNullableType();
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Char:
			case TypeCode.Byte:
			case TypeCode.UInt16:
			case TypeCode.UInt32:
			case TypeCode.UInt64:
				return true;
			}
			return false;
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x000153D8 File Offset: 0x000135D8
		internal static bool IsFloatingPoint(Type type)
		{
			type = type.GetNonNullableType();
			TypeCode typeCode = Type.GetTypeCode(type);
			return typeCode - TypeCode.Single <= 1;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00015400 File Offset: 0x00013600
		internal static MethodInfo GetBooleanOperator(Type type, string name)
		{
			MethodInfo methodValidated;
			for (;;)
			{
				methodValidated = type.GetMethodValidated(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
				{
					type
				}, null);
				if (methodValidated != null && methodValidated.IsSpecialName && !methodValidated.ContainsGenericParameters)
				{
					break;
				}
				type = type.BaseType;
				if (!(type != null))
				{
					goto Block_3;
				}
			}
			return methodValidated;
			Block_3:
			return null;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00015450 File Offset: 0x00013650
		internal static Type GetNonRefType(this Type type)
		{
			if (!type.IsByRef)
			{
				return type;
			}
			return type.GetElementType();
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00015464 File Offset: 0x00013664
		internal static bool CanCache(this Type t)
		{
			Assembly assembly = t.Assembly;
			if (assembly != TypeUtils._mscorlib && assembly != TypeUtils._systemCore)
			{
				return false;
			}
			if (t.IsGenericType)
			{
				foreach (Type t2 in t.GetGenericArguments())
				{
					if (!t2.CanCache())
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x040005BF RID: 1471
		private const BindingFlags AnyStatic = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x040005C0 RID: 1472
		internal const MethodAttributes PublicStatic = MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static;

		// Token: 0x040005C1 RID: 1473
		private static readonly Assembly _mscorlib = typeof(object).Assembly;

		// Token: 0x040005C2 RID: 1474
		private static readonly Assembly _systemCore = typeof(Expression).Assembly;
	}
}
