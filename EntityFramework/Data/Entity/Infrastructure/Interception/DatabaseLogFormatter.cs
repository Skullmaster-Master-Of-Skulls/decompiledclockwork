using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000172 RID: 370
	public class DatabaseLogFormatter : IDbCommandInterceptor, IDbConnectionInterceptor, IDbTransactionInterceptor, IDbInterceptor
	{
		// Token: 0x06000BF9 RID: 3065 RVA: 0x00039BC4 File Offset: 0x00037DC4
		public DatabaseLogFormatter(Action<string> writeAction)
		{
			Check.NotNull<Action<string>>(writeAction, "writeAction");
			this._writeAction = writeAction;
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x00039BEA File Offset: 0x00037DEA
		public DatabaseLogFormatter(DbContext context, Action<string> writeAction)
		{
			Check.NotNull<Action<string>>(writeAction, "writeAction");
			this._context = new WeakReference(context);
			this._writeAction = writeAction;
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x00039C1C File Offset: 0x00037E1C
		protected internal DbContext Context
		{
			get
			{
				if (this._context == null || !this._context.IsAlive)
				{
					return null;
				}
				return (DbContext)this._context.Target;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x00039C45 File Offset: 0x00037E45
		internal Action<string> WriteAction
		{
			get
			{
				return this._writeAction;
			}
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x00039C4D File Offset: 0x00037E4D
		protected virtual void Write(string output)
		{
			this._writeAction(output);
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x00039C5B File Offset: 0x00037E5B
		protected internal Stopwatch Stopwatch
		{
			get
			{
				return this._stopwatch;
			}
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x00039C63 File Offset: 0x00037E63
		public virtual void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
		{
			Check.NotNull<DbCommand>(command, "command");
			Check.NotNull<DbCommandInterceptionContext<int>>(interceptionContext, "interceptionContext");
			this.Executing<int>(command, interceptionContext);
			this.Stopwatch.Restart();
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x00039C90 File Offset: 0x00037E90
		public virtual void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
		{
			Check.NotNull<DbCommand>(command, "command");
			Check.NotNull<DbCommandInterceptionContext<int>>(interceptionContext, "interceptionContext");
			this.Stopwatch.Stop();
			this.Executed<int>(command, interceptionContext);
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x00039CBD File Offset: 0x00037EBD
		public virtual void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
		{
			Check.NotNull<DbCommand>(command, "command");
			Check.NotNull<DbCommandInterceptionContext<DbDataReader>>(interceptionContext, "interceptionContext");
			this.Executing<DbDataReader>(command, interceptionContext);
			this.Stopwatch.Restart();
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x00039CEA File Offset: 0x00037EEA
		public virtual void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
		{
			Check.NotNull<DbCommand>(command, "command");
			Check.NotNull<DbCommandInterceptionContext<DbDataReader>>(interceptionContext, "interceptionContext");
			this.Stopwatch.Stop();
			this.Executed<DbDataReader>(command, interceptionContext);
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x00039D17 File Offset: 0x00037F17
		public virtual void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
		{
			Check.NotNull<DbCommand>(command, "command");
			Check.NotNull<DbCommandInterceptionContext<object>>(interceptionContext, "interceptionContext");
			this.Executing<object>(command, interceptionContext);
			this.Stopwatch.Restart();
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00039D44 File Offset: 0x00037F44
		public virtual void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
		{
			Check.NotNull<DbCommand>(command, "command");
			Check.NotNull<DbCommandInterceptionContext<object>>(interceptionContext, "interceptionContext");
			this.Stopwatch.Stop();
			this.Executed<object>(command, interceptionContext);
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x00039D74 File Offset: 0x00037F74
		public virtual void Executing<TResult>(DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext)
		{
			Check.NotNull<DbCommand>(command, "command");
			Check.NotNull<DbCommandInterceptionContext<TResult>>(interceptionContext, "interceptionContext");
			if (this.Context == null || interceptionContext.DbContexts.Contains(this.Context, new Func<DbContext, DbContext, bool>(object.ReferenceEquals)))
			{
				this.LogCommand<TResult>(command, interceptionContext);
			}
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00039DC8 File Offset: 0x00037FC8
		public virtual void Executed<TResult>(DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext)
		{
			Check.NotNull<DbCommand>(command, "command");
			Check.NotNull<DbCommandInterceptionContext<TResult>>(interceptionContext, "interceptionContext");
			if (this.Context == null || interceptionContext.DbContexts.Contains(this.Context, new Func<DbContext, DbContext, bool>(object.ReferenceEquals)))
			{
				this.LogResult<TResult>(command, interceptionContext);
			}
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x00039E1C File Offset: 0x0003801C
		public virtual void LogCommand<TResult>(DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext)
		{
			Check.NotNull<DbCommand>(command, "command");
			Check.NotNull<DbCommandInterceptionContext<TResult>>(interceptionContext, "interceptionContext");
			string text = command.CommandText ?? "<null>";
			if (text.EndsWith(Environment.NewLine, StringComparison.Ordinal))
			{
				this.Write(text);
			}
			else
			{
				this.Write(text);
				this.Write(Environment.NewLine);
			}
			if (command.Parameters != null)
			{
				foreach (DbParameter parameter in command.Parameters.OfType<DbParameter>())
				{
					this.LogParameter<TResult>(command, interceptionContext, parameter);
				}
			}
			this.Write(interceptionContext.IsAsync ? Strings.CommandLogAsync(DateTimeOffset.Now, Environment.NewLine) : Strings.CommandLogNonAsync(DateTimeOffset.Now, Environment.NewLine));
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x00039F04 File Offset: 0x00038104
		public virtual void LogParameter<TResult>(DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext, DbParameter parameter)
		{
			Check.NotNull<DbCommand>(command, "command");
			Check.NotNull<DbCommandInterceptionContext<TResult>>(interceptionContext, "interceptionContext");
			Check.NotNull<DbParameter>(parameter, "parameter");
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("-- ").Append(parameter.ParameterName).Append(": '").Append((parameter.Value == null || parameter.Value == DBNull.Value) ? "null" : parameter.Value).Append("' (Type = ").Append(parameter.DbType);
			if (parameter.Direction != ParameterDirection.Input)
			{
				stringBuilder.Append(", Direction = ").Append(parameter.Direction);
			}
			if (!parameter.IsNullable)
			{
				stringBuilder.Append(", IsNullable = false");
			}
			if (parameter.Size != 0)
			{
				stringBuilder.Append(", Size = ").Append(parameter.Size);
			}
			if (((IDbDataParameter)parameter).Precision != 0)
			{
				stringBuilder.Append(", Precision = ").Append(((IDbDataParameter)parameter).Precision);
			}
			if (((IDbDataParameter)parameter).Scale != 0)
			{
				stringBuilder.Append(", Scale = ").Append(((IDbDataParameter)parameter).Scale);
			}
			stringBuilder.Append(")").Append(Environment.NewLine);
			this.Write(stringBuilder.ToString());
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0003A058 File Offset: 0x00038258
		public virtual void LogResult<TResult>(DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext)
		{
			Check.NotNull<DbCommand>(command, "command");
			Check.NotNull<DbCommandInterceptionContext<TResult>>(interceptionContext, "interceptionContext");
			if (interceptionContext.Exception != null)
			{
				this.Write(Strings.CommandLogFailed(this.Stopwatch.ElapsedMilliseconds, interceptionContext.Exception.Message, Environment.NewLine));
			}
			else if (interceptionContext.TaskStatus.HasFlag(TaskStatus.Canceled))
			{
				this.Write(Strings.CommandLogCanceled(this.Stopwatch.ElapsedMilliseconds, Environment.NewLine));
			}
			else
			{
				TResult result = interceptionContext.Result;
				string p = (result == null) ? "null" : ((result is DbDataReader) ? result.GetType().Name : result.ToString());
				this.Write(Strings.CommandLogComplete(this.Stopwatch.ElapsedMilliseconds, p, Environment.NewLine));
			}
			this.Write(Environment.NewLine);
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0003A160 File Offset: 0x00038360
		public virtual void BeginningTransaction(DbConnection connection, BeginTransactionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x0003A164 File Offset: 0x00038364
		public virtual void BeganTransaction(DbConnection connection, BeginTransactionInterceptionContext interceptionContext)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<BeginTransactionInterceptionContext>(interceptionContext, "interceptionContext");
			if (this.Context == null || interceptionContext.DbContexts.Contains(this.Context, new Func<DbContext, DbContext, bool>(object.ReferenceEquals)))
			{
				if (interceptionContext.Exception != null)
				{
					this.Write(Strings.TransactionStartErrorLog(DateTimeOffset.Now, interceptionContext.Exception.Message, Environment.NewLine));
					return;
				}
				this.Write(Strings.TransactionStartedLog(DateTimeOffset.Now, Environment.NewLine));
			}
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0003A1F8 File Offset: 0x000383F8
		public virtual void EnlistingTransaction(DbConnection connection, EnlistTransactionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0003A1FA File Offset: 0x000383FA
		public virtual void EnlistedTransaction(DbConnection connection, EnlistTransactionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0003A1FC File Offset: 0x000383FC
		public virtual void Opening(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0003A200 File Offset: 0x00038400
		public virtual void Opened(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<DbConnectionInterceptionContext>(interceptionContext, "interceptionContext");
			if (this.Context == null || interceptionContext.DbContexts.Contains(this.Context, new Func<DbContext, DbContext, bool>(object.ReferenceEquals)))
			{
				if (interceptionContext.Exception != null)
				{
					this.Write(interceptionContext.IsAsync ? Strings.ConnectionOpenErrorLogAsync(DateTimeOffset.Now, interceptionContext.Exception.Message, Environment.NewLine) : Strings.ConnectionOpenErrorLog(DateTimeOffset.Now, interceptionContext.Exception.Message, Environment.NewLine));
					return;
				}
				if (interceptionContext.TaskStatus.HasFlag(TaskStatus.Canceled))
				{
					this.Write(Strings.ConnectionOpenCanceledLog(DateTimeOffset.Now, Environment.NewLine));
					return;
				}
				this.Write(interceptionContext.IsAsync ? Strings.ConnectionOpenedLogAsync(DateTimeOffset.Now, Environment.NewLine) : Strings.ConnectionOpenedLog(DateTimeOffset.Now, Environment.NewLine));
			}
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0003A311 File Offset: 0x00038511
		public virtual void Closing(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0003A314 File Offset: 0x00038514
		public virtual void Closed(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<DbConnectionInterceptionContext>(interceptionContext, "interceptionContext");
			if (this.Context == null || interceptionContext.DbContexts.Contains(this.Context, new Func<DbContext, DbContext, bool>(object.ReferenceEquals)))
			{
				if (interceptionContext.Exception != null)
				{
					this.Write(Strings.ConnectionCloseErrorLog(DateTimeOffset.Now, interceptionContext.Exception.Message, Environment.NewLine));
					return;
				}
				this.Write(Strings.ConnectionClosedLog(DateTimeOffset.Now, Environment.NewLine));
			}
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0003A3A8 File Offset: 0x000385A8
		public virtual void ConnectionStringGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0003A3AA File Offset: 0x000385AA
		public virtual void ConnectionStringGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0003A3AC File Offset: 0x000385AC
		public virtual void ConnectionStringSetting(DbConnection connection, DbConnectionPropertyInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0003A3AE File Offset: 0x000385AE
		public virtual void ConnectionStringSet(DbConnection connection, DbConnectionPropertyInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x0003A3B0 File Offset: 0x000385B0
		public virtual void ConnectionTimeoutGetting(DbConnection connection, DbConnectionInterceptionContext<int> interceptionContext)
		{
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x0003A3B2 File Offset: 0x000385B2
		public virtual void ConnectionTimeoutGot(DbConnection connection, DbConnectionInterceptionContext<int> interceptionContext)
		{
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0003A3B4 File Offset: 0x000385B4
		public virtual void DatabaseGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0003A3B6 File Offset: 0x000385B6
		public virtual void DatabaseGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0003A3B8 File Offset: 0x000385B8
		public virtual void DataSourceGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0003A3BA File Offset: 0x000385BA
		public virtual void DataSourceGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x0003A3BC File Offset: 0x000385BC
		public virtual void Disposing(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<DbConnectionInterceptionContext>(interceptionContext, "interceptionContext");
			if ((this.Context == null || interceptionContext.DbContexts.Contains(this.Context, new Func<DbContext, DbContext, bool>(object.ReferenceEquals))) && connection.State == ConnectionState.Open)
			{
				this.Write(Strings.ConnectionDisposedLog(DateTimeOffset.Now, Environment.NewLine));
			}
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0003A42B File Offset: 0x0003862B
		public virtual void Disposed(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x0003A42D File Offset: 0x0003862D
		public virtual void ServerVersionGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x0003A42F File Offset: 0x0003862F
		public virtual void ServerVersionGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x0003A431 File Offset: 0x00038631
		public virtual void StateGetting(DbConnection connection, DbConnectionInterceptionContext<ConnectionState> interceptionContext)
		{
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0003A433 File Offset: 0x00038633
		public virtual void StateGot(DbConnection connection, DbConnectionInterceptionContext<ConnectionState> interceptionContext)
		{
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x0003A435 File Offset: 0x00038635
		public virtual void ConnectionGetting(DbTransaction transaction, DbTransactionInterceptionContext<DbConnection> interceptionContext)
		{
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0003A437 File Offset: 0x00038637
		public virtual void ConnectionGot(DbTransaction transaction, DbTransactionInterceptionContext<DbConnection> interceptionContext)
		{
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x0003A439 File Offset: 0x00038639
		public virtual void IsolationLevelGetting(DbTransaction transaction, DbTransactionInterceptionContext<IsolationLevel> interceptionContext)
		{
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x0003A43B File Offset: 0x0003863B
		public virtual void IsolationLevelGot(DbTransaction transaction, DbTransactionInterceptionContext<IsolationLevel> interceptionContext)
		{
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x0003A43D File Offset: 0x0003863D
		public virtual void Committing(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x0003A440 File Offset: 0x00038640
		public virtual void Committed(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext)
		{
			Check.NotNull<DbTransaction>(transaction, "transaction");
			Check.NotNull<DbTransactionInterceptionContext>(interceptionContext, "interceptionContext");
			if (this.Context == null || interceptionContext.DbContexts.Contains(this.Context, new Func<DbContext, DbContext, bool>(object.ReferenceEquals)))
			{
				if (interceptionContext.Exception != null)
				{
					this.Write(Strings.TransactionCommitErrorLog(DateTimeOffset.Now, interceptionContext.Exception.Message, Environment.NewLine));
					return;
				}
				this.Write(Strings.TransactionCommittedLog(DateTimeOffset.Now, Environment.NewLine));
			}
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x0003A4D4 File Offset: 0x000386D4
		public virtual void Disposing(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext)
		{
			Check.NotNull<DbTransaction>(transaction, "transaction");
			Check.NotNull<DbTransactionInterceptionContext>(interceptionContext, "interceptionContext");
			if ((this.Context == null || interceptionContext.DbContexts.Contains(this.Context, new Func<DbContext, DbContext, bool>(object.ReferenceEquals))) && transaction.Connection != null)
			{
				this.Write(Strings.TransactionDisposedLog(DateTimeOffset.Now, Environment.NewLine));
			}
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0003A542 File Offset: 0x00038742
		public virtual void Disposed(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x0003A544 File Offset: 0x00038744
		public virtual void RollingBack(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0003A548 File Offset: 0x00038748
		public virtual void RolledBack(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext)
		{
			Check.NotNull<DbTransaction>(transaction, "transaction");
			Check.NotNull<DbTransactionInterceptionContext>(interceptionContext, "interceptionContext");
			if (this.Context == null || interceptionContext.DbContexts.Contains(this.Context, new Func<DbContext, DbContext, bool>(object.ReferenceEquals)))
			{
				if (interceptionContext.Exception != null)
				{
					this.Write(Strings.TransactionRollbackErrorLog(DateTimeOffset.Now, interceptionContext.Exception.Message, Environment.NewLine));
					return;
				}
				this.Write(Strings.TransactionRolledBackLog(DateTimeOffset.Now, Environment.NewLine));
			}
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x0003A5DC File Offset: 0x000387DC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x0003A5E4 File Offset: 0x000387E4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x0003A5ED File Offset: 0x000387ED
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x0003A5F5 File Offset: 0x000387F5
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000340 RID: 832
		private readonly WeakReference _context;

		// Token: 0x04000341 RID: 833
		private readonly Action<string> _writeAction;

		// Token: 0x04000342 RID: 834
		private readonly Stopwatch _stopwatch = new Stopwatch();
	}
}
