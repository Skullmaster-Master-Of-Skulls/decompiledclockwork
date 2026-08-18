using System;

namespace TechnoPro.Common.DAO.Impl.Inventory.QueryStorage
{
	// Token: 0x020000C4 RID: 196
	internal static class QueryStorageInventoryProductStatus
	{
		// Token: 0x040002C6 RID: 710
		internal const string SQ_GET_PRODUCT_STATUS_BY_ID = "Select * from InventoryV2_ProductStatus where ProductStatusID=@productstatusid";

		// Token: 0x040002C7 RID: 711
		internal const string SQ_GET_PRODUCT_STATUS_LIST = "Select * from InventoryV2_ProductStatus";

		// Token: 0x040002C8 RID: 712
		internal const string IQ_CREATE_PRODUCT_STATUS = "INSERT INTO [InventoryV2_ProductStatus]\r\n                       ([ProductStatusName]\r\n                       ,[ProductStatusDescription])\r\n              VALUES\r\n                       (@productstatusname\r\n                       ,@productstatusdescription)\r\n\r\n            SET @productstatusid = scope_identity()";

		// Token: 0x040002C9 RID: 713
		internal const string UQ_UPDATE_PRODUCT_STATUS = "UPDATE [InventoryV2_ProductStatus]\r\n                SET [ProductStatusName] = @productstatusname\r\n                    ,[ProductStatusDescription] = @productstatusdescription\r\n                WHERE ProductStatusID=@productstatusid";
	}
}
