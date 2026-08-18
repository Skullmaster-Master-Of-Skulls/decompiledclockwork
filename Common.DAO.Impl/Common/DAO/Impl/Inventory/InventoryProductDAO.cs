using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Impl.Adapters;
using TechnoPro.Common.DAO.Impl.Inventory.Adapters;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DAO.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.DAO.Impl.Inventory
{
	// Token: 0x020000B8 RID: 184
	public class InventoryProductDAO : IInventoryProductDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600050F RID: 1295 RVA: 0x0002F243 File Offset: 0x0002D443
		public InventoryProductDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x0002F255 File Offset: 0x0002D455
		// (set) Token: 0x06000511 RID: 1297 RVA: 0x0002F25D File Offset: 0x0002D45D
		public OperationContext OpContext { get; set; }

		// Token: 0x06000512 RID: 1298 RVA: 0x0002F268 File Offset: 0x0002D468
		public Guid CreateProduct(InventoryProduct product, string barcodePrefix)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@productuniqueid", DbType.Guid, 0),
				databaseLayer.GetOutputParameter("@barcoderet", DbType.String, 100),
				databaseLayer.GetOutputParameter("@productdynamicdataid", DbType.Int32, 0),
				databaseLayer.GetParameter("@barcode", DbType.String, product.BarCode ?? string.Empty),
				databaseLayer.GetParameter("@productname", DbType.String, product.Name ?? string.Empty),
				databaseLayer.GetParameter("@serialnumber", DbType.String, product.SerialNumber ?? string.Empty),
				databaseLayer.GetParameter("@categoryname", DbType.String, product.CategoryName ?? string.Empty),
				databaseLayer.GetParameter("@productstatusid", DbType.Int32, (product.Status == null) ? 0 : product.Status.Id),
				databaseLayer.GetParameter("@productstatus", DbType.String, (product.Status != null) ? (product.Status.Name ?? string.Empty) : string.Empty),
				databaseLayer.GetParameter("@productdescription", DbType.String, product.Description ?? string.Empty),
				databaseLayer.GetParameter("@productnotes", DbType.String, product.Notes ?? string.Empty),
				databaseLayer.GetParameter("@thumbnail", DbType.Binary, (product.Thumbnail == null) ? DBNull.Value : product.Thumbnail.Serialize()),
				databaseLayer.GetParameter("@vendor", DbType.String, (product.Vendor == null || string.IsNullOrEmpty(product.Vendor.VendorName)) ? string.Empty : product.Vendor.VendorName),
				databaseLayer.GetParameter("@purchasedate", DbType.DateTime, (product.Vendor == null || product.Vendor.PurchaseDate == null) ? DBNull.Value : product.Vendor.PurchaseDate.Value),
				databaseLayer.GetParameter("@purchaseamount", DbType.Double, (product.Vendor == null) ? 0.0 : product.Vendor.PurchaseAmount),
				databaseLayer.GetParameter("@warrantyexpirationdate", DbType.DateTime, (product.Vendor == null || product.Vendor.WarrantyExpDate == null) ? DBNull.Value : product.Vendor.WarrantyExpDate.Value),
				databaseLayer.GetParameter("@purchaseinfo", DbType.String, (product.Vendor == null || string.IsNullOrEmpty(product.Vendor.PurchaseInfo)) ? string.Empty : product.Vendor.PurchaseInfo),
				databaseLayer.GetParameter("@locationid", DbType.Int32, (product.Location == null || product.Location.LocationId == 0) ? DBNull.Value : product.Location.Id),
				databaseLayer.GetParameter("@location", DbType.String, (product.Location != null) ? product.Location.ToString() : string.Empty),
				databaseLayer.GetParameter("@locationdate", DbType.DateTime, (product.LocationDatetime != null) ? product.LocationDatetime.Value : DBNull.Value),
				databaseLayer.GetParameter("@inchargepersonid", DbType.Int32, (product.InChargePerson != null) ? product.InChargePerson.Id : 0),
				databaseLayer.GetParameter("@groupid", DbType.Int32, (product.Group == null || product.Group.ProductGroupId == 0) ? DBNull.Value : product.Group.ProductGroupId),
				databaseLayer.GetParameter("@groupname", DbType.String, (product.Group == null) ? string.Empty : (product.Group.Name ?? string.Empty)),
				databaseLayer.GetParameter("@whomodifiedpersonid", DbType.Int32, this.OpContext.WhoAmI),
				databaseLayer.GetParameter("@barcodeprefix", DbType.String, barcodePrefix),
				databaseLayer.GetParameter("@accessories", DbType.Xml, (product.Accessories != null) ? product.Accessories.ToXml() : DBNull.Value)
			};
			databaseLayer.ExecuteStoredProcedure("sp_Inventory_CreateProduct", array);
			product.ProductDynamicDataId = ((array[2].Value is DBNull) ? 0 : Convert.ToInt32(array[2].Value));
			product.BarCode = Convert.ToString(array[1].Value);
			return product.UniqueId = ((array[0].Value is DBNull) ? Guid.Empty : ((Guid)array[0].Value));
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0002F76C File Offset: 0x0002D96C
		public IList<InventoryProduct> GetProductsMatching(string searchText, IList<string> allowedCategories, InventoryProductSearchByField searchByField = InventoryProductSearchByField.All, bool showOnlyLoanedProducts = false)
		{
			string text = searchText.ProccessSearchText();
			List<InventoryProduct> list = new List<InventoryProduct>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@searchText", DbType.String, searchText),
				databaseLayer.GetParameter("@allowedcategories", DbType.String, string.Join(",", allowedCategories.ToArray<string>()))
			};
			string query = showOnlyLoanedProducts ? "select top(100)\r\n                    p.ProductUniqueID, p.ProductName, p.SerialNumber, p.LoanID, p.ProductStatusID, ProductStatusName, ps.ProductStatusDescription,\r\n                    p.ProductDescription, p.ProductNotes, p.Thumbnail, p.Vendor, p.PurchaseDate, p.PurchaseAmount, p.WarrantyExpirationDate, p.PurchaseInfo,\r\n                    p.LocationID, loc.Campus, loc.Building, loc.RoomNumber, loc.Seat, loc.LocationNotes, p.LocationDate,\r\n                    pg.ProductGroupID, pg.GroupName, pg.GroupNotes, p.CategoryName, p.BarCode, p.ProductDynamicDataID, p.Accessories,\r\n                    p.InChargePersonID as personid, peo.firstname as firstname, peo.lastname as lastname, peo.middlename as middlename, peo.student_no as student_no, peog.mingroupid AS groupid\r\n            from InventoryV2_Product p\r\n            LEFT JOIN InventoryV2_ProductStatus ps ON p.ProductStatusID=ps.ProductStatusID\r\n            LEFT JOIN InventoryV2_Location loc ON p.LocationID=loc.LocationID\r\n            LEFT JOIN InventoryV2_ProductGroup pg ON p.GroupID=pg.ProductGroupID\r\n            LEFT JOIN people peo ON peo.personid=p.InChargePersonID\r\n            LEFT JOIN peoplemingroup peog ON peog.personid=p.InChargePersonID\r\n            where p.IsActive=1 AND p.LoanID is not NULL AND CategoryName in (select OrderID as CategoryName from SplitStrings2(@allowedcategories, ',')) \r\n                AND ( p.ProductName like '%' + @searchtext + '%'\r\n\t            OR    p.SerialNumber like '%' + @searchtext + '%'\r\n\t            OR    p.BarCode like '%' + @searchtext + '%'\r\n\t            OR    p.CategoryName like '%' + @searchtext + '%'\r\n\t            OR\t  pg.GroupName like '%' + @searchtext + '%')" : "select top(100)\r\n                    p.ProductUniqueID, p.ProductName, p.SerialNumber, p.LoanID, p.ProductStatusID, ProductStatusName, ps.ProductStatusDescription,\r\n                    p.ProductDescription, p.ProductNotes, p.Thumbnail, p.Vendor, p.PurchaseDate, p.PurchaseAmount, p.WarrantyExpirationDate, p.PurchaseInfo,\r\n                    p.LocationID, loc.Campus, loc.Building, loc.RoomNumber, loc.Seat, loc.LocationNotes, p.LocationDate,\r\n                    pg.ProductGroupID, pg.GroupName, pg.GroupNotes, p.CategoryName, p.BarCode, p.ProductDynamicDataID, p.Accessories,\r\n                    p.InChargePersonID as personid, peo.firstname as firstname, peo.lastname as lastname, peo.middlename as middlename, peo.student_no as student_no, peog.mingroupid AS groupid\r\n            from InventoryV2_Product p\r\n            LEFT JOIN InventoryV2_ProductStatus ps ON p.ProductStatusID=ps.ProductStatusID\r\n            LEFT JOIN InventoryV2_Location loc ON p.LocationID=loc.LocationID\r\n            LEFT JOIN InventoryV2_ProductGroup pg ON p.GroupID=pg.ProductGroupID\r\n            LEFT JOIN people peo ON peo.personid=p.InChargePersonID\r\n            LEFT JOIN peoplemingroup peog ON peog.personid=p.InChargePersonID\r\n            where p.IsActive=1 AND CategoryName in (select OrderID as CategoryName from SplitStrings2(@allowedcategories, ',')) \r\n                AND ( p.ProductName like '%' + @searchtext + '%'\r\n\t            OR    p.SerialNumber like '%' + @searchtext + '%'\r\n\t            OR    p.BarCode like '%' + @searchtext + '%'\r\n\t            OR    p.CategoryName like '%' + @searchtext + '%'\r\n\t            OR\t  pg.GroupName like '%' + @searchtext + '%')";
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader(query, parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						InventoryProduct productFromReader = InventoryProductDAO.GetProductFromReader(dataReader, this.OpContext, batchDecryptor);
						bool flag2 = productFromReader != null;
						if (flag2)
						{
							list.Add(productFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0002F870 File Offset: 0x0002DA70
		public InventoryProduct GetProductById(Guid uniqueid)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@productuniqueid", DbType.Guid, uniqueid);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select TOP(1) p.ProductUniqueID, p.ProductName, p.SerialNumber, p.LoanID, p.ProductStatusID, ProductStatusName, ps.ProductStatusDescription,\r\n\t                p.ProductDescription, p.ProductNotes, p.Thumbnail, p.Vendor, p.PurchaseDate, p.PurchaseAmount, p.WarrantyExpirationDate, p.PurchaseInfo,\r\n\t                p.LocationID, loc.Campus, loc.Building, loc.RoomNumber, loc.Seat, loc.LocationNotes, p.LocationDate,\r\n\t                pg.ProductGroupID, pg.GroupName, pg.GroupNotes, p.CategoryName, p.BarCode, p.ProductDynamicDataID, p.Accessories,\r\n                    p.InChargePersonID as personid, peo.firstname as firstname, peo.lastname as lastname, peo.middlename as middlename, peo.student_no as student_no, peog.mingroupid AS groupid\r\n            from InventoryV2_Product p\r\n            LEFT JOIN InventoryV2_ProductStatus ps ON p.ProductStatusID=ps.ProductStatusID\r\n            LEFT JOIN InventoryV2_Location loc ON p.LocationID=loc.LocationID\r\n            LEFT JOIN InventoryV2_ProductGroup pg ON p.GroupID=pg.ProductGroupID\r\n            LEFT JOIN people peo ON peo.personid=p.InChargePersonID\r\n            LEFT JOIN peoplemingroup peog ON peog.personid=p.InChargePersonID\r\n            where ProductUniqueID=@productuniqueid", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return InventoryProductDAO.GetProductFromReader(dataReader, this.OpContext, null);
				}
			}
			return null;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0002F90C File Offset: 0x0002DB0C
		public InventoryProduct GetProductById(int pId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@productid", DbType.Int32, pId);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select TOP(1) p.ProductUniqueID, p.ProductName, p.SerialNumber, p.LoanID, p.ProductStatusID, ProductStatusName, ps.ProductStatusDescription,\r\n\t                p.ProductDescription, p.ProductNotes, p.Thumbnail, p.Vendor, p.PurchaseDate, p.PurchaseAmount, p.WarrantyExpirationDate, p.PurchaseInfo,\r\n\t                p.LocationID, loc.Campus, loc.Building, loc.RoomNumber, loc.Seat, loc.LocationNotes, p.LocationDate,\r\n\t                pg.ProductGroupID, pg.GroupName, pg.GroupNotes, p.CategoryName, p.BarCode, p.ProductDynamicDataID, p.Accessories,\r\n                    p.InChargePersonID as personid, peo.firstname as firstname, peo.lastname as lastname, peo.middlename as middlename, peo.student_no as student_no, peog.mingroupid AS groupid\r\n            from InventoryV2_Product p\r\n            LEFT JOIN InventoryV2_ProductStatus ps ON p.ProductStatusID=ps.ProductStatusID\r\n            LEFT JOIN InventoryV2_Location loc ON p.LocationID=loc.LocationID\r\n            LEFT JOIN InventoryV2_ProductGroup pg ON p.GroupID=pg.ProductGroupID\r\n            LEFT JOIN people peo ON peo.personid=p.InChargePersonID\r\n            LEFT JOIN peoplemingroup peog ON peog.personid=p.InChargePersonID\r\n            where ProductDynamicDataID=@productid", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return InventoryProductDAO.GetProductFromReader(dataReader, this.OpContext, null);
				}
			}
			return null;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0002F9A8 File Offset: 0x0002DBA8
		public InventoryProduct GetProductById(Guid uniqueid, IList<string> allowedCategories)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@productuniqueid", DbType.Guid, uniqueid),
				databaseLayer.GetParameter("@allowedcategories", DbType.String, string.Join(",", allowedCategories.ToArray<string>()))
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select p.ProductUniqueID, p.ProductName, p.SerialNumber, p.LoanID, p.ProductStatusID, ProductStatusName, ps.ProductStatusDescription,\r\n\t                p.ProductDescription, p.ProductNotes, p.Thumbnail, p.Vendor, p.PurchaseDate, p.PurchaseAmount, p.WarrantyExpirationDate, p.PurchaseInfo,\r\n\t                p.LocationID, loc.Campus, loc.Building, loc.RoomNumber, loc.Seat, loc.LocationNotes, p.LocationDate,\r\n\t                pg.ProductGroupID, pg.GroupName, pg.GroupNotes, p.CategoryName, p.BarCode, p.ProductDynamicDataID, p.Accessories,\r\n                    p.InChargePersonID as personid, peo.firstname as firstname, peo.lastname as lastname, peo.middlename as middlename, peo.student_no as student_no, peog.mingroupid AS groupid\r\n            from InventoryV2_Product p\r\n            LEFT JOIN InventoryV2_ProductStatus ps ON p.ProductStatusID=ps.ProductStatusID\r\n            LEFT JOIN InventoryV2_Location loc ON p.LocationID=loc.LocationID\r\n            LEFT JOIN InventoryV2_ProductGroup pg ON p.GroupID=pg.ProductGroupID\r\n            LEFT JOIN people peo ON peo.personid=p.InChargePersonID\r\n            LEFT JOIN peoplemingroup peog ON peog.personid=p.InChargePersonID\r\n            where ProductUniqueID=@productuniqueid AND\r\n                    CategoryName in (select OrderID as CategoryName from SplitStrings2(@allowedcategories, ','))", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return InventoryProductDAO.GetProductFromReader(dataReader, this.OpContext, null);
				}
			}
			return null;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0002FA64 File Offset: 0x0002DC64
		public InventoryProduct GetProductBySerialNumber(string serialNumber, IList<string> allowedCategories)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@serialnumber", DbType.String, serialNumber),
				databaseLayer.GetParameter("@allowedcategories", DbType.String, string.Join(",", allowedCategories.ToArray<string>()))
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select TOP(1) p.ProductUniqueID, p.ProductName, p.SerialNumber, p.LoanID, p.ProductStatusID, ProductStatusName, ps.ProductStatusDescription,\r\n\t                p.ProductDescription, p.ProductNotes, p.Thumbnail, p.Vendor, p.PurchaseDate, p.PurchaseAmount, p.WarrantyExpirationDate, p.PurchaseInfo,\r\n\t                p.LocationID, loc.Campus, loc.Building, loc.RoomNumber, loc.Seat, loc.LocationNotes, p.LocationDate,\r\n\t                pg.ProductGroupID, pg.GroupName, pg.GroupNotes, p.CategoryName, p.BarCode, p.ProductDynamicDataID, p.Accessories,\r\n                    p.InChargePersonID as personid, peo.firstname as firstname, peo.lastname as lastname, peo.middlename as middlename, peo.student_no as student_no, peog.mingroupid AS groupid\r\n            from InventoryV2_Product p\r\n            LEFT JOIN InventoryV2_ProductStatus ps ON p.ProductStatusID=ps.ProductStatusID\r\n            LEFT JOIN InventoryV2_Location loc ON p.LocationID=loc.LocationID\r\n            LEFT JOIN InventoryV2_ProductGroup pg ON p.GroupID=pg.ProductGroupID\r\n            LEFT JOIN people peo ON peo.personid=p.InChargePersonID\r\n            LEFT JOIN peoplemingroup peog ON peog.personid=p.InChargePersonID\r\n            where SerialNumber=@serialnumber AND\r\n\t              CategoryName in (select OrderID as CategoryName from SplitStrings2(@allowedcategories, ','))", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return InventoryProductDAO.GetProductFromReader(dataReader, this.OpContext, null);
				}
			}
			return null;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0002FB18 File Offset: 0x0002DD18
		public InventoryProduct GetProductByBarCode(string barcode, IList<string> allowedCategories)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@barcode", DbType.String, barcode),
				databaseLayer.GetParameter("@allowedcategories", DbType.String, string.Join(",", allowedCategories.ToArray<string>()))
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select TOP(1) p.ProductUniqueID, p.ProductName, p.SerialNumber, p.LoanID, p.ProductStatusID, ProductStatusName, ps.ProductStatusDescription,\r\n\t                p.ProductDescription, p.ProductNotes, p.Thumbnail, p.Vendor, p.PurchaseDate, p.PurchaseAmount, p.WarrantyExpirationDate, p.PurchaseInfo,\r\n\t                p.LocationID, loc.Campus, loc.Building, loc.RoomNumber, loc.Seat, loc.LocationNotes, p.LocationDate,\r\n\t                pg.ProductGroupID, pg.GroupName, pg.GroupNotes, p.CategoryName, p.BarCode, p.ProductDynamicDataID, p.Accessories,\r\n                    p.InChargePersonID as personid, peo.firstname as firstname, peo.lastname as lastname, peo.middlename as middlename, peo.student_no as student_no, peog.mingroupid AS groupid\r\n            from InventoryV2_Product p\r\n            LEFT JOIN InventoryV2_ProductStatus ps ON p.ProductStatusID=ps.ProductStatusID\r\n            LEFT JOIN InventoryV2_Location loc ON p.LocationID=loc.LocationID\r\n            LEFT JOIN InventoryV2_ProductGroup pg ON p.GroupID=pg.ProductGroupID\r\n            LEFT JOIN people peo ON peo.personid=p.InChargePersonID\r\n            LEFT JOIN peoplemingroup peog ON peog.personid=p.InChargePersonID\r\n            where BarCode=@barcode AND\r\n\t              CategoryName in (select OrderID as CategoryName from SplitStrings2(@allowedcategories, ','))", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return InventoryProductDAO.GetProductFromReader(dataReader, this.OpContext, null);
				}
			}
			return null;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0002FBCC File Offset: 0x0002DDCC
		private InventoryProduct GetProductByBarCode(string barcode)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@barcode", DbType.String, barcode)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select TOP(1) p.ProductUniqueID, p.ProductName, p.SerialNumber, p.LoanID, p.ProductStatusID, ProductStatusName, ps.ProductStatusDescription,\r\n\t                p.ProductDescription, p.ProductNotes, p.Thumbnail, p.Vendor, p.PurchaseDate, p.PurchaseAmount, p.WarrantyExpirationDate, p.PurchaseInfo,\r\n\t                p.LocationID, loc.Campus, loc.Building, loc.RoomNumber, loc.Seat, loc.LocationNotes, p.LocationDate,\r\n\t                pg.ProductGroupID, pg.GroupName, pg.GroupNotes, p.CategoryName, p.BarCode, p.ProductDynamicDataID, p.Accessories,\r\n                    p.InChargePersonID as personid, peo.firstname as firstname, peo.lastname as lastname, peo.middlename as middlename, peo.student_no as student_no, peog.mingroupid AS groupid\r\n            from InventoryV2_Product p\r\n            LEFT JOIN InventoryV2_ProductStatus ps ON p.ProductStatusID=ps.ProductStatusID\r\n            LEFT JOIN InventoryV2_Location loc ON p.LocationID=loc.LocationID\r\n            LEFT JOIN InventoryV2_ProductGroup pg ON p.GroupID=pg.ProductGroupID\r\n            LEFT JOIN people peo ON peo.personid=p.InChargePersonID\r\n            LEFT JOIN peoplemingroup peog ON peog.personid=p.InChargePersonID\r\n            where BarCode=@barcode", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return InventoryProductDAO.GetProductFromReader(dataReader, this.OpContext, null);
				}
			}
			return null;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0002FC60 File Offset: 0x0002DE60
		public IList<InventoryProduct> GetProductsByRootCategory(string rootCategoryName)
		{
			List<InventoryProduct> list = new List<InventoryProduct>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@rootcategoryname", DbType.String, rootCategoryName);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select p.ProductUniqueID, p.ProductName, p.SerialNumber, p.LoanID, p.ProductStatusID, ProductStatusName, ps.ProductStatusDescription,\r\n\t                p.ProductDescription, p.ProductNotes, p.Thumbnail, p.Vendor, p.PurchaseDate, p.PurchaseAmount, p.WarrantyExpirationDate, p.PurchaseInfo,\r\n\t                p.LocationID, loc.Campus, loc.Building, loc.RoomNumber, loc.Seat, loc.LocationNotes, p.LocationDate,\r\n\t                pg.ProductGroupID, pg.GroupName, pg.GroupNotes, p.CategoryName, p.BarCode, p.ProductDynamicDataID, p.Accessories,\r\n                    p.InChargePersonID as personid, peo.firstname as firstname, peo.lastname as lastname, peo.middlename as middlename, peo.student_no as student_no, peog.mingroupid AS groupid\r\n            from InventoryV2_Product p\r\n            LEFT JOIN InventoryV2_ProductStatus ps ON p.ProductStatusID=ps.ProductStatusID\r\n            LEFT JOIN InventoryV2_Location loc ON p.LocationID=loc.LocationID\r\n            LEFT JOIN InventoryV2_ProductGroup pg ON p.GroupID=pg.ProductGroupID\r\n            LEFT JOIN people peo ON peo.personid=p.InChargePersonID\r\n            LEFT JOIN peoplemingroup peog ON peog.personid=p.InChargePersonID\r\n            where p.IsActive=1 AND (CategoryName=@rootcategoryname OR CategoryName LIKE @rootcategoryname + '.%')", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						InventoryProduct productFromReader = InventoryProductDAO.GetProductFromReader(dataReader, this.OpContext, batchDecryptor);
						bool flag2 = productFromReader != null;
						if (flag2)
						{
							list.Add(productFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0002FD28 File Offset: 0x0002DF28
		public IList<InventoryProduct> GetProductsByCategory(string exactCategoryName)
		{
			List<InventoryProduct> list = new List<InventoryProduct>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@categoryname", DbType.String, exactCategoryName);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select p.ProductUniqueID, p.ProductName, p.SerialNumber, p.LoanID, p.ProductStatusID, ProductStatusName, ps.ProductStatusDescription,\r\n\t                p.ProductDescription, p.ProductNotes, p.Thumbnail, p.Vendor, p.PurchaseDate, p.PurchaseAmount, p.WarrantyExpirationDate, p.PurchaseInfo,\r\n\t                p.LocationID, loc.Campus, loc.Building, loc.RoomNumber, loc.Seat, loc.LocationNotes, p.LocationDate,\r\n\t                pg.ProductGroupID, pg.GroupName, pg.GroupNotes, p.CategoryName, p.BarCode, p.ProductDynamicDataID, p.Accessories,\r\n                    p.InChargePersonID as personid, peo.firstname as firstname, peo.lastname as lastname, peo.middlename as middlename, peo.student_no as student_no, peog.mingroupid AS groupid\r\n            from InventoryV2_Product p\r\n            LEFT JOIN InventoryV2_ProductStatus ps ON p.ProductStatusID=ps.ProductStatusID\r\n            LEFT JOIN InventoryV2_Location loc ON p.LocationID=loc.LocationID\r\n            LEFT JOIN InventoryV2_ProductGroup pg ON p.GroupID=pg.ProductGroupID\r\n            LEFT JOIN people peo ON peo.personid=p.InChargePersonID\r\n            LEFT JOIN peoplemingroup peog ON peog.personid=p.InChargePersonID\r\n            where p.IsActive=1 AND CategoryName=@categoryname", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						InventoryProduct productFromReader = InventoryProductDAO.GetProductFromReader(dataReader, this.OpContext, batchDecryptor);
						bool flag2 = productFromReader != null;
						if (flag2)
						{
							list.Add(productFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0002FDF0 File Offset: 0x0002DFF0
		public IList<InventoryProduct> GetProductsByCategories(params string[] categories)
		{
			List<InventoryProduct> list = new List<InventoryProduct>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@categories", DbType.String, string.Join(",", categories.ToArray<string>()))
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select p.ProductUniqueID, p.ProductName, p.SerialNumber, p.LoanID, p.ProductStatusID, ProductStatusName, ps.ProductStatusDescription,\r\n\t                p.ProductDescription, p.ProductNotes, p.Thumbnail, p.Vendor, p.PurchaseDate, p.PurchaseAmount, p.WarrantyExpirationDate, p.PurchaseInfo,\r\n\t                p.LocationID, loc.Campus, loc.Building, loc.RoomNumber, loc.Seat, loc.LocationNotes, p.LocationDate,\r\n\t                pg.ProductGroupID, pg.GroupName, pg.GroupNotes, p.CategoryName, p.BarCode, p.ProductDynamicDataID, p.Accessories,\r\n                    p.InChargePersonID as personid, peo.firstname as firstname, peo.lastname as lastname, peo.middlename as middlename, peo.student_no as student_no, peog.mingroupid AS groupid\r\n            from InventoryV2_Product p\r\n            LEFT JOIN InventoryV2_ProductStatus ps ON p.ProductStatusID=ps.ProductStatusID\r\n            LEFT JOIN InventoryV2_Location loc ON p.LocationID=loc.LocationID\r\n            LEFT JOIN InventoryV2_ProductGroup pg ON p.GroupID=pg.ProductGroupID\r\n            LEFT JOIN people peo ON peo.personid=p.InChargePersonID\r\n            LEFT JOIN peoplemingroup peog ON peog.personid=p.InChargePersonID\r\n            where p.IsActive=1 AND CategoryName in (select OrderID as CategoryName from SplitStrings2(@categories, ','))", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						InventoryProduct productFromReader = InventoryProductDAO.GetProductFromReader(dataReader, this.OpContext, batchDecryptor);
						bool flag2 = productFromReader != null;
						if (flag2)
						{
							list.Add(productFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0002FEC4 File Offset: 0x0002E0C4
		public IList<InventoryProduct> GetProductsByLoan(int loanGroupId, IList<string> allowedCategories)
		{
			List<InventoryProduct> list = new List<InventoryProduct>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@loangroupid", DbType.Int32, loanGroupId),
				databaseLayer.GetParameter("@allowedcategories", DbType.String, string.Join(",", allowedCategories.ToArray<string>()))
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select p.ProductUniqueID, p.ProductName, p.SerialNumber, p.LoanID, p.ProductStatusID, ProductStatusName, ps.ProductStatusDescription,\r\n\t                p.ProductDescription, p.ProductNotes, p.Thumbnail, p.Vendor, p.PurchaseDate, p.PurchaseAmount, p.WarrantyExpirationDate, p.PurchaseInfo,\r\n\t                p.LocationID, loc.Campus, loc.Building, loc.RoomNumber, loc.Seat, loc.LocationNotes, p.LocationDate,\r\n\t                pg.ProductGroupID, pg.GroupName, pg.GroupNotes, p.CategoryName, p.BarCode, p.ProductDynamicDataID, p.Accessories,\r\n                    p.InChargePersonID as personid, peo.firstname as firstname, peo.lastname as lastname, peo.middlename as middlename, peo.student_no as student_no, peog.mingroupid AS groupid\r\n            from InventoryV2_Product p\r\n            INNER JOIN InventoryV2_ActiveLoan al ON al.ProductUniqueID=p.ProductUniqueID\r\n            LEFT JOIN InventoryV2_ProductStatus ps ON p.ProductStatusID=ps.ProductStatusID\r\n            LEFT JOIN InventoryV2_Location loc ON p.LocationID=loc.LocationID\r\n            LEFT JOIN InventoryV2_ProductGroup pg ON p.GroupID=pg.ProductGroupID\r\n            LEFT JOIN people peo ON peo.personid=p.InChargePersonID\r\n            LEFT JOIN peoplemingroup peog ON peog.personid=p.InChargePersonID\r\n            where p.IsActive=1 AND al.LoanGroupId=@loangroupid AND\r\n\t              CategoryName in (select OrderID as CategoryName from SplitStrings2(@allowedcategories, ','))", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						InventoryProduct productFromReader = InventoryProductDAO.GetProductFromReader(dataReader, this.OpContext, batchDecryptor);
						bool flag2 = productFromReader != null;
						if (flag2)
						{
							list.Add(productFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0002FFB0 File Offset: 0x0002E1B0
		public IList<InventoryProduct> GetProductsByGroup(int groupId, IList<string> allowedCategories)
		{
			List<InventoryProduct> list = new List<InventoryProduct>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@groupid", DbType.Int32, groupId),
				databaseLayer.GetParameter("@allowedcategories", DbType.String, string.Join(",", allowedCategories.ToArray<string>()))
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select p.ProductUniqueID, p.ProductName, p.SerialNumber, p.LoanID, p.ProductStatusID, ProductStatusName, ps.ProductStatusDescription,\r\n\t                p.ProductDescription, p.ProductNotes, p.Thumbnail, p.Vendor, p.PurchaseDate, p.PurchaseAmount, p.WarrantyExpirationDate, p.PurchaseInfo,\r\n\t                p.LocationID, loc.Campus, loc.Building, loc.RoomNumber, loc.Seat, loc.LocationNotes, p.LocationDate,\r\n\t                pg.ProductGroupID, pg.GroupName, pg.GroupNotes, p.CategoryName, p.BarCode, p.ProductDynamicDataID, p.Accessories,\r\n                    p.InChargePersonID as personid, peo.firstname as firstname, peo.lastname as lastname, peo.middlename as middlename, peo.student_no as student_no, peog.mingroupid AS groupid\r\n            from InventoryV2_Product p\r\n            LEFT JOIN InventoryV2_ProductStatus ps ON p.ProductStatusID=ps.ProductStatusID\r\n            LEFT JOIN InventoryV2_Location loc ON p.LocationID=loc.LocationID\r\n            LEFT JOIN InventoryV2_ProductGroup pg ON p.GroupID=pg.ProductGroupID\r\n            LEFT JOIN people peo ON peo.personid=p.InChargePersonID\r\n            LEFT JOIN peoplemingroup peog ON peog.personid=p.InChargePersonID\r\n            where p.IsActive=1 AND GroupID=@groupid AND\r\n\t              CategoryName in (select OrderID as CategoryName from SplitStrings2(@allowedcategories, ','))", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						InventoryProduct productFromReader = InventoryProductDAO.GetProductFromReader(dataReader, this.OpContext, batchDecryptor);
						bool flag2 = productFromReader != null;
						if (flag2)
						{
							list.Add(productFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0003009C File Offset: 0x0002E29C
		public IList<InventoryProduct> GetProductsInReservationGroup(int reservationGroupId, IList<string> allowedCategories)
		{
			List<InventoryProduct> list = new List<InventoryProduct>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@reservationgroupid", DbType.Int32, reservationGroupId),
				databaseLayer.GetParameter("@allowedcategories", DbType.String, string.Join(",", allowedCategories.ToArray<string>()))
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select p.ProductUniqueID, p.ProductName, p.SerialNumber, p.LoanID, p.ProductStatusID, ProductStatusName, ps.ProductStatusDescription,\r\n\t                p.ProductDescription, p.ProductNotes, p.Thumbnail, p.Vendor, p.PurchaseDate, p.PurchaseAmount, p.WarrantyExpirationDate, p.PurchaseInfo,\r\n\t                p.LocationID, loc.Campus, loc.Building, loc.RoomNumber, loc.Seat, loc.LocationNotes, p.LocationDate,\r\n\t                pg.ProductGroupID, pg.GroupName, pg.GroupNotes, p.CategoryName, p.BarCode, p.ProductDynamicDataID, p.Accessories,\r\n                    p.InChargePersonID as personid, peo.firstname as firstname, peo.lastname as lastname, peo.middlename as middlename, peo.student_no as student_no, peog.mingroupid AS groupid\r\n            from InventoryV2_Product p\r\n            INNER JOIN InventoryV2_Reservation r ON r.ProductUniqueID=p.ProductUniqueID\r\n            LEFT JOIN InventoryV2_ProductStatus ps ON p.ProductStatusID=ps.ProductStatusID\r\n            LEFT JOIN InventoryV2_Location loc ON p.LocationID=loc.LocationID\r\n            LEFT JOIN InventoryV2_ProductGroup pg ON p.GroupID=pg.ProductGroupID\r\n            LEFT JOIN people peo ON peo.personid=p.InChargePersonID\r\n            LEFT JOIN peoplemingroup peog ON peog.personid=p.InChargePersonID\r\n            where p.IsActive=1 AND r.IsCompleted=0 AND r.ReservationGroupId=@reservationgroupid AND\r\n\t              CategoryName in (select OrderID as CategoryName from SplitStrings2(@allowedcategories, ','))", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						InventoryProduct productFromReader = InventoryProductDAO.GetProductFromReader(dataReader, this.OpContext, batchDecryptor);
						bool flag2 = productFromReader != null;
						if (flag2)
						{
							list.Add(productFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00030188 File Offset: 0x0002E388
		public void UpdateProduct(InventoryProduct product, string barcodePrefix)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@barcoderet", DbType.String, 100),
				databaseLayer.GetParameter("@barcode", DbType.String, product.BarCode ?? string.Empty),
				databaseLayer.GetParameter("@productuniqueid", DbType.Guid, product.UniqueId),
				databaseLayer.GetParameter("@productdynamicdataid", DbType.Int32, product.ProductDynamicDataId),
				databaseLayer.GetParameter("@productname", DbType.String, product.Name ?? string.Empty),
				databaseLayer.GetParameter("@serialnumber", DbType.String, product.SerialNumber ?? string.Empty),
				databaseLayer.GetParameter("@categoryname", DbType.String, product.CategoryName ?? string.Empty),
				databaseLayer.GetParameter("@productstatusid", DbType.Int32, (product.Status == null) ? 0 : product.Status.Id),
				databaseLayer.GetParameter("@productstatus", DbType.String, (product.Status != null) ? (product.Status.Name ?? string.Empty) : string.Empty),
				databaseLayer.GetParameter("@productdescription", DbType.String, product.Description ?? string.Empty),
				databaseLayer.GetParameter("@productnotes", DbType.String, product.Notes ?? string.Empty),
				databaseLayer.GetParameter("@thumbnail", DbType.Binary, (product.Thumbnail == null) ? DBNull.Value : product.Thumbnail.Serialize()),
				databaseLayer.GetParameter("@vendor", DbType.String, (product.Vendor == null || string.IsNullOrEmpty(product.Vendor.VendorName)) ? string.Empty : product.Vendor.VendorName),
				databaseLayer.GetParameter("@purchasedate", DbType.DateTime, (product.Vendor == null || product.Vendor.PurchaseDate == null) ? DBNull.Value : product.Vendor.PurchaseDate.Value),
				databaseLayer.GetParameter("@purchaseamount", DbType.Double, (product.Vendor == null) ? 0.0 : product.Vendor.PurchaseAmount),
				databaseLayer.GetParameter("@warrantyexpirationdate", DbType.DateTime, (product.Vendor == null || product.Vendor.WarrantyExpDate == null) ? DBNull.Value : product.Vendor.WarrantyExpDate.Value),
				databaseLayer.GetParameter("@purchaseinfo", DbType.String, (product.Vendor == null || string.IsNullOrEmpty(product.Vendor.PurchaseInfo)) ? string.Empty : product.Vendor.PurchaseInfo),
				databaseLayer.GetParameter("@locationid", DbType.Int32, (product.Location == null || product.Location.LocationId == 0) ? DBNull.Value : product.Location.Id),
				databaseLayer.GetParameter("@location", DbType.String, (product.Location != null) ? product.Location.ToString() : string.Empty),
				databaseLayer.GetParameter("@locationdate", DbType.DateTime, (product.LocationDatetime != null) ? product.LocationDatetime.Value : DBNull.Value),
				databaseLayer.GetParameter("@inchargepersonid", DbType.Int32, (product.InChargePerson != null) ? product.InChargePerson.Id : 0),
				databaseLayer.GetParameter("@groupid", DbType.Int32, (product.Group == null || product.Group.ProductGroupId == 0) ? DBNull.Value : product.Group.Id),
				databaseLayer.GetParameter("@groupname", DbType.String, (product.Group == null) ? string.Empty : (product.Group.Name ?? string.Empty)),
				databaseLayer.GetParameter("@whomodifiedpersonid", DbType.Int32, this.OpContext.WhoAmI),
				databaseLayer.GetParameter("@barcodeprefix", DbType.String, barcodePrefix),
				databaseLayer.GetParameter("@accessories", DbType.Xml, (product.Accessories != null) ? product.Accessories.ToXml() : DBNull.Value)
			};
			databaseLayer.ExecuteStoredProcedure("sp_Inventory_UpdateProduct", array);
			product.BarCode = Convert.ToString(array[0].Value);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00030648 File Offset: 0x0002E848
		public bool DeleteProduct(Guid uniqueid)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@productuniqueid", DbType.Guid, uniqueid);
			InventoryProduct productById = this.GetProductById(uniqueid);
			DbTransaction dbTransaction = databaseLayer.BeginDbTransaction();
			bool flag = databaseLayer.ExecuteNonQueryTransaction("if not exists (select 1 from InventoryV2_Reservation r\r\n\t\t\t\t                inner join InventoryV2_ReservationGroup rg on rg.ReservationGroupId=r.ReservationGroupId\r\n\t\t\t\t                where r.ProductUniqueID=@productuniqueid and r.IsCompleted=0 and rg.ReservationEndDate > GETDATE())\r\n                begin\r\n\t                update InventoryV2_Product set IsActive=0, Barcode='' where ProductUniqueID=@productuniqueid AND LoanID is NULL\r\n                end", dbTransaction, new DbParameter[]
			{
				parameter
			}) > 0;
			bool flag2 = flag && productById != null;
			if (flag2)
			{
				this.UpdateProductSnapshot(productById, dbTransaction, eInventoryProductSnapshotReason.Product_Deleted);
			}
			databaseLayer.CommitDbTransaction(dbTransaction);
			return flag;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x000306D8 File Offset: 0x0002E8D8
		public void ChangeProductsCategory(string categoryName, IList<int> pIds)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[2];
			array[0] = databaseLayer.GetParameter("@productids", DbType.String, string.Join(",", (from p in pIds
			select p.ToString()).ToArray<string>()));
			array[1] = databaseLayer.GetParameter("@categoryname", DbType.String, categoryName);
			DbParameter[] parameters = array;
			databaseLayer.ExecuteNonQuery("update InventoryV2_Product\r\n                    set CategoryName=@categoryname\r\n                    where ProductDynamicDataID in (select OrderID as ProductDynamicDataID from SplitOrderIDs(@productids, ','))", parameters);
			foreach (int pId in pIds)
			{
				InventoryProduct productById = this.GetProductById(pId);
				bool flag = productById != null;
				if (flag)
				{
					this.UpdateProductSnapshot(productById, eInventoryProductSnapshotReason.Properties_Changed);
				}
			}
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x000307C0 File Offset: 0x0002E9C0
		public bool AssignProductToGroup(Guid productUniqueId, int groupId, IList<string> allowedCategories)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("productuniqueid", DbType.Guid, productUniqueId),
				databaseLayer.GetParameter("@groupid", DbType.Int32, groupId),
				databaseLayer.GetParameter("@allowedcategories", DbType.String, string.Join(",", allowedCategories.ToArray<string>()))
			};
			DbTransaction dbTransaction = databaseLayer.BeginDbTransaction();
			bool result = databaseLayer.ExecuteNonQueryTransaction("UPDATE InventoryV2_Product\r\n                SET GroupID=@groupid\r\n                WHERE ProductUniqueId=@productuniqueid AND\r\n\t                  CategoryName in (select OrderID as CategoryName from SplitStrings2(@allowedcategories, ','))", dbTransaction, parameters) > 0;
			InventoryProduct productById = this.GetProductById(productUniqueId);
			bool flag = productById != null;
			if (flag)
			{
				this.UpdateProductSnapshot(productById, dbTransaction, eInventoryProductSnapshotReason.Properties_Changed);
			}
			databaseLayer.CommitDbTransaction(dbTransaction);
			return result;
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00030884 File Offset: 0x0002EA84
		public void AssignProductsToGroup(IList<int> productIdList, int groupId, IList<string> allowedCategories)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("productidlist", DbType.String, productIdList.CommaSeparatedValuesWithoutSpace<int>()),
				databaseLayer.GetParameter("@groupid", DbType.Int32, (groupId > 0) ? groupId : DBNull.Value),
				databaseLayer.GetParameter("@allowedcategories", DbType.String, string.Join(",", allowedCategories.ToArray<string>()))
			};
			bool flag = databaseLayer.ExecuteNonQuery("UPDATE InventoryV2_Product\r\n                SET GroupID=@groupid\r\n                WHERE ProductDynamicDataID IN (select OrderID as ProductDynamicDataID from SplitOrderIDs(@productidlist, ',')) AND\r\n\t                  CategoryName in (select OrderID as CategoryName from SplitStrings2(@allowedcategories, ','))", parameters) > 0;
			bool flag2 = flag;
			if (flag2)
			{
				foreach (int pId in productIdList)
				{
					InventoryProduct productById = this.GetProductById(pId);
					bool flag3 = productById != null;
					if (flag3)
					{
						this.UpdateProductSnapshot(productById, eInventoryProductSnapshotReason.Properties_Changed);
					}
				}
			}
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00030980 File Offset: 0x0002EB80
		public int CreateProductSnapshot(InventoryProductSnapshot pSnapshot)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@productsnapshotid", DbType.Int32, 0),
				databaseLayer.GetParameter("@productuniqueid", DbType.Guid, pSnapshot.ProductUniqueId),
				databaseLayer.GetParameter("@productdynamicdataid", DbType.Int32, pSnapshot.ProductDynamicDataId),
				databaseLayer.GetParameter("@productname", DbType.String, pSnapshot.ProductName ?? string.Empty),
				databaseLayer.GetParameter("@barcode", DbType.String, pSnapshot.BarCode ?? string.Empty),
				databaseLayer.GetParameter("@serialnumber", DbType.String, pSnapshot.SerialNumber ?? string.Empty),
				databaseLayer.GetParameter("@categoryname", DbType.String, pSnapshot.CategoryName ?? string.Empty),
				databaseLayer.GetParameter("@productstatus", DbType.String, pSnapshot.ProductStatus ?? string.Empty),
				databaseLayer.GetParameter("@productlocation", DbType.String, pSnapshot.Location ?? string.Empty),
				databaseLayer.GetParameter("@locationdate", DbType.DateTime, (pSnapshot.LocationDate != null) ? pSnapshot.LocationDate.Value : DBNull.Value),
				databaseLayer.GetParameter("@inchargepersonid", DbType.Int32, (pSnapshot.InChargePerson != null) ? pSnapshot.InChargePerson.Id : 0),
				databaseLayer.GetParameter("@groupname", DbType.String, pSnapshot.GroupName ?? string.Empty),
				databaseLayer.GetParameter("@loanid", DbType.Int32, pSnapshot.ReturnLoanId),
				databaseLayer.GetParameter("@loangroupid", DbType.Int32, pSnapshot.LoanGroupId),
				databaseLayer.GetParameter("@loaneddate", DbType.DateTime, pSnapshot.LoanedDate),
				databaseLayer.GetParameter("@duedate", DbType.DateTime, pSnapshot.DueDate),
				databaseLayer.GetParameter("@returneddate", DbType.DateTime, (pSnapshot.ReturnedDate != null) ? pSnapshot.ReturnedDate.Value : DBNull.Value),
				databaseLayer.GetParameter("@loanedtopersonid", DbType.Int32, (pSnapshot.LoanedTo != null) ? pSnapshot.LoanedTo.PersonId : 0),
				databaseLayer.GetParameter("@loanlocation", DbType.String, pSnapshot.LoanLocation ?? string.Empty),
				databaseLayer.GetParameter("@wholoanedpersonid", DbType.Int32, (pSnapshot.WhoLoaned != null) ? pSnapshot.WhoLoaned.PersonId : 0),
				databaseLayer.GetParameter("@whoreturnedid", DbType.Int32, (pSnapshot.WhoReturned != null) ? pSnapshot.WhoReturned.PersonId : 0),
				databaseLayer.GetParameter("@loannotes", DbType.String, pSnapshot.ReturnedNotes ?? string.Empty),
				databaseLayer.GetParameter("@returnedstatus", DbType.String, pSnapshot.ReturnedStatus ?? string.Empty),
				databaseLayer.GetParameter("@returnednotes", DbType.String, pSnapshot.ReturnedNotes ?? string.Empty),
				databaseLayer.GetParameter("@whomodifiedpersonid", DbType.Int32, this.OpContext.WhoAmI),
				databaseLayer.GetParameter("@reason", DbType.String, pSnapshot.Reason.ToString()),
				databaseLayer.GetParameter("@accessories", DbType.Xml, (pSnapshot.Accessories != null) ? pSnapshot.Accessories.ToXml() : DBNull.Value)
			};
			databaseLayer.ExecuteNonQuery("insert into [InventoryV2_ProductSnapshot]\r\n                            (ProductUniqueID\r\n                            ,ProductDynamicDataID\r\n                            ,ProductName\r\n                            ,BarCode\r\n                            ,SerialNumber\r\n                            ,CategoryName\r\n                            ,Location\r\n                            ,LocationDate\r\n                            ,InChargePersonID\r\n                            ,GroupName\r\n                            ,ProductStatus\r\n\t\t\t\t            ,ReturnLoanID\r\n\t\t\t\t            ,LoanGroupId\r\n\t\t\t\t            ,LoanedDate\r\n\t\t\t\t            ,DueDate\r\n\t\t\t\t            ,ReturnedDate\r\n\t\t\t\t            ,LoanedToPersonId\r\n\t\t\t\t            ,LoanLocation\r\n\t\t\t\t            ,WhoLoanedPersonId\r\n\t\t\t\t            ,WhoReturnedPersonId\r\n\t\t\t\t            ,LoanNotes\r\n\t\t\t\t            ,ReturnedStatus\r\n\t\t\t\t            ,ReturnedNotes\r\n\t\t\t\t            ,WhoModifiedPersonId\r\n                            ,Reason\r\n                            ,Accessories)\r\n\t            Values\r\n                            (@productuniqueid\r\n                            ,@productdynamicdataid\r\n                            ,@productname\r\n                            ,@barcode\r\n                            ,@serialnumber\r\n                            ,@categoryname\r\n                            ,@productlocation\r\n                            ,@locationdate\r\n                            ,@inchargepersonid\r\n                            ,@groupname\r\n                            ,@productstatus\r\n\t\t\t\t            ,@loanid\r\n\t\t\t\t            ,@loangroupid\r\n\t\t\t\t            ,@loaneddate\r\n\t\t\t\t            ,@duedate\r\n\t\t\t\t            ,@returneddate\r\n\t\t\t\t            ,@loanedtopersonid\r\n\t\t\t\t            ,@loanlocation\r\n\t\t\t\t            ,@WhoLoanedPersonId\r\n\t\t\t\t            ,@whoreturnedid\r\n\t\t\t\t            ,@loannotes\r\n\t\t\t\t            ,@returnedstatus\r\n\t\t\t\t            ,@returnednotes\r\n\t\t\t\t            ,@whomodifiedpersonid\r\n\t\t\t\t            ,@reason\r\n                            ,@accessories)\r\n\r\n            set @productsnapshotid = SCOPE_IDENTITY()", array);
			return pSnapshot.ProductSnapshotId = ((array[0].Value is DBNull) ? 0 : ((int)array[0].Value));
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00030D94 File Offset: 0x0002EF94
		public InventoryProductSnapshot GetProductSnapshot(Guid productUniqueId, int loanId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@productuniqueid", DbType.Guid, productUniqueId),
				databaseLayer.GetParameter("@loanid", DbType.Int32, loanId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select top(1) ProductSnapshotID, ProductUniqueID, ProductDynamicDataID, ProductName, Barcode, SerialNumber, CategoryName, Location, LocationDate,\r\n\t\t            p.InChargePersonID as icpersonid, pic.firstname as icfirstname, pic.lastname as iclastname, pic.middlename as icmiddlename, pic.student_no as icstudent_no, pgic.mingroupid AS icgroupid,\r\n\t\t            GroupName, ProductStatus, ReturnLoanID, LoanGroupId, LoanedDate, DueDate, ReturnedDate,\r\n\t\t            p.WhoLoanedPersonId as wlpersonid, pwl.firstname as wlfirstname, pwl.lastname as wllastname, pwl.middlename as wlmiddlename, pwl.student_no as wlstudent_no, pgwl.mingroupid AS wlgroupid,\r\n\t\t            p.WhoReturnedPersonId as wrpersonid, pwr.firstname as wrfirstname, pwr.lastname as wrlastname, pwr.middlename as wrmiddlename, pwr.student_no as wrstudent_no, pgwr.mingroupid AS wrgroupid,\r\n\t\t            LoanLocation, LoanNotes, ReturnedStatus, ReturnedNotes,\r\n\t\t            p.LoanedToPersonId as ltpersonid, plt.firstname as ltfirstname, plt.lastname as ltlastname, plt.middlename as ltmiddlename, plt.student_no as ltstudent_no, pglt.mingroupid AS ltgroupid,\r\n\t\t            p.WhoModifiedPersonId as wmpersonid, pwm.firstname as wmfirstname, pwm.lastname as wmlastname, pwm.middlename as wmmiddlename, pwm.student_no as wmstudent_no, pgwm.mingroupid AS wmgroupid,\r\n\t\t            ModifiedDate, Reason, Accessories\r\n            from [InventoryV2_ProductSnapshot] p\r\n            LEFT JOIN people pic ON pic.personid=p.InChargePersonID\r\n            LEFT JOIN peoplemingroup pgic ON pgic.personid=p.InChargePersonID\r\n            LEFT JOIN people pwl ON pwl.personid=p.WhoLoanedPersonId\r\n            LEFT JOIN peoplemingroup pgwl ON pgwl.personid=p.WhoLoanedPersonId\r\n            LEFT JOIN people pwr ON pwr.personid=p.WhoReturnedPersonId\r\n            LEFT JOIN peoplemingroup pgwr ON pgwr.personid=p.WhoReturnedPersonId\r\n            LEFT JOIN people plt ON plt.personid=p.LoanedToPersonId\r\n            LEFT JOIN peoplemingroup pglt ON pglt.personid=p.LoanedToPersonId\r\n            LEFT JOIN people pwm ON pwm.personid=p.WhoModifiedPersonId\r\n            LEFT JOIN peoplemingroup pgwm ON pgwm.personid=p.WhoModifiedPersonId\r\n            where ProductUniqueID=@productuniqueid and ReturnLoanID=@loanid\r\n            order by ModifiedDate", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetProductSnapshotFromReader(dataReader, null);
				}
			}
			return null;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00030E40 File Offset: 0x0002F040
		public InventoryProductSnapshot GetProductSnapshot(Guid productUniqueId, int loanId, eInventoryProductSnapshotReason reason)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@productuniqueid", DbType.Guid, productUniqueId),
				databaseLayer.GetParameter("@loanid", DbType.Int32, loanId),
				databaseLayer.GetParameter("@reason", DbType.String, reason.ToString())
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select top(1) ProductSnapshotID, ProductUniqueID, ProductDynamicDataID, ProductName, Barcode, SerialNumber, CategoryName, Location, LocationDate,\r\n\t\t            p.InChargePersonID as icpersonid, pic.firstname as icfirstname, pic.lastname as iclastname, pic.middlename as icmiddlename, pic.student_no as icstudent_no, pgic.mingroupid AS icgroupid,\r\n\t\t            GroupName, ProductStatus, ReturnLoanID, LoanGroupId, LoanedDate, DueDate, ReturnedDate,\r\n\t\t            p.WhoLoanedPersonId as wlpersonid, pwl.firstname as wlfirstname, pwl.lastname as wllastname, pwl.middlename as wlmiddlename, pwl.student_no as wlstudent_no, pgwl.mingroupid AS wlgroupid,\r\n\t\t            p.WhoReturnedPersonId as wrpersonid, pwr.firstname as wrfirstname, pwr.lastname as wrlastname, pwr.middlename as wrmiddlename, pwr.student_no as wrstudent_no, pgwr.mingroupid AS wrgroupid,\r\n\t\t            LoanLocation, LoanNotes, ReturnedStatus, ReturnedNotes,\r\n\t\t            p.LoanedToPersonId as ltpersonid, plt.firstname as ltfirstname, plt.lastname as ltlastname, plt.middlename as ltmiddlename, plt.student_no as ltstudent_no, pglt.mingroupid AS ltgroupid,\r\n\t\t            p.WhoModifiedPersonId as wmpersonid, pwm.firstname as wmfirstname, pwm.lastname as wmlastname, pwm.middlename as wmmiddlename, pwm.student_no as wmstudent_no, pgwm.mingroupid AS wmgroupid,\r\n\t\t            ModifiedDate, Reason, Accessories\r\n            from [InventoryV2_ProductSnapshot] p\r\n            LEFT JOIN people pic ON pic.personid=p.InChargePersonID\r\n            LEFT JOIN peoplemingroup pgic ON pgic.personid=p.InChargePersonID\r\n            LEFT JOIN people pwl ON pwl.personid=p.WhoLoanedPersonId\r\n            LEFT JOIN peoplemingroup pgwl ON pgwl.personid=p.WhoLoanedPersonId\r\n            LEFT JOIN people pwr ON pwr.personid=p.WhoReturnedPersonId\r\n            LEFT JOIN peoplemingroup pgwr ON pgwr.personid=p.WhoReturnedPersonId\r\n            LEFT JOIN people plt ON plt.personid=p.LoanedToPersonId\r\n            LEFT JOIN peoplemingroup pglt ON pglt.personid=p.LoanedToPersonId\r\n            LEFT JOIN people pwm ON pwm.personid=p.WhoModifiedPersonId\r\n            LEFT JOIN peoplemingroup pgwm ON pgwm.personid=p.WhoModifiedPersonId\r\n            where ProductUniqueID=@productuniqueid and ReturnLoanID=@loanid and Reason=@reason\r\n            order by ModifiedDate", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetProductSnapshotFromReader(dataReader, null);
				}
			}
			return null;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00030F08 File Offset: 0x0002F108
		public InventoryProductSnapshot GetProductSnapshotByLoanGroup(Guid productUniqueId, int loanGroupId, eInventoryProductSnapshotReason reason)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@productuniqueid", DbType.Guid, productUniqueId),
				databaseLayer.GetParameter("@loangroupid", DbType.Int32, loanGroupId),
				databaseLayer.GetParameter("@reason", DbType.String, reason.ToString())
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select top(1) ProductSnapshotID, ProductUniqueID, ProductDynamicDataID, ProductName, Barcode, SerialNumber, CategoryName, Location, LocationDate,\r\n\t\t            p.InChargePersonID as icpersonid, pic.firstname as icfirstname, pic.lastname as iclastname, pic.middlename as icmiddlename, pic.student_no as icstudent_no, pgic.mingroupid AS icgroupid,\r\n\t\t            GroupName, ProductStatus, ReturnLoanID, LoanGroupId, LoanedDate, DueDate, ReturnedDate,\r\n\t\t            p.WhoLoanedPersonId as wlpersonid, pwl.firstname as wlfirstname, pwl.lastname as wllastname, pwl.middlename as wlmiddlename, pwl.student_no as wlstudent_no, pgwl.mingroupid AS wlgroupid,\r\n\t\t            p.WhoReturnedPersonId as wrpersonid, pwr.firstname as wrfirstname, pwr.lastname as wrlastname, pwr.middlename as wrmiddlename, pwr.student_no as wrstudent_no, pgwr.mingroupid AS wrgroupid,\r\n\t\t            LoanLocation, LoanNotes, ReturnedStatus, ReturnedNotes,\r\n\t\t            p.LoanedToPersonId as ltpersonid, plt.firstname as ltfirstname, plt.lastname as ltlastname, plt.middlename as ltmiddlename, plt.student_no as ltstudent_no, pglt.mingroupid AS ltgroupid,\r\n\t\t            p.WhoModifiedPersonId as wmpersonid, pwm.firstname as wmfirstname, pwm.lastname as wmlastname, pwm.middlename as wmmiddlename, pwm.student_no as wmstudent_no, pgwm.mingroupid AS wmgroupid,\r\n\t\t            ModifiedDate, Reason, Accessories\r\n            from [InventoryV2_ProductSnapshot] p\r\n            LEFT JOIN people pic ON pic.personid=p.InChargePersonID\r\n            LEFT JOIN peoplemingroup pgic ON pgic.personid=p.InChargePersonID\r\n            LEFT JOIN people pwl ON pwl.personid=p.WhoLoanedPersonId\r\n            LEFT JOIN peoplemingroup pgwl ON pgwl.personid=p.WhoLoanedPersonId\r\n            LEFT JOIN people pwr ON pwr.personid=p.WhoReturnedPersonId\r\n            LEFT JOIN peoplemingroup pgwr ON pgwr.personid=p.WhoReturnedPersonId\r\n            LEFT JOIN people plt ON plt.personid=p.LoanedToPersonId\r\n            LEFT JOIN peoplemingroup pglt ON pglt.personid=p.LoanedToPersonId\r\n            LEFT JOIN people pwm ON pwm.personid=p.WhoModifiedPersonId\r\n            LEFT JOIN peoplemingroup pgwm ON pgwm.personid=p.WhoModifiedPersonId\r\n            where ProductUniqueID=@productuniqueid and LoanGroupId=@loangroupid and Reason=@reason\r\n            order by ModifiedDate", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetProductSnapshotFromReader(dataReader, null);
				}
			}
			return null;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00030FD0 File Offset: 0x0002F1D0
		public IList<InventoryProductSnapshot> GetProductHistory(int productId, eInventoryProductSnapshotReason reason)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			List<InventoryProductSnapshot> list = new List<InventoryProductSnapshot>();
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@productid", DbType.Int32, productId),
				databaseLayer.GetParameter("@reason", DbType.String, reason.MaskToString<eInventoryProductSnapshotReason>())
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select OrderID as Reason into #temp from SplitStrings(@reason)\r\n\r\n                select ProductSnapshotID, ProductUniqueID, ProductDynamicDataID, ProductName, Barcode, SerialNumber, CategoryName, Location, LocationDate,\r\n\t\t                            p.InChargePersonID as icpersonid, pic.firstname as icfirstname, pic.lastname as iclastname, pic.middlename as icmiddlename, pic.student_no as icstudent_no, pgic.mingroupid AS icgroupid,\r\n\t\t                            GroupName, ProductStatus, ReturnLoanID, LoanGroupId, LoanedDate, DueDate, ReturnedDate,\r\n\t\t                            p.WhoLoanedPersonId as wlpersonid, pwl.firstname as wlfirstname, pwl.lastname as wllastname, pwl.middlename as wlmiddlename, pwl.student_no as wlstudent_no, pgwl.mingroupid AS wlgroupid,\r\n\t\t                            p.WhoReturnedPersonId as wrpersonid, pwr.firstname as wrfirstname, pwr.lastname as wrlastname, pwr.middlename as wrmiddlename, pwr.student_no as wrstudent_no, pgwr.mingroupid AS wrgroupid,\r\n\t\t                            LoanLocation, LoanNotes, ReturnedStatus, ReturnedNotes,\r\n\t\t                            p.LoanedToPersonId as ltpersonid, plt.firstname as ltfirstname, plt.lastname as ltlastname, plt.middlename as ltmiddlename, plt.student_no as ltstudent_no, pglt.mingroupid AS ltgroupid,\r\n\t\t                            p.WhoModifiedPersonId as wmpersonid, pwm.firstname as wmfirstname, pwm.lastname as wmlastname, pwm.middlename as wmmiddlename, pwm.student_no as wmstudent_no, pgwm.mingroupid AS wmgroupid,\r\n\t\t                            ModifiedDate, Reason, Accessories\r\n                            from [InventoryV2_ProductSnapshot] p\r\n                            LEFT JOIN people pic ON pic.personid=p.InChargePersonID\r\n                            LEFT JOIN peoplemingroup pgic ON pgic.personid=p.InChargePersonID\r\n                            LEFT JOIN people pwl ON pwl.personid=p.WhoLoanedPersonId\r\n                            LEFT JOIN peoplemingroup pgwl ON pgwl.personid=p.WhoLoanedPersonId\r\n                            LEFT JOIN people pwr ON pwr.personid=p.WhoReturnedPersonId\r\n                            LEFT JOIN peoplemingroup pgwr ON pgwr.personid=p.WhoReturnedPersonId\r\n                            LEFT JOIN people plt ON plt.personid=p.LoanedToPersonId\r\n                            LEFT JOIN peoplemingroup pglt ON pglt.personid=p.LoanedToPersonId\r\n                            LEFT JOIN people pwm ON pwm.personid=p.WhoModifiedPersonId\r\n                            LEFT JOIN peoplemingroup pgwm ON pgwm.personid=p.WhoModifiedPersonId\r\n                            where p.ProductDynamicDataID=@productid and Reason in (select Reason from #temp)\r\n\r\n                drop table #temp", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						InventoryProductSnapshot productSnapshotFromReader = this.GetProductSnapshotFromReader(dataReader, batchDecryptor);
						bool flag2 = productSnapshotFromReader != null;
						if (flag2)
						{
							list.Add(productSnapshotFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x000310B0 File Offset: 0x0002F2B0
		public IList<InventoryProductSnapshot> GetProductHistory(Guid productUniqueId, eInventoryProductSnapshotReason reason)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			List<InventoryProductSnapshot> list = new List<InventoryProductSnapshot>();
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@productuniqueid", DbType.Guid, productUniqueId),
				databaseLayer.GetParameter("@reason", DbType.String, reason.MaskToString<eInventoryProductSnapshotReason>())
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select OrderID as Reason into #temp from SplitStrings(@reason)\r\n\r\n                select ProductSnapshotID, ProductUniqueID, ProductDynamicDataID, ProductName, Barcode, SerialNumber, CategoryName, Location, LocationDate,\r\n\t\t                            p.InChargePersonID as icpersonid, pic.firstname as icfirstname, pic.lastname as iclastname, pic.middlename as icmiddlename, pic.student_no as icstudent_no, pgic.mingroupid AS icgroupid,\r\n\t\t                            GroupName, ProductStatus, ReturnLoanID, LoanGroupId, LoanedDate, DueDate, ReturnedDate,\r\n\t\t                            p.WhoLoanedPersonId as wlpersonid, pwl.firstname as wlfirstname, pwl.lastname as wllastname, pwl.middlename as wlmiddlename, pwl.student_no as wlstudent_no, pgwl.mingroupid AS wlgroupid,\r\n\t\t                            p.WhoReturnedPersonId as wrpersonid, pwr.firstname as wrfirstname, pwr.lastname as wrlastname, pwr.middlename as wrmiddlename, pwr.student_no as wrstudent_no, pgwr.mingroupid AS wrgroupid,\r\n\t\t                            LoanLocation, LoanNotes, ReturnedStatus, ReturnedNotes,\r\n\t\t                            p.LoanedToPersonId as ltpersonid, plt.firstname as ltfirstname, plt.lastname as ltlastname, plt.middlename as ltmiddlename, plt.student_no as ltstudent_no, pglt.mingroupid AS ltgroupid,\r\n\t\t                            p.WhoModifiedPersonId as wmpersonid, pwm.firstname as wmfirstname, pwm.lastname as wmlastname, pwm.middlename as wmmiddlename, pwm.student_no as wmstudent_no, pgwm.mingroupid AS wmgroupid,\r\n\t\t                            ModifiedDate, Reason, Accessories\r\n                            from [InventoryV2_ProductSnapshot] p\r\n                            LEFT JOIN people pic ON pic.personid=p.InChargePersonID\r\n                            LEFT JOIN peoplemingroup pgic ON pgic.personid=p.InChargePersonID\r\n                            LEFT JOIN people pwl ON pwl.personid=p.WhoLoanedPersonId\r\n                            LEFT JOIN peoplemingroup pgwl ON pgwl.personid=p.WhoLoanedPersonId\r\n                            LEFT JOIN people pwr ON pwr.personid=p.WhoReturnedPersonId\r\n                            LEFT JOIN peoplemingroup pgwr ON pgwr.personid=p.WhoReturnedPersonId\r\n                            LEFT JOIN people plt ON plt.personid=p.LoanedToPersonId\r\n                            LEFT JOIN peoplemingroup pglt ON pglt.personid=p.LoanedToPersonId\r\n                            LEFT JOIN people pwm ON pwm.personid=p.WhoModifiedPersonId\r\n                            LEFT JOIN peoplemingroup pgwm ON pgwm.personid=p.WhoModifiedPersonId\r\n                            where p.ProductUniqueID=@productuniqueid and Reason in (select Reason from #temp)\r\n\r\n                drop table #temp", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						InventoryProductSnapshot productSnapshotFromReader = this.GetProductSnapshotFromReader(dataReader, batchDecryptor);
						bool flag2 = productSnapshotFromReader != null;
						if (flag2)
						{
							list.Add(productSnapshotFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00031190 File Offset: 0x0002F390
		public IList<InventoryProductSnapshot> GetProductHistory(string barcode, eInventoryProductSnapshotReason reason)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			List<InventoryProductSnapshot> list = new List<InventoryProductSnapshot>();
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@barcode", DbType.String, barcode),
				databaseLayer.GetParameter("@reason", DbType.String, reason.MaskToString<eInventoryProductSnapshotReason>())
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select OrderID as Reason into #temp from SplitStrings(@reason)\r\n\r\n            select ProductSnapshotID, ProductUniqueID, ProductDynamicDataID, ProductName, Barcode, SerialNumber, CategoryName, Location, LocationDate,\r\n\t\t                        p.InChargePersonID as icpersonid, pic.firstname as icfirstname, pic.lastname as iclastname, pic.middlename as icmiddlename, pic.student_no as icstudent_no, pgic.mingroupid AS icgroupid,\r\n\t\t                        GroupName, ProductStatus, ReturnLoanID, LoanGroupId, LoanedDate, DueDate, ReturnedDate,\r\n\t\t                        p.WhoLoanedPersonId as wlpersonid, pwl.firstname as wlfirstname, pwl.lastname as wllastname, pwl.middlename as wlmiddlename, pwl.student_no as wlstudent_no, pgwl.mingroupid AS wlgroupid,\r\n\t\t                        p.WhoReturnedPersonId as wrpersonid, pwr.firstname as wrfirstname, pwr.lastname as wrlastname, pwr.middlename as wrmiddlename, pwr.student_no as wrstudent_no, pgwr.mingroupid AS wrgroupid,\r\n\t\t                        LoanLocation, LoanNotes, ReturnedStatus, ReturnedNotes,\r\n\t\t                        p.LoanedToPersonId as ltpersonid, plt.firstname as ltfirstname, plt.lastname as ltlastname, plt.middlename as ltmiddlename, plt.student_no as ltstudent_no, pglt.mingroupid AS ltgroupid,\r\n\t\t                        p.WhoModifiedPersonId as wmpersonid, pwm.firstname as wmfirstname, pwm.lastname as wmlastname, pwm.middlename as wmmiddlename, pwm.student_no as wmstudent_no, pgwm.mingroupid AS wmgroupid,\r\n\t\t                        ModifiedDate, Reason, Accessories\r\n                        from [InventoryV2_ProductSnapshot] p\r\n                        LEFT JOIN people pic ON pic.personid=p.InChargePersonID\r\n                        LEFT JOIN peoplemingroup pgic ON pgic.personid=p.InChargePersonID\r\n                        LEFT JOIN people pwl ON pwl.personid=p.WhoLoanedPersonId\r\n                        LEFT JOIN peoplemingroup pgwl ON pgwl.personid=p.WhoLoanedPersonId\r\n                        LEFT JOIN people pwr ON pwr.personid=p.WhoReturnedPersonId\r\n                        LEFT JOIN peoplemingroup pgwr ON pgwr.personid=p.WhoReturnedPersonId\r\n                        LEFT JOIN people plt ON plt.personid=p.LoanedToPersonId\r\n                        LEFT JOIN peoplemingroup pglt ON pglt.personid=p.LoanedToPersonId\r\n                        LEFT JOIN people pwm ON pwm.personid=p.WhoModifiedPersonId\r\n                        LEFT JOIN peoplemingroup pgwm ON pgwm.personid=p.WhoModifiedPersonId\r\n                        where p.BarCode=@barcode and Reason in (select Reason from #temp)\r\n\r\n            drop table #temp", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						InventoryProductSnapshot productSnapshotFromReader = this.GetProductSnapshotFromReader(dataReader, batchDecryptor);
						bool flag2 = productSnapshotFromReader != null;
						if (flag2)
						{
							list.Add(productSnapshotFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0003126C File Offset: 0x0002F46C
		public IList<InventoryProductBookedTime> GetProductAvailability(Guid uniqueId, DateTime startDate, DateTime endDate, bool includeLoans = true, bool includeReservations = true, int loanId = 0, int reservationId = 0)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			List<InventoryProductBookedTime> list = new List<InventoryProductBookedTime>();
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@productuniqueid", DbType.Guid, uniqueId),
				databaseLayer.GetParameter("@sdate", DbType.DateTime, startDate),
				databaseLayer.GetParameter("@edate", DbType.DateTime, endDate),
				databaseLayer.GetParameter("@includereservations", DbType.Boolean, includeReservations),
				databaseLayer.GetParameter("@includeloans", DbType.Boolean, includeLoans),
				databaseLayer.GetParameter("@reservationid", DbType.Int32, reservationId),
				databaseLayer.GetParameter("@loanid", DbType.Int32, loanId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select r.ProductUniqueId, r.ReservationID as [Id], rg.ReservationStartDate as [StartDate], rg.ReservationEndDate as [EndDate], 'Reservation' as BookedType,\r\n\t\t                rg.WhoMadeReservationId as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n\t\t                rg.WhoReservedStaffPersonId as frompersonid, pfrom.firstname as fromfirstname, pfrom.lastname as fromlastname, pfrom.middlename as frommiddlename, pfrom.student_no as fromstudent_no, pgfrom.mingroupid AS fromgroupid\r\n                from InventoryV2_Reservation r\r\n                inner join InventoryV2_ReservationGroup rg on rg.ReservationGroupId=r.ReservationGroupId\r\n                LEFT JOIN people pto ON pto.personid=rg.WhoMadeReservationId\r\n                LEFT JOIN peoplemingroup pgto ON pgto.personid=rg.WhoMadeReservationId\r\n                LEFT JOIN people pfrom ON pfrom.personid=rg.WhoReservedStaffPersonId\r\n                LEFT JOIN peoplemingroup pgfrom ON pgfrom.personid=rg.WhoReservedStaffPersonId\r\n                where @includereservations=1 AND ((@reservationid=0 OR r.ReservationID <> @reservationid) AND (r.IsCompleted=0 and r.ProductUniqueId=@productuniqueid and (@sdate < rg.ReservationEndDate and @edate > rg.ReservationStartDate)))\r\n                UNION\r\n                select l.ProductUniqueID, l.LoanID as [Id], lg.LoanedDate as [StartDate], lg.DueDate as [EndDate], 'Active_Loan' as BookedType,\r\n\t\t                lg.LoanedToID as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n\t\t                lg.WhoLoanedID as frompersonid, pfrom.firstname as fromfirstname, pfrom.lastname as fromlastname, pfrom.middlename as frommiddlename, pfrom.student_no as fromstudent_no, pgfrom.mingroupid AS fromgroupid\r\n                from InventoryV2_ActiveLoan l\r\n                inner join InventoryV2_LoanGroup lg on lg.LoanGroupId=l.LoanGroupId\r\n                LEFT JOIN people pto ON pto.personid=lg.LoanedToID\r\n                LEFT JOIN peoplemingroup pgto ON pgto.personid=lg.LoanedToID\r\n                LEFT JOIN people pfrom ON pfrom.personid=lg.WhoLoanedID\r\n                LEFT JOIN peoplemingroup pgfrom ON pgfrom.personid=lg.WhoLoanedID\r\n                where @includeloans=1 AND ((@loanid=0 OR l.LoanID <> @loanid) AND (l.ProductUniqueID=@productUniqueId and (@sdate < lg.DueDate and @edate > lg.LoanedDate)))", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						InventoryProductBookedTime productBookedTimeFromReader = this.GetProductBookedTimeFromReader(dataReader, batchDecryptor);
						bool flag2 = productBookedTimeFromReader != null;
						if (flag2)
						{
							list.Add(productBookedTimeFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x000313B8 File Offset: 0x0002F5B8
		public bool ProductBarcodeAlreadyExists(string barcode, int productId = 0)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@barcode", DbType.String, barcode),
				databaseLayer.GetParameter("@productid", DbType.Int32, productId)
			};
			object obj = databaseLayer.ExecuteScalar("select BarCode from InventoryV2_Product where Barcode=@barcode AND (@productid = 0 OR ProductDynamicDataID <> @productid)", parameters);
			return obj != null && !Convert.IsDBNull(obj) && !string.IsNullOrEmpty((string)obj);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0003143C File Offset: 0x0002F63C
		private InventoryProductBookedTime GetProductBookedTimeFromReader(IDataReader record, IBatchDecryptor decryptor)
		{
			return new InventoryProductBookedTime
			{
				Id = Convert.ToInt32(record["Id"]),
				ProductUniqueId = (Guid)record["ProductUniqueId"],
				StartDate = (DateTime)record["StartDate"],
				EndDate = (DateTime)record["EndDate"],
				From = PeopleDAO.GetPersonFromReader("from", record, this.OpContext, decryptor),
				To = PeopleDAO.GetPersonFromReader("to", record, this.OpContext, decryptor),
				BookingType = (InventoryProductBookingType)Enum.Parse(typeof(InventoryProductBookingType), (string)record["BookedType"])
			};
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00031510 File Offset: 0x0002F710
		private InventoryProductSnapshot GetProductSnapshotFromReader(IDataReader record, IBatchDecryptor decryptor = null)
		{
			return new InventoryProductSnapshot
			{
				ProductSnapshotId = Convert.ToInt32(record["ProductSnapShotId"]),
				ProductUniqueId = (Guid)record["ProductUniqueID"],
				ProductDynamicDataId = Convert.ToInt32(record["ProductDynamicDataID"]),
				ProductName = Convert.ToString(record["ProductName"]),
				SerialNumber = Convert.ToString(record["SerialNumber"]),
				BarCode = Convert.ToString(record["Barcode"]),
				CategoryName = Convert.ToString(record["CategoryName"]),
				Location = Convert.ToString(record["Location"]),
				LocationDate = ((record["LocationDate"] is DBNull) ? null : ((DateTime?)record["LocationDate"])),
				InChargePerson = PeopleDAO.GetPersonFromReader("ic", record, this.OpContext, decryptor),
				GroupName = Convert.ToString(record["GroupName"]),
				ProductStatus = Convert.ToString(record["ProductStatus"]),
				ReturnLoanId = ((record["ReturnLoanID"] is DBNull) ? 0 : Convert.ToInt32(record["ReturnLoanID"])),
				LoanGroupId = ((record["LoanGroupId"] is DBNull) ? 0 : Convert.ToInt32(record["LoanGroupId"])),
				LoanedDate = ((record["LoanedDate"] is DBNull) ? null : ((DateTime?)record["LoanedDate"])),
				DueDate = ((record["DueDate"] is DBNull) ? null : ((DateTime?)record["DueDate"])),
				ReturnedDate = ((record["ReturnedDate"] is DBNull) ? null : ((DateTime?)record["ReturnedDate"])),
				WhoLoaned = PeopleDAO.GetPersonFromReader("wl", record, this.OpContext, decryptor),
				WhoReturned = PeopleDAO.GetPersonFromReader("wr", record, this.OpContext, decryptor),
				LoanLocation = Convert.ToString(record["LoanLocation"]),
				LoanNotes = Convert.ToString(record["LoanNotes"]),
				ReturnedStatus = Convert.ToString(record["ReturnedStatus"]),
				ReturnedNotes = Convert.ToString(record["ReturnedNotes"]),
				LoanedTo = PeopleDAO.GetPersonFromReader("lt", record, this.OpContext, decryptor),
				WhoModified = PeopleDAO.GetPersonFromReader("wm", record, this.OpContext, decryptor),
				ModifiedDate = (DateTime)record["ModifiedDate"],
				Reason = (eInventoryProductSnapshotReason)Enum.Parse(typeof(eInventoryProductSnapshotReason), Convert.ToString(record["Reason"])),
				Accessories = ((record["Accessories"] is DBNull) ? null : ((string)record["Accessories"]).ToAccessoryList())
			};
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00031884 File Offset: 0x0002FA84
		private int UpdateProductSnapshot(InventoryProduct product, DbTransaction dbTransaction, eInventoryProductSnapshotReason reason)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@productsnapshotid", DbType.Int32, 0),
				databaseLayer.GetParameter("@productuniqueid", DbType.Guid, product.UniqueId),
				databaseLayer.GetParameter("@productdynamicdataid", DbType.Int32, product.ProductDynamicDataId),
				databaseLayer.GetParameter("@productname", DbType.String, product.Name ?? string.Empty),
				databaseLayer.GetParameter("@barcode", DbType.String, product.BarCode ?? string.Empty),
				databaseLayer.GetParameter("@serialnumber", DbType.String, product.SerialNumber ?? string.Empty),
				databaseLayer.GetParameter("@categoryname", DbType.String, product.CategoryName ?? string.Empty),
				databaseLayer.GetParameter("@location", DbType.String, (product.Location != null) ? product.Location.ToString() : string.Empty),
				databaseLayer.GetParameter("@locationdate", DbType.DateTime, (product.LocationDatetime != null) ? product.LocationDatetime.Value : DBNull.Value),
				databaseLayer.GetParameter("@inchargepersonid", DbType.Int32, (product.InChargePerson != null) ? product.InChargePerson.Id : 0),
				databaseLayer.GetParameter("@groupname", DbType.String, (product.Group != null) ? (product.Group.Name ?? string.Empty) : string.Empty),
				databaseLayer.GetParameter("@productstatus", DbType.String, (product.Status != null) ? (product.Status.Name ?? string.Empty) : string.Empty),
				databaseLayer.GetParameter("@whomodifiedpersonid", DbType.Int32, this.OpContext.WhoAmI),
				databaseLayer.GetParameter("@reason", DbType.String, reason.ToString()),
				databaseLayer.GetParameter("@accessories", DbType.Xml, (product.Accessories != null) ? product.Accessories.ToXml() : DBNull.Value)
			};
			databaseLayer.ExecuteNonQueryTransaction("insert into [InventoryV2_ProductSnapshot]\r\n                            (ProductUniqueID\r\n                            ,ProductDynamicDataID\r\n                            ,ProductName\r\n                            ,BarCode\r\n                            ,SerialNumber\r\n                            ,CategoryName\r\n                            ,Location\r\n                            ,LocationDate\r\n                            ,InChargePersonID\r\n                            ,GroupName\r\n                            ,ProductStatus\r\n                            ,WhoModifiedPersonId\r\n                            ,Reason\r\n                            ,Accessories)\r\n            Values\r\n                            (@productuniqueid\r\n                            ,@productdynamicdataid\r\n                            ,@productname\r\n                            ,@barcode\r\n                            ,@serialnumber\r\n                            ,@categoryname\r\n                            ,@location\r\n                            ,@locationdate\r\n                            ,@inchargepersonid\r\n                            ,@groupname\r\n                            ,@productstatus\r\n                            ,@whomodifiedpersonid\r\n                            ,@reason\r\n                            ,@accessories)\r\n            set @productsnapshotid = SCOPE_IDENTITY()", dbTransaction, array);
			return (array[0].Value is DBNull) ? 0 : ((int)array[0].Value);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00031B08 File Offset: 0x0002FD08
		private int UpdateProductSnapshot(InventoryProduct product, eInventoryProductSnapshotReason reason)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@productsnapshotid", DbType.Int32, 0),
				databaseLayer.GetParameter("@productuniqueid", DbType.Guid, product.UniqueId),
				databaseLayer.GetParameter("@productdynamicdataid", DbType.Int32, product.ProductDynamicDataId),
				databaseLayer.GetParameter("@productname", DbType.String, product.Name ?? string.Empty),
				databaseLayer.GetParameter("@barcode", DbType.String, product.BarCode ?? string.Empty),
				databaseLayer.GetParameter("@serialnumber", DbType.String, product.SerialNumber ?? string.Empty),
				databaseLayer.GetParameter("@categoryname", DbType.String, product.CategoryName ?? string.Empty),
				databaseLayer.GetParameter("@location", DbType.String, (product.Location != null) ? product.Location.ToString() : string.Empty),
				databaseLayer.GetParameter("@locationdate", DbType.DateTime, (product.LocationDatetime != null) ? product.LocationDatetime.Value : DBNull.Value),
				databaseLayer.GetParameter("@inchargepersonid", DbType.Int32, (product.InChargePerson != null) ? product.InChargePerson.Id : 0),
				databaseLayer.GetParameter("@groupname", DbType.String, (product.Group != null) ? (product.Group.Name ?? string.Empty) : string.Empty),
				databaseLayer.GetParameter("@productstatus", DbType.String, (product.Status != null) ? (product.Status.Name ?? string.Empty) : string.Empty),
				databaseLayer.GetParameter("@whomodifiedpersonid", DbType.Int32, this.OpContext.WhoAmI),
				databaseLayer.GetParameter("@reason", DbType.String, reason.ToString()),
				databaseLayer.GetParameter("@accessories", DbType.Xml, (product.Accessories != null) ? product.Accessories.ToXml() : DBNull.Value)
			};
			databaseLayer.ExecuteNonQuery("insert into [InventoryV2_ProductSnapshot]\r\n                            (ProductUniqueID\r\n                            ,ProductDynamicDataID\r\n                            ,ProductName\r\n                            ,BarCode\r\n                            ,SerialNumber\r\n                            ,CategoryName\r\n                            ,Location\r\n                            ,LocationDate\r\n                            ,InChargePersonID\r\n                            ,GroupName\r\n                            ,ProductStatus\r\n                            ,WhoModifiedPersonId\r\n                            ,Reason\r\n                            ,Accessories)\r\n            Values\r\n                            (@productuniqueid\r\n                            ,@productdynamicdataid\r\n                            ,@productname\r\n                            ,@barcode\r\n                            ,@serialnumber\r\n                            ,@categoryname\r\n                            ,@location\r\n                            ,@locationdate\r\n                            ,@inchargepersonid\r\n                            ,@groupname\r\n                            ,@productstatus\r\n                            ,@whomodifiedpersonid\r\n                            ,@reason\r\n                            ,@accessories)\r\n            set @productsnapshotid = SCOPE_IDENTITY()", array);
			return (array[0].Value is DBNull) ? 0 : ((int)array[0].Value);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00031D8C File Offset: 0x0002FF8C
		private static InventoryProduct GetProductFromReader(IDataReader record, OperationContext opContext, IBatchDecryptor decryptor = null)
		{
			return new InventoryProduct
			{
				UniqueId = (Guid)record["ProductUniqueID"],
				Name = Convert.ToString(record["ProductName"]),
				SerialNumber = Convert.ToString(record["SerialNumber"]),
				IsLoaned = !(record["LoanID"] is DBNull),
				Status = InventoryProductStatusDAO.GetProductStatusFromReader(record),
				Description = Convert.ToString(record["ProductDescription"]),
				Notes = Convert.ToString(record["ProductNotes"]),
				Thumbnail = ((record["Thumbnail"] is DBNull) ? null : ((byte[])record["Thumbnail"]).Deserialize()),
				Vendor = (string.IsNullOrEmpty(Convert.ToString(record["Vendor"])) ? null : new InventoryVendorInfo
				{
					VendorName = Convert.ToString(record["Vendor"]),
					PurchaseDate = ((record["PurchaseDate"] is DBNull) ? null : ((DateTime?)record["PurchaseDate"])),
					PurchaseAmount = (double)record["PurchaseAmount"],
					WarrantyExpDate = ((record["WarrantyExpirationDate"] is DBNull) ? null : ((DateTime?)record["WarrantyExpirationDate"])),
					PurchaseInfo = Convert.ToString(record["PurchaseInfo"])
				}),
				Location = InventoryLocationDAO.GetLocationFromReader(record),
				Group = ((record["ProductGroupID"] is DBNull) ? null : InventoryGroupDAO.GetGroupFromReader(record)),
				LocationDatetime = ((record["LocationDate"] is DBNull) ? null : ((DateTime?)record["LocationDate"])),
				InChargePerson = PeopleDAO.GetPersonFromReader("", record, opContext, decryptor),
				CategoryName = Convert.ToString(record["CategoryName"]),
				BarCode = Convert.ToString(record["BarCode"]),
				ProductDynamicDataId = Convert.ToInt32(record["ProductDynamicDataID"]),
				Accessories = ((record["Accessories"] is DBNull) ? null : ((string)record["Accessories"]).ToAccessoryList())
			};
		}
	}
}
