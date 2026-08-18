using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal.ContractParameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.ServiceProviderOriginal;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.ServiceProviderOriginal
{
	// Token: 0x02000023 RID: 35
	public class ServiceProviderOriginalMatchingClientManager : IServiceProviderOriginalMatchingClientManager, IWebService
	{
		// Token: 0x06000113 RID: 275 RVA: 0x000063EC File Offset: 0x000045EC
		public IList<ServiceProviderAssignmentDTO> LoadAssignmentsByProviderAndAssignedDate(int ServiceProviderId, DateTime StartDate, DateTime EndDate)
		{
			LoadAssignmentsByProviderAndAssignedDateReq loadAssignmentsByProviderAndAssignedDateReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAssignmentsByProviderAndAssignedDateReq>();
			loadAssignmentsByProviderAndAssignedDateReq.ServiceProviderId = ServiceProviderId;
			loadAssignmentsByProviderAndAssignedDateReq.StartDate = StartDate;
			loadAssignmentsByProviderAndAssignedDateReq.EndDate = EndDate;
			return ClientServiceFactory.GetClientInstance<IServiceProviderOriginalMatching>().LoadAssignmentsByProviderAndAssignedDate(loadAssignmentsByProviderAndAssignedDateReq).Assignments;
		}
	}
}
