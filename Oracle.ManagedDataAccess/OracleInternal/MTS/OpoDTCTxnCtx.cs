using System;

namespace OracleInternal.MTS
{
	// Token: 0x0200013E RID: 318
	internal class OpoDTCTxnCtx
	{
		// Token: 0x06000CAD RID: 3245 RVA: 0x0008C194 File Offset: 0x0008A394
		internal OpoDTCTxnCtx(OpoDTCTxnXIDRefCtx opoDTCTxnXID)
		{
			this.m_opoDTCTxnXID = opoDTCTxnXID;
		}

		// Token: 0x04000DAB RID: 3499
		internal OpoDTCTxnXIDRefCtx m_opoDTCTxnXID;

		// Token: 0x04000DAC RID: 3500
		internal byte[] m_txnCtx;

		// Token: 0x04000DAD RID: 3501
		internal long m_applicationValue;
	}
}
