using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Updates;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200015C RID: 348
	internal class UpdaterRequiredClientBaseProxy : ClientBase<IUpdaterRequired>, IUpdaterRequired, IService, IConnectivity
	{
		// Token: 0x06000D5C RID: 3420 RVA: 0x0002120C File Offset: 0x0001F40C
		public UpdaterRequiredClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x00021217 File Offset: 0x0001F417
		public UpdaterRequiredClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x00021224 File Offset: 0x0001F424
		public UpdateRequiredResponse IsUpdateRequired(UpdateRequiredRequest updateRequiredRequest)
		{
			return base.Channel.IsUpdateRequired(updateRequiredRequest);
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x00021244 File Offset: 0x0001F444
		public int CheckConnectivity()
		{
			return base.Channel.CheckConnectivity();
		}
	}
}
