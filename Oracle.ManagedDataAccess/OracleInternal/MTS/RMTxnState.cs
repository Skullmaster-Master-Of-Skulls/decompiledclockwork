using System;

namespace OracleInternal.MTS
{
	// Token: 0x02000126 RID: 294
	internal enum RMTxnState
	{
		// Token: 0x04000D72 RID: 3442
		Invalid,
		// Token: 0x04000D73 RID: 3443
		Started,
		// Token: 0x04000D74 RID: 3444
		Enlisted,
		// Token: 0x04000D75 RID: 3445
		Preparing,
		// Token: 0x04000D76 RID: 3446
		Prepared_OnePhase,
		// Token: 0x04000D77 RID: 3447
		Prepared_ToCommit,
		// Token: 0x04000D78 RID: 3448
		Prepared_ReadOnly,
		// Token: 0x04000D79 RID: 3449
		Prepared_Failed,
		// Token: 0x04000D7A RID: 3450
		Committing,
		// Token: 0x04000D7B RID: 3451
		Committed,
		// Token: 0x04000D7C RID: 3452
		Commit_Failed,
		// Token: 0x04000D7D RID: 3453
		RollingBack,
		// Token: 0x04000D7E RID: 3454
		RolledBack = 11,
		// Token: 0x04000D7F RID: 3455
		Rollback_Failed
	}
}
