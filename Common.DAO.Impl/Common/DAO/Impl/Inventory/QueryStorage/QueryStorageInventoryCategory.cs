using System;

namespace TechnoPro.Common.DAO.Impl.Inventory.QueryStorage
{
	// Token: 0x020000BE RID: 190
	internal static class QueryStorageInventoryCategory
	{
		// Token: 0x04000277 RID: 631
		internal const string SQ_CATEGORIES_BY_CATALOG = "select * from InventoryV2_category where CatalogID=@catalogid";

		// Token: 0x04000278 RID: 632
		internal const string SQ_CATEGORY_BY_NAME = "select * from InventoryV2_Category where CategoryName=@categoryname";

		// Token: 0x04000279 RID: 633
		internal const string DQ_DELETE_ROOT_CATEGORY_BY_CATALOG_ID = "declare @categoryname as varchar(250)\r\n                set @categoryname=(Select top(1) CatalogName as CategoryName from InventoryV2_Catalog where CatalogID=@catalogid)\r\n\r\n                if not(@categoryname is NULL)\r\n\t                begin\r\n\t\t                delete InventoryV2_Category \r\n\t\t                where CatalogId=@catalogid \r\n\t\t                AND CategoryName = @categoryname \r\n\t\t                AND NOT EXISTS (SELECT 1 from InventoryV2_Category where CategoryName LIKE @categoryname + '.%')\r\n\t\t                AND NOT EXISTS (SELECT 1 from InventoryV2_Product p where p.CategoryName=@categoryname)\r\n\t                end";

		// Token: 0x0400027A RID: 634
		internal const string DQ_DELETE_EMPTY_CATEGORY_BY_NAME = "DELETE FROM InventoryV2_Category\r\n                where CategoryName=@categoryname \r\n                AND NOT EXISTS (SELECT 1 from InventoryV2_Category where CategoryName LIKE @categoryname + '.%')\r\n                AND NOT EXISTS (SELECT 1 from InventoryV2_Product p where p.CategoryName=@categoryname)";

		// Token: 0x0400027B RID: 635
		internal const string UQ_ASSIGN_CATEGORY_DYNAMIC_FORM = "UPDATE InventoryV2_Category\r\n                SET DynamicFormID=@dynamicformid\r\n                WHERE CategoryName=@categoryname OR (CategoryName like @categoryname+ '.%' AND (DynamicFormID is NULL OR DynamicFormID=0))";

		// Token: 0x0400027C RID: 636
		internal const string IQ_CATEGORY = "if not exists (select 1 from InventoryV2_Category where CategoryName=@categoryname)\r\n                begin\r\n\t\t\t\t\tdeclare @dynamicformid as int\r\n\t\t\t\t\tset @dynamicformid = (select top(1) DynamicFormID from InventoryV2_Category where CategoryName=@parentcategoryname)\r\n                    \r\n\t\t\t\t\tinsert into InventoryV2_Category (CategoryName, DynamicFormID, CatalogId)\r\n                    values (@categoryname, @dynamicformid, @catalogid)\r\n                end";

		// Token: 0x0400027D RID: 637
		internal const string IQ_CATEGORY_LEAVE = "if not exists (select 1 from InventoryV2_Category where CategoryName=@categoryname)\r\n                begin\r\n\t\t\t\t\tdeclare @dynamicformid2 as int\r\n\t\t\t\t\tif(@dynamicformid is null or @dynamicformid=0)\r\n\t\t\t\t\t\tbegin\r\n\t\t\t\t\t\t\tset @dynamicformid2 = (select top(1) DynamicFormID from InventoryV2_Category where CategoryName=@parentcategoryname)\r\n\t\t\t\t\t\tend\r\n\t\t\t\t\telse\r\n\t\t\t\t\t\tbegin\r\n\t\t\t\t\t\t\tset @dynamicformid2 = @dynamicformid\r\n\t\t\t\t\t\tend\r\n\t\t\t\t\t\r\n                    insert into InventoryV2_Category (CategoryName, DynamicFormID, CatalogId)\r\n                    values (@categoryname, @dynamicformid2, @catalogid)\r\n                end";

		// Token: 0x0400027E RID: 638
		internal const string IQ_CATEGORY_ROOT = "if not exists (select 1 from InventoryV2_Category where CategoryName=@categoryname)\r\n                begin\r\n\t\t\t\t\tinsert into InventoryV2_Category (CategoryName, DynamicFormID, CatalogId)\r\n                    values (@categoryname, NULL, @catalogid)\r\n                end";
	}
}
