using System;

namespace System.Data.SqlClient
{
	// Token: 0x020001E2 RID: 482
	internal enum TransactionState
	{
		// Token: 0x04001130 RID: 4400
		Pending,
		// Token: 0x04001131 RID: 4401
		Active,
		// Token: 0x04001132 RID: 4402
		Aborted,
		// Token: 0x04001133 RID: 4403
		Committed,
		// Token: 0x04001134 RID: 4404
		Unknown
	}
}
