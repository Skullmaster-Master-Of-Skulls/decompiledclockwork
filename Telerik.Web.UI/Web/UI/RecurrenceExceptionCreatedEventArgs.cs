using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001A37 RID: 6711
	public class RecurrenceExceptionCreatedEventArgs : SchedulerCancelEventArgs
	{
		// Token: 0x06010485 RID: 66693 RVA: 0x003A387B File Offset: 0x003A1A7B
		public RecurrenceExceptionCreatedEventArgs(Appointment originalAppointment, Appointment exceptionAppointment, Appointment occurrenceAppointment) : base(originalAppointment)
		{
			this._exceptionAppointment = exceptionAppointment;
			this._occurrenceAppointment = occurrenceAppointment;
		}

		// Token: 0x17004EEC RID: 20204
		// (get) Token: 0x06010486 RID: 66694 RVA: 0x003A3892 File Offset: 0x003A1A92
		public Appointment ExceptionAppointment
		{
			get
			{
				return this._exceptionAppointment;
			}
		}

		// Token: 0x17004EED RID: 20205
		// (get) Token: 0x06010487 RID: 66695 RVA: 0x003A389A File Offset: 0x003A1A9A
		public override Appointment Appointment
		{
			get
			{
				return base.Appointment;
			}
		}

		// Token: 0x17004EEE RID: 20206
		// (get) Token: 0x06010488 RID: 66696 RVA: 0x003A38A2 File Offset: 0x003A1AA2
		public Appointment OccurrenceAppointment
		{
			get
			{
				return this._occurrenceAppointment;
			}
		}

		// Token: 0x04004957 RID: 18775
		private readonly Appointment _exceptionAppointment;

		// Token: 0x04004958 RID: 18776
		private readonly Appointment _occurrenceAppointment;
	}
}
