using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal.ContractParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000123 RID: 291
	public class ServiceProviderOriginalMatchingReusableClientProxy : WCFTokenBasedReusableClientProxy<IServiceProviderOriginalMatching>, IServiceProviderOriginalMatching, IService
	{
		// Token: 0x06000B93 RID: 2963 RVA: 0x0001D35A File Offset: 0x0001B55A
		public ServiceProviderOriginalMatchingReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0001D365 File Offset: 0x0001B565
		public ServiceProviderOriginalMatchingReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0001D374 File Offset: 0x0001B574
		public LoadAssignmentsByProviderAndAssignedDateResp LoadAssignmentsByProviderAndAssignedDate(LoadAssignmentsByProviderAndAssignedDateReq Request)
		{
			return this.WrapServiceMethod<LoadAssignmentsByProviderAndAssignedDateResp>(() => this.Proxy.LoadAssignmentsByProviderAndAssignedDate(Request));
		}
	}
}
