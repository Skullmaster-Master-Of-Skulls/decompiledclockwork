using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.EntitySql.AST;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000240 RID: 576
	internal static class CqlErrorHelper
	{
		// Token: 0x060013EF RID: 5103 RVA: 0x0005171C File Offset: 0x0004F91C
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
			throw EntitySqlException.Create(functionExpr.ErrCtx.CommandText, func(functionType.NamespaceName, functionType.Name, stringBuilder.ToString()), functionExpr.ErrCtx.InputPosition, Strings.CtxFunction(functionType.Name), false, null);
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x00051830 File Offset: 0x0004FA30
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.EntityUtil.EntitySqlError(System.Data.Entity.Core.Common.EntitySql.ErrorContext,System.String)")]
		internal static void ReportAliasAlreadyUsedError(string aliasName, ErrorContext errCtx, string contextMessage)
		{
			throw EntitySqlException.Create(errCtx, string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
			{
				Strings.AliasNameAlreadyUsed(aliasName),
				contextMessage
			}), null);
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x00051868 File Offset: 0x0004FA68
		internal static void ReportIncompatibleCommonType(ErrorContext errCtx, TypeUsage leftType, TypeUsage rightType)
		{
			CqlErrorHelper.ReportIncompatibleCommonType(errCtx, leftType, rightType, leftType, rightType);
			throw EntitySqlException.Create(errCtx, Strings.ArgumentTypesAreIncompatible(leftType.Identity, rightType.Identity), null);
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x0005188C File Offset: 0x0004FA8C
		private static void ReportIncompatibleCommonType(ErrorContext errCtx, TypeUsage rootLeftType, TypeUsage rootRightType, TypeUsage leftType, TypeUsage rightType)
		{
			TypeUsage typeUsage = null;
			bool flag = rootLeftType == leftType;
			string errorMessage = string.Empty;
			if (leftType.EdmType.BuiltInTypeKind != rightType.EdmType.BuiltInTypeKind)
			{
				throw EntitySqlException.Create(errCtx, Strings.TypeKindMismatch(CqlErrorHelper.GetReadableTypeKind(leftType), CqlErrorHelper.GetReadableTypeName(leftType), CqlErrorHelper.GetReadableTypeKind(rightType), CqlErrorHelper.GetReadableTypeName(rightType)), null);
			}
			BuiltInTypeKind builtInTypeKind = leftType.EdmType.BuiltInTypeKind;
			if (builtInTypeKind <= BuiltInTypeKind.EntityType)
			{
				switch (builtInTypeKind)
				{
				case BuiltInTypeKind.CollectionType:
					break;
				case BuiltInTypeKind.CollectionKind:
					goto IL_276;
				case BuiltInTypeKind.ComplexType:
				{
					ComplexType complexType = (ComplexType)leftType.EdmType;
					ComplexType complexType2 = (ComplexType)rightType.EdmType;
					if (complexType.Members.Count != complexType2.Members.Count)
					{
						if (flag)
						{
							errorMessage = Strings.InvalidRootComplexType(CqlErrorHelper.GetReadableTypeName(complexType), CqlErrorHelper.GetReadableTypeName(complexType2));
						}
						else
						{
							errorMessage = Strings.InvalidComplexType(CqlErrorHelper.GetReadableTypeName(complexType), CqlErrorHelper.GetReadableTypeName(rootLeftType), CqlErrorHelper.GetReadableTypeName(complexType2), CqlErrorHelper.GetReadableTypeName(rootRightType));
						}
						throw EntitySqlException.Create(errCtx, errorMessage, null);
					}
					for (int i = 0; i < complexType.Members.Count; i++)
					{
						CqlErrorHelper.ReportIncompatibleCommonType(errCtx, rootLeftType, rootRightType, complexType.Members[i].TypeUsage, complexType2.Members[i].TypeUsage);
					}
					return;
				}
				default:
					if (builtInTypeKind != BuiltInTypeKind.EntityType)
					{
						goto IL_276;
					}
					if (!TypeSemantics.TryGetCommonType(leftType, rightType, out typeUsage))
					{
						if (flag)
						{
							errorMessage = Strings.InvalidEntityRootTypeArgument(CqlErrorHelper.GetReadableTypeName(leftType), CqlErrorHelper.GetReadableTypeName(rightType));
						}
						else
						{
							errorMessage = Strings.InvalidEntityTypeArgument(CqlErrorHelper.GetReadableTypeName(leftType), CqlErrorHelper.GetReadableTypeName(rootLeftType), CqlErrorHelper.GetReadableTypeName(rightType), CqlErrorHelper.GetReadableTypeName(rootRightType));
						}
						throw EntitySqlException.Create(errCtx, errorMessage, null);
					}
					return;
				}
			}
			else if (builtInTypeKind != BuiltInTypeKind.RefType)
			{
				if (builtInTypeKind != BuiltInTypeKind.RowType)
				{
					goto IL_276;
				}
				RowType rowType = (RowType)leftType.EdmType;
				RowType rowType2 = (RowType)rightType.EdmType;
				if (rowType.Members.Count != rowType2.Members.Count)
				{
					if (flag)
					{
						errorMessage = Strings.InvalidRootRowType(CqlErrorHelper.GetReadableTypeName(rowType), CqlErrorHelper.GetReadableTypeName(rowType2));
					}
					else
					{
						errorMessage = Strings.InvalidRowType(CqlErrorHelper.GetReadableTypeName(rowType), CqlErrorHelper.GetReadableTypeName(rootLeftType), CqlErrorHelper.GetReadableTypeName(rowType2), CqlErrorHelper.GetReadableTypeName(rootRightType));
					}
					throw EntitySqlException.Create(errCtx, errorMessage, null);
				}
				for (int j = 0; j < rowType.Members.Count; j++)
				{
					CqlErrorHelper.ReportIncompatibleCommonType(errCtx, rootLeftType, rootRightType, rowType.Members[j].TypeUsage, rowType2.Members[j].TypeUsage);
				}
				return;
			}
			CqlErrorHelper.ReportIncompatibleCommonType(errCtx, rootLeftType, rootRightType, TypeHelpers.GetElementTypeUsage(leftType), TypeHelpers.GetElementTypeUsage(rightType));
			return;
			IL_276:
			if (!TypeSemantics.TryGetCommonType(leftType, rightType, out typeUsage))
			{
				if (flag)
				{
					errorMessage = Strings.InvalidPlaceholderRootTypeArgument(CqlErrorHelper.GetReadableTypeKind(leftType), CqlErrorHelper.GetReadableTypeName(leftType), CqlErrorHelper.GetReadableTypeKind(rightType), CqlErrorHelper.GetReadableTypeName(rightType));
				}
				else
				{
					errorMessage = Strings.InvalidPlaceholderTypeArgument(CqlErrorHelper.GetReadableTypeKind(leftType), CqlErrorHelper.GetReadableTypeName(leftType), CqlErrorHelper.GetReadableTypeName(rootLeftType), CqlErrorHelper.GetReadableTypeKind(rightType), CqlErrorHelper.GetReadableTypeName(rightType), CqlErrorHelper.GetReadableTypeName(rootRightType));
				}
				throw EntitySqlException.Create(errCtx, errorMessage, null);
			}
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x00051B75 File Offset: 0x0004FD75
		private static string GetReadableTypeName(TypeUsage type)
		{
			return CqlErrorHelper.GetReadableTypeName(type.EdmType);
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x00051B82 File Offset: 0x0004FD82
		private static string GetReadableTypeName(EdmType type)
		{
			if (type.BuiltInTypeKind == BuiltInTypeKind.RowType || type.BuiltInTypeKind == BuiltInTypeKind.CollectionType || type.BuiltInTypeKind == BuiltInTypeKind.RefType)
			{
				return type.Name;
			}
			return type.FullName;
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x00051BAE File Offset: 0x0004FDAE
		private static string GetReadableTypeKind(TypeUsage type)
		{
			return CqlErrorHelper.GetReadableTypeKind(type.EdmType);
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x00051BBC File Offset: 0x0004FDBC
		private static string GetReadableTypeKind(EdmType type)
		{
			string str = string.Empty;
			BuiltInTypeKind builtInTypeKind = type.BuiltInTypeKind;
			if (builtInTypeKind <= BuiltInTypeKind.EntityType)
			{
				switch (builtInTypeKind)
				{
				case BuiltInTypeKind.CollectionType:
					str = Strings.LocalizedCollection;
					goto IL_7D;
				case BuiltInTypeKind.CollectionKind:
					break;
				case BuiltInTypeKind.ComplexType:
					str = Strings.LocalizedComplex;
					goto IL_7D;
				default:
					if (builtInTypeKind == BuiltInTypeKind.EntityType)
					{
						str = Strings.LocalizedEntity;
						goto IL_7D;
					}
					break;
				}
			}
			else
			{
				if (builtInTypeKind == BuiltInTypeKind.PrimitiveType)
				{
					str = Strings.LocalizedPrimitive;
					goto IL_7D;
				}
				if (builtInTypeKind == BuiltInTypeKind.RefType)
				{
					str = Strings.LocalizedReference;
					goto IL_7D;
				}
				if (builtInTypeKind == BuiltInTypeKind.RowType)
				{
					str = Strings.LocalizedRow;
					goto IL_7D;
				}
			}
			str = type.BuiltInTypeKind.ToString();
			IL_7D:
			return str + " " + Strings.LocalizedType;
		}
	}
}
