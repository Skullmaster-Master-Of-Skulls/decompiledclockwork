using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments.AppointmentHistory;

namespace TechnoPro.Common.DAO.Appointments
{
	// Token: 0x020000A6 RID: 166
	public interface IAppointmentHistoryDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600044F RID: 1103
		IList<AppointmentRawHistoryItem> LoadAppointmentRawHistoryItems(int appId);
	}
}
