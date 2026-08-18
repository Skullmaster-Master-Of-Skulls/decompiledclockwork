using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000AD RID: 173
	public class InventoryAttachmentReusableClientProxy : WCFTokenBasedReusableClientProxy<IInventoryAttachment>, IInventoryAttachment, IService
	{
		// Token: 0x060006DD RID: 1757 RVA: 0x00012602 File Offset: 0x00010802
		public InventoryAttachmentReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0001260D File Offset: 0x0001080D
		public InventoryAttachmentReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0001261C File Offset: 0x0001081C
		public GetAttachmentByIdResp GetAttachmentById(GetAttachmentByIdReq request)
		{
			return this.WrapServiceMethod<GetAttachmentByIdResp>(() => this.Proxy.GetAttachmentById(request));
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00012654 File Offset: 0x00010854
		public GetProductAttachmentsResp GetProductAttachments(GetProductAttachmentsReq request)
		{
			return this.WrapServiceMethod<GetProductAttachmentsResp>(() => this.Proxy.GetProductAttachments(request));
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0001268C File Offset: 0x0001088C
		public AddAttachmentToProductResp AddAttachmentToProduct(AddAttachmentToProductReq request)
		{
			return this.WrapServiceMethod<AddAttachmentToProductResp>(() => this.Proxy.AddAttachmentToProduct(request));
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x000126C4 File Offset: 0x000108C4
		public AddAttachmentsToProductResp AddAttachmentsToProduct(AddAttachmentsToProductReq request)
		{
			return this.WrapServiceMethod<AddAttachmentsToProductResp>(() => this.Proxy.AddAttachmentsToProduct(request));
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x000126FC File Offset: 0x000108FC
		public RemoveAttachmentFromProductResp RemoveAttachmentFromProduct(RemoveAttachmentFromProductReq request)
		{
			return this.WrapServiceMethod<RemoveAttachmentFromProductResp>(() => this.Proxy.RemoveAttachmentFromProduct(request));
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00012734 File Offset: 0x00010934
		public RemoveAttachmentsFromProductResp RemoveAttachmentsFromProduct(RemoveAttachmentsFromProductReq request)
		{
			return this.WrapServiceMethod<RemoveAttachmentsFromProductResp>(() => this.Proxy.RemoveAttachmentsFromProduct(request));
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0001276C File Offset: 0x0001096C
		public RemoveAllAttachmentsFromProductResp RemoveAllAttachmentsFromProduct(RemoveAllAttachmentsFromProductReq request)
		{
			return this.WrapServiceMethod<RemoveAllAttachmentsFromProductResp>(() => this.Proxy.RemoveAllAttachmentsFromProduct(request));
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x000127A4 File Offset: 0x000109A4
		public GetProductPictureResp GetProductPicture(GetProductPictureReq request)
		{
			return this.WrapServiceMethod<GetProductPictureResp>(() => this.Proxy.GetProductPicture(request));
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x000127DC File Offset: 0x000109DC
		public SetProductPictureResp SetProductPicture(SetProductPictureReq request)
		{
			return this.WrapServiceMethod<SetProductPictureResp>(() => this.Proxy.SetProductPicture(request));
		}
	}
}
