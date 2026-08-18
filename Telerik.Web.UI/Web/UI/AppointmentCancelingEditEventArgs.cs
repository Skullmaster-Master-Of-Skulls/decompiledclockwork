using System;

namespace Telerik.Web.UI
{
	// Token: 0x020012D4 RID: 4820
	public class AppointmentCancelingEditEventArgs : SchedulerCancelEventArgs
	{
		// Token: 0x0600CA70 RID: 51824 RVA: 0x002D2B70 File Offset: 0x002D0D70
		public AppointmentCancelingEditEventArgs(Appointment appointment, SchedulerFormContainer container) : base(appointment)
		{
			this._container = container;
		}

		// Token: 0x17004174 RID: 16756
		// (get) Token: 0x0600CA71 RID: 51825 RVA: 0x002D2B80 File Offset: 0x002D0D80
		public SchedulerFormContainer Container
		{
			get
			{
				return this._container;
			}
		}

		// Token: 0x04003519 RID: 13593
		private SchedulerFormContainer _container;
	}
}
