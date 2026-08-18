using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000BC RID: 188
	internal class InventoryProductClientBaseProxy : ClientBase<IInventoryProduct>, IInventoryProduct, IService
	{
		// Token: 0x0600077F RID: 1919 RVA: 0x00013F24 File Offset: 0x00012124
		public InventoryProductClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00013F2F File Offset: 0x0001212F
		public InventoryProductClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00013F3C File Offset: 0x0001213C
		public GetProductsMatchingResp GetProductsMatching(GetProductsMatchingReq request)
		{
			return base.Channel.GetProductsMatching(request);
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00013F5C File Offset: 0x0001215C
		public GetProductByIdResp GetProductById(GetProductByIdReq request)
		{
			return base.Channel.GetProductById(request);
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00013F7C File Offset: 0x0001217C
		public GetProductBySerialNumberResp GetProductBySerialNumber(GetProductBySerialNumberReq request)
		{
			return base.Channel.GetProductBySerialNumber(request);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00013F9C File Offset: 0x0001219C
		public GetProductByBarCodeResp GetProductByBarCode(GetProductByBarCodeReq request)
		{
			return base.Channel.GetProductByBarCode(request);
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00013FBC File Offset: 0x000121BC
		public GetProductsByCatalogResp GetProductsByCatalog(GetProductsByCatalogReq request)
		{
			return base.Channel.GetProductsByCatalog(request);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00013FDC File Offset: 0x000121DC
		public GetProductsByRootCategoryResp GetProductsByRootCategory(GetProductsByRootCategoryReq request)
		{
			return base.Channel.GetProductsByRootCategory(request);
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00013FFC File Offset: 0x000121FC
		public GetProductsByCategoryResp GetProductsByCategory(GetProductsByCategoryReq request)
		{
			return base.Channel.GetProductsByCategory(request);
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0001401C File Offset: 0x0001221C
		public GetProductsByGroupResp GetProductsByGroup(GetProductsByGroupReq request)
		{
			return base.Channel.GetProductsByGroup(request);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0001403C File Offset: 0x0001223C
		public GetProductsByLoanResp GetProductsByLoan(GetProductsByLoanReq request)
		{
			return base.Channel.GetProductsByLoan(request);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0001405C File Offset: 0x0001225C
		public UpdateProductResp UpdateProduct(UpdateProductReq request)
		{
			return base.Channel.UpdateProduct(request);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0001407C File Offset: 0x0001227C
		public CreateProductResp CreateProduct(CreateProductReq request)
		{
			return base.Channel.CreateProduct(request);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0001409C File Offset: 0x0001229C
		public DeleteProductResp DeleteProduct(DeleteProductReq request)
		{
			return base.Channel.DeleteProduct(request);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x000140BC File Offset: 0x000122BC
		public DeleteProductsResp DeleteProducts(DeleteProductsReq request)
		{
			return base.Channel.DeleteProducts(request);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x000140DC File Offset: 0x000122DC
		public ChangeProductsCategoryResp ChangeProductsCategory(ChangeProductsCategoryReq request)
		{
			return base.Channel.ChangeProductsCategory(request);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x000140FC File Offset: 0x000122FC
		public AssignProductToGroupResp AssignProductToGroup(AssignProductToGroupReq request)
		{
			return base.Channel.AssignProductToGroup(request);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0001411C File Offset: 0x0001231C
		public AssignProductsToGroupResp AssignProductsToGroup(AssignProductsToGroupReq request)
		{
			return base.Channel.AssignProductsToGroup(request);
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0001413C File Offset: 0x0001233C
		public GetProductSnapshotResp GetProductSnapshot(GetProductSnapshotReq request)
		{
			return base.Channel.GetProductSnapshot(request);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0001415C File Offset: 0x0001235C
		public GetProductHistoryByIdResp GetProductHistoryById(GetProductHistoryByIdReq request)
		{
			return base.Channel.GetProductHistoryById(request);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0001417C File Offset: 0x0001237C
		public GetProductHistoryByBarcodeResp GetProductHistoryByBarcode(GetProductHistoryByBarcodeReq request)
		{
			return base.Channel.GetProductHistoryByBarcode(request);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0001419C File Offset: 0x0001239C
		public GetProductAvailabilityResp GetProductAvailability(GetProductAvailabilityReq request)
		{
			return base.Channel.GetProductAvailability(request);
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x000141BC File Offset: 0x000123BC
		public GetProductsInReservationGroupResp GetProductsInReservationGroup(GetProductsInReservationGroupReq request)
		{
			return base.Channel.GetProductsInReservationGroup(request);
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x000141DC File Offset: 0x000123DC
		public ProductBarcodeAlreadyExistsResp ProductBarcodeAlreadyExists(ProductBarcodeAlreadyExistsReq request)
		{
			return base.Channel.ProductBarcodeAlreadyExists(request);
		}
	}
}
