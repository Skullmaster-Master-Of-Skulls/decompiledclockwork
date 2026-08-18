using System;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace UnivOleDb22
{
	// Token: 0x02000009 RID: 9
	public class UnivTransaction : IDisposable
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00004CA4 File Offset: 0x00003CA4
		~UnivTransaction()
		{
			this.Dispose(false);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004CD8 File Offset: 0x00003CD8
		protected void Dispose(bool disposing)
		{
			if (disposing)
			{
				dbName dbName = this.myDbName;
				dbName dbName2 = dbName;
				if (dbName2 != dbName.MSAccess)
				{
					if (dbName2 == dbName.MSSQL)
					{
						SqlTransaction sqlTransaction = (SqlTransaction)this.myTransaction;
						sqlTransaction.Dispose();
					}
				}
				this.myTransaction = null;
			}
			this.disposed = true;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004D26 File Offset: 0x00003D26
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00004D38 File Offset: 0x00003D38
		public object Transaction
		{
			get
			{
				return this.myTransaction;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00004D50 File Offset: 0x00003D50
		public OleDbTransaction TransactionOleDb
		{
			get
			{
				return (OleDbTransaction)this.myTransaction;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00004D70 File Offset: 0x00003D70
		public SqlTransaction TransactionSQLServer
		{
			get
			{
				return (SqlTransaction)this.myTransaction;
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004D8D File Offset: 0x00003D8D
		public UnivTransaction(UnivConnection _univConnection, OleDbTransaction oledbTransaction)
		{
			this.myTransaction = oledbTransaction;
			this.myDbName = dbName.MSAccess;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004DAC File Offset: 0x00003DAC
		public UnivTransaction(UnivConnection _univConnection, SqlTransaction sqlTransaction)
		{
			this.myTransaction = sqlTransaction;
			this.myDbName = dbName.MSSQL;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00004DCC File Offset: 0x00003DCC
		public UnivConnection Connection
		{
			get
			{
				return this.univConnection;
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004DE4 File Offset: 0x00003DE4
		public void Commit()
		{
			dbName dbName = this.myDbName;
			dbName dbName2 = dbName;
			if (dbName2 != dbName.MSAccess)
			{
				if (dbName2 == dbName.MSSQL)
				{
					SqlTransaction sqlTransaction = (SqlTransaction)this.myTransaction;
					sqlTransaction.Commit();
				}
			}
			else
			{
				OleDbTransaction oleDbTransaction = (OleDbTransaction)this.myTransaction;
				oleDbTransaction.Commit();
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004E30 File Offset: 0x00003E30
		public void Rollback()
		{
			dbName dbName = this.myDbName;
			dbName dbName2 = dbName;
			if (dbName2 != dbName.MSAccess)
			{
				if (dbName2 == dbName.MSSQL)
				{
					SqlTransaction sqlTransaction = (SqlTransaction)this.myTransaction;
					sqlTransaction.Rollback();
				}
			}
			else
			{
				OleDbTransaction oleDbTransaction = (OleDbTransaction)this.myTransaction;
				oleDbTransaction.Rollback();
			}
		}

		// Token: 0x04000028 RID: 40
		private bool disposed = false;

		// Token: 0x04000029 RID: 41
		private object myTransaction;

		// Token: 0x0400002A RID: 42
		private dbName myDbName;

		// Token: 0x0400002B RID: 43
		private UnivConnection univConnection;
	}
}
