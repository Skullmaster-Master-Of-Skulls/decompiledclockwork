using System;

namespace TechnoPro.Common.DAO.Impl.Tutoring.QueryStorage
{
	// Token: 0x02000034 RID: 52
	internal static class QueryStorageStudentTutee
	{
		// Token: 0x0400007B RID: 123
		internal const string QS_IS_STUDENT_AUTHORIZED_TO_USE_TUTORING = "IF EXISTS(SELECT controlid FROM dynamicscreencontrols WHERE screennum=4 AND controlid=@cid)\r\nBEGIN --must be acc template\r\n    IF EXISTS(SELECT dataid FROM MainInfoAccommodationPS WHERE personid=@pid AND courseid=0 AND controlid=@cid AND NOT controlvalue=0)\r\n        SET @isallowed = 1\r\n    ELSE\r\n    SET @isallowed = 0\r\nEND\r\nELSE --must be per student\r\nBEGIN\r\n    IF EXISTS(SELECT dataid FROM MainInfoPS WHERE personid=@pid AND controlid=@cid AND NOT controlvalue=0)\r\n        SET @isallowed=1\r\n    ELSE\r\n        SET @isallowed=0\r\nEND";
	}
}
