using System;
using System.Collections.Generic;
using System.Data.Common.EntitySql.AST;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Globalization;
using System.Text;

namespace System.Data.Common.EntitySql
{
	// Token: 0x02000356 RID: 854
	internal static class CqlErrorHelper
	{
		// Token: 0x060031B4 RID: 12724 RVA: 0x000C3384 File Offset: 0x000C1584
		internal static void ReportFunctionOverloadError(MethodExpr functionExpr, EdmFunction functionType, List<TypeUsage> argTypes)
		{
			string value = "";
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(functionType.Name).Append("(");
			for (int i = 0; i < argTypes.Count; i++)
			{
				stringBuilder.Append(value);
				stringBuilder.Append((argTypes[i] != null) ? argTypes[i].EdmType.FullName : "NULL");
				value = ", ";
			}
			stringBuilder.Append(")");
			Func<object, object, object, string> func;
			if (TypeSemantics.IsAggregateFunction(functionType))
			{
				func = (TypeHelpers.IsCanonicalFunction(functionType) ? new Func<object, object, object, string>(Strings.NoCanonicalAggrFunctionOverloadMatch) : new Func<object, object, object, string>(Strings.NoAggrFunctionOverloadMatch));
			}
			else
			{
				func = (TypeHelpers.IsCanonicalFunction(functionType) ? new Func<object, object, object, string>(Strings.NoCanonicalFunctionOverloadMatch) : new Func<object, object, object, string>(Strings.NoFunctionOverloadMatch));
			}
			throw EntityUtil.EntitySqlError(functionExpr.ErrCtx.CommandText, func(functionType.NamespaceName, functionType.Name, stringBuilder.ToString()), functionExpr.ErrCtx.InputPosition, Strings.CtxFunction(functionType.Name), false);
		}

		// Token: 0x060031B5 RID: 12725 RVA: 0x000C3496 File Offset: 0x000C1696
		internal static void ReportAliasAlreadyUsedError(string aliasName, ErrorContext errCtx, string contextMessage)
		{
			throw EntityUtil.EntitySqlError(errCtx, string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
			{
				Strings.AliasNameAlreadyUsed(aliasName),
				contextMessage
			}));
		}

		// Token: 0x060031B6 RID: 12726 RVA: 0x000C34C0 File Offset: 0x000C16C0
		internal static void ReportIncompatibleCommonType(ErrorContext errCtx, TypeUsage leftType, TypeUsage rightType)
		{
			CqlErrorHelper.ReportIncompatibleCommonType(errCtx, leftType, rightType, leftType, rightType);
			throw EntityUtil.EntitySqlError(errCtx, Strings.ArgumentTypesAreIncompatible(leftType.Identity, rightType.Identity));
		}

