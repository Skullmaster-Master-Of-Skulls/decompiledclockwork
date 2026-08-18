using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000F95 RID: 3989
	public class ReminderSnoozeEventArgs : SchedulerEventArgs
	{
		// Token: 0x17003041 RID: 12353
		// (get) Token: 0x0600988E RID: 39054 RVA: 0x00221601 File Offset: 0x0021F801
		// (set) Token: 0x0600988F RID: 39055 RVA: 0x00221609 File Offset: 0x0021F809
		public Reminder Reminder { get; private set; }

		// Token: 0x17003042 RID: 12354
		// (get) Token: 0x06009890 RID: 39056 RVA: 0x00221612 File Offset: 0x0021F812
		// (set) Token: 0x06009891 RID: 39057 RVA: 0x0022161A File Offset: 0x0021F81A
		public int SnoozeMinutes { get; private set; }

		// Token: 0x06009892 RID: 39058 RVA: 0x00221623 File Offset: 0x0021F823
		public ReminderSnoozeEventArgs(Appointment appointment, Reminder reminder, int snoozeMinutes) : base(appointment)
		{
			this.Reminder = reminder;
			this.SnoozeMinutes = snoozeMinutes;
		}
	}
}
