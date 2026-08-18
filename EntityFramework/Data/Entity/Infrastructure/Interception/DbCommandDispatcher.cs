using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000177 RID: 375
	public class DbCommandDispatcher
	{
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x0003A98B File Offset: 0x00038B8B
		internal InternalDispatcher<IDbCommandInterceptor> InternalDispatcher
		{
			get
			{
				return this._internalDispatcher;
			}
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x0003A993 File Offset: 0x00038B93
		internal DbCommandDispatcher()
		{
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0003A9C4 File Offset: 0x00038BC4
		public virtual int NonQuery(DbCommand command, DbCommandInterceptionContext interceptionContext)
		{
			Check.NotNull<DbCommand>(command, "command");
			Check.NotNull<DbCommandInterceptionContext>(interceptionContext, "interceptionContext");
			return this._internalDispatcher.Dispatch<DbCommand, DbCommandInterceptionContext<int>, int>(command, (DbCommand t, DbCommandInterceptionContext<int> c) => t.ExecuteNonQuery(), new DbCommandInterceptionContext<int>(interceptionContext), delegate(IDbCommandInterceptor i, DbCommand t, DbCommandInterceptionContext<int> c)
			{
				i.NonQueryExecuting(t, c);
			}, delegate(IDbCommandInterceptor i, DbCommand t, DbCommandInterceptionContext<int> c)
			{
				i.NonQueryExecuted(t, c);
			});
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0003AA70 File Offset: 0x00038C70
		public virtual object Scalar(DbCommand command, DbCommandInterceptionContext interceptionContext)
		{
			Check.NotNull<DbCommand>(command, "command");
			Check.NotNull<DbCommandInterceptionContext>(interceptionContext, "interceptionContext");
			return this._internalDispatcher.Dispatch<DbCommand, DbCommandInterceptionContext<object>, object>(command, (DbCommand t, DbCommandInterceptionContext<object> c) => t.ExecuteScalar(), new DbCommandInterceptionContext<object>(interceptionContext), delegate(IDbCommandInterceptor i, DbCommand t, DbCommandInterceptionContext<object> c)
			{
				i.ScalarExecuting(t, c);
			}, delegate(IDbCommandInterceptor i, DbCommand t, DbCommandInterceptionContext<object> c)
			{
				i.ScalarExecuted(t, c);
			});
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0003AB20 File Offset: 0x00038D20
		public virtual DbDataReader Reader(DbCommand command, DbCommandInterceptionContext interceptionContext)
		{
			Check.NotNull<DbCommand>(command, "command");
			Check.NotNull<DbCommandInterceptionContext>(interceptionContext, "interceptionContext");
			return this._internalDispatcher.Dispatch<DbCommand, DbCommandInterceptionContext<DbDataReader>, DbDataReader>(command, (DbCommand t, DbCommandInterceptionContext<DbDataReader> c) => t.ExecuteReader(c.CommandBehavior), new DbCommandInterceptionContext<DbDataReader>(interceptionContext), delegate(IDbCommandInterceptor i, DbCommand t, DbCommandInterceptionContext<DbDataReader> c)
			{
				i.ReaderExecuting(t, c);
			}, delegate(IDbCommandInterceptor i, DbCommand t, DbCommandInterceptionContext<DbDataReader> c)
			{
				i.ReaderExecuted(t, c);
			});
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x0003ABCC File Offset: 0x00038DCC
		public virtual Task<int> NonQueryAsync(DbCommand command, DbCommandInterceptionContext interceptionContext, CancellationToken cancellationToken)
		{
			Check.NotNull<DbCommand>(command, "command");
			Check.NotNull<DbCommandInterceptionContext>(interceptionContext, "interceptionContext");
			return this._internalDispatcher.DispatchAsync<DbCommand, DbCommandInterceptionContext<int>, int>(command, (DbCommand t, DbCommandInterceptionContext<int> c, CancellationToken ct) => t.ExecuteNonQueryAsync(ct), new DbCommandInterceptionContext<int>(interceptionContext).AsAsync(), delegate(IDbCommandInterceptor i, DbCommand t, DbCommandInterceptionContext<int> c)
			{
				i.NonQueryExecuting(t, c);
			}, delegate(IDbCommandInterceptor i, DbCommand t, DbCommandInterceptionContext<int> c)
			{
				i.NonQueryExecuted(t, c);
			}, cancellationToken);
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x0003AC80 File Offset: 0x00038E80
		public virtual Task<object> ScalarAsync(DbCommand command, DbCommandInterceptionContext interceptionContext, CancellationToken cancellationToken)
		{
			Check.NotNull<DbCommand>(command, "command");
			Check.NotNull<DbCommandInterceptionContext>(interceptionContext, "interceptionContext");
			return this._internalDispatcher.DispatchAsync<DbCommand, DbCommandInterceptionContext<object>, object>(command, (DbCommand t, DbCommandInterceptionContext<object> c, CancellationToken ct) => t.ExecuteScalarAsync(ct), new DbCommandInterceptionContext<object>(interceptionContext).AsAsync(), delegate(IDbCommandInterceptor i, DbCommand t, DbCommandInterceptionContext<object> c)
			{
				i.ScalarExecuting(t, c);
			}, delegate(IDbCommandInterceptor i, DbCommand t, DbCommandInterceptionContext<object> c)
			{
				i.ScalarExecuted(t, c);
			}, cancellationToken);
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x0003AD38 File Offset: 0x00038F38
		public virtual Task<DbDataReader> ReaderAsync(DbCommand command, DbCommandInterceptionContext interceptionContext, CancellationToken cancellationToken)
		{
			Check.NotNull<DbCommand>(command, "command");
			Check.NotNull<DbCommandInterceptionContext>(interceptionContext, "interceptionContext");
			return this._internalDispatcher.DispatchAsync<DbCommand, DbCommandInterceptionContext<DbDataReader>, DbDataReader>(command, (DbCommand t, DbCommandInterceptionContext<DbDataReader> c, CancellationToken ct) => t.ExecuteReaderAsync(c.CommandBehavior, ct), new DbCommandInterceptionContext<DbDataReader>(interceptionContext).AsAsync(), delegate(IDbCommandInterceptor i, DbCommand t, DbCommandInterceptionContext<DbDataReader> c)
			{
				i.ReaderExecuting(t, c);
			}, delegate(IDbCommandInterceptor i, DbCommand t, DbCommandInterceptionContext<DbDataReader> c)
			{
				i.ReaderExecuted(t, c);
			}, cancellationToken);
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x0003ADCC File Offset: 0x00038FCC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x0003ADD4 File Offset: 0x00038FD4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x0003ADDD File Offset: 0x00038FDD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x0003ADE5 File Offset: 0x00038FE5
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000346 RID: 838
		private readonly InternalDispatcher<IDbCommandInterceptor> _internalDispatcher = new InternalDispatcher<IDbCommandInterceptor>();
	}
}
