using System;
using System.Collections.Generic;
using System.Drawing;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Inventory
{
	// Token: 0x02000049 RID: 73
	public interface IInventoryAttachmentClientManager : IWebService
	{
		// Token: 0x060001FE RID: 510
		InventoryAttachedFileDTO GetAttachmentById(int attachmentId);

		// Token: 0x060001FF RID: 511
		IList<InventoryAttachedFileInfoDTO> GetProductAttachments(Guid productUniqueId);

		// Token: 0x06000200 RID: 512
		int AddAttachmentToProduct(Guid productUniqueId, InventoryAttachedFileDTO attachedFile);

		// Token: 0x06000201 RID: 513
		void AddAttachmentsToProduct(Guid productUniqueId, IList<InventoryAttachedFileDTO> attachedFiles);

		// Token: 0x06000202 RID: 514
		void RemoveAttachmentFromProduct(int attachedFileId);

		// Token: 0x06000203 RID: 515
		void RemoveAttachmentsFromProduct(IList<int> attachedFileIds);

		// Token: 0x06000204 RID: 516
		void RemoveAllAttachmentsFromProduct(Guid productUniqueId);

		// Token: 0x06000205 RID: 517
		Image GetProductPicture(Guid productId);

		// Token: 0x06000206 RID: 518
		void SetProductPicture(Guid productId, Image picture);
	}
}
