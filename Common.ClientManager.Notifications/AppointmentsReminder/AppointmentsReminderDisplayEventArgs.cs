using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.AppointmentsReminder;

namespace TechnoPro.Common.ClientManager.Notifications.AppointmentsReminder
{
	// Token: 0x0200001C RID: 28
	public class AppointmentsReminderDisplayEventArgs : EventArgs
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00003866 File Offset: 0x00001A66
		// (set) Token: 0x060000CE RID: 206 RVA: 0x0000386E File Offset: 0x00001A6E
		public IList<AppointmentReminder> DisplayAppointmentReminderList { get; set; }

		// Token: 0x060000CF RID: 207 RVA: 0x00003877 File Offset: 0x00001A77
		public AppointmentsReminderDisplayEventArgs(IList<AppointmentReminder> appReminderList)
		{
			this.DisplayAppointmentReminderList = appReminderList;
		}
	}
}
