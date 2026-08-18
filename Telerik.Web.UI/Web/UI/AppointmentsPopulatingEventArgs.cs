using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000F8F RID: 3983
	public class AppointmentsPopulatingEventArgs : CancelEventArgs
	{
		// Token: 0x1700303A RID: 12346
		// (get) Token: 0x0600987C RID: 39036 RVA: 0x00221538 File Offset: 0x0021F738
		// (set) Token: 0x0600987D RID: 39037 RVA: 0x00221540 File Offset: 0x0021F740
		public ISchedulerInfo SchedulerInfo { get; set; }

		// Token: 0x0600987E RID: 39038 RVA: 0x00221549 File Offset: 0x0021F749
		public AppointmentsPopulatingEventArgs(ISchedulerInfo schedulerInfo)
		{
			this.SchedulerInfo = schedulerInfo;
		}
	}
}
