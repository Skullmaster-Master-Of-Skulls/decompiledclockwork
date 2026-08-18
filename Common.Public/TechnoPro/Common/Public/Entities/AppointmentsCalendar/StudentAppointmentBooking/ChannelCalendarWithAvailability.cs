using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.AppointmentsCalendar.StudentAppointmentBooking
{
	// Token: 0x02000562 RID: 1378
	public class ChannelCalendarWithAvailability
	{
		// Token: 0x17001292 RID: 4754
		// (get) Token: 0x06002C4C RID: 11340 RVA: 0x0003155E File Offset: 0x0002F75E
		// (set) Token: 0x06002C4D RID: 11341 RVA: 0x00031566 File Offset: 0x0002F766
		public string CalendarTitle { get; set; }

		// Token: 0x17001293 RID: 4755
		// (get) Token: 0x06002C4E RID: 11342 RVA: 0x0003156F File Offset: 0x0002F76F
		// (set) Token: 0x06002C4F RID: 11343 RVA: 0x00031577 File Offset: 0x0002F777
		public IList<AvailabilityForChannelCalendar> Availabilities { get; set; }
	}
}
