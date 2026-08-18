using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.Internal.Materialization;
using System.Data.Common.QueryCache;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.EntityClient;
using System.Data.Metadata.Edm;
using System.Data.Objects.ELinq;

namespace System.Data.Objects.Internal
{
	// Token: 0x02000160 RID: 352
	internal sealed class ObjectQueryExecutionPlan
	{
		// Token: 0x06001A5D RID: 6749 RVA: 0x0005A696 File Offset: 0x00058896
		private ObjectQueryExecutionPlan(DbCommandDefinition commandDefinition, ShaperFactory resultShaperFactory, TypeUsage resultType, MergeOption mergeOption, EntitySet singleEntitySet, ReadOnlyCollection<KeyValuePair<ObjectParameter, QueryParameterExpression>> compiledQueryParameters)
		{
			this.CommandDefinition = commandDefinition;
			this.ResultShaperFactory = resultShaperFactory;
			this.ResultType = resultType;
			this.MergeOption = mergeOption;
			this._singleEntitySet = singleEntitySet;
			this.CompiledQueryParameters = compiledQueryParameters;
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x0005A6CC File Offset: 0x000588CC
		internal static ObjectQueryExecutionPlan Prepare(ObjectContext context, DbQueryCommandTree tree, Type elementType, MergeOption mergeOption, Span span, ReadOnlyCollection<KeyValuePair<ObjectParameter, QueryParameterExpression>> compiledQueryParameters, AliasGenerator aliasGenerator)
		{
			TypeUsage resultType = tree.Query.ResultType;
			DbExpression query = null;
			SpanIndex spanInfo;
			if (ObjectSpanRewriter.TryRewrite(tree, span, mergeOption, aliasGenerator, out query, out spanInfo))
			{
				tree = DbQueryCommandTree.FromValidExpression(tree.MetadataWorkspace, tree.DataSpace, query);
			}
			else
			{
				spanInfo = null;
			}
			DbConnection connection = context.Connection;
			DbCommandDefinition dbCommandDefinition = null;
			if (connection == null)
			{
				throw EntityUtil.InvalidOperation(Strings.ObjectQuery_InvalidConnection);
			}
			DbProviderServices providerServices = DbProviderServices.GetProviderServices(connection);
			try
			{
				dbCommandDefinition = providerServices.CreateCommandDefinition(tree);
			}
			catch (EntityCommandCompilationException)
			{
				throw;
			}
			catch (Exception ex)
			{
				if (EntityUtil.IsCatchableExceptionType(ex))
				{
					throw EntityUtil.CommandCompilation(Strings.EntityClient_CommandDefinitionPreparationFailed, ex);
				}
				throw;
			}
			if (dbCommandDefinition == null)
			{
				throw EntityUtil.ProviderDoesNotSupportCommandTrees();
			}
			EntityCommandDefinition entityCommandDefinition = (EntityCommandDefinition)dbCommandDefinition;
			QueryCacheManager queryCacheManager = context.Perspective.MetadataWorkspace.GetQueryCacheManager();
			ShaperFactory resultShaperFactory = ShaperFactory.Create(elementType, queryCacheManager, entityCommandDefinition.CreateColumnMap(null), context.MetadataWorkspace, spanInfo, mergeOption, false);
			EntitySet entitySet = null;
			if (resultType.EdmType.BuiltInTypeKind == BuiltInTypeKind.CollectionType && entityCommandDefinition.EntitySets != null)
			{
				foreach (EntitySet entitySet2 in entityCommandDefinition.EntitySets)
				{
					if (entitySet2 != null && entitySet2.ElementType.IsAssignableFrom(((CollectionType)resultType.EdmType).TypeUsage.EdmType))
					{
						if (entitySet != null)
						{
							entitySet = null;
							break;
						}
						entitySet = entitySet2;
					}
				}
			}
			return new ObjectQueryExecutionPlan(dbCommandDefinition, resultShaperFactory, resultType, mergeOption, entitySet, compiledQueryParameters);
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x0005A854 File Offset: 0x00058A54
		internal string ToTraceString()
		{
			string result = string.Empty;
			EntityCommandDefinition entityCommandDefinition = this.CommandDefinition as EntityCommandDefinition;
			if (entityCommandDefinition != null)
			{
				result = entityCommandDefinition.ToTraceString();
			}
			return result;
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x0005A880 File Offset: 0x00058A80
		internal ObjectResult<TResultType> Execute<TResultType>(ObjectContext context, ObjectParameterCollection parameterValues)
		{
			DbDataReader dbDataReader = null;
			ObjectResult<TResultType> result;
			try
			{
				EntityCommandDefinition entityCommandDefinition = (EntityCommandDefinition)this.CommandDefinition;
				EntityCommand entityCommand = new EntityCommand((EntityConnection)context.Connection, entityCommandDefinition);
				if (context.CommandTimeout != null)
				{
					entityCommand.CommandTimeout = context.CommandTimeout.Value;
				}
				if (parameterValues != null)
				{
					foreach (ObjectParameter objectParameter in ((IEnumerable<ObjectParameter>)parameterValues))
					{
						int num = entityCommand.Parameters.IndexOf(objectParameter.Name);
						if (num != -1)
						{
							entityCommand.Parameters[num].Value = (objectParameter.Value ?? DBNull.Value);
						}
					}
				}
				dbDataReader = entityCommandDefinition.ExecuteStoreCommands(entityCommand, CommandBehavior.Default);
				ShaperFactory<TResultType> shaperFactory = (ShaperFactory<TResultType>)this.ResultShaperFactory;
				Shaper<TResultType> shaper = shaperFactory.Create(dbDataReader, context, context.MetadataWorkspace, this.MergeOption, true);
				TypeUsage resultItemType;
				if (this.ResultType.EdmType.BuiltInTypeKind == BuiltInTypeKind.CollectionType)
				{
					resultItemType = ((CollectionType)this.ResultType.EdmType).TypeUsage;
				}
				else
				{
					resultItemType = this.ResultType;
				}
				result = new ObjectResult<TResultType>(shaper, this._singleEntitySet, resultItemType);
			}
			catch (Exception)
			{
				if (dbDataReader != null)
				{
					dbDataReader.Dispose();
				}
				throw;
			}
			return result;
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x0005A9F0 File Offset: 0x00058BF0
		internal static ObjectResult<TResultType> ExecuteCommandTree<TResultType>(ObjectContext context, DbQueryCommandTree query, MergeOption mergeOption)
		{
			ObjectQueryExecutionPlan objectQueryExecutionPlan = ObjectQueryExecutionPlan.Prepare(context, query, typeof(TResultType), mergeOption, null, null, DbExpressionBuilder.AliasGenerator);
			return objectQueryExecutionPlan.Execute<TResultType>(context, null);
		}

		// Token: 0x04000B11 RID: 2833
		internal readonly DbCommandDefinition CommandDefinition;

		// Token: 0x04000B12 RID: 2834
		internal readonly ShaperFactory ResultShaperFactory;

		// Token: 0x04000B13 RID: 2835
		internal readonly TypeUsage ResultType;

		// Token: 0x04000B14 RID: 2836
		internal readonly MergeOption MergeOption;

		// Token: 0x04000B15 RID: 2837
		internal readonly ReadOnlyCollection<KeyValuePair<ObjectParameter, QueryParameterExpression>> CompiledQueryParameters;

		// Token: 0x04000B16 RID: 2838
		private readonly EntitySet _singleEntitySet;
	}
}
