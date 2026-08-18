using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200000F RID: 15
	internal class MediaJobStatusClientBaseProxy : ClientBase<IMediaJobStatus>, IMediaJobStatus, IService
	{
		// Token: 0x060000CC RID: 204 RVA: 0x000042A4 File Offset: 0x000024A4
		public MediaJobStatusClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000042AF File Offset: 0x000024AF
		public MediaJobStatusClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000042BC File Offset: 0x000024BC
		public CreateMediaJobStatusResp CreateMediaJobStatus(CreateMediaJobStatusReq request)
		{
			return base.Channel.CreateMediaJobStatus(request);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000042DC File Offset: 0x000024DC
		public GetMediaJobStatusByNameResp GetMediaJobStatusByName(GetMediaJobStatusByNameReq request)
		{
			return base.Channel.GetMediaJobStatusByName(request);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000042FC File Offset: 0x000024FC
		public GetMediaJobStatusByGroupResp GetMediaJobStatusByGroup(GetMediaJobStatusByGroupReq request)
		{
			return base.Channel.GetMediaJobStatusByGroup(request);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000431C File Offset: 0x0000251C
		public GetAllMediaJobStatusResp GetAllMediaJobStatus(GetAllMediaJobStatusReq request)
		{
			return base.Channel.GetAllMediaJobStatus(request);
		}
	}
}
