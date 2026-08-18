using System;
using System.Collections.Generic;
using System.Drawing;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.DAO.Inventory
{
	// Token: 0x02000064 RID: 100
	public interface IInventoryAttachmentDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000242 RID: 578
		InventoryAttachedFile GetAttachmentById(int attachmentId);

		// Token: 0x06000243 RID: 579
		IList<InventoryAttachedFileInfo> GetProductAttachments(Guid itemUniqueId);

		// Token: 0x06000244 RID: 580
		int AddAttachmentToProduct(Guid itemUniqueId, InventoryAttachedFile attachedFile);

		// Token: 0x06000245 RID: 581
		void RemoveAttachmentFromProduct(int attachedFileId);

		// Token: 0x06000246 RID: 582
		void RemoveAttachmentsFromProduct(IList<int> attachedFileIds);

		// Token: 0x06000247 RID: 583
		void RemoveAllAttachmentsFromProduct(Guid itemUniqueId);

		// Token: 0x06000248 RID: 584
		Image GetProductPicture(Guid productId);

		// Token: 0x06000249 RID: 585
		void SetProductPicture(Guid productId, Image picture);
	}
}
