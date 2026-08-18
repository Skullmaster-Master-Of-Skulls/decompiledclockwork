using System;

namespace Telerik.Web.UI
{
	// Token: 0x020012D1 RID: 4817
	public class SchedulerFormCreatingEventArgs : SchedulerCancelEventArgs
	{
		// Token: 0x0600CA6A RID: 51818 RVA: 0x002D2B27 File Offset: 0x002D0D27
		public SchedulerFormCreatingEventArgs(Appointment appointment, SchedulerFormMode mode) : base(appointment)
		{
			this._formMode = mode;
		}

		// Token: 0x17004171 RID: 16753
		// (get) Token: 0x0600CA6B RID: 51819 RVA: 0x002D2B37 File Offset: 0x002D0D37
		public SchedulerFormMode Mode
		{
			get
			{
				return this._formMode;
			}
		}

		// Token: 0x04003516 RID: 13590
		private SchedulerFormMode _formMode;
	}
}
