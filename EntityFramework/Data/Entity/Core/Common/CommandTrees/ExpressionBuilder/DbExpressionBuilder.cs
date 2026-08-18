using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder.Internal;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder
{
	// Token: 0x0200011C RID: 284
	[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Db")]
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	public static class DbExpressionBuilder
	{
		// Token: 0x06000779 RID: 1913 RVA: 0x00028498 File Offset: 0x00026698
		public static KeyValuePair<string, DbExpression> As(this DbExpression value, string alias)
		{
			return new KeyValuePair<string, DbExpression>(alias, value);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x000284A1 File Offset: 0x000266A1
		public static KeyValuePair<string, DbAggregate> As(this DbAggregate value, string alias)
		{
			return new KeyValuePair<string, DbAggregate>(alias, value);
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x000284AA File Offset: 0x000266AA
		public static DbExpressionBinding Bind(this DbExpression input)
		{
			Check.NotNull<DbExpression>(input, "input");
			return input.BindAs(DbExpressionBuilder._bindingAliases.Next());
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x000284C8 File Offset: 0x000266C8
		public static DbExpressionBinding BindAs(this DbExpression input, string varName)
		{
			Check.NotNull<DbExpression>(input, "input");
			Check.NotNull<string>(varName, "varName");
			Check.NotEmpty(varName, "varName");
			TypeUsage type = null;
			if (!TypeHelpers.TryGetCollectionElementType(input.ResultType, out type))
			{
				throw new ArgumentException(Strings.Cqt_Binding_CollectionRequired, "input");
			}
			DbVariableReferenceExpression varRef = new DbVariableReferenceExpression(type, varName);
			return new DbExpressionBinding(input, varRef);
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0002852C File Offset: 0x0002672C
		public static DbGroupExpressionBinding GroupBind(this DbExpression input)
		{
			Check.NotNull<DbExpression>(input, "input");
			string text = DbExpressionBuilder._bindingAliases.Next();
			return input.GroupBindAs(text, string.Format(CultureInfo.InvariantCulture, "Group{0}", new object[]
			{
				text
			}));
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00028574 File Offset: 0x00026774
		public static DbGroupExpressionBinding GroupBindAs(this DbExpression input, string varName, string groupVarName)
		{
			Check.NotNull<DbExpression>(input, "input");
			Check.NotNull<string>(varName, "varName");
			Check.NotEmpty(varName, "varName");
			Check.NotNull<string>(groupVarName, "groupVarName");
			Check.NotEmpty(groupVarName, "groupVarName");
			TypeUsage type = null;
			if (!TypeHelpers.TryGetCollectionElementType(input.ResultType, out type))
			{
				throw new ArgumentException(Strings.Cqt_GroupBinding_CollectionRequired, "input");
			}
			DbVariableReferenceExpression inputRef = new DbVariableReferenceExpression(type, varName);
			DbVariableReferenceExpression groupRef = new DbVariableReferenceExpression(type, groupVarName);
			return new DbGroupExpressionBinding(input, inputRef, groupRef);
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x000285F6 File Offset: 0x000267F6
		public static DbFunctionAggregate Aggregate(this EdmFunction function, DbExpression argument)
		{
			Check.NotNull<EdmFunction>(function, "function");
			Check.NotNull<DbExpression>(argument, "argument");
			return DbExpressionBuilder.CreateFunctionAggregate(function, argument, false);
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00028618 File Offset: 0x00026818
		public static DbFunctionAggregate AggregateDistinct(this EdmFunction function, DbExpression argument)
		{
			Check.NotNull<EdmFunction>(function, "function");
			Check.NotNull<DbExpression>(argument, "argument");
			return DbExpressionBuilder.CreateFunctionAggregate(function, argument, true);
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0002863C File Offset: 0x0002683C
		private static DbFunctionAggregate CreateFunctionAggregate(EdmFunction function, DbExpression argument, bool isDistinct)
		{
			DbExpressionList arguments = ArgumentValidation.ValidateFunctionAggregate(function, new DbExpression[]
			{
				argument
			});
			TypeUsage typeUsage = function.ReturnParameter.TypeUsage;
			return new DbFunctionAggregate(typeUsage, arguments, function, isDistinct);
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00028674 File Offset: 0x00026874
		public static DbGroupAggregate GroupAggregate(DbExpression argument)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			DbExpressionList arguments = new DbExpressionList(new DbExpression[]
			{
				argument
			});
			TypeUsage resultType = TypeHelpers.CreateCollectionTypeUsage(argument.ResultType);
			return new DbGroupAggregate(resultType, arguments);
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x000286B2 File Offset: 0x000268B2
		public static DbLambda Lambda(DbExpression body, IEnumerable<DbVariableReferenceExpression> variables)
		{
			Check.NotNull<DbExpression>(body, "body");
			Check.NotNull<IEnumerable<DbVariableReferenceExpression>>(variables, "variables");
			return DbExpressionBuilder.CreateLambda(body, variables);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x000286D3 File Offset: 0x000268D3
		public static DbLambda Lambda(DbExpression body, params DbVariableReferenceExpression[] variables)
		{
			Check.NotNull<DbExpression>(body, "body");
			Check.NotNull<DbVariableReferenceExpression[]>(variables, "variables");
			return DbExpressionBuilder.CreateLambda(body, variables);
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x000286F4 File Offset: 0x000268F4
		private static DbLambda CreateLambda(DbExpression body, IEnumerable<DbVariableReferenceExpression> variables)
		{
			ReadOnlyCollection<DbVariableReferenceExpression> variables2 = ArgumentValidation.ValidateLambda(variables);
			return new DbLambda(variables2, body);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0002870F File Offset: 0x0002690F
		public static DbSortClause ToSortClause(this DbExpression key)
		{
			Check.NotNull<DbExpression>(key, "key");
			ArgumentValidation.ValidateSortClause(key);
			return new DbSortClause(key, true, string.Empty);
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0002872F File Offset: 0x0002692F
		public static DbSortClause ToSortClauseDescending(this DbExpression key)
		{
			Check.NotNull<DbExpression>(key, "key");
			ArgumentValidation.ValidateSortClause(key);
			return new DbSortClause(key, false, string.Empty);
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0002874F File Offset: 0x0002694F
		public static DbSortClause ToSortClause(this DbExpression key, string collation)
		{
			Check.NotNull<DbExpression>(key, "key");
			Check.NotNull<string>(collation, "collation");
			ArgumentValidation.ValidateSortClause(key, collation);
			return new DbSortClause(key, true, collation);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00028778 File Offset: 0x00026978
		public static DbSortClause ToSortClauseDescending(this DbExpression key, string collation)
		{
			Check.NotNull<DbExpression>(key, "key");
			Check.NotNull<string>(collation, "collation");
			ArgumentValidation.ValidateSortClause(key, collation);
			return new DbSortClause(key, false, collation);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x000287A4 File Offset: 0x000269A4
		public static DbQuantifierExpression All(this DbExpressionBinding input, DbExpression predicate)
		{
			Check.NotNull<DbExpression>(predicate, "predicate");
			Check.NotNull<DbExpressionBinding>(input, "input");
			TypeUsage booleanResultType = ArgumentValidation.ValidateQuantifier(predicate);
			return new DbQuantifierExpression(DbExpressionKind.All, booleanResultType, input, predicate);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x000287DC File Offset: 0x000269DC
		public static DbQuantifierExpression Any(this DbExpressionBinding input, DbExpression predicate)
		{
			Check.NotNull<DbExpression>(predicate, "predicate");
			Check.NotNull<DbExpressionBinding>(input, "input");
			TypeUsage booleanResultType = ArgumentValidation.ValidateQuantifier(predicate);
			return new DbQuantifierExpression(DbExpressionKind.Any, booleanResultType, input, predicate);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00028814 File Offset: 0x00026A14
		public static DbApplyExpression CrossApply(this DbExpressionBinding input, DbExpressionBinding apply)
		{
			Check.NotNull<DbExpressionBinding>(input, "input");
			Check.NotNull<DbExpressionBinding>(apply, "apply");
			DbExpressionBuilder.ValidateApply(input, apply);
			TypeUsage resultRowCollectionTypeUsage = DbExpressionBuilder.CreateApplyResultType(input, apply);
			return new DbApplyExpression(DbExpressionKind.CrossApply, resultRowCollectionTypeUsage, input, apply);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x00028854 File Offset: 0x00026A54
		public static DbApplyExpression OuterApply(this DbExpressionBinding input, DbExpressionBinding apply)
		{
			Check.NotNull<DbExpressionBinding>(input, "input");
			Check.NotNull<DbExpressionBinding>(apply, "apply");
			DbExpressionBuilder.ValidateApply(input, apply);
			TypeUsage resultRowCollectionTypeUsage = DbExpressionBuilder.CreateApplyResultType(input, apply);
			return new DbApplyExpression(DbExpressionKind.OuterApply, resultRowCollectionTypeUsage, input, apply);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00028892 File Offset: 0x00026A92
		private static void ValidateApply(DbExpressionBinding input, DbExpressionBinding apply)
		{
			if (input.VariableName.Equals(apply.VariableName, StringComparison.Ordinal))
			{
				throw new ArgumentException(Strings.Cqt_Apply_DuplicateVariableNames);
			}
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x000288B4 File Offset: 0x00026AB4
		private static TypeUsage CreateApplyResultType(DbExpressionBinding input, DbExpressionBinding apply)
		{
			return ArgumentValidation.CreateCollectionOfRowResultType(new List<KeyValuePair<string, TypeUsage>>
			{
				new KeyValuePair<string, TypeUsage>(input.VariableName, input.VariableType),
				new KeyValuePair<string, TypeUsage>(apply.VariableName, apply.VariableType)
			});
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x000288FC File Offset: 0x00026AFC
		public static DbCrossJoinExpression CrossJoin(IEnumerable<DbExpressionBinding> inputs)
		{
			Check.NotNull<IEnumerable<DbExpressionBinding>>(inputs, "inputs");
			TypeUsage collectionOfRowResultType;
			ReadOnlyCollection<DbExpressionBinding> inputs2 = ArgumentValidation.ValidateCrossJoin(inputs, out collectionOfRowResultType);
			return new DbCrossJoinExpression(collectionOfRowResultType, inputs2);
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00028928 File Offset: 0x00026B28
		public static DbJoinExpression InnerJoin(this DbExpressionBinding left, DbExpressionBinding right, DbExpression joinCondition)
		{
			Check.NotNull<DbExpressionBinding>(left, "left");
			Check.NotNull<DbExpressionBinding>(right, "right");
			Check.NotNull<DbExpression>(joinCondition, "joinCondition");
			TypeUsage collectionOfRowResultType = ArgumentValidation.ValidateJoin(left, right, joinCondition);
			return new DbJoinExpression(DbExpressionKind.InnerJoin, collectionOfRowResultType, left, right, joinCondition);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00028970 File Offset: 0x00026B70
		public static DbJoinExpression LeftOuterJoin(this DbExpressionBinding left, DbExpressionBinding right, DbExpression joinCondition)
		{
			Check.NotNull<DbExpressionBinding>(left, "left");
			Check.NotNull<DbExpressionBinding>(right, "right");
			Check.NotNull<DbExpression>(joinCondition, "joinCondition");
			TypeUsage collectionOfRowResultType = ArgumentValidation.ValidateJoin(left, right, joinCondition);
			return new DbJoinExpression(DbExpressionKind.LeftOuterJoin, collectionOfRowResultType, left, right, joinCondition);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x000289B8 File Offset: 0x00026BB8
		public static DbJoinExpression FullOuterJoin(this DbExpressionBinding left, DbExpressionBinding right, DbExpression joinCondition)
		{
			Check.NotNull<DbExpressionBinding>(left, "left");
			Check.NotNull<DbExpressionBinding>(right, "right");
			Check.NotNull<DbExpression>(joinCondition, "joinCondition");
			TypeUsage collectionOfRowResultType = ArgumentValidation.ValidateJoin(left, right, joinCondition);
			return new DbJoinExpression(DbExpressionKind.FullOuterJoin, collectionOfRowResultType, left, right, joinCondition);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00028A00 File Offset: 0x00026C00
		public static DbFilterExpression Filter(this DbExpressionBinding input, DbExpression predicate)
		{
			Check.NotNull<DbExpressionBinding>(input, "input");
			Check.NotNull<DbExpression>(predicate, "predicate");
			TypeUsage resultType = ArgumentValidation.ValidateFilter(input, predicate);
			return new DbFilterExpression(resultType, input, predicate);
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00028A38 File Offset: 0x00026C38
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static DbGroupByExpression GroupBy(this DbGroupExpressionBinding input, IEnumerable<KeyValuePair<string, DbExpression>> keys, IEnumerable<KeyValuePair<string, DbAggregate>> aggregates)
		{
			Check.NotNull<DbGroupExpressionBinding>(input, "input");
			Check.NotNull<IEnumerable<KeyValuePair<string, DbExpression>>>(keys, "keys");
			Check.NotNull<IEnumerable<KeyValuePair<string, DbAggregate>>>(aggregates, "aggregates");
			DbExpressionList groupKeys;
			ReadOnlyCollection<DbAggregate> aggregates2;
			TypeUsage collectionOfRowResultType = ArgumentValidation.ValidateGroupBy(keys, aggregates, out groupKeys, out aggregates2);
			return new DbGroupByExpression(collectionOfRowResultType, input, groupKeys, aggregates2);
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00028A80 File Offset: 0x00026C80
		public static DbProjectExpression Project(this DbExpressionBinding input, DbExpression projection)
		{
			Check.NotNull<DbExpression>(projection, "projection");
			Check.NotNull<DbExpressionBinding>(input, "input");
			TypeUsage resultType = DbExpressionBuilder.CreateCollectionResultType(projection.ResultType);
			return new DbProjectExpression(resultType, input, projection);
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00028ABC File Offset: 0x00026CBC
		public static DbSkipExpression Skip(this DbExpressionBinding input, IEnumerable<DbSortClause> sortOrder, DbExpression count)
		{
			Check.NotNull<DbExpressionBinding>(input, "input");
			Check.NotNull<IEnumerable<DbSortClause>>(sortOrder, "sortOrder");
			Check.NotNull<DbExpression>(count, "count");
			ReadOnlyCollection<DbSortClause> sortOrder2 = ArgumentValidation.ValidateSortArguments(sortOrder);
			if (!TypeSemantics.IsIntegerNumericType(count.ResultType))
			{
				throw new ArgumentException(Strings.Cqt_Skip_IntegerRequired, "count");
			}
			if (count.ExpressionKind != DbExpressionKind.Constant && count.ExpressionKind != DbExpressionKind.ParameterReference)
			{
				throw new ArgumentException(Strings.Cqt_Skip_ConstantOrParameterRefRequired, "count");
			}
			if (DbExpressionBuilder.IsConstantNegativeInteger(count))
			{
				throw new ArgumentException(Strings.Cqt_Skip_NonNegativeCountRequired, "count");
			}
			return new DbSkipExpression(input.Expression.ResultType, input, sortOrder2, count);
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00028B60 File Offset: 0x00026D60
		public static DbSortExpression Sort(this DbExpressionBinding input, IEnumerable<DbSortClause> sortOrder)
		{
			Check.NotNull<DbExpressionBinding>(input, "input");
			ReadOnlyCollection<DbSortClause> sortOrder2 = ArgumentValidation.ValidateSort(sortOrder);
			return new DbSortExpression(input.Expression.ResultType, input, sortOrder2);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00028B92 File Offset: 0x00026D92
		public static DbNullExpression Null(this TypeUsage nullType)
		{
			Check.NotNull<TypeUsage>(nullType, "nullType");
			ArgumentValidation.CheckType(nullType, "nullType");
			return new DbNullExpression(nullType);
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x00028BB1 File Offset: 0x00026DB1
		public static DbConstantExpression True
		{
			get
			{
				return DbExpressionBuilder._boolTrue;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x00028BB8 File Offset: 0x00026DB8
		public static DbConstantExpression False
		{
			get
			{
				return DbExpressionBuilder._boolFalse;
			}
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00028BC0 File Offset: 0x00026DC0
		public static DbConstantExpression Constant(object value)
		{
			Check.NotNull<object>(value, "value");
			TypeUsage resultType = ArgumentValidation.ValidateConstant(value);
			return new DbConstantExpression(resultType, value);
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00028BE7 File Offset: 0x00026DE7
		public static DbConstantExpression Constant(this TypeUsage constantType, object value)
		{
			Check.NotNull<TypeUsage>(constantType, "constantType");
			Check.NotNull<object>(value, "value");
			ArgumentValidation.ValidateConstant(constantType, value);
			return new DbConstantExpression(constantType, value);
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00028C0F File Offset: 0x00026E0F
		public static DbParameterReferenceExpression Parameter(this TypeUsage type, string name)
		{
			Check.NotNull<TypeUsage>(type, "type");
			Check.NotNull<string>(name, "name");
			ArgumentValidation.CheckType(type);
			if (!DbCommandTree.IsValidParameterName(name))
			{
				throw new ArgumentException(Strings.Cqt_CommandTree_InvalidParameterName(name), "name");
			}
			return new DbParameterReferenceExpression(type, name);
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00028C4F File Offset: 0x00026E4F
		public static DbVariableReferenceExpression Variable(this TypeUsage type, string name)
		{
			Check.NotNull<TypeUsage>(type, "type");
			Check.NotNull<string>(name, "name");
			Check.NotEmpty(name, "name");
			ArgumentValidation.CheckType(type);
			return new DbVariableReferenceExpression(type, name);
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00028C84 File Offset: 0x00026E84
		public static DbScanExpression Scan(this EntitySetBase targetSet)
		{
			Check.NotNull<EntitySetBase>(targetSet, "targetSet");
			ArgumentValidation.CheckEntitySet(targetSet, "targetSet");
			TypeUsage collectionOfEntityType = DbExpressionBuilder.CreateCollectionResultType(targetSet.ElementType);
			return new DbScanExpression(collectionOfEntityType, targetSet);
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00028CBC File Offset: 0x00026EBC
		public static DbAndExpression And(this DbExpression left, DbExpression right)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			TypeUsage commonTypeUsage = TypeHelpers.GetCommonTypeUsage(left.ResultType, right.ResultType);
			if (commonTypeUsage == null || !TypeSemantics.IsPrimitiveType(commonTypeUsage, PrimitiveTypeKind.Boolean))
			{
				throw new ArgumentException(Strings.Cqt_And_BooleanArgumentsRequired);
			}
			return new DbAndExpression(commonTypeUsage, left, right);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00028D14 File Offset: 0x00026F14
		public static DbOrExpression Or(this DbExpression left, DbExpression right)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			TypeUsage commonTypeUsage = TypeHelpers.GetCommonTypeUsage(left.ResultType, right.ResultType);
			if (commonTypeUsage == null || !TypeSemantics.IsPrimitiveType(commonTypeUsage, PrimitiveTypeKind.Boolean))
			{
				throw new ArgumentException(Strings.Cqt_Or_BooleanArgumentsRequired);
			}
			return new DbOrExpression(commonTypeUsage, left, right);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00028D6C File Offset: 0x00026F6C
		public static DbInExpression In(this DbExpression expression, IList<DbConstantExpression> list)
		{
			Check.NotNull<DbExpression>(expression, "expression");
			Check.NotNull<IList<DbConstantExpression>>(list, "list");
			List<DbExpression> list2 = new List<DbExpression>(list.Count);
			foreach (DbConstantExpression dbConstantExpression in list)
			{
				if (!TypeSemantics.IsEqual(expression.ResultType, dbConstantExpression.ResultType))
				{
					throw new ArgumentException(Strings.Cqt_In_SameResultTypeRequired);
				}
				list2.Add(dbConstantExpression);
			}
			return DbExpressionBuilder.CreateInExpression(expression, list2);
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00028E00 File Offset: 0x00027000
		internal static DbInExpression CreateInExpression(DbExpression item, IList<DbExpression> list)
		{
			return new DbInExpression(DbExpressionBuilder._booleanType, item, new DbExpressionList(list));
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00028E14 File Offset: 0x00027014
		public static DbNotExpression Not(this DbExpression argument)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			if (!TypeSemantics.IsPrimitiveType(argument.ResultType, PrimitiveTypeKind.Boolean))
			{
				throw new ArgumentException(Strings.Cqt_Not_BooleanArgumentRequired);
			}
			TypeUsage resultType = argument.ResultType;
			return new DbNotExpression(resultType, argument);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x00028E54 File Offset: 0x00027054
		private static DbArithmeticExpression CreateArithmetic(DbExpressionKind kind, DbExpression left, DbExpression right)
		{
			TypeUsage commonTypeUsage = TypeHelpers.GetCommonTypeUsage(left.ResultType, right.ResultType);
			if (commonTypeUsage == null || !TypeSemantics.IsNumericType(commonTypeUsage))
			{
				throw new ArgumentException(Strings.Cqt_Arithmetic_NumericCommonType);
			}
			DbExpressionList args = new DbExpressionList(new DbExpression[]
			{
				left,
				right
			});
			return new DbArithmeticExpression(kind, commonTypeUsage, args);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00028EA7 File Offset: 0x000270A7
		public static DbArithmeticExpression Divide(this DbExpression left, DbExpression right)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			return DbExpressionBuilder.CreateArithmetic(DbExpressionKind.Divide, left, right);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00028ECA File Offset: 0x000270CA
		public static DbArithmeticExpression Minus(this DbExpression left, DbExpression right)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			return DbExpressionBuilder.CreateArithmetic(DbExpressionKind.Minus, left, right);
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00028EED File Offset: 0x000270ED
		public static DbArithmeticExpression Modulo(this DbExpression left, DbExpression right)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			return DbExpressionBuilder.CreateArithmetic(DbExpressionKind.Modulo, left, right);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00028F10 File Offset: 0x00027110
		public static DbArithmeticExpression Multiply(this DbExpression left, DbExpression right)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			return DbExpressionBuilder.CreateArithmetic(DbExpressionKind.Multiply, left, right);
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x00028F33 File Offset: 0x00027133
		public static DbArithmeticExpression Plus(this DbExpression left, DbExpression right)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			return DbExpressionBuilder.CreateArithmetic(DbExpressionKind.Plus, left, right);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00028F58 File Offset: 0x00027158
		public static DbArithmeticExpression UnaryMinus(this DbExpression argument)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			TypeUsage typeUsage = argument.ResultType;
			if (!TypeSemantics.IsNumericType(typeUsage))
			{
				throw new ArgumentException(Strings.Cqt_Arithmetic_NumericCommonType);
			}
			if (TypeSemantics.IsUnsignedNumericType(argument.ResultType))
			{
				typeUsage = null;
				if (!TypeHelpers.TryGetClosestPromotableType(argument.ResultType, out typeUsage))
				{
					throw new ArgumentException(Strings.Cqt_Arithmetic_InvalidUnsignedTypeForUnaryMinus(argument.ResultType.EdmType.FullName));
				}
			}
			return new DbArithmeticExpression(DbExpressionKind.UnaryMinus, typeUsage, new DbExpressionList(new DbExpression[]
			{
				argument
			}));
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00028FDD File Offset: 0x000271DD
		public static DbArithmeticExpression Negate(this DbExpression argument)
		{
			return argument.UnaryMinus();
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00028FE8 File Offset: 0x000271E8
		private static DbComparisonExpression CreateComparison(DbExpressionKind kind, DbExpression left, DbExpression right)
		{
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
				throw new ArgumentException(Strings.Cqt_Comparison_ComparableRequired);
			}
			return new DbComparisonExpression(kind, DbExpressionBuilder._booleanType, left, right);
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00029077 File Offset: 0x00027277
		public static DbComparisonExpression Equal(this DbExpression left, DbExpression right)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			return DbExpressionBuilder.CreateComparison(DbExpressionKind.Equals, left, right);
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0002909A File Offset: 0x0002729A
		public static DbComparisonExpression NotEqual(this DbExpression left, DbExpression right)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			return DbExpressionBuilder.CreateComparison(DbExpressionKind.NotEquals, left, right);
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x000290BD File Offset: 0x000272BD
		public static DbComparisonExpression GreaterThan(this DbExpression left, DbExpression right)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			return DbExpressionBuilder.CreateComparison(DbExpressionKind.GreaterThan, left, right);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x000290E0 File Offset: 0x000272E0
		public static DbComparisonExpression LessThan(this DbExpression left, DbExpression right)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			return DbExpressionBuilder.CreateComparison(DbExpressionKind.LessThan, left, right);
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00029103 File Offset: 0x00027303
		public static DbComparisonExpression GreaterThanOrEqual(this DbExpression left, DbExpression right)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			return DbExpressionBuilder.CreateComparison(DbExpressionKind.GreaterThanOrEquals, left, right);
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00029126 File Offset: 0x00027326
		public static DbComparisonExpression LessThanOrEqual(this DbExpression left, DbExpression right)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			return DbExpressionBuilder.CreateComparison(DbExpressionKind.LessThanOrEquals, left, right);
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00029149 File Offset: 0x00027349
		public static DbIsNullExpression IsNull(this DbExpression argument)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			DbExpressionBuilder.ValidateIsNull(argument);
			return new DbIsNullExpression(DbExpressionBuilder._booleanType, argument);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00029168 File Offset: 0x00027368
		private static void ValidateIsNull(DbExpression argument)
		{
			if (TypeSemantics.IsCollectionType(argument.ResultType))
			{
				throw new ArgumentException(Strings.Cqt_IsNull_CollectionNotAllowed);
			}
			if (!TypeHelpers.IsValidIsNullOpType(argument.ResultType))
			{
				throw new ArgumentException(Strings.Cqt_IsNull_InvalidType);
			}
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0002919C File Offset: 0x0002739C
		public static DbLikeExpression Like(this DbExpression argument, DbExpression pattern)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			Check.NotNull<DbExpression>(pattern, "pattern");
			DbExpressionBuilder.ValidateLike(argument, pattern);
			DbExpression escape = pattern.ResultType.Null();
			return new DbLikeExpression(DbExpressionBuilder._booleanType, argument, pattern, escape);
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x000291E1 File Offset: 0x000273E1
		public static DbLikeExpression Like(this DbExpression argument, DbExpression pattern, DbExpression escape)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			Check.NotNull<DbExpression>(pattern, "pattern");
			Check.NotNull<DbExpression>(escape, "escape");
			DbExpressionBuilder.ValidateLike(argument, pattern, escape);
			return new DbLikeExpression(DbExpressionBuilder._booleanType, argument, pattern, escape);
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0002921C File Offset: 0x0002741C
		private static void ValidateLike(DbExpression argument, DbExpression pattern, DbExpression escape)
		{
			DbExpressionBuilder.ValidateLike(argument, pattern);
			ArgumentValidation.RequireCompatibleType(escape, PrimitiveTypeKind.String, "escape");
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00029232 File Offset: 0x00027432
		private static void ValidateLike(DbExpression argument, DbExpression pattern)
		{
			ArgumentValidation.RequireCompatibleType(argument, PrimitiveTypeKind.String, "argument");
			ArgumentValidation.RequireCompatibleType(pattern, PrimitiveTypeKind.String, "pattern");
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00029250 File Offset: 0x00027450
		public static DbCastExpression CastTo(this DbExpression argument, TypeUsage toType)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			Check.NotNull<TypeUsage>(toType, "toType");
			ArgumentValidation.CheckType(toType, "toType");
			if (!TypeSemantics.IsCastAllowed(argument.ResultType, toType))
			{
				throw new ArgumentException(Strings.Cqt_Cast_InvalidCast(argument.ResultType.ToString(), toType.ToString()));
			}
			return new DbCastExpression(toType, argument);
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x000292B4 File Offset: 0x000274B4
		public static DbTreatExpression TreatAs(this DbExpression argument, TypeUsage treatType)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			Check.NotNull<TypeUsage>(treatType, "treatType");
			ArgumentValidation.CheckType(treatType, "treatType");
			ArgumentValidation.RequirePolymorphicType(treatType);
			if (!TypeSemantics.IsValidPolymorphicCast(argument.ResultType, treatType))
			{
				throw new ArgumentException(Strings.Cqt_General_PolymorphicArgRequired(typeof(DbTreatExpression).Name));
			}
			return new DbTreatExpression(treatType, argument);
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0002931C File Offset: 0x0002751C
		public static DbOfTypeExpression OfType(this DbExpression argument, TypeUsage type)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			Check.NotNull<TypeUsage>(type, "type");
			DbExpressionBuilder.ValidateOfType(argument, type);
			TypeUsage collectionResultType = DbExpressionBuilder.CreateCollectionResultType(type);
			return new DbOfTypeExpression(DbExpressionKind.OfType, collectionResultType, argument, type);
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0002935C File Offset: 0x0002755C
		public static DbOfTypeExpression OfTypeOnly(this DbExpression argument, TypeUsage type)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			Check.NotNull<TypeUsage>(type, "type");
			DbExpressionBuilder.ValidateOfType(argument, type);
			TypeUsage collectionResultType = DbExpressionBuilder.CreateCollectionResultType(type);
			return new DbOfTypeExpression(DbExpressionKind.OfTypeOnly, collectionResultType, argument, type);
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00029399 File Offset: 0x00027599
		public static DbIsOfExpression IsOf(this DbExpression argument, TypeUsage type)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			Check.NotNull<TypeUsage>(type, "type");
			DbExpressionBuilder.ValidateIsOf(argument, type);
			return new DbIsOfExpression(DbExpressionKind.IsOf, DbExpressionBuilder._booleanType, argument, type);
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x000293C8 File Offset: 0x000275C8
		public static DbIsOfExpression IsOfOnly(this DbExpression argument, TypeUsage type)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			Check.NotNull<TypeUsage>(type, "type");
			DbExpressionBuilder.ValidateIsOf(argument, type);
			return new DbIsOfExpression(DbExpressionKind.IsOfOnly, DbExpressionBuilder._booleanType, argument, type);
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x000293F8 File Offset: 0x000275F8
		private static void ValidateOfType(DbExpression argument, TypeUsage type)
		{
			ArgumentValidation.CheckType(type, "type");
			ArgumentValidation.RequirePolymorphicType(type);
			ArgumentValidation.RequireCollectionArgument<DbOfTypeExpression>(argument);
			TypeUsage fromType = null;
			if (!TypeHelpers.TryGetCollectionElementType(argument.ResultType, out fromType) || !TypeSemantics.IsValidPolymorphicCast(fromType, type))
			{
				throw new ArgumentException(Strings.Cqt_General_PolymorphicArgRequired(typeof(DbOfTypeExpression).Name));
			}
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00029450 File Offset: 0x00027650
		private static void ValidateIsOf(DbExpression argument, TypeUsage type)
		{
			ArgumentValidation.CheckType(type, "type");
			ArgumentValidation.RequirePolymorphicType(type);
			if (!TypeSemantics.IsValidPolymorphicCast(argument.ResultType, type))
			{
				throw new ArgumentException(Strings.Cqt_General_PolymorphicArgRequired(typeof(DbIsOfExpression).Name));
			}
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0002948C File Offset: 0x0002768C
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Deref")]
		public static DbDerefExpression Deref(this DbExpression argument)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			EntityType edmType;
			if (!TypeHelpers.TryGetRefEntityType(argument.ResultType, out edmType))
			{
				throw new ArgumentException(Strings.Cqt_DeRef_RefRequired, "argument");
			}
			TypeUsage entityResultType = TypeUsage.Create(edmType);
			return new DbDerefExpression(entityResultType, argument);
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x000294D4 File Offset: 0x000276D4
		public static DbEntityRefExpression GetEntityRef(this DbExpression argument)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			EntityType referencedEntityType = null;
			if (!TypeHelpers.TryGetEdmType<EntityType>(argument.ResultType, out referencedEntityType))
			{
				throw new ArgumentException(Strings.Cqt_GetEntityRef_EntityRequired, "argument");
			}
			TypeUsage refResultType = ArgumentValidation.CreateReferenceResultType(referencedEntityType);
			return new DbEntityRefExpression(refResultType, argument);
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0002951C File Offset: 0x0002771C
		public static DbRefExpression CreateRef(this EntitySet entitySet, IEnumerable<DbExpression> keyValues)
		{
			Check.NotNull<EntitySet>(entitySet, "entitySet");
			Check.NotNull<IEnumerable<DbExpression>>(keyValues, "keyValues");
			return DbExpressionBuilder.CreateRefExpression(entitySet, keyValues);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0002953D File Offset: 0x0002773D
		public static DbRefExpression CreateRef(this EntitySet entitySet, params DbExpression[] keyValues)
		{
			Check.NotNull<EntitySet>(entitySet, "entitySet");
			Check.NotNull<DbExpression[]>(keyValues, "keyValues");
			return DbExpressionBuilder.CreateRefExpression(entitySet, keyValues);
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0002955E File Offset: 0x0002775E
		public static DbRefExpression CreateRef(this EntitySet entitySet, EntityType entityType, IEnumerable<DbExpression> keyValues)
		{
			Check.NotNull<EntitySet>(entitySet, "entitySet");
			Check.NotNull<EntityType>(entityType, "entityType");
			Check.NotNull<IEnumerable<DbExpression>>(keyValues, "keyValues");
			return DbExpressionBuilder.CreateRefExpression(entitySet, entityType, keyValues);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0002958C File Offset: 0x0002778C
		public static DbRefExpression CreateRef(this EntitySet entitySet, EntityType entityType, params DbExpression[] keyValues)
		{
			Check.NotNull<EntitySet>(entitySet, "entitySet");
			Check.NotNull<EntityType>(entityType, "entityType");
			Check.NotNull<DbExpression[]>(keyValues, "keyValues");
			return DbExpressionBuilder.CreateRefExpression(entitySet, entityType, keyValues);
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x000295BC File Offset: 0x000277BC
		private static DbRefExpression CreateRefExpression(EntitySet entitySet, IEnumerable<DbExpression> keyValues)
		{
			DbExpression refKeys;
			TypeUsage refResultType = ArgumentValidation.ValidateCreateRef(entitySet, entitySet.ElementType, keyValues, out refKeys);
			return new DbRefExpression(refResultType, entitySet, refKeys);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x000295E4 File Offset: 0x000277E4
		private static DbRefExpression CreateRefExpression(EntitySet entitySet, EntityType entityType, IEnumerable<DbExpression> keyValues)
		{
			Check.NotNull<EntitySet>(entitySet, "entitySet");
			Check.NotNull<EntityType>(entityType, "entityType");
			DbExpression refKeys;
			TypeUsage refResultType = ArgumentValidation.ValidateCreateRef(entitySet, entityType, keyValues, out refKeys);
			return new DbRefExpression(refResultType, entitySet, refKeys);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0002961C File Offset: 0x0002781C
		public static DbRefExpression RefFromKey(this EntitySet entitySet, DbExpression keyRow)
		{
			Check.NotNull<EntitySet>(entitySet, "entitySet");
			Check.NotNull<DbExpression>(keyRow, "keyRow");
			TypeUsage refResultType = ArgumentValidation.ValidateRefFromKey(entitySet, keyRow, entitySet.ElementType);
			return new DbRefExpression(refResultType, entitySet, keyRow);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00029658 File Offset: 0x00027858
		public static DbRefExpression RefFromKey(this EntitySet entitySet, DbExpression keyRow, EntityType entityType)
		{
			Check.NotNull<EntitySet>(entitySet, "entitySet");
			Check.NotNull<DbExpression>(keyRow, "keyRow");
			Check.NotNull<EntityType>(entityType, "entityType");
			TypeUsage refResultType = ArgumentValidation.ValidateRefFromKey(entitySet, keyRow, entityType);
			return new DbRefExpression(refResultType, entitySet, keyRow);
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0002969C File Offset: 0x0002789C
		public static DbRefKeyExpression GetRefKey(this DbExpression argument)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			RefType refType = null;
			if (!TypeHelpers.TryGetEdmType<RefType>(argument.ResultType, out refType))
			{
				throw new ArgumentException(Strings.Cqt_GetRefKey_RefRequired, "argument");
			}
			TypeUsage rowResultType = TypeUsage.Create(TypeHelpers.CreateKeyRowType(refType.ElementType));
			return new DbRefKeyExpression(rowResultType, argument);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x000296F0 File Offset: 0x000278F0
		public static DbRelationshipNavigationExpression Navigate(this DbExpression navigateFrom, RelationshipEndMember fromEnd, RelationshipEndMember toEnd)
		{
			Check.NotNull<DbExpression>(navigateFrom, "navigateFrom");
			Check.NotNull<RelationshipEndMember>(fromEnd, "fromEnd");
			Check.NotNull<RelationshipEndMember>(toEnd, "toEnd");
			RelationshipType relType;
			TypeUsage resultType = ArgumentValidation.ValidateNavigate(navigateFrom, fromEnd, toEnd, out relType, false);
			return new DbRelationshipNavigationExpression(resultType, relType, fromEnd, toEnd, navigateFrom);
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00029738 File Offset: 0x00027938
		public static DbRelationshipNavigationExpression Navigate(this RelationshipType type, string fromEndName, string toEndName, DbExpression navigateFrom)
		{
			Check.NotNull<RelationshipType>(type, "type");
			Check.NotNull<string>(fromEndName, "fromEndName");
			Check.NotNull<string>(toEndName, "toEndName");
			Check.NotNull<DbExpression>(navigateFrom, "navigateFrom");
			RelationshipEndMember fromEnd;
			RelationshipEndMember toEnd;
			TypeUsage resultType = ArgumentValidation.ValidateNavigate(navigateFrom, type, fromEndName, toEndName, out fromEnd, out toEnd);
			return new DbRelationshipNavigationExpression(resultType, type, fromEnd, toEnd, navigateFrom);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00029790 File Offset: 0x00027990
		public static DbDistinctExpression Distinct(this DbExpression argument)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			ArgumentValidation.RequireCollectionArgument<DbDistinctExpression>(argument);
			CollectionType edmType = TypeHelpers.GetEdmType<CollectionType>(argument.ResultType);
			if (!TypeHelpers.IsValidDistinctOpType(edmType.TypeUsage))
			{
				throw new ArgumentException(Strings.Cqt_Distinct_InvalidCollection, "argument");
			}
			return new DbDistinctExpression(argument.ResultType, argument);
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x000297E4 File Offset: 0x000279E4
		public static DbElementExpression Element(this DbExpression argument)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			TypeUsage resultType = ArgumentValidation.ValidateElement(argument);
			return new DbElementExpression(resultType, argument);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0002980B File Offset: 0x00027A0B
		public static DbIsEmptyExpression IsEmpty(this DbExpression argument)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			ArgumentValidation.RequireCollectionArgument<DbIsEmptyExpression>(argument);
			return new DbIsEmptyExpression(DbExpressionBuilder._booleanType, argument);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0002982C File Offset: 0x00027A2C
		public static DbExceptExpression Except(this DbExpression left, DbExpression right)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			ArgumentValidation.RequireComparableCollectionArguments<DbExceptExpression>(left, right);
			TypeUsage resultType = left.ResultType;
			return new DbExceptExpression(resultType, left, right);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00029868 File Offset: 0x00027A68
		public static DbIntersectExpression Intersect(this DbExpression left, DbExpression right)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			TypeUsage resultType = ArgumentValidation.RequireComparableCollectionArguments<DbIntersectExpression>(left, right);
			return new DbIntersectExpression(resultType, left, right);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x000298A0 File Offset: 0x00027AA0
		public static DbUnionAllExpression UnionAll(this DbExpression left, DbExpression right)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			TypeUsage resultType = ArgumentValidation.RequireCollectionArguments<DbUnionAllExpression>(left, right);
			return new DbUnionAllExpression(resultType, left, right);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x000298D8 File Offset: 0x00027AD8
		public static DbLimitExpression Limit(this DbExpression argument, DbExpression count)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			Check.NotNull<DbExpression>(count, "count");
			ArgumentValidation.RequireCollectionArgument<DbLimitExpression>(argument);
			if (!TypeSemantics.IsIntegerNumericType(count.ResultType))
			{
				throw new ArgumentException(Strings.Cqt_Limit_IntegerRequired, "count");
			}
			if (count.ExpressionKind != DbExpressionKind.Constant && count.ExpressionKind != DbExpressionKind.ParameterReference)
			{
				throw new ArgumentException(Strings.Cqt_Limit_ConstantOrParameterRefRequired, "count");
			}
			if (DbExpressionBuilder.IsConstantNegativeInteger(count))
			{
				throw new ArgumentException(Strings.Cqt_Limit_NonNegativeLimitRequired, "count");
			}
			return new DbLimitExpression(argument.ResultType, argument, count, false);
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0002996C File Offset: 0x00027B6C
		public static DbCaseExpression Case(IEnumerable<DbExpression> whenExpressions, IEnumerable<DbExpression> thenExpressions, DbExpression elseExpression)
		{
			Check.NotNull<IEnumerable<DbExpression>>(whenExpressions, "whenExpressions");
			Check.NotNull<IEnumerable<DbExpression>>(thenExpressions, "thenExpressions");
			Check.NotNull<DbExpression>(elseExpression, "elseExpression");
			DbExpressionList whens;
			DbExpressionList thens;
			TypeUsage commonResultType = ArgumentValidation.ValidateCase(whenExpressions, thenExpressions, elseExpression, out whens, out thens);
			return new DbCaseExpression(commonResultType, whens, thens, elseExpression);
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x000299B3 File Offset: 0x00027BB3
		public static DbFunctionExpression Invoke(this EdmFunction function, IEnumerable<DbExpression> arguments)
		{
			Check.NotNull<EdmFunction>(function, "function");
			return DbExpressionBuilder.InvokeFunction(function, arguments);
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x000299C8 File Offset: 0x00027BC8
		public static DbFunctionExpression Invoke(this EdmFunction function, params DbExpression[] arguments)
		{
			Check.NotNull<EdmFunction>(function, "function");
			return DbExpressionBuilder.InvokeFunction(function, arguments);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x000299E0 File Offset: 0x00027BE0
		private static DbFunctionExpression InvokeFunction(EdmFunction function, IEnumerable<DbExpression> arguments)
		{
			DbExpressionList arguments2;
			TypeUsage resultType = ArgumentValidation.ValidateFunction(function, arguments, out arguments2);
			return new DbFunctionExpression(resultType, function, arguments2);
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x000299FF File Offset: 0x00027BFF
		public static DbLambdaExpression Invoke(this DbLambda lambda, IEnumerable<DbExpression> arguments)
		{
			Check.NotNull<DbLambda>(lambda, "lambda");
			Check.NotNull<IEnumerable<DbExpression>>(arguments, "arguments");
			return DbExpressionBuilder.InvokeLambda(lambda, arguments);
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00029A20 File Offset: 0x00027C20
		public static DbLambdaExpression Invoke(this DbLambda lambda, params DbExpression[] arguments)
		{
			Check.NotNull<DbLambda>(lambda, "lambda");
			Check.NotNull<DbExpression[]>(arguments, "arguments");
			return DbExpressionBuilder.InvokeLambda(lambda, arguments);
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00029A44 File Offset: 0x00027C44
		private static DbLambdaExpression InvokeLambda(DbLambda lambda, IEnumerable<DbExpression> arguments)
		{
			DbExpressionList args;
			TypeUsage resultType = ArgumentValidation.ValidateInvoke(lambda, arguments, out args);
			return new DbLambdaExpression(resultType, lambda, args);
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00029A63 File Offset: 0x00027C63
		public static DbNewInstanceExpression New(this TypeUsage instanceType, IEnumerable<DbExpression> arguments)
		{
			Check.NotNull<TypeUsage>(instanceType, "instanceType");
			return DbExpressionBuilder.NewInstance(instanceType, arguments);
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00029A78 File Offset: 0x00027C78
		public static DbNewInstanceExpression New(this TypeUsage instanceType, params DbExpression[] arguments)
		{
			Check.NotNull<TypeUsage>(instanceType, "instanceType");
			return DbExpressionBuilder.NewInstance(instanceType, arguments);
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00029A90 File Offset: 0x00027C90
		private static DbNewInstanceExpression NewInstance(TypeUsage instanceType, IEnumerable<DbExpression> arguments)
		{
			DbExpressionList args;
			TypeUsage type = ArgumentValidation.ValidateNew(instanceType, arguments, out args);
			return new DbNewInstanceExpression(type, args);
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00029AAE File Offset: 0x00027CAE
		public static DbNewInstanceExpression NewCollection(IEnumerable<DbExpression> elements)
		{
			return DbExpressionBuilder.CreateNewCollection(elements);
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00029AB6 File Offset: 0x00027CB6
		public static DbNewInstanceExpression NewCollection(params DbExpression[] elements)
		{
			Check.NotNull<DbExpression[]>(elements, "elements");
			return DbExpressionBuilder.CreateNewCollection(elements);
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00029B28 File Offset: 0x00027D28
		[SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly")]
		private static DbNewInstanceExpression CreateNewCollection(IEnumerable<DbExpression> elements)
		{
			TypeUsage commonElementType = null;
			DbExpressionList args = ArgumentValidation.CreateExpressionList(elements, "elements", delegate(DbExpression exp, int idx)
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
					throw new ArgumentException(Strings.Cqt_Factory_NewCollectionInvalidCommonType, "collectionElements");
				}
			});
			TypeUsage type = DbExpressionBuilder.CreateCollectionResultType(commonElementType);
			return new DbNewInstanceExpression(type, args);
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00029B70 File Offset: 0x00027D70
		public static DbNewInstanceExpression NewEmptyCollection(this TypeUsage collectionType)
		{
			Check.NotNull<TypeUsage>(collectionType, "collectionType");
			DbExpressionList args;
			TypeUsage type = ArgumentValidation.ValidateNewEmptyCollection(collectionType, out args);
			return new DbNewInstanceExpression(type, args);
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00029B9C File Offset: 0x00027D9C
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static DbNewInstanceExpression NewRow(IEnumerable<KeyValuePair<string, DbExpression>> columnValues)
		{
			Check.NotNull<IEnumerable<KeyValuePair<string, DbExpression>>>(columnValues, "columnValues");
			DbExpressionList args;
			TypeUsage type = ArgumentValidation.ValidateNewRow(columnValues, out args);
			return new DbNewInstanceExpression(type, args);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00029BC5 File Offset: 0x00027DC5
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "required for this feature")]
		public static DbPropertyExpression Property(this DbExpression instance, EdmProperty propertyMetadata)
		{
			Check.NotNull<DbExpression>(instance, "instance");
			Check.NotNull<EdmProperty>(propertyMetadata, "propertyMetadata");
			return DbExpressionBuilder.PropertyFromMember(instance, propertyMetadata, "propertyMetadata");
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00029BEB File Offset: 0x00027DEB
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "required for this feature")]
		public static DbPropertyExpression Property(this DbExpression instance, NavigationProperty navigationProperty)
		{
			Check.NotNull<DbExpression>(instance, "instance");
			Check.NotNull<NavigationProperty>(navigationProperty, "navigationProperty");
			return DbExpressionBuilder.PropertyFromMember(instance, navigationProperty, "navigationProperty");
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00029C11 File Offset: 0x00027E11
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "required for this feature")]
		public static DbPropertyExpression Property(this DbExpression instance, RelationshipEndMember relationshipEnd)
		{
			Check.NotNull<DbExpression>(instance, "instance");
			Check.NotNull<RelationshipEndMember>(relationshipEnd, "relationshipEnd");
			return DbExpressionBuilder.PropertyFromMember(instance, relationshipEnd, "relationshipEnd");
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00029C37 File Offset: 0x00027E37
		public static DbPropertyExpression Property(this DbExpression instance, string propertyName)
		{
			return DbExpressionBuilder.PropertyByName(instance, propertyName, false);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00029C44 File Offset: 0x00027E44
		private static DbPropertyExpression PropertyFromMember(DbExpression instance, EdmMember property, string propertyArgumentName)
		{
			ArgumentValidation.CheckMember(property, propertyArgumentName);
			if (instance == null)
			{
				throw new ArgumentException(Strings.Cqt_Property_InstanceRequiredForInstance, "instance");
			}
			TypeUsage requiredResultType = TypeUsage.Create(property.DeclaringType);
			ArgumentValidation.RequireCompatibleType(instance, requiredResultType, "instance");
			return new DbPropertyExpression(Helper.GetModelTypeUsage(property), property, instance);
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00029C90 File Offset: 0x00027E90
		private static DbPropertyExpression PropertyByName(DbExpression instance, string propertyName, bool ignoreCase)
		{
			Check.NotNull<DbExpression>(instance, "instance");
			Check.NotNull<string>(propertyName, "propertyName");
			EdmMember property;
			TypeUsage resultType = ArgumentValidation.ValidateProperty(instance, propertyName, ignoreCase, out property);
			return new DbPropertyExpression(resultType, property, instance);
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00029CC8 File Offset: 0x00027EC8
		public static DbSetClause SetClause(DbExpression property, DbExpression value)
		{
			Check.NotNull<DbExpression>(property, "property");
			Check.NotNull<DbExpression>(value, "value");
			return new DbSetClause(property, value);
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00029CEC File Offset: 0x00027EEC
		private static string ExtractAlias(MethodInfo method)
		{
			string[] array = DbExpressionBuilder.ExtractAliases(method);
			return array[0];
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00029D10 File Offset: 0x00027F10
		internal static string[] ExtractAliases(MethodInfo method)
		{
			ParameterInfo[] parameters = method.GetParameters();
			int num;
			int num2;
			if (method.IsStatic && typeof(Closure) == parameters[0].ParameterType)
			{
				num = 1;
				num2 = parameters.Length - 1;
			}
			else
			{
				num = 0;
				num2 = parameters.Length;
			}
			string[] array = new string[num2];
			bool flag = parameters.Skip(num).Any((ParameterInfo p) => p.Name == null);
			for (int i = num; i < parameters.Length; i++)
			{
				array[i - num] = (flag ? DbExpressionBuilder._bindingAliases.Next() : parameters[i].Name);
			}
			return array;
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00029DBC File Offset: 0x00027FBC
		private static DbExpressionBinding ConvertToBinding<TResult>(DbExpression source, Func<DbExpression, TResult> argument, out TResult argumentResult)
		{
			string varName = DbExpressionBuilder.ExtractAlias(argument.Method);
			DbExpressionBinding dbExpressionBinding = source.BindAs(varName);
			argumentResult = argument(dbExpressionBinding.Variable);
			return dbExpressionBinding;
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00029DF0 File Offset: 0x00027FF0
		private static DbExpressionBinding[] ConvertToBinding(DbExpression left, DbExpression right, Func<DbExpression, DbExpression, DbExpression> argument, out DbExpression argumentExp)
		{
			string[] array = DbExpressionBuilder.ExtractAliases(argument.Method);
			DbExpressionBinding dbExpressionBinding = left.BindAs(array[0]);
			DbExpressionBinding dbExpressionBinding2 = right.BindAs(array[1]);
			argumentExp = argument(dbExpressionBinding.Variable, dbExpressionBinding2.Variable);
			return new DbExpressionBinding[]
			{
				dbExpressionBinding,
				dbExpressionBinding2
			};
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00029E54 File Offset: 0x00028054
		internal static List<KeyValuePair<string, TRequired>> TryGetAnonymousTypeValues<TInstance, TRequired>(object instance)
		{
			IEnumerable<PropertyInfo> instanceProperties = typeof(TInstance).GetInstanceProperties();
			if (typeof(TInstance).BaseType() != typeof(object) || instanceProperties.Any((PropertyInfo p) => !p.IsPublic()))
			{
				return null;
			}
			List<KeyValuePair<string, TRequired>> list = null;
			foreach (PropertyInfo propertyInfo in from p in instanceProperties
			where p.IsPublic()
			select p)
			{
				if (!propertyInfo.CanRead || !typeof(TRequired).IsAssignableFrom(propertyInfo.PropertyType))
				{
					return null;
				}
				if (list == null)
				{
					list = new List<KeyValuePair<string, TRequired>>();
				}
				list.Add(new KeyValuePair<string, TRequired>(propertyInfo.Name, (TRequired)((object)propertyInfo.GetValue(instance, null))));
			}
			return list;
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00029F44 File Offset: 0x00028144
		private static bool TryResolveToConstant(Type type, object value, out DbExpression constantOrNullExpression)
		{
			constantOrNullExpression = null;
			Type clrType = type;
			if (type.IsGenericType() && typeof(Nullable<>).Equals(type.GetGenericTypeDefinition()))
			{
				clrType = type.GetGenericArguments()[0];
			}
			PrimitiveTypeKind primitiveTypeKind;
			if (ClrProviderManifest.TryGetPrimitiveTypeKind(clrType, out primitiveTypeKind))
			{
				TypeUsage literalTypeUsage = TypeHelpers.GetLiteralTypeUsage(primitiveTypeKind);
				if (value == null)
				{
					constantOrNullExpression = literalTypeUsage.Null();
				}
				else
				{
					constantOrNullExpression = literalTypeUsage.Constant(value);
				}
			}
			return constantOrNullExpression != null;
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00029FB0 File Offset: 0x000281B0
		private static DbExpression ResolveToExpression<TArgument>(TArgument argument)
		{
			object obj = argument;
			DbExpression result;
			if (DbExpressionBuilder.TryResolveToConstant(typeof(TArgument), obj, out result))
			{
				return result;
			}
			if (obj == null)
			{
				return null;
			}
			if (typeof(DbExpression).IsAssignableFrom(typeof(TArgument)))
			{
				return (DbExpression)obj;
			}
			if (typeof(Row).Equals(typeof(TArgument)))
			{
				return ((Row)obj).ToExpression();
			}
			List<KeyValuePair<string, DbExpression>> list = DbExpressionBuilder.TryGetAnonymousTypeValues<TArgument, DbExpression>(obj);
			if (list != null)
			{
				return DbExpressionBuilder.NewRow(list);
			}
			throw new NotSupportedException(Strings.Cqt_Factory_MethodResultTypeNotSupported(typeof(TArgument).FullName));
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0002A054 File Offset: 0x00028254
		private static DbApplyExpression CreateApply(DbExpression source, Func<DbExpression, KeyValuePair<string, DbExpression>> apply, Func<DbExpressionBinding, DbExpressionBinding, DbApplyExpression> resultBuilder)
		{
			KeyValuePair<string, DbExpression> keyValuePair;
			DbExpressionBinding arg = DbExpressionBuilder.ConvertToBinding<KeyValuePair<string, DbExpression>>(source, apply, out keyValuePair);
			DbExpressionBinding arg2 = keyValuePair.Value.BindAs(keyValuePair.Key);
			return resultBuilder(arg, arg2);
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0002A088 File Offset: 0x00028288
		public static DbQuantifierExpression All(this DbExpression source, Func<DbExpression, DbExpression> predicate)
		{
			Check.NotNull<DbExpression>(source, "source");
			Check.NotNull<Func<DbExpression, DbExpression>>(predicate, "predicate");
			DbExpression predicate2;
			DbExpressionBinding input = DbExpressionBuilder.ConvertToBinding<DbExpression>(source, predicate, out predicate2);
			return input.All(predicate2);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0002A0BE File Offset: 0x000282BE
		public static DbExpression Any(this DbExpression source)
		{
			return source.Exists();
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0002A0C6 File Offset: 0x000282C6
		public static DbExpression Exists(this DbExpression argument)
		{
			return argument.IsEmpty().Not();
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0002A0D4 File Offset: 0x000282D4
		public static DbQuantifierExpression Any(this DbExpression source, Func<DbExpression, DbExpression> predicate)
		{
			Check.NotNull<DbExpression>(source, "source");
			Check.NotNull<Func<DbExpression, DbExpression>>(predicate, "predicate");
			DbExpression predicate2;
			DbExpressionBinding input = DbExpressionBuilder.ConvertToBinding<DbExpression>(source, predicate, out predicate2);
			return input.Any(predicate2);
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0002A10A File Offset: 0x0002830A
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static DbApplyExpression CrossApply(this DbExpression source, Func<DbExpression, KeyValuePair<string, DbExpression>> apply)
		{
			Check.NotNull<DbExpression>(source, "source");
			Check.NotNull<Func<DbExpression, KeyValuePair<string, DbExpression>>>(apply, "apply");
			return DbExpressionBuilder.CreateApply(source, apply, new Func<DbExpressionBinding, DbExpressionBinding, DbApplyExpression>(DbExpressionBuilder.CrossApply));
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0002A137 File Offset: 0x00028337
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static DbApplyExpression OuterApply(this DbExpression source, Func<DbExpression, KeyValuePair<string, DbExpression>> apply)
		{
			Check.NotNull<DbExpression>(source, "source");
			Check.NotNull<Func<DbExpression, KeyValuePair<string, DbExpression>>>(apply, "apply");
			return DbExpressionBuilder.CreateApply(source, apply, new Func<DbExpressionBinding, DbExpressionBinding, DbApplyExpression>(DbExpressionBuilder.OuterApply));
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0002A164 File Offset: 0x00028364
		public static DbJoinExpression FullOuterJoin(this DbExpression left, DbExpression right, Func<DbExpression, DbExpression, DbExpression> joinCondition)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression>>(joinCondition, "joinCondition");
			DbExpression joinCondition2;
			DbExpressionBinding[] array = DbExpressionBuilder.ConvertToBinding(left, right, joinCondition, out joinCondition2);
			return array[0].FullOuterJoin(array[1], joinCondition2);
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0002A1AC File Offset: 0x000283AC
		public static DbJoinExpression InnerJoin(this DbExpression left, DbExpression right, Func<DbExpression, DbExpression, DbExpression> joinCondition)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression>>(joinCondition, "joinCondition");
			DbExpression joinCondition2;
			DbExpressionBinding[] array = DbExpressionBuilder.ConvertToBinding(left, right, joinCondition, out joinCondition2);
			return array[0].InnerJoin(array[1], joinCondition2);
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0002A1F4 File Offset: 0x000283F4
		public static DbJoinExpression LeftOuterJoin(this DbExpression left, DbExpression right, Func<DbExpression, DbExpression, DbExpression> joinCondition)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression>>(joinCondition, "joinCondition");
			DbExpression joinCondition2;
			DbExpressionBinding[] array = DbExpressionBuilder.ConvertToBinding(left, right, joinCondition, out joinCondition2);
			return array[0].LeftOuterJoin(array[1], joinCondition2);
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0002A23C File Offset: 0x0002843C
		public static DbJoinExpression Join(this DbExpression outer, DbExpression inner, Func<DbExpression, DbExpression> outerKey, Func<DbExpression, DbExpression> innerKey)
		{
			Check.NotNull<DbExpression>(outer, "outer");
			Check.NotNull<DbExpression>(inner, "inner");
			Check.NotNull<Func<DbExpression, DbExpression>>(outerKey, "outerKey");
			Check.NotNull<Func<DbExpression, DbExpression>>(innerKey, "innerKey");
			DbExpression left2;
			DbExpressionBinding left = DbExpressionBuilder.ConvertToBinding<DbExpression>(outer, outerKey, out left2);
			DbExpression right2;
			DbExpressionBinding right = DbExpressionBuilder.ConvertToBinding<DbExpression>(inner, innerKey, out right2);
			DbExpression joinCondition = left2.Equal(right2);
			return left.InnerJoin(right, joinCondition);
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0002A2A0 File Offset: 0x000284A0
		public static DbProjectExpression Join<TSelector>(this DbExpression outer, DbExpression inner, Func<DbExpression, DbExpression> outerKey, Func<DbExpression, DbExpression> innerKey, Func<DbExpression, DbExpression, TSelector> selector)
		{
			Check.NotNull<Func<DbExpression, DbExpression, TSelector>>(selector, "selector");
			DbJoinExpression dbJoinExpression = outer.Join(inner, outerKey, innerKey);
			DbExpressionBinding dbExpressionBinding = dbJoinExpression.Bind();
			DbExpression arg = dbExpressionBinding.Variable.Property(dbJoinExpression.Left.VariableName);
			DbExpression arg2 = dbExpressionBinding.Variable.Property(dbJoinExpression.Right.VariableName);
			TSelector argument = selector(arg, arg2);
			DbExpression projection = DbExpressionBuilder.ResolveToExpression<TSelector>(argument);
			return dbExpressionBinding.Project(projection);
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0002A318 File Offset: 0x00028518
		public static DbSortExpression OrderBy(this DbExpression source, Func<DbExpression, DbExpression> sortKey)
		{
			Check.NotNull<DbExpression>(source, "source");
			Check.NotNull<Func<DbExpression, DbExpression>>(sortKey, "sortKey");
			DbExpression key;
			DbExpressionBinding input = DbExpressionBuilder.ConvertToBinding<DbExpression>(source, sortKey, out key);
			DbSortClause dbSortClause = key.ToSortClause();
			return input.Sort(new DbSortClause[]
			{
				dbSortClause
			});
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0002A360 File Offset: 0x00028560
		public static DbSortExpression OrderBy(this DbExpression source, Func<DbExpression, DbExpression> sortKey, string collation)
		{
			Check.NotNull<DbExpression>(source, "source");
			Check.NotNull<Func<DbExpression, DbExpression>>(sortKey, "sortKey");
			DbExpression key;
			DbExpressionBinding input = DbExpressionBuilder.ConvertToBinding<DbExpression>(source, sortKey, out key);
			DbSortClause dbSortClause = key.ToSortClause(collation);
			return input.Sort(new DbSortClause[]
			{
				dbSortClause
			});
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0002A3AC File Offset: 0x000285AC
		public static DbSortExpression OrderByDescending(this DbExpression source, Func<DbExpression, DbExpression> sortKey)
		{
			Check.NotNull<DbExpression>(source, "source");
			Check.NotNull<Func<DbExpression, DbExpression>>(sortKey, "sortKey");
			DbExpression key;
			DbExpressionBinding input = DbExpressionBuilder.ConvertToBinding<DbExpression>(source, sortKey, out key);
			DbSortClause dbSortClause = key.ToSortClauseDescending();
			return input.Sort(new DbSortClause[]
			{
				dbSortClause
			});
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0002A3F4 File Offset: 0x000285F4
		public static DbSortExpression OrderByDescending(this DbExpression source, Func<DbExpression, DbExpression> sortKey, string collation)
		{
			Check.NotNull<DbExpression>(source, "source");
			Check.NotNull<Func<DbExpression, DbExpression>>(sortKey, "sortKey");
			DbExpression key;
			DbExpressionBinding input = DbExpressionBuilder.ConvertToBinding<DbExpression>(source, sortKey, out key);
			DbSortClause dbSortClause = key.ToSortClauseDescending(collation);
			return input.Sort(new DbSortClause[]
			{
				dbSortClause
			});
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0002A440 File Offset: 0x00028640
		public static DbProjectExpression Select<TProjection>(this DbExpression source, Func<DbExpression, TProjection> projection)
		{
			Check.NotNull<DbExpression>(source, "source");
			Check.NotNull<Func<DbExpression, TProjection>>(projection, "projection");
			TProjection argument;
			DbExpressionBinding input = DbExpressionBuilder.ConvertToBinding<TProjection>(source, projection, out argument);
			DbExpression projection2 = DbExpressionBuilder.ResolveToExpression<TProjection>(argument);
			return input.Project(projection2);
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0002A480 File Offset: 0x00028680
		public static DbProjectExpression SelectMany(this DbExpression source, Func<DbExpression, DbExpression> apply)
		{
			Check.NotNull<DbExpression>(source, "source");
			Check.NotNull<Func<DbExpression, DbExpression>>(apply, "apply");
			DbExpression input2;
			DbExpressionBinding input = DbExpressionBuilder.ConvertToBinding<DbExpression>(source, apply, out input2);
			DbExpressionBinding dbExpressionBinding = input2.Bind();
			DbApplyExpression input3 = input.CrossApply(dbExpressionBinding);
			DbExpressionBinding dbExpressionBinding2 = input3.Bind();
			return dbExpressionBinding2.Project(dbExpressionBinding2.Variable.Property(dbExpressionBinding.VariableName));
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0002A4E0 File Offset: 0x000286E0
		public static DbProjectExpression SelectMany<TSelector>(this DbExpression source, Func<DbExpression, DbExpression> apply, Func<DbExpression, DbExpression, TSelector> selector)
		{
			Check.NotNull<DbExpression>(source, "source");
			Check.NotNull<Func<DbExpression, DbExpression>>(apply, "apply");
			Check.NotNull<Func<DbExpression, DbExpression, TSelector>>(selector, "selector");
			DbExpression input;
			DbExpressionBinding dbExpressionBinding = DbExpressionBuilder.ConvertToBinding<DbExpression>(source, apply, out input);
			DbExpressionBinding dbExpressionBinding2 = input.Bind();
			DbApplyExpression input2 = dbExpressionBinding.CrossApply(dbExpressionBinding2);
			DbExpressionBinding dbExpressionBinding3 = input2.Bind();
			DbExpression arg = dbExpressionBinding3.Variable.Property(dbExpressionBinding.VariableName);
			DbExpression arg2 = dbExpressionBinding3.Variable.Property(dbExpressionBinding2.VariableName);
			TSelector argument = selector(arg, arg2);
			DbExpression projection = DbExpressionBuilder.ResolveToExpression<TSelector>(argument);
			return dbExpressionBinding3.Project(projection);
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0002A578 File Offset: 0x00028778
		public static DbSkipExpression Skip(this DbSortExpression argument, DbExpression count)
		{
			Check.NotNull<DbSortExpression>(argument, "argument");
			return argument.Input.Skip(argument.SortOrder, count);
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0002A598 File Offset: 0x00028798
		public static DbLimitExpression Take(this DbExpression argument, DbExpression count)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			Check.NotNull<DbExpression>(count, "count");
			return argument.Limit(count);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0002A5BC File Offset: 0x000287BC
		private static DbSortExpression CreateThenBy(DbSortExpression source, Func<DbExpression, DbExpression> sortKey, bool ascending, string collation, bool useCollation)
		{
			DbExpression key = sortKey(source.Input.Variable);
			DbSortClause item;
			if (useCollation)
			{
				item = (ascending ? key.ToSortClause(collation) : key.ToSortClauseDescending(collation));
			}
			else
			{
				item = (ascending ? key.ToSortClause() : key.ToSortClauseDescending());
			}
			List<DbSortClause> list = new List<DbSortClause>(source.SortOrder.Count + 1);
			list.AddRange(source.SortOrder);
			list.Add(item);
			return source.Input.Sort(list);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0002A639 File Offset: 0x00028839
		public static DbSortExpression ThenBy(this DbSortExpression source, Func<DbExpression, DbExpression> sortKey)
		{
			Check.NotNull<DbSortExpression>(source, "source");
			Check.NotNull<Func<DbExpression, DbExpression>>(sortKey, "sortKey");
			return DbExpressionBuilder.CreateThenBy(source, sortKey, true, null, false);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0002A65D File Offset: 0x0002885D
		public static DbSortExpression ThenBy(this DbSortExpression source, Func<DbExpression, DbExpression> sortKey, string collation)
		{
			Check.NotNull<DbSortExpression>(source, "source");
			Check.NotNull<Func<DbExpression, DbExpression>>(sortKey, "sortKey");
			return DbExpressionBuilder.CreateThenBy(source, sortKey, true, collation, true);
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0002A681 File Offset: 0x00028881
		public static DbSortExpression ThenByDescending(this DbSortExpression source, Func<DbExpression, DbExpression> sortKey)
		{
			Check.NotNull<DbSortExpression>(source, "source");
			Check.NotNull<Func<DbExpression, DbExpression>>(sortKey, "sortKey");
			return DbExpressionBuilder.CreateThenBy(source, sortKey, false, null, false);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0002A6A5 File Offset: 0x000288A5
		public static DbSortExpression ThenByDescending(this DbSortExpression source, Func<DbExpression, DbExpression> sortKey, string collation)
		{
			Check.NotNull<DbSortExpression>(source, "source");
			Check.NotNull<Func<DbExpression, DbExpression>>(sortKey, "sortKey");
			return DbExpressionBuilder.CreateThenBy(source, sortKey, false, collation, true);
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0002A6CC File Offset: 0x000288CC
		public static DbFilterExpression Where(this DbExpression source, Func<DbExpression, DbExpression> predicate)
		{
			Check.NotNull<DbExpression>(source, "source");
			Check.NotNull<Func<DbExpression, DbExpression>>(predicate, "predicate");
			DbExpression predicate2;
			DbExpressionBinding input = DbExpressionBuilder.ConvertToBinding<DbExpression>(source, predicate, out predicate2);
			return input.Filter(predicate2);
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0002A702 File Offset: 0x00028902
		public static DbExpression Union(this DbExpression left, DbExpression right)
		{
			return left.UnionAll(right).Distinct();
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x0002A710 File Offset: 0x00028910
		internal static AliasGenerator AliasGenerator
		{
			get
			{
				return DbExpressionBuilder._bindingAliases;
			}
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0002A718 File Offset: 0x00028918
		internal static DbNullExpression CreatePrimitiveNullExpression(PrimitiveTypeKind primitiveType)
		{
			switch (primitiveType)
			{
			case PrimitiveTypeKind.Binary:
				return DbExpressionBuilder._binaryNull;
			case PrimitiveTypeKind.Boolean:
				return DbExpressionBuilder._boolNull;
			case PrimitiveTypeKind.Byte:
				return DbExpressionBuilder._byteNull;
			case PrimitiveTypeKind.DateTime:
				return DbExpressionBuilder._dateTimeNull;
			case PrimitiveTypeKind.Decimal:
				return DbExpressionBuilder._decimalNull;
			case PrimitiveTypeKind.Double:
				return DbExpressionBuilder._doubleNull;
			case PrimitiveTypeKind.Guid:
				return DbExpressionBuilder._guidNull;
			case PrimitiveTypeKind.Single:
				return DbExpressionBuilder._singleNull;
			case PrimitiveTypeKind.SByte:
				return DbExpressionBuilder._sbyteNull;
			case PrimitiveTypeKind.Int16:
				return DbExpressionBuilder._int16Null;
			case PrimitiveTypeKind.Int32:
				return DbExpressionBuilder._int32Null;
			case PrimitiveTypeKind.Int64:
				return DbExpressionBuilder._int64Null;
			case PrimitiveTypeKind.String:
				return DbExpressionBuilder._stringNull;
			case PrimitiveTypeKind.Time:
				return DbExpressionBuilder._timeNull;
			case PrimitiveTypeKind.DateTimeOffset:
				return DbExpressionBuilder._dateTimeOffsetNull;
			case PrimitiveTypeKind.Geometry:
				return DbExpressionBuilder._geometryNull;
			case PrimitiveTypeKind.Geography:
				return DbExpressionBuilder._geographyNull;
			default:
			{
				string name = typeof(PrimitiveTypeKind).Name;
				string paramName = name;
				object p = name;
				int num = (int)primitiveType;
				throw new ArgumentOutOfRangeException(paramName, Strings.ADP_InvalidEnumerationValue(p, num.ToString(CultureInfo.InvariantCulture)));
			}
			}
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0002A804 File Offset: 0x00028A04
		internal static DbApplyExpression CreateApplyExpressionByKind(DbExpressionKind applyKind, DbExpressionBinding input, DbExpressionBinding apply)
		{
			if (applyKind == DbExpressionKind.CrossApply)
			{
				return input.CrossApply(apply);
			}
			if (applyKind != DbExpressionKind.OuterApply)
			{
				string name = typeof(DbExpressionKind).Name;
				string paramName = name;
				object p = name;
				int num = (int)applyKind;
				throw new ArgumentOutOfRangeException(paramName, Strings.ADP_InvalidEnumerationValue(p, num.ToString(CultureInfo.InvariantCulture)));
			}
			return input.OuterApply(apply);
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0002A858 File Offset: 0x00028A58
		internal static DbExpression CreateJoinExpressionByKind(DbExpressionKind joinKind, DbExpression joinCondition, DbExpressionBinding input1, DbExpressionBinding input2)
		{
			if (DbExpressionKind.CrossJoin == joinKind)
			{
				return DbExpressionBuilder.CrossJoin(new DbExpressionBinding[]
				{
					input1,
					input2
				});
			}
			if (joinKind == DbExpressionKind.FullOuterJoin)
			{
				return input1.FullOuterJoin(input2, joinCondition);
			}
			if (joinKind == DbExpressionKind.InnerJoin)
			{
				return input1.InnerJoin(input2, joinCondition);
			}
			if (joinKind != DbExpressionKind.LeftOuterJoin)
			{
				string name = typeof(DbExpressionKind).Name;
				string paramName = name;
				object p = name;
				int num = (int)joinKind;
				throw new ArgumentOutOfRangeException(paramName, Strings.ADP_InvalidEnumerationValue(p, num.ToString(CultureInfo.InvariantCulture)));
			}
			return input1.LeftOuterJoin(input2, joinCondition);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0002A8D8 File Offset: 0x00028AD8
		internal static DbElementExpression CreateElementExpressionUnwrapSingleProperty(DbExpression argument)
		{
			TypeUsage typeUsage = ArgumentValidation.ValidateElement(argument);
			IList<EdmProperty> properties = TypeHelpers.GetProperties(typeUsage);
			if (properties == null || properties.Count != 1)
			{
				throw new ArgumentException(Strings.Cqt_Element_InvalidArgumentForUnwrapSingleProperty, "argument");
			}
			typeUsage = properties[0].TypeUsage;
			return new DbElementExpression(typeUsage, argument, true);
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0002A924 File Offset: 0x00028B24
		internal static DbRelatedEntityRef CreateRelatedEntityRef(RelationshipEndMember sourceEnd, RelationshipEndMember targetEnd, DbExpression targetEntity)
		{
			return new DbRelatedEntityRef(sourceEnd, targetEnd, targetEntity);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0002A930 File Offset: 0x00028B30
		internal static DbNewInstanceExpression CreateNewEntityWithRelationshipsExpression(EntityType entityType, IList<DbExpression> attributeValues, IList<DbRelatedEntityRef> relationships)
		{
			DbExpressionList attributeValues2;
			ReadOnlyCollection<DbRelatedEntityRef> relationships2;
			TypeUsage resultType = ArgumentValidation.ValidateNewEntityWithRelationships(entityType, attributeValues, relationships, out attributeValues2, out relationships2);
			return new DbNewInstanceExpression(resultType, attributeValues2, relationships2);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0002A954 File Offset: 0x00028B54
		internal static DbRelationshipNavigationExpression NavigateAllowingAllRelationshipsInSameTypeHierarchy(this DbExpression navigateFrom, RelationshipEndMember fromEnd, RelationshipEndMember toEnd)
		{
			RelationshipType relType;
			TypeUsage resultType = ArgumentValidation.ValidateNavigate(navigateFrom, fromEnd, toEnd, out relType, true);
			return new DbRelationshipNavigationExpression(resultType, relType, fromEnd, toEnd, navigateFrom);
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0002A977 File Offset: 0x00028B77
		internal static DbPropertyExpression CreatePropertyExpressionFromMember(DbExpression instance, EdmMember member)
		{
			return DbExpressionBuilder.PropertyFromMember(instance, member, "member");
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0002A985 File Offset: 0x00028B85
		private static TypeUsage CreateCollectionResultType(EdmType type)
		{
			return TypeUsage.Create(TypeHelpers.CreateCollectionType(TypeUsage.Create(type)));
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0002A997 File Offset: 0x00028B97
		private static TypeUsage CreateCollectionResultType(TypeUsage elementType)
		{
			return TypeUsage.Create(TypeHelpers.CreateCollectionType(elementType));
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0002A9A4 File Offset: 0x00028BA4
		private static bool IsConstantNegativeInteger(DbExpression expression)
		{
			return expression.ExpressionKind == DbExpressionKind.Constant && TypeSemantics.IsIntegerNumericType(expression.ResultType) && Convert.ToInt64(((DbConstantExpression)expression).Value, CultureInfo.InvariantCulture) < 0L;
		}

		// Token: 0x0400025C RID: 604
		private static readonly TypeUsage _booleanType = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Boolean);

		// Token: 0x0400025D RID: 605
		private static readonly AliasGenerator _bindingAliases = new AliasGenerator("Var_", 0);

		// Token: 0x0400025E RID: 606
		private static readonly DbNullExpression _binaryNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Binary).Null();

		// Token: 0x0400025F RID: 607
		private static readonly DbNullExpression _boolNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Boolean).Null();

		// Token: 0x04000260 RID: 608
		private static readonly DbNullExpression _byteNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Byte).Null();

		// Token: 0x04000261 RID: 609
		private static readonly DbNullExpression _dateTimeNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.DateTime).Null();

		// Token: 0x04000262 RID: 610
		private static readonly DbNullExpression _dateTimeOffsetNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.DateTimeOffset).Null();

		// Token: 0x04000263 RID: 611
		private static readonly DbNullExpression _decimalNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Decimal).Null();

		// Token: 0x04000264 RID: 612
		private static readonly DbNullExpression _doubleNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Double).Null();

		// Token: 0x04000265 RID: 613
		private static readonly DbNullExpression _geographyNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Geography).Null();

		// Token: 0x04000266 RID: 614
		private static readonly DbNullExpression _geometryNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Geometry).Null();

		// Token: 0x04000267 RID: 615
		private static readonly DbNullExpression _guidNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Guid).Null();

		// Token: 0x04000268 RID: 616
		private static readonly DbNullExpression _int16Null = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Int16).Null();

		// Token: 0x04000269 RID: 617
		private static readonly DbNullExpression _int32Null = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Int32).Null();

		// Token: 0x0400026A RID: 618
		private static readonly DbNullExpression _int64Null = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Int64).Null();

		// Token: 0x0400026B RID: 619
		private static readonly DbNullExpression _sbyteNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.SByte).Null();

		// Token: 0x0400026C RID: 620
		private static readonly DbNullExpression _singleNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Single).Null();

		// Token: 0x0400026D RID: 621
		private static readonly DbNullExpression _stringNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.String).Null();

		// Token: 0x0400026E RID: 622
		private static readonly DbNullExpression _timeNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Time).Null();

		// Token: 0x0400026F RID: 623
		private static readonly DbConstantExpression _boolTrue = DbExpressionBuilder.Constant(true);

		// Token: 0x04000270 RID: 624
		private static readonly DbConstantExpression _boolFalse = DbExpressionBuilder.Constant(false);
	}
}
