using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Mapping;
using System.Data.Metadata.Edm;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Transactions;

namespace System.Data.EntityClient
{
	// Token: 0x0200011F RID: 287
	public sealed class EntityConnection : DbConnection
	{
		// Token: 0x06000F30 RID: 3888 RVA: 0x000403E8 File Offset: 0x0003E5E8
		public EntityConnection() : this(string.Empty)
		{
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x000403F5 File Offset: 0x0003E5F5
		public EntityConnection(string connectionString)
		{
			this._connectionStringLock = new object();
			base..ctor();
			GC.SuppressFinalize(this);
			this.ChangeConnectionString(connectionString);
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x00040418 File Offset: 0x0003E618
		public EntityConnection(MetadataWorkspace workspace, DbConnection connection)
		{
			this._connectionStringLock = new object();
			base..ctor();
			GC.SuppressFinalize(this);
			EntityUtil.CheckArgumentNull<MetadataWorkspace>(workspace, "workspace");
			EntityUtil.CheckArgumentNull<DbConnection>(connection, "connection");
			if (!workspace.IsItemCollectionAlreadyRegistered(DataSpace.CSpace))
			{
				throw EntityUtil.Argument(Strings.EntityClient_ItemCollectionsNotRegisteredInWorkspace("EdmItemCollection"));
			}
			if (!workspace.IsItemCollectionAlreadyRegistered(DataSpace.SSpace))
			{
				throw EntityUtil.Argument(Strings.EntityClient_ItemCollectionsNotRegisteredInWorkspace("StoreItemCollection"));
			}
			if (!workspace.IsItemCollectionAlreadyRegistered(DataSpace.CSSpace))
			{
				throw EntityUtil.Argument(Strings.EntityClient_ItemCollectionsNotRegisteredInWorkspace("StorageMappingItemCollection"));
			}
			if (connection.State != ConnectionState.Closed)
			{
				throw EntityUtil.Argument(Strings.EntityClient_ConnectionMustBeClosed);
			}
			if (DbProviderFactories.GetFactory(connection) == null)
			{
				throw EntityUtil.ProviderIncompatible(Strings.EntityClient_DbConnectionHasNoProvider(connection));
			}
			StoreItemCollection storeItemCollection = (StoreItemCollection)workspace.GetItemCollection(DataSpace.SSpace);
			this._providerFactory = storeItemCollection.StoreProviderFactory;
			this._storeConnection = connection;
			this._userOwnsStoreConnection = true;
			this._metadataWorkspace = workspace;
			this._initialized = true;
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000F33 RID: 3891 RVA: 0x000404FC File Offset: 0x0003E6FC
		// (set) Token: 0x06000F34 RID: 3892 RVA: 0x0004062C File Offset: 0x0003E82C
		public override string ConnectionString
		{
			get
			{
				if (this._userConnectionOptions == null)
				{
					string text;
					if (!EntityUtil.TryGetProviderInvariantName(DbProviderFactories.GetFactory(this._storeConnection), out text))
					{
						text = "";
					}
					return string.Format(CultureInfo.InvariantCulture, "{0}={3}{4};{1}={5};{2}=\"{6}\";", new object[]
					{
						"metadata",
						"provider",
						"provider connection string",
						"reader://",
						this._metadataWorkspace.MetadataWorkspaceId,
						text,
						EntityConnection.FormatProviderString(this._storeConnection.ConnectionString)
					});
				}
				string usersConnectionString = this._userConnectionOptions.UsersConnectionString;
				if (this._userConnectionOptions == this._effectiveConnectionOptions && this._storeConnection != null)
				{
					string text2 = null;
					try
					{
						text2 = this._storeConnection.ConnectionString;
					}
					catch (Exception ex)
					{
						if (EntityUtil.IsCatchableExceptionType(ex))
						{
							throw EntityUtil.Provider("ConnectionString", ex);
						}
						throw;
					}
					string text3 = this._userConnectionOptions["provider connection string"];
					if (text2 != text3 && (!string.IsNullOrEmpty(text2) || !string.IsNullOrEmpty(text3)))
					{
						return new EntityConnectionStringBuilder(usersConnectionString)
						{
							ProviderConnectionString = text2
						}.ConnectionString;
					}
				}
				return usersConnectionString;
			}
			set
			{
				this.ValidateChangesPermitted();
				this.ChangeConnectionString(value);
			}
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x0004063B File Offset: 0x0003E83B
		private static string FormatProviderString(string providerString)
		{
			return providerString.Trim().Replace("\"", "\\\"");
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000F36 RID: 3894 RVA: 0x00040654 File Offset: 0x0003E854
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
					connectionTimeout = this._storeConnection.ConnectionTimeout;
				}
				catch (Exception ex)
				{
					if (EntityUtil.IsCatchableExceptionType(ex))
					{
						throw EntityUtil.Provider("ConnectionTimeout", ex);
					}
					throw;
				}
				return connectionTimeout;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000F37 RID: 3895 RVA: 0x000406A4 File Offset: 0x0003E8A4
		public override string Database
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000F38 RID: 3896 RVA: 0x000406AC File Offset: 0x0003E8AC
		public override ConnectionState State
		{
			get
			{
				ConnectionState result;
				try
				{
					if (this._entityClientConnectionState == ConnectionState.Open && this.StoreConnection.State != ConnectionState.Open)
					{
						result = ConnectionState.Broken;
					}
					else
					{
						result = this._entityClientConnectionState;
					}
				}
				catch (Exception ex)
				{
					if (EntityUtil.IsCatchableExceptionType(ex))
					{
						throw EntityUtil.Provider("State", ex);
					}
					throw;
				}
				return result;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000F39 RID: 3897 RVA: 0x00040708 File Offset: 0x0003E908
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
					dataSource = this._storeConnection.DataSource;
				}
				catch (Exception ex)
				{
					if (EntityUtil.IsCatchableExceptionType(ex))
					{
						throw EntityUtil.Provider("DataSource", ex);
					}
					throw;
				}
				return dataSource;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000F3A RID: 3898 RVA: 0x0004075C File Offset: 0x0003E95C
		public override string ServerVersion
		{
			get
			{
				if (this._storeConnection == null)
				{
					throw EntityUtil.InvalidOperation(Strings.EntityClient_ConnectionStringNeededBeforeOperation);
				}
				if (this.State != ConnectionState.Open)
				{
					throw EntityUtil.InvalidOperation(Strings.EntityClient_ConnectionNotOpen);
				}
				string serverVersion;
				try
				{
					serverVersion = this._storeConnection.ServerVersion;
				}
				catch (Exception ex)
				{
					if (EntityUtil.IsCatchableExceptionType(ex))
					{
						throw EntityUtil.Provider("ServerVersion", ex);
					}
					throw;
				}
				return serverVersion;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000F3B RID: 3899 RVA: 0x000407C8 File Offset: 0x0003E9C8
		protected override DbProviderFactory DbProviderFactory
		{
			get
			{
				return EntityProviderFactory.Instance;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000F3C RID: 3900 RVA: 0x000407CF File Offset: 0x0003E9CF
		internal DbProviderFactory StoreProviderFactory
		{
			get
			{
				return this._providerFactory;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000F3D RID: 3901 RVA: 0x000407D7 File Offset: 0x0003E9D7
		public DbConnection StoreConnection
		{
			get
			{
				return this._storeConnection;
			}
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x000407DF File Offset: 0x0003E9DF
		[CLSCompliant(false)]
		public MetadataWorkspace GetMetadataWorkspace()
		{
			return this.GetMetadataWorkspace(true);
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x000407E8 File Offset: 0x0003E9E8
		private bool ShouldRecalculateMetadataArtifactLoader(List<MetadataArtifactLoader> loaders)
		{
			return loaders.Any((MetadataArtifactLoader loader) => loader.GetType() == typeof(MetadataArtifactLoaderCompositeFile));
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x00040814 File Offset: 0x0003EA14
		internal MetadataWorkspace GetMetadataWorkspace(bool initializeAllCollections)
		{
			if (this._metadataWorkspace == null || (initializeAllCollections && !this._metadataWorkspace.IsItemCollectionAlreadyRegistered(DataSpace.SSpace)))
			{
				object connectionStringLock = this._connectionStringLock;
				lock (connectionStringLock)
				{
					EdmItemCollection edmItemCollection;
					if (this._metadataWorkspace == null)
					{
						MetadataWorkspace metadataWorkspace = new MetadataWorkspace();
						List<MetadataArtifactLoader> list = new List<MetadataArtifactLoader>();
						string text = this._effectiveConnectionOptions["metadata"];
						if (!string.IsNullOrEmpty(text))
						{
							list = MetadataCache.GetOrCreateMetdataArtifactLoader(text);
							if (!this.ShouldRecalculateMetadataArtifactLoader(list))
							{
								this._artifactLoader = MetadataArtifactLoader.Create(list);
							}
							else
							{
								this._artifactLoader = MetadataArtifactLoader.Create(MetadataCache.SplitPaths(text));
							}
						}
						else
						{
							this._artifactLoader = MetadataArtifactLoader.Create(list);
						}
						edmItemCollection = EntityConnection.LoadEdmItemCollection(metadataWorkspace, this._artifactLoader);
						this._metadataWorkspace = metadataWorkspace;
					}
					else
					{
						edmItemCollection = (EdmItemCollection)this._metadataWorkspace.GetItemCollection(DataSpace.CSpace);
					}
					if (initializeAllCollections && !this._metadataWorkspace.IsItemCollectionAlreadyRegistered(DataSpace.SSpace))
					{
						EntityConnection.LoadStoreItemCollections(this._metadataWorkspace, this._storeConnection, this._providerFactory, this._effectiveConnectionOptions, edmItemCollection, this._artifactLoader);
						this._artifactLoader = null;
						this._initialized = true;
					}
				}
			}
			return this._metadataWorkspace;
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000F41 RID: 3905 RVA: 0x00040958 File Offset: 0x0003EB58
		internal EntityTransaction CurrentTransaction
		{
			get
			{
				if (this._currentTransaction != null && (this._currentTransaction.StoreTransaction.Connection == null || this.State == ConnectionState.Closed))
				{
					this.ClearCurrentTransaction();
				}
				return this._currentTransaction;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000F42 RID: 3906 RVA: 0x00040988 File Offset: 0x0003EB88
		internal bool EnlistedInUserTransaction
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

		// Token: 0x06000F43 RID: 3907 RVA: 0x000409DC File Offset: 0x0003EBDC
		public override void Open()
		{
			if (this._storeConnection == null)
			{
				throw EntityUtil.InvalidOperation(Strings.EntityClient_ConnectionStringNeededBeforeOperation);
			}
			if (this.State != ConnectionState.Closed)
			{
				throw EntityUtil.InvalidOperation(Strings.EntityClient_CannotReopenConnection);
			}
			bool closeOriginalConnectionOnFailure = false;
			this.OpenStoreConnectionIf(this._storeConnection.State != ConnectionState.Open, this._storeConnection, null, "EntityClient_ProviderSpecificError", "Open", ref closeOriginalConnectionOnFailure);
			if (this._storeConnection == null || this._storeConnection.State != ConnectionState.Open)
			{
				throw EntityUtil.InvalidOperation(Strings.EntityClient_ConnectionNotOpen);
			}
			this.InitializeMetadata(this._storeConnection, this._storeConnection, closeOriginalConnectionOnFailure);
			this.SetEntityClientConnectionStateToOpen();
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x00040A78 File Offset: 0x0003EC78
		private void OpenStoreConnectionIf(bool openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, string exceptionCode, string attemptedOperation, ref bool closeStoreConnectionOnFailure)
		{
			try
			{
				if (openCondition)
				{
					storeConnectionToOpen.Open();
					closeStoreConnectionOnFailure = true;
				}
				this.ResetStoreConnection(storeConnectionToOpen, originalConnection, false);
				this.ClearTransactions();
			}
			catch (Exception ex)
			{
				if (EntityUtil.IsCatchableExceptionType(ex))
				{
					string message = string.IsNullOrEmpty(attemptedOperation) ? EntityRes.GetString(exceptionCode) : EntityRes.GetString(exceptionCode, new object[]
					{
						attemptedOperation
					});
					throw EntityUtil.ProviderExceptionWithMessage(message, ex);
				}
				throw;
			}
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x00040AEC File Offset: 0x0003ECEC
		private void InitializeMetadata(DbConnection newConnection, DbConnection originalConnection, bool closeOriginalConnectionOnFailure)
		{
			try
			{
				this.GetMetadataWorkspace();
			}
			catch (Exception e)
			{
				if (EntityUtil.IsCatchableExceptionType(e))
				{
					this.ResetStoreConnection(newConnection, originalConnection, closeOriginalConnectionOnFailure);
				}
				throw;
			}
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x00040B28 File Offset: 0x0003ED28
		private void SetEntityClientConnectionStateToOpen()
		{
			this._entityClientConnectionState = ConnectionState.Open;
			this.OnStateChange(EntityConnection.StateChangeOpen);
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x00040B3C File Offset: 0x0003ED3C
		private void ResetStoreConnection(DbConnection newConnection, DbConnection originalConnection, bool closeOriginalConnection)
		{
			this._storeConnection = newConnection;
			if (closeOriginalConnection && originalConnection != null)
			{
				originalConnection.Close();
			}
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x00040B51 File Offset: 0x0003ED51
		public new EntityCommand CreateCommand()
		{
			return new EntityCommand(null, this);
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x00040B5A File Offset: 0x0003ED5A
		protected override DbCommand CreateDbCommand()
		{
			return this.CreateCommand();
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x00040B62 File Offset: 0x0003ED62
		public override void Close()
		{
			if (this._storeConnection == null)
			{
				return;
			}
			this.CloseHelper();
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x00013A81 File Offset: 0x00011C81
		public override void ChangeDatabase(string databaseName)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x00040B73 File Offset: 0x0003ED73
		public new EntityTransaction BeginTransaction()
		{
			return base.BeginTransaction() as EntityTransaction;
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x00040B80 File Offset: 0x0003ED80
		public new EntityTransaction BeginTransaction(IsolationLevel isolationLevel)
		{
			return base.BeginTransaction(isolationLevel) as EntityTransaction;
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x00040B90 File Offset: 0x0003ED90
		protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
		{
			if (this.CurrentTransaction != null)
			{
				throw EntityUtil.InvalidOperation(Strings.EntityClient_TransactionAlreadyStarted);
			}
			if (this._storeConnection == null)
			{
				throw EntityUtil.InvalidOperation(Strings.EntityClient_ConnectionStringNeededBeforeOperation);
			}
			if (this.State != ConnectionState.Open)
			{
				throw EntityUtil.InvalidOperation(Strings.EntityClient_ConnectionNotOpen);
			}
			DbTransaction dbTransaction = null;
			try
			{
				dbTransaction = this._storeConnection.BeginTransaction(isolationLevel);
			}
			catch (Exception ex)
			{
				if (EntityUtil.IsCatchableExceptionType(ex))
				{
					throw EntityUtil.ProviderExceptionWithMessage(Strings.EntityClient_ErrorInBeginningTransaction, ex);
				}
				throw;
			}
			if (dbTransaction == null)
			{
				throw EntityUtil.ProviderIncompatible(Strings.EntityClient_ReturnedNullOnProviderMethod("BeginTransaction", this._storeConnection.GetType().Name));
			}
			this._currentTransaction = new EntityTransaction(this, dbTransaction);
			return this._currentTransaction;
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x00040C48 File Offset: 0x0003EE48
		public override void EnlistTransaction(Transaction transaction)
		{
			if (this._storeConnection == null)
			{
				throw EntityUtil.InvalidOperation(Strings.EntityClient_ConnectionStringNeededBeforeOperation);
			}
			if (this.State != ConnectionState.Open)
			{
				throw EntityUtil.InvalidOperation(Strings.EntityClient_ConnectionNotOpen);
			}
			try
			{
				this._storeConnection.EnlistTransaction(transaction);
				if (transaction != null && !this.EnlistedInUserTransaction)
				{
					transaction.TransactionCompleted += this.EnlistedTransactionCompleted;
				}
				this._enlistedTransaction = transaction;
			}
			catch (Exception ex)
			{
				if (EntityUtil.IsCatchableExceptionType(ex))
				{
					throw EntityUtil.Provider("EnlistTransaction", ex);
				}
				throw;
			}
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x00040CDC File Offset: 0x0003EEDC
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.ClearTransactions();
				bool flag = this.EntityCloseHelper(false, this.State);
				if (this._storeConnection != null)
				{
					this.StoreCloseHelper();
					if (this._storeConnection != null)
					{
						if (!this._userOwnsStoreConnection)
						{
							this._storeConnection.Dispose();
						}
						this._storeConnection = null;
					}
				}
				this.ChangeConnectionString(string.Empty);
				if (flag)
				{
					this.OnStateChange(EntityConnection.StateChangeClosed);
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x00040D50 File Offset: 0x0003EF50
		private void ChangeConnectionString(string newConnectionString)
		{
			DbConnectionOptions dbConnectionOptions = EntityConnection.s_emptyConnectionOptions;
			if (!string.IsNullOrEmpty(newConnectionString))
			{
				dbConnectionOptions = new DbConnectionOptions(newConnectionString, EntityConnectionStringBuilder.Synonyms);
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
						throw EntityUtil.Argument(Strings.EntityClient_ExtraParametersWithNamedConnection);
					}
					ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings[text];
					if (connectionStringSettings == null || connectionStringSettings.ProviderName != "System.Data.EntityClient")
					{
						throw EntityUtil.Argument(Strings.EntityClient_InvalidNamedConnection);
					}
					dbConnectionOptions2 = new DbConnectionOptions(connectionStringSettings.ConnectionString, EntityConnectionStringBuilder.Synonyms);
					string value = dbConnectionOptions2["name"];
					if (!string.IsNullOrEmpty(value))
					{
						throw EntityUtil.Argument(Strings.EntityClient_NestedNamedConnection(text));
					}
				}
				EntityConnection.ValidateValueForTheKeyword(dbConnectionOptions2, "metadata");
				string providerString = EntityConnection.ValidateValueForTheKeyword(dbConnectionOptions2, "provider");
				dbProviderFactory = this.GetFactory(providerString);
				dbConnection = this.GetStoreConnection(dbProviderFactory);
				try
				{
					string text2 = dbConnectionOptions2["provider connection string"];
					if (text2 != null)
					{
						dbConnection.ConnectionString = text2;
					}
				}
				catch (Exception ex)
				{
					if (EntityUtil.IsCatchableExceptionType(ex))
					{
						throw EntityUtil.Provider("ConnectionString", ex);
					}
					throw;
				}
			}
			object connectionStringLock = this._connectionStringLock;
			lock (connectionStringLock)
			{
				this._providerFactory = dbProviderFactory;
				this._metadataWorkspace = null;
				this.ClearTransactions();
				this.ResetStoreConnection(dbConnection, null, false);
				this._userConnectionOptions = dbConnectionOptions;
				this._effectiveConnectionOptions = dbConnectionOptions2;
			}
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x00040EE4 File Offset: 0x0003F0E4
		private static string ValidateValueForTheKeyword(DbConnectionOptions effectiveConnectionOptions, string keywordName)
		{
			string text = effectiveConnectionOptions[keywordName];
			if (!string.IsNullOrEmpty(text))
			{
				text = text.Trim();
			}
			if (string.IsNullOrEmpty(text))
			{
				throw EntityUtil.Argument(Strings.EntityClient_ConnectionStringMissingInfo(keywordName));
			}
			return text;
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x00040F20 File Offset: 0x0003F120
		private static EdmItemCollection LoadEdmItemCollection(MetadataWorkspace workspace, MetadataArtifactLoader artifactLoader)
		{
			string cacheKey = EntityConnection.CreateMetadataCacheKey(artifactLoader.GetOriginalPaths(DataSpace.CSpace), null, null);
			object token;
			EdmItemCollection orCreateEdmItemCollection = MetadataCache.GetOrCreateEdmItemCollection(cacheKey, artifactLoader, out token);
			workspace.RegisterItemCollection(orCreateEdmItemCollection);
			workspace.AddMetadataEntryToken(token);
			return orCreateEdmItemCollection;
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x00040F58 File Offset: 0x0003F158
		private static void LoadStoreItemCollections(MetadataWorkspace workspace, DbConnection storeConnection, DbProviderFactory factory, DbConnectionOptions connectionOptions, EdmItemCollection edmItemCollection, MetadataArtifactLoader artifactLoader)
		{
			string text = connectionOptions["provider connection string"];
			if (string.IsNullOrEmpty(text) && storeConnection != null)
			{
				text = storeConnection.ConnectionString;
			}
			string cacheKey = EntityConnection.CreateMetadataCacheKey(artifactLoader.GetOriginalPaths(), connectionOptions["provider"], text);
			object token;
			StorageMappingItemCollection orCreateStoreAndMappingItemCollections = MetadataCache.GetOrCreateStoreAndMappingItemCollections(cacheKey, artifactLoader, edmItemCollection, out token);
			workspace.RegisterItemCollection(orCreateStoreAndMappingItemCollections.StoreItemCollection);
			workspace.RegisterItemCollection(orCreateStoreAndMappingItemCollections);
			workspace.AddMetadataEntryToken(token);
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x00040FC4 File Offset: 0x0003F1C4
		private static string GetErrorMessageWorthyProviderName(DbProviderFactory factory)
		{
			EntityUtil.CheckArgumentNull<DbProviderFactory>(factory, "factory");
			string fullName;
			if (!EntityUtil.TryGetProviderInvariantName(factory, out fullName))
			{
				fullName = factory.GetType().FullName;
			}
			return fullName;
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x00040FF4 File Offset: 0x0003F1F4
		private static string CreateMetadataCacheKey(IList<string> paths, string providerName, string providerConnectionString)
		{
			int num = 0;
			string result;
			EntityConnection.CreateMetadataCacheKeyWithCount(paths, providerName, providerConnectionString, false, ref num, out result);
			EntityConnection.CreateMetadataCacheKeyWithCount(paths, providerName, providerConnectionString, true, ref num, out result);
			return result;
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x00041020 File Offset: 0x0003F220
		private static void CreateMetadataCacheKeyWithCount(IList<string> paths, string providerName, string providerConnectionString, bool buildResult, ref int resultCount, out string result)
		{
			StringBuilder stringBuilder;
			if (buildResult)
			{
				stringBuilder = new StringBuilder(resultCount);
			}
			else
			{
				stringBuilder = null;
			}
			resultCount = 0;
			if (!string.IsNullOrEmpty(providerName))
			{
				resultCount += providerName.Length + 1;
				if (buildResult)
				{
					stringBuilder.Append(providerName);
					stringBuilder.Append(";");
				}
			}
			if (paths != null)
			{
				for (int i = 0; i < paths.Count; i++)
				{
					if (paths[i].Length > 0)
					{
						if (i > 0)
						{
							resultCount++;
							if (buildResult)
							{
								stringBuilder.Append("|");
							}
						}
						resultCount += paths[i].Length;
						if (buildResult)
						{
							stringBuilder.Append(paths[i]);
						}
					}
				}
				resultCount++;
				if (buildResult)
				{
					stringBuilder.Append(";");
				}
			}
			if (!string.IsNullOrEmpty(providerConnectionString))
			{
				resultCount += providerConnectionString.Length;
				if (buildResult)
				{
					stringBuilder.Append(providerConnectionString);
				}
			}
			if (buildResult)
			{
				result = stringBuilder.ToString();
				return;
			}
			result = null;
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x00041119 File Offset: 0x0003F319
		private void ClearTransactions()
		{
			this.ClearCurrentTransaction();
			this.ClearEnlistedTransaction();
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x00041127 File Offset: 0x0003F327
		internal void ClearCurrentTransaction()
		{
			this._currentTransaction = null;
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x00041130 File Offset: 0x0003F330
		private void ClearEnlistedTransaction()
		{
			if (this.EnlistedInUserTransaction)
			{
				this._enlistedTransaction.TransactionCompleted -= this.EnlistedTransactionCompleted;
			}
			this._enlistedTransaction = null;
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x00041158 File Offset: 0x0003F358
		private void EnlistedTransactionCompleted(object sender, TransactionEventArgs e)
		{
			e.Transaction.TransactionCompleted -= this.EnlistedTransactionCompleted;
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x00041174 File Offset: 0x0003F374
		private void CloseHelper()
		{
			ConnectionState state = this.State;
			this.StoreCloseHelper();
			this.EntityCloseHelper(true, state);
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x00041198 File Offset: 0x0003F398
		private void StoreCloseHelper()
		{
			try
			{
				if (this._storeConnection != null && this._storeConnection.State != ConnectionState.Closed)
				{
					this._storeConnection.Close();
				}
				this.ClearTransactions();
			}
			catch (Exception ex)
			{
				if (EntityUtil.IsCatchableExceptionType(ex))
				{
					throw EntityUtil.ProviderExceptionWithMessage(Strings.EntityClient_ErrorInClosingConnection, ex);
				}
				throw;
			}
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x000411F4 File Offset: 0x0003F3F4
		private bool EntityCloseHelper(bool fireEventOnStateChange, ConnectionState previousState)
		{
			bool result = false;
			this._entityClientConnectionState = ConnectionState.Closed;
			if (previousState == ConnectionState.Open)
			{
				if (fireEventOnStateChange)
				{
					this.OnStateChange(EntityConnection.StateChangeClosed);
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x00041221 File Offset: 0x0003F421
		private void ValidateChangesPermitted()
		{
			if (this._initialized)
			{
				throw EntityUtil.InvalidOperation(Strings.EntityClient_SettingsCannotBeChangedOnOpenConnection);
			}
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x00041238 File Offset: 0x0003F438
		private DbProviderFactory GetFactory(string providerString)
		{
			DbProviderFactory factory;
			try
			{
				factory = DbProviderFactories.GetFactory(providerString);
			}
			catch (ArgumentException inner)
			{
				throw EntityUtil.Argument(Strings.EntityClient_InvalidStoreProvider, inner);
			}
			return factory;
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x0004126C File Offset: 0x0003F46C
		private DbConnection GetStoreConnection(DbProviderFactory factory)
		{
			DbConnection dbConnection = factory.CreateConnection();
			if (dbConnection == null)
			{
				throw EntityUtil.ProviderIncompatible(Strings.EntityClient_ReturnedNullOnProviderMethod("CreateConnection", factory.GetType().Name));
			}
			return dbConnection;
		}

		// Token: 0x04000A09 RID: 2569
		private const string s_metadataPathSeparator = "|";

		// Token: 0x04000A0A RID: 2570
		private const string s_semicolonSeparator = ";";

		// Token: 0x04000A0B RID: 2571
		private const string s_entityClientProviderName = "System.Data.EntityClient";

		// Token: 0x04000A0C RID: 2572
		private const string s_providerInvariantName = "provider";

		// Token: 0x04000A0D RID: 2573
		private const string s_providerConnectionString = "provider connection string";

		// Token: 0x04000A0E RID: 2574
		private const string s_readerPrefix = "reader://";

		// Token: 0x04000A0F RID: 2575
		internal static readonly StateChangeEventArgs StateChangeClosed = new StateChangeEventArgs(ConnectionState.Open, ConnectionState.Closed);

		// Token: 0x04000A10 RID: 2576
		internal static readonly StateChangeEventArgs StateChangeOpen = new StateChangeEventArgs(ConnectionState.Closed, ConnectionState.Open);

		// Token: 0x04000A11 RID: 2577
		private readonly object _connectionStringLock;

		// Token: 0x04000A12 RID: 2578
		private static readonly DbConnectionOptions s_emptyConnectionOptions = new DbConnectionOptions(string.Empty, null);

		// Token: 0x04000A13 RID: 2579
		private DbConnectionOptions _userConnectionOptions;

		// Token: 0x04000A14 RID: 2580
		private DbConnectionOptions _effectiveConnectionOptions;

		// Token: 0x04000A15 RID: 2581
		private ConnectionState _entityClientConnectionState;

		// Token: 0x04000A16 RID: 2582
		private DbProviderFactory _providerFactory;

		// Token: 0x04000A17 RID: 2583
		private DbConnection _storeConnection;

		// Token: 0x04000A18 RID: 2584
		private readonly bool _userOwnsStoreConnection;

		// Token: 0x04000A19 RID: 2585
		private MetadataWorkspace _metadataWorkspace;

		// Token: 0x04000A1A RID: 2586
		private EntityTransaction _currentTransaction;

		// Token: 0x04000A1B RID: 2587
		private Transaction _enlistedTransaction;

		// Token: 0x04000A1C RID: 2588
		private bool _initialized;

		// Token: 0x04000A1D RID: 2589
		private MetadataArtifactLoader _artifactLoader;
	}
}
