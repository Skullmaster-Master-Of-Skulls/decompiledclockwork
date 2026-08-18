using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestExamViews.FinalExams;

namespace TechnoPro.Common.ICore.AppointmentsTestExamViews
{
	// Token: 0x020000E7 RID: 231
	public interface IFinalExamsViewManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600074A RID: 1866
		IList<FinalExamsViewLight> LoadFinalExamsLight(FinalExamsContext context);

		// Token: 0x0600074B RID: 1867
		IList<PotentialFinalExamBooking> LoadUnbookedFinalExams(DateTime startDate, DateTime endDate, bool requiresApprovedSelfReg, bool requiresUnexpiredAccommodations, bool requiresLoaGeneratedByStaff);
	}
}
