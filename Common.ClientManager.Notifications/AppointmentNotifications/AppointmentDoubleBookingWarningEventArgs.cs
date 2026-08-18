using System;

namespace TechnoPro.Common.ClientManager.Notifications.AppointmentNotifications
{
	// Token: 0x02000021 RID: 33
	public class AppointmentDoubleBookingWarningEventArgs : EventArgs
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00003D67 File Offset: 0x00001F67
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00003D6F File Offset: 0x00001F6F
		public string Guid { get; set; }
	}
}
