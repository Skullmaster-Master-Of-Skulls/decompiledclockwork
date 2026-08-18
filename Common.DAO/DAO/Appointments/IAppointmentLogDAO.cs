using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.DAO.Appointments
{
	// Token: 0x020000A8 RID: 168
	public interface IAppointmentLogDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000458 RID: 1112
		void LogAppModificationsPreChangeCommitted(int AppointmentId);

		// Token: 0x06000459 RID: 1113
		void LogAppModifications(int AppointmentId, eHowModifiedCode howModifiedCode, eAppointmentModifiedItemType modifiedItems);
	}
}
