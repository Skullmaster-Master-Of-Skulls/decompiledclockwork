using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.EntitySql;
using System.Data.Common.QueryCache;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Linq;

namespace System.Data.EntityClient
{
	// Token: 0x0200011D RID: 285
	public sealed class EntityCommand : DbCommand
	{
		// Token: 0x06000EE7 RID: 3815 RVA: 0x0003F1B0 File Offset: 0x0003D3B0
		public EntityCommand()
		{
			GC.SuppressFinalize(this);
			this._designTimeVisible = true;
			this._commandType = CommandType.Text;
			this._updatedRowSource = UpdateRowSource.Both;
			this._parameters = new EntityParameterCollection();
			this._enableQueryPlanCaching = true;
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0003F1E5 File Offset: 0x0003D3E5
		public EntityCommand(string statement) : this()
		{
			this._esqlCommandText = statement;
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0003F1F4 File Offset: 0x0003D3F4
		public EntityCommand(string statement, EntityConnection connection) : this(statement)
		{
			this._connection = connection;
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0003F204 File Offset: 0x0003D404
		public EntityCommand(string statement, EntityConnection connection, EntityTransaction transaction) : this(statement, connection)
		{
			this._transaction = transaction;
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x0003F218 File Offset: 0x0003D418
		internal EntityCommand(EntityCommandDefinition commandDefinition) : this()
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

		// Token: 0x06000EEC RID: 3820 RVA: 0x0003F29C File Offset: 0x0003D49C
		internal EntityCommand(EntityConnection connection, EntityCommandDefinition entityCommandDefinition) : this(entityCommandDefinition)
		{
			this._connection = connection;
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000EED RID: 3821 RVA: 0x0003F2AC File Offset: 0x0003D4AC
		// (set) Token: 0x06000EEE RID: 3822 RVA: 0x0003F2B4 File Offset: 0x0003D4B4
		public new EntityConnection Connection
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

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000EEF RID: 3823 RVA: 0x0003F2E1 File Offset: 0x0003D4E1
		// (set) Token: 0x06000EF0 RID: 3824 RVA: 0x0003F2E9 File Offset: 0x0003D4E9
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

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x0003F2F7 File Offset: 0x0003D4F7
		// (set) Token: 0x06000EF2 RID: 3826 RVA: 0x0003F31B File Offset: 0x0003D51B
		public override string CommandText
		{
			get
			{
				if (this._commandTreeSetByUser != null)
				{
					throw EntityUtil.InvalidOperation(Strings.EntityClient_CannotGetCommandText);
				}
				return this._esqlCommandText ?? "";
			}
			set
			{
				this.ThrowIfDataReaderIsOpen();
				if (this._commandTreeSetByUser != null)
				{
					throw EntityUtil.InvalidOperation(Strings.EntityClient_CannotSetCommandText);
				}
				if (this._esqlCommandText != value)
				{
					this._esqlCommandText = value;
					this.Unprepare();
					this._isCommandDefinitionBased = false;
				}
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x0003F358 File Offset: 0x0003D558
		// (set) Token: 0x06000EF4 RID: 3828 RVA: 0x0003F378 File Offset: 0x0003D578
		public DbCommandTree CommandTree
		{
			get
			{
				if (!string.IsNullOrEmpty(this._esqlCommandText))
				{
					throw EntityUtil.InvalidOperation(Strings.EntityClient_CannotGetCommandTree);
				}
				return this._commandTreeSetByUser;
			}
			set
			{
				this.ThrowIfDataReaderIsOpen();
				if (!string.IsNullOrEmpty(this._esqlCommandText))
				{
					throw EntityUtil.InvalidOperation(Strings.EntityClient_CannotSetCommandTree);
				}
				if (CommandType.Text != this.CommandType)
				{
					throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.CommandTreeOnStoredProcedureEntityCommand);
				}
				if (this._commandTreeSetByUser != value)
				{
					this._commandTreeSetByUser = value;
					this.Unprepare();
					this._isCommandDefinitionBased = false;
				}
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x0003F3D4 File Offset: 0x0003D5D4
		// (set) Token: 0x06000EF6 RID: 3830 RVA: 0x0003F42B File Offset: 0x0003D62B
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

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x0003F43F File Offset: 0x0003D63F
		// (set) Token: 0x06000EF8 RID: 3832 RVA: 0x0003F447 File Offset: 0x0003D647
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
					throw EntityUtil.NotSupported(Strings.EntityClient_UnsupportedCommandType);
				}
				this._commandType = value;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x0003F469 File Offset: 0x0003D669
		public new EntityParameterCollection Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000EFA RID: 3834 RVA: 0x0003F471 File Offset: 0x0003D671
		protected override DbParameterCollection DbParameterCollection
		{
			get
			{
				return this.Parameters;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000EFB RID: 3835 RVA: 0x0003F479 File Offset: 0x0003D679
		// (set) Token: 0x06000EFC RID: 3836 RVA: 0x0003F481 File Offset: 0x0003D681
		public new EntityTransaction Transaction
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

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x0003F490 File Offset: 0x0003D690
		// (set) Token: 0x06000EFE RID: 3838 RVA: 0x0003F498 File Offset: 0x0003D698
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

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x0003F4A6 File Offset: 0x0003D6A6
		// (set) Token: 0x06000F00 RID: 3840 RVA: 0x0003F4AE File Offset: 0x0003D6AE
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

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x0003F4BD File Offset: 0x0003D6BD
		// (set) Token: 0x06000F02 RID: 3842 RVA: 0x0003F4C5 File Offset: 0x0003D6C5
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

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000F03 RID: 3843 RVA: 0x0003F4DA File Offset: 0x0003D6DA
		// (set) Token: 0x06000F04 RID: 3844 RVA: 0x0003F4E2 File Offset: 0x0003D6E2
		public bool EnablePlanCaching
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

		// Token: 0x06000F05 RID: 3845 RVA: 0x000089D0 File Offset: 0x00006BD0
		public override void Cancel()
		{
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x0003F4F1 File Offset: 0x0003D6F1
		public new EntityParameter CreateParameter()
		{
			return new EntityParameter();
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x0003F4F8 File Offset: 0x0003D6F8
		protected override DbParameter CreateDbParameter()
		{
			return this.CreateParameter();
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x0003F500 File Offset: 0x0003D700
		public new EntityDataReader ExecuteReader()
		{
			return this.ExecuteReader(CommandBehavior.Default);
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x0003F50C File Offset: 0x0003D70C
		public new EntityDataReader ExecuteReader(CommandBehavior behavior)
		{
			this.Prepare();
			EntityDataReader entityDataReader = new EntityDataReader(this, this._commandDefinition.Execute(this, behavior), behavior);
			this._dataReader = entityDataReader;
			return entityDataReader;
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0003F53C File Offset: 0x0003D73C
		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			return this.ExecuteReader(behavior);
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x0003F545 File Offset: 0x0003D745
		public override int ExecuteNonQuery()
		{
			return this.ExecuteScalar<int>(delegate(DbDataReader reader)
			{
				CommandHelper.ConsumeReader(reader);
				return reader.RecordsAffected;
			});
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0003F56C File Offset: 0x0003D76C
		public override object ExecuteScalar()
		{
			return this.ExecuteScalar<object>(delegate(DbDataReader reader)
			{
				object result = reader.Read() ? reader.GetValue(0) : null;
				CommandHelper.ConsumeReader(reader);
				return result;
			});
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0003F594 File Offset: 0x0003D794
		private T_Result ExecuteScalar<T_Result>(Func<DbDataReader, T_Result> resultSelector)
		{
			T_Result result;
			using (EntityDataReader entityDataReader = this.ExecuteReader(CommandBehavior.SequentialAccess))
			{
				result = resultSelector(entityDataReader);
			}
			return result;
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x0003F5D0 File Offset: 0x0003D7D0
		internal void Unprepare()
		{
			this._commandDefinition = null;
			this._preparedCommandTree = null;
			this._parameters.ResetIsDirty();
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x0003F5EB File Offset: 0x0003D7EB
		public override void Prepare()
		{
			this.ThrowIfDataReaderIsOpen();
			this.CheckIfReadyToPrepare();
			this.InnerPrepare();
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0003F5FF File Offset: 0x0003D7FF
		private void InnerPrepare()
		{
			if (this._parameters.IsDirty)
			{
				this.Unprepare();
			}
			this._commandDefinition = this.GetCommandDefinition();
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x0003F620 File Offset: 0x0003D820
		private void MakeCommandTree()
		{
			if (this._preparedCommandTree == null)
			{
				DbCommandTree preparedCommandTree = null;
				if (this._commandTreeSetByUser != null)
				{
					preparedCommandTree = this._commandTreeSetByUser;
				}
				else if (CommandType.Text == this.CommandType)
				{
					if (!string.IsNullOrEmpty(this._esqlCommandText))
					{
						Perspective perspective = new ModelPerspective(this._connection.GetMetadataWorkspace());
						Dictionary<string, TypeUsage> parameterTypeUsage = this.GetParameterTypeUsage();
						preparedCommandTree = CqlQuery.Compile(this._esqlCommandText, perspective, null, from paramInfo in parameterTypeUsage
						select paramInfo.Value.Parameter(paramInfo.Key)).CommandTree;
					}
					else
					{
						if (this._isCommandDefinitionBased)
						{
							throw EntityUtil.InvalidOperation(Strings.EntityClient_CannotReprepareCommandDefinitionBasedCommand);
						}
						throw EntityUtil.InvalidOperation(Strings.EntityClient_NoCommandText);
					}
				}
				else if (CommandType.StoredProcedure == this.CommandType)
				{
					IEnumerable<KeyValuePair<string, TypeUsage>> parameterTypeUsage2 = this.GetParameterTypeUsage();
					EdmFunction edmFunction = this.DetermineFunctionImport();
					preparedCommandTree = new DbFunctionCommandTree(this.Connection.GetMetadataWorkspace(), DataSpace.CSpace, edmFunction, null, parameterTypeUsage2);
				}
				this._preparedCommandTree = preparedCommandTree;
			}
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x0003F70C File Offset: 0x0003D90C
		private EdmFunction DetermineFunctionImport()
		{
			if (string.IsNullOrEmpty(this.CommandText) || string.IsNullOrEmpty(this.CommandText.Trim()))
			{
				throw EntityUtil.InvalidOperation(Strings.EntityClient_FunctionImportEmptyCommandText);
			}
			MetadataWorkspace metadataWorkspace = this._connection.GetMetadataWorkspace();
			string defaultContainerName = null;
			string containerName;
			string functionImportName;
			CommandHelper.ParseFunctionImportCommandText(this.CommandText, defaultContainerName, out containerName, out functionImportName);
			return CommandHelper.FindFunctionImport(this._connection.GetMetadataWorkspace(), containerName, functionImportName);
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0003F774 File Offset: 0x0003D974
		internal EntityCommandDefinition GetCommandDefinition()
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

		// Token: 0x06000F14 RID: 3860 RVA: 0x0003F7A4 File Offset: 0x0003D9A4
		[Browsable(false)]
		public string ToTraceString()
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

		// Token: 0x06000F15 RID: 3861 RVA: 0x0003F7D4 File Offset: 0x0003D9D4
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

		// Token: 0x06000F16 RID: 3862 RVA: 0x0003F848 File Offset: 0x0003DA48
		private EntityCommandDefinition CreateCommandDefinition()
		{
			this.MakeCommandTree();
			if (!this._preparedCommandTree.MetadataWorkspace.IsMetadataWorkspaceCSCompatible(this.Connection.GetMetadataWorkspace()))
			{
				throw EntityUtil.InvalidOperation(Strings.EntityClient_CommandTreeMetadataIncompatible);
			}
			return EntityProviderServices.Instance.CreateCommandDefinition(this._connection.StoreProviderFactory, this._preparedCommandTree);
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x0003F8A0 File Offset: 0x0003DAA0
		private void CheckConnectionPresent()
		{
			if (this._connection == null)
			{
				throw EntityUtil.InvalidOperation(Strings.EntityClient_NoConnectionForCommand);
			}
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x0003F8B8 File Offset: 0x0003DAB8
		private void CheckIfReadyToPrepare()
		{
			this.CheckConnectionPresent();
			if (this._connection.StoreProviderFactory == null || this._connection.StoreConnection == null)
			{
				throw EntityUtil.InvalidOperation(Strings.EntityClient_ConnectionStringNeededBeforeOperation);
			}
			if (this._connection.State == ConnectionState.Closed || this._connection.State == ConnectionState.Broken)
			{
				string error = Strings.EntityClient_ExecutingOnClosedConnection((this._connection.State == ConnectionState.Closed) ? Strings.EntityClient_ConnectionStateClosed : Strings.EntityClient_ConnectionStateBroken);
				throw EntityUtil.InvalidOperation(error);
			}
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x0003F932 File Offset: 0x0003DB32
		private void ThrowIfDataReaderIsOpen()
		{
			if (this._dataReader != null)
			{
				throw EntityUtil.InvalidOperation(Strings.EntityClient_DataReaderIsStillOpen);
			}
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x0003F948 File Offset: 0x0003DB48
		internal Dictionary<string, TypeUsage> GetParameterTypeUsage()
		{
			Dictionary<string, TypeUsage> dictionary = new Dictionary<string, TypeUsage>(this._parameters.Count);
			foreach (object obj in this._parameters)
			{
				EntityParameter entityParameter = (EntityParameter)obj;
				string parameterName = entityParameter.ParameterName;
				if (string.IsNullOrEmpty(parameterName))
				{
					throw EntityUtil.InvalidOperation(Strings.EntityClient_EmptyParameterName);
				}
				if (this.CommandType == CommandType.Text && entityParameter.Direction != ParameterDirection.Input)
				{
					throw EntityUtil.InvalidOperation(Strings.EntityClient_InvalidParameterDirection(entityParameter.ParameterName));
				}
				if (entityParameter.EdmType == null && entityParameter.DbType == DbType.Object && (entityParameter.Value == null || entityParameter.Value is DBNull))
				{
					throw EntityUtil.InvalidOperation(Strings.EntityClient_UnknownParameterType(parameterName));
				}
				TypeUsage typeUsage = entityParameter.GetTypeUsage();
				try
				{
					dictionary.Add(parameterName, typeUsage);
				}
				catch (ArgumentException inner)
				{
					throw EntityUtil.InvalidOperation(Strings.EntityClient_DuplicateParameterNames(entityParameter.ParameterName), inner);
				}
			}
			return dictionary;
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x0003FA5C File Offset: 0x0003DC5C
		internal void NotifyDataReaderClosing()
		{
			this._dataReader = null;
			if (this._storeProviderCommand != null)
			{
				CommandHelper.SetEntityParameterValues(this, this._storeProviderCommand, this._connection);
				this._storeProviderCommand = null;
			}
			if (this.OnDataReaderClosing != null)
			{
				this.OnDataReaderClosing(this, new EventArgs());
			}
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x0003FAAA File Offset: 0x0003DCAA
		internal void SetStoreProviderCommand(DbCommand storeProviderCommand)
		{
			this._storeProviderCommand = storeProviderCommand;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000F1D RID: 3869 RVA: 0x0003FAB4 File Offset: 0x0003DCB4
		// (remove) Token: 0x06000F1E RID: 3870 RVA: 0x0003FAEC File Offset: 0x0003DCEC
		internal event EventHandler OnDataReaderClosing;

		// Token: 0x040009F4 RID: 2548
		private const int InvalidCloseCount = -1;

		// Token: 0x040009F5 RID: 2549
		private bool _designTimeVisible;

		// Token: 0x040009F6 RID: 2550
		private string _esqlCommandText;

		// Token: 0x040009F7 RID: 2551
		private EntityConnection _connection;

		// Token: 0x040009F8 RID: 2552
		private DbCommandTree _preparedCommandTree;

		// Token: 0x040009F9 RID: 2553
		private EntityParameterCollection _parameters;

		// Token: 0x040009FA RID: 2554
		private int? _commandTimeout;

		// Token: 0x040009FB RID: 2555
		private CommandType _commandType;

		// Token: 0x040009FC RID: 2556
		private EntityTransaction _transaction;

		// Token: 0x040009FD RID: 2557
		private UpdateRowSource _updatedRowSource;

		// Token: 0x040009FE RID: 2558
		private EntityCommandDefinition _commandDefinition;

		// Token: 0x040009FF RID: 2559
		private bool _isCommandDefinitionBased;

		// Token: 0x04000A00 RID: 2560
		private DbCommandTree _commandTreeSetByUser;

		// Token: 0x04000A01 RID: 2561
		private DbDataReader _dataReader;

		// Token: 0x04000A02 RID: 2562
		private bool _enableQueryPlanCaching;

		// Token: 0x04000A03 RID: 2563
		private DbCommand _storeProviderCommand;
	}
}
