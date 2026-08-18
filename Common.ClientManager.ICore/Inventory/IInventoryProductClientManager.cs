using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.ClientManager.ICore.Inventory
{
	// Token: 0x02000050 RID: 80
	public interface IInventoryProductClientManager : IWebService
	{
		// Token: 0x06000238 RID: 568
		IList<InventoryProductDTO> GetProductsMatching(int workingCatalogId, string searchText, InventoryProductSearchByField searchByField = InventoryProductSearchByField.All, bool showOnlyLoanedProducts = false);

		// Token: 0x06000239 RID: 569
		InventoryProductDTO GetProductById(int workingCatalogId, Guid pUniqueId);

		// Token: 0x0600023A RID: 570
		InventoryProductDTO GetProductBySerialNumber(int workingCatalogId, string serialNumber);

		// Token: 0x0600023B RID: 571
		InventoryProductDTO GetProductByBarCode(int workingCatalogId, string barcode);

		// Token: 0x0600023C RID: 572
		IList<InventoryProductDTO> GetProductsByCatalog(int catalogId);

		// Token: 0x0600023D RID: 573
		IList<InventoryProductDTO> GetProductsByRootCategory(int workingCatalogId, string rootCategoryName);

		// Token: 0x0600023E RID: 574
		IList<InventoryProductDTO> GetProductsByCategory(int workingCatalogId, string exactCategoryName);

		// Token: 0x0600023F RID: 575
		IList<InventoryProductDTO> GetProductsByGroup(int workingCatalogId, int groupId);

		// Token: 0x06000240 RID: 576
		IList<InventoryProductDTO> GetProductsByLoan(int workingCatalogId, int loanGroupId);

		// Token: 0x06000241 RID: 577
		IList<InventoryProductDTO> GetProductsInReservationGroup(int workingCatalogId, int reservationGroupId);

		// Token: 0x06000242 RID: 578
		void UpdateProduct(InventoryProductDTO product);

		// Token: 0x06000243 RID: 579
		Guid CreateProduct(InventoryProductDTO product);

		// Token: 0x06000244 RID: 580
		bool DeleteProduct(Guid id);

		// Token: 0x06000245 RID: 581
		IList<Guid> DeleteProducts(IList<Guid> productIds);

		// Token: 0x06000246 RID: 582
		void ChangeProductsCategory(string categoryName, IList<int> productIds);

		// Token: 0x06000247 RID: 583
		void AssignProductToGroup(int workingCatalogId, Guid productUniqueId, int groupId);

		// Token: 0x06000248 RID: 584
		void AssignProductsToGroup(int workingCatalogId, IList<int> productIdList, int groupId);

		// Token: 0x06000249 RID: 585
		InventoryProductSnapshotDTO GetProductSnapshot(Guid productUniqueId, int loanId);

		// Token: 0x0600024A RID: 586
		IList<InventoryProductSnapshotDTO> GetProductHistory(int productId, eInventoryProductSnapshotReason reason);

		// Token: 0x0600024B RID: 587
		IList<InventoryProductSnapshotDTO> GetProductHistory(string barcode, eInventoryProductSnapshotReason reason);

		// Token: 0x0600024C RID: 588
		IList<InventoryProductBookedTimeDTO> GetProductAvailability(Guid uniqueId, DateTime startDate, DateTime endDate, bool includeLoans = true, bool includeReservations = true, int loanId = 0, int reservationId = 0);

		// Token: 0x0600024D RID: 589
		bool ProductBarcodeAlreadyExists(string barcode, int productId = 0);
	}
}
