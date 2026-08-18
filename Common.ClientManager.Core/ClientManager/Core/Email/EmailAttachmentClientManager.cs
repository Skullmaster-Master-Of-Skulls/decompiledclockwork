using System;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Email;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Email
{
	// Token: 0x0200005F RID: 95
	public class EmailAttachmentClientManager : IEmailAttachmentClientManager, IWebService
	{
		// Token: 0x06000370 RID: 880 RVA: 0x0000EFC8 File Offset: 0x0000D1C8
		public TPMailAttachmentDTO LoadAttachment(int FileAttachmentId)
		{
			LoadAttachmentReq loadAttachmentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAttachmentReq>();
			loadAttachmentReq.FileAttachmentId = FileAttachmentId;
			return ClientServiceFactory.GetClientInstance<IEmailAttachment>().LoadAttachment(loadAttachmentReq).Attachment;
		}
	}
}
