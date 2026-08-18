using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000F91 RID: 3985
	public class AppointmentInsertEventArgs : SchedulerCancelEventArgs
	{
		// Token: 0x1700303C RID: 12348
		// (get) Token: 0x06009881 RID: 39041 RVA: 0x0022156F File Offset: 0x0021F76F
		// (set) Token: 0x06009882 RID: 39042 RVA: 0x00221577 File Offset: 0x0021F777
		public ISchedulerInfo SchedulerInfo { get; set; }

		// Token: 0x06009883 RID: 39043 RVA: 0x00221580 File Offset: 0x0021F780
		public AppointmentInsertEventArgs(Appointment appointment, ISchedulerInfo schedulerInfo) : base(appointment)
		{
			this.SchedulerInfo = schedulerInfo;
		}
	}
}
