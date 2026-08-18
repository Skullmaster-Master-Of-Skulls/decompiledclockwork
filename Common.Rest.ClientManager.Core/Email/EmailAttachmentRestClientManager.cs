using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Email
{
	// Token: 0x0200004F RID: 79
	public class EmailAttachmentRestClientManager : BearerTokenRestProxy<IEmailAttachmentClientManager>, IEmailAttachmentClientManager, IWebService
	{
		// Token: 0x060002FD RID: 765 RVA: 0x00009070 File Offset: 0x00007270
		public EmailAttachmentRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000907A File Offset: 0x0000727A
		public EmailAttachmentRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00009085 File Offset: 0x00007285
		public TPMailAttachmentDTO LoadAttachment(int FileAttachmentId)
		{
			return base.Get<TPMailAttachmentDTO>(string.Format("emailattachment/fileattachmentid/{0}", FileAttachmentId), true);
		}
	}
}
