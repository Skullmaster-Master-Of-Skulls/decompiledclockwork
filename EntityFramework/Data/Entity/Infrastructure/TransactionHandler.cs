using System;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000195 RID: 405
	public abstract class TransactionHandler : IDbTransactionInterceptor, IDbConnectionInterceptor, IDbInterceptor, IDisposable
	{
		// Token: 0x06000D96 RID: 3478 RVA: 0x0003D27A File Offset: 0x0003B47A
		protected TransactionHandler()
		{
			DbInterception.Add(this);
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x0003D288 File Offset: 0x0003B488
		public virtual void Initialize(ObjectContext context)
		{
			Check.NotNull<ObjectContext>(context, "context");
			if (this.ObjectContext != null || this.DbContext != null || this.Connection != null)
			{
				throw new InvalidOperationException(Strings.TransactionHandler_AlreadyInitialized);
			}
			this.ObjectContext = context;
			this.DbContext = context.InterceptionContext.DbContexts.FirstOrDefault<DbContext>();
			this.Connection = ((EntityConnection)this.ObjectContext.Connection).StoreConnection;
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x0003D2FC File Offset: 0x0003B4FC
		public virtual void Initialize(DbContext context, DbConnection connection)
		{
			Check.NotNull<DbContext>(context, "context");
			Check.NotNull<DbConnection>(connection, "connection");
			if (this.ObjectContext != null || this.DbContext != null || this.Connection != null)
			{
				throw new InvalidOperationException(Strings.TransactionHandler_AlreadyInitialized);
			}
			this.DbContext = context;
			this.Connection = connection;
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000D99 RID: 3481 RVA: 0x0003D352 File Offset: 0x0003B552
		// (set) Token: 0x06000D9A RID: 3482 RVA: 0x0003D37B File Offset: 0x0003B57B
		public ObjectContext ObjectContext
		{
			get
			{
				if (this._objectContext == null || !this._objectContext.IsAlive)
				{
					return null;
				}
				return (ObjectContext)this._objectContext.Target;
			}
			private set
			{
				this._objectContext = new WeakReference(value);
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000D9B RID: 3483 RVA: 0x0003D389 File Offset: 0x0003B589
		// (set) Token: 0x06000D9C RID: 3484 RVA: 0x0003D3B2 File Offset: 0x0003B5B2
		public DbContext DbContext
		{
			get
			{
				if (this._dbContext == null || !this._dbContext.IsAlive)
				{
					return null;
				}
				return (DbContext)this._dbContext.Target;
			}
			private set
			{
				this._dbContext = new WeakReference(value);
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x0003D3C0 File Offset: 0x0003B5C0
		// (set) Token: 0x06000D9E RID: 3486 RVA: 0x0003D3E9 File Offset: 0x0003B5E9
		public DbConnection Connection
		{
			get
			{
				if (this._connection == null || !this._connection.IsAlive)
				{
					return null;
				}
				return (DbConnection)this._connection.Target;
			}
			private set
			{
				this._connection = new WeakReference(value);
			}
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x0003D3F7 File Offset: 0x0003B5F7
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x0003D406 File Offset: 0x0003B606
		// (set) Token: 0x06000DA1 RID: 3489 RVA: 0x0003D40E File Offset: 0x0003B60E
		protected bool IsDisposed { get; set; }

		// Token: 0x06000DA2 RID: 3490 RVA: 0x0003D417 File Offset: 0x0003B617
		protected virtual void Dispose(bool disposing)
		{
			if (!this.IsDisposed)
			{
				DbInterception.Remove(this);
				this.IsDisposed = true;
			}
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x0003D430 File Offset: 0x0003B630
		protected internal virtual bool MatchesParentContext(DbConnection connection, DbInterceptionContext interceptionContext)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<DbInterceptionContext>(interceptionContext, "interceptionContext");
			return (this.DbContext != null && interceptionContext.DbContexts.Contains(this.DbContext, new Func<DbContext, DbContext, bool>(object.ReferenceEquals))) || (this.ObjectContext != null && interceptionContext.ObjectContexts.Contains(this.ObjectContext, new Func<ObjectContext, ObjectContext, bool>(object.ReferenceEquals))) || (this.Connection != null && !interceptionContext.ObjectContexts.Any<ObjectContext>() && !interceptionContext.DbContexts.Any<DbContext>() && object.ReferenceEquals(connection, this.Connection));
		}

		// Token: 0x06000DA4 RID: 3492
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public abstract string BuildDatabaseInitializationScript();

		// Token: 0x06000DA5 RID: 3493 RVA: 0x0003D4D7 File Offset: 0x0003B6D7
		public virtual void BeginningTransaction(DbConnection connection, BeginTransactionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x0003D4D9 File Offset: 0x0003B6D9
		public virtual void BeganTransaction(DbConnection connection, BeginTransactionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x0003D4DB File Offset: 0x0003B6DB
		public virtual void Closing(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x0003D4DD File Offset: 0x0003B6DD
		public virtual void Closed(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0003D4DF File Offset: 0x0003B6DF
		public virtual void ConnectionStringGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x0003D4E1 File Offset: 0x0003B6E1
		public virtual void ConnectionStringGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x0003D4E3 File Offset: 0x0003B6E3
		public virtual void ConnectionStringSetting(DbConnection connection, DbConnectionPropertyInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x0003D4E5 File Offset: 0x0003B6E5
		public virtual void ConnectionStringSet(DbConnection connection, DbConnectionPropertyInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x0003D4E7 File Offset: 0x0003B6E7
		public virtual void ConnectionTimeoutGetting(DbConnection connection, DbConnectionInterceptionContext<int> interceptionContext)
		{
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0003D4E9 File Offset: 0x0003B6E9
		public virtual void ConnectionTimeoutGot(DbConnection connection, DbConnectionInterceptionContext<int> interceptionContext)
		{
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0003D4EB File Offset: 0x0003B6EB
		public virtual void DatabaseGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0003D4ED File Offset: 0x0003B6ED
		public virtual void DatabaseGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0003D4EF File Offset: 0x0003B6EF
		public virtual void DataSourceGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0003D4F1 File Offset: 0x0003B6F1
		public virtual void DataSourceGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x0003D4F3 File Offset: 0x0003B6F3
		public virtual void Disposing(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x0003D4F5 File Offset: 0x0003B6F5
		public virtual void Disposed(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x0003D4F7 File Offset: 0x0003B6F7
		public virtual void EnlistingTransaction(DbConnection connection, EnlistTransactionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0003D4F9 File Offset: 0x0003B6F9
		public virtual void EnlistedTransaction(DbConnection connection, EnlistTransactionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x0003D4FB File Offset: 0x0003B6FB
		public virtual void Opening(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0003D4FD File Offset: 0x0003B6FD
		public virtual void Opened(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0003D4FF File Offset: 0x0003B6FF
		public virtual void ServerVersionGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0003D501 File Offset: 0x0003B701
		public virtual void ServerVersionGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0003D503 File Offset: 0x0003B703
		public virtual void StateGetting(DbConnection connection, DbConnectionInterceptionContext<ConnectionState> interceptionContext)
		{
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x0003D505 File Offset: 0x0003B705
		public virtual void StateGot(DbConnection connection, DbConnectionInterceptionContext<ConnectionState> interceptionContext)
		{
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0003D507 File Offset: 0x0003B707
		public virtual void ConnectionGetting(DbTransaction transaction, DbTransactionInterceptionContext<DbConnection> interceptionContext)
		{
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x0003D509 File Offset: 0x0003B709
		public virtual void ConnectionGot(DbTransaction transaction, DbTransactionInterceptionContext<DbConnection> interceptionContext)
		{
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0003D50B File Offset: 0x0003B70B
		public virtual void IsolationLevelGetting(DbTransaction transaction, DbTransactionInterceptionContext<IsolationLevel> interceptionContext)
		{
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0003D50D File Offset: 0x0003B70D
		public virtual void IsolationLevelGot(DbTransaction transaction, DbTransactionInterceptionContext<IsolationLevel> interceptionContext)
		{
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0003D50F File Offset: 0x0003B70F
		public virtual void Committing(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x0003D511 File Offset: 0x0003B711
		public virtual void Committed(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x0003D513 File Offset: 0x0003B713
		public virtual void Disposing(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x0003D515 File Offset: 0x0003B715
		public virtual void Disposed(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0003D517 File Offset: 0x0003B717
		public virtual void RollingBack(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x0003D519 File Offset: 0x0003B719
		public virtual void RolledBack(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x040003B2 RID: 946
		private WeakReference _objectContext;

		// Token: 0x040003B3 RID: 947
		private WeakReference _dbContext;

		// Token: 0x040003B4 RID: 948
		private WeakReference _connection;
	}
}
