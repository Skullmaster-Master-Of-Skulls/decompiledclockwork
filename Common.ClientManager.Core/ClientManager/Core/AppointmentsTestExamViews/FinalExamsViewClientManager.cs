using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestExamViews;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AppointmentsTestExamViews;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.AppointmentsTestExamViews
{
	// Token: 0x02000089 RID: 137
	public class FinalExamsViewClientManager : IFinalExamsViewClientManager, IWebService
	{
		// Token: 0x060004EC RID: 1260 RVA: 0x00016190 File Offset: 0x00014390
		public IList<FinalExamsViewLightDTO> LoadFinalExamsLight(FinalExamsContextDTO context)
		{
			LoadFinalExamsLightReq loadFinalExamsLightReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadFinalExamsLightReq>();
			loadFinalExamsLightReq.Context = context;
			LoadFinalExamsLightResp loadFinalExamsLightResp = ClientServiceFactory.GetClientInstance<IFinalExamsView>().LoadFinalExamsLight(loadFinalExamsLightReq);
			return (loadFinalExamsLightResp != null) ? loadFinalExamsLightResp.FinalExams : null;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x000161CC File Offset: 0x000143CC
		public IList<PotentialFinalExamBookingDTO> LoadUnbookedFinalExams(DateTime startDate, DateTime endDate, bool requiresApprovedSelfReg, bool requiresUnexpiredAccommodations, bool requiresLoaGeneratedByStaff)
		{
			LoadUnbookedFinalExamsReq loadUnbookedFinalExamsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadUnbookedFinalExamsReq>();
			loadUnbookedFinalExamsReq.StartDate = startDate;
			loadUnbookedFinalExamsReq.EndDate = endDate;
			loadUnbookedFinalExamsReq.RequiresApprovedSelfReg = requiresApprovedSelfReg;
			loadUnbookedFinalExamsReq.RequiresUnexpiredAccommodations = requiresUnexpiredAccommodations;
			loadUnbookedFinalExamsReq.RequiresLoaGeneratedByStaff = requiresLoaGeneratedByStaff;
			LoadUnbookedFinalExamsResp loadUnbookedFinalExamsResp = ClientServiceFactory.GetClientInstance<IFinalExamsView>().LoadUnbookedFinalExams(loadUnbookedFinalExamsReq);
			return (loadUnbookedFinalExamsResp != null) ? loadUnbookedFinalExamsResp.PotentialFinalExams : null;
		}
	}
}
