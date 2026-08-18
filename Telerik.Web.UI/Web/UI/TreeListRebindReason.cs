using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001277 RID: 4727
	[Flags]
	public enum TreeListRebindReason
	{
		// Token: 0x04003422 RID: 13346
		NotSpecified = 0,
		// Token: 0x04003423 RID: 13347
		InitialLoad = 1,
		// Token: 0x04003424 RID: 13348
		ExplicitRebind = 2,
		// Token: 0x04003425 RID: 13349
		PostBackEvent = 4,
		// Token: 0x04003426 RID: 13350
		PostbackViewStateNotPersisted = 8
	}
}
