using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsWorkshops;
using TechnoPro.Common.Public.Entities.Cases;

namespace TechnoPro.Common.Public.Entities.AppointmentsCalendar
{
	// Token: 0x0200055F RID: 1375
	public class Appointment : BaseExtendedAppointment
	{
		// Token: 0x06002C33 RID: 11315 RVA: 0x0003148D File Offset: 0x0002F68D
		public Appointment()
		{
			this.Icons = new List<AppointmentIcon>();
		}

		// Token: 0x17001287 RID: 4743
		// (get) Token: 0x06002C34 RID: 11316 RVA: 0x000314A3 File Offset: 0x0002F6A3
		// (set) Token: 0x06002C35 RID: 11317 RVA: 0x000314AB File Offset: 0x0002F6AB
		public IList<AppointmentIcon> Icons { get; set; }

		// Token: 0x17001288 RID: 4744
		// (get) Token: 0x06002C36 RID: 11318 RVA: 0x000314B4 File Offset: 0x0002F6B4
		// (set) Token: 0x06002C37 RID: 11319 RVA: 0x000314BC File Offset: 0x0002F6BC
		public CaseBase CaseInfo { get; set; }

		// Token: 0x17001289 RID: 4745
		// (get) Token: 0x06002C38 RID: 11320 RVA: 0x000314C5 File Offset: 0x0002F6C5
		// (set) Token: 0x06002C39 RID: 11321 RVA: 0x000314CD File Offset: 0x0002F6CD
		public BasicAppointmentTestExamInfo TestExamInfo { get; set; }

		// Token: 0x1700128A RID: 4746
		// (get) Token: 0x06002C3A RID: 11322 RVA: 0x000314D6 File Offset: 0x0002F6D6
		// (set) Token: 0x06002C3B RID: 11323 RVA: 0x000314DE File Offset: 0x0002F6DE
		public AppointmentWorkshopInfo WorkshopInfo { get; set; }
	}
}
