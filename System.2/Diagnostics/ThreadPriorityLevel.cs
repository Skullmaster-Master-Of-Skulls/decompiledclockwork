using System;

namespace System.Diagnostics
{
	// Token: 0x02000508 RID: 1288
	public enum ThreadPriorityLevel
	{
		// Token: 0x040028E9 RID: 10473
		Idle = -15,
		// Token: 0x040028EA RID: 10474
		Lowest = -2,
		// Token: 0x040028EB RID: 10475
		BelowNormal,
		// Token: 0x040028EC RID: 10476
		Normal,
		// Token: 0x040028ED RID: 10477
		AboveNormal,
		// Token: 0x040028EE RID: 10478
		Highest,
		// Token: 0x040028EF RID: 10479
		TimeCritical = 15
	}
}
