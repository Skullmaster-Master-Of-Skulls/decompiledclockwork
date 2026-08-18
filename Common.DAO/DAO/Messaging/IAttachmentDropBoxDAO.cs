using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DropBox;

namespace TechnoPro.Common.DAO.Messaging
{
	// Token: 0x0200004C RID: 76
	public interface IAttachmentDropBoxDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001AB RID: 427
		void Save(DropBox_Attachment item);

		// Token: 0x060001AC RID: 428
		IList<DropBox_AttachmentInfo> GetAllAttachmentsInfo(string username);

		// Token: 0x060001AD RID: 429
		DropBox_Attachment GetAttachment(int id);

		// Token: 0x060001AE RID: 430
		DropBox_Attachment GetAttachment(string filename, string extension);

		// Token: 0x060001AF RID: 431
		void Delete(int id);

		// Token: 0x060001B0 RID: 432
		int CountAttachments(string username);
	}
}
