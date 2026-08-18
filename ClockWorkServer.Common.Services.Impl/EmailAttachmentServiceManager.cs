using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Email;
using TechnoPro.Common.Core.Emailing;
using TechnoPro.Common.Core.Mappers.TPMailMan;
using TechnoPro.Common.ICore.Emailing;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000044 RID: 68
	public class EmailAttachmentServiceManager : IEmailAttachment, IService
	{
		// Token: 0x060002A1 RID: 673 RVA: 0x0000D470 File Offset: 0x0000B670
		public LoadAttachmentResp LoadAttachment(LoadAttachmentReq Request)
		{
			IEmailAttachmentManager emailAttachmentManager = new EmailAttachmentManager(Request.GetOperationContext());
			TPMailAttachment tpmailAttachment = emailAttachmentManager.LoadAttachment(Request.FileAttachmentId);
			return new LoadAttachmentResp
			{
				Attachment = ((tpmailAttachment == null) ? null : tpmailAttachment.ToDTO())
			};
		}
	}
}
