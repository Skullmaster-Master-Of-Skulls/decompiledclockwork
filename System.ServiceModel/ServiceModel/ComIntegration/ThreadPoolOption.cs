using System;
using System.Runtime.InteropServices;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000260 RID: 608
	[ComVisible(false)]
	[Serializable]
	internal enum ThreadPoolOption
	{
		// Token: 0x0400198E RID: 6542
		None,
		// Token: 0x0400198F RID: 6543
		Inherit,
		// Token: 0x04001990 RID: 6544
		STA,
		// Token: 0x04001991 RID: 6545
		MTA
	}
}
