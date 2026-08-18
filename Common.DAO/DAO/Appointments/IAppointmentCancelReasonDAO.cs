using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.DAO.Appointments
{
	// Token: 0x020000A5 RID: 165
	public interface IAppointmentCancelReasonDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600044A RID: 1098
		IList<AppCancelReason> LoadAllCancelReasons();

		// Token: 0x0600044B RID: 1099
		AppCancelReason LoadCancelReasonById(int CancelReasonId);

		// Token: 0x0600044C RID: 1100
		void DeleteCancelReason(int CancelReasonId);

		// Token: 0x0600044D RID: 1101
		void UpdateCancelReason(AppCancelReason CancelReason);

		// Token: 0x0600044E RID: 1102
		int CreateCancelReason(AppCancelReason CancelReason);
	}
}
