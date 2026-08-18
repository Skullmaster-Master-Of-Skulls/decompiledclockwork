using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200020F RID: 527
	[Flags]
	public enum RadDataFormRebindReason
	{
		// Token: 0x04000575 RID: 1397
		NotSpecified = 0,
		// Token: 0x04000576 RID: 1398
		InitialLoad = 1,
		// Token: 0x04000577 RID: 1399
		ExplicitRebind = 2,
		// Token: 0x04000578 RID: 1400
		PostBackEvent = 4,
		// Token: 0x04000579 RID: 1401
		PostbackViewStateNotPersisted = 8
	}
}
