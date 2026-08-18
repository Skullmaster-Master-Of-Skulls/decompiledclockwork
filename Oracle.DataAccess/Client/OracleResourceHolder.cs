using System;
using System.Collections;
using System.Transactions;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000090 RID: 144
	internal class OracleResourceHolder : IDisposable
	{
		// Token: 0x060006F7 RID: 1783 RVA: 0x00045BEC File Offset: 0x00044BEC
		public OracleResourceHolder(string txnLocalId, OracleResourcePool oraResPool)
		{
			this.m_txnLocalId = txnLocalId;
			this.m_oraResPool = oraResPool;
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00045C10 File Offset: 0x00044C10
		internal void TransactionCompleted(object sender, TransactionEventArgs e)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(2U, new string[]
				{
					" (POOL) OracleResourceHolder::TransactionCompleted(), Local Identifier = {0}\n",
					this.m_txnLocalId
				});
			}
			if (!this.m_disposed)
			{
				this.Dispose();
			}
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00045C51 File Offset: 0x00044C51
		public void Dispose()
		{
			if (this.m_disposed)
			{
				return;
			}
			GC.SuppressFinalize(this);
			this.m_disposed = true;
			if (this.m_oraResPool != null)
			{
				this.m_oraResPool.RemoveResourceHolder(this);
			}
			this.m_oraResPool = null;
			this.m_txnLocalId = null;
		}

		// Token: 0x04000412 RID: 1042
		internal string m_txnLocalId;

		// Token: 0x04000413 RID: 1043
		internal Stack m_stack = new Stack();

		// Token: 0x04000414 RID: 1044
		internal object m_resourceWithLocalTxn;

		// Token: 0x04000415 RID: 1045
		private OracleResourcePool m_oraResPool;

		// Token: 0x04000416 RID: 1046
		internal bool m_disposed;
	}
}
