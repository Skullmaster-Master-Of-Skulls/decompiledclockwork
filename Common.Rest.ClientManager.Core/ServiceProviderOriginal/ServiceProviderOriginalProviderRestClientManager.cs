using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal;
using TechnoPro.Common.ClientManager.ICore.ServiceProviderOriginal;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.ServiceProviderOriginal
{
	// Token: 0x0200001E RID: 30
	public class ServiceProviderOriginalProviderRestClientManager : BearerTokenRestProxy<IServiceProviderOriginalProviderClientManager>, IServiceProviderOriginalProviderClientManager, IWebService
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x00004617 File Offset: 0x00002817
		public ServiceProviderOriginalProviderRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004621 File Offset: 0x00002821
		public ServiceProviderOriginalProviderRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000462C File Offset: 0x0000282C
		public ServiceProviderDTO LoadProviderById(int ServiceProviderId)
		{
			return base.Get<ServiceProviderDTO>(string.Format("serviceprovideroriginalprovider/providerid/{0}", ServiceProviderId), true);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004645 File Offset: 0x00002845
		public ServiceProviderBaseDTO LoadProviderBaseById(int ServiceProviderId)
		{
			return base.Get<ServiceProviderBaseDTO>(string.Format("serviceprovideroriginalprovider/baseproviderid/{0}", ServiceProviderId), true);
		}
	}
}
