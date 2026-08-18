using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x0200017D RID: 381
	public class DbConnectionDispatcher
	{
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x0003B175 File Offset: 0x00039375
		internal InternalDispatcher<IDbConnectionInterceptor> InternalDispatcher
		{
			get
			{
				return this._internalDispatcher;
			}
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x0003B17D File Offset: 0x0003937D
		internal DbConnectionDispatcher()
		{
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x0003B1B4 File Offset: 0x000393B4
		public virtual DbTransaction BeginTransaction(DbConnection connection, BeginTransactionInterceptionContext interceptionContext)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<BeginTransactionInterceptionContext>(interceptionContext, "interceptionContext");
			return this.InternalDispatcher.Dispatch<DbConnection, BeginTransactionInterceptionContext, DbTransaction>(connection, (DbConnection t, BeginTransactionInterceptionContext c) => t.BeginTransaction(c.IsolationLevel), new BeginTransactionInterceptionContext(interceptionContext), delegate(IDbConnectionInterceptor i, DbConnection t, BeginTransactionInterceptionContext c)
			{
				i.BeginningTransaction(t, c);
			}, delegate(IDbConnectionInterceptor i, DbConnection t, BeginTransactionInterceptionContext c)
			{
				i.BeganTransaction(t, c);
			});
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x0003B260 File Offset: 0x00039460
		public virtual void Close(DbConnection connection, DbInterceptionContext interceptionContext)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<DbInterceptionContext>(interceptionContext, "interceptionContext");
			this.InternalDispatcher.Dispatch<DbConnection, DbConnectionInterceptionContext>(connection, delegate(DbConnection t, DbConnectionInterceptionContext c)
			{
				t.Close();
			}, new DbConnectionInterceptionContext(interceptionContext), delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext c)
			{
				i.Closing(t, c);
			}, delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext c)
			{
				i.Closed(t, c);
			});
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0003B330 File Offset: 0x00039530
		public virtual void Dispose(DbConnection connection, DbInterceptionContext interceptionContext)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<DbInterceptionContext>(interceptionContext, "interceptionContext");
			this.InternalDispatcher.Dispatch<DbConnection, DbConnectionInterceptionContext>(connection, delegate(DbConnection t, DbConnectionInterceptionContext c)
			{
				try
				{
				}
				finally
				{
					if (t != null)
					{
						((IDisposable)t).Dispose();
					}
				}
			}, new DbConnectionInterceptionContext(interceptionContext), delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext c)
			{
				i.Disposing(t, c);
			}, delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext c)
			{
				i.Disposed(t, c);
			});
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x0003B3DC File Offset: 0x000395DC
		public virtual string GetConnectionString(DbConnection connection, DbInterceptionContext interceptionContext)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<DbInterceptionContext>(interceptionContext, "interceptionContext");
			return this.InternalDispatcher.Dispatch<DbConnection, DbConnectionInterceptionContext<string>, string>(connection, (DbConnection t, DbConnectionInterceptionContext<string> c) => t.ConnectionString, new DbConnectionInterceptionContext<string>(interceptionContext), delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext<string> c)
			{
				i.ConnectionStringGetting(t, c);
			}, delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext<string> c)
			{
				i.ConnectionStringGot(t, c);
			});
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x0003B48C File Offset: 0x0003968C
		public virtual void SetConnectionString(DbConnection connection, DbConnectionPropertyInterceptionContext<string> interceptionContext)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<DbConnectionPropertyInterceptionContext<string>>(interceptionContext, "interceptionContext");
			this.InternalDispatcher.Dispatch<DbConnection, DbConnectionPropertyInterceptionContext<string>>(connection, delegate(DbConnection t, DbConnectionPropertyInterceptionContext<string> c)
			{
				t.ConnectionString = c.Value;
			}, new DbConnectionPropertyInterceptionContext<string>(interceptionContext), delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionPropertyInterceptionContext<string> c)
			{
				i.ConnectionStringSetting(t, c);
			}, delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionPropertyInterceptionContext<string> c)
			{
				i.ConnectionStringSet(t, c);
			});
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x0003B538 File Offset: 0x00039738
		public virtual int GetConnectionTimeout(DbConnection connection, DbInterceptionContext interceptionContext)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<DbInterceptionContext>(interceptionContext, "interceptionContext");
			return this.InternalDispatcher.Dispatch<DbConnection, DbConnectionInterceptionContext<int>, int>(connection, (DbConnection t, DbConnectionInterceptionContext<int> c) => t.ConnectionTimeout, new DbConnectionInterceptionContext<int>(interceptionContext), delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext<int> c)
			{
				i.ConnectionTimeoutGetting(t, c);
			}, delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext<int> c)
			{
				i.ConnectionTimeoutGot(t, c);
			});
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x0003B5E4 File Offset: 0x000397E4
		public virtual string GetDatabase(DbConnection connection, DbInterceptionContext interceptionContext)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<DbInterceptionContext>(interceptionContext, "interceptionContext");
			return this.InternalDispatcher.Dispatch<DbConnection, DbConnectionInterceptionContext<string>, string>(connection, (DbConnection t, DbConnectionInterceptionContext<string> c) => t.Database, new DbConnectionInterceptionContext<string>(interceptionContext), delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext<string> c)
			{
				i.DatabaseGetting(t, c);
			}, delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext<string> c)
			{
				i.DatabaseGot(t, c);
			});
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0003B690 File Offset: 0x00039890
		public virtual string GetDataSource(DbConnection connection, DbInterceptionContext interceptionContext)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<DbInterceptionContext>(interceptionContext, "interceptionContext");
			return this.InternalDispatcher.Dispatch<DbConnection, DbConnectionInterceptionContext<string>, string>(connection, (DbConnection t, DbConnectionInterceptionContext<string> c) => t.DataSource, new DbConnectionInterceptionContext<string>(interceptionContext), delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext<string> c)
			{
				i.DataSourceGetting(t, c);
			}, delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext<string> c)
			{
				i.DataSourceGot(t, c);
			});
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x0003B740 File Offset: 0x00039940
		public virtual void EnlistTransaction(DbConnection connection, EnlistTransactionInterceptionContext interceptionContext)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<EnlistTransactionInterceptionContext>(interceptionContext, "interceptionContext");
			this.InternalDispatcher.Dispatch<DbConnection, EnlistTransactionInterceptionContext>(connection, delegate(DbConnection t, EnlistTransactionInterceptionContext c)
			{
				t.EnlistTransaction(c.Transaction);
			}, new EnlistTransactionInterceptionContext(interceptionContext), delegate(IDbConnectionInterceptor i, DbConnection t, EnlistTransactionInterceptionContext c)
			{
				i.EnlistingTransaction(t, c);
			}, delegate(IDbConnectionInterceptor i, DbConnection t, EnlistTransactionInterceptionContext c)
			{
				i.EnlistedTransaction(t, c);
			});
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x0003B7EC File Offset: 0x000399EC
		public virtual void Open(DbConnection connection, DbInterceptionContext interceptionContext)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<DbInterceptionContext>(interceptionContext, "interceptionContext");
			this.InternalDispatcher.Dispatch<DbConnection, DbConnectionInterceptionContext>(connection, delegate(DbConnection t, DbConnectionInterceptionContext c)
			{
				t.Open();
			}, new DbConnectionInterceptionContext(interceptionContext), delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext c)
			{
				i.Opening(t, c);
			}, delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext c)
			{
				i.Opened(t, c);
			});
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x0003B898 File Offset: 0x00039A98
		public virtual Task OpenAsync(DbConnection connection, DbInterceptionContext interceptionContext, CancellationToken cancellationToken)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<DbInterceptionContext>(interceptionContext, "interceptionContext");
			return this.InternalDispatcher.DispatchAsync<DbConnection, DbConnectionInterceptionContext>(connection, (DbConnection t, DbConnectionInterceptionContext c, CancellationToken ct) => t.OpenAsync(ct), new DbConnectionInterceptionContext(interceptionContext).AsAsync(), delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext c)
			{
				i.Opening(t, c);
			}, delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext c)
			{
				i.Opened(t, c);
			}, cancellationToken);
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x0003B948 File Offset: 0x00039B48
		public virtual string GetServerVersion(DbConnection connection, DbInterceptionContext interceptionContext)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<DbInterceptionContext>(interceptionContext, "interceptionContext");
			return this.InternalDispatcher.Dispatch<DbConnection, DbConnectionInterceptionContext<string>, string>(connection, (DbConnection t, DbConnectionInterceptionContext<string> c) => t.ServerVersion, new DbConnectionInterceptionContext<string>(interceptionContext), delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext<string> c)
			{
				i.ServerVersionGetting(t, c);
			}, delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext<string> c)
			{
				i.ServerVersionGot(t, c);
			});
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x0003B9F4 File Offset: 0x00039BF4
		public virtual ConnectionState GetState(DbConnection connection, DbInterceptionContext interceptionContext)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<DbInterceptionContext>(interceptionContext, "interceptionContext");
			return this.InternalDispatcher.Dispatch<DbConnection, DbConnectionInterceptionContext<ConnectionState>, ConnectionState>(connection, (DbConnection t, DbConnectionInterceptionContext<ConnectionState> c) => t.State, new DbConnectionInterceptionContext<ConnectionState>(interceptionContext), delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext<ConnectionState> c)
			{
				i.StateGetting(t, c);
			}, delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext<ConnectionState> c)
			{
				i.StateGot(t, c);
			});
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0003BA82 File Offset: 0x00039C82
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x0003BA8A File Offset: 0x00039C8A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x0003BA93 File Offset: 0x00039C93
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0003BA9B File Offset: 0x00039C9B
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x0400035E RID: 862
		private readonly InternalDispatcher<IDbConnectionInterceptor> _internalDispatcher = new InternalDispatcher<IDbConnectionInterceptor>();
	}
}
