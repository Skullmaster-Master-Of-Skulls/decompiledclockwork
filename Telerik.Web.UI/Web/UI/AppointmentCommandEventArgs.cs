using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020012D2 RID: 4818
	public class AppointmentCommandEventArgs : CommandEventArgs
	{
		// Token: 0x0600CA6C RID: 51820 RVA: 0x002D2B3F File Offset: 0x002D0D3F
		public AppointmentCommandEventArgs(SchedulerAppointmentContainer container, string commandName, object commandArgument) : base(commandName, commandArgument)
		{
			this._container = container;
		}

		// Token: 0x17004172 RID: 16754
		// (get) Token: 0x0600CA6D RID: 51821 RVA: 0x002D2B50 File Offset: 0x002D0D50
		public SchedulerAppointmentContainer Container
		{
			get
			{
				return this._container;
			}
		}

		// Token: 0x04003517 RID: 13591
		private SchedulerAppointmentContainer _container;
	}
}
