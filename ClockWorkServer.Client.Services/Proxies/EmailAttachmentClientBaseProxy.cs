using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Email;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000097 RID: 151
	internal class EmailAttachmentClientBaseProxy : ClientBase<IEmailAttachment>, IEmailAttachment, IService
	{
		// Token: 0x06000640 RID: 1600 RVA: 0x00011084 File Offset: 0x0000F284
		public EmailAttachmentClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0001108F File Offset: 0x0000F28F
		public EmailAttachmentClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0001109C File Offset: 0x0000F29C
		public LoadAttachmentResp LoadAttachment(LoadAttachmentReq Request)
		{
			return base.Channel.LoadAttachment(Request);
		}
	}
}