		// Token: 0x060031B7 RID: 12727 RVA: 0x000C34E4 File Offset: 0x000C16E4
		private static void ReportIncompatibleCommonType(ErrorContext errCtx, TypeUsage rootLeftType, TypeUsage rootRightType, TypeUsage leftType, TypeUsage rightType)
		{
			TypeUsage typeUsage = null;
			bool flag = rootLeftType == leftType;
			string message = string.Empty;
			if (leftType.EdmType.BuiltInTypeKind != rightType.EdmType.BuiltInTypeKind)
			{
				throw EntityUtil.EntitySqlError(errCtx, Strings.TypeKindMismatch(CqlErrorHelper.GetReadableTypeKind(leftType), CqlErrorHelper.GetReadableTypeName(leftType), CqlErrorHelper.GetReadableTypeKind(rightType), CqlErrorHelper.GetReadableTypeName(rightType)));
			}
			BuiltInTypeKind builtInTypeKind = leftType.EdmType.BuiltInTypeKind;
			if (builtInTypeKind <= BuiltInTypeKind.ComplexType)
			{
				if (builtInTypeKind != BuiltInTypeKind.CollectionType)
				{
					if (builtInTypeKind != BuiltInTypeKind.ComplexType)
					{
						goto IL_26C;
					}
					ComplexType complexType = (ComplexType)leftType.EdmType;
					ComplexType complexType2 = (ComplexType)rightType.EdmType;
					if (complexType.Members.Count != complexType2.Members.Count)
					{
						if (flag)
						{
							message = Strings.InvalidRootComplexType(CqlErrorHelper.GetReadableTypeName(complexType), CqlErrorHelper.GetReadableTypeName(complexType2));
						}
						else
						{
							message = Strings.InvalidComplexType(CqlErrorHelper.GetReadableTypeName(complexType), CqlErrorHelper.GetReadableTypeName(rootLeftType), CqlErrorHelper.GetReadableTypeName(complexType2), CqlErrorHelper.GetReadableTypeName(rootRightType));
						}
						throw EntityUtil.EntitySqlError(errCtx, message);
					}
					for (int i = 0; i < complexType.Members.Count; i++)
					{
						CqlErrorHelper.ReportIncompatibleCommonType(errCtx, rootLeftType, rootRightType, complexType.Members[i].TypeUsage, complexType2.Members[i].TypeUsage);
					}
					return;
				}
			}
			else if (builtInTypeKind != BuiltInTypeKind.EntityType)
			{
				if (builtInTypeKind != BuiltInTypeKind.RefType)
				{
					if (builtInTypeKind != BuiltInTypeKind.RowType)
					{
						goto IL_26C;
					}
					RowType rowType = (RowType)leftType.EdmType;
					RowType rowType2 = (RowType)rightType.EdmType;
					if (rowType.Members.Count != rowType2.Members.Count)
					{
						if (flag)
						{
							message = Strings.InvalidRootRowType(CqlErrorHelper.GetReadableTypeName(rowType), CqlErrorHelper.GetReadableTypeName(rowType2));
						}
						else
						{
							message = Strings.InvalidRowType(CqlErrorHelper.GetReadableTypeName(rowType), CqlErrorHelper.GetReadableTypeName(rootLeftType), CqlErrorHelper.GetReadableTypeName(rowType2), CqlErrorHelper.GetReadableTypeName(rootRightType));
						}
						throw EntityUtil.EntitySqlError(errCtx, message);
					}
					for (int j = 0; j < rowType.Members.Count; j++)
					{
						CqlErrorHelper.ReportIncompatibleCommonType(errCtx, rootLeftType, rootRightType, rowType.Members[j].TypeUsage, rowType2.Members[j].TypeUsage);
					}
					return;
				}
			}
			else
			{
				if (!TypeSemantics.TryGetCommonType(leftType, rightType, out typeUsage))
				{
					if (flag)
					{
						message = Strings.InvalidEntityRootTypeArgument(CqlErrorHelper.GetReadableTypeName(leftType), CqlErrorHelper.GetReadableTypeName(rightType));
					}
					else
					{
						message = Strings.InvalidEntityTypeArgument(CqlErrorHelper.GetReadableTypeName(leftType), CqlErrorHelper.GetReadableTypeName(rootLeftType), CqlErrorHelper.GetReadableTypeName(rightType), CqlErrorHelper.GetReadableTypeName(rootRightType));
					}
					throw EntityUtil.EntitySqlError(errCtx, message);
				}
				return;
			}
			CqlErrorHelper.ReportIncompatibleCommonType(errCtx, rootLeftType, rootRightType, TypeHelpers.GetElementTypeUsage(leftType), TypeHelpers.GetElementTypeUsage(rightType));
			return;
			IL_26C:
			if (!TypeSemantics.TryGetCommonType(leftType, rightType, out typeUsage))
			{
				if (flag)
				{
					message = Strings.InvalidPlaceholderRootTypeArgument(CqlErrorHelper.GetReadableTypeKind(leftType), CqlErrorHelper.GetReadableTypeName(leftType), CqlErrorHelper.GetReadableTypeKind(rightType), CqlErrorHelper.GetReadableTypeName(rightType));
				}
				else
				{
					message = Strings.InvalidPlaceholderTypeArgument(CqlErrorHelper.GetReadableTypeKind(leftType), CqlErrorHelper.GetReadableTypeName(leftType), CqlErrorHelper.GetReadableTypeName(rootLeftType), CqlErrorHelper.GetReadableTypeKind(rightType), CqlErrorHelper.GetReadableTypeName(rightType), CqlErrorHelper.GetReadableTypeName(rootRightType));
				}
				throw EntityUtil.EntitySqlError(errCtx, message);
			}
		}

		// Token: 0x060031B8 RID: 12728 RVA: 0x000C37C2 File Offset: 0x000C19C2
		private static string GetReadableTypeName(TypeUsage type)
		{
			return CqlErrorHelper.GetReadableTypeName(type.EdmType);
		}

		// Token: 0x060031B9 RID: 12729 RVA: 0x000C37CF File Offset: 0x000C19CF
		private static string GetReadableTypeName(EdmType type)
		{
			if (type.BuiltInTypeKind == BuiltInTypeKind.RowType || type.BuiltInTypeKind == BuiltInTypeKind.CollectionType || type.BuiltInTypeKind == BuiltInTypeKind.RefType)
			{
				return type.Name;
			}
			return type.FullName;
		}

		// Token: 0x060031BA RID: 12730 RVA: 0x000C37FB File Offset: 0x000C19FB
		private static string GetReadableTypeKind(TypeUsage type)
		{
			return CqlErrorHelper.GetReadableTypeKind(type.EdmType);
		}

		// Token: 0x060031BB RID: 12731 RVA: 0x000C3808 File Offset: 0x000C1A08
		private static string GetReadableTypeKind(EdmType type)
		{
			string str = string.Empty;
			BuiltInTypeKind builtInTypeKind = type.BuiltInTypeKind;
			if (builtInTypeKind <= BuiltInTypeKind.EntityType)
			{
				if (builtInTypeKind == BuiltInTypeKind.CollectionType)
				{
					str = Strings.LocalizedCollection;
					goto IL_75;
				}
				if (builtInTypeKind == BuiltInTypeKind.ComplexType)
				{
					str = Strings.LocalizedComplex;
					goto IL_75;
				}
				if (builtInTypeKind == BuiltInTypeKind.EntityType)
				{
					str = Strings.LocalizedEntity;
					goto IL_75;
				}
			}
			else
			{
				if (builtInTypeKind == BuiltInTypeKind.PrimitiveType)
				{
					str = Strings.LocalizedPrimitive;
					goto IL_75;
				}
				if (builtInTypeKind == BuiltInTypeKind.RefType)
				{
					str = Strings.LocalizedReference;
					goto IL_75;
				}
				if (builtInTypeKind == BuiltInTypeKind.RowType)
				{
					str = Strings.LocalizedRow;
					goto IL_75;
				}
			}
			str = type.BuiltInTypeKind.ToString();
			IL_75:
			return str + " " + Strings.LocalizedType;
		}
	}
}
