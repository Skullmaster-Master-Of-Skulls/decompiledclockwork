using System;

namespace OracleInternal.SelfTuning
{
	// Token: 0x02000194 RID: 404
	internal enum OracleTunerState : byte
	{
		// Token: 0x040011F9 RID: 4601
		INIT,
		// Token: 0x040011FA RID: 4602
		WAIT,
		// Token: 0x040011FB RID: 4603
		SCAN,
		// Token: 0x040011FC RID: 4604
		REDUCE,
		// Token: 0x040011FD RID: 4605
		OPTIMIZE,
		// Token: 0x040011FE RID: 4606
		WATCH,
		// Token: 0x040011FF RID: 4607
		REVERT
	}
}
