using System;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001D7 RID: 471
	internal enum COMAdminIsolationLevel
	{
		// Token: 0x040017B7 RID: 6071
		Any,
		// Token: 0x040017B8 RID: 6072
		ReadUncommitted,
		// Token: 0x040017B9 RID: 6073
		ReadCommitted,
		// Token: 0x040017BA RID: 6074
		RepeatableRead,
		// Token: 0x040017BB RID: 6075
		Serializable
	}
}
