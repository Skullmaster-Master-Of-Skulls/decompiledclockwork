using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000BB RID: 187
	public class InventoryProductReusableClientProxy : WCFTokenBasedReusableClientProxy<IInventoryProduct>, IInventoryProduct, IService
	{
		// Token: 0x06000767 RID: 1895 RVA: 0x00013A3A File Offset: 0x00011C3A
		public InventoryProductReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00013A45 File Offset: 0x00011C45
		public InventoryProductReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00013A54 File Offset: 0x00011C54
		public GetProductsMatchingResp GetProductsMatching(GetProductsMatchingReq request)
		{
			return this.WrapServiceMethod<GetProductsMatchingResp>(() => this.Proxy.GetProductsMatching(request));
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00013A8C File Offset: 0x00011C8C
		public GetProductByIdResp GetProductById(GetProductByIdReq request)
		{
			return this.WrapServiceMethod<GetProductByIdResp>(() => this.Proxy.GetProductById(request));
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00013AC4 File Offset: 0x00011CC4
		public GetProductBySerialNumberResp GetProductBySerialNumber(GetProductBySerialNumberReq request)
		{
			return this.WrapServiceMethod<GetProductBySerialNumberResp>(() => this.Proxy.GetProductBySerialNumber(request));
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00013AFC File Offset: 0x00011CFC
		public GetProductByBarCodeResp GetProductByBarCode(GetProductByBarCodeReq request)
		{
			return this.WrapServiceMethod<GetProductByBarCodeResp>(() => this.Proxy.GetProductByBarCode(request));
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00013B34 File Offset: 0x00011D34
		public GetProductsByCatalogResp GetProductsByCatalog(GetProductsByCatalogReq request)
		{
			return this.WrapServiceMethod<GetProductsByCatalogResp>(() => this.Proxy.GetProductsByCatalog(request));
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00013B6C File Offset: 0x00011D6C
		public GetProductsByRootCategoryResp GetProductsByRootCategory(GetProductsByRootCategoryReq request)
		{
			return this.WrapServiceMethod<GetProductsByRootCategoryResp>(() => this.Proxy.GetProductsByRootCategory(request));
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00013BA4 File Offset: 0x00011DA4
		public GetProductsByCategoryResp GetProductsByCategory(GetProductsByCategoryReq request)
		{
			return this.WrapServiceMethod<GetProductsByCategoryResp>(() => this.Proxy.GetProductsByCategory(request));
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00013BDC File Offset: 0x00011DDC
		public GetProductsByGroupResp GetProductsByGroup(GetProductsByGroupReq request)
		{
			return this.WrapServiceMethod<GetProductsByGroupResp>(() => this.Proxy.GetProductsByGroup(request));
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00013C14 File Offset: 0x00011E14
		public GetProductsByLoanResp GetProductsByLoan(GetProductsByLoanReq request)
		{
			return this.WrapServiceMethod<GetProductsByLoanResp>(() => this.Proxy.GetProductsByLoan(request));
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00013C4C File Offset: 0x00011E4C
		public UpdateProductResp UpdateProduct(UpdateProductReq request)
		{
			return this.WrapServiceMethod<UpdateProductResp>(() => this.Proxy.UpdateProduct(request));
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00013C84 File Offset: 0x00011E84
		public CreateProductResp CreateProduct(CreateProductReq request)
		{
			return this.WrapServiceMethod<CreateProductResp>(() => this.Proxy.CreateProduct(request));
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00013CBC File Offset: 0x00011EBC
		public DeleteProductResp DeleteProduct(DeleteProductReq request)
		{
			return this.WrapServiceMethod<DeleteProductResp>(() => this.Proxy.DeleteProduct(request));
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00013CF4 File Offset: 0x00011EF4
		public DeleteProductsResp DeleteProducts(DeleteProductsReq request)
		{
			return this.WrapServiceMethod<DeleteProductsResp>(() => this.Proxy.DeleteProducts(request));
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00013D2C File Offset: 0x00011F2C
		public ChangeProductsCategoryResp ChangeProductsCategory(ChangeProductsCategoryReq request)
		{
			return this.WrapServiceMethod<ChangeProductsCategoryResp>(() => this.Proxy.ChangeProductsCategory(request));
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00013D64 File Offset: 0x00011F64
		public AssignProductToGroupResp AssignProductToGroup(AssignProductToGroupReq request)
		{
			return this.WrapServiceMethod<AssignProductToGroupResp>(() => this.Proxy.AssignProductToGroup(request));
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00013D9C File Offset: 0x00011F9C
		public AssignProductsToGroupResp AssignProductsToGroup(AssignProductsToGroupReq request)
		{
			return this.WrapServiceMethod<AssignProductsToGroupResp>(() => this.Proxy.AssignProductsToGroup(request));
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00013DD4 File Offset: 0x00011FD4
		public GetProductSnapshotResp GetProductSnapshot(GetProductSnapshotReq request)
		{
			return this.WrapServiceMethod<GetProductSnapshotResp>(() => this.Proxy.GetProductSnapshot(request));
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00013E0C File Offset: 0x0001200C
		public GetProductHistoryByIdResp GetProductHistoryById(GetProductHistoryByIdReq request)
		{
			return this.WrapServiceMethod<GetProductHistoryByIdResp>(() => this.Proxy.GetProductHistoryById(request));
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00013E44 File Offset: 0x00012044
		public GetProductHistoryByBarcodeResp GetProductHistoryByBarcode(GetProductHistoryByBarcodeReq request)
		{
			return this.WrapServiceMethod<GetProductHistoryByBarcodeResp>(() => this.Proxy.GetProductHistoryByBarcode(request));
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00013E7C File Offset: 0x0001207C
		public GetProductAvailabilityResp GetProductAvailability(GetProductAvailabilityReq request)
		{
			return this.WrapServiceMethod<GetProductAvailabilityResp>(() => this.Proxy.GetProductAvailability(request));
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00013EB4 File Offset: 0x000120B4
		public GetProductsInReservationGroupResp GetProductsInReservationGroup(GetProductsInReservationGroupReq request)
		{
			return this.WrapServiceMethod<GetProductsInReservationGroupResp>(() => this.Proxy.GetProductsInReservationGroup(request));
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00013EEC File Offset: 0x000120EC
		public ProductBarcodeAlreadyExistsResp ProductBarcodeAlreadyExists(ProductBarcodeAlreadyExistsReq request)
		{
			return this.WrapServiceMethod<ProductBarcodeAlreadyExistsResp>(() => this.Proxy.ProductBarcodeAlreadyExists(request));
		}
	}
}
