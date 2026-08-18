using System;

namespace TechnoPro.Common.ClientManager.Notifications.AppointmentNotifications
{
	// Token: 0x02000020 RID: 32
	public class AppointmentDoubleBookingEventArgs : EventArgs
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00003D23 File Offset: 0x00001F23
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00003D2B File Offset: 0x00001F2B
		public int PersonId { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00003D34 File Offset: 0x00001F34
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00003D3C File Offset: 0x00001F3C
		public DateTime StartDateTime { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00003D45 File Offset: 0x00001F45
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00003D4D File Offset: 0x00001F4D
		public DateTime EndDateTime { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00003D56 File Offset: 0x00001F56
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00003D5E File Offset: 0x00001F5E
		public string Guid { get; set; }
	}
}
