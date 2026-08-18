using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.Internal;
using System.Data.Common.QueryCache;
using System.Data.Metadata.Edm;
using System.Data.Objects.Internal;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Objects.ELinq
{
	// Token: 0x0200019D RID: 413
	internal class ELinqQueryState : ObjectQueryState
	{
		// Token: 0x06001E37 RID: 7735 RVA: 0x000682B6 File Offset: 0x000664B6
		internal ELinqQueryState(Type elementType, ObjectContext context, Expression expression) : base(elementType, context, null, null)
		{
			EntityUtil.CheckArgumentNull<Expression>(expression, "expression");
			this._expression = expression;
			this._useCSharpNullComparisonBehavior = context.ContextOptions.UseCSharpNullComparisonBehavior;
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x000682E6 File Offset: 0x000664E6
		internal ELinqQueryState(Type elementType, ObjectQuery query, Expression expression) : base(elementType, query)
		{
			EntityUtil.CheckArgumentNull<Expression>(expression, "expression");
			this._expression = expression;
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x00068304 File Offset: 0x00066504
		protected override TypeUsage GetResultType()
		{
			ExpressionConverter expressionConverter = this.CreateExpressionConverter();
			return expressionConverter.Convert().ResultType;
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x00068324 File Offset: 0x00066524
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
				base.ObjectContext.EnsureMetadata();
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
				if (this._linqParameters != null && this._linqParameters.Count > 0)
				{
					ObjectParameterCollection objectParameterCollection = base.EnsureParameters();
					objectParameterCollection.SetReadOnly(false);
					foreach (KeyValuePair<ObjectParameter, QueryParameterExpression> keyValuePair in this._linqParameters)
					{
						ObjectParameter key = keyValuePair.Key;
						objectParameterCollection.Add(key);
					}
					objectParameterCollection.SetReadOnly(true);
				}
				QueryCacheManager queryCacheManager = null;
				LinqQueryCacheKey linqQueryCacheKey = null;
				string expressionKey;
				if (base.PlanCachingEnabled && !this._recompileRequired() && ExpressionKeyGen.TryGenerateKey(dbExpression, out expressionKey))
				{
					linqQueryCacheKey = new LinqQueryCacheKey(expressionKey, (base.Parameters == null) ? 0 : base.Parameters.Count, (base.Parameters == null) ? null : base.Parameters.GetCacheKey(), (expressionConverter.PropagatedSpan == null) ? null : expressionConverter.PropagatedSpan.GetCacheKey(), mergeOption2, this._useCSharpNullComparisonBehavior, base.ElementType);
					queryCacheManager = base.ObjectContext.MetadataWorkspace.GetQueryCacheManager();
					ObjectQueryExecutionPlan objectQueryExecutionPlan2 = null;
					if (queryCacheManager.TryCacheLookup<LinqQueryCacheKey, ObjectQueryExecutionPlan>(linqQueryCacheKey, out objectQueryExecutionPlan2))
					{
						objectQueryExecutionPlan = objectQueryExecutionPlan2;
					}
				}
				if (objectQueryExecutionPlan == null)
				{
					DbQueryCommandTree tree = DbQueryCommandTree.FromValidExpression(base.ObjectContext.MetadataWorkspace, DataSpace.CSpace, dbExpression);
					objectQueryExecutionPlan = ObjectQueryExecutionPlan.Prepare(base.ObjectContext, tree, base.ElementType, mergeOption2, expressionConverter.PropagatedSpan, null, expressionConverter.AliasGenerator);
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
				foreach (KeyValuePair<ObjectParameter, QueryParameterExpression> keyValuePair2 in this._linqParameters)
				{
					ObjectParameter key2 = keyValuePair2.Key;
					QueryParameterExpression value = keyValuePair2.Value;
					if (value != null)
					{
						key2.Value = value.EvaluateParameter(null);
					}
				}
			}
			return objectQueryExecutionPlan;
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x00068624 File Offset: 0x00066824
		internal override ObjectQueryState Include<TElementType>(ObjectQuery<TElementType> sourceQuery, string includePath)
		{
			MethodInfo method = sourceQuery.GetType().GetMethod("Include", BindingFlags.Instance | BindingFlags.Public);
			Expression expression = Expression.Call(Expression.Constant(sourceQuery), method, new Expression[]
			{
				Expression.Constant(includePath, typeof(string))
			});
			ObjectQueryState objectQueryState = new ELinqQueryState(base.ElementType, base.ObjectContext, expression);
			base.ApplySettingsTo(objectQueryState);
			return objectQueryState;
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x0003D10D File Offset: 0x0003B30D
		internal override bool TryGetCommandText(out string commandText)
		{
			commandText = null;
			return false;
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x00068685 File Offset: 0x00066885
		internal override bool TryGetExpression(out Expression expression)
		{
			expression = this.Expression;
			return true;
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001E3E RID: 7742 RVA: 0x00068690 File Offset: 0x00066890
		internal virtual Expression Expression
		{
			get
			{
				return this._expression;
			}
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x00068698 File Offset: 0x00066898
		protected virtual ExpressionConverter CreateExpressionConverter()
		{
			Funcletizer funcletizer = Funcletizer.CreateQueryFuncletizer(base.ObjectContext);
			return new ExpressionConverter(funcletizer, this._expression);
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x000686C0 File Offset: 0x000668C0
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

		// Token: 0x04000C0F RID: 3087
		private readonly Expression _expression;

		// Token: 0x04000C10 RID: 3088
		private Func<bool> _recompileRequired;

		// Token: 0x04000C11 RID: 3089
		private ReadOnlyCollection<KeyValuePair<ObjectParameter, QueryParameterExpression>> _linqParameters;

		// Token: 0x04000C12 RID: 3090
		private bool _useCSharpNullComparisonBehavior;
	}
}
