using System;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestExamViews.TestBookings
{
	// Token: 0x020004F4 RID: 1268
	public class TestBookingsViewContext
	{
		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x0600265E RID: 9822 RVA: 0x00028F2E File Offset: 0x0002712E
		// (set) Token: 0x0600265F RID: 9823 RVA: 0x00028F36 File Offset: 0x00027136
		public DateTime StartDate { get; set; }

		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x06002660 RID: 9824 RVA: 0x00028F3F File Offset: 0x0002713F
		// (set) Token: 0x06002661 RID: 9825 RVA: 0x00028F47 File Offset: 0x00027147
		public DateTime EndDate { get; set; }

		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x06002662 RID: 9826 RVA: 0x00028F50 File Offset: 0x00027150
		// (set) Token: 0x06002663 RID: 9827 RVA: 0x00028F58 File Offset: 0x00027158
		public bool HideCancelled { get; set; }
	}
}
