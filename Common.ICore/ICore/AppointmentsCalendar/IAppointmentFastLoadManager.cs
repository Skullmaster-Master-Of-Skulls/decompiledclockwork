using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.ICore.AppointmentsCalendar
{
	// Token: 0x020000E9 RID: 233
	public interface IAppointmentFastLoadManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600074E RID: 1870
		void RefreshAppointmentFastLoadTables();
	}
}
