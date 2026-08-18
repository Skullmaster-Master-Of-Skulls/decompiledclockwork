using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.ICore.Inventory
{
	// Token: 0x02000087 RID: 135
	public interface IInventoryProductManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060003A9 RID: 937
		IList<InventoryProduct> GetProductsMatching(int workingCatalogId, string searchText, InventoryProductSearchByField searchByField = InventoryProductSearchByField.All, bool showOnlyLoanedProducts = false);

		// Token: 0x060003AA RID: 938
		InventoryProduct GetProductById(int workingCatalogId, Guid pUniqueId);

		// Token: 0x060003AB RID: 939
		InventoryProduct GetProductBySerialNumber(int workingCatalogId, string serialNumber);

		// Token: 0x060003AC RID: 940
		InventoryProduct GetProductByBarCode(int workingCatalogId, string barcode);

		// Token: 0x060003AD RID: 941
		IList<InventoryProduct> GetProductsByCatalog(int catalogId);

		// Token: 0x060003AE RID: 942
		IList<InventoryProduct> GetProductsByRootCategory(int workingCatalogId, string rootCategoryName);

		// Token: 0x060003AF RID: 943
		IList<InventoryProduct> GetProductsByCategory(int workingCatalogId, string exactCategoryName);

		// Token: 0x060003B0 RID: 944
		IList<InventoryProduct> GetProductsByGroup(int workingCatalogId, int groupId);

		// Token: 0x060003B1 RID: 945
		IList<InventoryProduct> GetProductsByLoan(int workingCatalogId, int loanId);

		// Token: 0x060003B2 RID: 946
		IList<InventoryProduct> GetProductsInReservationGroup(int workingCatalogId, int reservationGroupId);

		// Token: 0x060003B3 RID: 947
		void UpdateProduct(InventoryProduct product);

		// Token: 0x060003B4 RID: 948
		Guid CreateProduct(InventoryProduct product);

		// Token: 0x060003B5 RID: 949
		bool DeleteProduct(Guid id);

		// Token: 0x060003B6 RID: 950
		IList<Guid> DeleteProducts(IList<Guid> id);

		// Token: 0x060003B7 RID: 951
		void ChangeProductsCategory(string categoryName, IList<int> pIds);

		// Token: 0x060003B8 RID: 952
		void AssignProductToGroup(int workingCatalogId, Guid productUniqueId, int groupId);

		// Token: 0x060003B9 RID: 953
		void AssignProductsToGroup(int workingCatalogId, IList<int> productIdList, int groupId);

		// Token: 0x060003BA RID: 954
		InventoryProductSnapshot GetProductSnapshot(Guid productUniqueId, int loanId);

		// Token: 0x060003BB RID: 955
		IList<InventoryProductSnapshot> GetProductHistory(int productId, eInventoryProductSnapshotReason reason);

		// Token: 0x060003BC RID: 956
		IList<InventoryProductSnapshot> GetProductHistory(string barcode, eInventoryProductSnapshotReason reason);

		// Token: 0x060003BD RID: 957
		IList<InventoryProductBookedTime> GetProductAvailability(Guid uniqueId, DateTime startDate, DateTime endDate, bool includeLoans = true, bool includeReservations = true, int loanId = 0, int reservationId = 0);

		// Token: 0x060003BE RID: 958
		bool ProductBarcodeAlreadyExists(string barcode, int productId = 0);
	}
}
