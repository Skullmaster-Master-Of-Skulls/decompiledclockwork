using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.Appointments
{
	// Token: 0x020004B9 RID: 1209
	public class FreeTimeSearchContext
	{
		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x06002484 RID: 9348 RVA: 0x00027A50 File Offset: 0x00025C50
		// (set) Token: 0x06002485 RID: 9349 RVA: 0x00027A58 File Offset: 0x00025C58
		public IList<int> PersonIds { get; set; }

		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x06002486 RID: 9350 RVA: 0x00027A61 File Offset: 0x00025C61
		// (set) Token: 0x06002487 RID: 9351 RVA: 0x00027A69 File Offset: 0x00025C69
		public eFreeTimeSearchMethod SearchMethod { get; set; }

		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x06002488 RID: 9352 RVA: 0x00027A72 File Offset: 0x00025C72
		// (set) Token: 0x06002489 RID: 9353 RVA: 0x00027A7A File Offset: 0x00025C7A
		public DateTime SearchStartDateTime { get; set; }

		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x0600248A RID: 9354 RVA: 0x00027A83 File Offset: 0x00025C83
		// (set) Token: 0x0600248B RID: 9355 RVA: 0x00027A8B File Offset: 0x00025C8B
		public TimeSpan SearchEnd { get; set; }

		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x0600248C RID: 9356 RVA: 0x00027A94 File Offset: 0x00025C94
		// (set) Token: 0x0600248D RID: 9357 RVA: 0x00027A9C File Offset: 0x00025C9C
		public TimeSpan SearchAppointmentDuration { get; set; }

		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x0600248E RID: 9358 RVA: 0x00027AA5 File Offset: 0x00025CA5
		// (set) Token: 0x0600248F RID: 9359 RVA: 0x00027AAD File Offset: 0x00025CAD
		public IList<FreeTimeSearchRecurringRule> RecurringRules { get; set; }
	}
}
