using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;

namespace TechnoPro.Common.ClientManager.ICore.AppointmentsCalendar
{
	// Token: 0x02000095 RID: 149
	public class DatesAndAppointmentsWithAvailabilityAndTimetable
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x00002069 File Offset: 0x00000269
		// (set) Token: 0x0600049A RID: 1178 RVA: 0x00002071 File Offset: 0x00000271
		public IList<DateTime> DateTimes { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x0000207A File Offset: 0x0000027A
		// (set) Token: 0x0600049C RID: 1180 RVA: 0x00002082 File Offset: 0x00000282
		public AppointmentsWithAvailabilityAndTimetableDTO AppsWithAvailabilityAndTimetable { get; set; }
	}
}
