using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal.ContractParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000126 RID: 294
	internal class ServiceProviderOriginalProviderClientBaseProxy : ClientBase<IServiceProviderOriginalProvider>, IServiceProviderOriginalProvider, IService
	{
		// Token: 0x06000B9D RID: 2973 RVA: 0x0001D46C File Offset: 0x0001B66C
		public ServiceProviderOriginalProviderClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0001D477 File Offset: 0x0001B677
		public ServiceProviderOriginalProviderClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0001D484 File Offset: 0x0001B684
		public LoadProviderBaseByIdResp LoadProviderBaseById(LoadProviderBaseByIdReq Request)
		{
			return base.Channel.LoadProviderBaseById(Request);
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0001D4A4 File Offset: 0x0001B6A4
		public LoadProviderByIdResp LoadProviderById(LoadProviderByIdReq Request)
		{
			return base.Channel.LoadProviderById(Request);
		}
	}
}
