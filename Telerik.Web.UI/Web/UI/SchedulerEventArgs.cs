using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000F94 RID: 3988
	public class SchedulerEventArgs : EventArgs
	{
		// Token: 0x0600988C RID: 39052 RVA: 0x002215EA File Offset: 0x0021F7EA
		public SchedulerEventArgs(Appointment appointment)
		{
			this._appointment = appointment;
		}

		// Token: 0x17003040 RID: 12352
		// (get) Token: 0x0600988D RID: 39053 RVA: 0x002215F9 File Offset: 0x0021F7F9
		public Appointment Appointment
		{
			get
			{
				return this._appointment;
			}
		}

		// Token: 0x04002B8C RID: 11148
		private Appointment _appointment;
	}
}
