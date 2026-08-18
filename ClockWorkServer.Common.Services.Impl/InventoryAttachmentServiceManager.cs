using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Core.Inventory;
using TechnoPro.Common.Core.Mappers.Inventory;
using TechnoPro.Common.ICore.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200004D RID: 77
	public class InventoryAttachmentServiceManager : IInventoryAttachment, IService
	{
		// Token: 0x060002E0 RID: 736 RVA: 0x0000E2F4 File Offset: 0x0000C4F4
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000E308 File Offset: 0x0000C508
		public GetAttachmentByIdResp GetAttachmentById(GetAttachmentByIdReq request)
		{
			IInventoryAttachmentManager inventoryAttachmentManager = new InventoryAttachmentManager(request.GetOperationContext());
			return new GetAttachmentByIdResp
			{
				AttachedFile = inventoryAttachmentManager.GetAttachmentById(request.AttachmentId).ToDTO()
			};
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000E344 File Offset: 0x0000C544
		public GetProductAttachmentsResp GetProductAttachments(GetProductAttachmentsReq request)
		{
			IInventoryAttachmentManager inventoryAttachmentManager = new InventoryAttachmentManager(request.GetOperationContext());
			return new GetProductAttachmentsResp
			{
				AttachmentFiles = inventoryAttachmentManager.GetProductAttachments(new Guid(request.ProductUniqueId)).ToDTO()
			};
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000E384 File Offset: 0x0000C584
		public AddAttachmentToProductResp AddAttachmentToProduct(AddAttachmentToProductReq request)
		{
			IInventoryAttachmentManager inventoryAttachmentManager = new InventoryAttachmentManager(request.GetOperationContext());
			return new AddAttachmentToProductResp
			{
				AttachmentFileId = inventoryAttachmentManager.AddAttachmentToProduct(new Guid(request.ProductUniqueId), request.AttachedFile.ToDomainObject())
			};
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000E3CC File Offset: 0x0000C5CC
		public AddAttachmentsToProductResp AddAttachmentsToProduct(AddAttachmentsToProductReq request)
		{
			IInventoryAttachmentManager inventoryAttachmentManager = new InventoryAttachmentManager(request.GetOperationContext());
			inventoryAttachmentManager.AddAttachmentsToProduct(new Guid(request.ProductUniqueId), request.AttachedFiles.ToDomainObject());
			return new AddAttachmentsToProductResp();
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000E40C File Offset: 0x0000C60C
		public RemoveAttachmentFromProductResp RemoveAttachmentFromProduct(RemoveAttachmentFromProductReq request)
		{
			IInventoryAttachmentManager inventoryAttachmentManager = new InventoryAttachmentManager(request.GetOperationContext());
			inventoryAttachmentManager.RemoveAttachmentFromProduct(request.AttachmentFileId);
			return new RemoveAttachmentFromProductResp();
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000E43C File Offset: 0x0000C63C
		public RemoveAttachmentsFromProductResp RemoveAttachmentsFromProduct(RemoveAttachmentsFromProductReq request)
		{
			IInventoryAttachmentManager inventoryAttachmentManager = new InventoryAttachmentManager(request.GetOperationContext());
			inventoryAttachmentManager.RemoveAttachmentsFromProduct(request.AttachedFileIds);
			return new RemoveAttachmentsFromProductResp();
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000E46C File Offset: 0x0000C66C
		public RemoveAllAttachmentsFromProductResp RemoveAllAttachmentsFromProduct(RemoveAllAttachmentsFromProductReq request)
		{
			IInventoryAttachmentManager inventoryAttachmentManager = new InventoryAttachmentManager(request.GetOperationContext());
			inventoryAttachmentManager.RemoveAllAttachmentsFromProduct(new Guid(request.ProductUniqueId));
			return new RemoveAllAttachmentsFromProductResp();
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000E4A4 File Offset: 0x0000C6A4
		public GetProductPictureResp GetProductPicture(GetProductPictureReq request)
		{
			IInventoryAttachmentManager inventoryAttachmentManager = new InventoryAttachmentManager(request.GetOperationContext());
			return new GetProductPictureResp
			{
				Picture = inventoryAttachmentManager.GetProductPicture(request.ProductId)
			};
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000E4DC File Offset: 0x0000C6DC
		public SetProductPictureResp SetProductPicture(SetProductPictureReq request)
		{
			IInventoryAttachmentManager inventoryAttachmentManager = new InventoryAttachmentManager(request.GetOperationContext());
			inventoryAttachmentManager.SetProductPicture(request.ProductId, request.Picture);
			return new SetProductPictureResp();
		}
	}
}
