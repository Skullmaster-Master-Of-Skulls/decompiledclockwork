using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000F93 RID: 3987
	public class ReminderDismissEventArgs : SchedulerCancelEventArgs
	{
		// Token: 0x1700303E RID: 12350
		// (get) Token: 0x06009887 RID: 39047 RVA: 0x002215B1 File Offset: 0x0021F7B1
		// (set) Token: 0x06009888 RID: 39048 RVA: 0x002215B9 File Offset: 0x0021F7B9
		public Reminder Reminder { get; private set; }

		// Token: 0x1700303F RID: 12351
		// (get) Token: 0x06009889 RID: 39049 RVA: 0x002215C2 File Offset: 0x0021F7C2
		// (set) Token: 0x0600988A RID: 39050 RVA: 0x002215CA File Offset: 0x0021F7CA
		public Appointment ModifiedAppointment { get; private set; }

		// Token: 0x0600988B RID: 39051 RVA: 0x002215D3 File Offset: 0x0021F7D3
		public ReminderDismissEventArgs(Appointment appointment, Reminder reminder, Appointment modifiedAppointment) : base(appointment)
		{
			this.Reminder = reminder;
			this.ModifiedAppointment = modifiedAppointment;
		}
	}
}
