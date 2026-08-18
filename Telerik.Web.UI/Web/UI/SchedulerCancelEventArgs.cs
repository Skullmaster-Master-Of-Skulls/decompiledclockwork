using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000F90 RID: 3984
	public class SchedulerCancelEventArgs : CancelEventArgs
	{
		// Token: 0x0600987F RID: 39039 RVA: 0x00221558 File Offset: 0x0021F758
		public SchedulerCancelEventArgs(Appointment appointment)
		{
			this._appointment = appointment;
		}

		// Token: 0x1700303B RID: 12347
		// (get) Token: 0x06009880 RID: 39040 RVA: 0x00221567 File Offset: 0x0021F767
		public virtual Appointment Appointment
		{
			get
			{
				return this._appointment;
			}
		}

		// Token: 0x04002B87 RID: 11143
		private readonly Appointment _appointment;
	}
}
