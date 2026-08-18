using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000DFE RID: 3582
	[Flags]
	public enum PivotGridRebindReason
	{
		// Token: 0x04002518 RID: 9496
		NotSpecified = 0,
		// Token: 0x04002519 RID: 9497
		InitialLoad = 1,
		// Token: 0x0400251A RID: 9498
		ExplicitRebind = 2,
		// Token: 0x0400251B RID: 9499
		PostBackEvent = 4,
		// Token: 0x0400251C RID: 9500
		PostbackViewStateNotPersisted = 8
	}
}
