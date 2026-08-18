using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Updates;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200015B RID: 347
	public class UpdaterRequiredReusableClientProxy : WCFReusableClientProxy<IUpdaterRequired>, IUpdaterRequired, IService, IConnectivity
	{
		// Token: 0x06000D59 RID: 3417 RVA: 0x000211BA File Offset: 0x0001F3BA
		public UpdaterRequiredReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x000211C5 File Offset: 0x0001F3C5
		public UpdaterRequiredReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x000211D4 File Offset: 0x0001F3D4
		public UpdateRequiredResponse IsUpdateRequired(UpdateRequiredRequest updateRequiredRequest)
		{
			return this.WrapServiceMethod<UpdateRequiredResponse>(() => this.Proxy.IsUpdateRequired(updateRequiredRequest));
		}
	}
}
