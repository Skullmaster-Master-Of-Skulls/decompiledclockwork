using System;

namespace System.Data.SqlClient
{
	// Token: 0x020002FE RID: 766
	internal enum TransactionType
	{
		// Token: 0x04001926 RID: 6438
		LocalFromTSQL = 1,
		// Token: 0x04001927 RID: 6439
		LocalFromAPI,
		// Token: 0x04001928 RID: 6440
		Delegated,
		// Token: 0x04001929 RID: 6441
		Distributed,
		// Token: 0x0400192A RID: 6442
		Context
	}
}
