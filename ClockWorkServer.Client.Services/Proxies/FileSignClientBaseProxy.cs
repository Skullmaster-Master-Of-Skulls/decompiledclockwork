using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Storages;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000099 RID: 153
	internal class FileSignClientBaseProxy : ClientBase<IFileSign>, IFileSign, IService
	{
		// Token: 0x06000647 RID: 1607 RVA: 0x00011141 File Offset: 0x0000F341
		public FileSignClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0001114C File Offset: 0x0000F34C
		public FileSignClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00011158 File Offset: 0x0000F358
		public DecryptAndVerifyResp DecryptAndVerify(DecryptAndVerifyReq Request)
		{
			return base.Channel.DecryptAndVerify(Request);
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00011176 File Offset: 0x0000F376
		public void DecryptAndVerifyUsingFileSystem(DecryptAndVerifyUsingFileSystemReq Request)
		{
			base.Channel.DecryptAndVerifyUsingFileSystem(Request);
		}
	}
}
