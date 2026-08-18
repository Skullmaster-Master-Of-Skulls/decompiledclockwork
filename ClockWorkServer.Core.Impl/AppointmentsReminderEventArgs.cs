using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.AppointmentsReminder;

namespace TechnoPro.ClockWorkServer.Core.Impl
{
	// Token: 0x02000004 RID: 4
	public class AppointmentsReminderEventArgs : EventArgs
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002C3D File Offset: 0x00000E3D
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002C45 File Offset: 0x00000E45
		public IList<AppointmentReminder> AppointmentsReminderList { get; set; }

		// Token: 0x0600002B RID: 43 RVA: 0x00002C4E File Offset: 0x00000E4E
		public AppointmentsReminderEventArgs(IList<AppointmentReminder> appsReminderList)
		{
			this.AppointmentsReminderList = appsReminderList;
		}
	}
}
