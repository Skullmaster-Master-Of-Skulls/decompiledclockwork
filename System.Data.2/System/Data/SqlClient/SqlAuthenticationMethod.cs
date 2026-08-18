using System;

namespace System.Data.SqlClient
{
	// Token: 0x0200020B RID: 523
	public enum SqlAuthenticationMethod
	{
		// Token: 0x040013B7 RID: 5047
		NotSpecified,
		// Token: 0x040013B8 RID: 5048
		SqlPassword,
		// Token: 0x040013B9 RID: 5049
		ActiveDirectoryPassword,
		// Token: 0x040013BA RID: 5050
		ActiveDirectoryIntegrated,
		// Token: 0x040013BB RID: 5051
		ActiveDirectoryInteractive
	}
}
