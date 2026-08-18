using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200009C RID: 156
	internal class FileStorageAsyncClientBaseProxy : ClientBase<IFileStorageAsync>, IFileStorageAsync, IFileStorage, IService
	{
		// Token: 0x06000653 RID: 1619 RVA: 0x00011290 File Offset: 0x0000F490
		public FileStorageAsyncClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0001129B File Offset: 0x0000F49B
		public FileStorageAsyncClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x000112A8 File Offset: 0x0000F4A8
		public GetFileResp GetFile(GetFileReq request)
		{
			return base.Channel.GetFile(request);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x000112C8 File Offset: 0x0000F4C8
		public SaveFileResp SaveFile(SaveFileReq request)
		{
			return base.Channel.SaveFile(request);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x000112E8 File Offset: 0x0000F4E8
		public IAsyncResult BeginGetFile(GetFileReq request, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginGetFile(request, callback, asyncState);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00011308 File Offset: 0x0000F508
		public GetFileResp EndGetFile(IAsyncResult result)
		{
			return base.Channel.EndGetFile(result);
		}
	}
}
