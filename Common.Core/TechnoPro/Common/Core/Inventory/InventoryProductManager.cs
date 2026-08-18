using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Core.Inventory.Adapters;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.DAO.Impl.Inventory;
using TechnoPro.Common.DAO.Inventory;
using TechnoPro.Common.ICore.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Exceptions.PermissionDenied;

namespace TechnoPro.Common.Core.Inventory
{
	// Token: 0x020000E8 RID: 232
	public class InventoryProductManager : IInventoryProductManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x0003A44A File Offset: 0x0003864A
		// (set) Token: 0x060008EE RID: 2286 RVA: 0x0003A452 File Offset: 0x00038652
		internal IInventoryProductDAO InventoryProductDAO { get; set; }

		// Token: 0x060008EF RID: 2287 RVA: 0x0003A45B File Offset: 0x0003865B
		public InventoryProductManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.InventoryProductDAO = new InventoryProductDAO(opContext);
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060008F0 RID: 2288 RVA: 0x0003A47A File Offset: 0x0003867A
		// (set) Token: 0x060008F1 RID: 2289 RVA: 0x0003A482 File Offset: 0x00038682
		public OperationContext OpContext { get; set; }

		// Token: 0x060008F2 RID: 2290 RVA: 0x0003A48C File Offset: 0x0003868C
		public IList<InventoryProduct> GetProductsMatching(int workingCatalogId, string searchText, InventoryProductSearchByField searchByField = InventoryProductSearchByField.All, bool showOnlyLoanedProducts = false)
		{
			IInventoryCategoryManager inventoryCategoryManager = new InventoryCategoryManager(this.OpContext);
			IList<InventoryCategory> categoriesByCatalog = inventoryCategoryManager.GetCategoriesByCatalog(workingCatalogId);
			return this.InventoryProductDAO.GetProductsMatching(searchText, (from c in categoriesByCatalog
			select c.CategoryName).ToList<string>(), searchByField, showOnlyLoanedProducts);
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0003A4EC File Offset: 0x000386EC
		public InventoryProduct GetProductById(int workingCatalogId, Guid id)
		{
			IInventoryCategoryManager inventoryCategoryManager = new InventoryCategoryManager(this.OpContext);
			IList<InventoryCategory> categoriesByCatalog = inventoryCategoryManager.GetCategoriesByCatalog(workingCatalogId);
			return this.InventoryProductDAO.GetProductById(id, (from c in categoriesByCatalog
			select c.CategoryName).ToList<string>());
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0003A548 File Offset: 0x00038748
		public InventoryProduct GetProductBySerialNumber(int workingCatalogId, string serialNumber)
		{
			IInventoryCategoryManager inventoryCategoryManager = new InventoryCategoryManager(this.OpContext);
			IList<InventoryCategory> categoriesByCatalog = inventoryCategoryManager.GetCategoriesByCatalog(workingCatalogId);
			return this.InventoryProductDAO.GetProductBySerialNumber(serialNumber, (from c in categoriesByCatalog
			select c.CategoryName).ToList<string>());
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0003A5A4 File Offset: 0x000387A4
		public InventoryProduct GetProductByBarCode(int workingCatalogId, string barcode)
		{
			IInventoryCategoryManager inventoryCategoryManager = new InventoryCategoryManager(this.OpContext);
			IList<InventoryCategory> categoriesByCatalog = inventoryCategoryManager.GetCategoriesByCatalog(workingCatalogId);
			return this.InventoryProductDAO.GetProductByBarCode(barcode, (from c in categoriesByCatalog
			select c.CategoryName).ToList<string>());
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0003A600 File Offset: 0x00038800
		public IList<InventoryProduct> GetProductsByCatalog(int catalogId)
		{
			bool flag = this.OpContext.IsCatalogAllowedForUser(catalogId);
			if (flag)
			{
				IInventoryCategoryManager inventoryCategoryManager = new InventoryCategoryManager(this.OpContext);
				IList<InventoryCategory> categoriesByCatalog = inventoryCategoryManager.GetCategoriesByCatalog(catalogId);
				return this.InventoryProductDAO.GetProductsByCategories((from c in categoriesByCatalog
				select c.CategoryName).ToArray<string>());
			}
			throw new PermissionDeniedException(string.Format("User Id '{0}' does not have permission to read Catalog Id '{1}'", this.OpContext.WhoAmI, catalogId));
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0003A694 File Offset: 0x00038894
		public IList<InventoryProduct> GetProductsByRootCategory(int workingCatalogId, string rootCategoryName)
		{
			IInventoryCategoryManager inventoryCategoryManager = new InventoryCategoryManager(this.OpContext);
			IList<InventoryCategory> categoriesByCatalog = inventoryCategoryManager.GetCategoriesByCatalog(workingCatalogId);
			bool flag = categoriesByCatalog.Any((InventoryCategory c) => c.CategoryName.ToUpper().StartsWith(rootCategoryName.ToUpper()));
			IList<InventoryProduct> result;
			if (flag)
			{
				result = this.InventoryProductDAO.GetProductsByRootCategory(rootCategoryName);
			}
			else
			{
				result = new List<InventoryProduct>();
			}
			return result;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0003A6FC File Offset: 0x000388FC
		public IList<InventoryProduct> GetProductsByCategory(int workingCatalogId, string exactCategoryName)
		{
			IInventoryCategoryManager inventoryCategoryManager = new InventoryCategoryManager(this.OpContext);
			IList<InventoryCategory> categoriesByCatalog = inventoryCategoryManager.GetCategoriesByCatalog(workingCatalogId);
			bool flag = categoriesByCatalog.Any((InventoryCategory c) => c.CategoryName.ToUpper().Equals(exactCategoryName.ToUpper()));
			IList<InventoryProduct> result;
			if (flag)
			{
				result = this.InventoryProductDAO.GetProductsByCategory(exactCategoryName);
			}
			else
			{
				result = new List<InventoryProduct>();
			}
			return result;
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0003A764 File Offset: 0x00038964
		public IList<InventoryProduct> GetProductsByGroup(int workingCatalogId, int groupId)
		{
			IInventoryCategoryManager inventoryCategoryManager = new InventoryCategoryManager(this.OpContext);
			IList<InventoryCategory> categoriesByCatalog = inventoryCategoryManager.GetCategoriesByCatalog(workingCatalogId);
			return this.InventoryProductDAO.GetProductsByGroup(groupId, (from c in categoriesByCatalog
			select c.CategoryName).ToList<string>());
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0003A7C0 File Offset: 0x000389C0
		public IList<InventoryProduct> GetProductsByLoan(int workingCatalogId, int loanId)
		{
			IInventoryCategoryManager inventoryCategoryManager = new InventoryCategoryManager(this.OpContext);
			IList<InventoryCategory> categoriesByCatalog = inventoryCategoryManager.GetCategoriesByCatalog(workingCatalogId);
			return this.InventoryProductDAO.GetProductsByLoan(loanId, (from c in categoriesByCatalog
			select c.CategoryName).ToList<string>());
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0003A81C File Offset: 0x00038A1C
		public IList<InventoryProduct> GetProductsInReservationGroup(int workingCatalogId, int reservationGroupId)
		{
			IInventoryCategoryManager inventoryCategoryManager = new InventoryCategoryManager(this.OpContext);
			IList<InventoryCategory> categoriesByCatalog = inventoryCategoryManager.GetCategoriesByCatalog(workingCatalogId);
			return this.InventoryProductDAO.GetProductsInReservationGroup(reservationGroupId, (from c in categoriesByCatalog
			select c.CategoryName).ToList<string>());
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0003A878 File Offset: 0x00038A78
		public void UpdateProduct(InventoryProduct product)
		{
			string settingValue = SettingManager.CurrentInstance.GetSettingValue<string>(Setting.INVENTORYSYSTEM_ProductBarcodePrefix);
			this.InventoryProductDAO.UpdateProduct(product, settingValue);
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0003A8A4 File Offset: 0x00038AA4
		public Guid CreateProduct(InventoryProduct product)
		{
			string settingValue = SettingManager.CurrentInstance.GetSettingValue<string>(Setting.INVENTORYSYSTEM_ProductBarcodePrefix);
			return this.InventoryProductDAO.CreateProduct(product, settingValue);
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0003A8D4 File Offset: 0x00038AD4
		public bool DeleteProduct(Guid productId)
		{
			bool flag = this.OpContext.IsInventoryAdmin(true);
			bool flag2 = flag;
			if (flag2)
			{
				return this.InventoryProductDAO.DeleteProduct(productId);
			}
			throw new PermissionDeniedException(string.Format("User '{0}' does not have permission to delete a product", this.OpContext.WhoAmI));
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0003A924 File Offset: 0x00038B24
		public IList<Guid> DeleteProducts(IList<Guid> products)
		{
			return (from pGuid in products
			where !this.DeleteProduct(pGuid)
			select pGuid).ToList<Guid>();
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0003A94D File Offset: 0x00038B4D
		public void ChangeProductsCategory(string categoryName, IList<int> pIds)
		{
			this.InventoryProductDAO.ChangeProductsCategory(categoryName, pIds);
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0003A960 File Offset: 0x00038B60
		public void AssignProductToGroup(int workingCatalogId, Guid productUniqueId, int groupId)
		{
			IInventoryCategoryManager inventoryCategoryManager = new InventoryCategoryManager(this.OpContext);
			IList<InventoryCategory> categoriesByCatalog = inventoryCategoryManager.GetCategoriesByCatalog(workingCatalogId);
			this.InventoryProductDAO.AssignProductToGroup(productUniqueId, groupId, (from c in categoriesByCatalog
			select c.CategoryName).ToList<string>());
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0003A9BC File Offset: 0x00038BBC
		public void AssignProductsToGroup(int workingCatalogId, IList<int> productIdList, int groupId)
		{
			IInventoryCategoryManager inventoryCategoryManager = new InventoryCategoryManager(this.OpContext);
			IList<InventoryCategory> categoriesByCatalog = inventoryCategoryManager.GetCategoriesByCatalog(workingCatalogId);
			this.InventoryProductDAO.AssignProductsToGroup(productIdList, groupId, (from c in categoriesByCatalog
			select c.CategoryName).ToList<string>());
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0003AA18 File Offset: 0x00038C18
		public InventoryProductSnapshot GetProductSnapshot(Guid productUniqueId, int loanId)
		{
			return this.InventoryProductDAO.GetProductSnapshot(productUniqueId, loanId);
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0003AA38 File Offset: 0x00038C38
		public IList<InventoryProductSnapshot> GetProductHistory(int productId, eInventoryProductSnapshotReason reason)
		{
			return this.InventoryProductDAO.GetProductHistory(productId, reason);
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0003AA58 File Offset: 0x00038C58
		public IList<InventoryProductSnapshot> GetProductHistory(string barcode, eInventoryProductSnapshotReason reason)
		{
			return this.InventoryProductDAO.GetProductHistory(barcode, reason);
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0003AA78 File Offset: 0x00038C78
		public IList<InventoryProductBookedTime> GetProductAvailability(Guid uniqueId, DateTime startDate, DateTime endDate, bool includeLoans = true, bool includeReservations = true, int loanId = 0, int reservationId = 0)
		{
			return this.InventoryProductDAO.GetProductAvailability(uniqueId, startDate, endDate, includeLoans, includeReservations, loanId, reservationId);
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0003AAA0 File Offset: 0x00038CA0
		public bool ProductBarcodeAlreadyExists(string barcode, int productId = 0)
		{
			return this.InventoryProductDAO.ProductBarcodeAlreadyExists(barcode, productId);
		}
	}
}
