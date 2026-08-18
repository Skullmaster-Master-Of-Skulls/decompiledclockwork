using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Week
{
	// Token: 0x02001A9B RID: 6811
	internal class AppointmentFilter
	{
		// Token: 0x06010796 RID: 67478 RVA: 0x003AEC4C File Offset: 0x003ACE4C
		public virtual IList<Appointment> GetAllDayAppointments(DateTime timeSlotStart, DateTime timeSlotEnd, IList<TimeSlot> allDaySlots, AppointmentCollection allAppointments, IComparer<Appointment> comparer)
		{
			List<Appointment> list = new List<Appointment>();
			foreach (TimeSlot timeSlot in allDaySlots)
			{
				list.AddRange(timeSlot.Appointments);
			}
			AppointmentCollection appointmentCollection = new AppointmentCollection(allAppointments.GetAppointmentsEnclosingRange(timeSlotStart, timeSlotEnd));
			appointmentCollection.Remove(list);
			appointmentCollection.Sort(comparer);
			return appointmentCollection.ToArray();
		}
	}
}
