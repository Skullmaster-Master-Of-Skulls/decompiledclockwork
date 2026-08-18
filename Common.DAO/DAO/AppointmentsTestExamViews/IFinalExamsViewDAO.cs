using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestExamViews.FinalExams;

namespace TechnoPro.Common.DAO.AppointmentsTestExamViews
{
	// Token: 0x020000B5 RID: 181
	public interface IFinalExamsViewDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004DC RID: 1244
		IList<FinalExamsViewLight> LoadFinalExamsLight(FinalExamsContext context);

		// Token: 0x060004DD RID: 1245
		IList<PotentialFinalExamBooking> LoadUnbookedFinalExams(DateTime startDate, DateTime endDate, bool requiresApprovedSelfReg, bool requiresUnexpiredAccommodations, bool requiresLoaGeneratedByStaff, int accommodationExpiryControlId);
	}
}
