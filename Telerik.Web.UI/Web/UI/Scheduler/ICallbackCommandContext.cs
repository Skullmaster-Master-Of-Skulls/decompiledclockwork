using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler
{
	// Token: 0x020007F0 RID: 2032
	internal interface ICallbackCommandContext
	{
		// Token: 0x170016D9 RID: 5849
		// (get) Token: 0x060046A6 RID: 18086
		AppointmentCollection Appointments { get; }

		// Token: 0x060046A7 RID: 18087
		Appointment PrepareToEdit(Appointment appointmentToEdit, bool editSeries);

		// Token: 0x060046A8 RID: 18088
		void DismissAppointmentReminder(Appointment appointmentToUpdate, Appointment originalAppointment);

		// Token: 0x060046A9 RID: 18089
		void OnReminderSnooze(ReminderSnoozeEventArgs args);

		// Token: 0x060046AA RID: 18090
		bool OnReminderDismiss(ReminderDismissEventArgs args);

		// Token: 0x060046AB RID: 18091
		void SlotAppointments(IList<Appointment> appointments);
	}
}
