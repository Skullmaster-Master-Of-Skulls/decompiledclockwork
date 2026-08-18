using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Timeline.GroupedByDate
{
	// Token: 0x0200084B RID: 2123
	internal class AppointmentFilter : AppointmentFilter
	{
		// Token: 0x06004E57 RID: 20055 RVA: 0x000F59C8 File Offset: 0x000F3BC8
		public override IList<Appointment> GetAppointments(DateTime timeSlotStart, DateTime timeSlotEnd, IList<TimeSlot> slots, AppointmentCollection allAppointments, IComparer<Appointment> comparer)
		{
			List<Appointment> list = new List<Appointment>(allAppointments.GetAppointmentsInRange(timeSlotStart, timeSlotEnd));
			list.Sort(comparer);
			return list;
		}
	}
}
