using System;

namespace Telerik.Web.UI
{
	// Token: 0x020019C4 RID: 6596
	[Flags]
	public enum RadListViewRebindReason
	{
		// Token: 0x04004843 RID: 18499
		NotSpecified = 0,
		// Token: 0x04004844 RID: 18500
		InitialLoad = 1,
		// Token: 0x04004845 RID: 18501
		ExplicitRebind = 2,
		// Token: 0x04004846 RID: 18502
		PostBackEvent = 4,
		// Token: 0x04004847 RID: 18503
		PostbackViewStateNotPersisted = 8
	}
}
