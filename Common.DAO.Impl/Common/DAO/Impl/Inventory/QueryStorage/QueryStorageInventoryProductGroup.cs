using System;

namespace TechnoPro.Common.DAO.Impl.Inventory.QueryStorage
{
	// Token: 0x020000C3 RID: 195
	internal static class QueryStorageInventoryProductGroup
	{
		// Token: 0x040002C1 RID: 705
		internal const string SQ_GET_PRODUCT_GROUPS = "select * from InventoryV2_ProductGroup";

		// Token: 0x040002C2 RID: 706
		internal const string SQ_GET_PRODUCT_GROUP_BY_ID = "select * from InventoryV2_ProductGroup where ProductGroupID=@groupid";

		// Token: 0x040002C3 RID: 707
		internal const string UQ_UPDATE_PRODUCT_GROUP = "UPDATE [InventoryV2_ProductGroup]\r\n                SET GroupName=@groupname,\r\n\t                GroupNotes=@groupnotes\r\n                WHERE ProductGroupID=@groupid";

		// Token: 0x040002C4 RID: 708
		internal const string DQ_DELETE_PRODUCT_GROUP_BY_ID = "delete from InventoryV2_ProductGroup \r\n                where ProductGroupID=@productgroupid\r\n                AND NOT EXISTS (SELECT 1 from InventoryV2_Product p WHERE p.IsActive=1 AND p.GroupID=@productgroupid)";

		// Token: 0x040002C5 RID: 709
		internal const string IQ_CREATE_PRODUCT_GROUP = "INSERT INTO [InventoryV2_ProductGroup]\r\n                       ([GroupName]\r\n                       ,[GroupNotes])\r\n            VALUES\r\n                       (@groupname\r\n                       ,@groupnotes)\r\n\r\n            SET @productgroupid=scope_identity()";
	}
}
