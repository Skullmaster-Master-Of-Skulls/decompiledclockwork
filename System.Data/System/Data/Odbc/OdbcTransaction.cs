using System;
using System.Data.Common;

namespace System.Data.Odbc
{
	// Token: 0x02000207 RID: 519
	public sealed class OdbcTransaction : DbTransaction
	{
		// Token: 0x06001C9F RID: 7327 RVA: 0x00269888 File Offset: 0x00268C88
		internal OdbcTransaction(OdbcConnection connection, IsolationLevel isolevel, OdbcConnectionHandle handle)
		{
			this._connection = connection;
			this._isolevel = isolevel;
			this._handle = handle;
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06001CA0 RID: 7328 RVA: 0x002698B8 File Offset: 0x00268CB8
		public new OdbcConnection Connection
		{
			get
			{
				return this._connection;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06001CA1 RID: 7329 RVA: 0x002698D8 File Offset: 0x00268CD8
		protected override DbConnection DbConnection
		{
			get
			{
				return this.Connection;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06001CA2 RID: 7330 RVA: 0x002698F8 File Offset: 0x00268CF8
		public override IsolationLevel IsolationLevel
		{
			get
			{
				OdbcConnection connection = this._connection;
				if (connection == null)
				{
					throw ADP.TransactionZombied(this);
				}
				if (IsolationLevel.Unspecified == this._isolevel)
				{
					int connectAttr = connection.GetConnectAttr(ODBC32.SQL_ATTR.TXN_ISOLATION, ODBC32.HANDLER.THROW);
					ODBC32.SQL_TRANSACTION sql_TRANSACTION = (ODBC32.SQL_TRANSACTION)connectAttr;
					switch (sql_TRANSACTION)
					{
					case ODBC32.SQL_TRANSACTION.READ_UNCOMMITTED:
						this._isolevel = IsolationLevel.ReadUncommitted;
						goto IL_94;
					case ODBC32.SQL_TRANSACTION.READ_COMMITTED:
						this._isolevel = IsolationLevel.ReadCommitted;
						goto IL_94;
					case (ODBC32.SQL_TRANSACTION)3:
						break;
					case ODBC32.SQL_TRANSACTION.REPEATABLE_READ:
						this._isolevel = IsolationLevel.RepeatableRead;
						goto IL_94;
					default:
						if (sql_TRANSACTION == ODBC32.SQL_TRANSACTION.SERIALIZABLE)
						{
							this._isolevel = IsolationLevel.Serializable;
							goto IL_94;
						}
						if (sql_TRANSACTION == ODBC32.SQL_TRANSACTION.SNAPSHOT)
						{
							this._isolevel = IsolationLevel.Snapshot;
							goto IL_94;
						}
						break;
					}
					throw ODBC.NoMappingForSqlTransactionLevel(connectAttr);
				}
				IL_94:
				return this._isolevel;
			}
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x002699A8 File Offset: 0x00268DA8
		public override void Commit()
		{
			OdbcConnection.ExecutePermission.Demand();
			OdbcConnection connection = this._connection;
			if (connection == null)
			{
				throw ADP.TransactionZombied(this);
			}
			connection.CheckState("CommitTransaction");
			if (this._handle == null)
			{
				throw ODBC.NotInTransaction();
			}
			ODBC32.RetCode retCode = this._handle.CompleteTransaction(0);
			if (retCode == ODBC32.RetCode.ERROR)
			{
				connection.HandleError(this._handle, retCode);
			}
			connection.LocalTransaction = null;
			this._connection = null;
			this._handle = null;
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x00269A28 File Offset: 0x00268E28
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				OdbcConnectionHandle handle = this._handle;
				this._handle = null;
				if (handle != null)
				{
					try
					{
						ODBC32.RetCode retCode = handle.CompleteTransaction(1);
						if (retCode == ODBC32.RetCode.ERROR && this._connection != null)
						{
							Exception e = this._connection.HandleErrorNoThrow(handle, retCode);
							ADP.TraceExceptionWithoutRethrow(e);
						}
					}
					catch (Exception e2)
					{
						if (!ADP.IsCatchableExceptionType(e2))
						{
							throw;
						}
					}
				}
				if (this._connection != null && this._connection.IsOpen)
				{
					this._connection.LocalTransaction = null;
				}
				this._connection = null;
				this._isolevel = IsolationLevel.Unspecified;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x00269AD8 File Offset: 0x00268ED8
		public override void Rollback()
		{
			OdbcConnection connection = this._connection;
			if (connection == null)
			{
				throw ADP.TransactionZombied(this);
			}
			connection.CheckState("RollbackTransaction");
			if (this._handle == null)
			{
				throw ODBC.NotInTransaction();
			}
			ODBC32.RetCode retCode = this._handle.CompleteTransaction(1);
			if (retCode == ODBC32.RetCode.ERROR)
			{
				connection.HandleError(this._handle, retCode);
			}
			connection.LocalTransaction = null;
			this._connection = null;
			this._handle = null;
		}

		// Token: 0x04001079 RID: 4217
		private OdbcConnection _connection;

		// Token: 0x0400107A RID: 4218
		private IsolationLevel _isolevel = IsolationLevel.Unspecified;

		// Token: 0x0400107B RID: 4219
		private OdbcConnectionHandle _handle;
	}
}
