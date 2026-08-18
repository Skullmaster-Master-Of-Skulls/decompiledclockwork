using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.ICore.Emailing
{
	// Token: 0x02000091 RID: 145
	public interface IEmailAttachmentManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000416 RID: 1046
		TPMailAttachment LoadAttachment(int FileId);

		// Token: 0x06000417 RID: 1047
		void DeleteAttachment(int FileId);

		// Token: 0x06000418 RID: 1048
		int CreateAttachment(TPMailAttachment attachment);

		// Token: 0x06000419 RID: 1049
		void UpdateAttachment(TPMailAttachment attachment);
	}
}
