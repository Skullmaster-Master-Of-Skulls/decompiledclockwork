using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Resources;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Transactions;
using System.Xml;

namespace System.Data.Entity.Core.Common
{
	// Token: 0x02000209 RID: 521
	public abstract class DbProviderServices : IDbDependencyResolver
	{
		// Token: 0x060012D4 RID: 4820 RVA: 0x0004EDBF File Offset: 0x0004CFBF
		protected DbProviderServices() : this(() => DbConfiguration.DependencyResolver)
		{
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x0004EDF0 File Offset: 0x0004CFF0
		internal DbProviderServices(Func<IDbDependencyResolver> resolver) : this(resolver, new Lazy<DbCommandTreeDispatcher>(() => DbInterception.Dispatch.CommandTree))
		{
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x0004EE1B File Offset: 0x0004D01B
		internal DbProviderServices(Func<IDbDependencyResolver> resolver, Lazy<DbCommandTreeDispatcher> treeDispatcher)
		{
			Check.NotNull<Func<IDbDependencyResolver>>(resolver, "resolver");
			this._resolver = new Lazy<IDbDependencyResolver>(resolver);
			this._treeDispatcher = treeDispatcher;
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x0004EE4D File Offset: 0x0004D04D
		public virtual void RegisterInfoMessageHandler(DbConnection connection, Action<string> handler)
		{
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x0004EE4F File Offset: 0x0004D04F
		public DbCommandDefinition CreateCommandDefinition(DbCommandTree commandTree)
		{
			Check.NotNull<DbCommandTree>(commandTree, "commandTree");
			return this.CreateCommandDefinition(commandTree, new DbInterceptionContext());
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x0004EE6C File Offset: 0x0004D06C
		internal DbCommandDefinition CreateCommandDefinition(DbCommandTree commandTree, DbInterceptionContext interceptionContext)
		{
			this.ValidateDataSpace(commandTree);
			StoreItemCollection storeItemCollection = (StoreItemCollection)commandTree.MetadataWorkspace.GetItemCollection(DataSpace.SSpace);
			commandTree = this._treeDispatcher.Value.Created(commandTree, interceptionContext);
			return this.CreateDbCommandDefinition(storeItemCollection.ProviderManifest, commandTree, interceptionContext);
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x0004EEB4 File Offset: 0x0004D0B4
		internal virtual DbCommandDefinition CreateDbCommandDefinition(DbProviderManifest providerManifest, DbCommandTree commandTree, DbInterceptionContext interceptionContext)
		{
			return this.CreateDbCommandDefinition(providerManifest, commandTree);
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x0004EEC0 File Offset: 0x0004D0C0
		public DbCommandDefinition CreateCommandDefinition(DbProviderManifest providerManifest, DbCommandTree commandTree)
		{
			Check.NotNull<DbProviderManifest>(providerManifest, "providerManifest");
			Check.NotNull<DbCommandTree>(commandTree, "commandTree");
			DbCommandDefinition result;
			try
			{
				result = this.CreateDbCommandDefinition(providerManifest, commandTree);
			}
			catch (ProviderIncompatibleException)
			{
				throw;
			}
			catch (Exception ex)
			{
				if (ex.IsCatchableExceptionType())
				{
					throw new ProviderIncompatibleException(Strings.ProviderDidNotCreateACommandDefinition, ex);
				}
				throw;
			}
			return result;
		}

		// Token: 0x060012DC RID: 4828
		protected abstract DbCommandDefinition CreateDbCommandDefinition(DbProviderManifest providerManifest, DbCommandTree commandTree);

		// Token: 0x060012DD RID: 4829 RVA: 0x0004EF28 File Offset: 0x0004D128
		internal virtual void ValidateDataSpace(DbCommandTree commandTree)
		{
			if (commandTree.DataSpace != DataSpace.SSpace)
			{
				throw new ProviderIncompatibleException(Strings.ProviderRequiresStoreCommandTree);
			}
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x0004EF40 File Offset: 0x0004D140
		internal virtual DbCommand CreateCommand(DbCommandTree commandTree, DbInterceptionContext interceptionContext)
		{
			DbCommandDefinition dbCommandDefinition = this.CreateCommandDefinition(commandTree, interceptionContext);
			return dbCommandDefinition.CreateCommand();
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x0004EF5E File Offset: 0x0004D15E
		public virtual DbCommandDefinition CreateCommandDefinition(DbCommand prototype)
		{
			return new DbCommandDefinition(prototype, new Func<DbCommand, DbCommand>(this.CloneDbCommand));
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x0004EF74 File Offset: 0x0004D174
		protected virtual DbCommand CloneDbCommand(DbCommand fromDbCommand)
		{
			Check.NotNull<DbCommand>(fromDbCommand, "fromDbCommand");
			ICloneable cloneable = fromDbCommand as ICloneable;
			if (cloneable == null)
			{
				throw new ProviderIncompatibleException(Strings.EntityClient_CannotCloneStoreProvider);
			}
			return (DbCommand)cloneable.Clone();
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x0004EFB0 File Offset: 0x0004D1B0
		public string GetProviderManifestToken(DbConnection connection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			string result;
			try
			{
				string dbProviderManifestToken;
				using (new TransactionScope(TransactionScopeOption.Suppress))
				{
					dbProviderManifestToken = this.GetDbProviderManifestToken(connection);
				}
				if (dbProviderManifestToken == null)
				{
					throw new ProviderIncompatibleException(Strings.ProviderDidNotReturnAProviderManifestToken);
				}
				result = dbProviderManifestToken;
			}
			catch (ProviderIncompatibleException)
			{
				throw;
			}
			catch (Exception ex)
			{
				if (ex.IsCatchableExceptionType())
				{
					throw new ProviderIncompatibleException(Strings.ProviderDidNotReturnAProviderManifestToken, ex);
				}
				throw;
			}
			return result;
		}

		// Token: 0x060012E2 RID: 4834
		protected abstract string GetDbProviderManifestToken(DbConnection connection);

		// Token: 0x060012E3 RID: 4835 RVA: 0x0004F03C File Offset: 0x0004D23C
		public DbProviderManifest GetProviderManifest(string manifestToken)
		{
			Check.NotNull<string>(manifestToken, "manifestToken");
			DbProviderManifest result;
			try
			{
				DbProviderManifest dbProviderManifest = this.GetDbProviderManifest(manifestToken);
				if (dbProviderManifest == null)
				{
					throw new ProviderIncompatibleException(Strings.ProviderDidNotReturnAProviderManifest);
				}
				result = dbProviderManifest;
			}
			catch (ProviderIncompatibleException)
			{
				throw;
			}
			catch (Exception ex)
			{
				if (ex.IsCatchableExceptionType())
				{
					throw new ProviderIncompatibleException(Strings.ProviderDidNotReturnAProviderManifest, ex);
				}
				throw;
			}
			return result;
		}

		// Token: 0x060012E4 RID: 4836
		protected abstract DbProviderManifest GetDbProviderManifest(string manifestToken);

		// Token: 0x060012E5 RID: 4837 RVA: 0x0004F0A8 File Offset: 0x0004D2A8
		public static IDbExecutionStrategy GetExecutionStrategy(DbConnection connection)
		{
			return DbProviderServices.GetExecutionStrategy(connection, DbProviderServices.GetProviderFactory(connection), null);
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x0004F0B8 File Offset: 0x0004D2B8
		internal static IDbExecutionStrategy GetExecutionStrategy(DbConnection connection, MetadataWorkspace metadataWorkspace)
		{
			StoreItemCollection storeItemCollection = (StoreItemCollection)metadataWorkspace.GetItemCollection(DataSpace.SSpace);
			return DbProviderServices.GetExecutionStrategy(connection, storeItemCollection.ProviderFactory, null);
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x0004F0DF File Offset: 0x0004D2DF
		protected static IDbExecutionStrategy GetExecutionStrategy(DbConnection connection, string providerInvariantName)
		{
			return DbProviderServices.GetExecutionStrategy(connection, DbProviderServices.GetProviderFactory(connection), providerInvariantName);
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x0004F12C File Offset: 0x0004D32C
		private static IDbExecutionStrategy GetExecutionStrategy(DbConnection connection, DbProviderFactory providerFactory, string providerInvariantName = null)
		{
			EntityConnection entityConnection = connection as EntityConnection;
			if (entityConnection != null)
			{
				connection = entityConnection.StoreConnection;
			}
			string dataSource = DbInterception.Dispatch.Connection.GetDataSource(connection, new DbInterceptionContext());
			ExecutionStrategyKey key = new ExecutionStrategyKey(providerFactory.GetType().FullName, dataSource);
			Func<IDbExecutionStrategy> orAdd = DbProviderServices._executionStrategyFactories.GetOrAdd(key, (ExecutionStrategyKey k) => DbConfiguration.DependencyResolver.GetService(new ExecutionStrategyKey(providerInvariantName ?? DbConfiguration.DependencyResolver.GetService(providerFactory).Name, dataSource)));
			return orAdd();
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x0004F1B4 File Offset: 0x0004D3B4
		public DbSpatialDataReader GetSpatialDataReader(DbDataReader fromReader, string manifestToken)
		{
			DbSpatialDataReader dbSpatialDataReader;
			try
			{
				dbSpatialDataReader = this.GetDbSpatialDataReader(fromReader, manifestToken);
			}
			catch (ProviderIncompatibleException)
			{
				throw;
			}
			catch (Exception ex)
			{
				if (ex.IsCatchableExceptionType())
				{
					throw new ProviderIncompatibleException(Strings.ProviderDidNotReturnSpatialServices, ex);
				}
				throw;
			}
			return dbSpatialDataReader;
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x0004F204 File Offset: 0x0004D404
		[Obsolete("Use GetSpatialServices(DbProviderInfo) or DbConfiguration to ensure the configured spatial services are used. See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.")]
		public DbSpatialServices GetSpatialServices(string manifestToken)
		{
			DbSpatialServices result;
			try
			{
				result = this.DbGetSpatialServices(manifestToken);
			}
			catch (ProviderIncompatibleException)
			{
				throw;
			}
			catch (Exception innerException)
			{
				throw new ProviderIncompatibleException(Strings.ProviderDidNotReturnSpatialServices, innerException);
			}
			return result;
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x0004F264 File Offset: 0x0004D464
		internal static DbSpatialServices GetSpatialServices(IDbDependencyResolver resolver, EntityConnection connection)
		{
			StoreItemCollection storeItemCollection = (StoreItemCollection)connection.GetMetadataWorkspace().GetItemCollection(DataSpace.SSpace);
			DbProviderInfo key = new DbProviderInfo(storeItemCollection.ProviderInvariantName, storeItemCollection.ProviderManifestToken);
			return DbProviderServices.GetSpatialServices(resolver, key, () => DbProviderServices.GetProviderServices(connection.StoreConnection));
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x0004F2BD File Offset: 0x0004D4BD
		public DbSpatialServices GetSpatialServices(DbProviderInfo key)
		{
			return DbProviderServices.GetSpatialServices(this._resolver.Value, key, () => this);
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x0004F31C File Offset: 0x0004D51C
		private static DbSpatialServices GetSpatialServices(IDbDependencyResolver resolver, DbProviderInfo key, Func<DbProviderServices> providerServices)
		{
			DbSpatialServices orAdd = DbProviderServices._spatialServices.GetOrAdd(key, delegate(DbProviderInfo k)
			{
				DbSpatialServices result;
				if ((result = resolver.GetService(k)) == null)
				{
					result = (providerServices().GetSpatialServices(k.ProviderManifestToken) ?? resolver.GetService<DbSpatialServices>());
				}
				return result;
			});
			if (orAdd == null)
			{
				throw new ProviderIncompatibleException(Strings.ProviderDidNotReturnSpatialServices);
			}
			return orAdd;
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x0004F364 File Offset: 0x0004D564
		protected virtual DbSpatialDataReader GetDbSpatialDataReader(DbDataReader fromReader, string manifestToken)
		{
			Check.NotNull<DbDataReader>(fromReader, "fromReader");
			return null;
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x0004F373 File Offset: 0x0004D573
		[Obsolete("Return DbSpatialServices from the GetService method. See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.")]
		protected virtual DbSpatialServices DbGetSpatialServices(string manifestToken)
		{
			return null;
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x0004F376 File Offset: 0x0004D576
		public void SetParameterValue(DbParameter parameter, TypeUsage parameterType, object value)
		{
			Check.NotNull<DbParameter>(parameter, "parameter");
			Check.NotNull<TypeUsage>(parameterType, "parameterType");
			this.SetDbParameterValue(parameter, parameterType, value);
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x0004F399 File Offset: 0x0004D599
		protected virtual void SetDbParameterValue(DbParameter parameter, TypeUsage parameterType, object value)
		{
			Check.NotNull<DbParameter>(parameter, "parameter");
			Check.NotNull<TypeUsage>(parameterType, "parameterType");
			parameter.Value = value;
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x0004F3BA File Offset: 0x0004D5BA
		public static DbProviderServices GetProviderServices(DbConnection connection)
		{
			return DbProviderServices.GetProviderFactory(connection).GetProviderServices();
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x0004F3C8 File Offset: 0x0004D5C8
		public static DbProviderFactory GetProviderFactory(DbConnection connection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			DbProviderFactory providerFactory = connection.GetProviderFactory();
			if (providerFactory == null)
			{
				throw new ProviderIncompatibleException(Strings.EntityClient_ReturnedNullOnProviderMethod("get_ProviderFactory", connection.GetType().ToString()));
			}
			return providerFactory;
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x0004F407 File Offset: 0x0004D607
		public static XmlReader GetConceptualSchemaDefinition(string csdlName)
		{
			Check.NotEmpty(csdlName, "csdlName");
			return DbProviderServices.GetXmlResource("System.Data.Resources.DbProviderServices." + csdlName + ".csdl");
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x0004F42C File Offset: 0x0004D62C
		internal static XmlReader GetXmlResource(string resourceName)
		{
			Stream manifestResourceStream = typeof(DbProviderServices).Assembly().GetManifestResourceStream(resourceName);
			if (manifestResourceStream == null)
			{
				throw Error.InvalidResourceName(resourceName);
			}
			return XmlReader.Create(manifestResourceStream);
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x0004F45F File Offset: 0x0004D65F
		public string CreateDatabaseScript(string providerManifestToken, StoreItemCollection storeItemCollection)
		{
			Check.NotNull<string>(providerManifestToken, "providerManifestToken");
			Check.NotNull<StoreItemCollection>(storeItemCollection, "storeItemCollection");
			return this.DbCreateDatabaseScript(providerManifestToken, storeItemCollection);
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x0004F481 File Offset: 0x0004D681
		protected virtual string DbCreateDatabaseScript(string providerManifestToken, StoreItemCollection storeItemCollection)
		{
			Check.NotNull<string>(providerManifestToken, "providerManifestToken");
			Check.NotNull<StoreItemCollection>(storeItemCollection, "storeItemCollection");
			throw new ProviderIncompatibleException(Strings.ProviderDoesNotSupportCreateDatabaseScript);
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x0004F4A5 File Offset: 0x0004D6A5
		public void CreateDatabase(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<StoreItemCollection>(storeItemCollection, "storeItemCollection");
			this.DbCreateDatabase(connection, commandTimeout, storeItemCollection);
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x0004F4C8 File Offset: 0x0004D6C8
		protected virtual void DbCreateDatabase(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<StoreItemCollection>(storeItemCollection, "storeItemCollection");
			throw new ProviderIncompatibleException(Strings.ProviderDoesNotSupportCreateDatabase);
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x0004F4EC File Offset: 0x0004D6EC
		public bool DatabaseExists(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<StoreItemCollection>(storeItemCollection, "storeItemCollection");
			bool result;
			using (new TransactionScope(TransactionScopeOption.Suppress))
			{
				result = this.DbDatabaseExists(connection, commandTimeout, storeItemCollection);
			}
			return result;
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x0004F540 File Offset: 0x0004D740
		public bool DatabaseExists(DbConnection connection, int? commandTimeout, Lazy<StoreItemCollection> storeItemCollection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<Lazy<StoreItemCollection>>(storeItemCollection, "storeItemCollection");
			bool result;
			using (new TransactionScope(TransactionScopeOption.Suppress))
			{
				result = this.DbDatabaseExists(connection, commandTimeout, storeItemCollection);
			}
			return result;
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x0004F594 File Offset: 0x0004D794
		protected virtual bool DbDatabaseExists(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<StoreItemCollection>(storeItemCollection, "storeItemCollection");
			throw new ProviderIncompatibleException(Strings.ProviderDoesNotSupportDatabaseExists);
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x0004F5B8 File Offset: 0x0004D7B8
		protected virtual bool DbDatabaseExists(DbConnection connection, int? commandTimeout, Lazy<StoreItemCollection> storeItemCollection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<Lazy<StoreItemCollection>>(storeItemCollection, "storeItemCollection");
			return this.DbDatabaseExists(connection, commandTimeout, storeItemCollection.Value);
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x0004F5E0 File Offset: 0x0004D7E0
		public void DeleteDatabase(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<StoreItemCollection>(storeItemCollection, "storeItemCollection");
			this.DbDeleteDatabase(connection, commandTimeout, storeItemCollection);
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x0004F603 File Offset: 0x0004D803
		protected virtual void DbDeleteDatabase(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<StoreItemCollection>(storeItemCollection, "storeItemCollection");
			throw new ProviderIncompatibleException(Strings.ProviderDoesNotSupportDeleteDatabase);
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x0004F628 File Offset: 0x0004D828
		[SuppressMessage("Microsoft.Performance", "CA1820:TestForEmptyStringsUsingStringLength")]
		public static string ExpandDataDirectory(string path)
		{
			if (string.IsNullOrEmpty(path) || !path.StartsWith("|datadirectory|", StringComparison.OrdinalIgnoreCase))
			{
				return path;
			}
			object data = AppDomain.CurrentDomain.GetData("DataDirectory");
			string text = data as string;
			if (data != null && text == null)
			{
				throw new InvalidOperationException(Strings.ADP_InvalidDataDirectory);
			}
			if (text == string.Empty)
			{
				text = AppDomain.CurrentDomain.BaseDirectory;
			}
			if (text == null)
			{
				text = string.Empty;
			}
			path = path.Substring("|datadirectory|".Length);
			if (path.StartsWith("\\", StringComparison.Ordinal))
			{
				path = path.Substring(1);
			}
			string str = text.EndsWith("\\", StringComparison.Ordinal) ? text : (text + "\\");
			path = str + path;
			if (text.Contains(".."))
			{
				throw new ArgumentException(Strings.ExpandingDataDirectoryFailed);
			}
			return path;
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x0004F6FE File Offset: 0x0004D8FE
		protected void AddDependencyResolver(IDbDependencyResolver resolver)
		{
			Check.NotNull<IDbDependencyResolver>(resolver, "resolver");
			this._resolvers.Add(resolver);
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x0004F718 File Offset: 0x0004D918
		public virtual object GetService(Type type, object key)
		{
			return this._resolvers.GetService(type, key);
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x0004F727 File Offset: 0x0004D927
		public virtual IEnumerable<object> GetServices(Type type, object key)
		{
			return this._resolvers.GetServices(type, key);
		}

		// Token: 0x04000587 RID: 1415
		private readonly Lazy<IDbDependencyResolver> _resolver;

		// Token: 0x04000588 RID: 1416
		private readonly Lazy<DbCommandTreeDispatcher> _treeDispatcher;

		// Token: 0x04000589 RID: 1417
		private static readonly ConcurrentDictionary<DbProviderInfo, DbSpatialServices> _spatialServices = new ConcurrentDictionary<DbProviderInfo, DbSpatialServices>();

		// Token: 0x0400058A RID: 1418
		private static readonly ConcurrentDictionary<ExecutionStrategyKey, Func<IDbExecutionStrategy>> _executionStrategyFactories = new ConcurrentDictionary<ExecutionStrategyKey, Func<IDbExecutionStrategy>>();

		// Token: 0x0400058B RID: 1419
		private readonly ResolverChain _resolvers = new ResolverChain();
	}
}
