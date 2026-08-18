using System;

namespace TechnoPro.Common.Public.Entities.AvailabilitySchedule
{
	// Token: 0x0200047A RID: 1146
	public class AddAvailabilityActionResult
	{
		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x0600229A RID: 8858 RVA: 0x00026719 File Offset: 0x00024919
		// (set) Token: 0x0600229B RID: 8859 RVA: 0x00026721 File Offset: 0x00024921
		public AvailabilityScheduleItemActionResult Status { get; set; }

		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x0600229C RID: 8860 RVA: 0x0002672A File Offset: 0x0002492A
		// (set) Token: 0x0600229D RID: 8861 RVA: 0x00026732 File Offset: 0x00024932
		public DateTime Date { get; set; }

		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x0600229E RID: 8862 RVA: 0x0002673B File Offset: 0x0002493B
		// (set) Token: 0x0600229F RID: 8863 RVA: 0x00026743 File Offset: 0x00024943
		public AvailabilityScheduleTime Time { get; set; }
	}
}
