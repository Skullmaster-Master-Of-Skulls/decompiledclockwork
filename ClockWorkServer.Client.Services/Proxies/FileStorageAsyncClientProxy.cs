using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200009B RID: 155
	public class FileStorageAsyncClientProxy : WCFTokenBasedAsyncClientProxy<IFileStorageAsync>, IFileStorageAsync, IFileStorage, IService
	{
		// Token: 0x0600064D RID: 1613 RVA: 0x00011186 File Offset: 0x0000F386
		public FileStorageAsyncClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00011191 File Offset: 0x0000F391
		public FileStorageAsyncClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x000111A0 File Offset: 0x0000F3A0
		public GetFileResp GetFile(GetFileReq request)
		{
			return this.WrapServiceMethod<GetFileResp>(() => this.Proxy.GetFile(request));
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x000111D8 File Offset: 0x0000F3D8
		public SaveFileResp SaveFile(SaveFileReq request)
		{
			return this.WrapServiceMethod<SaveFileResp>(() => this.Proxy.SaveFile(request));
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00011210 File Offset: 0x0000F410
		public IAsyncResult BeginGetFile(GetFileReq request, AsyncCallback callback, object asyncState)
		{
			return this.WrapServiceMethod<IAsyncResult>(() => this.Proxy.BeginGetFile(request, callback, asyncState));
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00011258 File Offset: 0x0000F458
		public GetFileResp EndGetFile(IAsyncResult result)
		{
			return this.WrapServiceMethod<GetFileResp>(() => this.Proxy.EndGetFile(result));
		}
	}
}
