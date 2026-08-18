using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Notifications.MultiUserSimulatenousAccess;

namespace TechnoPro.Common.ClientManager.Notifications.MultiUserSimultaneousAccess
{
	// Token: 0x02000004 RID: 4
	public class MultiUserSimultaneousAccessArgs : EventArgs
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002904 File Offset: 0x00000B04
		// (set) Token: 0x06000027 RID: 39 RVA: 0x0000290C File Offset: 0x00000B0C
		public MultiAccessInfo MultiAccessInfo { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002915 File Offset: 0x00000B15
		// (set) Token: 0x06000029 RID: 41 RVA: 0x0000291D File Offset: 0x00000B1D
		public bool AlreadyEditing { get; set; }
	}
}
