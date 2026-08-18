using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.CommandTrees;
using System.Data.Common.QueryCache;
using System.Data.Metadata.Edm;
using System.Data.Objects.Internal;
using System.Linq;
using System.Linq.Expressions;

namespace System.Data.Objects.ELinq
{
	// Token: 0x0200019C RID: 412
	internal sealed class CompiledELinqQueryState : ELinqQueryState
	{
		// Token: 0x06001E32 RID: 7730 RVA: 0x00067F21 File Offset: 0x00066121
		internal CompiledELinqQueryState(Type elementType, ObjectContext context, LambdaExpression lambda, Guid cacheToken, object[] parameterValues) : base(elementType, context, lambda)
		{
			EntityUtil.CheckArgumentNull<object[]>(parameterValues, "parameterValues");
			this._cacheToken = cacheToken;
			this._parameterValues = parameterValues;
			base.EnsureParameters();
			base.Parameters.SetReadOnly(true);
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x00067F5C File Offset: 0x0006615C
		internal override ObjectQueryExecutionPlan GetExecutionPlan(MergeOption? forMergeOption)
		{
			base.ObjectContext.EnsureMetadata();
			ObjectQueryExecutionPlan objectQueryExecutionPlan = null;
			CompiledQueryCacheEntry compiledQueryCacheEntry = this._cacheEntry;
			bool useCSharpNullComparisonBehavior = base.ObjectContext.ContextOptions.UseCSharpNullComparisonBehavior;
			if (compiledQueryCacheEntry != null)
			{
				MergeOption mergeOption = ObjectQueryState.EnsureMergeOption(new MergeOption?[]
				{
					forMergeOption,
					base.UserSpecifiedMergeOption,
					compiledQueryCacheEntry.PropagatedMergeOption
				});
				objectQueryExecutionPlan = compiledQueryCacheEntry.GetExecutionPlan(mergeOption, useCSharpNullComparisonBehavior);
				if (objectQueryExecutionPlan == null)
				{
					ExpressionConverter expressionConverter = this.CreateExpressionConverter();
					DbExpression query = expressionConverter.Convert();
					ReadOnlyCollection<KeyValuePair<ObjectParameter, QueryParameterExpression>> parameters = expressionConverter.GetParameters();
					DbQueryCommandTree tree = DbQueryCommandTree.FromValidExpression(base.ObjectContext.MetadataWorkspace, DataSpace.CSpace, query);
					objectQueryExecutionPlan = ObjectQueryExecutionPlan.Prepare(base.ObjectContext, tree, base.ElementType, mergeOption, expressionConverter.PropagatedSpan, parameters, expressionConverter.AliasGenerator);
					objectQueryExecutionPlan = compiledQueryCacheEntry.SetExecutionPlan(objectQueryExecutionPlan, useCSharpNullComparisonBehavior);
				}
			}
			else
			{
				QueryCacheManager queryCacheManager = base.ObjectContext.MetadataWorkspace.GetQueryCacheManager();
				CompiledQueryCacheKey compiledQueryCacheKey = new CompiledQueryCacheKey(this._cacheToken);
				if (queryCacheManager.TryCacheLookup<CompiledQueryCacheKey, CompiledQueryCacheEntry>(compiledQueryCacheKey, out compiledQueryCacheEntry))
				{
					this._cacheEntry = compiledQueryCacheEntry;
					MergeOption mergeOption2 = ObjectQueryState.EnsureMergeOption(new MergeOption?[]
					{
						forMergeOption,
						base.UserSpecifiedMergeOption,
						compiledQueryCacheEntry.PropagatedMergeOption
					});
					objectQueryExecutionPlan = compiledQueryCacheEntry.GetExecutionPlan(mergeOption2, useCSharpNullComparisonBehavior);
				}
				if (objectQueryExecutionPlan == null)
				{
					ExpressionConverter expressionConverter2 = this.CreateExpressionConverter();
					DbExpression query2 = expressionConverter2.Convert();
					ReadOnlyCollection<KeyValuePair<ObjectParameter, QueryParameterExpression>> parameters2 = expressionConverter2.GetParameters();
					DbQueryCommandTree tree2 = DbQueryCommandTree.FromValidExpression(base.ObjectContext.MetadataWorkspace, DataSpace.CSpace, query2);
					if (compiledQueryCacheEntry == null)
					{
						compiledQueryCacheEntry = new CompiledQueryCacheEntry(compiledQueryCacheKey, expressionConverter2.PropagatedMergeOption);
						QueryCacheEntry queryCacheEntry;
						if (queryCacheManager.TryLookupAndAdd(compiledQueryCacheEntry, out queryCacheEntry))
						{
							compiledQueryCacheEntry = (CompiledQueryCacheEntry)queryCacheEntry;
						}
						this._cacheEntry = compiledQueryCacheEntry;
					}
					MergeOption mergeOption3 = ObjectQueryState.EnsureMergeOption(new MergeOption?[]
					{
						forMergeOption,
						base.UserSpecifiedMergeOption,
						compiledQueryCacheEntry.PropagatedMergeOption
					});
					objectQueryExecutionPlan = compiledQueryCacheEntry.GetExecutionPlan(mergeOption3, useCSharpNullComparisonBehavior);
					if (objectQueryExecutionPlan == null)
					{
						objectQueryExecutionPlan = ObjectQueryExecutionPlan.Prepare(base.ObjectContext, tree2, base.ElementType, mergeOption3, expressionConverter2.PropagatedSpan, parameters2, expressionConverter2.AliasGenerator);
						objectQueryExecutionPlan = compiledQueryCacheEntry.SetExecutionPlan(objectQueryExecutionPlan, useCSharpNullComparisonBehavior);
					}
				}
			}
			ObjectParameterCollection objectParameterCollection = base.EnsureParameters();
			if (objectQueryExecutionPlan.CompiledQueryParameters != null && objectQueryExecutionPlan.CompiledQueryParameters.Count > 0)
			{
				objectParameterCollection.SetReadOnly(false);
				objectParameterCollection.Clear();
				foreach (KeyValuePair<ObjectParameter, QueryParameterExpression> keyValuePair in objectQueryExecutionPlan.CompiledQueryParameters)
				{
					ObjectParameter objectParameter = keyValuePair.Key.ShallowCopy();
					QueryParameterExpression value = keyValuePair.Value;
					objectParameterCollection.Add(objectParameter);
					if (value != null)
					{
						objectParameter.Value = value.EvaluateParameter(this._parameterValues);
					}
				}
			}
			objectParameterCollection.SetReadOnly(true);
			return objectQueryExecutionPlan;
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x0006821C File Offset: 0x0006641C
		protected override TypeUsage GetResultType()
		{
			CompiledQueryCacheEntry cacheEntry = this._cacheEntry;
			TypeUsage result;
			if (cacheEntry != null && cacheEntry.TryGetResultType(out result))
			{
				return result;
			}
			return base.GetResultType();
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001E35 RID: 7733 RVA: 0x00068245 File Offset: 0x00066445
		internal override Expression Expression
		{
			get
			{
				return CompiledELinqQueryState.CreateDonateableExpressionVisitor.Replace((LambdaExpression)base.Expression, base.ObjectContext, this._parameterValues);
			}
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x00068264 File Offset: 0x00066464
		protected override ExpressionConverter CreateExpressionConverter()
		{
			LambdaExpression lambdaExpression = (LambdaExpression)base.Expression;
			Funcletizer funcletizer = Funcletizer.CreateCompiledQueryEvaluationFuncletizer(base.ObjectContext, lambdaExpression.Parameters.First<ParameterExpression>(), lambdaExpression.Parameters.Skip(1).ToList<ParameterExpression>().AsReadOnly());
			return new ExpressionConverter(funcletizer, lambdaExpression.Body);
		}

		// Token: 0x04000C0C RID: 3084
		private readonly Guid _cacheToken;

		// Token: 0x04000C0D RID: 3085
		private readonly object[] _parameterValues;

		// Token: 0x04000C0E RID: 3086
		private CompiledQueryCacheEntry _cacheEntry;

		// Token: 0x020004FF RID: 1279
		private sealed class CreateDonateableExpressionVisitor : EntityExpressionVisitor
		{
			// Token: 0x06003D81 RID: 15745 RVA: 0x000E65DC File Offset: 0x000E47DC
			private CreateDonateableExpressionVisitor(Dictionary<ParameterExpression, object> parameterToValueLookup)
			{
				this._parameterToValueLookup = parameterToValueLookup;
			}

			// Token: 0x06003D82 RID: 15746 RVA: 0x000E65EC File Offset: 0x000E47EC
			internal static Expression Replace(LambdaExpression query, ObjectContext objectContext, object[] parameterValues)
			{
				Dictionary<ParameterExpression, object> dictionary = query.Parameters.Skip(1).Zip(parameterValues).ToDictionary((KeyValuePair<ParameterExpression, object> pair) => pair.Key, (KeyValuePair<ParameterExpression, object> pair) => pair.Value);
				dictionary.Add(query.Parameters.First<ParameterExpression>(), objectContext);
				CompiledELinqQueryState.CreateDonateableExpressionVisitor createDonateableExpressionVisitor = new CompiledELinqQueryState.CreateDonateableExpressionVisitor(dictionary);
				return createDonateableExpressionVisitor.Visit(query.Body);
			}

			// Token: 0x06003D83 RID: 15747 RVA: 0x000E6674 File Offset: 0x000E4874
			internal override Expression VisitParameter(ParameterExpression p)
			{
				object value;
				Expression result;
				if (this._parameterToValueLookup.TryGetValue(p, out value))
				{
					result = Expression.Constant(value, p.Type);
				}
				else
				{
					result = base.VisitParameter(p);
				}
				return result;
			}

			// Token: 0x04001AEA RID: 6890
			private readonly Dictionary<ParameterExpression, object> _parameterToValueLookup;
		}
	}
}
