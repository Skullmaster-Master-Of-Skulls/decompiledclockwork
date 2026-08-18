using System;

namespace TechnoPro.Common.DAO.Impl.QueryStorage
{
	// Token: 0x0200011C RID: 284
	internal static class QueryStorage
	{
		// Token: 0x040004C0 RID: 1216
		internal const string SQ_PERSONS_BY_GROUP = "select p.PersonID, p.firstName, p.lastName, p.student_no from People as p\r\n            inner join PeopleGroups as pg on p.PersonID = pg.PersonID\r\n            where pg.GroupID = @groupid";

		// Token: 0x040004C1 RID: 1217
		internal const string SQ_PERSON_BY_ID = "select * from People where PersonID = @personid";

		// Token: 0x040004C2 RID: 1218
		internal const string SQ_SMTP_SETTINGS = "SELECT settingcode,settingstringvalue,settingvalue FROM settingsgroups WHERE settingcode IN (SELECT orderid AS settingcode FROM splitorderids(@codes,',')) AND groupid=-1";

		// Token: 0x040004C3 RID: 1219
		internal const string SQ_FILESTORAGE_LOADFILE = "SELECT TOP 1 cf.BinaryData, cf.Version, cf.UploadDateTime, cf.WhoUploaded, cf.IsActive, cf.FileTypeId, cf.Notes, ct.title, cf.AddrSize\r\nFROM Common_FileStore cf left join Common_FileType ct ON cf.FileTypeId = ct.FileTypeId\r\nWHERE   ct.title=@title AND cf.isactive=1 AND cf.AddrSize=@addrsize\r\nORDER BY cf.Version DESC, cf.UploadDateTime DESC";

		// Token: 0x040004C4 RID: 1220
		internal const string SQ_FILESTORAGE_GET_VERSION_BY_FILETYPE = "SELECT TOP 1 cf.Version\r\n            FROM Common_FileStore cf inner join Common_FileType ct ON cf.FileTypeId = ct.FileTypeId\r\n            WHERE ct.title=@title AND cf.isactive=1 AND AddrSize=@addrsize\r\n            ORDER BY cf.Version DESC, cf.UploadDateTime DESC";

		// Token: 0x040004C5 RID: 1221
		internal const string SQ_GET_LICENSE_KEY_INFO = "select * from [LicenseSystem_LicenseInfo] where LicenseKey=@serial";

		// Token: 0x040004C6 RID: 1222
		internal const string SQ_KEY_BY_PRODUCTNAME = "Select * from [LicenseSystem_LicenseInfo] Where ProductName=@productname";

		// Token: 0x040004C7 RID: 1223
		internal const string SQ_KEY_BY_TYPE = "select * from [LicenseSystem_LicenseInfo] where LicenseType=@licensetype";

		// Token: 0x040004C8 RID: 1224
		internal const string SQ_ALL_KEYS = "select * from [LicenseSystem_LicenseInfo]";

		// Token: 0x040004C9 RID: 1225
		internal const string SQ_ALL_PRODUCTS_INFO = "select * from [LicenseSystem_ProductInfo]";

		// Token: 0x040004CA RID: 1226
		internal const string SQ_ALL_PRODUCT_NAMES = "select ProductName from [LicensingSystem_ProductInfo]";

		// Token: 0x040004CB RID: 1227
		internal const string IQ_FILE_STORAGE = "IF EXISTS(SELECT filestoreid FROM Common_FileStore WHERE FileTypeId IN (SELECT FileTypeId FROM Common_FileType WHERE title=@filetype) AND Version=@version AND AddrSize=@addrsize)\r\n                    UPDATE Common_FileStore SET binarydata=@binarydata, UploadDatetime=@uploaddatetime WHERE FileTypeId IN (SELECT FileTypeId FROM Common_FileType WHERE title=@filetype) AND Version=@version AND AddrSize=@addrsize\r\n                ELSE\r\n                    INSERT INTO Common_FileStore (BinaryData,Version,WhoUploaded,FileTypeId, AddrSize) \r\n                    SELECT TOP 1 @binarydata,@version,@whouploaded,FileTypeId, @addrsize FROM Common_FileType WHERE title=@filetype";

		// Token: 0x040004CC RID: 1228
		internal const string DQ_RECENTFILES_BY_VERSION = "delete from Common_FileStore \r\nwhere FileTypeId IN (SELECT FileTypeId FROM Common_FileType WHERE title=@filetype) AND [Version] > @version AND (AddrSize=@addrsize or AddrSize=0 or AddrSize is NULL)";

		// Token: 0x040004CD RID: 1229
		internal const string IQ_LICENSE_KEY = "if not exists(select 1 from [LicenseSystem_LicenseInfo] where ProductName=@productname)\r\n\tbegin\r\n\t\tInsert into [LicenseSystem_LicenseInfo] (ProductName, LicenseKey, IssuedDate, ExpiryDate, LicenseType, NLicenses, LicensedTo)\r\n\t\tvalues (@productname, @licensekey, @issueddate, @expirydate, @licensetype, @nlicenses, @licensedto)\r\n\tend\r\nelse\r\n\tbegin\r\n\t\tupdate [LicenseSystem_LicenseInfo] \r\n\t\tset LicenseKey=@licensekey, \r\n            IssuedDate=@issueddate, \r\n            ExpiryDate=@expirydate, \r\n            NLicenses=@nlicenses, \r\n            LicensedTo=@licensedto,\r\n            LicenseType=@licensetype\r\n\t\twhere ProductName=@productname\r\n\tend";

		// Token: 0x040004CE RID: 1230
		internal const string IQ_PRODUCT_INFO = "if not exists (select 1 from LicenseSystem_ProductInfo where ProductName = @productname)\r\n                begin\r\n\t                insert into LicenseSystem_ProductInfo (ProductName, ProductParameters) values(@productname, @productparameters)\t\r\n                end";
	}
}
