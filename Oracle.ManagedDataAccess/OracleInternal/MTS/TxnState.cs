using System;

namespace OracleInternal.MTS
{
	// Token: 0x02000125 RID: 293
	internal enum TxnState
	{
		// Token: 0x04000D64 RID: 3428
		K2CMDprepare,
		// Token: 0x04000D65 RID: 3429
		K2CMDrqcommit,
		// Token: 0x04000D66 RID: 3430
		K2CMDcommit,
		// Token: 0x04000D67 RID: 3431
		K2CMDabort,
		// Token: 0x04000D68 RID: 3432
		K2CMDrdonly,
		// Token: 0x04000D69 RID: 3433
		K2CMDforget,
		// Token: 0x04000D6A RID: 3434
		K2CMDrecovered = 7,
		// Token: 0x04000D6B RID: 3435
		K2CMDtimeout,
		// Token: 0x04000D6C RID: 3436
		Error = 10,
		// Token: 0x04000D6D RID: 3437
		NotStarted,
		// Token: 0x04000D6E RID: 3438
		Started,
		// Token: 0x04000D6F RID: 3439
		XStarted,
		// Token: 0x04000D70 RID: 3440
		Detached
	}
}
