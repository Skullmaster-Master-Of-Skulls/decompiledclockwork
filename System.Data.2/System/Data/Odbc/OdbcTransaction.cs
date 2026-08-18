using System;
using System.Data.Common;

namespace System.Data.Odbc
{
	// Token: 0x020002B1 RID: 689
	public sealed class OdbcTransaction : DbTransaction
	{
		// Token: 0x060029DC RID: 10716 RVA: 0x00115138 File Offset: 0x00114538
		internal OdbcTransaction(OdbcConnection connection, IsolationLevel isolevel, OdbcConnectionHandle handle)
		{
			this._connection = connection;
			this._isolevel = isolevel;
			this._handle = handle;
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x060029DD RID: 10717 RVA: 0x00115168 File Offset: 0x00114568
		public new OdbcConnection Connection
		{
			get
			{
				return this._connection;
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x060029DE RID: 10718 RVA: 0x0011517C File Offset: 0x0011457C
		protected override DbConnection DbConnection
		{
			get
			{
				return this.Connection;
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x060029DF RID: 10719 RVA: 0x00115190 File Offset: 0x00114590
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

		// Token: 0x060029E0 RID: 10720 RVA: 0x00115238 File Offset: 0x00114638
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

		// Token: 0x060029E1 RID: 10721 RVA: 0x001152AC File Offset: 0x001146AC
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

		// Token: 0x060029E2 RID: 10722 RVA: 0x0011535C File Offset: 0x0011475C
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

		// Token: 0x04001AEB RID: 6891
		private OdbcConnection _connection;

		// Token: 0x04001AEC RID: 6892
		private IsolationLevel _isolevel = IsolationLevel.Unspecified;

		// Token: 0x04001AED RID: 6893
		private OdbcConnectionHandle _handle;
	}
}
