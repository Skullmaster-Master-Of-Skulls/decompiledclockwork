using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DropBox;

namespace TechnoPro.Common.ICore.Messaging
{
	// Token: 0x0200005C RID: 92
	public interface IAttachmentDropBoxManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000289 RID: 649
		void Save(DropBox_Attachment att);

		// Token: 0x0600028A RID: 650
		IList<DropBox_AttachmentInfo> GetAllAttachmentsInfo(string username);

		// Token: 0x0600028B RID: 651
		int CountAttachments(string username);

		// Token: 0x0600028C RID: 652
		DropBox_Attachment GetAttachment(int attID);

		// Token: 0x0600028D RID: 653
		DropBox_Attachment GetAttachment(string filename, string extension);

		// Token: 0x0600028E RID: 654
		void DeleteAttachment(int attID);
	}
}
