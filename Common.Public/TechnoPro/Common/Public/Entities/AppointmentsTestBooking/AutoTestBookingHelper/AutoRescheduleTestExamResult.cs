using System;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000536 RID: 1334
	public class AutoRescheduleTestExamResult
	{
		// Token: 0x170011CD RID: 4557
		// (get) Token: 0x06002A60 RID: 10848 RVA: 0x0002BE6C File Offset: 0x0002A06C
		// (set) Token: 0x06002A61 RID: 10849 RVA: 0x0002BE74 File Offset: 0x0002A074
		public bool Successful { get; set; }

		// Token: 0x170011CE RID: 4558
		// (get) Token: 0x06002A62 RID: 10850 RVA: 0x0002BE7D File Offset: 0x0002A07D
		// (set) Token: 0x06002A63 RID: 10851 RVA: 0x0002BE85 File Offset: 0x0002A085
		public AutoBookTestExamPreviewResult PreviewResult { get; set; }
	}
}
