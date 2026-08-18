using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200009E RID: 158
	internal class FileStorageClientBaseProxy : ClientBase<IFileStorage>, IFileStorage, IService
	{
		// Token: 0x0600065D RID: 1629 RVA: 0x000113B0 File Offset: 0x0000F5B0
		public FileStorageClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x000113BB File Offset: 0x0000F5BB
		public FileStorageClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x000113C8 File Offset: 0x0000F5C8
		public GetFileResp GetFile(GetFileReq request)
		{
			return base.Channel.GetFile(request);
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x000113E8 File Offset: 0x0000F5E8
		public SaveFileResp SaveFile(SaveFileReq request)
		{
			return base.Channel.SaveFile(request);
		}
	}
}
