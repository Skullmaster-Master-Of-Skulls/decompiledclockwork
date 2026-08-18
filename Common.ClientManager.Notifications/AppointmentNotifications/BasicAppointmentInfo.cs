using System;
using System.Collections.Generic;

namespace TechnoPro.Common.ClientManager.Notifications.AppointmentNotifications
{
	// Token: 0x0200001F RID: 31
	public class BasicAppointmentInfo
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00003CDF File Offset: 0x00001EDF
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00003CE7 File Offset: 0x00001EE7
		public int AppointmentId { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00003CF0 File Offset: 0x00001EF0
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00003CF8 File Offset: 0x00001EF8
		public IList<int> AttendeePersonIds { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00003D01 File Offset: 0x00001F01
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00003D09 File Offset: 0x00001F09
		public DateTime StartDateTime { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00003D12 File Offset: 0x00001F12
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00003D1A File Offset: 0x00001F1A
		public DateTime EndDateTime { get; set; }
	}
}
