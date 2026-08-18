using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Email
{
	// Token: 0x02000058 RID: 88
	public interface IEmailAttachmentClientManager : IWebService
	{
		// Token: 0x0600029D RID: 669
		TPMailAttachmentDTO LoadAttachment(int FileAttachmentId);
	}
}
