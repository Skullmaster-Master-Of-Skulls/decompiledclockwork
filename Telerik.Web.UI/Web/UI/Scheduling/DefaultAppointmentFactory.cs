using System;

namespace Telerik.Web.UI.Scheduling
{
	// Token: 0x02001A29 RID: 6697
	internal class DefaultAppointmentFactory : IAppointmentFactory
	{
		// Token: 0x06010427 RID: 66599 RVA: 0x003A24B2 File Offset: 0x003A06B2
		public Appointment CreateAppointment()
		{
			return new Appointment();
		}
	}
}
