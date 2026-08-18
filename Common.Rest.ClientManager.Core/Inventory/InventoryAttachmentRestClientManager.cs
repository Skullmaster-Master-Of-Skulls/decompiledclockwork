using System;
using System.Collections.Generic;
using System.Drawing;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Inventory
{
	// Token: 0x02000040 RID: 64
	public class InventoryAttachmentRestClientManager : BearerTokenRestProxy<IInventoryAttachmentClientManager>, IInventoryAttachmentClientManager, IWebService
	{
		// Token: 0x06000246 RID: 582 RVA: 0x00007529 File Offset: 0x00005729
		public InventoryAttachmentRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00007533 File Offset: 0x00005733
		public InventoryAttachmentRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000753E File Offset: 0x0000573E
		public InventoryAttachedFileDTO GetAttachmentById(int attachmentId)
		{
			return base.Get<InventoryAttachedFileDTO>(string.Format("inventoryattachment/attachmentid/{0}", attachmentId), true);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00007557 File Offset: 0x00005757
		public IList<InventoryAttachedFileInfoDTO> GetProductAttachments(Guid productUniqueId)
		{
			return base.GetMany<InventoryAttachedFileInfoDTO>(string.Format("inventoryattachment/productid/{0}", productUniqueId), true);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00007570 File Offset: 0x00005770
		public int AddAttachmentToProduct(Guid productUniqueId, InventoryAttachedFileDTO attachedFile)
		{
			AddAttachmentToProductReq addAttachmentToProductReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AddAttachmentToProductReq>();
			addAttachmentToProductReq.ProductUniqueId = productUniqueId.ToString();
			addAttachmentToProductReq.AttachedFile = attachedFile;
			return base.Post<AddAttachmentToProductReq, int>(addAttachmentToProductReq, "inventoryattachment/addattachmenttoproduct");
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000075B0 File Offset: 0x000057B0
		public void AddAttachmentsToProduct(Guid productUniqueId, IList<InventoryAttachedFileDTO> attachedFiles)
		{
			AddAttachmentsToProductReq addAttachmentsToProductReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AddAttachmentsToProductReq>();
			addAttachmentsToProductReq.ProductUniqueId = productUniqueId.ToString();
			addAttachmentsToProductReq.AttachedFiles = attachedFiles;
			base.Post<AddAttachmentsToProductReq>(addAttachmentsToProductReq, "inventoryattachment/addattachmentstoproduct");
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000075EE File Offset: 0x000057EE
		public void RemoveAttachmentFromProduct(int attachedFileId)
		{
			base.Delete(string.Format("inventoryattachment/attachmentfileid/{0}", attachedFileId));
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00007606 File Offset: 0x00005806
		public void RemoveAttachmentsFromProduct(IList<int> attachedFileIds)
		{
			base.Delete(string.Format("inventoryattachment/attachmentfileids/{0}", attachedFileIds.CommaSeparatedValuesWithoutSpace<int>()));
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000761E File Offset: 0x0000581E
		public void RemoveAllAttachmentsFromProduct(Guid productUniqueId)
		{
			base.Delete(string.Format("inventoryattachment/allattachmentsfromproduct/{0}", productUniqueId));
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00007636 File Offset: 0x00005836
		public Image GetProductPicture(Guid productId)
		{
			return base.Get<Image>(string.Format("inventoryattachment/productpicture/productid/{0}", productId), true);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00007650 File Offset: 0x00005850
		public void SetProductPicture(Guid productId, Image picture)
		{
			SetProductPictureReq setProductPictureReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SetProductPictureReq>();
			setProductPictureReq.ProductId = productId;
			setProductPictureReq.Picture = picture;
			base.Put<SetProductPictureReq>(setProductPictureReq, "inventoryattachment/productpicture");
		}
	}
}
