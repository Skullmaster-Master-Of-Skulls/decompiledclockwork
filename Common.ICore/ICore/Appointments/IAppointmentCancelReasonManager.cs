using System;
using System.Collections.Generic;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.ICore.Appointments
{
	// Token: 0x020000E1 RID: 225
	public interface IAppointmentCancelReasonManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060006FA RID: 1786
		Forest<AppCancelReasonOrGroup> LoadCancelReasons();

		// Token: 0x060006FB RID: 1787
		IList<AppCancelReason> LoadAllCancelReasons();

		// Token: 0x060006FC RID: 1788
		AppCancelReason LoadCancelReasonById(int CancelReasonId);

		// Token: 0x060006FD RID: 1789
		void DeleteCancelReason(int CancelReasonId);

		// Token: 0x060006FE RID: 1790
		void UpdateCancelReason(AppCancelReason CancelReason);

		// Token: 0x060006FF RID: 1791
		int CreateCancelReason(AppCancelReason CancelReason);
	}
}
