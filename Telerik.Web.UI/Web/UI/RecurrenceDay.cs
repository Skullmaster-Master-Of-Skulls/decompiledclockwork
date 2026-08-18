using System;

namespace Telerik.Web.UI
{
	// Token: 0x020012E7 RID: 4839
	[Flags]
	public enum RecurrenceDay
	{
		// Token: 0x0400354C RID: 13644
		None = 0,
		// Token: 0x0400354D RID: 13645
		Sunday = 1,
		// Token: 0x0400354E RID: 13646
		Monday = 2,
		// Token: 0x0400354F RID: 13647
		Tuesday = 4,
		// Token: 0x04003550 RID: 13648
		Wednesday = 8,
		// Token: 0x04003551 RID: 13649
		Thursday = 16,
		// Token: 0x04003552 RID: 13650
		Friday = 32,
		// Token: 0x04003553 RID: 13651
		Saturday = 64,
		// Token: 0x04003554 RID: 13652
		EveryDay = 127,
		// Token: 0x04003555 RID: 13653
		WeekDays = 62,
		// Token: 0x04003556 RID: 13654
		WeekendDays = 65
	}
}
