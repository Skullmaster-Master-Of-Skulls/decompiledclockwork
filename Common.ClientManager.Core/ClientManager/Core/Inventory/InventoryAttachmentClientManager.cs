using System;
using System.Collections.Generic;
using System.Drawing;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Inventory
{
	// Token: 0x02000050 RID: 80
	public class InventoryAttachmentClientManager : IInventoryAttachmentClientManager, IWebService
	{
		// Token: 0x060002BB RID: 699 RVA: 0x0000C4C0 File Offset: 0x0000A6C0
		public InventoryAttachedFileDTO GetAttachmentById(int attachmentId)
		{
			GetAttachmentByIdReq getAttachmentByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetAttachmentByIdReq>();
			getAttachmentByIdReq.AttachmentId = attachmentId;
			return ClientServiceFactory.GetClientInstance<IInventoryAttachment>().GetAttachmentById(getAttachmentByIdReq).AttachedFile;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000C4F8 File Offset: 0x0000A6F8
		public IList<InventoryAttachedFileInfoDTO> GetProductAttachments(Guid productUniqueId)
		{
			GetProductAttachmentsReq getProductAttachmentsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetProductAttachmentsReq>();
			getProductAttachmentsReq.ProductUniqueId = productUniqueId.ToString();
			return ClientServiceFactory.GetClientInstance<IInventoryAttachment>().GetProductAttachments(getProductAttachmentsReq).AttachmentFiles;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000C53C File Offset: 0x0000A73C
		public int AddAttachmentToProduct(Guid productUniqueId, InventoryAttachedFileDTO attachedFile)
		{
			AddAttachmentToProductReq addAttachmentToProductReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AddAttachmentToProductReq>();
			addAttachmentToProductReq.ProductUniqueId = productUniqueId.ToString();
			addAttachmentToProductReq.AttachedFile = attachedFile;
			return ClientServiceFactory.GetClientInstance<IInventoryAttachment>().AddAttachmentToProduct(addAttachmentToProductReq).AttachmentFileId;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000C588 File Offset: 0x0000A788
		public void AddAttachmentsToProduct(Guid productUniqueId, IList<InventoryAttachedFileDTO> attachedFiles)
		{
			AddAttachmentsToProductReq addAttachmentsToProductReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AddAttachmentsToProductReq>();
			addAttachmentsToProductReq.ProductUniqueId = productUniqueId.ToString();
			addAttachmentsToProductReq.AttachedFiles = attachedFiles;
			ClientServiceFactory.GetClientInstance<IInventoryAttachment>().AddAttachmentsToProduct(addAttachmentsToProductReq);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000C5CC File Offset: 0x0000A7CC
		public void RemoveAttachmentFromProduct(int attachedFileId)
		{
			RemoveAttachmentFromProductReq removeAttachmentFromProductReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<RemoveAttachmentFromProductReq>();
			removeAttachmentFromProductReq.AttachmentFileId = attachedFileId;
			ClientServiceFactory.GetClientInstance<IInventoryAttachment>().RemoveAttachmentFromProduct(removeAttachmentFromProductReq);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000C5FC File Offset: 0x0000A7FC
		public void RemoveAttachmentsFromProduct(IList<int> attachedFileIds)
		{
			RemoveAttachmentsFromProductReq removeAttachmentsFromProductReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<RemoveAttachmentsFromProductReq>();
			removeAttachmentsFromProductReq.AttachedFileIds = attachedFileIds;
			ClientServiceFactory.GetClientInstance<IInventoryAttachment>().RemoveAttachmentsFromProduct(removeAttachmentsFromProductReq);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000C62C File Offset: 0x0000A82C
		public void RemoveAllAttachmentsFromProduct(Guid productUniqueId)
		{
			RemoveAllAttachmentsFromProductReq removeAllAttachmentsFromProductReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<RemoveAllAttachmentsFromProductReq>();
			removeAllAttachmentsFromProductReq.ProductUniqueId = productUniqueId.ToString();
			ClientServiceFactory.GetClientInstance<IInventoryAttachment>().RemoveAllAttachmentsFromProduct(removeAllAttachmentsFromProductReq);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000C668 File Offset: 0x0000A868
		public Image GetProductPicture(Guid productId)
		{
			GetProductPictureReq getProductPictureReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetProductPictureReq>();
			getProductPictureReq.ProductId = productId;
			return ClientServiceFactory.GetClientInstance<IInventoryAttachment>().GetProductPicture(getProductPictureReq).Picture;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000C6A0 File Offset: 0x0000A8A0
		public void SetProductPicture(Guid productId, Image picture)
		{
			SetProductPictureReq setProductPictureReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SetProductPictureReq>();
			setProductPictureReq.ProductId = productId;
			setProductPictureReq.Picture = picture;
			ClientServiceFactory.GetClientInstance<IInventoryAttachment>().SetProductPicture(setProductPictureReq);
		}
	}
}
