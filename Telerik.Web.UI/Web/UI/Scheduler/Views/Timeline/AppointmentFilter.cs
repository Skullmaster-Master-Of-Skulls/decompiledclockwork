using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Timeline
{
	// Token: 0x0200084A RID: 2122
	internal class AppointmentFilter
	{
		// Token: 0x06004E55 RID: 20053 RVA: 0x000F5948 File Offset: 0x000F3B48
		public virtual IList<Appointment> GetAppointments(DateTime timeSlotStart, DateTime timeSlotEnd, IList<TimeSlot> slots, AppointmentCollection allAppointments, IComparer<Appointment> comparer)
		{
			List<Appointment> list = new List<Appointment>();
			foreach (TimeSlot timeSlot in slots)
			{
				list.AddRange(timeSlot.Appointments);
			}
			AppointmentCollection appointmentCollection = new AppointmentCollection(allAppointments.GetAppointmentsInRange(timeSlotStart, timeSlotEnd));
			appointmentCollection.Remove(list);
			appointmentCollection.Sort(comparer);
			return appointmentCollection.ToArray();
		}
	}
}
