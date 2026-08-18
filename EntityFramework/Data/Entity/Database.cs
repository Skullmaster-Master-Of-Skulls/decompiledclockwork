using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity
{
	// Token: 0x02000733 RID: 1843
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "The DbContextTransaction and EntityTransaction should never be disposed by this class")]
	public class Database
	{
		// Token: 0x06005344 RID: 21316 RVA: 0x0016EFD1 File Offset: 0x0016D1D1
		internal Database(InternalContext internalContext)
		{
			this._internalContext = internalContext;
		}

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x06005345 RID: 21317 RVA: 0x0016EFE0 File Offset: 0x0016D1E0
		public DbContextTransaction CurrentTransaction
		{
			get
			{
				EntityTransaction currentTransaction = ((EntityConnection)this._internalContext.ObjectContext.Connection).CurrentTransaction;
				if (this._dbContextTransaction == null || this._entityTransaction != currentTransaction)
				{
					this._entityTransaction = currentTransaction;
					if (currentTransaction != null)
					{
						this._dbContextTransaction = new DbContextTransaction(currentTransaction);
					}
					else
					{
						this._dbContextTransaction = null;
					}
				}
				return this._dbContextTransaction;
			}
		}

		// Token: 0x06005346 RID: 21318 RVA: 0x0016F03E File Offset: 0x0016D23E
		public void UseTransaction(DbTransaction transaction)
		{
			this._entityTransaction = ((EntityConnection)this._internalContext.GetObjectContextWithoutDatabaseInitialization().Connection).UseStoreTransaction(transaction);
			this._dbContextTransaction = null;
		}

		// Token: 0x06005347 RID: 21319 RVA: 0x0016F068 File Offset: 0x0016D268
		public DbContextTransaction BeginTransaction()
		{
			EntityConnection entityConnection = (EntityConnection)this._internalContext.ObjectContext.Connection;
			this._dbContextTransaction = new DbContextTransaction(entityConnection);
			this._entityTransaction = entityConnection.CurrentTransaction;
			return this._dbContextTransaction;
		}

		// Token: 0x06005348 RID: 21320 RVA: 0x0016F0AC File Offset: 0x0016D2AC
		public DbContextTransaction BeginTransaction(IsolationLevel isolationLevel)
		{
			EntityConnection entityConnection = (EntityConnection)this._internalContext.ObjectContext.Connection;
			this._dbContextTransaction = new DbContextTransaction(entityConnection, isolationLevel);
			this._entityTransaction = entityConnection.CurrentTransaction;
			return this._dbContextTransaction;
		}

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x06005349 RID: 21321 RVA: 0x0016F0EE File Offset: 0x0016D2EE
		public DbConnection Connection
		{
			get
			{
				return this._internalContext.Connection;
			}
		}

		// Token: 0x0600534A RID: 21322 RVA: 0x0016F0FB File Offset: 0x0016D2FB
		public static void SetInitializer<TContext>(IDatabaseInitializer<TContext> strategy) where TContext : DbContext
		{
			DbConfigurationManager.Instance.EnsureLoadedForContext(typeof(TContext));
			InternalConfiguration.Instance.RootResolver.DatabaseInitializerResolver.SetInitializer(typeof(TContext), strategy ?? new NullDatabaseInitializer<TContext>());
		}

		// Token: 0x0600534B RID: 21323 RVA: 0x0016F139 File Offset: 0x0016D339
		public void Initialize(bool force)
		{
			if (force)
			{
				this._internalContext.MarkDatabaseInitialized();
				this._internalContext.PerformDatabaseInitialization();
				return;
			}
			this._internalContext.Initialize();
		}

		// Token: 0x0600534C RID: 21324 RVA: 0x0016F160 File Offset: 0x0016D360
		public bool CompatibleWithModel(bool throwIfNoMetadata)
		{
			return this.CompatibleWithModel(throwIfNoMetadata, DatabaseExistenceState.Unknown);
		}

		// Token: 0x0600534D RID: 21325 RVA: 0x0016F16A File Offset: 0x0016D36A
		internal bool CompatibleWithModel(bool throwIfNoMetadata, DatabaseExistenceState existenceState)
		{
			return this._internalContext.CompatibleWithModel(throwIfNoMetadata, existenceState);
		}

		// Token: 0x0600534E RID: 21326 RVA: 0x0016F179 File Offset: 0x0016D379
		public void Create()
		{
			this.Create(DatabaseExistenceState.Unknown);
		}

		// Token: 0x0600534F RID: 21327 RVA: 0x0016F184 File Offset: 0x0016D384
		internal void Create(DatabaseExistenceState existenceState)
		{
			if (existenceState == DatabaseExistenceState.Unknown)
			{
				if (this._internalContext.DatabaseOperations.Exists(this._internalContext.Connection, this._internalContext.CommandTimeout, new Lazy<StoreItemCollection>(new Func<StoreItemCollection>(this.CreateStoreItemCollection))))
				{
					DbInterceptionContext dbInterceptionContext = new DbInterceptionContext();
					dbInterceptionContext = dbInterceptionContext.WithDbContext(this._internalContext.Owner);
					throw Error.Database_DatabaseAlreadyExists(DbInterception.Dispatch.Connection.GetDatabase(this._internalContext.Connection, dbInterceptionContext));
				}
				existenceState = DatabaseExistenceState.DoesNotExist;
			}
			using (ClonedObjectContext clonedObjectContext = this._internalContext.CreateObjectContextForDdlOps())
			{
				this._internalContext.CreateDatabase(clonedObjectContext.ObjectContext, existenceState);
			}
		}

		// Token: 0x06005350 RID: 21328 RVA: 0x0016F248 File Offset: 0x0016D448
		public bool CreateIfNotExists()
		{
			if (this._internalContext.DatabaseOperations.Exists(this._internalContext.Connection, this._internalContext.CommandTimeout, new Lazy<StoreItemCollection>(new Func<StoreItemCollection>(this.CreateStoreItemCollection))))
			{
				return false;
			}
			using (ClonedObjectContext clonedObjectContext = this._internalContext.CreateObjectContextForDdlOps())
			{
				this._internalContext.CreateDatabase(clonedObjectContext.ObjectContext, DatabaseExistenceState.DoesNotExist);
			}
			return true;
		}

		// Token: 0x06005351 RID: 21329 RVA: 0x0016F2D0 File Offset: 0x0016D4D0
		public bool Exists()
		{
			return this._internalContext.DatabaseOperations.Exists(this._internalContext.Connection, this._internalContext.CommandTimeout, new Lazy<StoreItemCollection>(new Func<StoreItemCollection>(this.CreateStoreItemCollection)));
		}

		// Token: 0x06005352 RID: 21330 RVA: 0x0016F30C File Offset: 0x0016D50C
		public bool Delete()
		{
			if (!this._internalContext.DatabaseOperations.Exists(this._internalContext.Connection, this._internalContext.CommandTimeout, new Lazy<StoreItemCollection>(new Func<StoreItemCollection>(this.CreateStoreItemCollection))))
			{
				return false;
			}
			using (ClonedObjectContext clonedObjectContext = this._internalContext.CreateObjectContextForDdlOps())
			{
				this._internalContext.DatabaseOperations.Delete(clonedObjectContext.ObjectContext);
				this._internalContext.MarkDatabaseNotInitialized();
			}
			return true;
		}

		// Token: 0x06005353 RID: 21331 RVA: 0x0016F3AC File Offset: 0x0016D5AC
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public static bool Exists(string nameOrConnectionString)
		{
			Check.NotEmpty(nameOrConnectionString, "nameOrConnectionString");
			bool result;
			using (LazyInternalConnection lazyInternalConnection = new LazyInternalConnection(nameOrConnectionString))
			{
				result = new DatabaseOperations().Exists(lazyInternalConnection.Connection, null, new Lazy<StoreItemCollection>(() => new StoreItemCollection()));
			}
			return result;
		}

		// Token: 0x06005354 RID: 21332 RVA: 0x0016F428 File Offset: 0x0016D628
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public static bool Delete(string nameOrConnectionString)
		{
			Check.NotEmpty(nameOrConnectionString, "nameOrConnectionString");
			if (!Database.Exists(nameOrConnectionString))
			{
				return false;
			}
			using (LazyInternalConnection lazyInternalConnection = new LazyInternalConnection(nameOrConnectionString))
			{
				using (ObjectContext objectContext = Database.CreateEmptyObjectContext(lazyInternalConnection.Connection))
				{
					new DatabaseOperations().Delete(objectContext);
				}
			}
			return true;
		}

		// Token: 0x06005355 RID: 21333 RVA: 0x0016F4A8 File Offset: 0x0016D6A8
		public static bool Exists(DbConnection existingConnection)
		{
			Check.NotNull<DbConnection>(existingConnection, "existingConnection");
			return new DatabaseOperations().Exists(existingConnection, null, new Lazy<StoreItemCollection>(() => new StoreItemCollection()));
		}

		// Token: 0x06005356 RID: 21334 RVA: 0x0016F4F8 File Offset: 0x0016D6F8
		public static bool Delete(DbConnection existingConnection)
		{
			Check.NotNull<DbConnection>(existingConnection, "existingConnection");
			if (!Database.Exists(existingConnection))
			{
				return false;
			}
			using (ObjectContext objectContext = Database.CreateEmptyObjectContext(existingConnection))
			{
				new DatabaseOperations().Delete(objectContext);
			}
			return true;
		}

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x06005357 RID: 21335 RVA: 0x0016F54C File Offset: 0x0016D74C
		// (set) Token: 0x06005358 RID: 21336 RVA: 0x0016F568 File Offset: 0x0016D768
		[Obsolete("The default connection factory should be set in the config file or using the DbConfiguration class. (See http://go.microsoft.com/fwlink/?LinkId=260883)")]
		public static IDbConnectionFactory DefaultConnectionFactory
		{
			get
			{
				return DbConfiguration.DependencyResolver.GetService<IDbConnectionFactory>();
			}
			set
			{
				Check.NotNull<IDbConnectionFactory>(value, "value");
				Database._defaultConnectionFactory = new Lazy<IDbConnectionFactory>(() => value, true);
			}
		}

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x06005359 RID: 21337 RVA: 0x0016F5AC File Offset: 0x0016D7AC
		internal static IDbConnectionFactory SetDefaultConnectionFactory
		{
			get
			{
				return Database._defaultConnectionFactory.Value;
			}
		}

		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x0600535A RID: 21338 RVA: 0x0016F5BA File Offset: 0x0016D7BA
		internal static bool DefaultConnectionFactoryChanged
		{
			get
			{
				return !object.ReferenceEquals(Database._defaultConnectionFactory, Database._defaultDefaultConnectionFactory);
			}
		}

		// Token: 0x0600535B RID: 21339 RVA: 0x0016F5D0 File Offset: 0x0016D7D0
		internal static void ResetDefaultConnectionFactory()
		{
			Database._defaultConnectionFactory = Database._defaultDefaultConnectionFactory;
		}

		// Token: 0x0600535C RID: 21340 RVA: 0x0016F5DE File Offset: 0x0016D7DE
		private static ObjectContext CreateEmptyObjectContext(DbConnection connection)
		{
			return new DbModelBuilder().Build(connection).Compile().CreateObjectContext<ObjectContext>(connection);
		}

		// Token: 0x0600535D RID: 21341 RVA: 0x0016F5F6 File Offset: 0x0016D7F6
		public DbRawSqlQuery<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
		{
			Check.NotEmpty(sql, "sql");
			Check.NotNull<object[]>(parameters, "parameters");
			return new DbRawSqlQuery<TElement>(new InternalSqlNonSetQuery(this._internalContext, typeof(TElement), sql, parameters));
		}

		// Token: 0x0600535E RID: 21342 RVA: 0x0016F62C File Offset: 0x0016D82C
		public DbRawSqlQuery SqlQuery(Type elementType, string sql, params object[] parameters)
		{
			Check.NotNull<Type>(elementType, "elementType");
			Check.NotEmpty(sql, "sql");
			Check.NotNull<object[]>(parameters, "parameters");
			return new DbRawSqlQuery(new InternalSqlNonSetQuery(this._internalContext, elementType, sql, parameters));
		}

		// Token: 0x0600535F RID: 21343 RVA: 0x0016F665 File Offset: 0x0016D865
		public int ExecuteSqlCommand(string sql, params object[] parameters)
		{
			return this.ExecuteSqlCommand(this._internalContext.EnsureTransactionsForFunctionsAndCommands ? TransactionalBehavior.EnsureTransaction : TransactionalBehavior.DoNotEnsureTransaction, sql, parameters);
		}

		// Token: 0x06005360 RID: 21344 RVA: 0x0016F680 File Offset: 0x0016D880
		public int ExecuteSqlCommand(TransactionalBehavior transactionalBehavior, string sql, params object[] parameters)
		{
			Check.NotEmpty(sql, "sql");
			Check.NotNull<object[]>(parameters, "parameters");
			return this._internalContext.ExecuteSqlCommand(transactionalBehavior, sql, parameters);
		}

		// Token: 0x06005361 RID: 21345 RVA: 0x0016F6A8 File Offset: 0x0016D8A8
		public Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters)
		{
			return this.ExecuteSqlCommandAsync(sql, CancellationToken.None, parameters);
		}

		// Token: 0x06005362 RID: 21346 RVA: 0x0016F6B7 File Offset: 0x0016D8B7
		public Task<int> ExecuteSqlCommandAsync(TransactionalBehavior transactionalBehavior, string sql, params object[] parameters)
		{
			return this.ExecuteSqlCommandAsync(transactionalBehavior, sql, CancellationToken.None, parameters);
		}

		// Token: 0x06005363 RID: 21347 RVA: 0x0016F6C7 File Offset: 0x0016D8C7
		public Task<int> ExecuteSqlCommandAsync(string sql, CancellationToken cancellationToken, params object[] parameters)
		{
			return this.ExecuteSqlCommandAsync(this._internalContext.EnsureTransactionsForFunctionsAndCommands ? TransactionalBehavior.EnsureTransaction : TransactionalBehavior.DoNotEnsureTransaction, sql, cancellationToken, parameters);
		}

		// Token: 0x06005364 RID: 21348 RVA: 0x0016F6E3 File Offset: 0x0016D8E3
		public Task<int> ExecuteSqlCommandAsync(TransactionalBehavior transactionalBehavior, string sql, CancellationToken cancellationToken, params object[] parameters)
		{
			Check.NotEmpty(sql, "sql");
			Check.NotNull<object[]>(parameters, "parameters");
			cancellationToken.ThrowIfCancellationRequested();
			return this._internalContext.ExecuteSqlCommandAsync(transactionalBehavior, sql, cancellationToken, parameters);
		}

		// Token: 0x06005365 RID: 21349 RVA: 0x0016F715 File Offset: 0x0016D915
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06005366 RID: 21350 RVA: 0x0016F71D File Offset: 0x0016D91D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06005367 RID: 21351 RVA: 0x0016F726 File Offset: 0x0016D926
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06005368 RID: 21352 RVA: 0x0016F72E File Offset: 0x0016D92E
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x06005369 RID: 21353 RVA: 0x0016F738 File Offset: 0x0016D938
		private StoreItemCollection CreateStoreItemCollection()
		{
			StoreItemCollection result;
			using (ClonedObjectContext clonedObjectContext = this._internalContext.CreateObjectContextForDdlOps())
			{
				EntityConnection entityConnection = clonedObjectContext.ObjectContext.Connection;
				result = (StoreItemCollection)entityConnection.GetMetadataWorkspace().GetItemCollection(DataSpace.SSpace);
			}
			return result;
		}

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x0600536A RID: 21354 RVA: 0x0016F794 File Offset: 0x0016D994
		// (set) Token: 0x0600536B RID: 21355 RVA: 0x0016F7A4 File Offset: 0x0016D9A4
		public int? CommandTimeout
		{
			get
			{
				return this._internalContext.CommandTimeout;
			}
			set
			{
				if (value != null && value < 0)
				{
					throw new ArgumentException(Strings.ObjectContext_InvalidCommandTimeout);
				}
				this._internalContext.CommandTimeout = value;
			}
		}

		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x0600536C RID: 21356 RVA: 0x0016F7E9 File Offset: 0x0016D9E9
		// (set) Token: 0x0600536D RID: 21357 RVA: 0x0016F7F6 File Offset: 0x0016D9F6
		public Action<string> Log
		{
			get
			{
				return this._internalContext.Log;
			}
			set
			{
				this._internalContext.Log = value;
			}
		}

		// Token: 0x0400225A RID: 8794
		private static readonly Lazy<IDbConnectionFactory> _defaultDefaultConnectionFactory = new Lazy<IDbConnectionFactory>(() => AppConfig.DefaultInstance.TryGetDefaultConnectionFactory() ?? new SqlConnectionFactory(), true);

		// Token: 0x0400225B RID: 8795
		private static volatile Lazy<IDbConnectionFactory> _defaultConnectionFactory = Database._defaultDefaultConnectionFactory;

		// Token: 0x0400225C RID: 8796
		private readonly InternalContext _internalContext;

		// Token: 0x0400225D RID: 8797
		private EntityTransaction _entityTransaction;

		// Token: 0x0400225E RID: 8798
		private DbContextTransaction _dbContextTransaction;
	}
}
