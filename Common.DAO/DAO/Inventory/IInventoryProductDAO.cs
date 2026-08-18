using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.DAO.Inventory
{
	// Token: 0x02000069 RID: 105
	public interface IInventoryProductDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600025F RID: 607
		IList<InventoryProduct> GetProductsMatching(string searchText, IList<string> allowedCategories, InventoryProductSearchByField searchByField = InventoryProductSearchByField.All, bool showOnlyLoanedProducts = false);

		// Token: 0x06000260 RID: 608
		InventoryProduct GetProductById(Guid uniqueid);

		// Token: 0x06000261 RID: 609
		InventoryProduct GetProductById(int pId);

		// Token: 0x06000262 RID: 610
		InventoryProduct GetProductById(Guid uniqueid, IList<string> allowedCategories);

		// Token: 0x06000263 RID: 611
		InventoryProduct GetProductBySerialNumber(string serialNumber, IList<string> allowedCategories);

		// Token: 0x06000264 RID: 612
		InventoryProduct GetProductByBarCode(string barcode, IList<string> allowedCategories);

		// Token: 0x06000265 RID: 613
		IList<InventoryProduct> GetProductsByRootCategory(string rootCategoryName);

		// Token: 0x06000266 RID: 614
		IList<InventoryProduct> GetProductsByCategory(string exactCategoryName);

		// Token: 0x06000267 RID: 615
		IList<InventoryProduct> GetProductsByCategories(params string[] categories);

		// Token: 0x06000268 RID: 616
		IList<InventoryProduct> GetProductsByLoan(int loanID, IList<string> allowedCategories);

		// Token: 0x06000269 RID: 617
		IList<InventoryProduct> GetProductsByGroup(int groupId, IList<string> allowedCategories);

		// Token: 0x0600026A RID: 618
		IList<InventoryProduct> GetProductsInReservationGroup(int reservationGroupId, IList<string> allowedCategories);

		// Token: 0x0600026B RID: 619
		void UpdateProduct(InventoryProduct product, string barcodePrefix);

		// Token: 0x0600026C RID: 620
		Guid CreateProduct(InventoryProduct productRequest, string barcodePrefix);

		// Token: 0x0600026D RID: 621
		bool DeleteProduct(Guid uniqueid);

		// Token: 0x0600026E RID: 622
		void ChangeProductsCategory(string categoryName, IList<int> pIds);

		// Token: 0x0600026F RID: 623
		bool AssignProductToGroup(Guid productUniqueId, int groupId, IList<string> allowedCategories);

		// Token: 0x06000270 RID: 624
		void AssignProductsToGroup(IList<int> productIdList, int groupId, IList<string> allowedCategories);

		// Token: 0x06000271 RID: 625
		int CreateProductSnapshot(InventoryProductSnapshot pSnapshot);

		// Token: 0x06000272 RID: 626
		InventoryProductSnapshot GetProductSnapshot(Guid productUniqueId, int loanId);

		// Token: 0x06000273 RID: 627
		InventoryProductSnapshot GetProductSnapshot(Guid productUniqueId, int loanId, eInventoryProductSnapshotReason reason);

		// Token: 0x06000274 RID: 628
		InventoryProductSnapshot GetProductSnapshotByLoanGroup(Guid productUniqueId, int loanGroupId, eInventoryProductSnapshotReason reason);

		// Token: 0x06000275 RID: 629
		IList<InventoryProductSnapshot> GetProductHistory(int productId, eInventoryProductSnapshotReason reason);

		// Token: 0x06000276 RID: 630
		IList<InventoryProductSnapshot> GetProductHistory(Guid productUniqueId, eInventoryProductSnapshotReason reason);

		// Token: 0x06000277 RID: 631
		IList<InventoryProductSnapshot> GetProductHistory(string barcode, eInventoryProductSnapshotReason reason);

		// Token: 0x06000278 RID: 632
		IList<InventoryProductBookedTime> GetProductAvailability(Guid uniqueId, DateTime startDate, DateTime endDate, bool includeLoans = true, bool includeReservations = true, int loanId = 0, int reservationId = 0);

		// Token: 0x06000279 RID: 633
		bool ProductBarcodeAlreadyExists(string barcode, int productId = 0);
	}
}
