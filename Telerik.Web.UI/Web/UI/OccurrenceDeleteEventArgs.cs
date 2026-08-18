using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001A12 RID: 6674
	public class OccurrenceDeleteEventArgs : SchedulerCancelEventArgs
	{
		// Token: 0x0601025B RID: 66139 RVA: 0x0039FA44 File Offset: 0x0039DC44
		public OccurrenceDeleteEventArgs(Appointment originalAppointment, Appointment occurrenceAppointment) : base(originalAppointment)
		{
			this._occurrenceAppointment = occurrenceAppointment;
		}

		// Token: 0x17004DF4 RID: 19956
		// (get) Token: 0x0601025C RID: 66140 RVA: 0x0039FA54 File Offset: 0x0039DC54
		public override Appointment Appointment
		{
			get
			{
				return base.Appointment;
			}
		}

		// Token: 0x17004DF5 RID: 19957
		// (get) Token: 0x0601025D RID: 66141 RVA: 0x0039FA5C File Offset: 0x0039DC5C
		public Appointment OccurrenceAppointment
		{
			get
			{
				return this._occurrenceAppointment;
			}
		}

		// Token: 0x04004916 RID: 18710
		private readonly Appointment _occurrenceAppointment;
	}
}
