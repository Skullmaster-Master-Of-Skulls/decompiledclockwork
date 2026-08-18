using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200009D RID: 157
	public class FileStorageReusableClientProxy : WCFTokenBasedReusableClientProxy<IFileStorage>, IFileStorage, IService
	{
		// Token: 0x06000659 RID: 1625 RVA: 0x00011326 File Offset: 0x0000F526
		public FileStorageReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00011331 File Offset: 0x0000F531
		public FileStorageReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00011340 File Offset: 0x0000F540
		public GetFileResp GetFile(GetFileReq request)
		{
			return this.WrapServiceMethod<GetFileResp>(() => this.Proxy.GetFile(request));
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00011378 File Offset: 0x0000F578
		public SaveFileResp SaveFile(SaveFileReq request)
		{
			return this.WrapServiceMethod<SaveFileResp>(() => this.Proxy.SaveFile(request));
		}
	}
}
