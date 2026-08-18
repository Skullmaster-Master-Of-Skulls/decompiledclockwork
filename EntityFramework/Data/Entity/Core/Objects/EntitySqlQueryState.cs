using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.EntitySql;
using System.Data.Entity.Core.Common.QueryCache;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Utilities;
using System.Linq.Expressions;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x0200057F RID: 1407
	internal sealed class EntitySqlQueryState : ObjectQueryState
	{
		// Token: 0x06003704 RID: 14084 RVA: 0x00105698 File Offset: 0x00103898
		internal EntitySqlQueryState(Type elementType, string commandText, bool allowsLimit, ObjectContext context, ObjectParameterCollection parameters, Span span) : this(elementType, commandText, null, allowsLimit, context, parameters, span, null)
		{
		}

		// Token: 0x06003705 RID: 14085 RVA: 0x001056B8 File Offset: 0x001038B8
		internal EntitySqlQueryState(Type elementType, string commandText, DbExpression expression, bool allowsLimit, ObjectContext context, ObjectParameterCollection parameters, Span span, ObjectQueryExecutionPlanFactory objectQueryExecutionPlanFactory = null) : base(elementType, context, parameters, span)
		{
			Check.NotEmpty(commandText, "commandText");
			this._queryText = commandText;
			this._queryExpression = expression;
			this._allowsLimit = allowsLimit;
			this._objectQueryExecutionPlanFactory = (objectQueryExecutionPlanFactory ?? new ObjectQueryExecutionPlanFactory(null));
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06003706 RID: 14086 RVA: 0x00105706 File Offset: 0x00103906
		internal bool AllowsLimitSubclause
		{
			get
			{
				return this._allowsLimit;
			}
		}

		// Token: 0x06003707 RID: 14087 RVA: 0x0010570E File Offset: 0x0010390E
		internal override bool TryGetCommandText(out string commandText)
		{
			commandText = this._queryText;
			return true;
		}

		// Token: 0x06003708 RID: 14088 RVA: 0x00105719 File Offset: 0x00103919
		internal override bool TryGetExpression(out Expression expression)
		{
			expression = null;
			return false;
		}

		// Token: 0x06003709 RID: 14089 RVA: 0x00105720 File Offset: 0x00103920
		protected override TypeUsage GetResultType()
		{
			DbExpression dbExpression = this.Parse();
			return dbExpression.ResultType;
		}

		// Token: 0x0600370A RID: 14090 RVA: 0x0010573C File Offset: 0x0010393C
		internal override ObjectQueryState Include<TElementType>(ObjectQuery<TElementType> sourceQuery, string includePath)
		{
			ObjectQueryState objectQueryState = new EntitySqlQueryState(base.ElementType, this._queryText, this._queryExpression, this._allowsLimit, base.ObjectContext, ObjectParameterCollection.DeepCopy(base.Parameters), Span.IncludeIn(base.Span, includePath), null);
			base.ApplySettingsTo(objectQueryState);
			return objectQueryState;
		}

		// Token: 0x0600370B RID: 14091 RVA: 0x00105790 File Offset: 0x00103990
		internal override ObjectQueryExecutionPlan GetExecutionPlan(MergeOption? forMergeOption)
		{
			MergeOption mergeOption = ObjectQueryState.EnsureMergeOption(new MergeOption?[]
			{
				forMergeOption,
				base.UserSpecifiedMergeOption
			});
			ObjectQueryExecutionPlan objectQueryExecutionPlan = this._cachedPlan;
			if (objectQueryExecutionPlan != null)
			{
				if (objectQueryExecutionPlan.MergeOption == mergeOption && objectQueryExecutionPlan.Streaming == base.EffectiveStreamingBehavior)
				{
					return objectQueryExecutionPlan;
				}
				objectQueryExecutionPlan = null;
			}
			QueryCacheManager queryCacheManager = null;
			EntitySqlQueryCacheKey entitySqlQueryCacheKey = null;
			if (base.PlanCachingEnabled)
			{
				entitySqlQueryCacheKey = new EntitySqlQueryCacheKey(base.ObjectContext.DefaultContainerName, this._queryText, (base.Parameters == null) ? 0 : base.Parameters.Count, (base.Parameters == null) ? null : base.Parameters.GetCacheKey(), (base.Span == null) ? null : base.Span.GetCacheKey(), mergeOption, base.EffectiveStreamingBehavior, base.ElementType);
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
				DbQueryCommandTree tree = DbQueryCommandTree.FromValidExpression(base.ObjectContext.MetadataWorkspace, DataSpace.CSpace, query, true);
				objectQueryExecutionPlan = this._objectQueryExecutionPlanFactory.Prepare(base.ObjectContext, tree, base.ElementType, mergeOption, base.EffectiveStreamingBehavior, base.Span, null, DbExpressionBuilder.AliasGenerator);
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

		// Token: 0x0600370C RID: 14092 RVA: 0x0010591C File Offset: 0x00103B1C
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
				foreach (ObjectParameter objectParameter in base.Parameters)
				{
					TypeUsage typeUsage = objectParameter.TypeUsage;
					if (typeUsage == null)
					{
						base.ObjectContext.Perspective.TryGetTypeByName(objectParameter.MappableType.FullNameWithNesting(), false, out typeUsage);
					}
					list.Add(typeUsage.Parameter(objectParameter.Name));
				}
			}
			DbLambda dbLambda = CqlQuery.CompileQueryCommandLambda(this._queryText, base.ObjectContext.Perspective, null, list, null);
			return dbLambda.Body;
		}

		// Token: 0x0400152B RID: 5419
		private readonly string _queryText;

		// Token: 0x0400152C RID: 5420
		private readonly DbExpression _queryExpression;

		// Token: 0x0400152D RID: 5421
		private readonly bool _allowsLimit;

		// Token: 0x0400152E RID: 5422
		private readonly ObjectQueryExecutionPlanFactory _objectQueryExecutionPlanFactory;
	}
}
