using System;

namespace Telerik.Web.UI
{
	// Token: 0x020011AE RID: 4526
	[Flags]
	public enum GridRebindReason
	{
		// Token: 0x04003122 RID: 12578
		NotSpecified = 0,
		// Token: 0x04003123 RID: 12579
		InitialLoad = 1,
		// Token: 0x04003124 RID: 12580
		DetailTableBinding = 2,
		// Token: 0x04003125 RID: 12581
		ExplicitRebind = 4,
		// Token: 0x04003126 RID: 12582
		PostBackEvent = 8,
		// Token: 0x04003127 RID: 12583
		PostbackViewStateNotPersisted = 16
	}
}
