using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal;
using TechnoPro.Common.ClientManager.ICore.ServiceProviderOriginal;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.ServiceProviderOriginal
{
	// Token: 0x0200001D RID: 29
	public class ServiceProviderOriginalMatchingRestClientManager : BearerTokenRestProxy<IServiceProviderOriginalMatchingClientManager>, IServiceProviderOriginalMatchingClientManager, IWebService
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x000045DD File Offset: 0x000027DD
		public ServiceProviderOriginalMatchingRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000045E7 File Offset: 0x000027E7
		public ServiceProviderOriginalMatchingRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000045F2 File Offset: 0x000027F2
		public IList<ServiceProviderAssignmentDTO> LoadAssignmentsByProviderAndAssignedDate(int ServiceProviderId, DateTime StartDate, DateTime EndDate)
		{
			return base.GetMany<ServiceProviderAssignmentDTO>(string.Format("serviceprovideroriginalmatching/assigments/providerid/{0}/range/{1}/{2}", ServiceProviderId, StartDate, EndDate), true);
		}
	}
}
