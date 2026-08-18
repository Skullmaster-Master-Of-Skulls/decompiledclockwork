using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestExamViews;
using TechnoPro.Common.Core.AppointmentsTestExamViews;
using TechnoPro.Common.Core.Mappers.AppointmentsTestExamViews;
using TechnoPro.Common.ICore.AppointmentsTestExamViews;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsTestExamViews.FinalExams;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200001C RID: 28
	public class FinalExamsViewServiceManager : IFinalExamsView, IService
	{
		// Token: 0x0600014E RID: 334 RVA: 0x000071F0 File Offset: 0x000053F0
		public LoadFinalExamsLightResp LoadFinalExamsLight(LoadFinalExamsLightReq Request)
		{
			IFinalExamsViewManager finalExamsViewManager = new FinalExamsViewManager(Request.GetOperationContext());
			IFinalExamsViewManager finalExamsViewManager2 = finalExamsViewManager;
			FinalExamsContextDTO context = Request.Context;
			IList<FinalExamsViewLight> list = finalExamsViewManager2.LoadFinalExamsLight((context != null) ? context.ToDomainObject() : null);
			List<FinalExamsViewLightDTO> list2;
			if (list == null)
			{
				list2 = null;
			}
			else
			{
				list2 = (from g in list
				select g.ToDTO()).ToList<FinalExamsViewLightDTO>();
			}
			List<FinalExamsViewLightDTO> finalExams = list2;
			return new LoadFinalExamsLightResp
			{
				FinalExams = finalExams
			};
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00007264 File Offset: 0x00005464
		public LoadUnbookedFinalExamsResp LoadUnbookedFinalExams(LoadUnbookedFinalExamsReq Request)
		{
			IFinalExamsViewManager finalExamsViewManager = new FinalExamsViewManager(Request.GetOperationContext());
			IList<PotentialFinalExamBooking> list = finalExamsViewManager.LoadUnbookedFinalExams(Request.StartDate, Request.EndDate, Request.RequiresApprovedSelfReg, Request.RequiresUnexpiredAccommodations, Request.RequiresLoaGeneratedByStaff);
			LoadUnbookedFinalExamsResp loadUnbookedFinalExamsResp = new LoadUnbookedFinalExamsResp();
			IList<PotentialFinalExamBookingDTO> potentialFinalExams;
			if (list == null)
			{
				potentialFinalExams = null;
			}
			else
			{
				potentialFinalExams = (from g in list
				select g.ToDTO()).ToList<PotentialFinalExamBookingDTO>();
			}
			loadUnbookedFinalExamsResp.PotentialFinalExams = potentialFinalExams;
			return loadUnbookedFinalExamsResp;
		}
	}
}
