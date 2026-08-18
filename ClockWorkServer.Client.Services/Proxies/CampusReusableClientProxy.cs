using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000059 RID: 89
	public class CampusReusableClientProxy : WCFTokenBasedReusableClientProxy<ICampus>, ICampus, IService
	{
		// Token: 0x0600041F RID: 1055 RVA: 0x0000BF38 File Offset: 0x0000A138
		public CampusReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000BF43 File Offset: 0x0000A143
		public CampusReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000BF50 File Offset: 0x0000A150
		public CreateCampusResp CreateCampus(CreateCampusReq request)
		{
			return this.WrapServiceMethod<CreateCampusResp>(() => this.Proxy.CreateCampus(request));
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000BF88 File Offset: 0x0000A188
		public DeleteCampusResp DeleteCampus(DeleteCampusReq request)
		{
			return this.WrapServiceMethod<DeleteCampusResp>(() => this.Proxy.DeleteCampus(request));
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000BFC0 File Offset: 0x0000A1C0
		public GetCampusListResp GetCampusList(GetCampusListReq request)
		{
			return this.WrapServiceMethod<GetCampusListResp>(() => this.Proxy.GetCampusList(request));
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000BFF8 File Offset: 0x0000A1F8
		public UpdateCampusResp UpdateCampus(UpdateCampusReq request)
		{
			return this.WrapServiceMethod<UpdateCampusResp>(() => this.Proxy.UpdateCampus(request));
		}
	}
}
