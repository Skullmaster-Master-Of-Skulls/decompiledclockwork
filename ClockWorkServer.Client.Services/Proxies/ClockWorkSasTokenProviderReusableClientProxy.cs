using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Azure.Storage;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000055 RID: 85
	public class ClockWorkSasTokenProviderReusableClientProxy : WCFReusableClientProxy<IClockWorkSasTokenProvider>, IClockWorkSasTokenProvider, IService
	{
		// Token: 0x06000409 RID: 1033 RVA: 0x0000BCB4 File Offset: 0x00009EB4
		public ClockWorkSasTokenProviderReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000BCBF File Offset: 0x00009EBF
		public ClockWorkSasTokenProviderReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000BCCC File Offset: 0x00009ECC
		public GetContainerSasUriResp GetContainerSasUri(GetContainerSasUriReq request)
		{
			return this.WrapServiceMethod<GetContainerSasUriResp>(() => this.Proxy.GetContainerSasUri(request));
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000BD04 File Offset: 0x00009F04
		public GetBlobSasUriResp GetBlobSasUri(GetBlobSasUriReq request)
		{
			return this.WrapServiceMethod<GetBlobSasUriResp>(() => this.Proxy.GetBlobSasUri(request));
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000BD3C File Offset: 0x00009F3C
		public GetUpdatingSystemClientPrivateContainerSasUriResp GetUpdatingSystemClientPrivateContainerSasUri(GetUpdatingSystemClientPrivateContainerSasUriReq request)
		{
			return this.WrapServiceMethod<GetUpdatingSystemClientPrivateContainerSasUriResp>(() => this.Proxy.GetUpdatingSystemClientPrivateContainerSasUri(request));
		}
	}
}
