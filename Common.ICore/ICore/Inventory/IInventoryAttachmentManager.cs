using System;
using System.Collections.Generic;
using System.Drawing;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.ICore.Inventory
{
	// Token: 0x0200007F RID: 127
	public interface IInventoryAttachmentManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000372 RID: 882
		InventoryAttachedFile GetAttachmentById(int attachmentId);

		// Token: 0x06000373 RID: 883
		IList<InventoryAttachedFileInfo> GetProductAttachments(Guid productUniqueId);

		// Token: 0x06000374 RID: 884
		int AddAttachmentToProduct(Guid productUniqueId, InventoryAttachedFile attachedFile);

		// Token: 0x06000375 RID: 885
		void AddAttachmentsToProduct(Guid productUniqueId, IList<InventoryAttachedFile> attachedFiles);

		// Token: 0x06000376 RID: 886
		void RemoveAttachmentFromProduct(int attachedFileId);

		// Token: 0x06000377 RID: 887
		void RemoveAttachmentsFromProduct(IList<int> attachedFileIds);

		// Token: 0x06000378 RID: 888
		void RemoveAllAttachmentsFromProduct(Guid productUniqueId);

		// Token: 0x06000379 RID: 889
		Image GetProductPicture(Guid productId);

		// Token: 0x0600037A RID: 890
		void SetProductPicture(Guid productId, Image picture);
	}
}
