using System;
using System.Collections.Generic;

namespace TechnoPro.Common.ClientManager.Notifications.AppointmentNotifications
{
	// Token: 0x0200001E RID: 30
	public class CalendarRefreshRequiredEventArgs : EventArgs
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00003CCE File Offset: 0x00001ECE
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00003CD6 File Offset: 0x00001ED6
		public IList<BasicAppointmentInfo> AppointmentInfos { get; set; }
	}
}
