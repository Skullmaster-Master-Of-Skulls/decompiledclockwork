using System;

namespace TechnoPro.Common.ClientManager.Notifications.Entities
{
	// Token: 0x02000018 RID: 24
	public class AppointmentMessageEventArgs
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000AD RID: 173 RVA: 0x000031E3 File Offset: 0x000013E3
		// (set) Token: 0x060000AE RID: 174 RVA: 0x000031EB File Offset: 0x000013EB
		public eMessageTypeCode MsgCode { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000031F4 File Offset: 0x000013F4
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x000031FC File Offset: 0x000013FC
		public MessageAppointmentsParameter Parameters { get; set; }
	}
}
