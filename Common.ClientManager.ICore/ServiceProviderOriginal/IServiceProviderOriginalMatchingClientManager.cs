using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.ServiceProviderOriginal
{
	// Token: 0x02000020 RID: 32
	public interface IServiceProviderOriginalMatchingClientManager : IWebService
	{
		// Token: 0x060000CA RID: 202
		IList<ServiceProviderAssignmentDTO> LoadAssignmentsByProviderAndAssignedDate(int ServiceProviderId, DateTime StartDate, DateTime EndDate);
	}
}
