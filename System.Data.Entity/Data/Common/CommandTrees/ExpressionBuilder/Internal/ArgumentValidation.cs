using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.CommandTrees.Internal;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Globalization;
using System.Linq;

namespace System.Data.Common.CommandTrees.ExpressionBuilder.Internal
{
	// Token: 0x0200042A RID: 1066
	internal static class ArgumentValidation
	{
		// Token: 0x06003871 RID: 14449 RVA: 0x000D6318 File Offset: 0x000D4518
		internal static ReadOnlyCollection<TElement> NewReadOnlyCollection<TElement>(IList<TElement> list)
		{
			return new ReadOnlyCollection<TElement>(list);
		}

		// Token: 0x06003872 RID: 14450 RVA: 0x000D6320 File Offset: 0x000D4520
		private static void RequirePolymorphicType(TypeUsage type, string typeArgumentName)
		{
			if (!TypeSemantics.IsPolymorphicType(type))
			{
				throw EntityUtil.Argument(Strings.Cqt_General_PolymorphicTypeRequired(TypeHelpers.GetFullName(type)), "type");
			}
		}

		// Token: 0x06003873 RID: 14451 RVA: 0x000D6340 File Offset: 0x000D4540
		private static void RequireCompatibleType(DbExpression expression, TypeUsage requiredResultType, string argumentName)
		{
			ArgumentValidation.RequireCompatibleType(expression, requiredResultType, argumentName, -1);
		}

		// Token: 0x06003874 RID: 14452 RVA: 0x000D634B File Offset: 0x000D454B
		private static void RequireCompatibleType(DbExpression expression, TypeUsage requiredResultType, string argumentName, int argumentIndex)
		{
			if (!TypeSemantics.IsStructurallyEqualOrPromotableTo(expression.ResultType, requiredResultType))
			{
				if (argumentIndex != -1)
				{
					argumentName = StringUtil.FormatIndex(argumentName, argumentIndex);
				}
				throw EntityUtil.Argument(Strings.Cqt_ExpressionLink_TypeMismatch(TypeHelpers.GetFullName(expression.ResultType), TypeHelpers.GetFullName(requiredResultType)), argumentName);
			}
		}

		// Token: 0x06003875 RID: 14453 RVA: 0x000D6385 File Offset: 0x000D4585
		private static void RequireCompatibleType(DbExpression expression, PrimitiveTypeKind requiredResultType, string argumentName)
		{
			ArgumentValidation.RequireCompatibleType(expression, requiredResultType, argumentName, -1);
		}

