using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Week.GroupedByDate
{
	// Token: 0x02001A9E RID: 6814
	internal class AppointmentFilter : AppointmentFilter
	{
		// Token: 0x0601079E RID: 67486 RVA: 0x003AED1C File Offset: 0x003ACF1C
		public override IList<Appointment> GetAllDayAppointments(DateTime timeSlotStart, DateTime timeSlotEnd, IList<TimeSlot> allDaySlots, AppointmentCollection allAppointments, IComparer<Appointment> comparer)
		{
			List<Appointment> list = new List<Appointment>(allAppointments.GetAppointmentsEnclosingRange(timeSlotStart, timeSlotEnd));
			list.Sort(comparer);
			return list;
		}
	}
}
