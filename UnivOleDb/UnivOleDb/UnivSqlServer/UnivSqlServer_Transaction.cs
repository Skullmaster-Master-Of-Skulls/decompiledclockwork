using System;
using System.Data.SqlClient;

namespace UnivOleDb.UnivSqlServer
{
	// Token: 0x0200001B RID: 27
	[Serializable]
	public class UnivSqlServer_Transaction : UnivTransaction
	{
		// Token: 0x06000185 RID: 389 RVA: 0x00007E14 File Offset: 0x00006E14
		public UnivSqlServer_Transaction(UnivSqlServer_Connection conn, SqlTransaction sqlTransaction)
		{
			this.myUnivConnection = conn;
			this.mySqlTransaction = sqlTransaction;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00007E2C File Offset: 0x00006E2C
		public SqlTransaction Transaction
		{
			get
			{
				return this.mySqlTransaction;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00007E44 File Offset: 0x00006E44
		public UnivConnection Connection
		{
			get
			{
				return this.myUnivConnection;
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00007E5C File Offset: 0x00006E5C
		public void Commit()
		{
			this.mySqlTransaction.Commit();
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00007E6B File Offset: 0x00006E6B
		public void Rollback()
		{
			this.mySqlTransaction.Rollback();
		}

		// Token: 0x04000051 RID: 81
		private UnivSqlServer_Connection myUnivConnection;

		// Token: 0x04000052 RID: 82
		private SqlTransaction mySqlTransaction;
	}
}
