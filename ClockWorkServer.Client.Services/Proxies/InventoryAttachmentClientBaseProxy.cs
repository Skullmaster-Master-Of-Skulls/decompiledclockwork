using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000AE RID: 174
	internal class InventoryAttachmentClientBaseProxy : ClientBase<IInventoryAttachment>, IInventoryAttachment, IService
	{
		// Token: 0x060006E8 RID: 1768 RVA: 0x00012814 File Offset: 0x00010A14
		public InventoryAttachmentClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0001281F File Offset: 0x00010A1F
		public InventoryAttachmentClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0001282C File Offset: 0x00010A2C
		public GetAttachmentByIdResp GetAttachmentById(GetAttachmentByIdReq request)
		{
			return base.Channel.GetAttachmentById(request);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0001284C File Offset: 0x00010A4C
		public GetProductAttachmentsResp GetProductAttachments(GetProductAttachmentsReq request)
		{
			return base.Channel.GetProductAttachments(request);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0001286C File Offset: 0x00010A6C
		public AddAttachmentToProductResp AddAttachmentToProduct(AddAttachmentToProductReq request)
		{
			return base.Channel.AddAttachmentToProduct(request);
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0001288C File Offset: 0x00010A8C
		public AddAttachmentsToProductResp AddAttachmentsToProduct(AddAttachmentsToProductReq request)
		{
			return base.Channel.AddAttachmentsToProduct(request);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x000128AC File Offset: 0x00010AAC
		public RemoveAttachmentFromProductResp RemoveAttachmentFromProduct(RemoveAttachmentFromProductReq request)
		{
			return base.Channel.RemoveAttachmentFromProduct(request);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x000128CC File Offset: 0x00010ACC
		public RemoveAttachmentsFromProductResp RemoveAttachmentsFromProduct(RemoveAttachmentsFromProductReq request)
		{
			return base.Channel.RemoveAttachmentsFromProduct(request);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x000128EC File Offset: 0x00010AEC
		public RemoveAllAttachmentsFromProductResp RemoveAllAttachmentsFromProduct(RemoveAllAttachmentsFromProductReq request)
		{
			return base.Channel.RemoveAllAttachmentsFromProduct(request);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0001290C File Offset: 0x00010B0C
		public GetProductPictureResp GetProductPicture(GetProductPictureReq request)
		{
			return base.Channel.GetProductPicture(request);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0001292C File Offset: 0x00010B2C
		public SetProductPictureResp SetProductPicture(SetProductPictureReq request)
		{
			return base.Channel.SetProductPicture(request);
		}
	}
}
