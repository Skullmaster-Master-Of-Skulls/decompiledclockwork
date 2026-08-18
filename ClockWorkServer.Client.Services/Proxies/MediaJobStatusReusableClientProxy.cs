using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200000E RID: 14
	public class MediaJobStatusReusableClientProxy : WCFTokenBasedReusableClientProxy<IMediaJobStatus>, IMediaJobStatus, IService
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x000041AA File Offset: 0x000023AA
		public MediaJobStatusReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000041B5 File Offset: 0x000023B5
		public MediaJobStatusReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000041C4 File Offset: 0x000023C4
		public CreateMediaJobStatusResp CreateMediaJobStatus(CreateMediaJobStatusReq request)
		{
			return this.WrapServiceMethod<CreateMediaJobStatusResp>(() => this.Proxy.CreateMediaJobStatus(request));
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000041FC File Offset: 0x000023FC
		public GetMediaJobStatusByNameResp GetMediaJobStatusByName(GetMediaJobStatusByNameReq request)
		{
			return this.WrapServiceMethod<GetMediaJobStatusByNameResp>(() => this.Proxy.GetMediaJobStatusByName(request));
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004234 File Offset: 0x00002434
		public GetMediaJobStatusByGroupResp GetMediaJobStatusByGroup(GetMediaJobStatusByGroupReq request)
		{
			return this.WrapServiceMethod<GetMediaJobStatusByGroupResp>(() => this.Proxy.GetMediaJobStatusByGroup(request));
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000426C File Offset: 0x0000246C
		public GetAllMediaJobStatusResp GetAllMediaJobStatus(GetAllMediaJobStatusReq request)
		{
			return this.WrapServiceMethod<GetAllMediaJobStatusResp>(() => this.Proxy.GetAllMediaJobStatus(request));
		}
	}
}
