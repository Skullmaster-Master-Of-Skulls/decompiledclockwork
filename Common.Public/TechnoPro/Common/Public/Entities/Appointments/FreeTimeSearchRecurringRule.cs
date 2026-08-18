using System;

namespace TechnoPro.Common.Public.Entities.Appointments
{
	// Token: 0x020004BA RID: 1210
	public class FreeTimeSearchRecurringRule
	{
		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x06002491 RID: 9361 RVA: 0x00027AB6 File Offset: 0x00025CB6
		// (set) Token: 0x06002492 RID: 9362 RVA: 0x00027ABE File Offset: 0x00025CBE
		public DayOfWeek DayOfWeek { get; set; }

		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x06002493 RID: 9363 RVA: 0x00027AC7 File Offset: 0x00025CC7
		// (set) Token: 0x06002494 RID: 9364 RVA: 0x00027ACF File Offset: 0x00025CCF
		public TimeSpan StartTime { get; set; }

		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x06002495 RID: 9365 RVA: 0x00027AD8 File Offset: 0x00025CD8
		// (set) Token: 0x06002496 RID: 9366 RVA: 0x00027AE0 File Offset: 0x00025CE0
		public TimeSpan EndTime { get; set; }
	}
}
