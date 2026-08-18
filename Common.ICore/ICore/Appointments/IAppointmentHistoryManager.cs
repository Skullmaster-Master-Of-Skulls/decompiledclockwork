using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments.AppointmentHistory;

namespace TechnoPro.Common.ICore.Appointments
{
	// Token: 0x020000DE RID: 222
	public interface IAppointmentHistoryManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060006E2 RID: 1762
		IList<AppointmentChangeLogEntry> LoadAppointmentChangeLog(int appId);
	}
}
