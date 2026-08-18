using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.QueryCache;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.Internal;
using System.Linq;
using System.Linq.Expressions;

namespace System.Data.Entity.Core.Objects.ELinq
{
	// Token: 0x02000551 RID: 1361
	internal sealed class CompiledELinqQueryState : ELinqQueryState
	{
		// Token: 0x060034DA RID: 13530 RVA: 0x000F98F4 File Offset: 0x000F7AF4
		internal CompiledELinqQueryState(Type elementType, ObjectContext context, LambdaExpression lambda, Guid cacheToken, object[] parameterValues, ObjectQueryExecutionPlanFactory objectQueryExecutionPlanFactory = null) : base(elementType, context, lambda, null)
		{
			this._cacheToken = cacheToken;
			this._parameterValues = parameterValues;
			base.EnsureParameters();
			base.Parameters.SetReadOnly(true);
			this._objectQueryExecutionPlanFactory = (objectQueryExecutionPlanFactory ?? new ObjectQueryExecutionPlanFactory(null));
		}

		// Token: 0x060034DB RID: 13531 RVA: 0x000F9940 File Offset: 0x000F7B40
		internal override ObjectQueryExecutionPlan GetExecutionPlan(MergeOption? forMergeOption)
		{
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
					IEnumerable<Tuple<ObjectParameter, QueryParameterExpression>> parameters = expressionConverter.GetParameters();
					DbQueryCommandTree tree = DbQueryCommandTree.FromValidExpression(base.ObjectContext.MetadataWorkspace, DataSpace.CSpace, query, !useCSharpNullComparisonBehavior);
					objectQueryExecutionPlan = this._objectQueryExecutionPlanFactory.Prepare(base.ObjectContext, tree, base.ElementType, mergeOption, base.EffectiveStreamingBehavior, expressionConverter.PropagatedSpan, parameters, expressionConverter.AliasGenerator);
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
					IEnumerable<Tuple<ObjectParameter, QueryParameterExpression>> parameters2 = expressionConverter2.GetParameters();
					DbQueryCommandTree tree2 = DbQueryCommandTree.FromValidExpression(base.ObjectContext.MetadataWorkspace, DataSpace.CSpace, query2, !useCSharpNullComparisonBehavior);
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
						objectQueryExecutionPlan = this._objectQueryExecutionPlanFactory.Prepare(base.ObjectContext, tree2, base.ElementType, mergeOption3, base.EffectiveStreamingBehavior, expressionConverter2.PropagatedSpan, parameters2, expressionConverter2.AliasGenerator);
						objectQueryExecutionPlan = compiledQueryCacheEntry.SetExecutionPlan(objectQueryExecutionPlan, useCSharpNullComparisonBehavior);
					}
				}
			}
			ObjectParameterCollection objectParameterCollection = base.EnsureParameters();
			if (objectQueryExecutionPlan.CompiledQueryParameters != null && objectQueryExecutionPlan.CompiledQueryParameters.Any<Tuple<ObjectParameter, QueryParameterExpression>>())
			{
				objectParameterCollection.SetReadOnly(false);
				objectParameterCollection.Clear();
				foreach (Tuple<ObjectParameter, QueryParameterExpression> tuple in objectQueryExecutionPlan.CompiledQueryParameters)
				{
					ObjectParameter objectParameter = tuple.Item1.ShallowCopy();
					QueryParameterExpression item = tuple.Item2;
					objectParameterCollection.Add(objectParameter);
					if (item != null)
					{
						objectParameter.Value = item.EvaluateParameter(this._parameterValues);
					}
				}
			}
			objectParameterCollection.SetReadOnly(true);
			return objectQueryExecutionPlan;
		}

		// Token: 0x060034DC RID: 13532 RVA: 0x000F9C58 File Offset: 0x000F7E58
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

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x060034DD RID: 13533 RVA: 0x000F9C81 File Offset: 0x000F7E81
		internal override Expression Expression
		{
			get
			{
				return CompiledELinqQueryState.CreateDonateableExpressionVisitor.Replace((LambdaExpression)base.Expression, base.ObjectContext, this._parameterValues);
			}
		}

		// Token: 0x060034DE RID: 13534 RVA: 0x000F9CA0 File Offset: 0x000F7EA0
		protected override ExpressionConverter CreateExpressionConverter()
		{
			LambdaExpression lambdaExpression = (LambdaExpression)base.Expression;
			Funcletizer funcletizer = Funcletizer.CreateCompiledQueryEvaluationFuncletizer(base.ObjectContext, lambdaExpression.Parameters.First<ParameterExpression>(), new ReadOnlyCollection<ParameterExpression>(lambdaExpression.Parameters.Skip(1).ToList<ParameterExpression>()));
			return new ExpressionConverter(funcletizer, lambdaExpression.Body);
		}

		// Token: 0x040013C8 RID: 5064
		private readonly Guid _cacheToken;

		// Token: 0x040013C9 RID: 5065
		private readonly object[] _parameterValues;

		// Token: 0x040013CA RID: 5066
		private CompiledQueryCacheEntry _cacheEntry;

		// Token: 0x040013CB RID: 5067
		private readonly ObjectQueryExecutionPlanFactory _objectQueryExecutionPlanFactory;

		// Token: 0x02000552 RID: 1362
		private sealed class CreateDonateableExpressionVisitor : EntityExpressionVisitor
		{
			// Token: 0x060034DF RID: 13535 RVA: 0x000F9CF2 File Offset: 0x000F7EF2
			private CreateDonateableExpressionVisitor(Dictionary<ParameterExpression, object> parameterToValueLookup)
			{
				this._parameterToValueLookup = parameterToValueLookup;
			}

			// Token: 0x060034E0 RID: 13536 RVA: 0x000F9D14 File Offset: 0x000F7F14
			internal static Expression Replace(LambdaExpression query, ObjectContext objectContext, object[] parameterValues)
			{
				Dictionary<ParameterExpression, object> dictionary = query.Parameters.Skip(1).Zip(parameterValues).ToDictionary((KeyValuePair<ParameterExpression, object> pair) => pair.Key, (KeyValuePair<ParameterExpression, object> pair) => pair.Value);
				dictionary.Add(query.Parameters.First<ParameterExpression>(), objectContext);
				CompiledELinqQueryState.CreateDonateableExpressionVisitor createDonateableExpressionVisitor = new CompiledELinqQueryState.CreateDonateableExpressionVisitor(dictionary);
				return createDonateableExpressionVisitor.Visit(query.Body);
			}

			// Token: 0x060034E1 RID: 13537 RVA: 0x000F9D98 File Offset: 0x000F7F98
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

			// Token: 0x040013CC RID: 5068
			private readonly Dictionary<ParameterExpression, object> _parameterToValueLookup;
		}
	}
}
