using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Inventory;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Inventory
{
	// Token: 0x02000047 RID: 71
	public class InventoryProductRestClientManager : BearerTokenRestProxy<IInventoryProductClientManager>, IInventoryProductClientManager, IWebService
	{
		// Token: 0x0600028E RID: 654 RVA: 0x00007BB1 File Offset: 0x00005DB1
		public InventoryProductRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00007BBB File Offset: 0x00005DBB
		public InventoryProductRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00007BC6 File Offset: 0x00005DC6
		public IList<InventoryProductDTO> GetProductsMatching(int workingCatalogId, string searchText, InventoryProductSearchByField searchByField = InventoryProductSearchByField.All, bool showOnlyLoanedProducts = false)
		{
			return base.GetMany<InventoryProductDTO>(string.Format("inventoryproduct/matching?searchtext={0}&catalogid={1}&searchbyfield={2}&showonlyloanedproducts={3}", new object[]
			{
				searchText,
				workingCatalogId,
				searchByField,
				showOnlyLoanedProducts
			}), true);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00007BFF File Offset: 0x00005DFF
		public InventoryProductDTO GetProductById(int workingCatalogId, Guid pUniqueId)
		{
			return base.Get<InventoryProductDTO>(string.Format("inventoryproduct/productid/{0}/catalogid/{1}", pUniqueId, workingCatalogId), true);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00007C1E File Offset: 0x00005E1E
		public InventoryProductDTO GetProductBySerialNumber(int workingCatalogId, string serialNumber)
		{
			return base.Get<InventoryProductDTO>(string.Format("inventoryproduct/productserialnumber/{0}/catalogid/{1}", serialNumber, workingCatalogId), true);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00007C38 File Offset: 0x00005E38
		public InventoryProductDTO GetProductByBarCode(int workingCatalogId, string barcode)
		{
			return base.Get<InventoryProductDTO>(string.Format("inventoryproduct/productbarcode/{0}/catalogid/{1}", barcode, workingCatalogId), true);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00007C52 File Offset: 0x00005E52
		public IList<InventoryProductDTO> GetProductsByCatalog(int catalogId)
		{
			return base.GetMany<InventoryProductDTO>(string.Format("inventoryproduct/catalogid/{0}", catalogId), true);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00007C6B File Offset: 0x00005E6B
		public IList<InventoryProductDTO> GetProductsByRootCategory(int workingCatalogId, string rootCategoryName)
		{
			return base.GetMany<InventoryProductDTO>(string.Format("inventoryproduct/rootcategory/{0}/catalogid/{1}", rootCategoryName, workingCatalogId), true);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00007C85 File Offset: 0x00005E85
		public IList<InventoryProductDTO> GetProductsByCategory(int workingCatalogId, string exactCategoryName)
		{
			return base.GetMany<InventoryProductDTO>(string.Format("inventoryproduct/category/{0}/catalogid/{1}", exactCategoryName, workingCatalogId), true);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00007C9F File Offset: 0x00005E9F
		public IList<InventoryProductDTO> GetProductsByGroup(int workingCatalogId, int groupId)
		{
			return base.GetMany<InventoryProductDTO>(string.Format("inventoryproduct/groupid/{0}/catalogid/{1}", groupId, workingCatalogId), true);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00007CBE File Offset: 0x00005EBE
		public IList<InventoryProductDTO> GetProductsByLoan(int workingCatalogId, int loanGroupId)
		{
			return base.GetMany<InventoryProductDTO>(string.Format("inventoryproduct/loanid/{0}/catalogid/{1}", loanGroupId, workingCatalogId), true);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00007CDD File Offset: 0x00005EDD
		public IList<InventoryProductDTO> GetProductsInReservationGroup(int workingCatalogId, int reservationGroupId)
		{
			return base.GetMany<InventoryProductDTO>(string.Format("inventoryproduct/reservationgroupid/{0}/catalogid/{1}", reservationGroupId, workingCatalogId), true);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00007CFC File Offset: 0x00005EFC
		public void UpdateProduct(InventoryProductDTO product)
		{
			base.Put<InventoryProductDTO>(product, "inventoryproduct");
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00007D0A File Offset: 0x00005F0A
		public Guid CreateProduct(InventoryProductDTO product)
		{
			return base.Post<InventoryProductDTO, Guid>(product, "inventoryproduct");
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00007D18 File Offset: 0x00005F18
		public bool DeleteProduct(Guid id)
		{
			base.Delete(string.Format("inventoryproduct/productid/{0}", id));
			return true;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00007D34 File Offset: 0x00005F34
		public IList<Guid> DeleteProducts(IList<Guid> productIds)
		{
			DeleteProductsReq deleteProductsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteProductsReq>();
			deleteProductsReq.ProductUniqueIds = productIds;
			return base.Post<DeleteProductsReq, IList<Guid>>(deleteProductsReq, "inventoryproduct/deleteproducts");
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00007D60 File Offset: 0x00005F60
		public void ChangeProductsCategory(string categoryName, IList<int> productIds)
		{
			ChangeProductsCategoryReq changeProductsCategoryReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ChangeProductsCategoryReq>();
			changeProductsCategoryReq.CategoryName = categoryName;
			changeProductsCategoryReq.Products = productIds;
			base.Post<ChangeProductsCategoryReq>(changeProductsCategoryReq, "inventoryproduct/changeproductscategory");
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00007D94 File Offset: 0x00005F94
		public void AssignProductToGroup(int workingCatalogId, Guid productUniqueId, int groupId)
		{
			AssignProductToGroupReq assignProductToGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AssignProductToGroupReq>();
			assignProductToGroupReq.ProductUniqueId = productUniqueId.ToString();
			assignProductToGroupReq.WorkingCatalogId = workingCatalogId;
			assignProductToGroupReq.GroupId = groupId;
			base.Post<AssignProductToGroupReq>(assignProductToGroupReq, "inventoryproduct/assignproducttogroup");
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00007DDC File Offset: 0x00005FDC
		public void AssignProductsToGroup(int workingCatalogId, IList<int> productIdList, int groupId)
		{
			AssignProductsToGroupReq assignProductsToGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AssignProductsToGroupReq>();
			assignProductsToGroupReq.WorkingCatalogId = workingCatalogId;
			assignProductsToGroupReq.ProductIdList = productIdList;
			assignProductsToGroupReq.GroupId = groupId;
			base.Post<AssignProductsToGroupReq>(assignProductsToGroupReq, "inventoryproduct/assignproductstogroup");
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00007E15 File Offset: 0x00006015
		public InventoryProductSnapshotDTO GetProductSnapshot(Guid productUniqueId, int loanId)
		{
			return base.Get<InventoryProductSnapshotDTO>(string.Format("inventoryproduct/productsnapshot/productid/{0}/loanid/{1}", productUniqueId, loanId), true);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00007E34 File Offset: 0x00006034
		public IList<InventoryProductSnapshotDTO> GetProductHistory(int productId, eInventoryProductSnapshotReason reason)
		{
			return base.GetMany<InventoryProductSnapshotDTO>(string.Format("inventoryproduct/producthistory/productid/{0}/reason/{1}", productId, reason), true);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00007E53 File Offset: 0x00006053
		public IList<InventoryProductSnapshotDTO> GetProductHistory(string barcode, eInventoryProductSnapshotReason reason)
		{
			return base.GetMany<InventoryProductSnapshotDTO>(string.Format("inventoryproduct/producthistory/barcode/{0}/reason/{1}", barcode, reason), true);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00007E70 File Offset: 0x00006070
		public IList<InventoryProductBookedTimeDTO> GetProductAvailability(Guid uniqueId, DateTime startDate, DateTime endDate, bool includeLoans = true, bool includeReservations = true, int loanId = 0, int reservationId = 0)
		{
			return base.GetMany<InventoryProductBookedTimeDTO>(string.Format("inventoryproduct/availability/productid/{0}/range/{1}/{2}/loanid/{3}/reservationid/{4}?includeloans={5}&includereservations={6}", new object[]
			{
				uniqueId,
				startDate,
				endDate,
				loanId,
				reservationId,
				includeLoans,
				includeReservations
			}), true);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00007ED7 File Offset: 0x000060D7
		public bool ProductBarcodeAlreadyExists(string barcode, int productId = 0)
		{
			return base.Get<bool>(string.Format("inventoryproduct/isbarcodeavailable/barcode/{0}/productid/{1}", barcode, productId), true);
		}
	}
}
