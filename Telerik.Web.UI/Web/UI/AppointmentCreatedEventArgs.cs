using System;

namespace Telerik.Web.UI
{
	// Token: 0x020012D3 RID: 4819
	public class AppointmentCreatedEventArgs : SchedulerEventArgs
	{
		// Token: 0x0600CA6E RID: 51822 RVA: 0x002D2B58 File Offset: 0x002D0D58
		public AppointmentCreatedEventArgs(Appointment appointment, SchedulerAppointmentContainer container) : base(appointment)
		{
			this._container = container;
		}

		// Token: 0x17004173 RID: 16755
		// (get) Token: 0x0600CA6F RID: 51823 RVA: 0x002D2B68 File Offset: 0x002D0D68
		public SchedulerAppointmentContainer Container
		{
			get
			{
				return this._container;
			}
		}

		// Token: 0x04003518 RID: 13592
		private SchedulerAppointmentContainer _container;
	}
}
