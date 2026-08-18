using System;

namespace System.Data.SqlClient
{
	// Token: 0x0200021E RID: 542
	internal sealed class SqlFedAuthToken
	{
		// Token: 0x04001458 RID: 5208
		internal uint dataLen;

		// Token: 0x04001459 RID: 5209
		internal byte[] accessToken;

		// Token: 0x0400145A RID: 5210
		internal long expirationFileTime;
	}
}
