using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.DAO.Email
{
	// Token: 0x0200007B RID: 123
	public interface IEmailAttachmentDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000310 RID: 784
		TPMailAttachment LoadAttachment(int FileId);

		// Token: 0x06000311 RID: 785
		void DeleteAttachment(int FileId);

		// Token: 0x06000312 RID: 786
		int CreateAttachment(TPMailAttachment attachment);
	}
}
