using System;

namespace System.Data.SqlClient
{
	// Token: 0x020002FD RID: 765
	internal enum TransactionState
	{
		// Token: 0x04001920 RID: 6432
		Pending,
		// Token: 0x04001921 RID: 6433
		Active,
		// Token: 0x04001922 RID: 6434
		Aborted,
		// Token: 0x04001923 RID: 6435
		Committed,
		// Token: 0x04001924 RID: 6436
		Unknown
	}
}
