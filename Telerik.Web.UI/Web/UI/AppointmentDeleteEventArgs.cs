using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000F92 RID: 3986
	public class AppointmentDeleteEventArgs : SchedulerCancelEventArgs
	{
		// Token: 0x1700303D RID: 12349
		// (get) Token: 0x06009884 RID: 39044 RVA: 0x00221590 File Offset: 0x0021F790
		// (set) Token: 0x06009885 RID: 39045 RVA: 0x00221598 File Offset: 0x0021F798
		public ISchedulerInfo SchedulerInfo { get; set; }

		// Token: 0x06009886 RID: 39046 RVA: 0x002215A1 File Offset: 0x0021F7A1
		public AppointmentDeleteEventArgs(Appointment appointment, ISchedulerInfo schedulerInfo) : base(appointment)
		{
			this.SchedulerInfo = schedulerInfo;
		}
	}
}
