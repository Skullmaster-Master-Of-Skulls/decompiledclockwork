using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Azure.Storage;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000056 RID: 86
	internal class ClockWorkSasTokenProviderClientBaseProxy : ClientBase<IClockWorkSasTokenProvider>, IClockWorkSasTokenProvider, IService
	{
		// Token: 0x0600040E RID: 1038 RVA: 0x0000BD74 File Offset: 0x00009F74
		public ClockWorkSasTokenProviderClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000BD7F File Offset: 0x00009F7F
		public ClockWorkSasTokenProviderClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000BD8C File Offset: 0x00009F8C
		public GetContainerSasUriResp GetContainerSasUri(GetContainerSasUriReq request)
		{
			return base.Channel.GetContainerSasUri(request);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000BDAC File Offset: 0x00009FAC
		public GetBlobSasUriResp GetBlobSasUri(GetBlobSasUriReq request)
		{
			return base.Channel.GetBlobSasUri(request);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000BDCC File Offset: 0x00009FCC
		public GetUpdatingSystemClientPrivateContainerSasUriResp GetUpdatingSystemClientPrivateContainerSasUri(GetUpdatingSystemClientPrivateContainerSasUriReq request)
		{
			return base.Channel.GetUpdatingSystemClientPrivateContainerSasUri(request);
		}
	}
}
