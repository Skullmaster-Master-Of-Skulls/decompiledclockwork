using System;

namespace TechnoPro.Common.Public.Entities.AvailabilitySchedule
{
	// Token: 0x02000480 RID: 1152
	public class DeleteAvailabilityActionResult
	{
		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x060022C3 RID: 8899 RVA: 0x00026944 File Offset: 0x00024B44
		// (set) Token: 0x060022C4 RID: 8900 RVA: 0x0002694C File Offset: 0x00024B4C
		public AvailabilityScheduleItemActionResult Status { get; set; }

		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x060022C5 RID: 8901 RVA: 0x00026955 File Offset: 0x00024B55
		// (set) Token: 0x060022C6 RID: 8902 RVA: 0x0002695D File Offset: 0x00024B5D
		public DateTime Date { get; set; }

		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x060022C7 RID: 8903 RVA: 0x00026966 File Offset: 0x00024B66
		// (set) Token: 0x060022C8 RID: 8904 RVA: 0x0002696E File Offset: 0x00024B6E
		public AvailabilityScheduleTime Time { get; set; }
	}
}
