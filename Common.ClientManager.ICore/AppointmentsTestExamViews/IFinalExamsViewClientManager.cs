using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestExamViews;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.AppointmentsTestExamViews
{
	// Token: 0x02000083 RID: 131
	public interface IFinalExamsViewClientManager : IWebService
	{
		// Token: 0x060003D8 RID: 984
		IList<FinalExamsViewLightDTO> LoadFinalExamsLight(FinalExamsContextDTO context);

		// Token: 0x060003D9 RID: 985
		IList<PotentialFinalExamBookingDTO> LoadUnbookedFinalExams(DateTime startDate, DateTime endDate, bool requiresApprovedSelfReg, bool requiresUnexpiredAccommodations, bool requiresLoaGeneratedByStaff);
	}
}
