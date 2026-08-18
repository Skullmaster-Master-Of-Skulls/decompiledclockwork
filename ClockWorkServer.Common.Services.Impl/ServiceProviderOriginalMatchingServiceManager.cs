using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal.ContractParameters;
using TechnoPro.Common.Core.Mappers.ServiceProvidersOriginal;
using TechnoPro.Common.Core.ServiceProvidersOriginal;
using TechnoPro.Common.ICore.ServiceProvidersOriginal;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000080 RID: 128
	public class ServiceProviderOriginalMatchingServiceManager : IServiceProviderOriginalMatching, IService
	{
		// Token: 0x060004BE RID: 1214 RVA: 0x00016904 File Offset: 0x00014B04
		public LoadAssignmentsByProviderAndAssignedDateResp LoadAssignmentsByProviderAndAssignedDate(LoadAssignmentsByProviderAndAssignedDateReq Request)
		{
			IServiceProviderOriginalMatchingManager serviceProviderOriginalMatchingManager = new ServiceProviderOriginalMatchingManager(Request.GetOperationContext());
			IList<ServiceProviderAssignment> list = serviceProviderOriginalMatchingManager.LoadAssignmentsByProviderAndAssignedDate(Request.ServiceProviderId, Request.StartDate, Request.EndDate);
			LoadAssignmentsByProviderAndAssignedDateResp loadAssignmentsByProviderAndAssignedDateResp = new LoadAssignmentsByProviderAndAssignedDateResp();
			IList<ServiceProviderAssignmentDTO> assignments;
			if (list != null)
			{
				assignments = list.ToList<ServiceProviderAssignment>().ConvertAll<ServiceProviderAssignmentDTO>((ServiceProviderAssignment g) => g.ToDTO());
			}
			else
			{
				assignments = null;
			}
			loadAssignmentsByProviderAndAssignedDateResp.Assignments = assignments;
			return loadAssignmentsByProviderAndAssignedDateResp;
		}
	}
}
