using System;

namespace Telerik.Web.UI
{
	// Token: 0x020012D0 RID: 4816
	public class SchedulerFormCreatedEventArgs : SchedulerEventArgs
	{
		// Token: 0x0600CA68 RID: 51816 RVA: 0x002D2B0F File Offset: 0x002D0D0F
		public SchedulerFormCreatedEventArgs(Appointment appointment, SchedulerFormContainer container) : base(appointment)
		{
			this._container = container;
		}

		// Token: 0x17004170 RID: 16752
		// (get) Token: 0x0600CA69 RID: 51817 RVA: 0x002D2B1F File Offset: 0x002D0D1F
		public SchedulerFormContainer Container
		{
			get
			{
				return this._container;
			}
		}

		// Token: 0x04003515 RID: 13589
		private SchedulerFormContainer _container;
	}
}
