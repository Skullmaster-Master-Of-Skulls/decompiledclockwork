using System;

namespace System.Data.SqlClient
{
	// Token: 0x020001C0 RID: 448
	internal enum SqlConnectionTimeoutErrorPhase
	{
		// Token: 0x04000FF2 RID: 4082
		Undefined,
		// Token: 0x04000FF3 RID: 4083
		PreLoginBegin,
		// Token: 0x04000FF4 RID: 4084
		InitializeConnection,
		// Token: 0x04000FF5 RID: 4085
		SendPreLoginHandshake,
		// Token: 0x04000FF6 RID: 4086
		ConsumePreLoginHandshake,
		// Token: 0x04000FF7 RID: 4087
		LoginBegin,
		// Token: 0x04000FF8 RID: 4088
		ProcessConnectionAuth,
		// Token: 0x04000FF9 RID: 4089
		PostLogin,
		// Token: 0x04000FFA RID: 4090
		Complete,
		// Token: 0x04000FFB RID: 4091
		Count
	}
}
