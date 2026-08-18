using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AvailabilitySchedule;

namespace TechnoPro.Common.Public.Entities.AppointmentsCalendar
{
	// Token: 0x0200055A RID: 1370
	public class AppointmentsWithAvailabilityAndTimetable
	{
		// Token: 0x1700127E RID: 4734
		// (get) Token: 0x06002C1D RID: 11293 RVA: 0x000313DA File Offset: 0x0002F5DA
		// (set) Token: 0x06002C1E RID: 11294 RVA: 0x000313E2 File Offset: 0x0002F5E2
		public IList<Appointment> Appointments { get; set; }

		// Token: 0x1700127F RID: 4735
		// (get) Token: 0x06002C1F RID: 11295 RVA: 0x000313EB File Offset: 0x0002F5EB
		// (set) Token: 0x06002C20 RID: 11296 RVA: 0x000313F3 File Offset: 0x0002F5F3
		public IList<AvailabilityScheduleItemsForContext> AvailabilitySchedules { get; set; }

		// Token: 0x17001280 RID: 4736
		// (get) Token: 0x06002C21 RID: 11297 RVA: 0x000313FC File Offset: 0x0002F5FC
		// (set) Token: 0x06002C22 RID: 11298 RVA: 0x00031404 File Offset: 0x0002F604
		public IList<Holiday> Holidays { get; set; }

		// Token: 0x17001281 RID: 4737
		// (get) Token: 0x06002C23 RID: 11299 RVA: 0x0003140D File Offset: 0x0002F60D
		// (set) Token: 0x06002C24 RID: 11300 RVA: 0x00031415 File Offset: 0x0002F615
		public IDictionary<int, IList<AppointmentTimetableItem>> TimetableItems { get; set; }
	}
}
