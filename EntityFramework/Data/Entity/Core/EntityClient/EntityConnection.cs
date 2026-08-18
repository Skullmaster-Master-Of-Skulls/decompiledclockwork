using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.EntityClient.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace System.Data.Entity.Core.EntityClient
{
	// Token: 0x0200033A RID: 826
	public class EntityConnection : DbConnection
	{
		// Token: 0x06001CF7 RID: 7415 RVA: 0x0008CD27 File Offset: 0x0008AF27
		[SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope", Justification = "Object is in fact passed to property of the class and gets Disposed properly in the Dispose() method.")]
		public EntityConnection() : this(string.Empty)
		{
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x0008CD34 File Offset: 0x0008AF34
		[SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope", Justification = "Object is in fact passed to property of the class and gets Disposed properly in the Dispose() method.")]
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public EntityConnection(string connectionString)
		{
			this._connectionStringLock = new object();
			this._entityConnectionOwnsStoreConnection = true;
			this._associatedContexts = new List<ObjectContext>();
			base..ctor();
			this.ChangeConnectionString(connectionString);
		}

		// Token: 0x06001CF9 RID: 7417 RVA: 0x0008CD60 File Offset: 0x0008AF60
		[SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope", Justification = "Object is in fact passed to property of the class and gets Disposed properly in the Dispose() method.")]
		public EntityConnection(MetadataWorkspace workspace, DbConnection connection) : this(Check.NotNull<MetadataWorkspace>(workspace, "workspace"), Check.NotNull<DbConnection>(connection, "connection"), false, false)
		{
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x0008CD80 File Offset: 0x0008AF80
		[SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope", Justification = "Object is in fact passed to property of the class and gets Disposed properly in the Dispose() method.")]
		public EntityConnection(MetadataWorkspace workspace, DbConnection connection, bool entityConnectionOwnsStoreConnection) : this(Check.NotNull<MetadataWorkspace>(workspace, "workspace"), Check.NotNull<DbConnection>(connection, "connection"), false, entityConnectionOwnsStoreConnection)
		{
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x0008CDA0 File Offset: 0x0008AFA0
		internal EntityConnection(MetadataWorkspace workspace, DbConnection connection, bool skipInitialization, bool entityConnectionOwnsStoreConnection)
		{
			this._connectionStringLock = new object();
			this._entityConnectionOwnsStoreConnection = true;
			this._associatedContexts = new List<ObjectContext>();
			base..ctor();
			if (!skipInitialization)
			{
				if (!workspace.IsItemCollectionAlreadyRegistered(DataSpace.CSpace))
				{
					throw new ArgumentException(Strings.EntityClient_ItemCollectionsNotRegisteredInWorkspace("EdmItemCollection"));
				}
				if (!workspace.IsItemCollectionAlreadyRegistered(DataSpace.SSpace))
				{
					throw new ArgumentException(Strings.EntityClient_ItemCollectionsNotRegisteredInWorkspace("StoreItemCollection"));
				}
				if (!workspace.IsItemCollectionAlreadyRegistered(DataSpace.CSSpace))
				{
					throw new ArgumentException(Strings.EntityClient_ItemCollectionsNotRegisteredInWorkspace("StorageMappingItemCollection"));
				}
				if (connection.GetProviderFactory() == null)
				{
					throw new ProviderIncompatibleException(Strings.EntityClient_DbConnectionHasNoProvider(connection));
				}
				StoreItemCollection storeItemCollection = (StoreItemCollection)workspace.GetItemCollection(DataSpace.SSpace);
				this._providerFactory = storeItemCollection.ProviderFactory;
				this._initialized = true;
			}
			this._metadataWorkspace = workspace;
			this._storeConnection = connection;
			this._entityConnectionOwnsStoreConnection = entityConnectionOwnsStoreConnection;
			if (this._storeConnection != null)
			{
				this._entityClientConnectionState = DbInterception.Dispatch.Connection.GetState(this._storeConnection, this.InterceptionContext);
			}
			this.SubscribeToStoreConnectionStateChangeEvents();
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x0008CE9C File Offset: 0x0008B09C
		private void SubscribeToStoreConnectionStateChangeEvents()
		{
			if (this._storeConnection != null)
			{
				this._storeConnection.StateChange += this.StoreConnectionStateChangeHandler;
			}
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x0008CEBE File Offset: 0x0008B0BE
		private void UnsubscribeFromStoreConnectionStateChangeEvents()
		{
			if (this._storeConnection != null)
			{
				this._storeConnection.StateChange -= this.StoreConnectionStateChangeHandler;
			}
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x0008CEE0 File Offset: 0x0008B0E0
		internal virtual void StoreConnectionStateChangeHandler(object sender, StateChangeEventArgs stateChange)
		{
			ConnectionState currentState = stateChange.CurrentState;
			if (this._entityClientConnectionState != currentState)
			{
				ConnectionState entityClientConnectionState = this._entityClientConnectionState;
				this._entityClientConnectionState = stateChange.CurrentState;
				this.OnStateChange(new StateChangeEventArgs(entityClientConnectionState, currentState));
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06001CFF RID: 7423 RVA: 0x0008CF20 File Offset: 0x0008B120
		// (set) Token: 0x06001D00 RID: 7424 RVA: 0x0008D074 File Offset: 0x0008B274
		[SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
		public override string ConnectionString
		{
			get
			{
				if (this._userConnectionOptions == null)
				{
					return string.Format(CultureInfo.InvariantCulture, "{0}={3}{4};{1}={5};{2}=\"{6}\";", new object[]
					{
						"metadata",
						"provider",
						"provider connection string",
						"reader://",
						this._metadataWorkspace.MetadataWorkspaceId,
						this._storeConnection.GetProviderInvariantName(),
						DbInterception.Dispatch.Connection.GetConnectionString(this._storeConnection, this.InterceptionContext)
					});
				}
				string usersConnectionString = this._userConnectionOptions.UsersConnectionString;
				if (object.ReferenceEquals(this._userConnectionOptions, this._effectiveConnectionOptions) && this._storeConnection != null)
				{
					string text = null;
					try
					{
						text = DbInterception.Dispatch.Connection.GetConnectionString(this._storeConnection, this.InterceptionContext);
					}
					catch (Exception ex)
					{
						if (ex.IsCatchableExceptionType())
						{
							throw new EntityException(Strings.EntityClient_ProviderSpecificError("ConnectionString"), ex);
						}
						throw;
					}
					string text2 = this._userConnectionOptions["provider connection string"];
					if (text != text2 && (!string.IsNullOrEmpty(text) || !string.IsNullOrEmpty(text2)))
					{
						return new EntityConnectionStringBuilder(usersConnectionString)
						{
							ProviderConnectionString = text
						}.ConnectionString;
					}
				}
				return usersConnectionString;
			}
			set
			{
				if (this._initialized)
				{
					throw new InvalidOperationException(Strings.EntityClient_SettingsCannotBeChangedOnOpenConnection);
				}
				this.ChangeConnectionString(value);
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06001D01 RID: 7425 RVA: 0x0008D090 File Offset: 0x0008B290
		internal IEnumerable<ObjectContext> AssociatedContexts
		{
			get
			{
				return this._associatedContexts;
			}
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x0008D098 File Offset: 0x0008B298
		internal virtual void AssociateContext(ObjectContext context)
		{
			if (this._associatedContexts.Count != 0)
			{
				foreach (ObjectContext objectContext in this._associatedContexts.ToArray())
				{
					if (object.ReferenceEquals(context, objectContext) || objectContext.IsDisposed)
					{
						this._associatedContexts.Remove(objectContext);
					}
				}
			}
			this._associatedContexts.Add(context);
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06001D03 RID: 7427 RVA: 0x0008D102 File Offset: 0x0008B302
		internal DbInterceptionContext InterceptionContext
		{
			get
			{
				return DbInterceptionContext.Combine(from c in this.AssociatedContexts
				select c.InterceptionContext);
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06001D04 RID: 7428 RVA: 0x0008D134 File Offset: 0x0008B334
		[SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
		public override int ConnectionTimeout
		{
			get
			{
				if (this._storeConnection == null)
				{
					return 0;
				}
				int connectionTimeout;
				try
				{
					connectionTimeout = DbInterception.Dispatch.Connection.GetConnectionTimeout(this._storeConnection, this.InterceptionContext);
				}
				catch (Exception ex)
				{
					if (ex.IsCatchableExceptionType())
					{
						throw new EntityException(Strings.EntityClient_ProviderSpecificError("ConnectionTimeout"), ex);
					}
					throw;
				}
				return connectionTimeout;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06001D05 RID: 7429 RVA: 0x0008D198 File Offset: 0x0008B398
		public override string Database
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06001D06 RID: 7430 RVA: 0x0008D1A0 File Offset: 0x0008B3A0
		[SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
		public override ConnectionState State
		{
			get
			{
				ConnectionState? fakeConnectionState = this._fakeConnectionState;
				if (fakeConnectionState == null)
				{
					return this._entityClientConnectionState;
				}
				return fakeConnectionState.GetValueOrDefault();
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06001D07 RID: 7431 RVA: 0x0008D1CC File Offset: 0x0008B3CC
		[SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
		public override string DataSource
		{
			get
			{
				if (this._storeConnection == null)
				{
					return string.Empty;
				}
				string dataSource;
				try
				{
					dataSource = DbInterception.Dispatch.Connection.GetDataSource(this._storeConnection, this.InterceptionContext);
				}
				catch (Exception ex)
				{
					if (ex.IsCatchableExceptionType())
					{
						throw new EntityException(Strings.EntityClient_ProviderSpecificError("DataSource"), ex);
					}
					throw;
				}
				return dataSource;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06001D08 RID: 7432 RVA: 0x0008D234 File Offset: 0x0008B434
		[SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
		public override string ServerVersion
		{
			get
			{
				if (this._storeConnection == null)
				{
					throw Error.EntityClient_ConnectionStringNeededBeforeOperation();
				}
				if (this.State != ConnectionState.Open)
				{
					throw Error.EntityClient_ConnectionNotOpen();
				}
				string serverVersion;
				try
				{
					serverVersion = DbInterception.Dispatch.Connection.GetServerVersion(this._storeConnection, this.InterceptionContext);
				}
				catch (Exception ex)
				{
					if (ex.IsCatchableExceptionType())
					{
						throw new EntityException(Strings.EntityClient_ProviderSpecificError("ServerVersion"), ex);
					}
					throw;
				}
				return serverVersion;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06001D09 RID: 7433 RVA: 0x0008D2AC File Offset: 0x0008B4AC
		protected override DbProviderFactory DbProviderFactory
		{
			get
			{
				return EntityProviderFactory.Instance;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06001D0A RID: 7434 RVA: 0x0008D2B3 File Offset: 0x0008B4B3
		internal virtual DbProviderFactory StoreProviderFactory
		{
			get
			{
				return this._providerFactory;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06001D0B RID: 7435 RVA: 0x0008D2BB File Offset: 0x0008B4BB
		public virtual DbConnection StoreConnection
		{
			get
			{
				return this._storeConnection;
			}
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x0008D2C3 File Offset: 0x0008B4C3
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public virtual MetadataWorkspace GetMetadataWorkspace()
		{
			if (this._metadataWorkspace != null)
			{
				return this._metadataWorkspace;
			}
			this._metadataWorkspace = MetadataCache.Instance.GetMetadataWorkspace(this._effectiveConnectionOptions);
			this._initialized = true;
			return this._metadataWorkspace;
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06001D0D RID: 7437 RVA: 0x0008D2F7 File Offset: 0x0008B4F7
		public virtual EntityTransaction CurrentTransaction
		{
			get
			{
				if (this._currentTransaction != null && (DbInterception.Dispatch.Transaction.GetConnection(this._currentTransaction.StoreTransaction, this.InterceptionContext) == null || this.State == ConnectionState.Closed))
				{
					this.ClearCurrentTransaction();
				}
				return this._currentTransaction;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06001D0E RID: 7438 RVA: 0x0008D338 File Offset: 0x0008B538
		internal virtual bool EnlistedInUserTransaction
		{
			get
			{
				bool result;
				try
				{
					result = (this._enlistedTransaction != null && this._enlistedTransaction.TransactionInformation.Status == TransactionStatus.Active);
				}
				catch (ObjectDisposedException)
				{
					this._enlistedTransaction = null;
					result = false;
				}
				return result;
			}
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x0008D3AC File Offset: 0x0008B5AC
		public override void Open()
		{
			this._fakeConnectionState = null;
			if (!DbInterception.Dispatch.CancelableEntityConnection.Opening(this, this.InterceptionContext))
			{
				this._fakeConnectionState = new ConnectionState?(ConnectionState.Open);
				return;
			}
			if (this._storeConnection == null)
			{
				throw Error.EntityClient_ConnectionStringNeededBeforeOperation();
			}
			if (this.State == ConnectionState.Broken)
			{
				throw Error.EntityClient_CannotOpenBrokenConnection();
			}
			if (DbInterception.Dispatch.Connection.GetState(this._storeConnection, this.InterceptionContext) != ConnectionState.Open)
			{
				MetadataWorkspace metadataWorkspace = this.GetMetadataWorkspace();
				try
				{
					DbProviderServices.GetExecutionStrategy(this._storeConnection, metadataWorkspace).Execute(delegate()
					{
						DbInterception.Dispatch.Connection.Open(this._storeConnection, this.InterceptionContext);
					});
				}
				catch (Exception ex)
				{
					if (ex.IsCatchableExceptionType())
					{
						string message = Strings.EntityClient_ProviderSpecificError("Open");
						throw new EntityException(message, ex);
					}
					throw;
				}
				this.ClearTransactions();
			}
			if (this._storeConnection == null || DbInterception.Dispatch.Connection.GetState(this._storeConnection, this.InterceptionContext) != ConnectionState.Open)
			{
				throw Error.EntityClient_ConnectionNotOpen();
			}
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x0008D75C File Offset: 0x0008B95C
		public override async Task OpenAsync(CancellationToken cancellationToken)
		{
			if (this._storeConnection == null)
			{
				throw Error.EntityClient_ConnectionStringNeededBeforeOperation();
			}
			if (this.State == ConnectionState.Broken)
			{
				throw Error.EntityClient_CannotOpenBrokenConnection();
			}
			cancellationToken.ThrowIfCancellationRequested();
			if (DbInterception.Dispatch.Connection.GetState(this._storeConnection, this.InterceptionContext) != ConnectionState.Open)
			{
				MetadataWorkspace metadataWorkspace = this.GetMetadataWorkspace();
				try
				{
					IDbExecutionStrategy executionStrategy = DbProviderServices.GetExecutionStrategy(this._storeConnection, metadataWorkspace);
					await executionStrategy.ExecuteAsync(() => DbInterception.Dispatch.Connection.OpenAsync(this._storeConnection, this.InterceptionContext, cancellationToken), cancellationToken).WithCurrentCulture();
				}
				catch (Exception ex)
				{
					if (ex.IsCatchableExceptionType())
					{
						string message = Strings.EntityClient_ProviderSpecificError("Open");
						throw new EntityException(message, ex);
					}
					throw;
				}
				this.ClearTransactions();
			}
			if (this._storeConnection == null || DbInterception.Dispatch.Connection.GetState(this._storeConnection, this.InterceptionContext) != ConnectionState.Open)
			{
				throw Error.EntityClient_ConnectionNotOpen();
			}
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x0008D7AA File Offset: 0x0008B9AA
		public new virtual EntityCommand CreateCommand()
		{
			return new EntityCommand(null, this);
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x0008D7B3 File Offset: 0x0008B9B3
		protected override DbCommand CreateDbCommand()
		{
			return this.CreateCommand();
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x0008D7BB File Offset: 0x0008B9BB
		public override void Close()
		{
			this._fakeConnectionState = null;
			if (this._storeConnection == null)
			{
				return;
			}
			this.StoreCloseHelper();
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x0008D7D8 File Offset: 0x0008B9D8
		public override void ChangeDatabase(string databaseName)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x0008D7DF File Offset: 0x0008B9DF
		public new virtual EntityTransaction BeginTransaction()
		{
			return base.BeginTransaction() as EntityTransaction;
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x0008D7EC File Offset: 0x0008B9EC
		public new virtual EntityTransaction BeginTransaction(IsolationLevel isolationLevel)
		{
			return base.BeginTransaction(isolationLevel) as EntityTransaction;
		}

		// Token: 0x06001D17 RID: 7447 RVA: 0x0008D8C4 File Offset: 0x0008BAC4
		protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
		{
			if (this._fakeConnectionState != null)
			{
				return new EntityTransaction();
			}
			if (this.CurrentTransaction != null)
			{
				throw new InvalidOperationException(Strings.EntityClient_TransactionAlreadyStarted);
			}
			if (this._storeConnection == null)
			{
				throw Error.EntityClient_ConnectionStringNeededBeforeOperation();
			}
			if (this.State != ConnectionState.Open)
			{
				throw Error.EntityClient_ConnectionNotOpen();
			}
			BeginTransactionInterceptionContext interceptionContext = new BeginTransactionInterceptionContext(this.InterceptionContext);
			if (isolationLevel != IsolationLevel.Unspecified)
			{
				interceptionContext = interceptionContext.WithIsolationLevel(isolationLevel);
			}
			DbTransaction dbTransaction = null;
			try
			{
				IDbExecutionStrategy executionStrategy = DbProviderServices.GetExecutionStrategy(this._storeConnection, this.GetMetadataWorkspace());
				dbTransaction = executionStrategy.Execute<DbTransaction>(delegate()
				{
					if (DbInterception.Dispatch.Connection.GetState(this._storeConnection, this.InterceptionContext) == ConnectionState.Broken)
					{
						DbInterception.Dispatch.Connection.Close(this._storeConnection, interceptionContext);
					}
					if (DbInterception.Dispatch.Connection.GetState(this._storeConnection, this.InterceptionContext) == ConnectionState.Closed)
					{
						DbInterception.Dispatch.Connection.Open(this._storeConnection, interceptionContext);
					}
					return DbInterception.Dispatch.Connection.BeginTransaction(this._storeConnection, interceptionContext);
				});
			}
			catch (Exception ex)
			{
				if (ex.IsCatchableExceptionType())
				{
					throw new EntityException(Strings.EntityClient_ErrorInBeginningTransaction, ex);
				}
				throw;
			}
			if (dbTransaction == null)
			{
				throw new ProviderIncompatibleException(Strings.EntityClient_ReturnedNullOnProviderMethod("BeginTransaction", this._storeConnection.GetType().Name));
			}
			this._currentTransaction = new EntityTransaction(this, dbTransaction);
			return this._currentTransaction;
		}

		// Token: 0x06001D18 RID: 7448 RVA: 0x0008D9DC File Offset: 0x0008BBDC
		internal virtual EntityTransaction UseStoreTransaction(DbTransaction storeTransaction)
		{
			if (storeTransaction == null)
			{
				this.ClearCurrentTransaction();
			}
			else
			{
				if (this.CurrentTransaction != null)
				{
					throw new InvalidOperationException(Strings.DbContext_TransactionAlreadyStarted);
				}
				if (this.EnlistedInUserTransaction)
				{
					throw new InvalidOperationException(Strings.DbContext_TransactionAlreadyEnlistedInUserTransaction);
				}
				DbConnection connection = DbInterception.Dispatch.Transaction.GetConnection(storeTransaction, this.InterceptionContext);
				if (connection == null)
				{
					throw new InvalidOperationException(Strings.DbContext_InvalidTransactionNoConnection);
				}
				if (connection != this.StoreConnection)
				{
					throw new InvalidOperationException(Strings.DbContext_InvalidTransactionForConnection);
				}
				this._currentTransaction = new EntityTransaction(this, storeTransaction);
			}
			return this._currentTransaction;
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x0008DA68 File Offset: 0x0008BC68
		public override void EnlistTransaction(Transaction transaction)
		{
			if (this._storeConnection == null)
			{
				throw Error.EntityClient_ConnectionStringNeededBeforeOperation();
			}
			if (this.State != ConnectionState.Open)
			{
				throw Error.EntityClient_ConnectionNotOpen();
			}
			try
			{
				EnlistTransactionInterceptionContext enlistTransactionInterceptionContext = new EnlistTransactionInterceptionContext(this.InterceptionContext);
				enlistTransactionInterceptionContext = enlistTransactionInterceptionContext.WithTransaction(transaction);
				DbInterception.Dispatch.Connection.EnlistTransaction(this._storeConnection, enlistTransactionInterceptionContext);
				if (transaction != null && !this.EnlistedInUserTransaction)
				{
					transaction.TransactionCompleted += this.EnlistedTransactionCompleted;
				}
				this._enlistedTransaction = transaction;
			}
			catch (Exception ex)
			{
				if (ex.IsCatchableExceptionType())
				{
					throw new EntityException(Strings.EntityClient_ProviderSpecificError("EnlistTransaction"), ex);
				}
				throw;
			}
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x0008DB14 File Offset: 0x0008BD14
		[SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_currentTransaction")]
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.ClearTransactions();
				if (this._storeConnection != null)
				{
					if (this._entityConnectionOwnsStoreConnection)
					{
						this.StoreCloseHelper();
					}
					this.UnsubscribeFromStoreConnectionStateChangeEvents();
					if (this._entityConnectionOwnsStoreConnection)
					{
						DbInterception.Dispatch.Connection.Dispose(this._storeConnection, this.InterceptionContext);
					}
					this._storeConnection = null;
				}
				this._entityClientConnectionState = ConnectionState.Closed;
				this.ChangeConnectionString(string.Empty);
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x0008DB89 File Offset: 0x0008BD89
		internal virtual void ClearCurrentTransaction()
		{
			this._currentTransaction = null;
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x0008DB94 File Offset: 0x0008BD94
		private void ChangeConnectionString(string newConnectionString)
		{
			DbConnectionOptions dbConnectionOptions = EntityConnection._emptyConnectionOptions;
			if (!string.IsNullOrEmpty(newConnectionString))
			{
				dbConnectionOptions = new DbConnectionOptions(newConnectionString, EntityConnectionStringBuilder.ValidKeywords);
			}
			DbProviderFactory dbProviderFactory = null;
			DbConnection dbConnection = null;
			DbConnectionOptions dbConnectionOptions2 = dbConnectionOptions;
			if (!dbConnectionOptions.IsEmpty)
			{
				string text = dbConnectionOptions["name"];
				if (!string.IsNullOrEmpty(text))
				{
					if (1 < dbConnectionOptions.Parsetable.Count)
					{
						throw new ArgumentException(Strings.EntityClient_ExtraParametersWithNamedConnection);
					}
					ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings[text];
					if (connectionStringSettings == null || connectionStringSettings.ProviderName != "System.Data.EntityClient")
					{
						throw new ArgumentException(Strings.EntityClient_InvalidNamedConnection);
					}
					dbConnectionOptions2 = new DbConnectionOptions(connectionStringSettings.ConnectionString, EntityConnectionStringBuilder.ValidKeywords);
					string value = dbConnectionOptions2["name"];
					if (!string.IsNullOrEmpty(value))
					{
						throw new ArgumentException(Strings.EntityClient_NestedNamedConnection(text));
					}
				}
				EntityConnection.ValidateValueForTheKeyword(dbConnectionOptions2, "metadata");
				string key = EntityConnection.ValidateValueForTheKeyword(dbConnectionOptions2, "provider");
				dbProviderFactory = DbConfiguration.DependencyResolver.GetService(key);
				dbConnection = EntityConnection.GetStoreConnection(dbProviderFactory);
				try
				{
					string text2 = dbConnectionOptions2["provider connection string"];
					if (text2 != null)
					{
						DbInterception.Dispatch.Connection.SetConnectionString(dbConnection, new DbConnectionPropertyInterceptionContext<string>(this.InterceptionContext).WithValue(text2));
					}
				}
				catch (Exception ex)
				{
					if (ex.IsCatchableExceptionType())
					{
						throw new EntityException(Strings.EntityClient_ProviderSpecificError("ConnectionString"), ex);
					}
					throw;
				}
			}
			lock (this._connectionStringLock)
			{
				this._providerFactory = dbProviderFactory;
				this._metadataWorkspace = null;
				this.ClearTransactions();
				this.UnsubscribeFromStoreConnectionStateChangeEvents();
				this._storeConnection = dbConnection;
				this.SubscribeToStoreConnectionStateChangeEvents();
				this._userConnectionOptions = dbConnectionOptions;
				this._effectiveConnectionOptions = dbConnectionOptions2;
			}
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x0008DD54 File Offset: 0x0008BF54
		private static string ValidateValueForTheKeyword(DbConnectionOptions effectiveConnectionOptions, string keywordName)
		{
			string text = effectiveConnectionOptions[keywordName];
			if (!string.IsNullOrEmpty(text))
			{
				text = text.Trim();
			}
			if (string.IsNullOrEmpty(text))
			{
				throw new ArgumentException(Strings.EntityClient_ConnectionStringMissingInfo(keywordName));
			}
			return text;
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x0008DD8D File Offset: 0x0008BF8D
		private void ClearTransactions()
		{
			this.ClearCurrentTransaction();
			this.ClearEnlistedTransaction();
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x0008DD9B File Offset: 0x0008BF9B
		private void ClearEnlistedTransaction()
		{
			if (this.EnlistedInUserTransaction)
			{
				this._enlistedTransaction.TransactionCompleted -= this.EnlistedTransactionCompleted;
			}
			this._enlistedTransaction = null;
		}

		// Token: 0x06001D20 RID: 7456 RVA: 0x0008DDC3 File Offset: 0x0008BFC3
		private void EnlistedTransactionCompleted(object sender, TransactionEventArgs e)
		{
			e.Transaction.TransactionCompleted -= this.EnlistedTransactionCompleted;
		}

		// Token: 0x06001D21 RID: 7457 RVA: 0x0008DDDC File Offset: 0x0008BFDC
		private void StoreCloseHelper()
		{
			try
			{
				if (this._storeConnection != null && DbInterception.Dispatch.Connection.GetState(this._storeConnection, this.InterceptionContext) != ConnectionState.Closed)
				{
					DbInterception.Dispatch.Connection.Close(this._storeConnection, this.InterceptionContext);
				}
				this.ClearTransactions();
			}
			catch (Exception ex)
			{
				if (ex.IsCatchableExceptionType())
				{
					throw new EntityException(Strings.EntityClient_ErrorInClosingConnection, ex);
				}
				throw;
			}
		}

		// Token: 0x06001D22 RID: 7458 RVA: 0x0008DE58 File Offset: 0x0008C058
		private static DbConnection GetStoreConnection(DbProviderFactory factory)
		{
			DbConnection dbConnection = factory.CreateConnection();
			if (dbConnection == null)
			{
				throw new ProviderIncompatibleException(Strings.EntityClient_ReturnedNullOnProviderMethod("CreateConnection", factory.GetType().Name));
			}
			return dbConnection;
		}

		// Token: 0x040009EA RID: 2538
		private const string EntityClientProviderName = "System.Data.EntityClient";

		// Token: 0x040009EB RID: 2539
		private const string ProviderInvariantName = "provider";

		// Token: 0x040009EC RID: 2540
		private const string ProviderConnectionString = "provider connection string";

		// Token: 0x040009ED RID: 2541
		private const string ReaderPrefix = "reader://";

		// Token: 0x040009EE RID: 2542
		private readonly object _connectionStringLock;

		// Token: 0x040009EF RID: 2543
		private static readonly DbConnectionOptions _emptyConnectionOptions = new DbConnectionOptions(string.Empty, new string[0]);

		// Token: 0x040009F0 RID: 2544
		private DbConnectionOptions _userConnectionOptions;

		// Token: 0x040009F1 RID: 2545
		private DbConnectionOptions _effectiveConnectionOptions;

		// Token: 0x040009F2 RID: 2546
		private ConnectionState _entityClientConnectionState;

		// Token: 0x040009F3 RID: 2547
		private DbProviderFactory _providerFactory;

		// Token: 0x040009F4 RID: 2548
		private DbConnection _storeConnection;

		// Token: 0x040009F5 RID: 2549
		private readonly bool _entityConnectionOwnsStoreConnection;

		// Token: 0x040009F6 RID: 2550
		private MetadataWorkspace _metadataWorkspace;

		// Token: 0x040009F7 RID: 2551
		private EntityTransaction _currentTransaction;

		// Token: 0x040009F8 RID: 2552
		private Transaction _enlistedTransaction;

		// Token: 0x040009F9 RID: 2553
		private bool _initialized;

		// Token: 0x040009FA RID: 2554
		private ConnectionState? _fakeConnectionState;

		// Token: 0x040009FB RID: 2555
		private readonly List<ObjectContext> _associatedContexts;
	}
}
