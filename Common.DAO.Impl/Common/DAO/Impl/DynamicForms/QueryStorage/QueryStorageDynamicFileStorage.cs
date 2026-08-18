using System;

namespace TechnoPro.Common.DAO.Impl.DynamicForms.QueryStorage
{
	// Token: 0x020000E8 RID: 232
	public static class QueryStorageDynamicFileStorage
	{
		// Token: 0x040003F8 RID: 1016
		internal const string QS_FILE_BY_ID = "SELECT    f.fileid,CASE WHEN @loadfilecontents=1 THEN f.filebytes ELSE CAST(NULL AS image) END AS filebytes,f.filename,f.filetypecode,f.isencrypted,f.iscompressed,f.dateuploaded,f.whouploaded\r\nFROM        files f\r\nWHERE       f.fileid=@fileid";

		// Token: 0x040003F9 RID: 1017
		internal const string QS_SINGLE_FILE_DESCRIPTIONS_BY_STUDENT_AND_CIDS = "SELECT dataid,personid,controlid,CAST(metadata AS varchar(max)) AS metadata,CASE WHEN metadata IS NULL THEN controlvalue ELSE CAST(NULL AS varbinary(max)) END AS controlvalue \r\nFROM imageinfops WHERE personid=@pid AND controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,','))";

		// Token: 0x040003FA RID: 1018
		internal const string QS_FILELIST_FILE_DESCRIPTIONS_BY_STUDENT_AND_CIDS = "SELECT dataid,personid,controlid,controlvalue,CAST(NULL AS varchar(max)) AS metadata \r\nFROM otherinfops WHERE personid=@pid AND controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,','))";
	}
}
