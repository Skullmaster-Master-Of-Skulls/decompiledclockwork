using System;

namespace TechnoPro.Common.DAO.Impl.AlternativeFormat.QueryStorage
{
	// Token: 0x02000176 RID: 374
	internal static class QueryStorageVendor
	{
		// Token: 0x04000712 RID: 1810
		internal const string IQ_NEW_VENDOR = "SET @vendorid = 0\r\n\r\nIF NOT EXISTS(SELECT 1 FROM [AlternativeFormat_Vendor] WHERE VendorName=@vendorname)\r\nBEGIN\r\n\tINSERT INTO [AlternativeFormat_Vendor]\r\n            ([VendorName]\r\n            ,[VendorDescription]\r\n            ,[VendorNotes]\r\n            ,[VendorPhone]\r\n            ,[VendorCellPhone]\r\n            ,[VendorAddress]\r\n            ,[VendorFax]\r\n            ,[VendorEmail]\r\n            ,[VendorWebSite])\r\n        VALUES\r\n            (@vendorname\r\n            ,@vendordescription\r\n            ,@vendornotes\r\n            ,@vendorphone\r\n            ,@vendorcellphone\r\n            ,@vendoraddress\r\n            ,@vendorfax\r\n            ,@vendoremail\r\n            ,@vendorwebsite)\r\n\r\n    set @vendorid = SCOPE_IDENTITY()\r\nEND";

		// Token: 0x04000713 RID: 1811
		internal const string UQ_UPDATE_VENDOR = "IF NOT EXISTS(SELECT 1 FROM [AlternativeFormat_Vendor] WHERE VendorName=@vendorname and VendorId <> @vendorid)\r\nBEGIN\r\n\tUPDATE [AlternativeFormat_Vendor]\r\n    SET [VendorName] = @vendorname\r\n        ,[VendorDescription] = @vendordescription\r\n        ,[VendorNotes] = @vendornotes\r\n        ,[VendorPhone] = @vendorphone\r\n        ,[VendorCellPhone] = @vendorcellphone\r\n        ,[VendorAddress] = @vendoraddress\r\n        ,[VendorFax] = @vendorfax\r\n        ,[VendorEmail] = @vendoremail\r\n        ,[VendorWebSite] = @vendorwebsite\r\n    WHERE VendorId = @vendorid\r\nEND";

		// Token: 0x04000714 RID: 1812
		internal const string DQ_DELETE_VENDOR_BY_ID = "delete from AlternativeFormat_Vendor where vendorid=@vendorid";

		// Token: 0x04000715 RID: 1813
		internal const string SQ_GET_VENDOR_BY_ID = "select * from AlternativeFormat_Vendor where vendorid=@vendorid";

		// Token: 0x04000716 RID: 1814
		internal const string SQ_GET_VENDOR_BY_NAME = "select * from AlternativeFormat_Vendor where vendorname=@vendorname";

		// Token: 0x04000717 RID: 1815
		internal const string SQ_GET_ALL_VENDORS = "select * from AlternativeFormat_Vendor";
	}
}
