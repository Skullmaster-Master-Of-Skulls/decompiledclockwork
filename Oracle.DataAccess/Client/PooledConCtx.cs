using System;
using System.Collections;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000030 RID: 48
	internal class PooledConCtx
	{
		// Token: 0x04000163 RID: 355
		public IntPtr opsConCtx;

		// Token: 0x04000164 RID: 356
		public IntPtr opsErrCtx;

		// Token: 0x04000165 RID: 357
		public unsafe OpoConValCtx* pOpoConValCtx;

		// Token: 0x04000166 RID: 358
		public OpoConRefCtx opoConRefCtx;

		// Token: 0x04000167 RID: 359
		public string conString;

		// Token: 0x04000168 RID: 360
		public DateTime creationTime;

		// Token: 0x04000169 RID: 361
		public ConPooler m_conPooler;

		// Token: 0x0400016A RID: 362
		public ConPooler m_udtDescPoolerByName;

		// Token: 0x0400016B RID: 363
		public ConPooler m_udtDescPoolerByTDO;

		// Token: 0x0400016C RID: 364
		public string m_txnid;

		// Token: 0x0400016D RID: 365
		internal Hashtable m_statementData;

		// Token: 0x0400016E RID: 366
		internal int m_totalDataAvailable;

		// Token: 0x0400016F RID: 367
		public PromotableTxnMgr m_promotableTxnManager;

		// Token: 0x04000170 RID: 368
		public FetchArrayPooler m_fetchArrayPooler;
	}
}
