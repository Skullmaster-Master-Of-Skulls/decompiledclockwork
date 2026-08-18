using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Veteran;
using TechnoPro.Common.Core.Mappers.Veteran;
using TechnoPro.Common.Core.Veteran;
using TechnoPro.Common.ICore.Veteran;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Veteran;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x020000A0 RID: 160
	public class VeteranServiceManager : IVeteran, IService
	{
		// Token: 0x060005DA RID: 1498 RVA: 0x0001B1D4 File Offset: 0x000193D4
		public LoadChangeInBenefitRequestsResp LoadChangeInBenefitRequests(LoadChangeInBenefitRequestsReq Request)
		{
			IVeteranManager veteranManager = new VeteranManager(new OperationContext
			{
				WhoAmI = Request.WhoAmI
			});
			IList<ChangeInBenefitRequest> list = veteranManager.LoadChangeInBenefitRequests(Request.PersonId, Request.StartDate, Request.EndDate);
			LoadChangeInBenefitRequestsResp loadChangeInBenefitRequestsResp = new LoadChangeInBenefitRequestsResp();
			IList<ChangeInBenefitRequestDTO> changeInBenefitRequests;
			if (list == null)
			{
				changeInBenefitRequests = null;
			}
			else
			{
				changeInBenefitRequests = (from g in list
				select g.ToDTO()).ToList<ChangeInBenefitRequestDTO>();
			}
			loadChangeInBenefitRequestsResp.ChangeInBenefitRequests = changeInBenefitRequests;
			return loadChangeInBenefitRequestsResp;
		}
	}
}
