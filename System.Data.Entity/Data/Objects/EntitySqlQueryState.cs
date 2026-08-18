using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.EntitySql;
using System.Data.Common.QueryCache;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Data.Objects.Internal;
using System.Linq.Expressions;

namespace System.Data.Objects
{
	// Token: 0x0200012C RID: 300
	internal sealed class EntitySqlQueryState : ObjectQueryState
	{
		// Token: 0x060015ED RID: 5613 RVA: 0x00049EBF File Offset: 0x000480BF
		internal EntitySqlQueryState(Type elementType, string commandText, bool allowsLimit, ObjectContext context, ObjectParameterCollection parameters, Span span) : this(elementType, commandText, null, allowsLimit, context, parameters, span)
		{
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x00049ED4 File Offset: 0x000480D4
		internal EntitySqlQueryState(Type elementType, string commandText, DbExpression expression, bool allowsLimit, ObjectContext context, ObjectParameterCollection parameters, Span span) : base(elementType, context, parameters, span)
		{
			EntityUtil.CheckArgumentNull<string>(commandText, "commandText");
			if (string.IsNullOrEmpty(commandText))
			{
				throw EntityUtil.Argument(Strings.ObjectQuery_InvalidEmptyQuery, "commandText");
			}
			this._queryText = commandText;
			this._queryExpression = expression;
			this._allowsLimit = allowsLimit;
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x060015EF RID: 5615 RVA: 0x00049F28 File Offset: 0x00048128
		internal bool AllowsLimitSubclause
		{
			get
			{
				return this._allowsLimit;
			}
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x00049F30 File Offset: 0x00048130
		internal override bool TryGetCommandText(out string commandText)
		{
			commandText = this._queryText;
			return true;
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x0003D10D File Offset: 0x0003B30D
		internal override bool TryGetExpression(out Expression expression)
		{
			expression = null;
			return false;
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x00049F3C File Offset: 0x0004813C
		protected override TypeUsage GetResultType()
		{
			DbExpression dbExpression = this.Parse();
			return dbExpression.ResultType;
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x00049F58 File Offset: 0x00048158
		internal override ObjectQueryState Include<TElementType>(ObjectQuery<TElementType> sourceQuery, string includePath)
		{
			ObjectQueryState objectQueryState = new EntitySqlQueryState(base.ElementType, this._queryText, this._queryExpression, this._allowsLimit, base.ObjectContext, ObjectParameterCollection.DeepCopy(base.Parameters), Span.IncludeIn(base.Span, includePath));
			base.ApplySettingsTo(objectQueryState);
			return objectQueryState;
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x00049FA8 File Offset: 0x000481A8
		internal override ObjectQueryExecutionPlan GetExecutionPlan(MergeOption? forMergeOption)
		{
			base.ObjectContext.EnsureMetadata();
			MergeOption mergeOption = ObjectQueryState.EnsureMergeOption(new MergeOption?[]
			{
				forMergeOption,
				base.UserSpecifiedMergeOption
			});
			ObjectQueryExecutionPlan objectQueryExecutionPlan = this._cachedPlan;
			if (objectQueryExecutionPlan != null)
			{
				if (objectQueryExecutionPlan.MergeOption == mergeOption)
				{
					return objectQueryExecutionPlan;
				}
				objectQueryExecutionPlan = null;
			}
			QueryCacheManager queryCacheManager = null;
			EntitySqlQueryCacheKey entitySqlQueryCacheKey = null;
			if (base.PlanCachingEnabled)
			{
				entitySqlQueryCacheKey = new EntitySqlQueryCacheKey(base.ObjectContext.DefaultContainerName, this._queryText, (base.Parameters == null) ? 0 : base.Parameters.Count, (base.Parameters == null) ? null : base.Parameters.GetCacheKey(), (base.Span == null) ? null : base.Span.GetCacheKey(), mergeOption, base.ElementType);
				queryCacheManager = base.ObjectContext.MetadataWorkspace.GetQueryCacheManager();
				ObjectQueryExecutionPlan objectQueryExecutionPlan2 = null;
				if (queryCacheManager.TryCacheLookup<EntitySqlQueryCacheKey, ObjectQueryExecutionPlan>(entitySqlQueryCacheKey, out objectQueryExecutionPlan2))
				{
					objectQueryExecutionPlan = objectQueryExecutionPlan2;
				}
			}
			if (objectQueryExecutionPlan == null)
			{
				DbExpression query = this.Parse();
				DbQueryCommandTree tree = DbQueryCommandTree.FromValidExpression(base.ObjectContext.MetadataWorkspace, DataSpace.CSpace, query);
				objectQueryExecutionPlan = ObjectQueryExecutionPlan.Prepare(base.ObjectContext, tree, base.ElementType, mergeOption, base.Span, null, DbExpressionBuilder.AliasGenerator);
				if (entitySqlQueryCacheKey != null)
				{
					QueryCacheEntry inQueryCacheEntry = new QueryCacheEntry(entitySqlQueryCacheKey, objectQueryExecutionPlan);
					QueryCacheEntry queryCacheEntry = null;
					if (queryCacheManager.TryLookupAndAdd(inQueryCacheEntry, out queryCacheEntry))
					{
						objectQueryExecutionPlan = (ObjectQueryExecutionPlan)queryCacheEntry.GetTarget();
					}
				}
			}
			if (base.Parameters != null)
			{
				base.Parameters.SetReadOnly(true);
			}
			this._cachedPlan = objectQueryExecutionPlan;
			return objectQueryExecutionPlan;
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x0004A110 File Offset: 0x00048310
		internal DbExpression Parse()
		{
			if (this._queryExpression != null)
			{
				return this._queryExpression;
			}
			List<DbParameterReferenceExpression> list = null;
			if (base.Parameters != null)
			{
				list = new List<DbParameterReferenceExpression>(base.Parameters.Count);
				foreach (ObjectParameter objectParameter in ((IEnumerable<ObjectParameter>)base.Parameters))
				{
					TypeUsage typeUsage = objectParameter.TypeUsage;
					if (typeUsage == null)
					{
						base.ObjectContext.Perspective.TryGetTypeByName(objectParameter.MappableType.FullName, false, out typeUsage);
					}
					list.Add(typeUsage.Parameter(objectParameter.Name));
				}
			}
			DbLambda dbLambda = CqlQuery.CompileQueryCommandLambda(this._queryText, base.ObjectContext.Perspective, null, list, null);
			return dbLambda.Body;
		}

		// Token: 0x04000A41 RID: 2625
		private readonly string _queryText;

		// Token: 0x04000A42 RID: 2626
		private readonly DbExpression _queryExpression;

		// Token: 0x04000A43 RID: 2627
		private readonly bool _allowsLimit;
	}
}
