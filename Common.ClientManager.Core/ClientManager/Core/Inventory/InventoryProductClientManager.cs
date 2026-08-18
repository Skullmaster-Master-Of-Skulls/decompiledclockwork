using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Inventory;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Inventory
{
	// Token: 0x02000057 RID: 87
	public class InventoryProductClientManager : IInventoryProductClientManager, IWebService
	{
		// Token: 0x060002FC RID: 764 RVA: 0x0000D210 File Offset: 0x0000B410
		public IList<InventoryProductDTO> GetProductsMatching(int workingCatalogId, string searchText, InventoryProductSearchByField searchByField = InventoryProductSearchByField.All, bool showOnlyLoanedProducts = false)
		{
			GetProductsMatchingReq getProductsMatchingReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetProductsMatchingReq>();
			getProductsMatchingReq.WorkingCatalogId = workingCatalogId;
			getProductsMatchingReq.SearchByField = searchByField;
			getProductsMatchingReq.SearchText = searchText;
			getProductsMatchingReq.ShowOnlyLoanedProducts = showOnlyLoanedProducts;
			return ClientServiceFactory.GetClientInstance<IInventoryProduct>().GetProductsMatching(getProductsMatchingReq).MatchingProducts;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000D260 File Offset: 0x0000B460
		public InventoryProductDTO GetProductById(int workingCatalogId, Guid pUniqueId)
		{
			GetProductByIdReq getProductByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetProductByIdReq>();
			getProductByIdReq.WorkingCatalogId = workingCatalogId;
			getProductByIdReq.ProductUniqueId = pUniqueId.ToString();
			return ClientServiceFactory.GetClientInstance<IInventoryProduct>().GetProductById(getProductByIdReq).Product;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000D2AC File Offset: 0x0000B4AC
		public InventoryProductDTO GetProductBySerialNumber(int workingCatalogId, string serialNumber)
		{
			GetProductBySerialNumberReq getProductBySerialNumberReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetProductBySerialNumberReq>();
			getProductBySerialNumberReq.WorkingCatalogId = workingCatalogId;
			getProductBySerialNumberReq.ProductSerialNumber = serialNumber;
			return ClientServiceFactory.GetClientInstance<IInventoryProduct>().GetProductBySerialNumber(getProductBySerialNumberReq).Product;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000D2EC File Offset: 0x0000B4EC
		public InventoryProductDTO GetProductByBarCode(int workingCatalogId, string barcode)
		{
			GetProductByBarCodeReq getProductByBarCodeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetProductByBarCodeReq>();
			getProductByBarCodeReq.WorkingCatalogId = workingCatalogId;
			getProductByBarCodeReq.ProductBarCode = barcode;
			return ClientServiceFactory.GetClientInstance<IInventoryProduct>().GetProductByBarCode(getProductByBarCodeReq).Product;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000D32C File Offset: 0x0000B52C
		public IList<InventoryProductDTO> GetProductsByCatalog(int catalogId)
		{
			GetProductsByCatalogReq getProductsByCatalogReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetProductsByCatalogReq>();
			getProductsByCatalogReq.CatalogId = catalogId;
			return ClientServiceFactory.GetClientInstance<IInventoryProduct>().GetProductsByCatalog(getProductsByCatalogReq).Products;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000D364 File Offset: 0x0000B564
		public IList<InventoryProductDTO> GetProductsByRootCategory(int workingCatalogId, string rootCategoryName)
		{
			GetProductsByRootCategoryReq getProductsByRootCategoryReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetProductsByRootCategoryReq>();
			getProductsByRootCategoryReq.WorkingCatalogId = workingCatalogId;
			getProductsByRootCategoryReq.RootCategoryName = rootCategoryName;
			return ClientServiceFactory.GetClientInstance<IInventoryProduct>().GetProductsByRootCategory(getProductsByRootCategoryReq).Products;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000D3A4 File Offset: 0x0000B5A4
		public IList<InventoryProductDTO> GetProductsByCategory(int workingCatalogId, string exactCategoryName)
		{
			GetProductsByCategoryReq getProductsByCategoryReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetProductsByCategoryReq>();
			getProductsByCategoryReq.WorkingCatalogId = workingCatalogId;
			getProductsByCategoryReq.CategoryName = exactCategoryName;
			return ClientServiceFactory.GetClientInstance<IInventoryProduct>().GetProductsByCategory(getProductsByCategoryReq).Products;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000D3E4 File Offset: 0x0000B5E4
		public IList<InventoryProductDTO> GetProductsByGroup(int workingCatalogId, int groupId)
		{
			GetProductsByGroupReq getProductsByGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetProductsByGroupReq>();
			getProductsByGroupReq.WorkingCatalogId = workingCatalogId;
			getProductsByGroupReq.GroupId = groupId;
			return ClientServiceFactory.GetClientInstance<IInventoryProduct>().GetProductsByGroup(getProductsByGroupReq).Products;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000D424 File Offset: 0x0000B624
		public IList<InventoryProductDTO> GetProductsByLoan(int workingCatalogId, int loanGroupId)
		{
			GetProductsByLoanReq getProductsByLoanReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetProductsByLoanReq>();
			getProductsByLoanReq.WorkingCatalogId = workingCatalogId;
			getProductsByLoanReq.LoanId = loanGroupId;
			return ClientServiceFactory.GetClientInstance<IInventoryProduct>().GetProductsByLoan(getProductsByLoanReq).Products;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000D464 File Offset: 0x0000B664
		public IList<InventoryProductDTO> GetProductsInReservationGroup(int workingCatalogId, int reservationGroupId)
		{
			GetProductsInReservationGroupReq getProductsInReservationGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetProductsInReservationGroupReq>();
			getProductsInReservationGroupReq.WorkingCatalogId = workingCatalogId;
			getProductsInReservationGroupReq.ReservationGroupId = reservationGroupId;
			return ClientServiceFactory.GetClientInstance<IInventoryProduct>().GetProductsInReservationGroup(getProductsInReservationGroupReq).Products;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000D4A4 File Offset: 0x0000B6A4
		public void UpdateProduct(InventoryProductDTO product)
		{
			UpdateProductReq updateProductReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateProductReq>();
			updateProductReq.Product = product;
			UpdateProductResp updateProductResp = ClientServiceFactory.GetClientInstance<IInventoryProduct>().UpdateProduct(updateProductReq);
			product.BarCode = updateProductResp.BarCode;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000D4E0 File Offset: 0x0000B6E0
		public Guid CreateProduct(InventoryProductDTO product)
		{
			CreateProductReq createProductReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateProductReq>();
			createProductReq.Product = product;
			CreateProductResp createProductResp = ClientServiceFactory.GetClientInstance<IInventoryProduct>().CreateProduct(createProductReq);
			product.ProductDynamicDataId = createProductResp.ProductDynamicDataId;
			product.BarCode = createProductResp.Barcode;
			Guid guid = new Guid(createProductResp.ProductUniqueId);
			product.UniqueId = guid;
			return guid;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000D544 File Offset: 0x0000B744
		public bool DeleteProduct(Guid id)
		{
			DeleteProductReq deleteProductReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteProductReq>();
			deleteProductReq.ProductUniqueId = id.ToString();
			return ClientServiceFactory.GetClientInstance<IInventoryProduct>().DeleteProduct(deleteProductReq).WasDeleted;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000D588 File Offset: 0x0000B788
		public IList<Guid> DeleteProducts(IList<Guid> productIds)
		{
			DeleteProductsReq deleteProductsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteProductsReq>();
			deleteProductsReq.ProductUniqueIds = productIds;
			return ClientServiceFactory.GetClientInstance<IInventoryProduct>().DeleteProducts(deleteProductsReq).NotDeletedProducts;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000D5C0 File Offset: 0x0000B7C0
		public void ChangeProductsCategory(string categoryName, IList<int> productIds)
		{
			ChangeProductsCategoryReq changeProductsCategoryReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ChangeProductsCategoryReq>();
			changeProductsCategoryReq.CategoryName = categoryName;
			changeProductsCategoryReq.Products = productIds;
			ClientServiceFactory.GetClientInstance<IInventoryProduct>().ChangeProductsCategory(changeProductsCategoryReq);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000D5F8 File Offset: 0x0000B7F8
		public void AssignProductToGroup(int workingCatalogId, Guid productUniqueId, int groupId)
		{
			AssignProductToGroupReq assignProductToGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AssignProductToGroupReq>();
			assignProductToGroupReq.ProductUniqueId = productUniqueId.ToString();
			assignProductToGroupReq.WorkingCatalogId = workingCatalogId;
			assignProductToGroupReq.GroupId = groupId;
			ClientServiceFactory.GetClientInstance<IInventoryProduct>().AssignProductToGroup(assignProductToGroupReq);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000D644 File Offset: 0x0000B844
		public void AssignProductsToGroup(int workingCatalogId, IList<int> productIdList, int groupId)
		{
			AssignProductsToGroupReq assignProductsToGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AssignProductsToGroupReq>();
			assignProductsToGroupReq.WorkingCatalogId = workingCatalogId;
			assignProductsToGroupReq.ProductIdList = productIdList;
			assignProductsToGroupReq.GroupId = groupId;
			ClientServiceFactory.GetClientInstance<IInventoryProduct>().AssignProductsToGroup(assignProductsToGroupReq);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000D684 File Offset: 0x0000B884
		public InventoryProductSnapshotDTO GetProductSnapshot(Guid productUniqueId, int loanId)
		{
			GetProductSnapshotReq getProductSnapshotReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetProductSnapshotReq>();
			getProductSnapshotReq.ProductUniqueId = productUniqueId;
			getProductSnapshotReq.LoanId = loanId;
			return ClientServiceFactory.GetClientInstance<IInventoryProduct>().GetProductSnapshot(getProductSnapshotReq).ProductSnapshot;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000D6C4 File Offset: 0x0000B8C4
		public IList<InventoryProductSnapshotDTO> GetProductHistory(int productId, eInventoryProductSnapshotReason reason)
		{
			GetProductHistoryByIdReq getProductHistoryByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetProductHistoryByIdReq>();
			getProductHistoryByIdReq.ProductId = productId;
			getProductHistoryByIdReq.Reason = reason;
			return ClientServiceFactory.GetClientInstance<IInventoryProduct>().GetProductHistoryById(getProductHistoryByIdReq).ProductSnapshotList;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000D704 File Offset: 0x0000B904
		public IList<InventoryProductSnapshotDTO> GetProductHistory(string barcode, eInventoryProductSnapshotReason reason)
		{
			GetProductHistoryByBarcodeReq getProductHistoryByBarcodeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetProductHistoryByBarcodeReq>();
			getProductHistoryByBarcodeReq.ProductBarcode = barcode;
			getProductHistoryByBarcodeReq.Reason = reason;
			return ClientServiceFactory.GetClientInstance<IInventoryProduct>().GetProductHistoryByBarcode(getProductHistoryByBarcodeReq).ProductSnapshotList;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000D744 File Offset: 0x0000B944
		public IList<InventoryProductBookedTimeDTO> GetProductAvailability(Guid uniqueId, DateTime startDate, DateTime endDate, bool includeLoans = true, bool includeReservations = true, int loanId = 0, int reservationId = 0)
		{
			GetProductAvailabilityReq getProductAvailabilityReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetProductAvailabilityReq>();
			getProductAvailabilityReq.ProductUniqueId = uniqueId;
			getProductAvailabilityReq.StartDate = startDate;
			getProductAvailabilityReq.EndDate = endDate;
			getProductAvailabilityReq.IncludeLoans = includeLoans;
			getProductAvailabilityReq.IncludeReservations = includeReservations;
			getProductAvailabilityReq.LoanId = loanId;
			getProductAvailabilityReq.ReservationId = reservationId;
			return ClientServiceFactory.GetClientInstance<IInventoryProduct>().GetProductAvailability(getProductAvailabilityReq).ProductBookedTimeList;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000D7AC File Offset: 0x0000B9AC
		public bool ProductBarcodeAlreadyExists(string barcode, int productId = 0)
		{
			ProductBarcodeAlreadyExistsReq productBarcodeAlreadyExistsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ProductBarcodeAlreadyExistsReq>();
			productBarcodeAlreadyExistsReq.ProductId = productId;
			productBarcodeAlreadyExistsReq.Barcode = barcode;
			return ClientServiceFactory.GetClientInstance<IInventoryProduct>().ProductBarcodeAlreadyExists(productBarcodeAlreadyExistsReq).BarcodeExists;
		}
	}
}
