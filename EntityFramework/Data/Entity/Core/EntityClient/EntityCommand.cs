using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.EntitySql;
using System.Data.Entity.Core.Common.QueryCache;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.EntityClient.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.EntityClient
{
	// Token: 0x02000338 RID: 824
	public class EntityCommand : DbCommand
	{
		// Token: 0x06001CAD RID: 7341 RVA: 0x0008BD8D File Offset: 0x00089F8D
		public EntityCommand() : this(new DbInterceptionContext())
		{
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x0008BD9A File Offset: 0x00089F9A
		internal EntityCommand(DbInterceptionContext interceptionContext) : this(interceptionContext, new EntityCommand.EntityDataReaderFactory())
		{
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x0008BDA8 File Offset: 0x00089FA8
		internal EntityCommand(DbInterceptionContext interceptionContext, EntityCommand.EntityDataReaderFactory factory)
		{
			this._designTimeVisible = true;
			this._commandType = CommandType.Text;
			this._updatedRowSource = UpdateRowSource.Both;
			this._parameters = new EntityParameterCollection();
			this._interceptionContext = interceptionContext;
			this._enableQueryPlanCaching = true;
			this._entityDataReaderFactory = (factory ?? new EntityCommand.EntityDataReaderFactory());
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x0008BDF9 File Offset: 0x00089FF9
		public EntityCommand(string statement) : this(statement, new DbInterceptionContext(), new EntityCommand.EntityDataReaderFactory())
		{
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x0008BE0C File Offset: 0x0008A00C
		internal EntityCommand(string statement, DbInterceptionContext context, EntityCommand.EntityDataReaderFactory factory) : this(context, factory)
		{
			this._esqlCommandText = statement;
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x0008BE1D File Offset: 0x0008A01D
		public EntityCommand(string statement, EntityConnection connection, IDbDependencyResolver resolver) : this(statement, connection)
		{
			this._dependencyResolver = resolver;
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x0008BE2E File Offset: 0x0008A02E
		public EntityCommand(string statement, EntityConnection connection) : this(statement, connection, new EntityCommand.EntityDataReaderFactory())
		{
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x0008BE3D File Offset: 0x0008A03D
		internal EntityCommand(string statement, EntityConnection connection, EntityCommand.EntityDataReaderFactory factory) : this(statement, new DbInterceptionContext(), factory)
		{
			this._connection = connection;
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x0008BE53 File Offset: 0x0008A053
		public EntityCommand(string statement, EntityConnection connection, EntityTransaction transaction) : this(statement, connection, transaction, new EntityCommand.EntityDataReaderFactory())
		{
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x0008BE63 File Offset: 0x0008A063
		internal EntityCommand(string statement, EntityConnection connection, EntityTransaction transaction, EntityCommand.EntityDataReaderFactory factory) : this(statement, connection, factory)
		{
			this._transaction = transaction;
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x0008BE78 File Offset: 0x0008A078
		internal EntityCommand(EntityCommandDefinition commandDefinition, DbInterceptionContext context, EntityCommand.EntityDataReaderFactory factory = null) : this(context, factory)
		{
			this._commandDefinition = commandDefinition;
			this._parameters = new EntityParameterCollection();
			foreach (EntityParameter entityParameter in commandDefinition.Parameters)
			{
				this._parameters.Add(entityParameter.Clone());
			}
			this._parameters.ResetIsDirty();
			this._isCommandDefinitionBased = true;
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x0008BEFC File Offset: 0x0008A0FC
		internal EntityCommand(EntityConnection connection, EntityCommandDefinition entityCommandDefinition, DbInterceptionContext context, EntityCommand.EntityDataReaderFactory factory = null) : this(entityCommandDefinition, context, factory)
		{
			this._connection = connection;
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06001CB9 RID: 7353 RVA: 0x0008BF0F File Offset: 0x0008A10F
		internal virtual DbInterceptionContext InterceptionContext
		{
			get
			{
				return this._interceptionContext;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06001CBA RID: 7354 RVA: 0x0008BF17 File Offset: 0x0008A117
		// (set) Token: 0x06001CBB RID: 7355 RVA: 0x0008BF1F File Offset: 0x0008A11F
		public new virtual EntityConnection Connection
		{
			get
			{
				return this._connection;
			}
			set
			{
				this.ThrowIfDataReaderIsOpen();
				if (this._connection != value)
				{
					if (this._connection != null)
					{
						this.Unprepare();
					}
					this._connection = value;
					this._transaction = null;
				}
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06001CBC RID: 7356 RVA: 0x0008BF4C File Offset: 0x0008A14C
		// (set) Token: 0x06001CBD RID: 7357 RVA: 0x0008BF54 File Offset: 0x0008A154
		protected override DbConnection DbConnection
		{
			get
			{
				return this.Connection;
			}
			set
			{
				this.Connection = (EntityConnection)value;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06001CBE RID: 7358 RVA: 0x0008BF62 File Offset: 0x0008A162
		// (set) Token: 0x06001CBF RID: 7359 RVA: 0x0008BF86 File Offset: 0x0008A186
		public override string CommandText
		{
			get
			{
				if (this._commandTreeSetByUser != null)
				{
					throw new InvalidOperationException(Strings.EntityClient_CannotGetCommandText);
				}
				return this._esqlCommandText ?? "";
			}
			set
			{
				this.ThrowIfDataReaderIsOpen();
				if (this._commandTreeSetByUser != null)
				{
					throw new InvalidOperationException(Strings.EntityClient_CannotSetCommandText);
				}
				if (this._esqlCommandText != value)
				{
					this._esqlCommandText = value;
					this.Unprepare();
					this._isCommandDefinitionBased = false;
				}
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06001CC0 RID: 7360 RVA: 0x0008BFC3 File Offset: 0x0008A1C3
		// (set) Token: 0x06001CC1 RID: 7361 RVA: 0x0008BFE4 File Offset: 0x0008A1E4
		public virtual DbCommandTree CommandTree
		{
			get
			{
				if (!string.IsNullOrEmpty(this._esqlCommandText))
				{
					throw new InvalidOperationException(Strings.EntityClient_CannotGetCommandTree);
				}
				return this._commandTreeSetByUser;
			}
			set
			{
				this.ThrowIfDataReaderIsOpen();
				if (!string.IsNullOrEmpty(this._esqlCommandText))
				{
					throw new InvalidOperationException(Strings.EntityClient_CannotSetCommandTree);
				}
				if (CommandType.Text != this.CommandType)
				{
					throw new InvalidOperationException(Strings.ADP_InternalProviderError(1026));
				}
				if (this._commandTreeSetByUser != value)
				{
					this._commandTreeSetByUser = value;
					this.Unprepare();
					this._isCommandDefinitionBased = false;
				}
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06001CC2 RID: 7362 RVA: 0x0008C04C File Offset: 0x0008A24C
		// (set) Token: 0x06001CC3 RID: 7363 RVA: 0x0008C0A3 File Offset: 0x0008A2A3
		public override int CommandTimeout
		{
			get
			{
				if (this._commandTimeout != null)
				{
					return this._commandTimeout.Value;
				}
				if (this._connection != null && this._connection.StoreProviderFactory != null)
				{
					DbCommand dbCommand = this._connection.StoreProviderFactory.CreateCommand();
					if (dbCommand != null)
					{
						return dbCommand.CommandTimeout;
					}
				}
				return 0;
			}
			set
			{
				this.ThrowIfDataReaderIsOpen();
				this._commandTimeout = new int?(value);
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06001CC4 RID: 7364 RVA: 0x0008C0B7 File Offset: 0x0008A2B7
		// (set) Token: 0x06001CC5 RID: 7365 RVA: 0x0008C0BF File Offset: 0x0008A2BF
		public override CommandType CommandType
		{
			get
			{
				return this._commandType;
			}
			set
			{
				this.ThrowIfDataReaderIsOpen();
				if (value != CommandType.Text && value != CommandType.StoredProcedure)
				{
					throw new NotSupportedException(Strings.EntityClient_UnsupportedCommandType);
				}
				this._commandType = value;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06001CC6 RID: 7366 RVA: 0x0008C0E1 File Offset: 0x0008A2E1
		public new virtual EntityParameterCollection Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06001CC7 RID: 7367 RVA: 0x0008C0E9 File Offset: 0x0008A2E9
		protected override DbParameterCollection DbParameterCollection
		{
			get
			{
				return this.Parameters;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06001CC8 RID: 7368 RVA: 0x0008C0F1 File Offset: 0x0008A2F1
		// (set) Token: 0x06001CC9 RID: 7369 RVA: 0x0008C0F9 File Offset: 0x0008A2F9
		public new virtual EntityTransaction Transaction
		{
			get
			{
				return this._transaction;
			}
			set
			{
				this.ThrowIfDataReaderIsOpen();
				this._transaction = value;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06001CCA RID: 7370 RVA: 0x0008C108 File Offset: 0x0008A308
		// (set) Token: 0x06001CCB RID: 7371 RVA: 0x0008C110 File Offset: 0x0008A310
		protected override DbTransaction DbTransaction
		{
			get
			{
				return this.Transaction;
			}
			set
			{
				this.Transaction = (EntityTransaction)value;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06001CCC RID: 7372 RVA: 0x0008C11E File Offset: 0x0008A31E
		// (set) Token: 0x06001CCD RID: 7373 RVA: 0x0008C126 File Offset: 0x0008A326
		public override UpdateRowSource UpdatedRowSource
		{
			get
			{
				return this._updatedRowSource;
			}
			set
			{
				this.ThrowIfDataReaderIsOpen();
				this._updatedRowSource = value;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06001CCE RID: 7374 RVA: 0x0008C135 File Offset: 0x0008A335
		// (set) Token: 0x06001CCF RID: 7375 RVA: 0x0008C13D File Offset: 0x0008A33D
		public override bool DesignTimeVisible
		{
			get
			{
				return this._designTimeVisible;
			}
			set
			{
				this.ThrowIfDataReaderIsOpen();
				this._designTimeVisible = value;
				TypeDescriptor.Refresh(this);
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06001CD0 RID: 7376 RVA: 0x0008C152 File Offset: 0x0008A352
		// (set) Token: 0x06001CD1 RID: 7377 RVA: 0x0008C15A File Offset: 0x0008A35A
		public virtual bool EnablePlanCaching
		{
			get
			{
				return this._enableQueryPlanCaching;
			}
			set
			{
				this.ThrowIfDataReaderIsOpen();
				this._enableQueryPlanCaching = value;
			}
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x0008C169 File Offset: 0x0008A369
		public override void Cancel()
		{
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x0008C16B File Offset: 0x0008A36B
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public new virtual EntityParameter CreateParameter()
		{
			return new EntityParameter();
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x0008C172 File Offset: 0x0008A372
		protected override DbParameter CreateDbParameter()
		{
			return this.CreateParameter();
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x0008C17A File Offset: 0x0008A37A
		public new virtual EntityDataReader ExecuteReader()
		{
			return this.ExecuteReader(CommandBehavior.Default);
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x0008C184 File Offset: 0x0008A384
		public new virtual EntityDataReader ExecuteReader(CommandBehavior behavior)
		{
			this.Prepare();
			EntityDataReader entityDataReader = this._entityDataReaderFactory.CreateEntityDataReader(this, this._commandDefinition.Execute(this, behavior), behavior);
			this._dataReader = entityDataReader;
			return entityDataReader;
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x0008C1BA File Offset: 0x0008A3BA
		public new virtual Task<EntityDataReader> ExecuteReaderAsync()
		{
			return this.ExecuteReaderAsync(CommandBehavior.Default, CancellationToken.None);
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x0008C1C8 File Offset: 0x0008A3C8
		public new virtual Task<EntityDataReader> ExecuteReaderAsync(CancellationToken cancellationToken)
		{
			return this.ExecuteReaderAsync(CommandBehavior.Default, cancellationToken);
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x0008C1D2 File Offset: 0x0008A3D2
		public new virtual Task<EntityDataReader> ExecuteReaderAsync(CommandBehavior behavior)
		{
			return this.ExecuteReaderAsync(behavior, CancellationToken.None);
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x0008C334 File Offset: 0x0008A534
		public new virtual async Task<EntityDataReader> ExecuteReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.Prepare();
			DbDataReader dbDataReader = await this._commandDefinition.ExecuteAsync(this, behavior, cancellationToken).WithCurrentCulture<DbDataReader>();
			EntityDataReader reader = this._entityDataReaderFactory.CreateEntityDataReader(this, dbDataReader, behavior);
			this._dataReader = reader;
			return reader;
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x0008C38A File Offset: 0x0008A58A
		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			return this.ExecuteReader(behavior);
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x0008C47C File Offset: 0x0008A67C
		protected override async Task<DbDataReader> ExecuteDbDataReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
		{
			return await this.ExecuteReaderAsync(behavior, cancellationToken).WithCurrentCulture<EntityDataReader>();
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x0008C4D4 File Offset: 0x0008A6D4
		public override int ExecuteNonQuery()
		{
			int recordsAffected;
			using (EntityDataReader entityDataReader = this.ExecuteReader(CommandBehavior.SequentialAccess))
			{
				CommandHelper.ConsumeReader(entityDataReader);
				recordsAffected = entityDataReader.RecordsAffected;
			}
			return recordsAffected;
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x0008C6DC File Offset: 0x0008A8DC
		public override async Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken)
		{
			int recordsAffected;
			using (EntityDataReader reader = await this.ExecuteReaderAsync(CommandBehavior.SequentialAccess, cancellationToken).WithCurrentCulture<EntityDataReader>())
			{
				await CommandHelper.ConsumeReaderAsync(reader, cancellationToken).WithCurrentCulture();
				recordsAffected = reader.RecordsAffected;
			}
			return recordsAffected;
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x0008C72C File Offset: 0x0008A92C
		public override object ExecuteScalar()
		{
			object result;
			using (EntityDataReader entityDataReader = this.ExecuteReader(CommandBehavior.SequentialAccess))
			{
				object obj = entityDataReader.Read() ? entityDataReader.GetValue(0) : null;
				CommandHelper.ConsumeReader(entityDataReader);
				result = obj;
			}
			return result;
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x0008C77C File Offset: 0x0008A97C
		internal virtual void Unprepare()
		{
			this._commandDefinition = null;
			this._preparedCommandTree = null;
			this._parameters.ResetIsDirty();
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x0008C797 File Offset: 0x0008A997
		public override void Prepare()
		{
			this.ThrowIfDataReaderIsOpen();
			this.CheckIfReadyToPrepare();
			this.InnerPrepare();
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x0008C7AB File Offset: 0x0008A9AB
		private void InnerPrepare()
		{
			if (this._parameters.IsDirty)
			{
				this.Unprepare();
			}
			this._commandDefinition = this.GetCommandDefinition();
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x0008C7E4 File Offset: 0x0008A9E4
		private DbCommandTree MakeCommandTree()
		{
			DbCommandTree result = null;
			if (this._commandTreeSetByUser != null)
			{
				result = this._commandTreeSetByUser;
			}
			else if (CommandType.Text == this.CommandType)
			{
				if (!string.IsNullOrEmpty(this._esqlCommandText))
				{
					Perspective perspective = new ModelPerspective(this._connection.GetMetadataWorkspace());
					Dictionary<string, TypeUsage> parameterTypeUsage = this.GetParameterTypeUsage();
					result = CqlQuery.Compile(this._esqlCommandText, perspective, null, from paramInfo in parameterTypeUsage
					select paramInfo.Value.Parameter(paramInfo.Key)).CommandTree;
				}
				else
				{
					if (this._isCommandDefinitionBased)
					{
						throw new InvalidOperationException(Strings.EntityClient_CannotReprepareCommandDefinitionBasedCommand);
					}
					throw new InvalidOperationException(Strings.EntityClient_NoCommandText);
				}
			}
			else if (CommandType.StoredProcedure == this.CommandType)
			{
				IEnumerable<KeyValuePair<string, TypeUsage>> parameterTypeUsage2 = this.GetParameterTypeUsage();
				EdmFunction edmFunction = this.DetermineFunctionImport();
				result = new DbFunctionCommandTree(this.Connection.GetMetadataWorkspace(), DataSpace.CSpace, edmFunction, null, parameterTypeUsage2);
			}
			return result;
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x0008C8BC File Offset: 0x0008AABC
		private EdmFunction DetermineFunctionImport()
		{
			if (string.IsNullOrEmpty(this.CommandText) || string.IsNullOrEmpty(this.CommandText.Trim()))
			{
				throw new InvalidOperationException(Strings.EntityClient_FunctionImportEmptyCommandText);
			}
			string defaultContainerName = null;
			string containerName;
			string functionImportName;
			CommandHelper.ParseFunctionImportCommandText(this.CommandText, defaultContainerName, out containerName, out functionImportName);
			return CommandHelper.FindFunctionImport(this._connection.GetMetadataWorkspace(), containerName, functionImportName);
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x0008C918 File Offset: 0x0008AB18
		internal virtual EntityCommandDefinition GetCommandDefinition()
		{
			EntityCommandDefinition entityCommandDefinition = this._commandDefinition;
			if (entityCommandDefinition == null)
			{
				if (!this.TryGetEntityCommandDefinitionFromQueryCache(out entityCommandDefinition))
				{
					entityCommandDefinition = this.CreateCommandDefinition();
				}
				this._commandDefinition = entityCommandDefinition;
			}
			return entityCommandDefinition;
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x0008C948 File Offset: 0x0008AB48
		internal virtual EntityTransaction ValidateAndGetEntityTransaction()
		{
			if (this.Transaction != null && this.Transaction != this.Connection.CurrentTransaction)
			{
				throw new InvalidOperationException(Strings.EntityClient_InvalidTransactionForCommand);
			}
			return this.Connection.CurrentTransaction;
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x0008C97C File Offset: 0x0008AB7C
		[Browsable(false)]
		public virtual string ToTraceString()
		{
			this.CheckConnectionPresent();
			this.InnerPrepare();
			EntityCommandDefinition commandDefinition = this._commandDefinition;
			if (commandDefinition != null)
			{
				return commandDefinition.ToTraceString();
			}
			return string.Empty;
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x0008C9AC File Offset: 0x0008ABAC
		private bool TryGetEntityCommandDefinitionFromQueryCache(out EntityCommandDefinition entityCommandDefinition)
		{
			entityCommandDefinition = null;
			if (!this._enableQueryPlanCaching || string.IsNullOrEmpty(this._esqlCommandText))
			{
				return false;
			}
			EntityClientCacheKey entityClientCacheKey = new EntityClientCacheKey(this);
			QueryCacheManager queryCacheManager = this._connection.GetMetadataWorkspace().GetQueryCacheManager();
			if (!queryCacheManager.TryCacheLookup<EntityClientCacheKey, EntityCommandDefinition>(entityClientCacheKey, out entityCommandDefinition))
			{
				entityCommandDefinition = this.CreateCommandDefinition();
				QueryCacheEntry queryCacheEntry = null;
				if (queryCacheManager.TryLookupAndAdd(new QueryCacheEntry(entityClientCacheKey, entityCommandDefinition), out queryCacheEntry))
				{
					entityCommandDefinition = (EntityCommandDefinition)queryCacheEntry.GetTarget();
				}
			}
			return true;
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x0008CA20 File Offset: 0x0008AC20
		private EntityCommandDefinition CreateCommandDefinition()
		{
			if (this._preparedCommandTree == null)
			{
				this._preparedCommandTree = this.MakeCommandTree();
			}
			if (!this._preparedCommandTree.MetadataWorkspace.IsMetadataWorkspaceCSCompatible(this.Connection.GetMetadataWorkspace()))
			{
				throw new InvalidOperationException(Strings.EntityClient_CommandTreeMetadataIncompatible);
			}
			return EntityProviderServices.CreateCommandDefinition(this._connection.StoreProviderFactory, this._preparedCommandTree, this._interceptionContext, this._dependencyResolver);
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x0008CA8B File Offset: 0x0008AC8B
		private void CheckConnectionPresent()
		{
			if (this._connection == null)
			{
				throw new InvalidOperationException(Strings.EntityClient_NoConnectionForCommand);
			}
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x0008CAA0 File Offset: 0x0008ACA0
		private void CheckIfReadyToPrepare()
		{
			this.CheckConnectionPresent();
			if (this._connection.StoreProviderFactory == null || this._connection.StoreConnection == null)
			{
				throw Error.EntityClient_ConnectionStringNeededBeforeOperation();
			}
			if (this._connection.State == ConnectionState.Closed || this._connection.State == ConnectionState.Broken)
			{
				string message = Strings.EntityClient_ExecutingOnClosedConnection((this._connection.State == ConnectionState.Closed) ? Strings.EntityClient_ConnectionStateClosed : Strings.EntityClient_ConnectionStateBroken);
				throw new InvalidOperationException(message);
			}
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x0008CB15 File Offset: 0x0008AD15
		private void ThrowIfDataReaderIsOpen()
		{
			if (this._dataReader != null)
			{
				throw new InvalidOperationException(Strings.EntityClient_DataReaderIsStillOpen);
			}
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x0008CB2C File Offset: 0x0008AD2C
		internal virtual Dictionary<string, TypeUsage> GetParameterTypeUsage()
		{
			Dictionary<string, TypeUsage> dictionary = new Dictionary<string, TypeUsage>(this._parameters.Count);
			foreach (object obj in this._parameters)
			{
				EntityParameter entityParameter = (EntityParameter)obj;
				string parameterName = entityParameter.ParameterName;
				if (string.IsNullOrEmpty(parameterName))
				{
					throw new InvalidOperationException(Strings.EntityClient_EmptyParameterName);
				}
				if (this.CommandType == CommandType.Text && entityParameter.Direction != ParameterDirection.Input)
				{
					throw new InvalidOperationException(Strings.EntityClient_InvalidParameterDirection(entityParameter.ParameterName));
				}
				if (entityParameter.EdmType == null && entityParameter.DbType == DbType.Object && (entityParameter.Value == null || entityParameter.Value is DBNull))
				{
					throw new InvalidOperationException(Strings.EntityClient_UnknownParameterType(parameterName));
				}
				TypeUsage typeUsage = entityParameter.GetTypeUsage();
				try
				{
					dictionary.Add(parameterName, typeUsage);
				}
				catch (ArgumentException innerException)
				{
					throw new InvalidOperationException(Strings.EntityClient_DuplicateParameterNames(entityParameter.ParameterName), innerException);
				}
			}
			return dictionary;
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x0008CC44 File Offset: 0x0008AE44
		internal virtual void NotifyDataReaderClosing()
		{
			this._dataReader = null;
			if (this._storeProviderCommand != null)
			{
				CommandHelper.SetEntityParameterValues(this, this._storeProviderCommand, this._connection);
				this._storeProviderCommand = null;
			}
			if (this.IsNotNullOnDataReaderClosingEvent())
			{
				this.InvokeOnDataReaderClosingEvent(this, new EventArgs());
			}
		}

		// Token: 0x06001CEF RID: 7407 RVA: 0x0008CC82 File Offset: 0x0008AE82
		internal virtual void SetStoreProviderCommand(DbCommand storeProviderCommand)
		{
			this._storeProviderCommand = storeProviderCommand;
		}

		// Token: 0x06001CF0 RID: 7408 RVA: 0x0008CC8B File Offset: 0x0008AE8B
		internal virtual bool IsNotNullOnDataReaderClosingEvent()
		{
			return null != this.OnDataReaderClosing;
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x0008CC99 File Offset: 0x0008AE99
		internal virtual void InvokeOnDataReaderClosingEvent(EntityCommand sender, EventArgs e)
		{
			this.OnDataReaderClosing(sender, e);
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06001CF2 RID: 7410 RVA: 0x0008CCA8 File Offset: 0x0008AEA8
		// (remove) Token: 0x06001CF3 RID: 7411 RVA: 0x0008CCE0 File Offset: 0x0008AEE0
		internal event EventHandler OnDataReaderClosing;

		// Token: 0x040009D6 RID: 2518
		private bool _designTimeVisible;

		// Token: 0x040009D7 RID: 2519
		private string _esqlCommandText;

		// Token: 0x040009D8 RID: 2520
		private EntityConnection _connection;

		// Token: 0x040009D9 RID: 2521
		private DbCommandTree _preparedCommandTree;

		// Token: 0x040009DA RID: 2522
		private readonly EntityParameterCollection _parameters;

		// Token: 0x040009DB RID: 2523
		private int? _commandTimeout;

		// Token: 0x040009DC RID: 2524
		private CommandType _commandType;

		// Token: 0x040009DD RID: 2525
		private EntityTransaction _transaction;

		// Token: 0x040009DE RID: 2526
		private UpdateRowSource _updatedRowSource;

		// Token: 0x040009DF RID: 2527
		private EntityCommandDefinition _commandDefinition;

		// Token: 0x040009E0 RID: 2528
		private bool _isCommandDefinitionBased;

		// Token: 0x040009E1 RID: 2529
		private DbCommandTree _commandTreeSetByUser;

		// Token: 0x040009E2 RID: 2530
		private DbDataReader _dataReader;

		// Token: 0x040009E3 RID: 2531
		private bool _enableQueryPlanCaching;

		// Token: 0x040009E4 RID: 2532
		private DbCommand _storeProviderCommand;

		// Token: 0x040009E5 RID: 2533
		private readonly EntityCommand.EntityDataReaderFactory _entityDataReaderFactory;

		// Token: 0x040009E6 RID: 2534
		private readonly IDbDependencyResolver _dependencyResolver;

		// Token: 0x040009E7 RID: 2535
		private readonly DbInterceptionContext _interceptionContext;

		// Token: 0x02000339 RID: 825
		internal class EntityDataReaderFactory
		{
			// Token: 0x06001CF5 RID: 7413 RVA: 0x0008CD15 File Offset: 0x0008AF15
			internal virtual EntityDataReader CreateEntityDataReader(EntityCommand entityCommand, DbDataReader storeDataReader, CommandBehavior behavior)
			{
				return new EntityDataReader(entityCommand, storeDataReader, behavior);
			}
		}
	}
}
