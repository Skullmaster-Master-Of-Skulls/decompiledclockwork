using System;

namespace TechnoPro.Common.Public.Entities
{
	// Token: 0x020000E7 RID: 231
	public class ClockWorkServerOperationContext : OperationContext
	{
		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x0000E4A5 File Offset: 0x0000C6A5
		// (set) Token: 0x06000557 RID: 1367 RVA: 0x0000E4AD File Offset: 0x0000C6AD
		public eClockWorkServerInstanceName ClockWorkServerInstanceName { get; set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x0000E4B6 File Offset: 0x0000C6B6
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x0000E4BE File Offset: 0x0000C6BE
		public string ClockWorkServerVirtualDirectory { get; set; }

		// Token: 0x0600055A RID: 1370 RVA: 0x0000E4C8 File Offset: 0x0000C6C8
		public ClockWorkServerOperationContext()
		{
			this.ClockWorkServerInstanceName = eClockWorkServerInstanceName.ClockWorkServer;
			this.ClockWorkServerVirtualDirectory = eClockWorkServerInstanceName.ClockWorkServer.ToString();
		}
	}
}
