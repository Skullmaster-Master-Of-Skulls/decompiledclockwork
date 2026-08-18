using System;
using System.Collections.Generic;

namespace TechnoPro.Common.UI.Web.Entity.appt
{
	// Token: 0x02000043 RID: 67
	[Serializable]
	public class AppointmentBookingCalendarContext
	{
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00003E40 File Offset: 0x00002040
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00003E48 File Offset: 0x00002048
		public DateTime StartDate { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00003E51 File Offset: 0x00002051
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x00003E59 File Offset: 0x00002059
		public IList<string> UserTitles { get; set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00003E62 File Offset: 0x00002062
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x00003E6A File Offset: 0x0000206A
		public string CurrentChannel { get; set; }
	}
}
