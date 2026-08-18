using System;
using System.Data.OleDb;

namespace UnivOleDb.UnivMSAccess
{
	// Token: 0x02000027 RID: 39
	public class UnivMSAccess_Transaction : UnivTransaction
	{
		// Token: 0x06000204 RID: 516 RVA: 0x00009454 File Offset: 0x00008454
		public UnivMSAccess_Transaction(UnivMSAccess_Connection conn, OleDbTransaction oleDbTransaction)
		{
			this.myUnivConnection = conn;
			this.myOleDbTransaction = oleDbTransaction;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0000946C File Offset: 0x0000846C
		public OleDbTransaction Transaction
		{
			get
			{
				return this.myOleDbTransaction;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00009484 File Offset: 0x00008484
		public UnivConnection Connection
		{
			get
			{
				return this.myUnivConnection;
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000949C File Offset: 0x0000849C
		public void Commit()
		{
			this.myOleDbTransaction.Commit();
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000094AB File Offset: 0x000084AB
		public void Rollback()
		{
			this.myOleDbTransaction.Rollback();
		}

		// Token: 0x04000071 RID: 113
		private UnivMSAccess_Connection myUnivConnection;

		// Token: 0x04000072 RID: 114
		private OleDbTransaction myOleDbTransaction;
	}
}
