using System;

namespace System.Data.SqlClient
{
	// Token: 0x020001DA RID: 474
	internal enum ActiveDirectoryAuthenticationTimeoutRetryState
	{
		// Token: 0x0400111A RID: 4378
		NotStarted,
		// Token: 0x0400111B RID: 4379
		Retrying,
		// Token: 0x0400111C RID: 4380
		HasLoggedIn
	}
}
