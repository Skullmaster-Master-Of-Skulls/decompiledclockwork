using System;

namespace TechnoPro.Common.DAO.Impl.Inventory.QueryStorage
{
	// Token: 0x020000BC RID: 188
	internal static class QueryStorageInventoryAttachments
	{
		// Token: 0x04000262 RID: 610
		internal const string SQ_ATTACHMENT_BY_ID = "select * from InventoryV2_AttachedFile where AttachmentID=@attachmentid";

		// Token: 0x04000263 RID: 611
		internal const string SQ_ATTACHMENT_DATA_BY_ID = "select AttachmentID, BinaryData from [InventoryV2_AttachedFileData] where AttachmentID=@attachmentid";

		// Token: 0x04000264 RID: 612
		internal const string SQ_ATTACHMENTS_BY_ITEM = "select * from InventoryV2_AttachedFile where ItemID=@itemuniqueid";

		// Token: 0x04000265 RID: 613
		internal const string SQ_GET_PRODUCT_PICTURE = "select ProductId, [Picture] from InventoryV2_ProductImage where ProductId=@productid";

		// Token: 0x04000266 RID: 614
		internal const string IQ_ADD_ATTACHMENT_DATA_TO_PRODUCT = "insert into [InventoryV2_AttachedFileData] (AttachmentID, BinaryData) values (@attachmentid, @binarydata)";

		// Token: 0x04000267 RID: 615
		internal const string IQ_ADD_ATTACHMENT_TO_ITEM = "insert into InventoryV2_AttachedFile \r\n(ItemID ,AttachmentName, CreatedDate, Notes, SizeInBytes)\r\nvalues (@itemuniqueid, @attachmentname, @createddate, @notes, @sizeinbytes)\r\nset @attachmentid=SCOPE_IDENTITY()";

		// Token: 0x04000268 RID: 616
		internal const string DQ_ATTACHMENT_BY_ID = "delete from InventoryV2_AttachedFile where AttachmentID=@attachmentid";

		// Token: 0x04000269 RID: 617
		internal const string DQ_ATTACHMENT_DATA_BY_ID = "delete from [InventoryV2_AttachedFileData] where AttachmentID=@attachmentid";

		// Token: 0x0400026A RID: 618
		internal const string DQ_ATTACHMENTS_DATA_BY_ID = "delete from InventoryV2_AttachedFileData where AttachmentID in (select OrderID as AttachmentID from SplitOrderIDs(@attachmentids))";

		// Token: 0x0400026B RID: 619
		internal const string DQ_ATTACHMENTS_BY_ID = "delete from InventoryV2_AttachedFile where AttachmentID in (select OrderID as AttachmentID from SplitOrderIDs(@attachmentids))";

		// Token: 0x0400026C RID: 620
		internal const string DQ_ATTACHMENTS_BY_ITEM = "delete from InventoryV2_AttachedFile where ItemID=@itemuniqueid";

		// Token: 0x0400026D RID: 621
		internal const string DQ_ATTACHMENTS_DATA_BY_ITEM = "delete from InventoryV2_AttachedFileData where ItemID=@itemuniqueid";

		// Token: 0x0400026E RID: 622
		internal const string DQ_DELETE_PRODUCT_PICTURE = "delete from InventoryV2_ProductImage where ProductId=@productid";

		// Token: 0x0400026F RID: 623
		internal const string UQ_INSERT_OR_UPDATE_PRODUCT_PICTURE = "IF EXISTS (SELECT 1 FROM [InventoryV2_ProductImage] where ProductId=@productid)\r\n                begin\r\n                    update [InventoryV2_ProductImage] set [Picture]=@picture where ProductId=@productid\r\n                end\r\n              ELSE\r\n                begin\r\n                    insert into [InventoryV2_ProductImage] (ProductId, [Picture]) VALUES (@productid, @picture)\r\n                end";
	}
}
