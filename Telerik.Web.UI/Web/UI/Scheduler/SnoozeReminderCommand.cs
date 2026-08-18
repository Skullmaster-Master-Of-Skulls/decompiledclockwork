using System;

namespace Telerik.Web.UI.Scheduler
{
	// Token: 0x02000F8C RID: 3980
	internal class SnoozeReminderCommand : ReminderCommand
	{
		// Token: 0x17003033 RID: 12339
		// (get) Token: 0x06009867 RID: 39015 RVA: 0x0022135C File Offset: 0x0021F55C
		// (set) Token: 0x06009868 RID: 39016 RVA: 0x00221364 File Offset: 0x0021F564
		public int SnoozeMinutes { get; set; }

		// Token: 0x06009869 RID: 39017 RVA: 0x00221370 File Offset: 0x0021F570
		public override void Execute(ICallbackCommandContext context)
		{
			if (base.AppointmentID == null)
			{
				throw new InvalidOperationException("Missing or invalid appointment ID");
			}
			Appointment appointment = context.Appointments.FindByID(base.AppointmentID);
			if (appointment != null)
			{
				Reminder reminder = appointment.Reminders.FindByID(base.ReminderID);
				if (reminder != null)
				{
					context.OnReminderSnooze(new ReminderSnoozeEventArgs(appointment, reminder, this.SnoozeMinutes));
				}
			}
		}
	}
}
