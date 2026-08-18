using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.Public.Entities.AppointmentsWorkshops
{
	// Token: 0x020004A9 RID: 1193
	public class WorkshopAppointment : BaseExtendedAppointment
	{
		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x060023EA RID: 9194 RVA: 0x000273B0 File Offset: 0x000255B0
		// (set) Token: 0x060023EB RID: 9195 RVA: 0x000273B8 File Offset: 0x000255B8
		public IList<AppointmentIcon> Icons { get; set; }

		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x060023EC RID: 9196 RVA: 0x000273C1 File Offset: 0x000255C1
		// (set) Token: 0x060023ED RID: 9197 RVA: 0x000273C9 File Offset: 0x000255C9
		public virtual int MaxAttendeeCount { get; set; }

		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x060023EE RID: 9198 RVA: 0x000273D2 File Offset: 0x000255D2
		// (set) Token: 0x060023EF RID: 9199 RVA: 0x000273DA File Offset: 0x000255DA
		public WorkshopDefinition WorkshopDefinition { get; set; }
	}
}
