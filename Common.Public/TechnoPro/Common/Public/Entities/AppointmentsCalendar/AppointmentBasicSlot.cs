using System;

namespace TechnoPro.Common.Public.Entities.AppointmentsCalendar
{
	// Token: 0x02000559 RID: 1369
	public class AppointmentBasicSlot
	{
		// Token: 0x1700127B RID: 4731
		// (get) Token: 0x06002C16 RID: 11286 RVA: 0x000313A7 File Offset: 0x0002F5A7
		// (set) Token: 0x06002C17 RID: 11287 RVA: 0x000313AF File Offset: 0x0002F5AF
		public int AppointmentId { get; set; }

		// Token: 0x1700127C RID: 4732
		// (get) Token: 0x06002C18 RID: 11288 RVA: 0x000313B8 File Offset: 0x0002F5B8
		// (set) Token: 0x06002C19 RID: 11289 RVA: 0x000313C0 File Offset: 0x0002F5C0
		public DateTime StartDateTime { get; set; }

		// Token: 0x1700127D RID: 4733
		// (get) Token: 0x06002C1A RID: 11290 RVA: 0x000313C9 File Offset: 0x0002F5C9
		// (set) Token: 0x06002C1B RID: 11291 RVA: 0x000313D1 File Offset: 0x0002F5D1
		public DateTime EndDateTime { get; set; }
	}
}
