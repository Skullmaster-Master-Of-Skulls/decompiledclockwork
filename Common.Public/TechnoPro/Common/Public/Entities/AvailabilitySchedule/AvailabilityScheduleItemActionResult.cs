using System;

namespace TechnoPro.Common.Public.Entities.AvailabilitySchedule
{
	// Token: 0x0200047C RID: 1148
	public class AvailabilityScheduleItemActionResult
	{
		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x060022A9 RID: 8873 RVA: 0x000267E4 File Offset: 0x000249E4
		// (set) Token: 0x060022AA RID: 8874 RVA: 0x000267EC File Offset: 0x000249EC
		public eAvailabilityScheduleAction ActionTaken { get; set; }

		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x060022AB RID: 8875 RVA: 0x000267F5 File Offset: 0x000249F5
		// (set) Token: 0x060022AC RID: 8876 RVA: 0x000267FD File Offset: 0x000249FD
		public eAvailabilityScheduleActionFailureReason FailureReason { get; set; }

		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x060022AD RID: 8877 RVA: 0x00026806 File Offset: 0x00024A06
		// (set) Token: 0x060022AE RID: 8878 RVA: 0x0002680E File Offset: 0x00024A0E
		public string PublicMessage { get; set; }
	}
}
