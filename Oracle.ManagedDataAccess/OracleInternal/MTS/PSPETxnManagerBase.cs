using System;
using System.Transactions;
using Oracle.ManagedDataAccess.Client;

namespace OracleInternal.MTS
{
	// Token: 0x02000113 RID: 275
	internal abstract class PSPETxnManagerBase : IPromotableSinglePhaseNotification, ITransactionPromoter
	{
		// Token: 0x06000BF0 RID: 3056
		public abstract void Initialize();

		// Token: 0x06000BF1 RID: 3057
		public abstract void Rollback(SinglePhaseEnlistment singlePhaseEnlistment);

		// Token: 0x06000BF2 RID: 3058
		public abstract void SinglePhaseCommit(SinglePhaseEnlistment singlePhaseEnlistment);

		// Token: 0x06000BF3 RID: 3059
		public abstract byte[] Promote();

		// Token: 0x04000D21 RID: 3361
		internal OracleTransaction m_oraTransaction;

		// Token: 0x04000D22 RID: 3362
		internal bool m_bLocalTxnPromoted;

		// Token: 0x04000D23 RID: 3363
		internal string m_localTxnIdentifier;

		// Token: 0x04000D24 RID: 3364
		internal bool m_bConnectionClosed;
	}
}
