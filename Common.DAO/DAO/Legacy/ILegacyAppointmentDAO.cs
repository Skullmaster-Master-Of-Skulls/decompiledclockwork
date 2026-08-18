using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Legacy.Appointment;

namespace TechnoPro.Common.DAO.Legacy
{
	// Token: 0x02000060 RID: 96
	public interface ILegacyAppointmentDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600022D RID: 557
		IList<AppointmentModifiedHistoryItem> LoadAsAppointmentModifiedHistory(int AppointmentId);
	}
}
