using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000180 RID: 384
	public class DbTransactionDispatcher
	{
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x0003BCDD File Offset: 0x00039EDD
		internal InternalDispatcher<IDbTransactionInterceptor> InternalDispatcher
		{
			get
			{
				return this._internalDispatcher;
			}
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x0003BCE5 File Offset: 0x00039EE5
		internal DbTransactionDispatcher()
		{
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x0003BD14 File Offset: 0x00039F14
		public virtual DbConnection GetConnection(DbTransaction transaction, DbInterceptionContext interceptionContext)
		{
			Check.NotNull<DbTransaction>(transaction, "transaction");
			Check.NotNull<DbInterceptionContext>(interceptionContext, "interceptionContext");
			return this.InternalDispatcher.Dispatch<DbTransaction, DbTransactionInterceptionContext<DbConnection>, DbConnection>(transaction, (DbTransaction t, DbTransactionInterceptionContext<DbConnection> c) => t.Connection, new DbTransactionInterceptionContext<DbConnection>(interceptionContext), delegate(IDbTransactionInterceptor i, DbTransaction t, DbTransactionInterceptionContext<DbConnection> c)
			{
				i.ConnectionGetting(t, c);
			}, delegate(IDbTransactionInterceptor i, DbTransaction t, DbTransactionInterceptionContext<DbConnection> c)
			{
				i.ConnectionGot(t, c);
			});
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x0003BDC0 File Offset: 0x00039FC0
		public virtual IsolationLevel GetIsolationLevel(DbTransaction transaction, DbInterceptionContext interceptionContext)
		{
			Check.NotNull<DbTransaction>(transaction, "transaction");
			Check.NotNull<DbInterceptionContext>(interceptionContext, "interceptionContext");
			return this.InternalDispatcher.Dispatch<DbTransaction, DbTransactionInterceptionContext<IsolationLevel>, IsolationLevel>(transaction, (DbTransaction t, DbTransactionInterceptionContext<IsolationLevel> c) => t.IsolationLevel, new DbTransactionInterceptionContext<IsolationLevel>(interceptionContext), delegate(IDbTransactionInterceptor i, DbTransaction t, DbTransactionInterceptionContext<IsolationLevel> c)
			{
				i.IsolationLevelGetting(t, c);
			}, delegate(IDbTransactionInterceptor i, DbTransaction t, DbTransactionInterceptionContext<IsolationLevel> c)
			{
				i.IsolationLevelGot(t, c);
			});
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x0003BE6C File Offset: 0x0003A06C
		public virtual void Commit(DbTransaction transaction, DbInterceptionContext interceptionContext)
		{
			Check.NotNull<DbTransaction>(transaction, "transaction");
			Check.NotNull<DbInterceptionContext>(interceptionContext, "interceptionContext");
			this.InternalDispatcher.Dispatch<DbTransaction, DbTransactionInterceptionContext>(transaction, delegate(DbTransaction t, DbTransactionInterceptionContext c)
			{
				t.Commit();
			}, new DbTransactionInterceptionContext(interceptionContext).WithConnection(transaction.Connection), delegate(IDbTransactionInterceptor i, DbTransaction t, DbTransactionInterceptionContext c)
			{
				i.Committing(t, c);
			}, delegate(IDbTransactionInterceptor i, DbTransaction t, DbTransactionInterceptionContext c)
			{
				i.Committed(t, c);
			});
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x0003BF24 File Offset: 0x0003A124
		public virtual void Dispose(DbTransaction transaction, DbInterceptionContext interceptionContext)
		{
			Check.NotNull<DbTransaction>(transaction, "transaction");
			Check.NotNull<DbInterceptionContext>(interceptionContext, "interceptionContext");
			DbTransactionInterceptionContext dbTransactionInterceptionContext = new DbTransactionInterceptionContext(interceptionContext);
			if (transaction.Connection != null)
			{
				dbTransactionInterceptionContext = dbTransactionInterceptionContext.WithConnection(transaction.Connection);
			}
			this.InternalDispatcher.Dispatch<DbTransaction, DbTransactionInterceptionContext>(transaction, delegate(DbTransaction t, DbTransactionInterceptionContext c)
			{
				t.Dispose();
			}, dbTransactionInterceptionContext, delegate(IDbTransactionInterceptor i, DbTransaction t, DbTransactionInterceptionContext c)
			{
				i.Disposing(t, c);
			}, delegate(IDbTransactionInterceptor i, DbTransaction t, DbTransactionInterceptionContext c)
			{
				i.Disposed(t, c);
			});
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x0003BFE8 File Offset: 0x0003A1E8
		public virtual void Rollback(DbTransaction transaction, DbInterceptionContext interceptionContext)
		{
			Check.NotNull<DbTransaction>(transaction, "transaction");
			Check.NotNull<DbInterceptionContext>(interceptionContext, "interceptionContext");
			this.InternalDispatcher.Dispatch<DbTransaction, DbTransactionInterceptionContext>(transaction, delegate(DbTransaction t, DbTransactionInterceptionContext c)
			{
				t.Rollback();
			}, new DbTransactionInterceptionContext(interceptionContext).WithConnection(transaction.Connection), delegate(IDbTransactionInterceptor i, DbTransaction t, DbTransactionInterceptionContext c)
			{
				i.RollingBack(t, c);
			}, delegate(IDbTransactionInterceptor i, DbTransaction t, DbTransactionInterceptionContext c)
			{
				i.RolledBack(t, c);
			});
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x0003C081 File Offset: 0x0003A281
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x0003C089 File Offset: 0x0003A289
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0003C092 File Offset: 0x0003A292
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x0003C09A File Offset: 0x0003A29A
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x0400038F RID: 911
		private readonly InternalDispatcher<IDbTransactionInterceptor> _internalDispatcher = new InternalDispatcher<IDbTransactionInterceptor>();
	}
}
