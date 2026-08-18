using System;
using System.Collections.Generic;
using System.Drawing;
using TechnoPro.Common.DAO.Impl.Inventory;
using TechnoPro.Common.DAO.Inventory;
using TechnoPro.Common.ICore.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.Core.Inventory
{
	// Token: 0x020000E1 RID: 225
	public class InventoryAttachmentManager : IInventoryAttachmentManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600088E RID: 2190 RVA: 0x0003944D File Offset: 0x0003764D
		public InventoryAttachmentManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.AttachmentDAO = new InventoryAttachmentDAO(opContext);
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x0003946C File Offset: 0x0003766C
		// (set) Token: 0x06000890 RID: 2192 RVA: 0x00039474 File Offset: 0x00037674
		private IInventoryAttachmentDAO AttachmentDAO { get; set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x0003947D File Offset: 0x0003767D
		// (set) Token: 0x06000892 RID: 2194 RVA: 0x00039485 File Offset: 0x00037685
		public OperationContext OpContext { get; set; }

		// Token: 0x06000893 RID: 2195 RVA: 0x00039490 File Offset: 0x00037690
		public InventoryAttachedFile GetAttachmentById(int attachmentId)
		{
			return this.AttachmentDAO.GetAttachmentById(attachmentId);
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x000394B0 File Offset: 0x000376B0
		public IList<InventoryAttachedFileInfo> GetProductAttachments(Guid productUniqueId)
		{
			return this.AttachmentDAO.GetProductAttachments(productUniqueId);
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x000394D0 File Offset: 0x000376D0
		public int AddAttachmentToProduct(Guid productUniqueId, InventoryAttachedFile attachedFile)
		{
			return this.AttachmentDAO.AddAttachmentToProduct(productUniqueId, attachedFile);
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x000394F0 File Offset: 0x000376F0
		public void AddAttachmentsToProduct(Guid productUniqueId, IList<InventoryAttachedFile> attachedFiles)
		{
			foreach (InventoryAttachedFile attachedFile in attachedFiles)
			{
				this.AddAttachmentToProduct(productUniqueId, attachedFile);
			}
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00039540 File Offset: 0x00037740
		public void RemoveAttachmentFromProduct(int attachedFileId)
		{
			this.AttachmentDAO.RemoveAttachmentFromProduct(attachedFileId);
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00039550 File Offset: 0x00037750
		public void RemoveAttachmentsFromProduct(IList<int> attachedFileIds)
		{
			this.AttachmentDAO.RemoveAttachmentsFromProduct(attachedFileIds);
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00039560 File Offset: 0x00037760
		public void RemoveAllAttachmentsFromProduct(Guid productUniqueId)
		{
			this.AttachmentDAO.RemoveAllAttachmentsFromProduct(productUniqueId);
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00039570 File Offset: 0x00037770
		public Image GetProductPicture(Guid productId)
		{
			return this.AttachmentDAO.GetProductPicture(productId);
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0003958E File Offset: 0x0003778E
		public void SetProductPicture(Guid productId, Image picture)
		{
			this.AttachmentDAO.SetProductPicture(productId, picture);
		}
	}
}
