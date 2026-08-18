using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Email;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000096 RID: 150
	public class EmailAttachmentReusableClientProxy : WCFTokenBasedReusableClientProxy<IEmailAttachment>, IEmailAttachment, IService
	{
		// Token: 0x0600063D RID: 1597 RVA: 0x00011032 File Offset: 0x0000F232
		public EmailAttachmentReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0001103D File Offset: 0x0000F23D
		public EmailAttachmentReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0001104C File Offset: 0x0000F24C
		public LoadAttachmentResp LoadAttachment(LoadAttachmentReq Request)
		{
			return this.WrapServiceMethod<LoadAttachmentResp>(() => this.Proxy.LoadAttachment(Request));
		}
	}
}
