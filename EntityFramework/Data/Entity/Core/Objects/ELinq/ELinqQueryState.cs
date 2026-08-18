using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Common.QueryCache;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Core.Objects.ELinq
{
	// Token: 0x02000550 RID: 1360
	internal class ELinqQueryState : ObjectQueryState
	{
		// Token: 0x060034CF RID: 13519 RVA: 0x000F9440 File Offset: 0x000F7640
		internal ELinqQueryState(Type elementType, ObjectContext context, Expression expression, ObjectQueryExecutionPlanFactory objectQueryExecutionPlanFactory = null) : base(elementType, context, null, null)
		{
			this._expression = expression;
			this._useCSharpNullComparisonBehavior = context.ContextOptions.UseCSharpNullComparisonBehavior;
			this._objectQueryExecutionPlanFactory = (objectQueryExecutionPlanFactory ?? new ObjectQueryExecutionPlanFactory(null));
		}

		// Token: 0x060034D0 RID: 13520 RVA: 0x000F9476 File Offset: 0x000F7676
		internal ELinqQueryState(Type elementType, ObjectQuery query, Expression expression, ObjectQueryExecutionPlanFactory objectQueryExecutionPlanFactory = null) : base(elementType, query)
		{
			this._expression = expression;
			this._objectQueryExecutionPlanFactory = (objectQueryExecutionPlanFactory ?? new ObjectQueryExecutionPlanFactory(null));
		}

		// Token: 0x060034D1 RID: 13521 RVA: 0x000F949C File Offset: 0x000F769C
		protected override TypeUsage GetResultType()
		{
			ExpressionConverter expressionConverter = this.CreateExpressionConverter();
			return expressionConverter.Convert().ResultType;
		}

		// Token: 0x060034D2 RID: 13522 RVA: 0x000F94BC File Offset: 0x000F76BC
		internal override ObjectQueryExecutionPlan GetExecutionPlan(MergeOption? forMergeOption)
		{
			ObjectQueryExecutionPlan objectQueryExecutionPlan = this._cachedPlan;
			if (objectQueryExecutionPlan != null)
			{
				MergeOption? mergeOption = ObjectQueryState.GetMergeOption(new MergeOption?[]
				{
					forMergeOption,
					base.UserSpecifiedMergeOption
				});
				if ((mergeOption != null && mergeOption.Value != objectQueryExecutionPlan.MergeOption) || this._recompileRequired() || base.ObjectContext.ContextOptions.UseCSharpNullComparisonBehavior != this._useCSharpNullComparisonBehavior)
				{
					objectQueryExecutionPlan = null;
				}
			}
			if (objectQueryExecutionPlan == null)
			{
				this._recompileRequired = null;
				this.ResetParameters();
				ExpressionConverter expressionConverter = this.CreateExpressionConverter();
				DbExpression dbExpression = expressionConverter.Convert();
				this._recompileRequired = expressionConverter.RecompileRequired;
				MergeOption mergeOption2 = ObjectQueryState.EnsureMergeOption(new MergeOption?[]
				{
					forMergeOption,
					base.UserSpecifiedMergeOption,
					expressionConverter.PropagatedMergeOption
				});
				this._useCSharpNullComparisonBehavior = base.ObjectContext.ContextOptions.UseCSharpNullComparisonBehavior;
				this._linqParameters = expressionConverter.GetParameters();
				if (this._linqParameters != null && this._linqParameters.Any<Tuple<ObjectParameter, QueryParameterExpression>>())
				{
					ObjectParameterCollection objectParameterCollection = base.EnsureParameters();
					objectParameterCollection.SetReadOnly(false);
					foreach (Tuple<ObjectParameter, QueryParameterExpression> tuple in this._linqParameters)
					{
						ObjectParameter item = tuple.Item1;
						objectParameterCollection.Add(item);
					}
					objectParameterCollection.SetReadOnly(true);
				}
				QueryCacheManager queryCacheManager = null;
				LinqQueryCacheKey linqQueryCacheKey = null;
				string expressionKey;
				if (base.PlanCachingEnabled && !this._recompileRequired() && ExpressionKeyGen.TryGenerateKey(dbExpression, out expressionKey))
				{
					linqQueryCacheKey = new LinqQueryCacheKey(expressionKey, (base.Parameters == null) ? 0 : base.Parameters.Count, (base.Parameters == null) ? null : base.Parameters.GetCacheKey(), (expressionConverter.PropagatedSpan == null) ? null : expressionConverter.PropagatedSpan.GetCacheKey(), mergeOption2, base.EffectiveStreamingBehavior, this._useCSharpNullComparisonBehavior, base.ElementType);
					queryCacheManager = base.ObjectContext.MetadataWorkspace.GetQueryCacheManager();
					ObjectQueryExecutionPlan objectQueryExecutionPlan2 = null;
					if (queryCacheManager.TryCacheLookup<LinqQueryCacheKey, ObjectQueryExecutionPlan>(linqQueryCacheKey, out objectQueryExecutionPlan2))
					{
						objectQueryExecutionPlan = objectQueryExecutionPlan2;
					}
				}
				if (objectQueryExecutionPlan == null)
				{
					DbQueryCommandTree tree = DbQueryCommandTree.FromValidExpression(base.ObjectContext.MetadataWorkspace, DataSpace.CSpace, dbExpression, !this._useCSharpNullComparisonBehavior);
					objectQueryExecutionPlan = this._objectQueryExecutionPlanFactory.Prepare(base.ObjectContext, tree, base.ElementType, mergeOption2, base.EffectiveStreamingBehavior, expressionConverter.PropagatedSpan, null, expressionConverter.AliasGenerator);
					if (linqQueryCacheKey != null)
					{
						QueryCacheEntry inQueryCacheEntry = new QueryCacheEntry(linqQueryCacheKey, objectQueryExecutionPlan);
						QueryCacheEntry queryCacheEntry = null;
						if (queryCacheManager.TryLookupAndAdd(inQueryCacheEntry, out queryCacheEntry))
						{
							objectQueryExecutionPlan = (ObjectQueryExecutionPlan)queryCacheEntry.GetTarget();
						}
					}
				}
				this._cachedPlan = objectQueryExecutionPlan;
			}
			if (this._linqParameters != null)
			{
				foreach (Tuple<ObjectParameter, QueryParameterExpression> tuple2 in this._linqParameters)
				{
					ObjectParameter item2 = tuple2.Item1;
					QueryParameterExpression item3 = tuple2.Item2;
					if (item3 != null)
					{
						item2.Value = item3.EvaluateParameter(null);
					}
				}
			}
			return objectQueryExecutionPlan;
		}

		// Token: 0x060034D3 RID: 13523 RVA: 0x000F97F4 File Offset: 0x000F79F4
		internal override ObjectQueryState Include<TElementType>(ObjectQuery<TElementType> sourceQuery, string includePath)
		{
			MethodInfo includeMethod = ELinqQueryState.GetIncludeMethod<TElementType>(sourceQuery);
			Expression expression = Expression.Call(Expression.Constant(sourceQuery), includeMethod, new Expression[]
			{
				Expression.Constant(includePath, typeof(string))
			});
			ObjectQueryState objectQueryState = new ELinqQueryState(base.ElementType, base.ObjectContext, expression, null);
			base.ApplySettingsTo(objectQueryState);
			return objectQueryState;
		}

		// Token: 0x060034D4 RID: 13524 RVA: 0x000F984C File Offset: 0x000F7A4C
		internal static MethodInfo GetIncludeMethod<TElementType>(ObjectQuery<TElementType> sourceQuery)
		{
			return sourceQuery.GetType().GetOnlyDeclaredMethod("Include");
		}

		// Token: 0x060034D5 RID: 13525 RVA: 0x000F985E File Offset: 0x000F7A5E
		internal override bool TryGetCommandText(out string commandText)
		{
			commandText = null;
			return false;
		}

		// Token: 0x060034D6 RID: 13526 RVA: 0x000F9864 File Offset: 0x000F7A64
		internal override bool TryGetExpression(out Expression expression)
		{
			expression = this.Expression;
			return true;
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x060034D7 RID: 13527 RVA: 0x000F986F File Offset: 0x000F7A6F
		internal virtual Expression Expression
		{
			get
			{
				return this._expression;
			}
		}

		// Token: 0x060034D8 RID: 13528 RVA: 0x000F9878 File Offset: 0x000F7A78
		protected virtual ExpressionConverter CreateExpressionConverter()
		{
			Funcletizer funcletizer = Funcletizer.CreateQueryFuncletizer(base.ObjectContext);
			return new ExpressionConverter(funcletizer, this._expression);
		}

		// Token: 0x060034D9 RID: 13529 RVA: 0x000F98A0 File Offset: 0x000F7AA0
		private void ResetParameters()
		{
			if (base.Parameters != null)
			{
				bool isReadOnly = ((ICollection<ObjectParameter>)base.Parameters).IsReadOnly;
				if (isReadOnly)
				{
					base.Parameters.SetReadOnly(false);
				}
				base.Parameters.Clear();
				if (isReadOnly)
				{
					base.Parameters.SetReadOnly(true);
				}
			}
			this._linqParameters = null;
		}

		// Token: 0x040013C3 RID: 5059
		private readonly Expression _expression;

		// Token: 0x040013C4 RID: 5060
		private Func<bool> _recompileRequired;

		// Token: 0x040013C5 RID: 5061
		private IEnumerable<Tuple<ObjectParameter, QueryParameterExpression>> _linqParameters;

		// Token: 0x040013C6 RID: 5062
		private bool _useCSharpNullComparisonBehavior;

		// Token: 0x040013C7 RID: 5063
		private readonly ObjectQueryExecutionPlanFactory _objectQueryExecutionPlanFactory;
	}
}
