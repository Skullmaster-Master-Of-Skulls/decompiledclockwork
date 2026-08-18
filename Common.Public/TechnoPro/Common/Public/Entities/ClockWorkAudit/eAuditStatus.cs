using System;

namespace TechnoPro.Common.Public.Entities.ClockWorkAudit
{
	// Token: 0x02000462 RID: 1122
	[Serializable]
	public enum eAuditStatus
	{
		// Token: 0x040019AD RID: 6573
		Pending,
		// Token: 0x040019AE RID: 6574
		CompletedSuccessful,
		// Token: 0x040019AF RID: 6575
		CompletedSuccessfulWithWarnings,
		// Token: 0x040019B0 RID: 6576
		Failed
	}
}
