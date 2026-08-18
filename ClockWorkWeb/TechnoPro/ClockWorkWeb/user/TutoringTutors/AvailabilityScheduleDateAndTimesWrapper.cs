using System;
using System.Collections.Generic;

namespace TechnoPro.ClockWorkWeb.user.TutoringTutors
{
	// Token: 0x0200003C RID: 60
	public class AvailabilityScheduleDateAndTimesWrapper
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600017A RID: 378 RVA: 0x0000B082 File Offset: 0x00009282
		// (set) Token: 0x0600017B RID: 379 RVA: 0x0000B08A File Offset: 0x0000928A
		public DateTime Date { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600017C RID: 380 RVA: 0x0000B093 File Offset: 0x00009293
		// (set) Token: 0x0600017D RID: 381 RVA: 0x0000B09B File Offset: 0x0000929B
		public IList<AvailabilityScheduleTimeWrapper> Times { get; set; }
	}
}
