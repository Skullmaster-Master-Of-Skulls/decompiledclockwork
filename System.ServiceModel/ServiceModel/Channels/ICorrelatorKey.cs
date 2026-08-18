using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200074B RID: 1867
	internal interface ICorrelatorKey
	{
		// Token: 0x17001216 RID: 4630
		// (get) Token: 0x0600475C RID: 18268
		// (set) Token: 0x0600475D RID: 18269
		RequestReplyCorrelator.Key RequestCorrelatorKey { get; set; }
	}
}
