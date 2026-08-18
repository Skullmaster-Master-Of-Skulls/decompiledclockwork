using System;
using System.Collections;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Internal
{
	// Token: 0x020001D7 RID: 471
	internal sealed class InterceptableDbCommand : DbCommand
	{
		// Token: 0x06001066 RID: 4198 RVA: 0x00046541 File Offset: 0x00044741
		public InterceptableDbCommand(DbCommand command, DbInterceptionContext context, DbDispatchers dispatchers = null)
		{
			GC.SuppressFinalize(this);
			this._command = command;
			this._interceptionContext = context;
			this._dispatchers = (dispatchers ?? DbInterception.Dispatch);
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x0004656D File Offset: 0x0004476D
		public DbInterceptionContext InterceptionContext
		{
			get
			{
				return this._interceptionContext;
			}
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x00046575 File Offset: 0x00044775
		public override void Prepare()
		{
			this._command.Prepare();
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x00046582 File Offset: 0x00044782
		// (set) Token: 0x0600106A RID: 4202 RVA: 0x0004658F File Offset: 0x0004478F
		[SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
		public override string CommandText
		{
			get
			{
				return this._command.CommandText;
			}
			set
			{
				this._command.CommandText = value;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x0004659D File Offset: 0x0004479D
		// (set) Token: 0x0600106C RID: 4204 RVA: 0x000465AA File Offset: 0x000447AA
		public override int CommandTimeout
		{
			get
			{
				return this._command.CommandTimeout;
			}
			set
			{
				this._command.CommandTimeout = value;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600106D RID: 4205 RVA: 0x000465B8 File Offset: 0x000447B8
		// (set) Token: 0x0600106E RID: 4206 RVA: 0x000465C5 File Offset: 0x000447C5
		public override CommandType CommandType
		{
			get
			{
				return this._command.CommandType;
			}
			set
			{
				this._command.CommandType = value;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600106F RID: 4207 RVA: 0x000465D3 File Offset: 0x000447D3
		// (set) Token: 0x06001070 RID: 4208 RVA: 0x000465E0 File Offset: 0x000447E0
		public override UpdateRowSource UpdatedRowSource
		{
			get
			{
				return this._command.UpdatedRowSource;
			}
			set
			{
				this._command.UpdatedRowSource = value;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x000465EE File Offset: 0x000447EE
		// (set) Token: 0x06001072 RID: 4210 RVA: 0x000465FB File Offset: 0x000447FB
		protected override DbConnection DbConnection
		{
			get
			{
				return this._command.Connection;
			}
			set
			{
				this._command.Connection = value;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06001073 RID: 4211 RVA: 0x00046609 File Offset: 0x00044809
		protected override DbParameterCollection DbParameterCollection
		{
			get
			{
				return this._command.Parameters;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x00046616 File Offset: 0x00044816
		// (set) Token: 0x06001075 RID: 4213 RVA: 0x00046623 File Offset: 0x00044823
		protected override DbTransaction DbTransaction
		{
			get
			{
				return this._command.Transaction;
			}
			set
			{
				this._command.Transaction = value;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x00046631 File Offset: 0x00044831
		// (set) Token: 0x06001077 RID: 4215 RVA: 0x0004663E File Offset: 0x0004483E
		public override bool DesignTimeVisible
		{
			get
			{
				return this._command.DesignTimeVisible;
			}
			set
			{
				this._command.DesignTimeVisible = value;
			}
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x0004664C File Offset: 0x0004484C
		public override void Cancel()
		{
			this._command.Cancel();
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x00046659 File Offset: 0x00044859
		protected override DbParameter CreateDbParameter()
		{
			return this._command.CreateParameter();
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x00046668 File Offset: 0x00044868
		public override int ExecuteNonQuery()
		{
			if (!this._dispatchers.CancelableCommand.Executing(this._command, this._interceptionContext))
			{
				return 1;
			}
			return this._dispatchers.Command.NonQuery(this._command, new DbCommandInterceptionContext(this._interceptionContext));
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x000466B8 File Offset: 0x000448B8
		public override object ExecuteScalar()
		{
			if (!this._dispatchers.CancelableCommand.Executing(this._command, this._interceptionContext))
			{
				return null;
			}
			return this._dispatchers.Command.Scalar(this._command, new DbCommandInterceptionContext(this._interceptionContext));
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x00046708 File Offset: 0x00044908
		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			if (!this._dispatchers.CancelableCommand.Executing(this._command, this._interceptionContext))
			{
				return new InterceptableDbCommand.NullDataReader();
			}
			DbCommandInterceptionContext dbCommandInterceptionContext = new DbCommandInterceptionContext(this._interceptionContext);
			if (behavior != CommandBehavior.Default)
			{
				dbCommandInterceptionContext = dbCommandInterceptionContext.WithCommandBehavior(behavior);
			}
			return this._dispatchers.Command.Reader(this._command, dbCommandInterceptionContext);
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0004676C File Offset: 0x0004496C
		public override Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			if (!this._dispatchers.CancelableCommand.Executing(this._command, this._interceptionContext))
			{
				return new Task<int>(() => 1);
			}
			return this._dispatchers.Command.NonQueryAsync(this._command, new DbCommandInterceptionContext(this._interceptionContext), cancellationToken);
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x000467E8 File Offset: 0x000449E8
		public override Task<object> ExecuteScalarAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			if (!this._dispatchers.CancelableCommand.Executing(this._command, this._interceptionContext))
			{
				return new Task<object>(() => null);
			}
			return this._dispatchers.Command.ScalarAsync(this._command, new DbCommandInterceptionContext(this._interceptionContext), cancellationToken);
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x00046868 File Offset: 0x00044A68
		protected override Task<DbDataReader> ExecuteDbDataReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			if (!this._dispatchers.CancelableCommand.Executing(this._command, this._interceptionContext))
			{
				return new Task<DbDataReader>(() => new InterceptableDbCommand.NullDataReader());
			}
			DbCommandInterceptionContext dbCommandInterceptionContext = new DbCommandInterceptionContext(this._interceptionContext);
			if (behavior != CommandBehavior.Default)
			{
				dbCommandInterceptionContext = dbCommandInterceptionContext.WithCommandBehavior(behavior);
			}
			return this._dispatchers.Command.ReaderAsync(this._command, dbCommandInterceptionContext, cancellationToken);
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x000468EC File Offset: 0x00044AEC
		protected override void Dispose(bool disposing)
		{
			if (disposing && this._command != null)
			{
				this._command.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x040004B2 RID: 1202
		private readonly DbCommand _command;

		// Token: 0x040004B3 RID: 1203
		private readonly DbInterceptionContext _interceptionContext;

		// Token: 0x040004B4 RID: 1204
		private readonly DbDispatchers _dispatchers;

		// Token: 0x020001D8 RID: 472
		private class NullDataReader : DbDataReader
		{
			// Token: 0x06001084 RID: 4228 RVA: 0x0004690B File Offset: 0x00044B0B
			public override void Close()
			{
			}

			// Token: 0x06001085 RID: 4229 RVA: 0x00046910 File Offset: 0x00044B10
			public override bool NextResult()
			{
				return this._resultCount++ == 0;
			}

			// Token: 0x06001086 RID: 4230 RVA: 0x00046934 File Offset: 0x00044B34
			public override bool Read()
			{
				return this._readCount++ == 0;
			}

			// Token: 0x17000191 RID: 401
			// (get) Token: 0x06001087 RID: 4231 RVA: 0x00046955 File Offset: 0x00044B55
			public override bool IsClosed
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000192 RID: 402
			// (get) Token: 0x06001088 RID: 4232 RVA: 0x00046958 File Offset: 0x00044B58
			public override int FieldCount
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x06001089 RID: 4233 RVA: 0x0004695B File Offset: 0x00044B5B
			public override int GetOrdinal(string name)
			{
				return -1;
			}

			// Token: 0x0600108A RID: 4234 RVA: 0x0004695E File Offset: 0x00044B5E
			public override object GetValue(int ordinal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600108B RID: 4235 RVA: 0x00046965 File Offset: 0x00044B65
			public override DataTable GetSchemaTable()
			{
				throw new NotImplementedException();
			}

			// Token: 0x17000193 RID: 403
			// (get) Token: 0x0600108C RID: 4236 RVA: 0x0004696C File Offset: 0x00044B6C
			public override int Depth
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000194 RID: 404
			// (get) Token: 0x0600108D RID: 4237 RVA: 0x00046973 File Offset: 0x00044B73
			public override int RecordsAffected
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x0600108E RID: 4238 RVA: 0x00046976 File Offset: 0x00044B76
			public override bool GetBoolean(int ordinal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600108F RID: 4239 RVA: 0x0004697D File Offset: 0x00044B7D
			public override byte GetByte(int ordinal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06001090 RID: 4240 RVA: 0x00046984 File Offset: 0x00044B84
			public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06001091 RID: 4241 RVA: 0x0004698B File Offset: 0x00044B8B
			public override char GetChar(int ordinal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06001092 RID: 4242 RVA: 0x00046992 File Offset: 0x00044B92
			public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06001093 RID: 4243 RVA: 0x00046999 File Offset: 0x00044B99
			public override Guid GetGuid(int ordinal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06001094 RID: 4244 RVA: 0x000469A0 File Offset: 0x00044BA0
			public override short GetInt16(int ordinal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06001095 RID: 4245 RVA: 0x000469A7 File Offset: 0x00044BA7
			public override int GetInt32(int ordinal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06001096 RID: 4246 RVA: 0x000469AE File Offset: 0x00044BAE
			public override long GetInt64(int ordinal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06001097 RID: 4247 RVA: 0x000469B5 File Offset: 0x00044BB5
			public override DateTime GetDateTime(int ordinal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06001098 RID: 4248 RVA: 0x000469BC File Offset: 0x00044BBC
			public override string GetString(int ordinal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06001099 RID: 4249 RVA: 0x000469C3 File Offset: 0x00044BC3
			public override decimal GetDecimal(int ordinal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600109A RID: 4250 RVA: 0x000469CA File Offset: 0x00044BCA
			public override double GetDouble(int ordinal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600109B RID: 4251 RVA: 0x000469D1 File Offset: 0x00044BD1
			public override float GetFloat(int ordinal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600109C RID: 4252 RVA: 0x000469D8 File Offset: 0x00044BD8
			public override string GetName(int ordinal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600109D RID: 4253 RVA: 0x000469DF File Offset: 0x00044BDF
			public override int GetValues(object[] values)
			{
				return 0;
			}

			// Token: 0x0600109E RID: 4254 RVA: 0x000469E2 File Offset: 0x00044BE2
			public override bool IsDBNull(int ordinal)
			{
				return true;
			}

			// Token: 0x17000195 RID: 405
			public override object this[int ordinal]
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000196 RID: 406
			public override object this[string name]
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000197 RID: 407
			// (get) Token: 0x060010A1 RID: 4257 RVA: 0x000469F3 File Offset: 0x00044BF3
			public override bool HasRows
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x060010A2 RID: 4258 RVA: 0x000469FA File Offset: 0x00044BFA
			public override string GetDataTypeName(int ordinal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060010A3 RID: 4259 RVA: 0x00046A01 File Offset: 0x00044C01
			public override Type GetFieldType(int ordinal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060010A4 RID: 4260 RVA: 0x00046A08 File Offset: 0x00044C08
			public override IEnumerator GetEnumerator()
			{
				throw new NotImplementedException();
			}

			// Token: 0x040004B8 RID: 1208
			private int _resultCount;

			// Token: 0x040004B9 RID: 1209
			private int _readCount;
		}
	}
}
