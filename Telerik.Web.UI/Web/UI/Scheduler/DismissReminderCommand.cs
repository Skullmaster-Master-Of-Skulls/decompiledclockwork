using System;

namespace Telerik.Web.UI.Scheduler
{
	// Token: 0x02000F8B RID: 3979
	internal class DismissReminderCommand : ReminderCommand
	{
		// Token: 0x06009865 RID: 39013 RVA: 0x002212D8 File Offset: 0x0021F4D8
		public override void Execute(ICallbackCommandContext context)
		{
			if (base.AppointmentID == null)
			{
				throw new InvalidOperationException("Missing or invalid appointment ID");
			}
			Appointment appointment = context.Appointments.FindByID(base.AppointmentID);
			if (appointment != null)
			{
				Appointment appointment2 = context.PrepareToEdit(appointment, false).Clone();
				Reminder reminder = appointment2.Reminders.FindByID(base.ReminderID);
				if (reminder != null)
				{
					appointment2.Reminders.Remove(reminder);
					if (context.OnReminderDismiss(new ReminderDismissEventArgs(appointment, reminder, appointment2)))
					{
						context.DismissAppointmentReminder(appointment2, appointment);
					}
				}
			}
		}
	}
}
