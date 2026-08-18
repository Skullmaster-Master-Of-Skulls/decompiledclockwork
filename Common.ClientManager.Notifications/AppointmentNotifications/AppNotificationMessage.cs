using System;
using System.Collections.Generic;

namespace TechnoPro.Common.ClientManager.Notifications.AppointmentNotifications
{
	// Token: 0x02000022 RID: 34
	public class AppNotificationMessage
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00003D78 File Offset: 0x00001F78
		// (set) Token: 0x060000FF RID: 255 RVA: 0x00003D80 File Offset: 0x00001F80
		public eAppNotificationMessageCode Code { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00003D89 File Offset: 0x00001F89
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00003D91 File Offset: 0x00001F91
		public IList<BasicAppointmentInfo> AppInfos { get; set; }
	}
}
