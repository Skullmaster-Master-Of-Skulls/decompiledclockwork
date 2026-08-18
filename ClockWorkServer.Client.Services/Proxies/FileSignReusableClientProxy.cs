using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Storages;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000098 RID: 152
	public class FileSignReusableClientProxy : WCFTokenBasedReusableClientProxy<IFileSign>, IFileSign, IService
	{
		// Token: 0x06000643 RID: 1603 RVA: 0x000110BA File Offset: 0x0000F2BA
		public FileSignReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x000110C5 File Offset: 0x0000F2C5
		public FileSignReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x000110D4 File Offset: 0x0000F2D4
		public DecryptAndVerifyResp DecryptAndVerify(DecryptAndVerifyReq Request)
		{
			return this.WrapServiceMethod<DecryptAndVerifyResp>(() => this.Proxy.DecryptAndVerify(Request));
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0001110C File Offset: 0x0000F30C
		public void DecryptAndVerifyUsingFileSystem(DecryptAndVerifyUsingFileSystemReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DecryptAndVerifyUsingFileSystem(Request);
			});
		}
	}
}
