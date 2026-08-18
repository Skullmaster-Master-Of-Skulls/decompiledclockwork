using System;

namespace TechnoPro.ClockWorkWeb.user.TutoringTutors
{
	// Token: 0x0200003E RID: 62
	public class AvailabilityScheduleDateAndTimeWrapper
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000B1DB File Offset: 0x000093DB
		// (set) Token: 0x0600018C RID: 396 RVA: 0x0000B1E3 File Offset: 0x000093E3
		public string Date { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0000B1EC File Offset: 0x000093EC
		// (set) Token: 0x0600018E RID: 398 RVA: 0x0000B1F4 File Offset: 0x000093F4
		public int StartMinutes { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0000B1FD File Offset: 0x000093FD
		// (set) Token: 0x06000190 RID: 400 RVA: 0x0000B205 File Offset: 0x00009405
		public int EndMinutes { get; set; }
	}
}
