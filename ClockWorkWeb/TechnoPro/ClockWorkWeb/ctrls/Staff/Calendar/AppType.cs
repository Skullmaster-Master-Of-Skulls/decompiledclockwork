using System;

namespace TechnoPro.ClockWorkWeb.ctrls.Staff.Calendar
{
	// Token: 0x0200013D RID: 317
	public class AppType
	{
		// Token: 0x0600098F RID: 2447 RVA: 0x0000AF9E File Offset: 0x0000919E
		public AppType()
		{
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x00043A06 File Offset: 0x00041C06
		public AppType(int appTypeId, string title)
		{
			this.AppTypeId = appTypeId;
			this.Title = title;
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x00043A20 File Offset: 0x00041C20
		// (set) Token: 0x06000992 RID: 2450 RVA: 0x00043A28 File Offset: 0x00041C28
		public int AppTypeId { get; set; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x00043A31 File Offset: 0x00041C31
		// (set) Token: 0x06000994 RID: 2452 RVA: 0x00043A39 File Offset: 0x00041C39
		public string Title { get; set; }
	}
}
