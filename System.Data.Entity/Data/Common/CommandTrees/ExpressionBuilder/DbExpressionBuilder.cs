using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.CommandTrees.ExpressionBuilder.Internal;
using System.Data.Common.CommandTrees.Internal;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Data.Common.CommandTrees.ExpressionBuilder
{
	// Token: 0x02000427 RID: 1063
	public static class DbExpressionBuilder
	{
		// Token: 0x0600378B RID: 14219 RVA: 0x000D3F6D File Offset: 0x000D216D
		public static KeyValuePair<string, DbExpression> As(this DbExpression value, string alias)
		{
			return new KeyValuePair<string, DbExpression>(alias, value);
		}

		// Token: 0x0600378C RID: 14220 RVA: 0x000D3F76 File Offset: 0x000D2176
		public static KeyValuePair<string, DbAggregate> As(this DbAggregate value, string alias)
		{
			return new KeyValuePair<string, DbAggregate>(alias, value);
		}

		// Token: 0x0600378D RID: 14221 RVA: 0x000D3F7F File Offset: 0x000D217F
		public static DbExpressionBinding Bind(this DbExpression input)
		{
			return input.BindAs(DbExpressionBuilder._bindingAliases.Next());
		}

		// Token: 0x0600378E RID: 14222 RVA: 0x000D3F94 File Offset: 0x000D2194
		public static DbExpressionBinding BindAs(this DbExpression input, string varName)
		{
			TypeUsage type = ArgumentValidation.ValidateBindAs(input, varName);
			DbVariableReferenceExpression varRef = new DbVariableReferenceExpression(type, varName);
			return new DbExpressionBinding(input, varRef);
		}

		// Token: 0x0600378F RID: 14223 RVA: 0x000D3FB8 File Offset: 0x000D21B8
		public static DbGroupExpressionBinding GroupBind(this DbExpression input)
		{
			string text = DbExpressionBuilder._bindingAliases.Next();
			return input.GroupBindAs(text, string.Format(CultureInfo.InvariantCulture, "Group{0}", new object[]
			{
				text
			}));
		}

		// Token: 0x06003790 RID: 14224 RVA: 0x000D3FF0 File Offset: 0x000D21F0
		public static DbGroupExpressionBinding GroupBindAs(this DbExpression input, string varName, string groupVarName)
		{
			TypeUsage type = ArgumentValidation.ValidateGroupBindAs(input, varName, groupVarName);
			DbVariableReferenceExpression inputRef = new DbVariableReferenceExpression(type, varName);
			DbVariableReferenceExpression groupRef = new DbVariableReferenceExpression(type, groupVarName);
			return new DbGroupExpressionBinding(input, inputRef, groupRef);
		}

		// Token: 0x06003791 RID: 14225 RVA: 0x000D401E File Offset: 0x000D221E
		public static DbFunctionAggregate Aggregate(this EdmFunction function, DbExpression argument)
		{
			return DbExpressionBuilder.CreateFunctionAggregate(function, argument, false);
		}

		// Token: 0x06003792 RID: 14226 RVA: 0x000D4028 File Offset: 0x000D2228
		public static DbFunctionAggregate AggregateDistinct(this EdmFunction function, DbExpression argument)
		{
			return DbExpressionBuilder.CreateFunctionAggregate(function, argument, true);
		}

		// Token: 0x06003793 RID: 14227 RVA: 0x000D4034 File Offset: 0x000D2234
		private static DbFunctionAggregate CreateFunctionAggregate(EdmFunction function, DbExpression argument, bool isDistinct)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(argument, "argument");
			DbExpressionList arguments = ArgumentValidation.ValidateFunctionAggregate(function, new DbExpression[]
			{
				argument
			});
			TypeUsage typeUsage = function.ReturnParameter.TypeUsage;
			return new DbFunctionAggregate(typeUsage, arguments, function, isDistinct);
		}

		// Token: 0x06003794 RID: 14228 RVA: 0x000D4074 File Offset: 0x000D2274
		internal static DbGroupAggregate GroupAggregate(DbExpression argument)
		{
			DbExpressionList arguments = ArgumentValidation.ValidateGroupAggregate(argument);
			TypeUsage resultType = TypeHelpers.CreateCollectionTypeUsage(argument.ResultType);
			return new DbGroupAggregate(resultType, arguments);
		}

		// Token: 0x06003795 RID: 14229 RVA: 0x000D409B File Offset: 0x000D229B
		public static DbLambda Lambda(DbExpression body, IEnumerable<DbVariableReferenceExpression> variables)
		{
			return DbExpressionBuilder.CreateLambda(body, variables);
		}

		// Token: 0x06003796 RID: 14230 RVA: 0x000D409B File Offset: 0x000D229B
		public static DbLambda Lambda(DbExpression body, params DbVariableReferenceExpression[] variables)
		{
			return DbExpressionBuilder.CreateLambda(body, variables);
		}

		// Token: 0x06003797 RID: 14231 RVA: 0x000D40A4 File Offset: 0x000D22A4
		private static DbLambda CreateLambda(DbExpression body, IEnumerable<DbVariableReferenceExpression> variables)
		{
			ReadOnlyCollection<DbVariableReferenceExpression> variables2 = ArgumentValidation.ValidateLambda(variables, body);
			return new DbLambda(variables2, body);
		}

		// Token: 0x06003798 RID: 14232 RVA: 0x000D40C0 File Offset: 0x000D22C0
		public static DbSortClause ToSortClause(this DbExpression key)
		{
			ArgumentValidation.ValidateSortClause(key);
			return new DbSortClause(key, true, string.Empty);
		}

		// Token: 0x06003799 RID: 14233 RVA: 0x000D40D4 File Offset: 0x000D22D4
		public static DbSortClause ToSortClauseDescending(this DbExpression key)
		{
			ArgumentValidation.ValidateSortClause(key);
			return new DbSortClause(key, false, string.Empty);
		}

		// Token: 0x0600379A RID: 14234 RVA: 0x000D40E8 File Offset: 0x000D22E8
		public static DbSortClause ToSortClause(this DbExpression key, string collation)
		{
			ArgumentValidation.ValidateSortClause(key, collation);
			return new DbSortClause(key, true, collation);
		}

		// Token: 0x0600379B RID: 14235 RVA: 0x000D40F9 File Offset: 0x000D22F9
		public static DbSortClause ToSortClauseDescending(this DbExpression key, string collation)
		{
			ArgumentValidation.ValidateSortClause(key, collation);
			return new DbSortClause(key, false, collation);
		}

		// Token: 0x0600379C RID: 14236 RVA: 0x000D410C File Offset: 0x000D230C
		public static DbQuantifierExpression All(this DbExpressionBinding input, DbExpression predicate)
		{
			TypeUsage booleanResultType = ArgumentValidation.ValidateQuantifier(input, predicate);
			return new DbQuantifierExpression(DbExpressionKind.All, booleanResultType, input, predicate);
		}

		// Token: 0x0600379D RID: 14237 RVA: 0x000D412C File Offset: 0x000D232C
		public static DbQuantifierExpression Any(this DbExpressionBinding input, DbExpression predicate)
		{
			TypeUsage booleanResultType = ArgumentValidation.ValidateQuantifier(input, predicate);
			return new DbQuantifierExpression(DbExpressionKind.Any, booleanResultType, input, predicate);
		}

		// Token: 0x0600379E RID: 14238 RVA: 0x000D414C File Offset: 0x000D234C
		public static DbApplyExpression CrossApply(this DbExpressionBinding input, DbExpressionBinding apply)
		{
			TypeUsage resultRowCollectionTypeUsage = ArgumentValidation.ValidateApply(input, apply);
			return new DbApplyExpression(DbExpressionKind.CrossApply, resultRowCollectionTypeUsage, input, apply);
		}

		// Token: 0x0600379F RID: 14239 RVA: 0x000D416C File Offset: 0x000D236C
		public static DbApplyExpression OuterApply(this DbExpressionBinding input, DbExpressionBinding apply)
		{
			TypeUsage resultRowCollectionTypeUsage = ArgumentValidation.ValidateApply(input, apply);
			return new DbApplyExpression(DbExpressionKind.OuterApply, resultRowCollectionTypeUsage, input, apply);
		}

		// Token: 0x060037A0 RID: 14240 RVA: 0x000D418C File Offset: 0x000D238C
		public static DbCrossJoinExpression CrossJoin(IEnumerable<DbExpressionBinding> inputs)
		{
			TypeUsage collectionOfRowResultType;
			ReadOnlyCollection<DbExpressionBinding> inputs2 = ArgumentValidation.ValidateCrossJoin(inputs, out collectionOfRowResultType);
			return new DbCrossJoinExpression(collectionOfRowResultType, inputs2);
		}

		// Token: 0x060037A1 RID: 14241 RVA: 0x000D41AC File Offset: 0x000D23AC
		public static DbJoinExpression InnerJoin(this DbExpressionBinding left, DbExpressionBinding right, DbExpression joinCondition)
		{
			TypeUsage collectionOfRowResultType = ArgumentValidation.ValidateJoin(left, right, joinCondition);
			return new DbJoinExpression(DbExpressionKind.InnerJoin, collectionOfRowResultType, left, right, joinCondition);
		}

		// Token: 0x060037A2 RID: 14242 RVA: 0x000D41D0 File Offset: 0x000D23D0
		public static DbJoinExpression LeftOuterJoin(this DbExpressionBinding left, DbExpressionBinding right, DbExpression joinCondition)
		{
			TypeUsage collectionOfRowResultType = ArgumentValidation.ValidateJoin(left, right, joinCondition);
			return new DbJoinExpression(DbExpressionKind.LeftOuterJoin, collectionOfRowResultType, left, right, joinCondition);
		}

		// Token: 0x060037A3 RID: 14243 RVA: 0x000D41F4 File Offset: 0x000D23F4
		public static DbJoinExpression FullOuterJoin(this DbExpressionBinding left, DbExpressionBinding right, DbExpression joinCondition)
		{
			TypeUsage collectionOfRowResultType = ArgumentValidation.ValidateJoin(left, right, joinCondition);
			return new DbJoinExpression(DbExpressionKind.FullOuterJoin, collectionOfRowResultType, left, right, joinCondition);
		}

		// Token: 0x060037A4 RID: 14244 RVA: 0x000D4218 File Offset: 0x000D2418
		public static DbFilterExpression Filter(this DbExpressionBinding input, DbExpression predicate)
		{
			TypeUsage resultType = ArgumentValidation.ValidateFilter(input, predicate);
			return new DbFilterExpression(resultType, input, predicate);
		}

		// Token: 0x060037A5 RID: 14245 RVA: 0x000D4238 File Offset: 0x000D2438
		public static DbGroupByExpression GroupBy(this DbGroupExpressionBinding input, IEnumerable<KeyValuePair<string, DbExpression>> keys, IEnumerable<KeyValuePair<string, DbAggregate>> aggregates)
		{
			DbExpressionList groupKeys;
			ReadOnlyCollection<DbAggregate> aggregates2;
			TypeUsage collectionOfRowResultType = ArgumentValidation.ValidateGroupBy(input, keys, aggregates, out groupKeys, out aggregates2);
			return new DbGroupByExpression(collectionOfRowResultType, input, groupKeys, aggregates2);
		}

		// Token: 0x060037A6 RID: 14246 RVA: 0x000D425C File Offset: 0x000D245C
		public static DbProjectExpression Project(this DbExpressionBinding input, DbExpression projection)
		{
			TypeUsage resultType = ArgumentValidation.ValidateProject(input, projection);
			return new DbProjectExpression(resultType, input, projection);
		}

		// Token: 0x060037A7 RID: 14247 RVA: 0x000D427C File Offset: 0x000D247C
		public static DbSkipExpression Skip(this DbExpressionBinding input, IEnumerable<DbSortClause> sortOrder, DbExpression count)
		{
			ReadOnlyCollection<DbSortClause> sortOrder2 = ArgumentValidation.ValidateSkip(input, sortOrder, count);
			return new DbSkipExpression(input.Expression.ResultType, input, sortOrder2, count);
		}

		// Token: 0x060037A8 RID: 14248 RVA: 0x000D42A8 File Offset: 0x000D24A8
		public static DbSortExpression Sort(this DbExpressionBinding input, IEnumerable<DbSortClause> sortOrder)
		{
			ReadOnlyCollection<DbSortClause> sortOrder2 = ArgumentValidation.ValidateSort(input, sortOrder);
			return new DbSortExpression(input.Expression.ResultType, input, sortOrder2);
		}

		// Token: 0x060037A9 RID: 14249 RVA: 0x000D42CF File Offset: 0x000D24CF
		public static DbNullExpression Null(this TypeUsage nullType)
		{
			ArgumentValidation.ValidateNull(nullType);
			return new DbNullExpression(nullType);
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x060037AA RID: 14250 RVA: 0x000D42DD File Offset: 0x000D24DD
		public static DbConstantExpression True
		{
			get
			{
				return DbExpressionBuilder._boolTrue;
			}
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x060037AB RID: 14251 RVA: 0x000D42E4 File Offset: 0x000D24E4
		public static DbConstantExpression False
		{
			get
			{
				return DbExpressionBuilder._boolFalse;
			}
		}

		// Token: 0x060037AC RID: 14252 RVA: 0x000D42EC File Offset: 0x000D24EC
		public static DbConstantExpression Constant(object value)
		{
			TypeUsage resultType = ArgumentValidation.ValidateConstant(value);
			return new DbConstantExpression(resultType, value);
		}

		// Token: 0x060037AD RID: 14253 RVA: 0x000D4307 File Offset: 0x000D2507
		public static DbConstantExpression Constant(this TypeUsage constantType, object value)
		{
			ArgumentValidation.ValidateConstant(constantType, value);
			return new DbConstantExpression(constantType, value);
		}

		// Token: 0x060037AE RID: 14254 RVA: 0x000D4317 File Offset: 0x000D2517
		public static DbParameterReferenceExpression Parameter(this TypeUsage type, string name)
		{
			ArgumentValidation.ValidateParameter(type, name);
			return new DbParameterReferenceExpression(type, name);
		}

		// Token: 0x060037AF RID: 14255 RVA: 0x000D4327 File Offset: 0x000D2527
		public static DbVariableReferenceExpression Variable(this TypeUsage type, string name)
		{
			ArgumentValidation.ValidateVariable(type, name);
			return new DbVariableReferenceExpression(type, name);
		}

		// Token: 0x060037B0 RID: 14256 RVA: 0x000D4338 File Offset: 0x000D2538
		public static DbScanExpression Scan(this EntitySetBase targetSet)
		{
			TypeUsage collectionOfEntityType = ArgumentValidation.ValidateScan(targetSet);
			return new DbScanExpression(collectionOfEntityType, targetSet);
		}

		// Token: 0x060037B1 RID: 14257 RVA: 0x000D4354 File Offset: 0x000D2554
		public static DbAndExpression And(this DbExpression left, DbExpression right)
		{
			TypeUsage booleanResultType = ArgumentValidation.ValidateAnd(left, right);
			return new DbAndExpression(booleanResultType, left, right);
		}

		// Token: 0x060037B2 RID: 14258 RVA: 0x000D4374 File Offset: 0x000D2574
		public static DbOrExpression Or(this DbExpression left, DbExpression right)
		{
			TypeUsage booleanResultType = ArgumentValidation.ValidateOr(left, right);
			return new DbOrExpression(booleanResultType, left, right);
		}

		// Token: 0x060037B3 RID: 14259 RVA: 0x000D4394 File Offset: 0x000D2594
		public static DbNotExpression Not(this DbExpression argument)
		{
			TypeUsage booleanResultType = ArgumentValidation.ValidateNot(argument);
			return new DbNotExpression(booleanResultType, argument);
		}

		// Token: 0x060037B4 RID: 14260 RVA: 0x000D43B0 File Offset: 0x000D25B0
		private static DbArithmeticExpression CreateArithmetic(DbExpressionKind kind, DbExpression left, DbExpression right)
		{
			TypeUsage numericResultType;
			DbExpressionList args = ArgumentValidation.ValidateArithmetic(left, right, out numericResultType);
			return new DbArithmeticExpression(kind, numericResultType, args);
		}

		// Token: 0x060037B5 RID: 14261 RVA: 0x000D43CF File Offset: 0x000D25CF
		public static DbArithmeticExpression Divide(this DbExpression left, DbExpression right)
		{
			return DbExpressionBuilder.CreateArithmetic(DbExpressionKind.Divide, left, right);
		}

		// Token: 0x060037B6 RID: 14262 RVA: 0x000D43DA File Offset: 0x000D25DA
		public static DbArithmeticExpression Minus(this DbExpression left, DbExpression right)
		{
			return DbExpressionBuilder.CreateArithmetic(DbExpressionKind.Minus, left, right);
		}

		// Token: 0x060037B7 RID: 14263 RVA: 0x000D43E5 File Offset: 0x000D25E5
		public static DbArithmeticExpression Modulo(this DbExpression left, DbExpression right)
		{
			return DbExpressionBuilder.CreateArithmetic(DbExpressionKind.Modulo, left, right);
		}

		// Token: 0x060037B8 RID: 14264 RVA: 0x000D43F0 File Offset: 0x000D25F0
		public static DbArithmeticExpression Multiply(this DbExpression left, DbExpression right)
		{
			return DbExpressionBuilder.CreateArithmetic(DbExpressionKind.Multiply, left, right);
		}

		// Token: 0x060037B9 RID: 14265 RVA: 0x000D43FB File Offset: 0x000D25FB
		public static DbArithmeticExpression Plus(this DbExpression left, DbExpression right)
		{
			return DbExpressionBuilder.CreateArithmetic(DbExpressionKind.Plus, left, right);
		}

		// Token: 0x060037BA RID: 14266 RVA: 0x000D4408 File Offset: 0x000D2608
		public static DbArithmeticExpression UnaryMinus(this DbExpression argument)
		{
			TypeUsage numericResultType;
			DbExpressionList args = ArgumentValidation.ValidateArithmetic(argument, out numericResultType);
			return new DbArithmeticExpression(DbExpressionKind.UnaryMinus, numericResultType, args);
		}

		// Token: 0x060037BB RID: 14267 RVA: 0x000D4427 File Offset: 0x000D2627
		public static DbArithmeticExpression Negate(this DbExpression argument)
		{
			return argument.UnaryMinus();
		}

		// Token: 0x060037BC RID: 14268 RVA: 0x000D4430 File Offset: 0x000D2630
		private static DbComparisonExpression CreateComparison(DbExpressionKind kind, DbExpression left, DbExpression right)
		{
			TypeUsage booleanResultType = ArgumentValidation.ValidateComparison(kind, left, right);
			return new DbComparisonExpression(kind, booleanResultType, left, right);
		}

		// Token: 0x060037BD RID: 14269 RVA: 0x000D444F File Offset: 0x000D264F
		public static DbComparisonExpression Equal(this DbExpression left, DbExpression right)
		{
			return DbExpressionBuilder.CreateComparison(DbExpressionKind.Equals, left, right);
		}

		// Token: 0x060037BE RID: 14270 RVA: 0x000D445A File Offset: 0x000D265A
		public static DbComparisonExpression NotEqual(this DbExpression left, DbExpression right)
		{
			return DbExpressionBuilder.CreateComparison(DbExpressionKind.NotEquals, left, right);
		}

		// Token: 0x060037BF RID: 14271 RVA: 0x000D4465 File Offset: 0x000D2665
		public static DbComparisonExpression GreaterThan(this DbExpression left, DbExpression right)
		{
			return DbExpressionBuilder.CreateComparison(DbExpressionKind.GreaterThan, left, right);
		}

		// Token: 0x060037C0 RID: 14272 RVA: 0x000D4470 File Offset: 0x000D2670
		public static DbComparisonExpression LessThan(this DbExpression left, DbExpression right)
		{
			return DbExpressionBuilder.CreateComparison(DbExpressionKind.LessThan, left, right);
		}

		// Token: 0x060037C1 RID: 14273 RVA: 0x000D447B File Offset: 0x000D267B
		public static DbComparisonExpression GreaterThanOrEqual(this DbExpression left, DbExpression right)
		{
			return DbExpressionBuilder.CreateComparison(DbExpressionKind.GreaterThanOrEquals, left, right);
		}

		// Token: 0x060037C2 RID: 14274 RVA: 0x000D4486 File Offset: 0x000D2686
		public static DbComparisonExpression LessThanOrEqual(this DbExpression left, DbExpression right)
		{
			return DbExpressionBuilder.CreateComparison(DbExpressionKind.LessThanOrEquals, left, right);
		}

		// Token: 0x060037C3 RID: 14275 RVA: 0x000D4494 File Offset: 0x000D2694
		public static DbIsNullExpression IsNull(this DbExpression argument)
		{
			TypeUsage booleanResultType = ArgumentValidation.ValidateIsNull(argument);
			return new DbIsNullExpression(booleanResultType, argument, false);
		}

		// Token: 0x060037C4 RID: 14276 RVA: 0x000D44B0 File Offset: 0x000D26B0
		public static DbLikeExpression Like(this DbExpression argument, DbExpression pattern)
		{
			TypeUsage booleanResultType = ArgumentValidation.ValidateLike(argument, pattern);
			DbExpression escape = pattern.ResultType.Null();
			return new DbLikeExpression(booleanResultType, argument, pattern, escape);
		}

		// Token: 0x060037C5 RID: 14277 RVA: 0x000D44DC File Offset: 0x000D26DC
		public static DbLikeExpression Like(this DbExpression argument, DbExpression pattern, DbExpression escape)
		{
			TypeUsage booleanResultType = ArgumentValidation.ValidateLike(argument, pattern, escape);
			return new DbLikeExpression(booleanResultType, argument, pattern, escape);
		}

		// Token: 0x060037C6 RID: 14278 RVA: 0x000D44FB File Offset: 0x000D26FB
		public static DbCastExpression CastTo(this DbExpression argument, TypeUsage toType)
		{
			ArgumentValidation.ValidateCastTo(argument, toType);
			return new DbCastExpression(toType, argument);
		}

		// Token: 0x060037C7 RID: 14279 RVA: 0x000D450B File Offset: 0x000D270B
		public static DbTreatExpression TreatAs(this DbExpression argument, TypeUsage treatType)
		{
			ArgumentValidation.ValidateTreatAs(argument, treatType);
			return new DbTreatExpression(treatType, argument);
		}

		// Token: 0x060037C8 RID: 14280 RVA: 0x000D451C File Offset: 0x000D271C
		public static DbOfTypeExpression OfType(this DbExpression argument, TypeUsage type)
		{
			TypeUsage collectionResultType = ArgumentValidation.ValidateOfType(argument, type);
			return new DbOfTypeExpression(DbExpressionKind.OfType, collectionResultType, argument, type);
		}

		// Token: 0x060037C9 RID: 14281 RVA: 0x000D453C File Offset: 0x000D273C
		public static DbOfTypeExpression OfTypeOnly(this DbExpression argument, TypeUsage type)
		{
			TypeUsage collectionResultType = ArgumentValidation.ValidateOfType(argument, type);
			return new DbOfTypeExpression(DbExpressionKind.OfTypeOnly, collectionResultType, argument, type);
		}

		// Token: 0x060037CA RID: 14282 RVA: 0x000D455C File Offset: 0x000D275C
		public static DbIsOfExpression IsOf(this DbExpression argument, TypeUsage type)
		{
			TypeUsage booleanResultType = ArgumentValidation.ValidateIsOf(argument, type);
			return new DbIsOfExpression(DbExpressionKind.IsOf, booleanResultType, argument, type);
		}

		// Token: 0x060037CB RID: 14283 RVA: 0x000D457C File Offset: 0x000D277C
		public static DbIsOfExpression IsOfOnly(this DbExpression argument, TypeUsage type)
		{
			TypeUsage booleanResultType = ArgumentValidation.ValidateIsOf(argument, type);
			return new DbIsOfExpression(DbExpressionKind.IsOfOnly, booleanResultType, argument, type);
		}

		// Token: 0x060037CC RID: 14284 RVA: 0x000D459C File Offset: 0x000D279C
		public static DbDerefExpression Deref(this DbExpression argument)
		{
			TypeUsage entityResultType = ArgumentValidation.ValidateDeref(argument);
			return new DbDerefExpression(entityResultType, argument);
		}

		// Token: 0x060037CD RID: 14285 RVA: 0x000D45B8 File Offset: 0x000D27B8
		public static DbEntityRefExpression GetEntityRef(this DbExpression argument)
		{
			TypeUsage refResultType = ArgumentValidation.ValidateGetEntityRef(argument);
			return new DbEntityRefExpression(refResultType, argument);
		}

		// Token: 0x060037CE RID: 14286 RVA: 0x000D45D3 File Offset: 0x000D27D3
		public static DbRefExpression CreateRef(this EntitySet entitySet, IEnumerable<DbExpression> keyValues)
		{
			return DbExpressionBuilder.CreateRefExpression(entitySet, keyValues);
		}

		// Token: 0x060037CF RID: 14287 RVA: 0x000D45D3 File Offset: 0x000D27D3
		public static DbRefExpression CreateRef(this EntitySet entitySet, params DbExpression[] keyValues)
		{
			return DbExpressionBuilder.CreateRefExpression(entitySet, keyValues);
		}

		// Token: 0x060037D0 RID: 14288 RVA: 0x000D45DC File Offset: 0x000D27DC
		public static DbRefExpression CreateRef(this EntitySet entitySet, EntityType entityType, IEnumerable<DbExpression> keyValues)
		{
			return DbExpressionBuilder.CreateRefExpression(entitySet, entityType, keyValues);
		}

		// Token: 0x060037D1 RID: 14289 RVA: 0x000D45DC File Offset: 0x000D27DC
		public static DbRefExpression CreateRef(this EntitySet entitySet, EntityType entityType, params DbExpression[] keyValues)
		{
			return DbExpressionBuilder.CreateRefExpression(entitySet, entityType, keyValues);
		}

		// Token: 0x060037D2 RID: 14290 RVA: 0x000D45E8 File Offset: 0x000D27E8
		private static DbRefExpression CreateRefExpression(EntitySet entitySet, IEnumerable<DbExpression> keyValues)
		{
			DbExpression refKeys;
			TypeUsage refResultType = ArgumentValidation.ValidateCreateRef(entitySet, keyValues, out refKeys);
			return new DbRefExpression(refResultType, entitySet, refKeys);
		}

		// Token: 0x060037D3 RID: 14291 RVA: 0x000D4608 File Offset: 0x000D2808
		private static DbRefExpression CreateRefExpression(EntitySet entitySet, EntityType entityType, IEnumerable<DbExpression> keyValues)
		{
			DbExpression refKeys;
			TypeUsage refResultType = ArgumentValidation.ValidateCreateRef(entitySet, entityType, keyValues, out refKeys);
			return new DbRefExpression(refResultType, entitySet, refKeys);
		}

		// Token: 0x060037D4 RID: 14292 RVA: 0x000D4628 File Offset: 0x000D2828
		public static DbRefExpression RefFromKey(this EntitySet entitySet, DbExpression keyRow)
		{
			TypeUsage refResultType = ArgumentValidation.ValidateRefFromKey(entitySet, keyRow);
			return new DbRefExpression(refResultType, entitySet, keyRow);
		}

		// Token: 0x060037D5 RID: 14293 RVA: 0x000D4648 File Offset: 0x000D2848
		public static DbRefExpression RefFromKey(this EntitySet entitySet, DbExpression keyRow, EntityType entityType)
		{
			TypeUsage refResultType = ArgumentValidation.ValidateRefFromKey(entitySet, keyRow, entityType);
			return new DbRefExpression(refResultType, entitySet, keyRow);
		}

		// Token: 0x060037D6 RID: 14294 RVA: 0x000D4668 File Offset: 0x000D2868
		public static DbRefKeyExpression GetRefKey(this DbExpression argument)
		{
			TypeUsage rowResultType = ArgumentValidation.ValidateGetRefKey(argument);
			return new DbRefKeyExpression(rowResultType, argument);
		}

		// Token: 0x060037D7 RID: 14295 RVA: 0x000D4684 File Offset: 0x000D2884
		public static DbRelationshipNavigationExpression Navigate(this DbExpression navigateFrom, RelationshipEndMember fromEnd, RelationshipEndMember toEnd)
		{
			RelationshipType relType;
			TypeUsage resultType = ArgumentValidation.ValidateNavigate(navigateFrom, fromEnd, toEnd, out relType, false);
			return new DbRelationshipNavigationExpression(resultType, relType, fromEnd, toEnd, navigateFrom);
		}

		// Token: 0x060037D8 RID: 14296 RVA: 0x000D46A8 File Offset: 0x000D28A8
		public static DbRelationshipNavigationExpression Navigate(this RelationshipType type, string fromEndName, string toEndName, DbExpression navigateFrom)
		{
			RelationshipEndMember fromEnd;
			RelationshipEndMember toEnd;
			TypeUsage resultType = ArgumentValidation.ValidateNavigate(navigateFrom, type, fromEndName, toEndName, out fromEnd, out toEnd);
			return new DbRelationshipNavigationExpression(resultType, type, fromEnd, toEnd, navigateFrom);
		}

		// Token: 0x060037D9 RID: 14297 RVA: 0x000D46D0 File Offset: 0x000D28D0
		public static DbDistinctExpression Distinct(this DbExpression argument)
		{
			TypeUsage resultType = ArgumentValidation.ValidateDistinct(argument);
			return new DbDistinctExpression(resultType, argument);
		}

		// Token: 0x060037DA RID: 14298 RVA: 0x000D46EC File Offset: 0x000D28EC
		public static DbElementExpression Element(this DbExpression argument)
		{
			TypeUsage resultType = ArgumentValidation.ValidateElement(argument);
			return new DbElementExpression(resultType, argument);
		}

		// Token: 0x060037DB RID: 14299 RVA: 0x000D4708 File Offset: 0x000D2908
		public static DbIsEmptyExpression IsEmpty(this DbExpression argument)
		{
			TypeUsage booleanResultType = ArgumentValidation.ValidateIsEmpty(argument);
			return new DbIsEmptyExpression(booleanResultType, argument);
		}

		// Token: 0x060037DC RID: 14300 RVA: 0x000D4724 File Offset: 0x000D2924
		public static DbExceptExpression Except(this DbExpression left, DbExpression right)
		{
			TypeUsage resultType = ArgumentValidation.ValidateExcept(left, right);
			return new DbExceptExpression(resultType, left, right);
		}

		// Token: 0x060037DD RID: 14301 RVA: 0x000D4744 File Offset: 0x000D2944
		public static DbIntersectExpression Intersect(this DbExpression left, DbExpression right)
		{
			TypeUsage resultType = ArgumentValidation.ValidateIntersect(left, right);
			return new DbIntersectExpression(resultType, left, right);
		}

		// Token: 0x060037DE RID: 14302 RVA: 0x000D4764 File Offset: 0x000D2964
		public static DbUnionAllExpression UnionAll(this DbExpression left, DbExpression right)
		{
			TypeUsage resultType = ArgumentValidation.ValidateUnionAll(left, right);
			return new DbUnionAllExpression(resultType, left, right);
		}

		// Token: 0x060037DF RID: 14303 RVA: 0x000D4784 File Offset: 0x000D2984
		public static DbLimitExpression Limit(this DbExpression argument, DbExpression count)
		{
			TypeUsage resultType = ArgumentValidation.ValidateLimit(argument, count);
			return new DbLimitExpression(resultType, argument, count, false);
		}

		// Token: 0x060037E0 RID: 14304 RVA: 0x000D47A4 File Offset: 0x000D29A4
		public static DbCaseExpression Case(IEnumerable<DbExpression> whenExpressions, IEnumerable<DbExpression> thenExpressions, DbExpression elseExpression)
		{
			DbExpressionList whens;
			DbExpressionList thens;
			TypeUsage commonResultType = ArgumentValidation.ValidateCase(whenExpressions, thenExpressions, elseExpression, out whens, out thens);
			return new DbCaseExpression(commonResultType, whens, thens, elseExpression);
		}

		// Token: 0x060037E1 RID: 14305 RVA: 0x000D47C7 File Offset: 0x000D29C7
		public static DbFunctionExpression Invoke(this EdmFunction function, IEnumerable<DbExpression> arguments)
		{
			return DbExpressionBuilder.InvokeFunction(function, arguments);
		}

		// Token: 0x060037E2 RID: 14306 RVA: 0x000D47C7 File Offset: 0x000D29C7
		public static DbFunctionExpression Invoke(this EdmFunction function, params DbExpression[] arguments)
		{
			return DbExpressionBuilder.InvokeFunction(function, arguments);
		}

		// Token: 0x060037E3 RID: 14307 RVA: 0x000D47D0 File Offset: 0x000D29D0
		private static DbFunctionExpression InvokeFunction(EdmFunction function, IEnumerable<DbExpression> arguments)
		{
			DbExpressionList arguments2;
			TypeUsage resultType = ArgumentValidation.ValidateFunction(function, arguments, out arguments2);
			return new DbFunctionExpression(resultType, function, arguments2);
		}

		// Token: 0x060037E4 RID: 14308 RVA: 0x000D47EF File Offset: 0x000D29EF
		public static DbLambdaExpression Invoke(this DbLambda lambda, IEnumerable<DbExpression> arguments)
		{
			return DbExpressionBuilder.InvokeLambda(lambda, arguments);
		}

		// Token: 0x060037E5 RID: 14309 RVA: 0x000D47EF File Offset: 0x000D29EF
		public static DbLambdaExpression Invoke(this DbLambda lambda, params DbExpression[] arguments)
		{
			return DbExpressionBuilder.InvokeLambda(lambda, arguments);
		}

		// Token: 0x060037E6 RID: 14310 RVA: 0x000D47F8 File Offset: 0x000D29F8
		private static DbLambdaExpression InvokeLambda(DbLambda lambda, IEnumerable<DbExpression> arguments)
		{
			DbExpressionList args;
			TypeUsage resultType = ArgumentValidation.ValidateInvoke(lambda, arguments, out args);
			return new DbLambdaExpression(resultType, lambda, args);
		}

		// Token: 0x060037E7 RID: 14311 RVA: 0x000D4817 File Offset: 0x000D2A17
		public static DbNewInstanceExpression New(this TypeUsage instanceType, IEnumerable<DbExpression> arguments)
		{
			return DbExpressionBuilder.NewInstance(instanceType, arguments);
		}

		// Token: 0x060037E8 RID: 14312 RVA: 0x000D4817 File Offset: 0x000D2A17
		public static DbNewInstanceExpression New(this TypeUsage instanceType, params DbExpression[] arguments)
		{
			return DbExpressionBuilder.NewInstance(instanceType, arguments);
		}

		// Token: 0x060037E9 RID: 14313 RVA: 0x000D4820 File Offset: 0x000D2A20
		private static DbNewInstanceExpression NewInstance(TypeUsage instanceType, IEnumerable<DbExpression> arguments)
		{
			DbExpressionList args;
			TypeUsage type = ArgumentValidation.ValidateNew(instanceType, arguments, out args);
			return new DbNewInstanceExpression(type, args);
		}

		// Token: 0x060037EA RID: 14314 RVA: 0x000D483E File Offset: 0x000D2A3E
		public static DbNewInstanceExpression NewCollection(IEnumerable<DbExpression> elements)
		{
			return DbExpressionBuilder.CreateNewCollection(elements);
		}

		// Token: 0x060037EB RID: 14315 RVA: 0x000D483E File Offset: 0x000D2A3E
		public static DbNewInstanceExpression NewCollection(params DbExpression[] elements)
		{
			return DbExpressionBuilder.CreateNewCollection(elements);
		}

		// Token: 0x060037EC RID: 14316 RVA: 0x000D4848 File Offset: 0x000D2A48
		private static DbNewInstanceExpression CreateNewCollection(IEnumerable<DbExpression> elements)
		{
			DbExpressionList args;
			TypeUsage type = ArgumentValidation.ValidateNewCollection(elements, out args);
			return new DbNewInstanceExpression(type, args);
		}

		// Token: 0x060037ED RID: 14317 RVA: 0x000D4868 File Offset: 0x000D2A68
		public static DbNewInstanceExpression NewEmptyCollection(this TypeUsage collectionType)
		{
			DbExpressionList args;
			TypeUsage type = ArgumentValidation.ValidateNewEmptyCollection(collectionType, out args);
			return new DbNewInstanceExpression(type, args);
		}

		// Token: 0x060037EE RID: 14318 RVA: 0x000D4888 File Offset: 0x000D2A88
		public static DbNewInstanceExpression NewRow(IEnumerable<KeyValuePair<string, DbExpression>> columnValues)
		{
			DbExpressionList args;
			TypeUsage type = ArgumentValidation.ValidateNewRow(columnValues, out args);
			return new DbNewInstanceExpression(type, args);
		}

		// Token: 0x060037EF RID: 14319 RVA: 0x000D48A5 File Offset: 0x000D2AA5
		public static DbPropertyExpression Property(this DbExpression instance, EdmProperty propertyMetadata)
		{
			return DbExpressionBuilder.PropertyFromMember(instance, propertyMetadata, "propertyMetadata");
		}

		// Token: 0x060037F0 RID: 14320 RVA: 0x000D48B3 File Offset: 0x000D2AB3
		public static DbPropertyExpression Property(this DbExpression instance, NavigationProperty navigationProperty)
		{
			return DbExpressionBuilder.PropertyFromMember(instance, navigationProperty, "navigationProperty");
		}

		// Token: 0x060037F1 RID: 14321 RVA: 0x000D48C1 File Offset: 0x000D2AC1
		public static DbPropertyExpression Property(this DbExpression instance, RelationshipEndMember relationshipEnd)
		{
			return DbExpressionBuilder.PropertyFromMember(instance, relationshipEnd, "relationshipEnd");
		}

		// Token: 0x060037F2 RID: 14322 RVA: 0x000D48CF File Offset: 0x000D2ACF
		public static DbPropertyExpression Property(this DbExpression instance, string propertyName)
		{
			return DbExpressionBuilder.PropertyByName(instance, propertyName, false);
		}

		// Token: 0x060037F3 RID: 14323 RVA: 0x000D48DC File Offset: 0x000D2ADC
		private static DbPropertyExpression PropertyFromMember(DbExpression instance, EdmMember property, string propertyArgumentName)
		{
			TypeUsage resultType = ArgumentValidation.ValidateProperty(instance, property, propertyArgumentName);
			return new DbPropertyExpression(resultType, property, instance);
		}

		// Token: 0x060037F4 RID: 14324 RVA: 0x000D48FC File Offset: 0x000D2AFC
		private static DbPropertyExpression PropertyByName(DbExpression instance, string propertyName, bool ignoreCase)
		{
			EdmMember property;
			TypeUsage resultType = ArgumentValidation.ValidateProperty(instance, propertyName, ignoreCase, out property);
			return new DbPropertyExpression(resultType, property, instance);
		}

		// Token: 0x060037F5 RID: 14325 RVA: 0x000D491C File Offset: 0x000D2B1C
		private static string ExtractAlias(MethodInfo method)
		{
			string[] array = DbExpressionBuilder.ExtractAliases(method);
			return array[0];
		}

		// Token: 0x060037F6 RID: 14326 RVA: 0x000D4934 File Offset: 0x000D2B34
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

		// Token: 0x060037F7 RID: 14327 RVA: 0x000D49DF File Offset: 0x000D2BDF
		private static DbExpressionBinding ConvertToBinding<TResult>(DbExpression source, Func<DbExpression, TResult> argument, string argumentName, out TResult argumentResult)
		{
			return DbExpressionBuilder.ConvertToBinding<TResult>(source, "source", argument, argumentName, out argumentResult);
		}

		// Token: 0x060037F8 RID: 14328 RVA: 0x000D49F0 File Offset: 0x000D2BF0
		private static DbExpressionBinding ConvertToBinding<TResult>(DbExpression source, string sourceName, Func<DbExpression, TResult> argument, string argumentName, out TResult argumentResult)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(source, sourceName);
			EntityUtil.CheckArgumentNull<Func<DbExpression, TResult>>(argument, argumentName);
			string varName = DbExpressionBuilder.ExtractAlias(argument.Method);
			DbExpressionBinding dbExpressionBinding = source.BindAs(varName);
			argumentResult = argument(dbExpressionBinding.Variable);
			return dbExpressionBinding;
		}

		// Token: 0x060037F9 RID: 14329 RVA: 0x000D4A38 File Offset: 0x000D2C38
		private static DbExpressionBinding[] ConvertToBinding(DbExpression left, string leftArgumentName, DbExpression right, string rightArgumentName, Func<DbExpression, DbExpression, DbExpression> argument, string argumentName, out DbExpression argumentExp)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(left, leftArgumentName);
			EntityUtil.CheckArgumentNull<DbExpression>(right, rightArgumentName);
			EntityUtil.CheckArgumentNull<Func<DbExpression, DbExpression, DbExpression>>(argument, argumentName);
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

		// Token: 0x060037FA RID: 14330 RVA: 0x000D4AA4 File Offset: 0x000D2CA4
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		private static bool TryGetAnonymousTypeValues<TInstance, TRequired>(object instance, out List<KeyValuePair<string, TRequired>> values)
		{
			values = null;
			if (typeof(TInstance).BaseType.Equals(typeof(object)) && typeof(TInstance).GetProperties(BindingFlags.Static).Length == 0 && typeof(TInstance).GetProperties(BindingFlags.Instance | BindingFlags.NonPublic).Length == 0)
			{
				List<KeyValuePair<string, TRequired>> list = null;
				foreach (PropertyInfo propertyInfo in typeof(TInstance).GetProperties(BindingFlags.Instance | BindingFlags.Public))
				{
					if (!propertyInfo.CanRead || !typeof(TRequired).IsAssignableFrom(propertyInfo.PropertyType))
					{
						list = null;
						break;
					}
					if (list == null)
					{
						list = new List<KeyValuePair<string, TRequired>>();
					}
					list.Add(new KeyValuePair<string, TRequired>(propertyInfo.Name, (TRequired)((object)propertyInfo.GetValue(instance, null))));
				}
				values = list;
			}
			return values != null;
		}

		// Token: 0x060037FB RID: 14331 RVA: 0x000D4B7C File Offset: 0x000D2D7C
		private static bool TryResolveToConstant(Type type, object value, out DbExpression constantOrNullExpression)
		{
			constantOrNullExpression = null;
			Type clrType = type;
			if (type.IsGenericType && typeof(Nullable<>).Equals(type.GetGenericTypeDefinition()))
			{
				clrType = type.GetGenericArguments()[0];
			}
			PrimitiveTypeKind primitiveTypeKind;
			if (ClrProviderManifest.Instance.TryGetPrimitiveTypeKind(clrType, out primitiveTypeKind))
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

		// Token: 0x060037FC RID: 14332 RVA: 0x000D4BE8 File Offset: 0x000D2DE8
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
			List<KeyValuePair<string, DbExpression>> columnValues;
			if (DbExpressionBuilder.TryGetAnonymousTypeValues<TArgument, DbExpression>(obj, out columnValues))
			{
				return DbExpressionBuilder.NewRow(columnValues);
			}
			throw EntityUtil.NotSupported(Strings.Cqt_Factory_MethodResultTypeNotSupported(typeof(TArgument).FullName));
		}

		// Token: 0x060037FD RID: 14333 RVA: 0x000D4C8C File Offset: 0x000D2E8C
		private static DbApplyExpression CreateApply(DbExpression source, Func<DbExpression, KeyValuePair<string, DbExpression>> apply, Func<DbExpressionBinding, DbExpressionBinding, DbApplyExpression> resultBuilder)
		{
			KeyValuePair<string, DbExpression> keyValuePair;
			DbExpressionBinding arg = DbExpressionBuilder.ConvertToBinding<KeyValuePair<string, DbExpression>>(source, apply, "apply", out keyValuePair);
			DbExpressionBinding arg2 = keyValuePair.Value.BindAs(keyValuePair.Key);
			return resultBuilder(arg, arg2);
		}

		// Token: 0x060037FE RID: 14334 RVA: 0x000D4CC4 File Offset: 0x000D2EC4
		public static DbQuantifierExpression All(this DbExpression source, Func<DbExpression, DbExpression> predicate)
		{
			DbExpression predicate2;
			DbExpressionBinding input = DbExpressionBuilder.ConvertToBinding<DbExpression>(source, predicate, "predicate", out predicate2);
			return input.All(predicate2);
		}

		// Token: 0x060037FF RID: 14335 RVA: 0x000D4CE7 File Offset: 0x000D2EE7
		public static DbExpression Any(this DbExpression source)
		{
			return source.Exists();
		}

		// Token: 0x06003800 RID: 14336 RVA: 0x000D4CEF File Offset: 0x000D2EEF
		public static DbExpression Exists(this DbExpression argument)
		{
			return argument.IsEmpty().Not();
		}

		// Token: 0x06003801 RID: 14337 RVA: 0x000D4CFC File Offset: 0x000D2EFC
		public static DbQuantifierExpression Any(this DbExpression source, Func<DbExpression, DbExpression> predicate)
		{
			DbExpression predicate2;
			DbExpressionBinding input = DbExpressionBuilder.ConvertToBinding<DbExpression>(source, predicate, "predicate", out predicate2);
			return input.Any(predicate2);
		}

		// Token: 0x06003802 RID: 14338 RVA: 0x000D4D1F File Offset: 0x000D2F1F
		public static DbApplyExpression CrossApply(this DbExpression source, Func<DbExpression, KeyValuePair<string, DbExpression>> apply)
		{
			return DbExpressionBuilder.CreateApply(source, apply, new Func<DbExpressionBinding, DbExpressionBinding, DbApplyExpression>(DbExpressionBuilder.CrossApply));
		}

		// Token: 0x06003803 RID: 14339 RVA: 0x000D4D34 File Offset: 0x000D2F34
		public static DbApplyExpression OuterApply(this DbExpression source, Func<DbExpression, KeyValuePair<string, DbExpression>> apply)
		{
			return DbExpressionBuilder.CreateApply(source, apply, new Func<DbExpressionBinding, DbExpressionBinding, DbApplyExpression>(DbExpressionBuilder.OuterApply));
		}

		// Token: 0x06003804 RID: 14340 RVA: 0x000D4D4C File Offset: 0x000D2F4C
		public static DbJoinExpression FullOuterJoin(this DbExpression left, DbExpression right, Func<DbExpression, DbExpression, DbExpression> joinCondition)
		{
			DbExpression joinCondition2;
			DbExpressionBinding[] array = DbExpressionBuilder.ConvertToBinding(left, "left", right, "right", joinCondition, "joinCondition", out joinCondition2);
			return array[0].FullOuterJoin(array[1], joinCondition2);
		}

		// Token: 0x06003805 RID: 14341 RVA: 0x000D4D80 File Offset: 0x000D2F80
		public static DbJoinExpression InnerJoin(this DbExpression left, DbExpression right, Func<DbExpression, DbExpression, DbExpression> joinCondition)
		{
			DbExpression joinCondition2;
			DbExpressionBinding[] array = DbExpressionBuilder.ConvertToBinding(left, "left", right, "right", joinCondition, "joinCondition", out joinCondition2);
			return array[0].InnerJoin(array[1], joinCondition2);
		}

		// Token: 0x06003806 RID: 14342 RVA: 0x000D4DB4 File Offset: 0x000D2FB4
		public static DbJoinExpression LeftOuterJoin(this DbExpression left, DbExpression right, Func<DbExpression, DbExpression, DbExpression> joinCondition)
		{
			DbExpression joinCondition2;
			DbExpressionBinding[] array = DbExpressionBuilder.ConvertToBinding(left, "left", right, "right", joinCondition, "joinCondition", out joinCondition2);
			return array[0].LeftOuterJoin(array[1], joinCondition2);
		}

		// Token: 0x06003807 RID: 14343 RVA: 0x000D4DE8 File Offset: 0x000D2FE8
		public static DbJoinExpression Join(this DbExpression outer, DbExpression inner, Func<DbExpression, DbExpression> outerKey, Func<DbExpression, DbExpression> innerKey)
		{
			DbExpression left2;
			DbExpressionBinding left = DbExpressionBuilder.ConvertToBinding<DbExpression>(outer, "outer", outerKey, "outerKey", out left2);
			DbExpression right2;
			DbExpressionBinding right = DbExpressionBuilder.ConvertToBinding<DbExpression>(inner, "inner", innerKey, "innerKey", out right2);
			DbExpression joinCondition = left2.Equal(right2);
			return left.InnerJoin(right, joinCondition);
		}

		// Token: 0x06003808 RID: 14344 RVA: 0x000D4E30 File Offset: 0x000D3030
		public static DbProjectExpression Join<TSelector>(this DbExpression outer, DbExpression inner, Func<DbExpression, DbExpression> outerKey, Func<DbExpression, DbExpression> innerKey, Func<DbExpression, DbExpression, TSelector> selector)
		{
			DbJoinExpression dbJoinExpression = outer.Join(inner, outerKey, innerKey);
			EntityUtil.CheckArgumentNull<Func<DbExpression, DbExpression, TSelector>>(selector, "selector");
			DbExpressionBinding dbExpressionBinding = dbJoinExpression.Bind();
			DbExpression arg = dbExpressionBinding.Variable.Property(dbJoinExpression.Left.VariableName);
			DbExpression arg2 = dbExpressionBinding.Variable.Property(dbJoinExpression.Right.VariableName);
			TSelector argument = selector(arg, arg2);
			DbExpression projection = DbExpressionBuilder.ResolveToExpression<TSelector>(argument);
			return dbExpressionBinding.Project(projection);
		}

		// Token: 0x06003809 RID: 14345 RVA: 0x000D4EA8 File Offset: 0x000D30A8
		public static DbSortExpression OrderBy(this DbExpression source, Func<DbExpression, DbExpression> sortKey)
		{
			DbExpression key;
			DbExpressionBinding input = DbExpressionBuilder.ConvertToBinding<DbExpression>(source, sortKey, "sortKey", out key);
			DbSortClause dbSortClause = key.ToSortClause();
			return input.Sort(new DbSortClause[]
			{
				dbSortClause
			});
		}

		// Token: 0x0600380A RID: 14346 RVA: 0x000D4EDC File Offset: 0x000D30DC
		public static DbSortExpression OrderBy(this DbExpression source, Func<DbExpression, DbExpression> sortKey, string collation)
		{
			DbExpression key;
			DbExpressionBinding input = DbExpressionBuilder.ConvertToBinding<DbExpression>(source, sortKey, "sortKey", out key);
			DbSortClause dbSortClause = key.ToSortClause(collation);
			return input.Sort(new DbSortClause[]
			{
				dbSortClause
			});
		}

		// Token: 0x0600380B RID: 14347 RVA: 0x000D4F10 File Offset: 0x000D3110
		public static DbSortExpression OrderByDescending(this DbExpression source, Func<DbExpression, DbExpression> sortKey)
		{
			DbExpression key;
			DbExpressionBinding input = DbExpressionBuilder.ConvertToBinding<DbExpression>(source, sortKey, "sortKey", out key);
			DbSortClause dbSortClause = key.ToSortClauseDescending();
			return input.Sort(new DbSortClause[]
			{
				dbSortClause
			});
		}

		// Token: 0x0600380C RID: 14348 RVA: 0x000D4F44 File Offset: 0x000D3144
		public static DbSortExpression OrderByDescending(this DbExpression source, Func<DbExpression, DbExpression> sortKey, string collation)
		{
			DbExpression key;
			DbExpressionBinding input = DbExpressionBuilder.ConvertToBinding<DbExpression>(source, sortKey, "sortKey", out key);
			DbSortClause dbSortClause = key.ToSortClauseDescending(collation);
			return input.Sort(new DbSortClause[]
			{
				dbSortClause
			});
		}

		// Token: 0x0600380D RID: 14349 RVA: 0x000D4F78 File Offset: 0x000D3178
		public static DbProjectExpression Select<TProjection>(this DbExpression source, Func<DbExpression, TProjection> projection)
		{
			EntityUtil.CheckArgumentNull<Func<DbExpression, TProjection>>(projection, "projection");
			TProjection argument;
			DbExpressionBinding input = DbExpressionBuilder.ConvertToBinding<TProjection>(source, projection, "projection", out argument);
			DbExpression projection2 = DbExpressionBuilder.ResolveToExpression<TProjection>(argument);
			return input.Project(projection2);
		}

		// Token: 0x0600380E RID: 14350 RVA: 0x000D4FB0 File Offset: 0x000D31B0
		public static DbProjectExpression SelectMany(this DbExpression source, Func<DbExpression, DbExpression> apply)
		{
			DbExpression input2;
			DbExpressionBinding input = DbExpressionBuilder.ConvertToBinding<DbExpression>(source, apply, "apply", out input2);
			DbExpressionBinding dbExpressionBinding = input2.Bind();
			DbApplyExpression input3 = input.CrossApply(dbExpressionBinding);
			DbExpressionBinding dbExpressionBinding2 = input3.Bind();
			return dbExpressionBinding2.Project(dbExpressionBinding2.Variable.Property(dbExpressionBinding.VariableName));
		}

		// Token: 0x0600380F RID: 14351 RVA: 0x000D4FFC File Offset: 0x000D31FC
		public static DbProjectExpression SelectMany<TSelector>(this DbExpression source, Func<DbExpression, DbExpression> apply, Func<DbExpression, DbExpression, TSelector> selector)
		{
			DbExpression input;
			DbExpressionBinding dbExpressionBinding = DbExpressionBuilder.ConvertToBinding<DbExpression>(source, apply, "apply", out input);
			EntityUtil.CheckArgumentNull<Func<DbExpression, DbExpression, TSelector>>(selector, "selector");
			DbExpressionBinding dbExpressionBinding2 = input.Bind();
			DbApplyExpression input2 = dbExpressionBinding.CrossApply(dbExpressionBinding2);
			DbExpressionBinding dbExpressionBinding3 = input2.Bind();
			DbExpression arg = dbExpressionBinding3.Variable.Property(dbExpressionBinding.VariableName);
			DbExpression arg2 = dbExpressionBinding3.Variable.Property(dbExpressionBinding2.VariableName);
			TSelector argument = selector(arg, arg2);
			DbExpression projection = DbExpressionBuilder.ResolveToExpression<TSelector>(argument);
			return dbExpressionBinding3.Project(projection);
		}

		// Token: 0x06003810 RID: 14352 RVA: 0x000D5081 File Offset: 0x000D3281
		public static DbSkipExpression Skip(this DbSortExpression argument, DbExpression count)
		{
			EntityUtil.CheckArgumentNull<DbSortExpression>(argument, "argument");
			return argument.Input.Skip(argument.SortOrder, count);
		}

		// Token: 0x06003811 RID: 14353 RVA: 0x000D50A1 File Offset: 0x000D32A1
		public static DbLimitExpression Take(this DbExpression argument, DbExpression count)
		{
			return argument.Limit(count);
		}

		// Token: 0x06003812 RID: 14354 RVA: 0x000D50AC File Offset: 0x000D32AC
		private static DbSortExpression CreateThenBy(DbSortExpression source, Func<DbExpression, DbExpression> sortKey, bool ascending, string collation, bool useCollation)
		{
			EntityUtil.CheckArgumentNull<DbSortExpression>(source, "source");
			EntityUtil.CheckArgumentNull<Func<DbExpression, DbExpression>>(sortKey, "sortKey");
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

		// Token: 0x06003813 RID: 14355 RVA: 0x000D5141 File Offset: 0x000D3341
		public static DbSortExpression ThenBy(this DbSortExpression source, Func<DbExpression, DbExpression> sortKey)
		{
			return DbExpressionBuilder.CreateThenBy(source, sortKey, true, null, false);
		}

		// Token: 0x06003814 RID: 14356 RVA: 0x000D514D File Offset: 0x000D334D
		public static DbSortExpression ThenBy(this DbSortExpression source, Func<DbExpression, DbExpression> sortKey, string collation)
		{
			return DbExpressionBuilder.CreateThenBy(source, sortKey, true, collation, true);
		}

		// Token: 0x06003815 RID: 14357 RVA: 0x000D5159 File Offset: 0x000D3359
		public static DbSortExpression ThenByDescending(this DbSortExpression source, Func<DbExpression, DbExpression> sortKey)
		{
			return DbExpressionBuilder.CreateThenBy(source, sortKey, false, null, false);
		}

		// Token: 0x06003816 RID: 14358 RVA: 0x000D5165 File Offset: 0x000D3365
		public static DbSortExpression ThenByDescending(this DbSortExpression source, Func<DbExpression, DbExpression> sortKey, string collation)
		{
			return DbExpressionBuilder.CreateThenBy(source, sortKey, false, collation, true);
		}

		// Token: 0x06003817 RID: 14359 RVA: 0x000D5174 File Offset: 0x000D3374
		public static DbFilterExpression Where(this DbExpression source, Func<DbExpression, DbExpression> predicate)
		{
			DbExpression predicate2;
			DbExpressionBinding input = DbExpressionBuilder.ConvertToBinding<DbExpression>(source, predicate, "predicate", out predicate2);
			return input.Filter(predicate2);
		}

		// Token: 0x06003818 RID: 14360 RVA: 0x000D5197 File Offset: 0x000D3397
		public static DbExpression Union(this DbExpression left, DbExpression right)
		{
			return left.UnionAll(right).Distinct();
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x06003819 RID: 14361 RVA: 0x000D51A5 File Offset: 0x000D33A5
		internal static AliasGenerator AliasGenerator
		{
			get
			{
				return DbExpressionBuilder._bindingAliases;
			}
		}

		// Token: 0x0600381A RID: 14362 RVA: 0x000D51AC File Offset: 0x000D33AC
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
				throw EntityUtil.InvalidEnumerationValue(typeof(PrimitiveTypeKind), (int)primitiveType);
			}
		}

		// Token: 0x0600381B RID: 14363 RVA: 0x000D527B File Offset: 0x000D347B
		internal static DbApplyExpression CreateApplyExpressionByKind(DbExpressionKind applyKind, DbExpressionBinding input, DbExpressionBinding apply)
		{
			if (applyKind == DbExpressionKind.CrossApply)
			{
				return input.CrossApply(apply);
			}
			if (applyKind != DbExpressionKind.OuterApply)
			{
				throw EntityUtil.InvalidEnumerationValue(typeof(DbExpressionKind), (int)applyKind);
			}
			return input.OuterApply(apply);
		}

		// Token: 0x0600381C RID: 14364 RVA: 0x000D52A8 File Offset: 0x000D34A8
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
				throw EntityUtil.InvalidEnumerationValue(typeof(DbExpressionKind), (int)joinKind);
			}
			return input1.LeftOuterJoin(input2, joinCondition);
		}

		// Token: 0x0600381D RID: 14365 RVA: 0x000D530C File Offset: 0x000D350C
		internal static DbIsNullExpression CreateIsNullExpressionAllowingRowTypeArgument(DbExpression argument)
		{
			TypeUsage booleanResultType = ArgumentValidation.ValidateIsNull(argument, true);
			return new DbIsNullExpression(booleanResultType, argument, true);
		}

		// Token: 0x0600381E RID: 14366 RVA: 0x000D532C File Offset: 0x000D352C
		internal static DbElementExpression CreateElementExpressionUnwrapSingleProperty(DbExpression argument)
		{
			TypeUsage typeUsage = ArgumentValidation.ValidateElement(argument);
			IList<EdmProperty> properties = TypeHelpers.GetProperties(typeUsage);
			if (properties == null || properties.Count != 1)
			{
				throw EntityUtil.Argument(Strings.Cqt_Element_InvalidArgumentForUnwrapSingleProperty, "arg");
			}
			typeUsage = properties[0].TypeUsage;
			return new DbElementExpression(typeUsage, argument, true);
		}

		// Token: 0x0600381F RID: 14367 RVA: 0x000D5378 File Offset: 0x000D3578
		internal static DbRelatedEntityRef CreateRelatedEntityRef(RelationshipEndMember sourceEnd, RelationshipEndMember targetEnd, DbExpression targetEntity)
		{
			return new DbRelatedEntityRef(sourceEnd, targetEnd, targetEntity);
		}

		// Token: 0x06003820 RID: 14368 RVA: 0x000D5384 File Offset: 0x000D3584
		internal static DbNewInstanceExpression CreateNewEntityWithRelationshipsExpression(EntityType entityType, IList<DbExpression> attributeValues, IList<DbRelatedEntityRef> relationships)
		{
			DbExpressionList attributeValues2;
			ReadOnlyCollection<DbRelatedEntityRef> relationships2;
			TypeUsage resultType = ArgumentValidation.ValidateNewEntityWithRelationships(entityType, attributeValues, relationships, out attributeValues2, out relationships2);
			return new DbNewInstanceExpression(resultType, attributeValues2, relationships2);
		}

		// Token: 0x06003821 RID: 14369 RVA: 0x000D53A8 File Offset: 0x000D35A8
		internal static DbRelationshipNavigationExpression NavigateAllowingAllRelationshipsInSameTypeHierarchy(this DbExpression navigateFrom, RelationshipEndMember fromEnd, RelationshipEndMember toEnd)
		{
			RelationshipType relType;
			TypeUsage resultType = ArgumentValidation.ValidateNavigate(navigateFrom, fromEnd, toEnd, out relType, true);
			return new DbRelationshipNavigationExpression(resultType, relType, fromEnd, toEnd, navigateFrom);
		}

		// Token: 0x06003822 RID: 14370 RVA: 0x000D53CB File Offset: 0x000D35CB
		internal static DbPropertyExpression CreatePropertyExpressionFromMember(DbExpression instance, EdmMember member)
		{
			return DbExpressionBuilder.PropertyFromMember(instance, member, "member");
		}

		// Token: 0x0400183C RID: 6204
		private static readonly AliasGenerator _bindingAliases = new AliasGenerator("Var_", 0);

		// Token: 0x0400183D RID: 6205
		private static readonly DbNullExpression _binaryNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Binary).Null();

		// Token: 0x0400183E RID: 6206
		private static readonly DbNullExpression _boolNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Boolean).Null();

		// Token: 0x0400183F RID: 6207
		private static readonly DbNullExpression _byteNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Byte).Null();

		// Token: 0x04001840 RID: 6208
		private static readonly DbNullExpression _dateTimeNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.DateTime).Null();

		// Token: 0x04001841 RID: 6209
		private static readonly DbNullExpression _dateTimeOffsetNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.DateTimeOffset).Null();

		// Token: 0x04001842 RID: 6210
		private static readonly DbNullExpression _decimalNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Decimal).Null();

		// Token: 0x04001843 RID: 6211
		private static readonly DbNullExpression _doubleNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Double).Null();

		// Token: 0x04001844 RID: 6212
		private static readonly DbNullExpression _geographyNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Geography).Null();

		// Token: 0x04001845 RID: 6213
		private static readonly DbNullExpression _geometryNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Geometry).Null();

		// Token: 0x04001846 RID: 6214
		private static readonly DbNullExpression _guidNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Guid).Null();

		// Token: 0x04001847 RID: 6215
		private static readonly DbNullExpression _int16Null = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Int16).Null();

		// Token: 0x04001848 RID: 6216
		private static readonly DbNullExpression _int32Null = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Int32).Null();

		// Token: 0x04001849 RID: 6217
		private static readonly DbNullExpression _int64Null = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Int64).Null();

		// Token: 0x0400184A RID: 6218
		private static readonly DbNullExpression _sbyteNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.SByte).Null();

		// Token: 0x0400184B RID: 6219
		private static readonly DbNullExpression _singleNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Single).Null();

		// Token: 0x0400184C RID: 6220
		private static readonly DbNullExpression _stringNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.String).Null();

		// Token: 0x0400184D RID: 6221
		private static readonly DbNullExpression _timeNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Time).Null();

		// Token: 0x0400184E RID: 6222
		private static readonly DbConstantExpression _boolTrue = DbExpressionBuilder.Constant(true);

		// Token: 0x0400184F RID: 6223
		private static readonly DbConstantExpression _boolFalse = DbExpressionBuilder.Constant(false);
	}
}