		// Token: 0x06003876 RID: 14454 RVA: 0x000D6390 File Offset: 0x000D4590
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
				throw EntityUtil.Argument(Strings.Cqt_ExpressionLink_TypeMismatch(flag ? Enum.GetName(typeof(PrimitiveTypeKind), primitiveTypeKind) : TypeHelpers.GetFullName(expression.ResultType), Enum.GetName(typeof(PrimitiveTypeKind), requiredResultType)), argumentName);
			}
		}

		// Token: 0x06003877 RID: 14455 RVA: 0x000D6408 File Offset: 0x000D4608
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
					throw EntityUtil.Argument(Strings.Cqt_RelNav_WrongSourceType(TypeHelpers.GetFullName(typeUsage)), "from");
				}
			}
			else if (!TypeSemantics.IsStructurallyEqualOrPromotableTo(from.ResultType.EdmType, typeUsage.EdmType))
			{
				throw EntityUtil.Argument(Strings.Cqt_RelNav_WrongSourceType(TypeHelpers.GetFullName(typeUsage)), "from");
			}
		}

		// Token: 0x06003878 RID: 14456 RVA: 0x000D6485 File Offset: 0x000D4685
		private static void RequireCollectionArgument<TExpressionType>(DbExpression argument)
		{
			if (!TypeSemantics.IsCollectionType(argument.ResultType))
			{
				throw EntityUtil.Argument(Strings.Cqt_Unary_CollectionRequired(typeof(TExpressionType).Name), "argument");
			}
		}

		// Token: 0x06003879 RID: 14457 RVA: 0x000D64B4 File Offset: 0x000D46B4
		private static TypeUsage RequireCollectionArguments<TExpressionType>(DbExpression left, DbExpression right)
		{
			if (!TypeSemantics.IsCollectionType(left.ResultType) || !TypeSemantics.IsCollectionType(right.ResultType))
			{
				throw EntityUtil.Argument(Strings.Cqt_Binary_CollectionsRequired(typeof(TExpressionType).Name));
			}
			TypeUsage commonTypeUsage = TypeHelpers.GetCommonTypeUsage(left.ResultType, right.ResultType);
			if (commonTypeUsage == null)
			{
				throw EntityUtil.Argument(Strings.Cqt_Binary_CollectionsRequired(typeof(TExpressionType).Name));
			}
			return commonTypeUsage;
		}

		// Token: 0x0600387A RID: 14458 RVA: 0x000D6528 File Offset: 0x000D4728
		private static TypeUsage RequireComparableCollectionArguments<TExpressionType>(DbExpression left, DbExpression right)
		{
			TypeUsage result = ArgumentValidation.RequireCollectionArguments<TExpressionType>(left, right);
			if (!TypeHelpers.IsSetComparableOpType(TypeHelpers.GetElementTypeUsage(left.ResultType)))
			{
				throw EntityUtil.Argument(Strings.Cqt_InvalidTypeForSetOperation(TypeHelpers.GetElementTypeUsage(left.ResultType).Identity, typeof(TExpressionType).Name), "left");
			}
			if (!TypeHelpers.IsSetComparableOpType(TypeHelpers.GetElementTypeUsage(right.ResultType)))
			{
				throw EntityUtil.Argument(Strings.Cqt_InvalidTypeForSetOperation(TypeHelpers.GetElementTypeUsage(right.ResultType).Identity, typeof(TExpressionType).Name), "right");
			}
			return result;
		}

		// Token: 0x0600387B RID: 14459 RVA: 0x000D65C0 File Offset: 0x000D47C0
		private static EnumerableValidator<TElementIn, TElementOut, TResult> CreateValidator<TElementIn, TElementOut, TResult>(IEnumerable<TElementIn> argument, string argumentName, Func<TElementIn, int, TElementOut> convertElement, Func<List<TElementOut>, TResult> createResult)
		{
			return new EnumerableValidator<TElementIn, TElementOut, TResult>(argument, argumentName)
			{
				ConvertElement = convertElement,
				CreateResult = createResult
			};
		}

		// Token: 0x0600387C RID: 14460 RVA: 0x000D65E4 File Offset: 0x000D47E4
		private static DbExpressionList CreateExpressionList(IEnumerable<DbExpression> arguments, string argumentName, Action<DbExpression, int> validationCallback)
		{
			return ArgumentValidation.CreateExpressionList(arguments, argumentName, false, validationCallback);
		}

		// Token: 0x0600387D RID: 14461 RVA: 0x000D65F0 File Offset: 0x000D47F0
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

		// Token: 0x0600387E RID: 14462 RVA: 0x000D664C File Offset: 0x000D484C
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

		// Token: 0x0600387F RID: 14463 RVA: 0x000D66AD File Offset: 0x000D48AD
		private static TypeUsage ValidateBinary(DbExpression left, DbExpression right)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(left, "left");
			EntityUtil.CheckArgumentNull<DbExpression>(right, "right");
			return TypeHelpers.GetCommonTypeUsage(left.ResultType, right.ResultType);
		}

		// Token: 0x06003880 RID: 14464 RVA: 0x000D66D8 File Offset: 0x000D48D8
		private static void ValidateUnary(DbExpression argument)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(argument, "argument");
		}

		// Token: 0x06003881 RID: 14465 RVA: 0x000D66E6 File Offset: 0x000D48E6
		private static void ValidateTypeUnary(DbExpression argument, TypeUsage type, string typeArgumentName)
		{
			ArgumentValidation.ValidateUnary(argument);
			ArgumentValidation.CheckType(type, typeArgumentName);
		}

		// Token: 0x06003882 RID: 14466 RVA: 0x000D66F8 File Offset: 0x000D48F8
		internal static TypeUsage ValidateBindAs(DbExpression input, string varName)
		{
			EntityUtil.CheckArgumentNull<string>(varName, "varName");
			EntityUtil.CheckArgumentNull<DbExpression>(input, "input");
			if (string.IsNullOrEmpty(varName))
			{
				throw EntityUtil.Argument(Strings.Cqt_Binding_VariableNameNotValid, "varName");
			}
			TypeUsage result = null;
			if (!TypeHelpers.TryGetCollectionElementType(input.ResultType, out result))
			{
				throw EntityUtil.Argument(Strings.Cqt_Binding_CollectionRequired, "input");
			}
			return result;
		}

		// Token: 0x06003883 RID: 14467 RVA: 0x000D6758 File Offset: 0x000D4958
		internal static TypeUsage ValidateGroupBindAs(DbExpression input, string varName, string groupVarName)
		{
			EntityUtil.CheckArgumentNull<string>(varName, "varName");
			EntityUtil.CheckArgumentNull<string>(groupVarName, "groupVarName");
			EntityUtil.CheckArgumentNull<DbExpression>(input, "input");
			if (string.IsNullOrEmpty(varName))
			{
				throw EntityUtil.Argument(Strings.Cqt_Binding_VariableNameNotValid, "varName");
			}
			if (string.IsNullOrEmpty(groupVarName))
			{
				throw EntityUtil.Argument(Strings.Cqt_GroupBinding_GroupVariableNameNotValid, "groupVarName");
			}
			TypeUsage result = null;
			if (!TypeHelpers.TryGetCollectionElementType(input.ResultType, out result))
			{
				throw EntityUtil.Argument(Strings.Cqt_GroupBinding_CollectionRequired, "input");
			}
			return result;
		}

		// Token: 0x06003884 RID: 14468 RVA: 0x000D67DB File Offset: 0x000D49DB
		private static FunctionParameter[] GetExpectedParameters(EdmFunction function)
		{
			return (from p in function.Parameters
			where p.Mode == ParameterMode.In || p.Mode == ParameterMode.InOut
			select p).ToArray<FunctionParameter>();
		}

		// Token: 0x06003885 RID: 14469 RVA: 0x000D680C File Offset: 0x000D4A0C
		internal static DbExpressionList ValidateFunctionAggregate(EdmFunction function, IEnumerable<DbExpression> args)
		{
			ArgumentValidation.CheckFunction(function);
			if (!TypeSemantics.IsAggregateFunction(function) || function.ReturnParameter == null)
			{
				throw EntityUtil.Argument(Strings.Cqt_Aggregate_InvalidFunction, "function");
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

		// Token: 0x06003886 RID: 14470 RVA: 0x000D6872 File Offset: 0x000D4A72
		internal static DbExpressionList ValidateGroupAggregate(DbExpression argument)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(argument, "argument");
			return new DbExpressionList(new DbExpression[]
			{
				argument
			});
		}

		// Token: 0x06003887 RID: 14471 RVA: 0x000D688F File Offset: 0x000D4A8F
		internal static void ValidateSortClause(DbExpression key)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(key, "key");
			if (!TypeHelpers.IsValidSortOpKeyType(key.ResultType))
			{
				throw EntityUtil.Argument(Strings.Cqt_Sort_OrderComparable, "key");
			}
		}

		// Token: 0x06003888 RID: 14472 RVA: 0x000D68BC File Offset: 0x000D4ABC
		internal static void ValidateSortClause(DbExpression key, string collation)
		{
			ArgumentValidation.ValidateSortClause(key);
			EntityUtil.CheckArgumentNull<string>(collation, "collation");
			if (StringUtil.IsNullOrEmptyOrWhiteSpace(collation))
			{
				throw EntityUtil.ArgumentOutOfRange(Strings.Cqt_Sort_EmptyCollationInvalid, "collation");
			}
			if (!TypeSemantics.IsPrimitiveType(key.ResultType, PrimitiveTypeKind.String))
			{
				throw EntityUtil.Argument(Strings.Cqt_Sort_NonStringCollationInvalid, "collation");
			}
		}

		// Token: 0x06003889 RID: 14473 RVA: 0x000D6914 File Offset: 0x000D4B14
		internal static ReadOnlyCollection<DbVariableReferenceExpression> ValidateLambda(IEnumerable<DbVariableReferenceExpression> variables, DbExpression body)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(body, "body");
			EnumerableValidator<DbVariableReferenceExpression, DbVariableReferenceExpression, ReadOnlyCollection<DbVariableReferenceExpression>> enumerableValidator = ArgumentValidation.CreateValidator<DbVariableReferenceExpression, DbVariableReferenceExpression, ReadOnlyCollection<DbVariableReferenceExpression>>(variables, "variables", delegate(DbVariableReferenceExpression varExp, int idx)
			{
				if (varExp == null)
				{
					throw EntityUtil.ArgumentNull(StringUtil.FormatIndex("variables", idx));
				}
				return varExp;
			}, (List<DbVariableReferenceExpression> varList) => new ReadOnlyCollection<DbVariableReferenceExpression>(varList));
			enumerableValidator.AllowEmpty = true;
			enumerableValidator.GetName = ((DbVariableReferenceExpression varDef, int idx) => varDef.VariableName);
			return enumerableValidator.Validate();
		}

		// Token: 0x0600388A RID: 14474 RVA: 0x000D69AB File Offset: 0x000D4BAB
		private static void ValidateBinding(DbExpressionBinding binding, string argumentName)
		{
			EntityUtil.CheckArgumentNull<DbExpressionBinding>(binding, argumentName);
		}

		// Token: 0x0600388B RID: 14475 RVA: 0x000D69B5 File Offset: 0x000D4BB5
		private static void ValidateGroupBinding(DbGroupExpressionBinding binding, string argumentName)
		{
			EntityUtil.CheckArgumentNull<DbGroupExpressionBinding>(binding, argumentName);
		}

		// Token: 0x0600388C RID: 14476 RVA: 0x000D69BF File Offset: 0x000D4BBF
		private static void ValidateBound(DbExpressionBinding input, DbExpression argument, string argumentName)
		{
			ArgumentValidation.ValidateBinding(input, "input");
			EntityUtil.CheckArgumentNull<DbExpression>(argument, argumentName);
		}

		// Token: 0x0600388D RID: 14477 RVA: 0x000D69D4 File Offset: 0x000D4BD4
		internal static TypeUsage ValidateQuantifier(DbExpressionBinding input, DbExpression predicate)
		{
			ArgumentValidation.ValidateBound(input, predicate, "predicate");
			ArgumentValidation.RequireCompatibleType(predicate, PrimitiveTypeKind.Boolean, "predicate");
			return predicate.ResultType;
		}

		// Token: 0x0600388E RID: 14478 RVA: 0x000D69F4 File Offset: 0x000D4BF4
		internal static TypeUsage ValidateApply(DbExpressionBinding input, DbExpressionBinding apply)
		{
			ArgumentValidation.ValidateBinding(input, "input");
			ArgumentValidation.ValidateBinding(apply, "apply");
			if (input.VariableName.Equals(apply.VariableName, StringComparison.Ordinal))
			{
				throw EntityUtil.Argument(Strings.Cqt_Apply_DuplicateVariableNames);
			}
			return ArgumentValidation.CreateCollectionOfRowResultType(new List<KeyValuePair<string, TypeUsage>>
			{
				new KeyValuePair<string, TypeUsage>(input.VariableName, input.VariableType),
				new KeyValuePair<string, TypeUsage>(apply.VariableName, apply.VariableType)
			});
		}

		// Token: 0x0600388F RID: 14479 RVA: 0x000D6A70 File Offset: 0x000D4C70
		internal static ReadOnlyCollection<DbExpressionBinding> ValidateCrossJoin(IEnumerable<DbExpressionBinding> inputs, out TypeUsage resultType)
		{
			EntityUtil.CheckArgumentNull<IEnumerable<DbExpressionBinding>>(inputs, "inputs");
			List<DbExpressionBinding> list = new List<DbExpressionBinding>();
			List<KeyValuePair<string, TypeUsage>> list2 = new List<KeyValuePair<string, TypeUsage>>();
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			IEnumerator<DbExpressionBinding> enumerator = inputs.GetEnumerator();
			int num = 0;
			while (enumerator.MoveNext())
			{
				DbExpressionBinding dbExpressionBinding = enumerator.Current;
				ArgumentValidation.ValidateBinding(dbExpressionBinding, StringUtil.FormatIndex("inputs", num));
				int num2 = -1;
				if (dictionary.TryGetValue(dbExpressionBinding.VariableName, out num2))
				{
					throw EntityUtil.Argument(Strings.Cqt_CrossJoin_DuplicateVariableNames(num2, num, dbExpressionBinding.VariableName));
				}
				list.Add(dbExpressionBinding);
				dictionary.Add(dbExpressionBinding.VariableName, num);
				list2.Add(new KeyValuePair<string, TypeUsage>(dbExpressionBinding.VariableName, dbExpressionBinding.VariableType));
				num++;
			}
			if (list.Count < 2)
			{
				throw EntityUtil.Argument(Strings.Cqt_CrossJoin_AtLeastTwoInputs, "inputs");
			}
			resultType = ArgumentValidation.CreateCollectionOfRowResultType(list2);
			return list.AsReadOnly();
		}

		// Token: 0x06003890 RID: 14480 RVA: 0x000D6B64 File Offset: 0x000D4D64
		internal static TypeUsage ValidateJoin(DbExpressionBinding left, DbExpressionBinding right, DbExpression joinCondition)
		{
			ArgumentValidation.ValidateBinding(left, "left");
			ArgumentValidation.ValidateBinding(left, "right");
			EntityUtil.CheckArgumentNull<DbExpression>(joinCondition, "joinCondition");
			if (left.VariableName.Equals(right.VariableName, StringComparison.Ordinal))
			{
				throw EntityUtil.Argument(Strings.Cqt_Join_DuplicateVariableNames);
			}
			ArgumentValidation.RequireCompatibleType(joinCondition, PrimitiveTypeKind.Boolean, "joinCondition");
			return ArgumentValidation.CreateCollectionOfRowResultType(new List<KeyValuePair<string, TypeUsage>>(2)
			{
				new KeyValuePair<string, TypeUsage>(left.VariableName, left.VariableType),
				new KeyValuePair<string, TypeUsage>(right.VariableName, right.VariableType)
			});
		}

		// Token: 0x06003891 RID: 14481 RVA: 0x000D6BF9 File Offset: 0x000D4DF9
		internal static TypeUsage ValidateFilter(DbExpressionBinding input, DbExpression predicate)
		{
			ArgumentValidation.ValidateBound(input, predicate, "predicate");
			ArgumentValidation.RequireCompatibleType(predicate, PrimitiveTypeKind.Boolean, "predicate");
			return input.Expression.ResultType;
		}

		// Token: 0x06003892 RID: 14482 RVA: 0x000D6C20 File Offset: 0x000D4E20
		internal static TypeUsage ValidateGroupBy(DbGroupExpressionBinding input, IEnumerable<KeyValuePair<string, DbExpression>> keys, IEnumerable<KeyValuePair<string, DbAggregate>> aggregates, out DbExpressionList validKeys, out ReadOnlyCollection<DbAggregate> validAggregates)
		{
			ArgumentValidation.ValidateGroupBinding(input, "input");
			List<KeyValuePair<string, TypeUsage>> columns = new List<KeyValuePair<string, TypeUsage>>();
			HashSet<string> keyNames = new HashSet<string>();
			EnumerableValidator<KeyValuePair<string, DbExpression>, DbExpression, DbExpressionList> enumerableValidator = ArgumentValidation.CreateValidator<KeyValuePair<string, DbExpression>, DbExpression, DbExpressionList>(keys, "keys", delegate(KeyValuePair<string, DbExpression> keyInfo, int index)
			{
				ArgumentValidation.CheckNamed<DbExpression>(keyInfo, "keys", index);
				if (!TypeHelpers.IsValidGroupKeyType(keyInfo.Value.ResultType))
				{
					throw EntityUtil.Argument(Strings.Cqt_GroupBy_KeyNotEqualityComparable(keyInfo.Key));
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
					throw EntityUtil.Argument(Strings.Cqt_GroupBy_AggregateColumnExistsAsGroupColumn(aggInfo.Key));
				}
				if (aggInfo.Value is DbGroupAggregate)
				{
					if (hasGroupAggregate)
					{
						throw EntityUtil.Argument(Strings.Cqt_GroupBy_MoreThanOneGroupAggregate);
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
				throw EntityUtil.Argument(Strings.Cqt_GroupBy_AtLeastOneKeyOrAggregate);
			}
			return ArgumentValidation.CreateCollectionOfRowResultType(columns);
		}

		// Token: 0x06003893 RID: 14483 RVA: 0x000D6D5B File Offset: 0x000D4F5B
		internal static TypeUsage ValidateProject(DbExpressionBinding input, DbExpression projection)
		{
			ArgumentValidation.ValidateBound(input, projection, "projection");
			return ArgumentValidation.CreateCollectionResultType(projection.ResultType);
		}

		// Token: 0x06003894 RID: 14484 RVA: 0x000D6D74 File Offset: 0x000D4F74
		private static ReadOnlyCollection<DbSortClause> ValidateSortArguments(DbExpressionBinding input, IEnumerable<DbSortClause> sortOrder)
		{
			ArgumentValidation.ValidateBinding(input, "input");
			EnumerableValidator<DbSortClause, DbSortClause, ReadOnlyCollection<DbSortClause>> enumerableValidator = ArgumentValidation.CreateValidator<DbSortClause, DbSortClause, ReadOnlyCollection<DbSortClause>>(sortOrder, "sortOrder", (DbSortClause key, int idx) => key, (List<DbSortClause> keyList) => ArgumentValidation.NewReadOnlyCollection<DbSortClause>(keyList));
			enumerableValidator.AllowEmpty = false;
			return enumerableValidator.Validate();
		}

		// Token: 0x06003895 RID: 14485 RVA: 0x000D6DE4 File Offset: 0x000D4FE4
		internal static ReadOnlyCollection<DbSortClause> ValidateSkip(DbExpressionBinding input, IEnumerable<DbSortClause> sortOrder, DbExpression count)
		{
			ReadOnlyCollection<DbSortClause> result = ArgumentValidation.ValidateSortArguments(input, sortOrder);
			EntityUtil.CheckArgumentNull<DbExpression>(count, "count");
			if (!TypeSemantics.IsIntegerNumericType(count.ResultType))
			{
				throw EntityUtil.Argument(Strings.Cqt_Skip_IntegerRequired, "count");
			}
			if (count.ExpressionKind != DbExpressionKind.Constant && count.ExpressionKind != DbExpressionKind.ParameterReference)
			{
				throw EntityUtil.Argument(Strings.Cqt_Skip_ConstantOrParameterRefRequired, "count");
			}
			if (ArgumentValidation.IsConstantNegativeInteger(count))
			{
				throw EntityUtil.Argument(Strings.Cqt_Skip_NonNegativeCountRequired, "count");
			}
			return result;
		}

		// Token: 0x06003896 RID: 14486 RVA: 0x000D6E5E File Offset: 0x000D505E
		internal static ReadOnlyCollection<DbSortClause> ValidateSort(DbExpressionBinding input, IEnumerable<DbSortClause> sortOrder)
		{
			return ArgumentValidation.ValidateSortArguments(input, sortOrder);
		}

		// Token: 0x06003897 RID: 14487 RVA: 0x000D6E67 File Offset: 0x000D5067
		internal static void ValidateNull(TypeUsage nullType)
		{
			ArgumentValidation.CheckType(nullType, "nullType");
		}

		// Token: 0x06003898 RID: 14488 RVA: 0x000D6E74 File Offset: 0x000D5074
		internal static TypeUsage ValidateConstant(object value)
		{
			EntityUtil.CheckArgumentNull<object>(value, "value");
			PrimitiveTypeKind primitiveTypeKind;
			if (!ArgumentValidation.TryGetPrimitiveTypeKind(value.GetType(), out primitiveTypeKind))
			{
				throw EntityUtil.Argument(Strings.Cqt_Constant_InvalidType, "value");
			}
			return TypeHelpers.GetLiteralTypeUsage(primitiveTypeKind);
		}

		// Token: 0x06003899 RID: 14489 RVA: 0x000D6EB4 File Offset: 0x000D50B4
		internal static void ValidateConstant(TypeUsage constantType, object value)
		{
			EntityUtil.CheckArgumentNull<object>(value, "value");
			ArgumentValidation.CheckType(constantType, "constantType");
			EnumType enumType;
			if (TypeHelpers.TryGetEdmType<EnumType>(constantType, out enumType))
			{
				Type clrEquivalentType = enumType.UnderlyingType.ClrEquivalentType;
				if ((value.GetType().IsEnum || clrEquivalentType != value.GetType()) && !ArgumentValidation.ClrEdmEnumTypesMatch(enumType, value.GetType()))
				{
					throw EntityUtil.Argument(Strings.Cqt_Constant_ClrEnumTypeDoesNotMatchEdmEnumType(value.GetType().Name, enumType.Name, clrEquivalentType.Name), "value");
				}
			}
			else
			{
				PrimitiveType primitiveType;
				if (!TypeHelpers.TryGetEdmType<PrimitiveType>(constantType, out primitiveType))
				{
					throw EntityUtil.Argument(Strings.Cqt_Constant_InvalidConstantType(constantType.ToString()), "constantType");
				}
				PrimitiveTypeKind primitiveTypeKind;
				if ((!ArgumentValidation.TryGetPrimitiveTypeKind(value.GetType(), out primitiveTypeKind) || primitiveType.PrimitiveTypeKind != primitiveTypeKind) && (!Helper.IsGeographicType(primitiveType) || primitiveTypeKind != PrimitiveTypeKind.Geography) && (!Helper.IsGeometricType(primitiveType) || primitiveTypeKind != PrimitiveTypeKind.Geometry))
				{
					throw EntityUtil.Argument(Strings.Cqt_Constant_InvalidValueForType(constantType.ToString()), "value");
				}
			}
		}

		// Token: 0x0600389A RID: 14490 RVA: 0x000D6FAC File Offset: 0x000D51AC
		internal static void ValidateParameter(TypeUsage type, string name)
		{
			ArgumentValidation.CheckType(type);
			EntityUtil.CheckArgumentNull<string>(name, "name");
			if (!DbCommandTree.IsValidParameterName(name))
			{
				throw EntityUtil.Argument(Strings.Cqt_CommandTree_InvalidParameterName(name), "name");
			}
		}

		// Token: 0x0600389B RID: 14491 RVA: 0x000D6FD9 File Offset: 0x000D51D9
		internal static TypeUsage ValidateScan(EntitySetBase entitySet)
		{
			ArgumentValidation.CheckEntitySet(entitySet, "targetSet");
			return ArgumentValidation.CreateCollectionResultType(entitySet.ElementType);
		}

		// Token: 0x0600389C RID: 14492 RVA: 0x000D6FF1 File Offset: 0x000D51F1
		internal static void ValidateVariable(TypeUsage type, string name)
		{
			ArgumentValidation.CheckType(type);
			EntityUtil.CheckArgumentNull<string>(name, "name");
			if (string.IsNullOrEmpty(name))
			{
				throw EntityUtil.Argument(Strings.Cqt_Binding_VariableNameNotValid, "name");
			}
		}

		// Token: 0x0600389D RID: 14493 RVA: 0x000D7020 File Offset: 0x000D5220
		internal static TypeUsage ValidateAnd(DbExpression left, DbExpression right)
		{
			TypeUsage typeUsage = ArgumentValidation.ValidateBinary(left, right);
			if (typeUsage == null || !TypeSemantics.IsPrimitiveType(typeUsage, PrimitiveTypeKind.Boolean))
			{
				throw EntityUtil.Argument(Strings.Cqt_And_BooleanArgumentsRequired);
			}
			return typeUsage;
		}

		// Token: 0x0600389E RID: 14494 RVA: 0x000D7050 File Offset: 0x000D5250
		internal static TypeUsage ValidateOr(DbExpression left, DbExpression right)
		{
			TypeUsage typeUsage = ArgumentValidation.ValidateBinary(left, right);
			if (typeUsage == null || !TypeSemantics.IsPrimitiveType(typeUsage, PrimitiveTypeKind.Boolean))
			{
				throw EntityUtil.Argument(Strings.Cqt_Or_BooleanArgumentsRequired);
			}
			return typeUsage;
		}

		// Token: 0x0600389F RID: 14495 RVA: 0x000D707D File Offset: 0x000D527D
		internal static TypeUsage ValidateNot(DbExpression argument)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(argument, "argument");
			if (!TypeSemantics.IsPrimitiveType(argument.ResultType, PrimitiveTypeKind.Boolean))
			{
				throw EntityUtil.Argument(Strings.Cqt_Not_BooleanArgumentRequired);
			}
			return argument.ResultType;
		}

		// Token: 0x060038A0 RID: 14496 RVA: 0x000D70AC File Offset: 0x000D52AC
		internal static DbExpressionList ValidateArithmetic(DbExpression argument, out TypeUsage resultType)
		{
			ArgumentValidation.ValidateUnary(argument);
			resultType = argument.ResultType;
			if (!TypeSemantics.IsNumericType(resultType))
			{
				throw EntityUtil.Argument(Strings.Cqt_Arithmetic_NumericCommonType);
			}
			if (TypeSemantics.IsUnsignedNumericType(argument.ResultType))
			{
				TypeUsage typeUsage = null;
				if (!TypeHelpers.TryGetClosestPromotableType(argument.ResultType, out typeUsage))
				{
					throw EntityUtil.Argument(Strings.Cqt_Arithmetic_InvalidUnsignedTypeForUnaryMinus(argument.ResultType.EdmType.FullName));
				}
				resultType = typeUsage;
			}
			return new DbExpressionList(new DbExpression[]
			{
				argument
			});
		}

		// Token: 0x060038A1 RID: 14497 RVA: 0x000D7128 File Offset: 0x000D5328
		internal static DbExpressionList ValidateArithmetic(DbExpression left, DbExpression right, out TypeUsage resultType)
		{
			resultType = ArgumentValidation.ValidateBinary(left, right);
			if (resultType == null || !TypeSemantics.IsNumericType(resultType))
			{
				throw EntityUtil.Argument(Strings.Cqt_Arithmetic_NumericCommonType);
			}
			return new DbExpressionList(new DbExpression[]
			{
				left,
				right
			});
		}

		// Token: 0x060038A2 RID: 14498 RVA: 0x000D7160 File Offset: 0x000D5360
		internal static TypeUsage ValidateComparison(DbExpressionKind kind, DbExpression left, DbExpression right)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(left, "left");
			EntityUtil.CheckArgumentNull<DbExpression>(right, "right");
			bool flag = true;
			bool flag2 = true;
			if (DbExpressionKind.GreaterThanOrEquals == kind || DbExpressionKind.LessThanOrEquals == kind)
			{
				flag = TypeSemantics.IsEqualComparableTo(left.ResultType, right.ResultType);
				flag2 = TypeSemantics.IsOrderComparableTo(left.ResultType, right.ResultType);
			}
			else if (DbExpressionKind.Equals == kind || DbExpressionKind.NotEquals == kind)
			{
				flag = TypeSemantics.IsEqualComparableTo(left.ResultType, right.ResultType);
			}
			else
			{
				flag2 = TypeSemantics.IsOrderComparableTo(left.ResultType, right.ResultType);
			}
			if (!flag || !flag2)
			{
				throw EntityUtil.Argument(Strings.Cqt_Comparison_ComparableRequired);
			}
			return ArgumentValidation._booleanType;
		}

		// Token: 0x060038A3 RID: 14499 RVA: 0x000D71FF File Offset: 0x000D53FF
		internal static TypeUsage ValidateIsNull(DbExpression argument)
		{
			return ArgumentValidation.ValidateIsNull(argument, false);
		}

		// Token: 0x060038A4 RID: 14500 RVA: 0x000D7208 File Offset: 0x000D5408
		internal static TypeUsage ValidateIsNull(DbExpression argument, bool allowRowType)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(argument, "argument");
			if (TypeSemantics.IsCollectionType(argument.ResultType))
			{
				throw EntityUtil.Argument(Strings.Cqt_IsNull_CollectionNotAllowed);
			}
			if (!TypeHelpers.IsValidIsNullOpType(argument.ResultType) && (!allowRowType || !TypeSemantics.IsRowType(argument.ResultType)))
			{
				throw EntityUtil.Argument(Strings.Cqt_IsNull_InvalidType);
			}
			return ArgumentValidation._booleanType;
		}

		// Token: 0x060038A5 RID: 14501 RVA: 0x000D7266 File Offset: 0x000D5466
		internal static TypeUsage ValidateLike(DbExpression argument, DbExpression pattern)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(argument, "argument");
			EntityUtil.CheckArgumentNull<DbExpression>(pattern, "pattern");
			ArgumentValidation.RequireCompatibleType(argument, PrimitiveTypeKind.String, "argument");
			ArgumentValidation.RequireCompatibleType(pattern, PrimitiveTypeKind.String, "pattern");
			return ArgumentValidation._booleanType;
		}

		// Token: 0x060038A6 RID: 14502 RVA: 0x000D72A0 File Offset: 0x000D54A0
		internal static TypeUsage ValidateLike(DbExpression argument, DbExpression pattern, DbExpression escape)
		{
			TypeUsage result = ArgumentValidation.ValidateLike(argument, pattern);
			EntityUtil.CheckArgumentNull<DbExpression>(escape, "escape");
			ArgumentValidation.RequireCompatibleType(escape, PrimitiveTypeKind.String, "escape");
			return result;
		}

		// Token: 0x060038A7 RID: 14503 RVA: 0x000D72CF File Offset: 0x000D54CF
		internal static void ValidateCastTo(DbExpression argument, TypeUsage toType)
		{
			ArgumentValidation.ValidateTypeUnary(argument, toType, "toType");
			if (!TypeSemantics.IsCastAllowed(argument.ResultType, toType))
			{
				throw EntityUtil.Argument(Strings.Cqt_Cast_InvalidCast(TypeHelpers.GetFullName(argument.ResultType), TypeHelpers.GetFullName(toType)));
			}
		}

		// Token: 0x060038A8 RID: 14504 RVA: 0x000D7308 File Offset: 0x000D5508
		internal static void ValidateTreatAs(DbExpression argument, TypeUsage asType)
		{
			ArgumentValidation.ValidateTypeUnary(argument, asType, "asType");
			ArgumentValidation.RequirePolymorphicType(asType, "asType");
			if (!TypeSemantics.IsValidPolymorphicCast(argument.ResultType, asType))
			{
				throw EntityUtil.Argument(Strings.Cqt_General_PolymorphicArgRequired(typeof(DbTreatExpression).Name));
			}
		}

		// Token: 0x060038A9 RID: 14505 RVA: 0x000D7354 File Offset: 0x000D5554
		internal static TypeUsage ValidateOfType(DbExpression argument, TypeUsage type)
		{
			ArgumentValidation.ValidateTypeUnary(argument, type, "type");
			ArgumentValidation.RequirePolymorphicType(type, "type");
			ArgumentValidation.RequireCollectionArgument<DbOfTypeExpression>(argument);
			TypeUsage fromType = null;
			if (!TypeHelpers.TryGetCollectionElementType(argument.ResultType, out fromType) || !TypeSemantics.IsValidPolymorphicCast(fromType, type))
			{
				throw EntityUtil.Argument(Strings.Cqt_General_PolymorphicArgRequired(typeof(DbOfTypeExpression).Name));
			}
			return ArgumentValidation.CreateCollectionResultType(type);
		}

		// Token: 0x060038AA RID: 14506 RVA: 0x000D73B8 File Offset: 0x000D55B8
		internal static TypeUsage ValidateIsOf(DbExpression argument, TypeUsage type)
		{
			ArgumentValidation.ValidateTypeUnary(argument, type, "type");
			ArgumentValidation.RequirePolymorphicType(type, "type");
			if (!TypeSemantics.IsValidPolymorphicCast(argument.ResultType, type))
			{
				throw EntityUtil.Argument(Strings.Cqt_General_PolymorphicArgRequired(typeof(DbIsOfExpression).Name));
			}
			return ArgumentValidation._booleanType;
		}

		// Token: 0x060038AB RID: 14507 RVA: 0x000D740C File Offset: 0x000D560C
		internal static TypeUsage ValidateDeref(DbExpression argument)
		{
			ArgumentValidation.ValidateUnary(argument);
			EntityType resultType;
			if (!TypeHelpers.TryGetRefEntityType(argument.ResultType, out resultType))
			{
				throw EntityUtil.Argument(Strings.Cqt_DeRef_RefRequired, "argument");
			}
			return ArgumentValidation.CreateResultType(resultType);
		}

		// Token: 0x060038AC RID: 14508 RVA: 0x000D7444 File Offset: 0x000D5644
		internal static TypeUsage ValidateGetEntityRef(DbExpression argument)
		{
			ArgumentValidation.ValidateUnary(argument);
			EntityType entityType = null;
			if (!TypeHelpers.TryGetEdmType<EntityType>(argument.ResultType, out entityType) || entityType == null)
			{
				throw EntityUtil.Argument(Strings.Cqt_GetEntityRef_EntityRequired, "argument");
			}
			return ArgumentValidation.CreateReferenceResultType(entityType);
		}

		// Token: 0x060038AD RID: 14509 RVA: 0x000D7481 File Offset: 0x000D5681
		internal static TypeUsage ValidateCreateRef(EntitySet entitySet, IEnumerable<DbExpression> keyValues, out DbExpression keyConstructor)
		{
			EntityUtil.CheckArgumentNull<EntitySet>(entitySet, "entitySet");
			return ArgumentValidation.ValidateCreateRef(entitySet, entitySet.ElementType, keyValues, out keyConstructor);
		}

		// Token: 0x060038AE RID: 14510 RVA: 0x000D74A0 File Offset: 0x000D56A0
		internal static TypeUsage ValidateCreateRef(EntitySet entitySet, EntityType entityType, IEnumerable<DbExpression> keyValues, out DbExpression keyConstructor)
		{
			ArgumentValidation.CheckEntitySet(entitySet, "entitySet");
			ArgumentValidation.CheckType(entityType, "entityType");
			if (!TypeSemantics.IsValidPolymorphicCast(entitySet.ElementType, entityType))
			{
				throw EntityUtil.Argument(Strings.Cqt_Ref_PolymorphicArgRequired);
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

		// Token: 0x060038AF RID: 14511 RVA: 0x000D754B File Offset: 0x000D574B
		internal static TypeUsage ValidateRefFromKey(EntitySet entitySet, DbExpression keyValues)
		{
			EntityUtil.CheckArgumentNull<EntitySet>(entitySet, "entitySet");
			return ArgumentValidation.ValidateRefFromKey(entitySet, keyValues, entitySet.ElementType);
		}

		// Token: 0x060038B0 RID: 14512 RVA: 0x000D7568 File Offset: 0x000D5768
		internal static TypeUsage ValidateRefFromKey(EntitySet entitySet, DbExpression keyValues, EntityType entityType)
		{
			ArgumentValidation.CheckEntitySet(entitySet, "entitySet");
			EntityUtil.CheckArgumentNull<DbExpression>(keyValues, "keyValues");
			ArgumentValidation.CheckType(entityType);
			if (!TypeSemantics.IsValidPolymorphicCast(entitySet.ElementType, entityType))
			{
				throw EntityUtil.Argument(Strings.Cqt_Ref_PolymorphicArgRequired);
			}
			TypeUsage requiredResultType = ArgumentValidation.CreateResultType(TypeHelpers.CreateKeyRowType(entitySet.ElementType));
			ArgumentValidation.RequireCompatibleType(keyValues, requiredResultType, "keyValues");
			return ArgumentValidation.CreateReferenceResultType(entityType);
		}

		// Token: 0x060038B1 RID: 14513 RVA: 0x000D75D0 File Offset: 0x000D57D0
		internal static TypeUsage ValidateGetRefKey(DbExpression argument)
		{
			ArgumentValidation.ValidateUnary(argument);
			RefType refType = null;
			if (!TypeHelpers.TryGetEdmType<RefType>(argument.ResultType, out refType) || refType == null)
			{
				throw EntityUtil.Argument(Strings.Cqt_GetRefKey_RefRequired, "argument");
			}
			return ArgumentValidation.CreateResultType(TypeHelpers.CreateKeyRowType(refType.ElementType));
		}

		// Token: 0x060038B2 RID: 14514 RVA: 0x000D7618 File Offset: 0x000D5818
		internal static TypeUsage ValidateNavigate(DbExpression navigateFrom, RelationshipType type, string fromEndName, string toEndName, out RelationshipEndMember fromEnd, out RelationshipEndMember toEnd)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(navigateFrom, "navigateFrom");
			ArgumentValidation.CheckType(type);
			EntityUtil.CheckArgumentNull<string>(fromEndName, "fromEndName");
			EntityUtil.CheckArgumentNull<string>(toEndName, "toEndName");
			if (!type.RelationshipEndMembers.TryGetValue(fromEndName, false, out fromEnd))
			{
				throw EntityUtil.ArgumentOutOfRange(Strings.Cqt_Factory_NoSuchRelationEnd, fromEndName);
			}
			if (!type.RelationshipEndMembers.TryGetValue(toEndName, false, out toEnd))
			{
				throw EntityUtil.ArgumentOutOfRange(Strings.Cqt_Factory_NoSuchRelationEnd, toEndName);
			}
			ArgumentValidation.RequireCompatibleType(navigateFrom, fromEnd, false);
			return ArgumentValidation.CreateResultType(toEnd);
		}

		// Token: 0x060038B3 RID: 14515 RVA: 0x000D769C File Offset: 0x000D589C
		internal static TypeUsage ValidateNavigate(DbExpression navigateFrom, RelationshipEndMember fromEnd, RelationshipEndMember toEnd, out RelationshipType relType, bool allowAllRelationshipsInSameTypeHierarchy)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(navigateFrom, "navigateFrom");
			ArgumentValidation.CheckMember(fromEnd, "fromEnd");
			ArgumentValidation.CheckMember(toEnd, "toEnd");
			relType = (fromEnd.DeclaringType as RelationshipType);
			ArgumentValidation.CheckType(relType);
			if (!relType.Equals(toEnd.DeclaringType))
			{
				throw EntityUtil.Argument(Strings.Cqt_Factory_IncompatibleRelationEnds, "toEnd");
			}
			ArgumentValidation.RequireCompatibleType(navigateFrom, fromEnd, allowAllRelationshipsInSameTypeHierarchy);
			return ArgumentValidation.CreateResultType(toEnd);
		}

		// Token: 0x060038B4 RID: 14516 RVA: 0x000D7710 File Offset: 0x000D5910
		internal static TypeUsage ValidateDistinct(DbExpression argument)
		{
			ArgumentValidation.ValidateUnary(argument);
			ArgumentValidation.RequireCollectionArgument<DbDistinctExpression>(argument);
			CollectionType edmType = TypeHelpers.GetEdmType<CollectionType>(argument.ResultType);
			if (!TypeHelpers.IsValidDistinctOpType(edmType.TypeUsage))
			{
				throw EntityUtil.Argument(Strings.Cqt_Distinct_InvalidCollection, "argument");
			}
			return argument.ResultType;
		}

		// Token: 0x060038B5 RID: 14517 RVA: 0x000D7758 File Offset: 0x000D5958
		internal static TypeUsage ValidateElement(DbExpression argument)
		{
			ArgumentValidation.ValidateUnary(argument);
			ArgumentValidation.RequireCollectionArgument<DbElementExpression>(argument);
			return TypeHelpers.GetEdmType<CollectionType>(argument.ResultType).TypeUsage;
		}

		// Token: 0x060038B6 RID: 14518 RVA: 0x000D7776 File Offset: 0x000D5976
		internal static TypeUsage ValidateIsEmpty(DbExpression argument)
		{
			ArgumentValidation.ValidateUnary(argument);
			ArgumentValidation.RequireCollectionArgument<DbIsEmptyExpression>(argument);
			return ArgumentValidation._booleanType;
		}

		// Token: 0x060038B7 RID: 14519 RVA: 0x000D7789 File Offset: 0x000D5989
		internal static TypeUsage ValidateExcept(DbExpression left, DbExpression right)
		{
			ArgumentValidation.ValidateBinary(left, right);
			ArgumentValidation.RequireComparableCollectionArguments<DbExceptExpression>(left, right);
			return left.ResultType;
		}

		// Token: 0x060038B8 RID: 14520 RVA: 0x000D77A1 File Offset: 0x000D59A1
		internal static TypeUsage ValidateIntersect(DbExpression left, DbExpression right)
		{
			ArgumentValidation.ValidateBinary(left, right);
			return ArgumentValidation.RequireComparableCollectionArguments<DbIntersectExpression>(left, right);
		}

		// Token: 0x060038B9 RID: 14521 RVA: 0x000D77B2 File Offset: 0x000D59B2
		internal static TypeUsage ValidateUnionAll(DbExpression left, DbExpression right)
		{
			ArgumentValidation.ValidateBinary(left, right);
			return ArgumentValidation.RequireCollectionArguments<DbUnionAllExpression>(left, right);
		}

		// Token: 0x060038BA RID: 14522 RVA: 0x000D77C4 File Offset: 0x000D59C4
		internal static TypeUsage ValidateLimit(DbExpression argument, DbExpression limit)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(argument, "argument");
			ArgumentValidation.RequireCollectionArgument<DbLimitExpression>(argument);
			EntityUtil.CheckArgumentNull<DbExpression>(limit, "count");
			if (!TypeSemantics.IsIntegerNumericType(limit.ResultType))
			{
				throw EntityUtil.Argument(Strings.Cqt_Limit_IntegerRequired, "limit");
			}
			if (limit.ExpressionKind != DbExpressionKind.Constant && limit.ExpressionKind != DbExpressionKind.ParameterReference)
			{
				throw EntityUtil.Argument(Strings.Cqt_Limit_ConstantOrParameterRefRequired, "limit");
			}
			if (ArgumentValidation.IsConstantNegativeInteger(limit))
			{
				throw EntityUtil.Argument(Strings.Cqt_Limit_NonNegativeLimitRequired, "limit");
			}
			return argument.ResultType;
		}

		// Token: 0x060038BB RID: 14523 RVA: 0x000D7850 File Offset: 0x000D5A50
		internal static TypeUsage ValidateCase(IEnumerable<DbExpression> whenExpressions, IEnumerable<DbExpression> thenExpressions, DbExpression elseExpression, out DbExpressionList validWhens, out DbExpressionList validThens)
		{
			EntityUtil.CheckArgumentNull<IEnumerable<DbExpression>>(whenExpressions, "whenExpressions");
			EntityUtil.CheckArgumentNull<IEnumerable<DbExpression>>(thenExpressions, "thenExpressions");
			EntityUtil.CheckArgumentNull<DbExpression>(elseExpression, "elseExpression");
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
					throw EntityUtil.Argument(Strings.Cqt_Case_InvalidResultType);
				}
			});
			commonResultType = TypeHelpers.GetCommonTypeUsage(elseExpression.ResultType, commonResultType);
			if (commonResultType == null)
			{
				throw EntityUtil.Argument(Strings.Cqt_Case_InvalidResultType);
			}
			if (validWhens.Count != validThens.Count)
			{
				throw EntityUtil.Argument(Strings.Cqt_Case_WhensMustEqualThens);
			}
			return commonResultType;
		}

		// Token: 0x060038BC RID: 14524 RVA: 0x000D7920 File Offset: 0x000D5B20
		internal static TypeUsage ValidateFunction(EdmFunction function, IEnumerable<DbExpression> arguments, out DbExpressionList validArgs)
		{
			ArgumentValidation.CheckFunction(function);
			if (!function.IsComposableAttribute)
			{
				throw EntityUtil.Argument(Strings.Cqt_Function_NonComposableInExpression, "function");
			}
			if (!string.IsNullOrEmpty(function.CommandTextAttribute) && !function.HasUserDefinedBody)
			{
				throw EntityUtil.Argument(Strings.Cqt_Function_CommandTextInExpression, "function");
			}
			if (function.ReturnParameter == null)
			{
				throw EntityUtil.Argument(Strings.Cqt_Function_VoidResultInvalid, "function");
			}
			FunctionParameter[] expectedParams = ArgumentValidation.GetExpectedParameters(function);
			validArgs = ArgumentValidation.CreateExpressionList(arguments, "arguments", expectedParams.Length, delegate(DbExpression exp, int idx)
			{
				ArgumentValidation.RequireCompatibleType(exp, expectedParams[idx].TypeUsage, "arguments", idx);
			});
			return function.ReturnParameter.TypeUsage;
		}

		// Token: 0x060038BD RID: 14525 RVA: 0x000D79C8 File Offset: 0x000D5BC8
		internal static TypeUsage ValidateInvoke(DbLambda lambda, IEnumerable<DbExpression> arguments, out DbExpressionList validArguments)
		{
			EntityUtil.CheckArgumentNull<DbLambda>(lambda, "lambda");
			EntityUtil.CheckArgumentNull<IEnumerable<DbExpression>>(arguments, "arguments");
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

		// Token: 0x060038BE RID: 14526 RVA: 0x000D7A68 File Offset: 0x000D5C68
		internal static TypeUsage ValidateNewCollection(IEnumerable<DbExpression> elements, out DbExpressionList validElements)
		{
			TypeUsage commonElementType = null;
			validElements = ArgumentValidation.CreateExpressionList(elements, "elements", delegate(DbExpression exp, int idx)
			{
				if (commonElementType == null)
				{
					commonElementType = exp.ResultType;
				}
				else
				{
					commonElementType = TypeSemantics.GetCommonType(commonElementType, exp.ResultType);
				}
				if (commonElementType == null)
				{
					throw EntityUtil.Argument(Strings.Cqt_Factory_NewCollectionInvalidCommonType, "collectionElements");
				}
			});
			return ArgumentValidation.CreateCollectionResultType(commonElementType);
		}

		// Token: 0x060038BF RID: 14527 RVA: 0x000D7AA6 File Offset: 0x000D5CA6
		internal static TypeUsage ValidateNewEmptyCollection(TypeUsage collectionType, out DbExpressionList validElements)
		{
			ArgumentValidation.CheckType(collectionType, "collectionType");
			if (!TypeSemantics.IsCollectionType(collectionType))
			{
				throw EntityUtil.Argument(Strings.Cqt_NewInstance_CollectionTypeRequired, "collectionType");
			}
			validElements = new DbExpressionList(new DbExpression[0]);
			return collectionType;
		}

		// Token: 0x060038C0 RID: 14528 RVA: 0x000D7ADC File Offset: 0x000D5CDC
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

		// Token: 0x060038C1 RID: 14529 RVA: 0x000D7B70 File Offset: 0x000D5D70
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
					List<TypeUsage> expectedTypes = expectedTypes;
					int pos = pos;
					pos++;
					ArgumentValidation.RequireCompatibleType(exp, expectedTypes[pos], "arguments", idx);
				});
			}
			return instanceType;
		}

		// Token: 0x060038C2 RID: 14530 RVA: 0x000D7C04 File Offset: 0x000D5E04
		private static List<TypeUsage> GetStructuralMemberTypes(TypeUsage instanceType)
		{
			StructuralType structuralType = instanceType.EdmType as StructuralType;
			if (structuralType == null)
			{
				throw EntityUtil.Argument(Strings.Cqt_NewInstance_StructuralTypeRequired, "instanceType");
			}
			if (structuralType.Abstract)
			{
				throw EntityUtil.Argument(Strings.Cqt_NewInstance_CannotInstantiateAbstractType(TypeHelpers.GetFullName(instanceType)), "instanceType");
			}
			IBaseList<EdmMember> allStructuralMembers = TypeHelpers.GetAllStructuralMembers(structuralType);
			if (allStructuralMembers == null || allStructuralMembers.Count < 1)
			{
				throw EntityUtil.Argument(Strings.Cqt_NewInstance_CannotInstantiateMemberlessType(TypeHelpers.GetFullName(instanceType)), "instanceType");
			}
			List<TypeUsage> list = new List<TypeUsage>(allStructuralMembers.Count);
			for (int i = 0; i < allStructuralMembers.Count; i++)
			{
				list.Add(Helper.GetModelTypeUsage(allStructuralMembers[i]));
			}
			return list;
		}

		// Token: 0x060038C3 RID: 14531 RVA: 0x000D7CA8 File Offset: 0x000D5EA8
		internal static TypeUsage ValidateNewEntityWithRelationships(EntityType entityType, IEnumerable<DbExpression> attributeValues, IList<DbRelatedEntityRef> relationships, out DbExpressionList validArguments, out ReadOnlyCollection<DbRelatedEntityRef> validRelatedRefs)
		{
			EntityUtil.CheckArgumentNull<EntityType>(entityType, "entityType");
			EntityUtil.CheckArgumentNull<IEnumerable<DbExpression>>(attributeValues, "attributeValues");
			EntityUtil.CheckArgumentNull<IList<DbRelatedEntityRef>>(relationships, "relationships");
			TypeUsage typeUsage = ArgumentValidation.CreateResultType(entityType);
			typeUsage = ArgumentValidation.ValidateNew(typeUsage, attributeValues, out validArguments);
			if (relationships.Count > 0)
			{
				List<DbRelatedEntityRef> list = new List<DbRelatedEntityRef>(relationships.Count);
				for (int i = 0; i < relationships.Count; i++)
				{
					DbRelatedEntityRef dbRelatedEntityRef = relationships[i];
					EntityUtil.CheckArgumentNull<DbRelatedEntityRef>(dbRelatedEntityRef, StringUtil.FormatIndex("relationships", i));
					EntityTypeBase elementType = TypeHelpers.GetEdmType<RefType>(dbRelatedEntityRef.SourceEnd.TypeUsage).ElementType;
					if (!entityType.EdmEquals(elementType) && !entityType.IsSubtypeOf(elementType))
					{
						throw EntityUtil.Argument(Strings.Cqt_NewInstance_IncompatibleRelatedEntity_SourceTypeNotValid, StringUtil.FormatIndex("relationships", i));
					}
					list.Add(dbRelatedEntityRef);
				}
				validRelatedRefs = list.AsReadOnly();
			}
			else
			{
				validRelatedRefs = new ReadOnlyCollection<DbRelatedEntityRef>(new DbRelatedEntityRef[0]);
			}
			return typeUsage;
		}

		// Token: 0x060038C4 RID: 14532 RVA: 0x000D7D90 File Offset: 0x000D5F90
		internal static TypeUsage ValidateProperty(DbExpression instance, EdmMember property, string propertyArgumentName)
		{
			ArgumentValidation.CheckMember(property, propertyArgumentName);
			if (instance == null)
			{
				throw EntityUtil.Argument(Strings.Cqt_Property_InstanceRequiredForInstance, "instance");
			}
			TypeUsage requiredResultType = TypeUsage.Create(property.DeclaringType);
			ArgumentValidation.RequireCompatibleType(instance, requiredResultType, "instance");
			return Helper.GetModelTypeUsage(property);
		}

		// Token: 0x060038C5 RID: 14533 RVA: 0x000D7DD8 File Offset: 0x000D5FD8
		internal static TypeUsage ValidateProperty(DbExpression instance, string propertyName, bool ignoreCase, out EdmMember foundMember)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(instance, "instance");
			EntityUtil.CheckArgumentNull<string>(propertyName, "propertyName");
			StructuralType structuralType;
			if (TypeHelpers.TryGetEdmType<StructuralType>(instance.ResultType, out structuralType) && structuralType.Members.TryGetValue(propertyName, ignoreCase, out foundMember) && foundMember != null && (Helper.IsRelationshipEndMember(foundMember) || Helper.IsEdmProperty(foundMember) || Helper.IsNavigationProperty(foundMember)))
			{
				return Helper.GetModelTypeUsage(foundMember);
			}
			throw EntityUtil.ArgumentOutOfRange(Strings.Cqt_Factory_NoSuchProperty(propertyName, TypeHelpers.GetFullName(instance.ResultType)), "propertyName");
		}

		// Token: 0x060038C6 RID: 14534 RVA: 0x000D7E60 File Offset: 0x000D6060
		private static void CheckNamed<T>(KeyValuePair<string, T> element, string argumentName, int index)
		{
			if (string.IsNullOrEmpty(element.Key))
			{
				if (index != -1)
				{
					argumentName = StringUtil.FormatIndex(argumentName, index);
				}
				throw EntityUtil.ArgumentNull(string.Format(CultureInfo.InvariantCulture, "{0}.Key", new object[]
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
				throw EntityUtil.ArgumentNull(string.Format(CultureInfo.InvariantCulture, "{0}.Value", new object[]
				{
					argumentName
				}));
			}
		}

		// Token: 0x060038C7 RID: 14535 RVA: 0x000D7EE1 File Offset: 0x000D60E1
		private static void CheckReadOnly(GlobalItem item, string varName)
		{
			EntityUtil.CheckArgumentNull<GlobalItem>(item, varName);
			if (!item.IsReadOnly)
			{
				throw EntityUtil.Argument(Strings.Cqt_General_MetadataNotReadOnly, varName);
			}
		}

		// Token: 0x060038C8 RID: 14536 RVA: 0x000D7EFF File Offset: 0x000D60FF
		private static void CheckReadOnly(TypeUsage item, string varName)
		{
			EntityUtil.CheckArgumentNull<TypeUsage>(item, varName);
			if (!item.IsReadOnly)
			{
				throw EntityUtil.Argument(Strings.Cqt_General_MetadataNotReadOnly, varName);
			}
		}

		// Token: 0x060038C9 RID: 14537 RVA: 0x000D7F1D File Offset: 0x000D611D
		private static void CheckReadOnly(EntitySetBase item, string varName)
		{
			EntityUtil.CheckArgumentNull<EntitySetBase>(item, varName);
			if (!item.IsReadOnly)
			{
				throw EntityUtil.Argument(Strings.Cqt_General_MetadataNotReadOnly, varName);
			}
		}

		// Token: 0x060038CA RID: 14538 RVA: 0x000D7F3B File Offset: 0x000D613B
		private static void CheckType(EdmType type)
		{
			ArgumentValidation.CheckType(type, "type");
		}

		// Token: 0x060038CB RID: 14539 RVA: 0x000D7F48 File Offset: 0x000D6148
		private static void CheckType(EdmType type, string argumentName)
		{
			EntityUtil.CheckArgumentNull<EdmType>(type, argumentName);
			ArgumentValidation.CheckReadOnly(type, argumentName);
		}

		// Token: 0x060038CC RID: 14540 RVA: 0x000D7F59 File Offset: 0x000D6159
		private static void CheckType(TypeUsage type)
		{
			ArgumentValidation.CheckType(type, "type");
		}

		// Token: 0x060038CD RID: 14541 RVA: 0x000D7F66 File Offset: 0x000D6166
		private static void CheckType(TypeUsage type, string varName)
		{
			EntityUtil.CheckArgumentNull<TypeUsage>(type, varName);
			ArgumentValidation.CheckReadOnly(type, varName);
			if (!ArgumentValidation.CheckDataSpace(type))
			{
				throw EntityUtil.Argument(Strings.Cqt_Metadata_TypeUsageIncorrectSpace, "type");
			}
		}

		// Token: 0x060038CE RID: 14542 RVA: 0x000D7F8F File Offset: 0x000D618F
		private static void CheckMember(EdmMember memberMeta, string varName)
		{
			EntityUtil.CheckArgumentNull<EdmMember>(memberMeta, varName);
			ArgumentValidation.CheckReadOnly(memberMeta.DeclaringType, varName);
			if (!ArgumentValidation.CheckDataSpace(memberMeta.TypeUsage) || !ArgumentValidation.CheckDataSpace(memberMeta.DeclaringType))
			{
				throw EntityUtil.Argument(Strings.Cqt_Metadata_EdmMemberIncorrectSpace, varName);
			}
		}

		// Token: 0x060038CF RID: 14543 RVA: 0x000D7FCB File Offset: 0x000D61CB
		private static void CheckParameter(FunctionParameter paramMeta, string varName)
		{
			EntityUtil.CheckArgumentNull<FunctionParameter>(paramMeta, varName);
			ArgumentValidation.CheckReadOnly(paramMeta.DeclaringFunction, varName);
			if (!ArgumentValidation.CheckDataSpace(paramMeta.TypeUsage))
			{
				throw EntityUtil.Argument(Strings.Cqt_Metadata_FunctionParameterIncorrectSpace, varName);
			}
		}

		// Token: 0x060038D0 RID: 14544 RVA: 0x000D7FFC File Offset: 0x000D61FC
		private static void CheckFunction(EdmFunction function)
		{
			EntityUtil.CheckArgumentNull<EdmFunction>(function, "function");
			ArgumentValidation.CheckReadOnly(function, "function");
			if (!ArgumentValidation.CheckDataSpace(function))
			{
				throw EntityUtil.Argument(Strings.Cqt_Metadata_FunctionIncorrectSpace, "function");
			}
			if (function.IsComposableAttribute && function.ReturnParameter == null)
			{
				throw EntityUtil.Argument(Strings.Cqt_Metadata_FunctionReturnParameterNull, "function");
			}
			if (function.ReturnParameter != null && !ArgumentValidation.CheckDataSpace(function.ReturnParameter.TypeUsage))
			{
				throw EntityUtil.Argument(Strings.Cqt_Metadata_FunctionParameterIncorrectSpace, "function.ReturnParameter");
			}
			IList<FunctionParameter> parameters = function.Parameters;
			for (int i = 0; i < parameters.Count; i++)
			{
				ArgumentValidation.CheckParameter(parameters[i], StringUtil.FormatIndex("function.Parameters", i));
			}
		}

		// Token: 0x060038D1 RID: 14545 RVA: 0x000D80B4 File Offset: 0x000D62B4
		private static void CheckEntitySet(EntitySetBase entitySet, string varName)
		{
			EntityUtil.CheckArgumentNull<EntitySetBase>(entitySet, varName);
			ArgumentValidation.CheckReadOnly(entitySet, varName);
			if (entitySet.EntityContainer == null)
			{
				throw EntityUtil.Argument(Strings.Cqt_Metadata_EntitySetEntityContainerNull, varName);
			}
			if (!ArgumentValidation.CheckDataSpace(entitySet.EntityContainer))
			{
				throw EntityUtil.Argument(Strings.Cqt_Metadata_EntitySetIncorrectSpace, varName);
			}
			if (!ArgumentValidation.CheckDataSpace(entitySet.ElementType))
			{
				throw EntityUtil.Argument(Strings.Cqt_Metadata_EntitySetIncorrectSpace, varName);
			}
		}

		// Token: 0x060038D2 RID: 14546 RVA: 0x000D8116 File Offset: 0x000D6316
		private static bool CheckDataSpace(TypeUsage type)
		{
			return ArgumentValidation.CheckDataSpace(type.EdmType);
		}

		// Token: 0x060038D3 RID: 14547 RVA: 0x000D8124 File Offset: 0x000D6324
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

		// Token: 0x060038D4 RID: 14548 RVA: 0x000D81F8 File Offset: 0x000D63F8
		private static TypeUsage CreateCollectionOfRowResultType(List<KeyValuePair<string, TypeUsage>> columns)
		{
			return TypeUsage.Create(TypeHelpers.CreateCollectionType(TypeUsage.Create(TypeHelpers.CreateRowType(columns))));
		}

		// Token: 0x060038D5 RID: 14549 RVA: 0x000D821C File Offset: 0x000D641C
		private static TypeUsage CreateCollectionResultType(EdmType type)
		{
			return TypeUsage.Create(TypeHelpers.CreateCollectionType(TypeUsage.Create(type)));
		}

		// Token: 0x060038D6 RID: 14550 RVA: 0x000D823C File Offset: 0x000D643C
		private static TypeUsage CreateCollectionResultType(TypeUsage type)
		{
			return TypeUsage.Create(TypeHelpers.CreateCollectionType(type));
		}

		// Token: 0x060038D7 RID: 14551 RVA: 0x000D8256 File Offset: 0x000D6456
		private static TypeUsage CreateResultType(EdmType resultType)
		{
			return TypeUsage.Create(resultType);
		}

		// Token: 0x060038D8 RID: 14552 RVA: 0x000D8260 File Offset: 0x000D6460
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

		// Token: 0x060038D9 RID: 14553 RVA: 0x000B4E22 File Offset: 0x000B3022
		private static TypeUsage CreateReferenceResultType(EntityTypeBase referencedEntityType)
		{
			return TypeUsage.Create(TypeHelpers.CreateReferenceType(referencedEntityType));
		}

		// Token: 0x060038DA RID: 14554 RVA: 0x000D8299 File Offset: 0x000D6499
		private static bool IsConstantNegativeInteger(DbExpression expression)
		{
			return expression.ExpressionKind == DbExpressionKind.Constant && TypeSemantics.IsIntegerNumericType(expression.ResultType) && Convert.ToInt64(((DbConstantExpression)expression).Value, CultureInfo.InvariantCulture) < 0L;
		}

		// Token: 0x060038DB RID: 14555 RVA: 0x000D82CC File Offset: 0x000D64CC
		private static bool TryGetPrimitiveTypeKind(Type clrType, out PrimitiveTypeKind primitiveTypeKind)
		{
			return ClrProviderManifest.Instance.TryGetPrimitiveTypeKind(clrType, out primitiveTypeKind);
		}

		// Token: 0x060038DC RID: 14556 RVA: 0x000D82DC File Offset: 0x000D64DC
		private static bool ClrEdmEnumTypesMatch(EnumType edmEnumType, Type clrEnumType)
		{
			if (clrEnumType.Name != edmEnumType.Name || clrEnumType.GetEnumNames().Length != edmEnumType.Members.Count)
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

		// Token: 0x04001851 RID: 6225
		private static TypeUsage _booleanType = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Boolean);
	}
}
