using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal.ContractParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000124 RID: 292
	internal class ServiceProviderOriginalMatchingClientBaseProxy : ClientBase<IServiceProviderOriginalMatching>, IServiceProviderOriginalMatching, IService
	{
		// Token: 0x06000B96 RID: 2966 RVA: 0x0001D3AC File Offset: 0x0001B5AC
		public ServiceProviderOriginalMatchingClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0001D3B7 File Offset: 0x0001B5B7
		public ServiceProviderOriginalMatchingClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0001D3C4 File Offset: 0x0001B5C4
		public LoadAssignmentsByProviderAndAssignedDateResp LoadAssignmentsByProviderAndAssignedDate(LoadAssignmentsByProviderAndAssignedDateReq Request)
		{
			return base.Channel.LoadAssignmentsByProviderAndAssignedDate(Request);
		}
	}
}
