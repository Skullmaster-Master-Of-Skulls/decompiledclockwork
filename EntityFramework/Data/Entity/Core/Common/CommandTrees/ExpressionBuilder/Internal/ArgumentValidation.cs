using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder.Internal
{
	// Token: 0x0200011E RID: 286
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	internal static class ArgumentValidation
	{
		// Token: 0x0600086A RID: 2154 RVA: 0x0002BC81 File Offset: 0x00029E81
		internal static ReadOnlyCollection<TElement> NewReadOnlyCollection<TElement>(IList<TElement> list)
		{
			return new ReadOnlyCollection<TElement>(list);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0002BC89 File Offset: 0x00029E89
		internal static void RequirePolymorphicType(TypeUsage type)
		{
			if (!TypeSemantics.IsPolymorphicType(type))
			{
				throw new ArgumentException(Strings.Cqt_General_PolymorphicTypeRequired(type.ToString()), "type");
			}
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0002BCA9 File Offset: 0x00029EA9
		internal static void RequireCompatibleType(DbExpression expression, TypeUsage requiredResultType, string argumentName)
		{
			ArgumentValidation.RequireCompatibleType(expression, requiredResultType, argumentName, -1);
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0002BCB4 File Offset: 0x00029EB4
		private static void RequireCompatibleType(DbExpression expression, TypeUsage requiredResultType, string argumentName, int argumentIndex)
		{
			if (!TypeSemantics.IsStructurallyEqualOrPromotableTo(expression.ResultType, requiredResultType))
			{
				if (argumentIndex != -1)
				{
					argumentName = StringUtil.FormatIndex(argumentName, argumentIndex);
				}
				throw new ArgumentException(Strings.Cqt_ExpressionLink_TypeMismatch(expression.ResultType.ToString(), requiredResultType.ToString()), argumentName);
			}
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0002BCEE File Offset: 0x00029EEE
		internal static void RequireCompatibleType(DbExpression expression, PrimitiveTypeKind requiredResultType, string argumentName)
		{
			ArgumentValidation.RequireCompatibleType(expression, requiredResultType, argumentName, -1);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0002BCFC File Offset: 0x00029EFC
		private static void RequireCompatibleType(DbExpression expression, PrimitiveTypeKind requiredResultType, string argumentName, int index)
		{
			PrimitiveTypeKind primitiveTypeKind;
			bool flag = TypeHelpers.TryGetPrimitiveTypeKind(expression.ResultType, out primitiveTypeKind);
			if (!flag || primitiveTypeKind != requiredResultType)
			{
				if (index != -1)
				{
					argumentName = StringUtil.FormatIndex(argumentName, index);
				}
				throw new ArgumentException(Strings.Cqt_ExpressionLink_TypeMismatch(flag ? Enum.GetName(typeof(PrimitiveTypeKind), primitiveTypeKind) : expression.ResultType.ToString(), Enum.GetName(typeof(PrimitiveTypeKind), requiredResultType)), argumentName);
			}
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0002BD74 File Offset: 0x00029F74
		private static void RequireCompatibleType(DbExpression from, RelationshipEndMember end, bool allowAllRelationshipsInSameTypeHierarchy)
		{
			TypeUsage typeUsage = end.TypeUsage;
			if (!TypeSemantics.IsReferenceType(typeUsage))
			{
				typeUsage = TypeHelpers.CreateReferenceTypeUsage(TypeHelpers.GetEdmType<EntityType>(typeUsage));
			}
			if (allowAllRelationshipsInSameTypeHierarchy)
			{
				if (TypeHelpers.GetCommonTypeUsage(typeUsage, from.ResultType) == null)
				{
					throw new ArgumentException(Strings.Cqt_RelNav_WrongSourceType(typeUsage.ToString()), "from");
				}
			}
			else if (!TypeSemantics.IsStructurallyEqualOrPromotableTo(from.ResultType.EdmType, typeUsage.EdmType))
			{
				throw new ArgumentException(Strings.Cqt_RelNav_WrongSourceType(typeUsage.ToString()), "from");
			}
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0002BDF1 File Offset: 0x00029FF1
		internal static void RequireCollectionArgument<TExpressionType>(DbExpression argument)
		{
			if (!TypeSemantics.IsCollectionType(argument.ResultType))
			{
				throw new ArgumentException(Strings.Cqt_Unary_CollectionRequired(typeof(TExpressionType).Name), "argument");
			}
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0002BE20 File Offset: 0x0002A020
		internal static TypeUsage RequireCollectionArguments<TExpressionType>(DbExpression left, DbExpression right)
		{
			if (!TypeSemantics.IsCollectionType(left.ResultType) || !TypeSemantics.IsCollectionType(right.ResultType))
			{
				throw new ArgumentException(Strings.Cqt_Binary_CollectionsRequired(typeof(TExpressionType).Name));
			}
			TypeUsage commonTypeUsage = TypeHelpers.GetCommonTypeUsage(left.ResultType, right.ResultType);
			if (commonTypeUsage == null)
			{
				throw new ArgumentException(Strings.Cqt_Binary_CollectionsRequired(typeof(TExpressionType).Name));
			}
			return commonTypeUsage;
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0002BE94 File Offset: 0x0002A094
		internal static TypeUsage RequireComparableCollectionArguments<TExpressionType>(DbExpression left, DbExpression right)
		{
			TypeUsage result = ArgumentValidation.RequireCollectionArguments<TExpressionType>(left, right);
			if (!TypeHelpers.IsSetComparableOpType(TypeHelpers.GetElementTypeUsage(left.ResultType)))
			{
				throw new ArgumentException(Strings.Cqt_InvalidTypeForSetOperation(TypeHelpers.GetElementTypeUsage(left.ResultType).Identity, typeof(TExpressionType).Name), "left");
			}
			if (!TypeHelpers.IsSetComparableOpType(TypeHelpers.GetElementTypeUsage(right.ResultType)))
			{
				throw new ArgumentException(Strings.Cqt_InvalidTypeForSetOperation(TypeHelpers.GetElementTypeUsage(right.ResultType).Identity, typeof(TExpressionType).Name), "right");
			}
			return result;
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0002BF2C File Offset: 0x0002A12C
		private static EnumerableValidator<TElementIn, TElementOut, TResult> CreateValidator<TElementIn, TElementOut, TResult>(IEnumerable<TElementIn> argument, string argumentName, Func<TElementIn, int, TElementOut> convertElement, Func<List<TElementOut>, TResult> createResult)
		{
			return new EnumerableValidator<TElementIn, TElementOut, TResult>(argument, argumentName)
			{
				ConvertElement = convertElement,
				CreateResult = createResult
			};
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0002BF50 File Offset: 0x0002A150
		internal static DbExpressionList CreateExpressionList(IEnumerable<DbExpression> arguments, string argumentName, Action<DbExpression, int> validationCallback)
		{
			return ArgumentValidation.CreateExpressionList(arguments, argumentName, false, validationCallback);
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0002BF84 File Offset: 0x0002A184
		private static DbExpressionList CreateExpressionList(IEnumerable<DbExpression> arguments, string argumentName, bool allowEmpty, Action<DbExpression, int> validationCallback)
		{
			EnumerableValidator<DbExpression, DbExpression, DbExpressionList> enumerableValidator = ArgumentValidation.CreateValidator<DbExpression, DbExpression, DbExpressionList>(arguments, argumentName, delegate(DbExpression exp, int idx)
			{
				if (validationCallback != null)
				{
					validationCallback(exp, idx);
				}
				return exp;
			}, (List<DbExpression> expList) => new DbExpressionList(expList));
			enumerableValidator.AllowEmpty = allowEmpty;
			return enumerableValidator.Validate();
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0002C004 File Offset: 0x0002A204
		private static DbExpressionList CreateExpressionList(IEnumerable<DbExpression> arguments, string argumentName, int expectedElementCount, Action<DbExpression, int> validationCallback)
		{
			EnumerableValidator<DbExpression, DbExpression, DbExpressionList> enumerableValidator = ArgumentValidation.CreateValidator<DbExpression, DbExpression, DbExpressionList>(arguments, argumentName, delegate(DbExpression exp, int idx)
			{
				if (validationCallback != null)
				{
					validationCallback(exp, idx);
				}
				return exp;
			}, (List<DbExpression> expList) => new DbExpressionList(expList));
			enumerableValidator.ExpectedElementCount = expectedElementCount;
			enumerableValidator.AllowEmpty = false;
			return enumerableValidator.Validate();
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0002C078 File Offset: 0x0002A278
		private static FunctionParameter[] GetExpectedParameters(EdmFunction function)
		{
			return (from p in function.Parameters
			where p.Mode == ParameterMode.In || p.Mode == ParameterMode.InOut
			select p).ToArray<FunctionParameter>();
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0002C0E8 File Offset: 0x0002A2E8
		internal static DbExpressionList ValidateFunctionAggregate(EdmFunction function, IEnumerable<DbExpression> args)
		{
			ArgumentValidation.CheckFunction(function);
			if (!TypeSemantics.IsAggregateFunction(function) || function.ReturnParameter == null)
			{
				throw new ArgumentException(Strings.Cqt_Aggregate_InvalidFunction, "function");
			}
			FunctionParameter[] expectedParams = ArgumentValidation.GetExpectedParameters(function);
			return ArgumentValidation.CreateExpressionList(args, "argument", expectedParams.Length, delegate(DbExpression exp, int idx)
			{
				TypeUsage typeUsage = expectedParams[idx].TypeUsage;
				TypeUsage typeUsage2 = null;
				if (TypeHelpers.TryGetCollectionElementType(typeUsage, out typeUsage2))
				{
					typeUsage = typeUsage2;
				}
				ArgumentValidation.RequireCompatibleType(exp, typeUsage, "argument");
			});
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0002C14E File Offset: 0x0002A34E
		internal static void ValidateSortClause(DbExpression key)
		{
			if (!TypeHelpers.IsValidSortOpKeyType(key.ResultType))
			{
				throw new ArgumentException(Strings.Cqt_Sort_OrderComparable, "key");
			}
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0002C16D File Offset: 0x0002A36D
		internal static void ValidateSortClause(DbExpression key, string collation)
		{
			ArgumentValidation.ValidateSortClause(key);
			Check.NotEmpty(collation, "collation");
			if (!TypeSemantics.IsPrimitiveType(key.ResultType, PrimitiveTypeKind.String))
			{
				throw new ArgumentException(Strings.Cqt_Sort_NonStringCollationInvalid, "collation");
			}
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0002C1C8 File Offset: 0x0002A3C8
		internal static ReadOnlyCollection<DbVariableReferenceExpression> ValidateLambda(IEnumerable<DbVariableReferenceExpression> variables)
		{
			EnumerableValidator<DbVariableReferenceExpression, DbVariableReferenceExpression, ReadOnlyCollection<DbVariableReferenceExpression>> enumerableValidator = ArgumentValidation.CreateValidator<DbVariableReferenceExpression, DbVariableReferenceExpression, ReadOnlyCollection<DbVariableReferenceExpression>>(variables, "variables", delegate(DbVariableReferenceExpression varExp, int idx)
			{
				if (varExp == null)
				{
					throw new ArgumentNullException(StringUtil.FormatIndex("variables", idx));
				}
				return varExp;
			}, (List<DbVariableReferenceExpression> varList) => new ReadOnlyCollection<DbVariableReferenceExpression>(varList));
			enumerableValidator.AllowEmpty = true;
			enumerableValidator.GetName = ((DbVariableReferenceExpression varDef, int idx) => varDef.VariableName);
			return enumerableValidator.Validate();
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0002C24D File Offset: 0x0002A44D
		internal static TypeUsage ValidateQuantifier(DbExpression predicate)
		{
			ArgumentValidation.RequireCompatibleType(predicate, PrimitiveTypeKind.Boolean, "predicate");
			return predicate.ResultType;
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0002C264 File Offset: 0x0002A464
		internal static TypeUsage ValidateApply(DbExpressionBinding input, DbExpressionBinding apply)
		{
			if (input.VariableName.Equals(apply.VariableName, StringComparison.Ordinal))
			{
				throw new ArgumentException(Strings.Cqt_Apply_DuplicateVariableNames);
			}
			return ArgumentValidation.CreateCollectionOfRowResultType(new List<KeyValuePair<string, TypeUsage>>
			{
				new KeyValuePair<string, TypeUsage>(input.VariableName, input.VariableType),
				new KeyValuePair<string, TypeUsage>(apply.VariableName, apply.VariableType)
			});
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0002C2CC File Offset: 0x0002A4CC
		internal static ReadOnlyCollection<DbExpressionBinding> ValidateCrossJoin(IEnumerable<DbExpressionBinding> inputs, out TypeUsage resultType)
		{
			List<DbExpressionBinding> list = new List<DbExpressionBinding>();
			List<KeyValuePair<string, TypeUsage>> list2 = new List<KeyValuePair<string, TypeUsage>>();
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			IEnumerator<DbExpressionBinding> enumerator = inputs.GetEnumerator();
			int num = 0;
			while (enumerator.MoveNext())
			{
				DbExpressionBinding dbExpressionBinding = enumerator.Current;
				string paramName = StringUtil.FormatIndex("inputs", num);
				if (dbExpressionBinding == null)
				{
					throw new ArgumentNullException(paramName);
				}
				int num2 = -1;
				if (dictionary.TryGetValue(dbExpressionBinding.VariableName, out num2))
				{
					throw new ArgumentException(Strings.Cqt_CrossJoin_DuplicateVariableNames(num2, num, dbExpressionBinding.VariableName));
				}
				list.Add(dbExpressionBinding);
				dictionary.Add(dbExpressionBinding.VariableName, num);
				list2.Add(new KeyValuePair<string, TypeUsage>(dbExpressionBinding.VariableName, dbExpressionBinding.VariableType));
				num++;
			}
			if (list.Count < 2)
			{
				throw new ArgumentException(Strings.Cqt_CrossJoin_AtLeastTwoInputs, "inputs");
			}
			resultType = ArgumentValidation.CreateCollectionOfRowResultType(list2);
			return new ReadOnlyCollection<DbExpressionBinding>(list);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0002C3B8 File Offset: 0x0002A5B8
		internal static TypeUsage ValidateJoin(DbExpressionBinding left, DbExpressionBinding right, DbExpression joinCondition)
		{
			if (left.VariableName.Equals(right.VariableName, StringComparison.Ordinal))
			{
				throw new ArgumentException(Strings.Cqt_Join_DuplicateVariableNames);
			}
			ArgumentValidation.RequireCompatibleType(joinCondition, PrimitiveTypeKind.Boolean, "joinCondition");
			return ArgumentValidation.CreateCollectionOfRowResultType(new List<KeyValuePair<string, TypeUsage>>(2)
			{
				new KeyValuePair<string, TypeUsage>(left.VariableName, left.VariableType),
				new KeyValuePair<string, TypeUsage>(right.VariableName, right.VariableType)
			});
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0002C42B File Offset: 0x0002A62B
		internal static TypeUsage ValidateFilter(DbExpressionBinding input, DbExpression predicate)
		{
			ArgumentValidation.RequireCompatibleType(predicate, PrimitiveTypeKind.Boolean, "predicate");
			return input.Expression.ResultType;
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0002C57C File Offset: 0x0002A77C
		internal static TypeUsage ValidateGroupBy(IEnumerable<KeyValuePair<string, DbExpression>> keys, IEnumerable<KeyValuePair<string, DbAggregate>> aggregates, out DbExpressionList validKeys, out ReadOnlyCollection<DbAggregate> validAggregates)
		{
			List<KeyValuePair<string, TypeUsage>> columns = new List<KeyValuePair<string, TypeUsage>>();
			HashSet<string> keyNames = new HashSet<string>();
			EnumerableValidator<KeyValuePair<string, DbExpression>, DbExpression, DbExpressionList> enumerableValidator = ArgumentValidation.CreateValidator<KeyValuePair<string, DbExpression>, DbExpression, DbExpressionList>(keys, "keys", delegate(KeyValuePair<string, DbExpression> keyInfo, int index)
			{
				ArgumentValidation.CheckNamed<DbExpression>(keyInfo, "keys", index);
				if (!TypeHelpers.IsValidGroupKeyType(keyInfo.Value.ResultType))
				{
					throw new ArgumentException(Strings.Cqt_GroupBy_KeyNotEqualityComparable(keyInfo.Key));
				}
				keyNames.Add(keyInfo.Key);
				columns.Add(new KeyValuePair<string, TypeUsage>(keyInfo.Key, keyInfo.Value.ResultType));
				return keyInfo.Value;
			}, (List<DbExpression> expList) => new DbExpressionList(expList));
			enumerableValidator.AllowEmpty = true;
			enumerableValidator.GetName = ((KeyValuePair<string, DbExpression> keyInfo, int idx) => keyInfo.Key);
			validKeys = enumerableValidator.Validate();
			bool hasGroupAggregate = false;
			EnumerableValidator<KeyValuePair<string, DbAggregate>, DbAggregate, ReadOnlyCollection<DbAggregate>> enumerableValidator2 = ArgumentValidation.CreateValidator<KeyValuePair<string, DbAggregate>, DbAggregate, ReadOnlyCollection<DbAggregate>>(aggregates, "aggregates", delegate(KeyValuePair<string, DbAggregate> aggInfo, int idx)
			{
				ArgumentValidation.CheckNamed<DbAggregate>(aggInfo, "aggregates", idx);
				if (keyNames.Contains(aggInfo.Key))
				{
					throw new ArgumentException(Strings.Cqt_GroupBy_AggregateColumnExistsAsGroupColumn(aggInfo.Key));
				}
				if (aggInfo.Value is DbGroupAggregate)
				{
					if (hasGroupAggregate)
					{
						throw new ArgumentException(Strings.Cqt_GroupBy_MoreThanOneGroupAggregate);
					}
					hasGroupAggregate = true;
				}
				columns.Add(new KeyValuePair<string, TypeUsage>(aggInfo.Key, aggInfo.Value.ResultType));
				return aggInfo.Value;
			}, (List<DbAggregate> aggList) => ArgumentValidation.NewReadOnlyCollection<DbAggregate>(aggList));
			enumerableValidator2.AllowEmpty = true;
			enumerableValidator2.GetName = ((KeyValuePair<string, DbAggregate> aggInfo, int idx) => aggInfo.Key);
			validAggregates = enumerableValidator2.Validate();
			if (validKeys.Count == 0 && validAggregates.Count == 0)
			{
				throw new ArgumentException(Strings.Cqt_GroupBy_AtLeastOneKeyOrAggregate);
			}
			return ArgumentValidation.CreateCollectionOfRowResultType(columns);
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0002C6B0 File Offset: 0x0002A8B0
		internal static ReadOnlyCollection<DbSortClause> ValidateSortArguments(IEnumerable<DbSortClause> sortOrder)
		{
			EnumerableValidator<DbSortClause, DbSortClause, ReadOnlyCollection<DbSortClause>> enumerableValidator = ArgumentValidation.CreateValidator<DbSortClause, DbSortClause, ReadOnlyCollection<DbSortClause>>(sortOrder, "sortOrder", (DbSortClause key, int idx) => key, (List<DbSortClause> keyList) => ArgumentValidation.NewReadOnlyCollection<DbSortClause>(keyList));
			enumerableValidator.AllowEmpty = false;
			return enumerableValidator.Validate();
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0002C710 File Offset: 0x0002A910
		internal static ReadOnlyCollection<DbSortClause> ValidateSort(IEnumerable<DbSortClause> sortOrder)
		{
			return ArgumentValidation.ValidateSortArguments(sortOrder);
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0002C718 File Offset: 0x0002A918
		internal static TypeUsage ValidateConstant(Type type)
		{
			PrimitiveTypeKind primitiveTypeKind;
			if (!ArgumentValidation.TryGetPrimitiveTypeKind(type, out primitiveTypeKind))
			{
				throw new ArgumentException(Strings.Cqt_Constant_InvalidType, "type");
			}
			return TypeHelpers.GetLiteralTypeUsage(primitiveTypeKind);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0002C745 File Offset: 0x0002A945
		internal static TypeUsage ValidateConstant(object value)
		{
			return ArgumentValidation.ValidateConstant(value.GetType());
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0002C754 File Offset: 0x0002A954
		internal static void ValidateConstant(TypeUsage constantType, object value)
		{
			ArgumentValidation.CheckType(constantType, "constantType");
			EnumType enumType;
			if (TypeHelpers.TryGetEdmType<EnumType>(constantType, out enumType))
			{
				Type clrEquivalentType = enumType.UnderlyingType.ClrEquivalentType;
				if (clrEquivalentType != value.GetType() && (!value.GetType().IsEnum() || !ArgumentValidation.ClrEdmEnumTypesMatch(enumType, value.GetType())))
				{
					throw new ArgumentException(Strings.Cqt_Constant_ClrEnumTypeDoesNotMatchEdmEnumType(value.GetType().Name, enumType.Name, clrEquivalentType.Name), "value");
				}
			}
			else
			{
				PrimitiveType primitiveType;
				if (!TypeHelpers.TryGetEdmType<PrimitiveType>(constantType, out primitiveType))
				{
					throw new ArgumentException(Strings.Cqt_Constant_InvalidConstantType(constantType.ToString()), "constantType");
				}
				PrimitiveTypeKind primitiveTypeKind;
				if ((!ArgumentValidation.TryGetPrimitiveTypeKind(value.GetType(), out primitiveTypeKind) || primitiveType.PrimitiveTypeKind != primitiveTypeKind) && (!Helper.IsGeographicType(primitiveType) || primitiveTypeKind != PrimitiveTypeKind.Geography) && (!Helper.IsGeometricType(primitiveType) || primitiveTypeKind != PrimitiveTypeKind.Geometry))
				{
					throw new ArgumentException(Strings.Cqt_Constant_InvalidValueForType(constantType.ToString()), "value");
				}
			}
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0002C884 File Offset: 0x0002AA84
		internal static TypeUsage ValidateCreateRef(EntitySet entitySet, EntityType entityType, IEnumerable<DbExpression> keyValues, out DbExpression keyConstructor)
		{
			ArgumentValidation.CheckEntitySet(entitySet, "entitySet");
			ArgumentValidation.CheckType(entityType, "entityType");
			if (!TypeSemantics.IsValidPolymorphicCast(entitySet.ElementType, entityType))
			{
				throw new ArgumentException(Strings.Cqt_Ref_PolymorphicArgRequired);
			}
			IList<EdmMember> keyMembers = entityType.KeyMembers;
			EnumerableValidator<DbExpression, KeyValuePair<string, DbExpression>, List<KeyValuePair<string, DbExpression>>> enumerableValidator = ArgumentValidation.CreateValidator<DbExpression, KeyValuePair<string, DbExpression>, List<KeyValuePair<string, DbExpression>>>(keyValues, "keyValues", delegate(DbExpression valueExp, int idx)
			{
				ArgumentValidation.RequireCompatibleType(valueExp, keyMembers[idx].TypeUsage, "keyValues", idx);
				return new KeyValuePair<string, DbExpression>(keyMembers[idx].Name, valueExp);
			}, (List<KeyValuePair<string, DbExpression>> columnList) => columnList);
			enumerableValidator.ExpectedElementCount = keyMembers.Count;
			List<KeyValuePair<string, DbExpression>> columnValues = enumerableValidator.Validate();
			keyConstructor = DbExpressionBuilder.NewRow(columnValues);
			return ArgumentValidation.CreateReferenceResultType(entityType);
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0002C930 File Offset: 0x0002AB30
		internal static TypeUsage ValidateRefFromKey(EntitySet entitySet, DbExpression keyValues, EntityType entityType)
		{
			ArgumentValidation.CheckEntitySet(entitySet, "entitySet");
			ArgumentValidation.CheckType(entityType);
			if (!TypeSemantics.IsValidPolymorphicCast(entitySet.ElementType, entityType))
			{
				throw new ArgumentException(Strings.Cqt_Ref_PolymorphicArgRequired);
			}
			TypeUsage requiredResultType = ArgumentValidation.CreateResultType(TypeHelpers.CreateKeyRowType(entitySet.ElementType));
			ArgumentValidation.RequireCompatibleType(keyValues, requiredResultType, "keyValues");
			return ArgumentValidation.CreateReferenceResultType(entityType);
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0002C98C File Offset: 0x0002AB8C
		internal static TypeUsage ValidateNavigate(DbExpression navigateFrom, RelationshipType type, string fromEndName, string toEndName, out RelationshipEndMember fromEnd, out RelationshipEndMember toEnd)
		{
			ArgumentValidation.CheckType(type);
			if (!type.RelationshipEndMembers.TryGetValue(fromEndName, false, out fromEnd))
			{
				throw new ArgumentOutOfRangeException(fromEndName, Strings.Cqt_Factory_NoSuchRelationEnd);
			}
			if (!type.RelationshipEndMembers.TryGetValue(toEndName, false, out toEnd))
			{
				throw new ArgumentOutOfRangeException(toEndName, Strings.Cqt_Factory_NoSuchRelationEnd);
			}
			ArgumentValidation.RequireCompatibleType(navigateFrom, fromEnd, false);
			return ArgumentValidation.CreateResultType(toEnd);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0002C9EC File Offset: 0x0002ABEC
		internal static TypeUsage ValidateNavigate(DbExpression navigateFrom, RelationshipEndMember fromEnd, RelationshipEndMember toEnd, out RelationshipType relType, bool allowAllRelationshipsInSameTypeHierarchy)
		{
			ArgumentValidation.CheckMember(fromEnd, "fromEnd");
			ArgumentValidation.CheckMember(toEnd, "toEnd");
			relType = (fromEnd.DeclaringType as RelationshipType);
			ArgumentValidation.CheckType(relType);
			if (!relType.Equals(toEnd.DeclaringType))
			{
				throw new ArgumentException(Strings.Cqt_Factory_IncompatibleRelationEnds, "toEnd");
			}
			ArgumentValidation.RequireCompatibleType(navigateFrom, fromEnd, allowAllRelationshipsInSameTypeHierarchy);
			return ArgumentValidation.CreateResultType(toEnd);
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0002CA51 File Offset: 0x0002AC51
		internal static TypeUsage ValidateElement(DbExpression argument)
		{
			ArgumentValidation.RequireCollectionArgument<DbElementExpression>(argument);
			return TypeHelpers.GetEdmType<CollectionType>(argument.ResultType).TypeUsage;
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0002CACC File Offset: 0x0002ACCC
		internal static TypeUsage ValidateCase(IEnumerable<DbExpression> whenExpressions, IEnumerable<DbExpression> thenExpressions, DbExpression elseExpression, out DbExpressionList validWhens, out DbExpressionList validThens)
		{
			validWhens = ArgumentValidation.CreateExpressionList(whenExpressions, "whenExpressions", delegate(DbExpression exp, int idx)
			{
				ArgumentValidation.RequireCompatibleType(exp, PrimitiveTypeKind.Boolean, "whenExpressions", idx);
			});
			TypeUsage commonResultType = null;
			validThens = ArgumentValidation.CreateExpressionList(thenExpressions, "thenExpressions", delegate(DbExpression exp, int idx)
			{
				if (commonResultType == null)
				{
					commonResultType = exp.ResultType;
					return;
				}
				commonResultType = TypeHelpers.GetCommonTypeUsage(exp.ResultType, commonResultType);
				if (commonResultType == null)
				{
					throw new ArgumentException(Strings.Cqt_Case_InvalidResultType);
				}
			});
			commonResultType = TypeHelpers.GetCommonTypeUsage(elseExpression.ResultType, commonResultType);
			if (commonResultType == null)
			{
				throw new ArgumentException(Strings.Cqt_Case_InvalidResultType);
			}
			if (validWhens.Count != validThens.Count)
			{
				throw new ArgumentException(Strings.Cqt_Case_WhensMustEqualThens);
			}
			return commonResultType;
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0002CB9C File Offset: 0x0002AD9C
		internal static TypeUsage ValidateFunction(EdmFunction function, IEnumerable<DbExpression> arguments, out DbExpressionList validArgs)
		{
			ArgumentValidation.CheckFunction(function);
			if (!function.IsComposableAttribute)
			{
				throw new ArgumentException(Strings.Cqt_Function_NonComposableInExpression, "function");
			}
			if (!string.IsNullOrEmpty(function.CommandTextAttribute) && !function.HasUserDefinedBody)
			{
				throw new ArgumentException(Strings.Cqt_Function_CommandTextInExpression, "function");
			}
			if (function.ReturnParameter == null)
			{
				throw new ArgumentException(Strings.Cqt_Function_VoidResultInvalid, "function");
			}
			FunctionParameter[] expectedParams = ArgumentValidation.GetExpectedParameters(function);
			validArgs = ArgumentValidation.CreateExpressionList(arguments, "arguments", expectedParams.Length, delegate(DbExpression exp, int idx)
			{
				ArgumentValidation.RequireCompatibleType(exp, expectedParams[idx].TypeUsage, "arguments", idx);
			});
			return function.ReturnParameter.TypeUsage;
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0002CC78 File Offset: 0x0002AE78
		internal static TypeUsage ValidateInvoke(DbLambda lambda, IEnumerable<DbExpression> arguments, out DbExpressionList validArguments)
		{
			validArguments = null;
			EnumerableValidator<DbExpression, DbExpression, DbExpressionList> enumerableValidator = ArgumentValidation.CreateValidator<DbExpression, DbExpression, DbExpressionList>(arguments, "arguments", delegate(DbExpression exp, int idx)
			{
				ArgumentValidation.RequireCompatibleType(exp, lambda.Variables[idx].ResultType, "arguments", idx);
				return exp;
			}, (List<DbExpression> expList) => new DbExpressionList(expList));
			enumerableValidator.ExpectedElementCount = lambda.Variables.Count;
			validArguments = enumerableValidator.Validate();
			return lambda.Body.ResultType;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0002CCF8 File Offset: 0x0002AEF8
		internal static TypeUsage ValidateNewEmptyCollection(TypeUsage collectionType, out DbExpressionList validElements)
		{
			ArgumentValidation.CheckType(collectionType, "collectionType");
			if (!TypeSemantics.IsCollectionType(collectionType))
			{
				throw new ArgumentException(Strings.Cqt_NewInstance_CollectionTypeRequired, "collectionType");
			}
			validElements = new DbExpressionList(new DbExpression[0]);
			return collectionType;
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0002CD7C File Offset: 0x0002AF7C
		internal static TypeUsage ValidateNewRow(IEnumerable<KeyValuePair<string, DbExpression>> columnValues, out DbExpressionList validElements)
		{
			List<KeyValuePair<string, TypeUsage>> columnTypes = new List<KeyValuePair<string, TypeUsage>>();
			EnumerableValidator<KeyValuePair<string, DbExpression>, DbExpression, DbExpressionList> enumerableValidator = ArgumentValidation.CreateValidator<KeyValuePair<string, DbExpression>, DbExpression, DbExpressionList>(columnValues, "columnValues", delegate(KeyValuePair<string, DbExpression> columnValue, int idx)
			{
				ArgumentValidation.CheckNamed<DbExpression>(columnValue, "columnValues", idx);
				columnTypes.Add(new KeyValuePair<string, TypeUsage>(columnValue.Key, columnValue.Value.ResultType));
				return columnValue.Value;
			}, (List<DbExpression> expList) => new DbExpressionList(expList));
			enumerableValidator.GetName = ((KeyValuePair<string, DbExpression> columnValue, int idx) => columnValue.Key);
			validElements = enumerableValidator.Validate();
			return ArgumentValidation.CreateResultType(TypeHelpers.CreateRowType(columnTypes));
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0002CE68 File Offset: 0x0002B068
		internal static TypeUsage ValidateNew(TypeUsage instanceType, IEnumerable<DbExpression> arguments, out DbExpressionList validArguments)
		{
			ArgumentValidation.CheckType(instanceType, "instanceType");
			CollectionType collectionType = null;
			if (TypeHelpers.TryGetEdmType<CollectionType>(instanceType, out collectionType) && collectionType != null)
			{
				TypeUsage elementType = collectionType.TypeUsage;
				validArguments = ArgumentValidation.CreateExpressionList(arguments, "arguments", true, delegate(DbExpression exp, int idx)
				{
					ArgumentValidation.RequireCompatibleType(exp, elementType, "arguments", idx);
				});
			}
			else
			{
				List<TypeUsage> expectedTypes = ArgumentValidation.GetStructuralMemberTypes(instanceType);
				int pos = 0;
				validArguments = ArgumentValidation.CreateExpressionList(arguments, "arguments", expectedTypes.Count, delegate(DbExpression exp, int idx)
				{
					ArgumentValidation.RequireCompatibleType(exp, expectedTypes[pos++], "arguments", idx);
				});
			}
			return instanceType;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0002CEFC File Offset: 0x0002B0FC
		private static List<TypeUsage> GetStructuralMemberTypes(TypeUsage instanceType)
		{
			StructuralType structuralType = instanceType.EdmType as StructuralType;
			if (structuralType == null)
			{
				throw new ArgumentException(Strings.Cqt_NewInstance_StructuralTypeRequired, "instanceType");
			}
			if (structuralType.Abstract)
			{
				throw new ArgumentException(Strings.Cqt_NewInstance_CannotInstantiateAbstractType(instanceType.ToString()), "instanceType");
			}
			IBaseList<EdmMember> allStructuralMembers = TypeHelpers.GetAllStructuralMembers(structuralType);
			if (allStructuralMembers == null || allStructuralMembers.Count < 1)
			{
				throw new ArgumentException(Strings.Cqt_NewInstance_CannotInstantiateMemberlessType(instanceType.ToString()), "instanceType");
			}
			List<TypeUsage> list = new List<TypeUsage>(allStructuralMembers.Count);
			for (int i = 0; i < allStructuralMembers.Count; i++)
			{
				list.Add(Helper.GetModelTypeUsage(allStructuralMembers[i]));
			}
			return list;
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0002CFA0 File Offset: 0x0002B1A0
		internal static TypeUsage ValidateNewEntityWithRelationships(EntityType entityType, IEnumerable<DbExpression> attributeValues, IList<DbRelatedEntityRef> relationships, out DbExpressionList validArguments, out ReadOnlyCollection<DbRelatedEntityRef> validRelatedRefs)
		{
			TypeUsage typeUsage = ArgumentValidation.CreateResultType(entityType);
			typeUsage = ArgumentValidation.ValidateNew(typeUsage, attributeValues, out validArguments);
			if (relationships.Count > 0)
			{
				List<DbRelatedEntityRef> list = new List<DbRelatedEntityRef>(relationships.Count);
				for (int i = 0; i < relationships.Count; i++)
				{
					DbRelatedEntityRef dbRelatedEntityRef = relationships[i];
					EntityTypeBase elementType = TypeHelpers.GetEdmType<RefType>(dbRelatedEntityRef.SourceEnd.TypeUsage).ElementType;
					if (!entityType.EdmEquals(elementType) && !entityType.IsSubtypeOf(elementType))
					{
						throw new ArgumentException(Strings.Cqt_NewInstance_IncompatibleRelatedEntity_SourceTypeNotValid, StringUtil.FormatIndex("relationships", i));
					}
					list.Add(dbRelatedEntityRef);
				}
				validRelatedRefs = new ReadOnlyCollection<DbRelatedEntityRef>(list);
			}
			else
			{
				validRelatedRefs = new ReadOnlyCollection<DbRelatedEntityRef>(new DbRelatedEntityRef[0]);
			}
			return typeUsage;
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0002D050 File Offset: 0x0002B250
		internal static TypeUsage ValidateProperty(DbExpression instance, string propertyName, bool ignoreCase, out EdmMember foundMember)
		{
			StructuralType structuralType;
			if (TypeHelpers.TryGetEdmType<StructuralType>(instance.ResultType, out structuralType) && structuralType.Members.TryGetValue(propertyName, ignoreCase, out foundMember) && foundMember != null && (Helper.IsRelationshipEndMember(foundMember) || Helper.IsEdmProperty(foundMember) || Helper.IsNavigationProperty(foundMember)))
			{
				return Helper.GetModelTypeUsage(foundMember);
			}
			throw new ArgumentOutOfRangeException("propertyName", Strings.NoSuchProperty(propertyName, instance.ResultType.ToString()));
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0002D0C0 File Offset: 0x0002B2C0
		private static void CheckNamed<T>(KeyValuePair<string, T> element, string argumentName, int index)
		{
			if (string.IsNullOrEmpty(element.Key))
			{
				if (index != -1)
				{
					argumentName = StringUtil.FormatIndex(argumentName, index);
				}
				throw new ArgumentNullException(string.Format(CultureInfo.InvariantCulture, "{0}.Key", new object[]
				{
					argumentName
				}));
			}
			if (element.Value == null)
			{
				if (index != -1)
				{
					argumentName = StringUtil.FormatIndex(argumentName, index);
				}
				throw new ArgumentNullException(string.Format(CultureInfo.InvariantCulture, "{0}.Value", new object[]
				{
					argumentName
				}));
			}
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0002D145 File Offset: 0x0002B345
		private static void CheckReadOnly(GlobalItem item, string varName)
		{
			if (!item.IsReadOnly)
			{
				throw new ArgumentException(Strings.Cqt_General_MetadataNotReadOnly, varName);
			}
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0002D15B File Offset: 0x0002B35B
		private static void CheckReadOnly(TypeUsage item, string varName)
		{
			if (!item.IsReadOnly)
			{
				throw new ArgumentException(Strings.Cqt_General_MetadataNotReadOnly, varName);
			}
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0002D171 File Offset: 0x0002B371
		private static void CheckReadOnly(EntitySetBase item, string varName)
		{
			if (!item.IsReadOnly)
			{
				throw new ArgumentException(Strings.Cqt_General_MetadataNotReadOnly, varName);
			}
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0002D187 File Offset: 0x0002B387
		private static void CheckType(EdmType type)
		{
			ArgumentValidation.CheckType(type, "type");
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0002D194 File Offset: 0x0002B394
		private static void CheckType(EdmType type, string argumentName)
		{
			ArgumentValidation.CheckReadOnly(type, argumentName);
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0002D19D File Offset: 0x0002B39D
		internal static void CheckType(TypeUsage type)
		{
			ArgumentValidation.CheckType(type, "type");
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0002D1AA File Offset: 0x0002B3AA
		internal static void CheckType(TypeUsage type, string varName)
		{
			ArgumentValidation.CheckReadOnly(type, varName);
			if (!ArgumentValidation.CheckDataSpace(type))
			{
				throw new ArgumentException(Strings.Cqt_Metadata_TypeUsageIncorrectSpace, "type");
			}
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0002D1CB File Offset: 0x0002B3CB
		internal static void CheckMember(EdmMember memberMeta, string varName)
		{
			ArgumentValidation.CheckReadOnly(memberMeta.DeclaringType, varName);
			if (!ArgumentValidation.CheckDataSpace(memberMeta.TypeUsage) || !ArgumentValidation.CheckDataSpace(memberMeta.DeclaringType))
			{
				throw new ArgumentException(Strings.Cqt_Metadata_EdmMemberIncorrectSpace, varName);
			}
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0002D1FF File Offset: 0x0002B3FF
		private static void CheckParameter(FunctionParameter paramMeta, string varName)
		{
			ArgumentValidation.CheckReadOnly(paramMeta.DeclaringFunction, varName);
			if (!ArgumentValidation.CheckDataSpace(paramMeta.TypeUsage))
			{
				throw new ArgumentException(Strings.Cqt_Metadata_FunctionParameterIncorrectSpace, varName);
			}
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0002D228 File Offset: 0x0002B428
		[SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly")]
		private static void CheckFunction(EdmFunction function)
		{
			ArgumentValidation.CheckReadOnly(function, "function");
			if (!ArgumentValidation.CheckDataSpace(function))
			{
				throw new ArgumentException(Strings.Cqt_Metadata_FunctionIncorrectSpace, "function");
			}
			if (function.IsComposableAttribute && function.ReturnParameter == null)
			{
				throw new ArgumentException(Strings.Cqt_Metadata_FunctionReturnParameterNull, "function");
			}
			if (function.ReturnParameter != null && !ArgumentValidation.CheckDataSpace(function.ReturnParameter.TypeUsage))
			{
				throw new ArgumentException(Strings.Cqt_Metadata_FunctionParameterIncorrectSpace, "function.ReturnParameter");
			}
			IList<FunctionParameter> parameters = function.Parameters;
			for (int i = 0; i < parameters.Count; i++)
			{
				ArgumentValidation.CheckParameter(parameters[i], StringUtil.FormatIndex("function.Parameters", i));
			}
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0002D2D4 File Offset: 0x0002B4D4
		internal static void CheckEntitySet(EntitySetBase entitySet, string varName)
		{
			ArgumentValidation.CheckReadOnly(entitySet, varName);
			if (entitySet.EntityContainer == null)
			{
				throw new ArgumentException(Strings.Cqt_Metadata_EntitySetEntityContainerNull, varName);
			}
			if (!ArgumentValidation.CheckDataSpace(entitySet.EntityContainer))
			{
				throw new ArgumentException(Strings.Cqt_Metadata_EntitySetIncorrectSpace, varName);
			}
			if (!ArgumentValidation.CheckDataSpace(entitySet.ElementType))
			{
				throw new ArgumentException(Strings.Cqt_Metadata_EntitySetIncorrectSpace, varName);
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0002D32E File Offset: 0x0002B52E
		private static bool CheckDataSpace(TypeUsage type)
		{
			return ArgumentValidation.CheckDataSpace(type.EdmType);
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0002D33C File Offset: 0x0002B53C
		private static bool CheckDataSpace(GlobalItem item)
		{
			if (BuiltInTypeKind.PrimitiveType == item.BuiltInTypeKind || (BuiltInTypeKind.EdmFunction == item.BuiltInTypeKind && DataSpace.CSpace == item.DataSpace))
			{
				return true;
			}
			if (Helper.IsRowType(item))
			{
				foreach (EdmProperty edmProperty in ((RowType)item).Properties)
				{
					if (!ArgumentValidation.CheckDataSpace(edmProperty.TypeUsage))
					{
						return false;
					}
				}
				return true;
			}
			if (Helper.IsCollectionType(item))
			{
				return ArgumentValidation.CheckDataSpace(((CollectionType)item).TypeUsage);
			}
			if (Helper.IsRefType(item))
			{
				return ArgumentValidation.CheckDataSpace(((RefType)item).ElementType);
			}
			return item.DataSpace == DataSpace.SSpace || item.DataSpace == DataSpace.CSpace;
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0002D410 File Offset: 0x0002B610
		internal static TypeUsage CreateCollectionOfRowResultType(List<KeyValuePair<string, TypeUsage>> columns)
		{
			return TypeUsage.Create(TypeHelpers.CreateCollectionType(TypeUsage.Create(TypeHelpers.CreateRowType(columns))));
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0002D434 File Offset: 0x0002B634
		private static TypeUsage CreateResultType(EdmType resultType)
		{
			return TypeUsage.Create(resultType);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0002D43C File Offset: 0x0002B63C
		private static TypeUsage CreateResultType(RelationshipEndMember end)
		{
			TypeUsage typeUsage = end.TypeUsage;
			if (!TypeSemantics.IsReferenceType(typeUsage))
			{
				typeUsage = TypeHelpers.CreateReferenceTypeUsage(TypeHelpers.GetEdmType<EntityType>(typeUsage));
			}
			if (RelationshipMultiplicity.Many == end.RelationshipMultiplicity)
			{
				typeUsage = TypeHelpers.CreateCollectionTypeUsage(typeUsage);
			}
			return typeUsage;
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0002D475 File Offset: 0x0002B675
		internal static TypeUsage CreateReferenceResultType(EntityTypeBase referencedEntityType)
		{
			return TypeUsage.Create(TypeHelpers.CreateReferenceType(referencedEntityType));
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0002D482 File Offset: 0x0002B682
		private static bool TryGetPrimitiveTypeKind(Type clrType, out PrimitiveTypeKind primitiveTypeKind)
		{
			return ClrProviderManifest.TryGetPrimitiveTypeKind(clrType, out primitiveTypeKind);
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0002D48C File Offset: 0x0002B68C
		private static bool ClrEdmEnumTypesMatch(EnumType edmEnumType, Type clrEnumType)
		{
			if (clrEnumType.Name != edmEnumType.Name || clrEnumType.GetEnumNames().Length < edmEnumType.Members.Count)
			{
				return false;
			}
			PrimitiveTypeKind primitiveTypeKind;
			if (!ArgumentValidation.TryGetPrimitiveTypeKind(clrEnumType.GetEnumUnderlyingType(), out primitiveTypeKind) || primitiveTypeKind != edmEnumType.UnderlyingType.PrimitiveTypeKind)
			{
				return false;
			}
			foreach (EnumMember enumMember in edmEnumType.Members)
			{
				if (!clrEnumType.GetEnumNames().Contains(enumMember.Name) || !enumMember.Value.Equals(Convert.ChangeType(Enum.Parse(clrEnumType, enumMember.Name), clrEnumType.GetEnumUnderlyingType(), CultureInfo.InvariantCulture)))
				{
					return false;
				}
			}
			return true;
		}
	}
}
