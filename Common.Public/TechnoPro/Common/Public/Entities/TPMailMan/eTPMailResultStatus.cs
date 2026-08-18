using System;

namespace TechnoPro.Common.Public.Entities.TPMailMan
{
	// Token: 0x02000168 RID: 360
	public enum eTPMailResultStatus
	{
		// Token: 0x040006AE RID: 1710
		Unknown,
		// Token: 0x040006AF RID: 1711
		Pending,
		// Token: 0x040006B0 RID: 1712
		CompletedSuccess,
		// Token: 0x040006B1 RID: 1713
		CompletedWithWarnings,
		// Token: 0x040006B2 RID: 1714
		Failed,
		// Token: 0x040006B3 RID: 1715
		NotSentBecauseTemplateIsDisabled,
		// Token: 0x040006B4 RID: 1716
		NotSentBecausePreviewMode,
		// Token: 0x040006B5 RID: 1717
		NotSentBecauseSendFirstEmailOnly
	}
}
