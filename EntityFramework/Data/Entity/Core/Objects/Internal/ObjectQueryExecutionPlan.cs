using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.Internal.Materialization;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.EntityClient.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.ELinq;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000595 RID: 1429
	internal class ObjectQueryExecutionPlan
	{
		// Token: 0x060037D9 RID: 14297 RVA: 0x00108BC7 File Offset: 0x00106DC7
		public ObjectQueryExecutionPlan(DbCommandDefinition commandDefinition, ShaperFactory resultShaperFactory, TypeUsage resultType, MergeOption mergeOption, bool streaming, EntitySet singleEntitySet, IEnumerable<Tuple<ObjectParameter, QueryParameterExpression>> compiledQueryParameters)
		{
			this.CommandDefinition = commandDefinition;
			this.ResultShaperFactory = resultShaperFactory;
			this.ResultType = resultType;
			this.MergeOption = mergeOption;
			this.Streaming = streaming;
			this._singleEntitySet = singleEntitySet;
			this.CompiledQueryParameters = compiledQueryParameters;
		}

		// Token: 0x060037DA RID: 14298 RVA: 0x00108C04 File Offset: 0x00106E04
		internal string ToTraceString()
		{
			EntityCommandDefinition entityCommandDefinition = this.CommandDefinition as EntityCommandDefinition;
			if (entityCommandDefinition == null)
			{
				return string.Empty;
			}
			return entityCommandDefinition.ToTraceString();
		}

		// Token: 0x060037DB RID: 14299 RVA: 0x00108C2C File Offset: 0x00106E2C
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Buffer disposed by the returned ObjectResult")]
		internal virtual ObjectResult<TResultType> Execute<TResultType>(ObjectContext context, ObjectParameterCollection parameterValues)
		{
			DbDataReader dbDataReader = null;
			BufferedDataReader bufferedDataReader = null;
			ObjectResult<TResultType> result;
			try
			{
				using (EntityCommand entityCommand = this.PrepareEntityCommand(context, parameterValues))
				{
					dbDataReader = entityCommand.GetCommandDefinition().ExecuteStoreCommands(entityCommand, this.Streaming ? CommandBehavior.Default : CommandBehavior.SequentialAccess);
				}
				ShaperFactory<TResultType> shaperFactory = (ShaperFactory<TResultType>)this.ResultShaperFactory;
				Shaper<TResultType> shaper;
				if (this.Streaming)
				{
					shaper = shaperFactory.Create(dbDataReader, context, context.MetadataWorkspace, this.MergeOption, true, this.Streaming);
				}
				else
				{
					StoreItemCollection storeItemCollection = (StoreItemCollection)context.MetadataWorkspace.GetItemCollection(DataSpace.SSpace);
					DbProviderServices service = DbConfiguration.DependencyResolver.GetService(storeItemCollection.ProviderInvariantName);
					bufferedDataReader = new BufferedDataReader(dbDataReader);
					bufferedDataReader.Initialize(storeItemCollection.ProviderManifestToken, service, shaperFactory.ColumnTypes, shaperFactory.NullableColumns);
					shaper = shaperFactory.Create(bufferedDataReader, context, context.MetadataWorkspace, this.MergeOption, true, this.Streaming);
				}
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
				if (this.Streaming && dbDataReader != null)
				{
					dbDataReader.Dispose();
				}
				if (!this.Streaming && bufferedDataReader != null)
				{
					bufferedDataReader.Dispose();
				}
				throw;
			}
			return result;
		}

		// Token: 0x060037DC RID: 14300 RVA: 0x001091A8 File Offset: 0x001073A8
		internal virtual async Task<ObjectResult<TResultType>> ExecuteAsync<TResultType>(ObjectContext context, ObjectParameterCollection parameterValues, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			DbDataReader storeReader = null;
			BufferedDataReader bufferedReader = null;
			ObjectResult<TResultType> result;
			try
			{
				using (EntityCommand entityCommand = this.PrepareEntityCommand(context, parameterValues))
				{
					storeReader = await entityCommand.GetCommandDefinition().ExecuteStoreCommandsAsync(entityCommand, this.Streaming ? CommandBehavior.Default : CommandBehavior.SequentialAccess, cancellationToken).WithCurrentCulture<DbDataReader>();
				}
				ShaperFactory<TResultType> shaperFactory = (ShaperFactory<TResultType>)this.ResultShaperFactory;
				Shaper<TResultType> shaper;
				if (this.Streaming)
				{
					shaper = shaperFactory.Create(storeReader, context, context.MetadataWorkspace, this.MergeOption, true, this.Streaming);
				}
				else
				{
					StoreItemCollection storeItemCollection = (StoreItemCollection)context.MetadataWorkspace.GetItemCollection(DataSpace.SSpace);
					DbProviderServices providerServices = DbConfiguration.DependencyResolver.GetService(storeItemCollection.ProviderInvariantName);
					bufferedReader = new BufferedDataReader(storeReader);
					await bufferedReader.InitializeAsync(storeItemCollection.ProviderManifestToken, providerServices, shaperFactory.ColumnTypes, shaperFactory.NullableColumns, cancellationToken).WithCurrentCulture();
					shaper = shaperFactory.Create(bufferedReader, context, context.MetadataWorkspace, this.MergeOption, true, this.Streaming);
				}
				TypeUsage resultItemEdmType;
				if (this.ResultType.EdmType.BuiltInTypeKind == BuiltInTypeKind.CollectionType)
				{
					resultItemEdmType = ((CollectionType)this.ResultType.EdmType).TypeUsage;
				}
				else
				{
					resultItemEdmType = this.ResultType;
				}
				result = new ObjectResult<TResultType>(shaper, this._singleEntitySet, resultItemEdmType);
			}
			catch (Exception)
			{
				if (this.Streaming && storeReader != null)
				{
					storeReader.Dispose();
				}
				if (!this.Streaming && bufferedReader != null)
				{
					bufferedReader.Dispose();
				}
				throw;
			}
			return result;
		}

		// Token: 0x060037DD RID: 14301 RVA: 0x00109208 File Offset: 0x00107408
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Disposed by caller")]
		private EntityCommand PrepareEntityCommand(ObjectContext context, ObjectParameterCollection parameterValues)
		{
			EntityCommandDefinition entityCommandDefinition = (EntityCommandDefinition)this.CommandDefinition;
			EntityConnection entityConnection = (EntityConnection)context.Connection;
			EntityCommand entityCommand = new EntityCommand(entityConnection, entityCommandDefinition, context.InterceptionContext, null);
			if (context.CommandTimeout != null)
			{
				entityCommand.CommandTimeout = context.CommandTimeout.Value;
			}
			if (parameterValues != null)
			{
				foreach (ObjectParameter objectParameter in parameterValues)
				{
					int num = entityCommand.Parameters.IndexOf(objectParameter.Name);
					if (num != -1)
					{
						entityCommand.Parameters[num].Value = (objectParameter.Value ?? DBNull.Value);
					}
				}
			}
			if (entityConnection.CurrentTransaction != null)
			{
				entityCommand.Transaction = entityConnection.CurrentTransaction;
			}
			return entityCommand;
		}

		// Token: 0x04001574 RID: 5492
		internal readonly DbCommandDefinition CommandDefinition;

		// Token: 0x04001575 RID: 5493
		internal readonly bool Streaming;

		// Token: 0x04001576 RID: 5494
		internal readonly ShaperFactory ResultShaperFactory;

		// Token: 0x04001577 RID: 5495
		internal readonly TypeUsage ResultType;

		// Token: 0x04001578 RID: 5496
		internal readonly MergeOption MergeOption;

		// Token: 0x04001579 RID: 5497
		internal readonly IEnumerable<Tuple<ObjectParameter, QueryParameterExpression>> CompiledQueryParameters;

		// Token: 0x0400157A RID: 5498
		private readonly EntitySet _singleEntitySet;
	}
}
