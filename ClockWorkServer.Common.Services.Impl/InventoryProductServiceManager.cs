using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Core.Inventory;
using TechnoPro.Common.Core.Mappers.Inventory;
using TechnoPro.Common.ICore.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000054 RID: 84
	public class InventoryProductServiceManager : IInventoryProduct, IService
	{
		// Token: 0x06000325 RID: 805 RVA: 0x0000F0FC File Offset: 0x0000D2FC
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000F110 File Offset: 0x0000D310
		public GetProductsMatchingResp GetProductsMatching(GetProductsMatchingReq request)
		{
			IInventoryProductManager inventoryProductManager = new InventoryProductManager(request.GetOperationContext());
			return new GetProductsMatchingResp
			{
				MatchingProducts = inventoryProductManager.GetProductsMatching(request.WorkingCatalogId, request.SearchText, request.SearchByField, request.ShowOnlyLoanedProducts).ToDTO()
			};
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000F160 File Offset: 0x0000D360
		public GetProductByIdResp GetProductById(GetProductByIdReq request)
		{
			IInventoryProductManager inventoryProductManager = new InventoryProductManager(request.GetOperationContext());
			return new GetProductByIdResp
			{
				Product = inventoryProductManager.GetProductById(request.WorkingCatalogId, new Guid(request.ProductUniqueId)).ToDTO()
			};
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000F1A8 File Offset: 0x0000D3A8
		public GetProductBySerialNumberResp GetProductBySerialNumber(GetProductBySerialNumberReq request)
		{
			IInventoryProductManager inventoryProductManager = new InventoryProductManager(request.GetOperationContext());
			return new GetProductBySerialNumberResp
			{
				Product = inventoryProductManager.GetProductBySerialNumber(request.WorkingCatalogId, request.ProductSerialNumber).ToDTO()
			};
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000F1EC File Offset: 0x0000D3EC
		public GetProductByBarCodeResp GetProductByBarCode(GetProductByBarCodeReq request)
		{
			IInventoryProductManager inventoryProductManager = new InventoryProductManager(request.GetOperationContext());
			return new GetProductByBarCodeResp
			{
				Product = inventoryProductManager.GetProductByBarCode(request.WorkingCatalogId, request.ProductBarCode).ToDTO()
			};
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000F230 File Offset: 0x0000D430
		public GetProductsByCatalogResp GetProductsByCatalog(GetProductsByCatalogReq request)
		{
			IInventoryProductManager inventoryProductManager = new InventoryProductManager(request.GetOperationContext());
			return new GetProductsByCatalogResp
			{
				Products = inventoryProductManager.GetProductsByCatalog(request.CatalogId).ToDTO()
			};
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000F26C File Offset: 0x0000D46C
		public GetProductsByRootCategoryResp GetProductsByRootCategory(GetProductsByRootCategoryReq request)
		{
			IInventoryProductManager inventoryProductManager = new InventoryProductManager(request.GetOperationContext());
			return new GetProductsByRootCategoryResp
			{
				Products = inventoryProductManager.GetProductsByRootCategory(request.WorkingCatalogId, request.RootCategoryName).ToDTO()
			};
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000F2B0 File Offset: 0x0000D4B0
		public GetProductsByCategoryResp GetProductsByCategory(GetProductsByCategoryReq request)
		{
			IInventoryProductManager inventoryProductManager = new InventoryProductManager(request.GetOperationContext());
			return new GetProductsByCategoryResp
			{
				Products = inventoryProductManager.GetProductsByCategory(request.WorkingCatalogId, request.CategoryName).ToDTO()
			};
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000F2F4 File Offset: 0x0000D4F4
		public GetProductsByGroupResp GetProductsByGroup(GetProductsByGroupReq request)
		{
			IInventoryProductManager inventoryProductManager = new InventoryProductManager(request.GetOperationContext());
			return new GetProductsByGroupResp
			{
				Products = inventoryProductManager.GetProductsByGroup(request.WorkingCatalogId, request.GroupId).ToDTO()
			};
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000F338 File Offset: 0x0000D538
		public GetProductsByLoanResp GetProductsByLoan(GetProductsByLoanReq request)
		{
			IInventoryProductManager inventoryProductManager = new InventoryProductManager(request.GetOperationContext());
			return new GetProductsByLoanResp
			{
				Products = inventoryProductManager.GetProductsByLoan(request.WorkingCatalogId, request.LoanId).ToDTO()
			};
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000F37C File Offset: 0x0000D57C
		public UpdateProductResp UpdateProduct(UpdateProductReq request)
		{
			IInventoryProductManager inventoryProductManager = new InventoryProductManager(request.GetOperationContext());
			InventoryProduct inventoryProduct = request.Product.ToDomainObject();
			inventoryProductManager.UpdateProduct(inventoryProduct);
			return new UpdateProductResp
			{
				BarCode = inventoryProduct.BarCode
			};
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000F3C0 File Offset: 0x0000D5C0
		public CreateProductResp CreateProduct(CreateProductReq request)
		{
			IInventoryProductManager inventoryProductManager = new InventoryProductManager(request.GetOperationContext());
			InventoryProduct inventoryProduct = request.Product.ToDomainObject();
			string productUniqueId = inventoryProductManager.CreateProduct(inventoryProduct).ToString();
			return new CreateProductResp
			{
				ProductUniqueId = productUniqueId,
				ProductDynamicDataId = inventoryProduct.ProductDynamicDataId,
				Barcode = inventoryProduct.BarCode
			};
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000F42C File Offset: 0x0000D62C
		public DeleteProductResp DeleteProduct(DeleteProductReq request)
		{
			IInventoryProductManager inventoryProductManager = new InventoryProductManager(request.GetOperationContext());
			return new DeleteProductResp
			{
				WasDeleted = inventoryProductManager.DeleteProduct(new Guid(request.ProductUniqueId))
			};
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000F468 File Offset: 0x0000D668
		public DeleteProductsResp DeleteProducts(DeleteProductsReq request)
		{
			IInventoryProductManager inventoryProductManager = new InventoryProductManager(request.GetOperationContext());
			return new DeleteProductsResp
			{
				NotDeletedProducts = inventoryProductManager.DeleteProducts(request.ProductUniqueIds)
			};
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000F4A0 File Offset: 0x0000D6A0
		public ChangeProductsCategoryResp ChangeProductsCategory(ChangeProductsCategoryReq request)
		{
			IInventoryProductManager inventoryProductManager = new InventoryProductManager(request.GetOperationContext());
			inventoryProductManager.ChangeProductsCategory(request.CategoryName, request.Products);
			return new ChangeProductsCategoryResp();
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000F4D8 File Offset: 0x0000D6D8
		public AssignProductToGroupResp AssignProductToGroup(AssignProductToGroupReq request)
		{
			IInventoryProductManager inventoryProductManager = new InventoryProductManager(request.GetOperationContext());
			inventoryProductManager.AssignProductToGroup(request.WorkingCatalogId, new Guid(request.ProductUniqueId), request.GroupId);
			return new AssignProductToGroupResp();
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000F51C File Offset: 0x0000D71C
		public AssignProductsToGroupResp AssignProductsToGroup(AssignProductsToGroupReq request)
		{
			IInventoryProductManager inventoryProductManager = new InventoryProductManager(request.GetOperationContext());
			inventoryProductManager.AssignProductsToGroup(request.WorkingCatalogId, request.ProductIdList, request.GroupId);
			return new AssignProductsToGroupResp();
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000F558 File Offset: 0x0000D758
		public GetProductSnapshotResp GetProductSnapshot(GetProductSnapshotReq request)
		{
			IInventoryProductManager inventoryProductManager = new InventoryProductManager(request.GetOperationContext());
			return new GetProductSnapshotResp
			{
				ProductSnapshot = inventoryProductManager.GetProductSnapshot(request.ProductUniqueId, request.LoanId).ToDTO()
			};
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000F59C File Offset: 0x0000D79C
		public GetProductHistoryByIdResp GetProductHistoryById(GetProductHistoryByIdReq request)
		{
			IInventoryProductManager inventoryProductManager = new InventoryProductManager(request.GetOperationContext());
			return new GetProductHistoryByIdResp
			{
				ProductSnapshotList = inventoryProductManager.GetProductHistory(request.ProductId, request.Reason).ToDTO()
			};
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000F5E0 File Offset: 0x0000D7E0
		public GetProductHistoryByBarcodeResp GetProductHistoryByBarcode(GetProductHistoryByBarcodeReq request)
		{
			IInventoryProductManager inventoryProductManager = new InventoryProductManager(request.GetOperationContext());
			return new GetProductHistoryByBarcodeResp
			{
				ProductSnapshotList = inventoryProductManager.GetProductHistory(request.ProductBarcode, request.Reason).ToDTO()
			};
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000F624 File Offset: 0x0000D824
		public GetProductAvailabilityResp GetProductAvailability(GetProductAvailabilityReq request)
		{
			IInventoryProductManager inventoryProductManager = new InventoryProductManager(request.GetOperationContext());
			return new GetProductAvailabilityResp
			{
				ProductBookedTimeList = inventoryProductManager.GetProductAvailability(request.ProductUniqueId, request.StartDate, request.EndDate, request.IncludeLoans, request.IncludeReservations, request.LoanId, request.ReservationId).ToDTO()
			};
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000F684 File Offset: 0x0000D884
		public GetProductsInReservationGroupResp GetProductsInReservationGroup(GetProductsInReservationGroupReq request)
		{
			IInventoryProductManager inventoryProductManager = new InventoryProductManager(request.GetOperationContext());
			return new GetProductsInReservationGroupResp
			{
				Products = inventoryProductManager.GetProductsInReservationGroup(request.WorkingCatalogId, request.ReservationGroupId).ToDTO()
			};
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000F6C8 File Offset: 0x0000D8C8
		public ProductBarcodeAlreadyExistsResp ProductBarcodeAlreadyExists(ProductBarcodeAlreadyExistsReq request)
		{
			IInventoryProductManager inventoryProductManager = new InventoryProductManager(request.GetOperationContext());
			return new ProductBarcodeAlreadyExistsResp
			{
				BarcodeExists = inventoryProductManager.ProductBarcodeAlreadyExists(request.Barcode, request.ProductId)
			};
		}
	}
}
