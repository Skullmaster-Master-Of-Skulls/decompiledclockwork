using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Data.Entity.Migrations.History;
using System.Data.Entity.Migrations.Sql;
using System.Data.Entity.Resources;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace System.Data.Entity
{
	// Token: 0x020000D9 RID: 217
	public class DbConfiguration
	{
		// Token: 0x0600057B RID: 1403 RVA: 0x0002490B File Offset: 0x00022B0B
		protected internal DbConfiguration() : this(new InternalConfiguration(null, null, null, null, null))
		{
			this._internalConfiguration.Owner = this;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00024929 File Offset: 0x00022B29
		internal DbConfiguration(InternalConfiguration internalConfiguration)
		{
			this._internalConfiguration = internalConfiguration;
			this._internalConfiguration.Owner = this;
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00024944 File Offset: 0x00022B44
		public static void SetConfiguration(DbConfiguration configuration)
		{
			Check.NotNull<DbConfiguration>(configuration, "configuration");
			InternalConfiguration.Instance = configuration.InternalConfiguration;
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0002495D File Offset: 0x00022B5D
		public static void LoadConfiguration(Type contextType)
		{
			Check.NotNull<Type>(contextType, "contextType");
			if (!typeof(DbContext).IsAssignableFrom(contextType))
			{
				throw new ArgumentException(Strings.BadContextTypeForDiscovery(contextType.Name));
			}
			DbConfigurationManager.Instance.EnsureLoadedForContext(contextType);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00024999 File Offset: 0x00022B99
		public static void LoadConfiguration(Assembly assemblyHint)
		{
			Check.NotNull<Assembly>(assemblyHint, "assemblyHint");
			DbConfigurationManager.Instance.EnsureLoadedForAssembly(assemblyHint, null);
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000580 RID: 1408 RVA: 0x000249B3 File Offset: 0x00022BB3
		// (remove) Token: 0x06000581 RID: 1409 RVA: 0x000249CC File Offset: 0x00022BCC
		public static event EventHandler<DbConfigurationLoadedEventArgs> Loaded
		{
			add
			{
				Check.NotNull<EventHandler<DbConfigurationLoadedEventArgs>>(value, "value");
				DbConfigurationManager.Instance.AddLoadedHandler(value);
			}
			remove
			{
				Check.NotNull<EventHandler<DbConfigurationLoadedEventArgs>>(value, "value");
				DbConfigurationManager.Instance.RemoveLoadedHandler(value);
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x000249E5 File Offset: 0x00022BE5
		protected internal void AddDependencyResolver(IDbDependencyResolver resolver)
		{
			Check.NotNull<IDbDependencyResolver>(resolver, "resolver");
			this._internalConfiguration.CheckNotLocked("AddDependencyResolver");
			this._internalConfiguration.AddDependencyResolver(resolver, false);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00024A10 File Offset: 0x00022C10
		protected internal void AddDefaultResolver(IDbDependencyResolver resolver)
		{
			Check.NotNull<IDbDependencyResolver>(resolver, "resolver");
			this._internalConfiguration.CheckNotLocked("AddDefaultResolver");
			this._internalConfiguration.AddDefaultResolver(resolver);
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x00024A3A File Offset: 0x00022C3A
		public static IDbDependencyResolver DependencyResolver
		{
			get
			{
				return InternalConfiguration.Instance.DependencyResolver;
			}
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00024A46 File Offset: 0x00022C46
		protected internal void SetProviderServices(string providerInvariantName, DbProviderServices provider)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			Check.NotNull<DbProviderServices>(provider, "provider");
			this._internalConfiguration.CheckNotLocked("SetProviderServices");
			this._internalConfiguration.RegisterSingleton<DbProviderServices>(provider, providerInvariantName);
			this.AddDefaultResolver(provider);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00024A84 File Offset: 0x00022C84
		protected internal void SetProviderFactory(string providerInvariantName, DbProviderFactory providerFactory)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			Check.NotNull<DbProviderFactory>(providerFactory, "providerFactory");
			this._internalConfiguration.CheckNotLocked("SetProviderFactory");
			this._internalConfiguration.RegisterSingleton<DbProviderFactory>(providerFactory, providerInvariantName);
			this._internalConfiguration.AddDependencyResolver(new InvariantNameResolver(providerFactory, providerInvariantName), false);
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00024AD9 File Offset: 0x00022CD9
		protected internal void SetExecutionStrategy(string providerInvariantName, Func<IDbExecutionStrategy> getExecutionStrategy)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			Check.NotNull<Func<IDbExecutionStrategy>>(getExecutionStrategy, "getExecutionStrategy");
			this._internalConfiguration.CheckNotLocked("SetExecutionStrategy");
			this._internalConfiguration.AddDependencyResolver(new ExecutionStrategyResolver<IDbExecutionStrategy>(providerInvariantName, null, getExecutionStrategy), false);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00024B18 File Offset: 0x00022D18
		protected internal void SetExecutionStrategy(string providerInvariantName, Func<IDbExecutionStrategy> getExecutionStrategy, string serverName)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			Check.NotEmpty(serverName, "serverName");
			Check.NotNull<Func<IDbExecutionStrategy>>(getExecutionStrategy, "getExecutionStrategy");
			this._internalConfiguration.CheckNotLocked("SetExecutionStrategy");
			this._internalConfiguration.AddDependencyResolver(new ExecutionStrategyResolver<IDbExecutionStrategy>(providerInvariantName, serverName, getExecutionStrategy), false);
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00024B6D File Offset: 0x00022D6D
		protected internal void SetDefaultTransactionHandler(Func<TransactionHandler> transactionHandlerFactory)
		{
			Check.NotNull<Func<TransactionHandler>>(transactionHandlerFactory, "transactionHandlerFactory");
			this._internalConfiguration.CheckNotLocked("SetTransactionHandler");
			this._internalConfiguration.AddDependencyResolver(new TransactionHandlerResolver(transactionHandlerFactory, null, null), false);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00024B9F File Offset: 0x00022D9F
		protected internal void SetTransactionHandler(string providerInvariantName, Func<TransactionHandler> transactionHandlerFactory)
		{
			Check.NotNull<Func<TransactionHandler>>(transactionHandlerFactory, "transactionHandlerFactory");
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			this._internalConfiguration.CheckNotLocked("SetTransactionHandler");
			this._internalConfiguration.AddDependencyResolver(new TransactionHandlerResolver(transactionHandlerFactory, providerInvariantName, null), false);
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00024BE0 File Offset: 0x00022DE0
		protected internal void SetTransactionHandler(string providerInvariantName, Func<TransactionHandler> transactionHandlerFactory, string serverName)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			Check.NotNull<Func<TransactionHandler>>(transactionHandlerFactory, "transactionHandlerFactory");
			Check.NotEmpty(serverName, "serverName");
			this._internalConfiguration.CheckNotLocked("SetTransactionHandler");
			this._internalConfiguration.AddDependencyResolver(new TransactionHandlerResolver(transactionHandlerFactory, providerInvariantName, serverName), false);
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00024C35 File Offset: 0x00022E35
		protected internal void SetDefaultConnectionFactory(IDbConnectionFactory connectionFactory)
		{
			Check.NotNull<IDbConnectionFactory>(connectionFactory, "connectionFactory");
			this._internalConfiguration.CheckNotLocked("SetDefaultConnectionFactory");
			this._internalConfiguration.RegisterSingleton<IDbConnectionFactory>(connectionFactory);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00024C5F File Offset: 0x00022E5F
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Pluralization")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "pluralization")]
		protected internal void SetPluralizationService(IPluralizationService pluralizationService)
		{
			Check.NotNull<IPluralizationService>(pluralizationService, "pluralizationService");
			this._internalConfiguration.CheckNotLocked("SetPluralizationService");
			this._internalConfiguration.RegisterSingleton<IPluralizationService>(pluralizationService);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00024C89 File Offset: 0x00022E89
		protected internal void SetDatabaseInitializer<TContext>(IDatabaseInitializer<TContext> initializer) where TContext : DbContext
		{
			this._internalConfiguration.CheckNotLocked("SetDatabaseInitializer");
			this._internalConfiguration.RegisterSingleton<IDatabaseInitializer<TContext>>(initializer ?? new NullDatabaseInitializer<TContext>());
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00024CB0 File Offset: 0x00022EB0
		protected internal void SetMigrationSqlGenerator(string providerInvariantName, Func<MigrationSqlGenerator> sqlGenerator)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			Check.NotNull<Func<MigrationSqlGenerator>>(sqlGenerator, "sqlGenerator");
			this._internalConfiguration.CheckNotLocked("SetMigrationSqlGenerator");
			this._internalConfiguration.RegisterSingleton<Func<MigrationSqlGenerator>>(sqlGenerator, providerInvariantName);
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00024CE7 File Offset: 0x00022EE7
		protected internal void SetManifestTokenResolver(IManifestTokenResolver resolver)
		{
			Check.NotNull<IManifestTokenResolver>(resolver, "resolver");
			this._internalConfiguration.CheckNotLocked("SetManifestTokenResolver");
			this._internalConfiguration.RegisterSingleton<IManifestTokenResolver>(resolver);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00024D11 File Offset: 0x00022F11
		protected internal void SetMetadataAnnotationSerializer(string annotationName, Func<IMetadataAnnotationSerializer> serializerFactory)
		{
			Check.NotEmpty(annotationName, "annotationName");
			Check.NotNull<Func<IMetadataAnnotationSerializer>>(serializerFactory, "serializerFactory");
			this._internalConfiguration.CheckNotLocked("SetMetadataAnnotationSerializer");
			this._internalConfiguration.RegisterSingleton<Func<IMetadataAnnotationSerializer>>(serializerFactory, annotationName);
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00024D48 File Offset: 0x00022F48
		protected internal void SetProviderFactoryResolver(IDbProviderFactoryResolver providerFactoryResolver)
		{
			Check.NotNull<IDbProviderFactoryResolver>(providerFactoryResolver, "providerFactoryResolver");
			this._internalConfiguration.CheckNotLocked("SetProviderFactoryResolver");
			this._internalConfiguration.RegisterSingleton<IDbProviderFactoryResolver>(providerFactoryResolver);
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00024D72 File Offset: 0x00022F72
		protected internal void SetModelCacheKey(Func<DbContext, IDbModelCacheKey> keyFactory)
		{
			Check.NotNull<Func<DbContext, IDbModelCacheKey>>(keyFactory, "keyFactory");
			this._internalConfiguration.CheckNotLocked("SetModelCacheKey");
			this._internalConfiguration.RegisterSingleton<Func<DbContext, IDbModelCacheKey>>(keyFactory);
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00024D9C File Offset: 0x00022F9C
		protected internal void SetDefaultHistoryContext(Func<DbConnection, string, HistoryContext> factory)
		{
			Check.NotNull<Func<DbConnection, string, HistoryContext>>(factory, "factory");
			this._internalConfiguration.CheckNotLocked("SetDefaultHistoryContext");
			this._internalConfiguration.RegisterSingleton<Func<DbConnection, string, HistoryContext>>(factory);
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00024DC6 File Offset: 0x00022FC6
		protected internal void SetHistoryContext(string providerInvariantName, Func<DbConnection, string, HistoryContext> factory)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			Check.NotNull<Func<DbConnection, string, HistoryContext>>(factory, "factory");
			this._internalConfiguration.CheckNotLocked("SetHistoryContext");
			this._internalConfiguration.RegisterSingleton<Func<DbConnection, string, HistoryContext>>(factory, providerInvariantName);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00024DFD File Offset: 0x00022FFD
		protected internal void SetDefaultSpatialServices(DbSpatialServices spatialProvider)
		{
			Check.NotNull<DbSpatialServices>(spatialProvider, "spatialProvider");
			this._internalConfiguration.CheckNotLocked("SetDefaultSpatialServices");
			this._internalConfiguration.RegisterSingleton<DbSpatialServices>(spatialProvider);
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00024E27 File Offset: 0x00023027
		protected internal void SetSpatialServices(DbProviderInfo key, DbSpatialServices spatialProvider)
		{
			Check.NotNull<DbProviderInfo>(key, "key");
			Check.NotNull<DbSpatialServices>(spatialProvider, "spatialProvider");
			this._internalConfiguration.CheckNotLocked("SetSpatialServices");
			this._internalConfiguration.RegisterSingleton<DbSpatialServices>(spatialProvider, key);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00024E5E File Offset: 0x0002305E
		protected internal void SetSpatialServices(string providerInvariantName, DbSpatialServices spatialProvider)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			Check.NotNull<DbSpatialServices>(spatialProvider, "spatialProvider");
			this._internalConfiguration.CheckNotLocked("SetSpatialServices");
			this.RegisterSpatialServices(providerInvariantName, spatialProvider);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00024EC4 File Offset: 0x000230C4
		private void RegisterSpatialServices(string providerInvariantName, DbSpatialServices spatialProvider)
		{
			this._internalConfiguration.RegisterSingleton<DbSpatialServices>(spatialProvider, delegate(object k)
			{
				DbProviderInfo dbProviderInfo = k as DbProviderInfo;
				return dbProviderInfo != null && dbProviderInfo.ProviderInvariantName == providerInvariantName;
			});
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00024EF6 File Offset: 0x000230F6
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		protected internal void SetDatabaseLogFormatter(Func<DbContext, Action<string>, DatabaseLogFormatter> logFormatterFactory)
		{
			Check.NotNull<Func<DbContext, Action<string>, DatabaseLogFormatter>>(logFormatterFactory, "logFormatterFactory");
			this._internalConfiguration.CheckNotLocked("SetDatabaseLogFormatter");
			this._internalConfiguration.RegisterSingleton<Func<DbContext, Action<string>, DatabaseLogFormatter>>(logFormatterFactory);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00024F20 File Offset: 0x00023120
		protected internal void AddInterceptor(IDbInterceptor interceptor)
		{
			Check.NotNull<IDbInterceptor>(interceptor, "interceptor");
			this._internalConfiguration.CheckNotLocked("AddInterceptor");
			this._internalConfiguration.RegisterSingleton<IDbInterceptor>(interceptor);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00024F4C File Offset: 0x0002314C
		protected internal void SetContextFactory(Type contextType, Func<DbContext> factory)
		{
			Check.NotNull<Type>(contextType, "contextType");
			Check.NotNull<Func<DbContext>>(factory, "factory");
			if (!typeof(DbContext).IsAssignableFrom(contextType))
			{
				throw new ArgumentException(Strings.ContextFactoryContextType(contextType.FullName));
			}
			this._internalConfiguration.CheckNotLocked("SetContextFactory");
			this._internalConfiguration.RegisterSingleton<Func<DbContext>>(factory, contextType);
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00024FB1 File Offset: 0x000231B1
		protected internal void SetContextFactory<TContext>(Func<TContext> factory) where TContext : DbContext
		{
			Check.NotNull<Func<TContext>>(factory, "factory");
			this.SetContextFactory(typeof(TContext), factory);
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00024FD0 File Offset: 0x000231D0
		protected internal void SetTableExistenceChecker(string providerInvariantName, TableExistenceChecker tableExistenceChecker)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			Check.NotNull<TableExistenceChecker>(tableExistenceChecker, "tableExistenceChecker");
			this._internalConfiguration.CheckNotLocked("SetTableExistenceChecker");
			this._internalConfiguration.RegisterSingleton<TableExistenceChecker>(tableExistenceChecker, providerInvariantName);
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x00025007 File Offset: 0x00023207
		internal virtual InternalConfiguration InternalConfiguration
		{
			get
			{
				return this._internalConfiguration;
			}
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0002500F File Offset: 0x0002320F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00025017 File Offset: 0x00023217
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00025020 File Offset: 0x00023220
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00025028 File Offset: 0x00023228
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00025030 File Offset: 0x00023230
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected new object MemberwiseClone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x040001B8 RID: 440
		private readonly InternalConfiguration _internalConfiguration;
	}
}
