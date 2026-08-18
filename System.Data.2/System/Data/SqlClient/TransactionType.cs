using System;

namespace System.Data.SqlClient
{
	// Token: 0x020001E3 RID: 483
	internal enum TransactionType
	{
		// Token: 0x04001136 RID: 4406
		LocalFromTSQL = 1,
		// Token: 0x04001137 RID: 4407
		LocalFromAPI,
		// Token: 0x04001138 RID: 4408
		Delegated,
		// Token: 0x04001139 RID: 4409
		Distributed,
		// Token: 0x0400113A RID: 4410
		Context
	}
}
