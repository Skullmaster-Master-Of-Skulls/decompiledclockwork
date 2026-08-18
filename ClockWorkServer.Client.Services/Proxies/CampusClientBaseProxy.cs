using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200005A RID: 90
	internal class CampusClientBaseProxy : ClientBase<ICampus>, ICampus, IService
	{
		// Token: 0x06000425 RID: 1061 RVA: 0x0000C030 File Offset: 0x0000A230
		public CampusClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000C03B File Offset: 0x0000A23B
		public CampusClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000C048 File Offset: 0x0000A248
		public GetCampusListResp GetCampusList(GetCampusListReq request)
		{
			return base.Channel.GetCampusList(request);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000C068 File Offset: 0x0000A268
		public CreateCampusResp CreateCampus(CreateCampusReq request)
		{
			return base.Channel.CreateCampus(request);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000C088 File Offset: 0x0000A288
		public UpdateCampusResp UpdateCampus(UpdateCampusReq request)
		{
			return base.Channel.UpdateCampus(request);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000C0A8 File Offset: 0x0000A2A8
		public DeleteCampusResp DeleteCampus(DeleteCampusReq request)
		{
			return base.Channel.DeleteCampus(request);
		}
	}
}
