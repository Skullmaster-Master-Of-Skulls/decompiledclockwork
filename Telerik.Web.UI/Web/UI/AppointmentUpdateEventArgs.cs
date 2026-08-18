using System;

namespace Telerik.Web.UI
{
	// Token: 0x020011BF RID: 4543
	public class AppointmentUpdateEventArgs : SchedulerCancelEventArgs
	{
		// Token: 0x17003C5D RID: 15453
		// (get) Token: 0x0600BAF9 RID: 47865 RVA: 0x00299538 File Offset: 0x00297738
		// (set) Token: 0x0600BAFA RID: 47866 RVA: 0x00299540 File Offset: 0x00297740
		public Appointment ModifiedAppointment { get; private set; }

		// Token: 0x17003C5E RID: 15454
		// (get) Token: 0x0600BAFB RID: 47867 RVA: 0x00299549 File Offset: 0x00297749
		// (set) Token: 0x0600BAFC RID: 47868 RVA: 0x00299551 File Offset: 0x00297751
		public ISchedulerInfo SchedulerInfo { get; set; }

		// Token: 0x0600BAFD RID: 47869 RVA: 0x0029955A File Offset: 0x0029775A
		public AppointmentUpdateEventArgs(Appointment originalAppointment, Appointment modifiedAppointment, ISchedulerInfo schedulerInfo) : base(originalAppointment)
		{
			this.ModifiedAppointment = modifiedAppointment;
			this.SchedulerInfo = schedulerInfo;
		}
	}
}
