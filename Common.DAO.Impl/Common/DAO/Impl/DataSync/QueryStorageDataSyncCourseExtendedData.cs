using System;

namespace TechnoPro.Common.DAO.Impl.DataSync
{
	// Token: 0x020000FC RID: 252
	public static class QueryStorageDataSyncCourseExtendedData
	{
		// Token: 0x04000429 RID: 1065
		internal const string QS_COURSE_EXTENDED_FIELDS_ACTIVE = "SELECT controlid,controlcaption,controlcode,isactive,ordernum FROM LuCourseDataSyncExtendedFields WHERE isactive=1 ORDER BY ordernum";

		// Token: 0x0400042A RID: 1066
		internal const string QU_COURSE_EXTENDED_DATA_SYNC_FIELD = "UPDATE LuCourseDataSyncExtendedFields SET ControlCaption=@caption,ControlCode=@code,IsActive=@isactive,OrderNum=@ordernum\r\nWHERE controlid=@cid";

		// Token: 0x0400042B RID: 1067
		internal const string QI_COURSE_EXTENDED_DATA_SYNC_FIELD = "INSERT INTO LuCourseDataSyncExtendedFields (ControlCaption,ControlCode,IsActive,OrderNum) VALUES (@caption,@code,@isactive,@ordernum) \r\nSET @cid=(SELECT TOP 1 CAST(IDENTITY_SCOPE() AS int))";

		// Token: 0x0400042C RID: 1068
		internal const string QI_COURSE_EXTENDED_DATA_SYNC_DATA = "INSERT INTO LuCourseDataSyncExtendedData (lucourseid,controlid,datavalue) VALUES (@lucid,@cid,@val)";

		// Token: 0x0400042D RID: 1069
		internal const string QD_COURSE_EXTENDED_FIELD = "UPDATE LuCourseDataSyncExtendedFields SET isactive=0 WHERE controlid=@cid";

		// Token: 0x0400042E RID: 1070
		internal const string QD_COURSE_EXTENDED_DATA_BY_LUCOURSEID = "DELETE FROM LuCourseDataSyncExtendedData WHERE LuCourseId=@lucid";
	}
}
