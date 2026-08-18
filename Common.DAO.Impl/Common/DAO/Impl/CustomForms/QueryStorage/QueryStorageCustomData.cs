using System;

namespace TechnoPro.Common.DAO.Impl.CustomForms.QueryStorage
{
	// Token: 0x02000102 RID: 258
	public static class QueryStorageCustomData
	{
		// Token: 0x04000434 RID: 1076
		private const string _baseLoadData = "SELECT orderid AS datainstanceid INTO #t1 FROM splitstrings2(@datainstanceids,',')\r\n\r\nSELECT\td.dataid,d.personid,d.datainstanceid,d.datatypecode,d.datavalue,d.datavaluejoinid,\r\n\t\tcli.ItemCaption AS joinedlistitemcaption,\r\n        '' AS joinedfilename --f.filename \r\nFROM\t{0} d LEFT JOIN CustomListItem cli ON d.datatypecode=6 AND cli.CustomListItemid=d.datavaluejoinid --list items\r\n\t\t--LEFT JOIN Files f ON d.datatypecode=3 AND f.fileid=d.datavaluejoinid -- files\r\nWHERE\t{1} AND d.datainstanceid IN (SELECT datainstanceid FROM #t1) \r\nORDER BY d.datainstanceid\r\n\r\nDROP TABLE #t1";

		// Token: 0x04000435 RID: 1077
		internal static string QS_LOAD_DATA_PER_STUDENT = string.Format("SELECT orderid AS datainstanceid INTO #t1 FROM splitstrings2(@datainstanceids,',')\r\n\r\nSELECT\td.dataid,d.personid,d.datainstanceid,d.datatypecode,d.datavalue,d.datavaluejoinid,\r\n\t\tcli.ItemCaption AS joinedlistitemcaption,\r\n        '' AS joinedfilename --f.filename \r\nFROM\t{0} d LEFT JOIN CustomListItem cli ON d.datatypecode=6 AND cli.CustomListItemid=d.datavaluejoinid --list items\r\n\t\t--LEFT JOIN Files f ON d.datatypecode=3 AND f.fileid=d.datavaluejoinid -- files\r\nWHERE\t{1} AND d.datainstanceid IN (SELECT datainstanceid FROM #t1) \r\nORDER BY d.datainstanceid\r\n\r\nDROP TABLE #t1", "CustomDataPerStudent", "d.personid=@pid");

		// Token: 0x04000436 RID: 1078
		internal static string QS_LOAD_DATA_PER_SEMESTER = string.Format("SELECT orderid AS datainstanceid INTO #t1 FROM splitstrings2(@datainstanceids,',')\r\n\r\nSELECT\td.dataid,d.personid,d.datainstanceid,d.datatypecode,d.datavalue,d.datavaluejoinid,\r\n\t\tcli.ItemCaption AS joinedlistitemcaption,\r\n        '' AS joinedfilename --f.filename \r\nFROM\t{0} d LEFT JOIN CustomListItem cli ON d.datatypecode=6 AND cli.CustomListItemid=d.datavaluejoinid --list items\r\n\t\t--LEFT JOIN Files f ON d.datatypecode=3 AND f.fileid=d.datavaluejoinid -- files\r\nWHERE\t{1} AND d.datainstanceid IN (SELECT datainstanceid FROM #t1) \r\nORDER BY d.datainstanceid\r\n\r\nDROP TABLE #t1", "CustomDataPerSemester", "personid=@pid AND SemesterId=@semesterid");

		// Token: 0x04000437 RID: 1079
		internal static string QS_LOAD_DATA_PER_DATE = string.Format("SELECT orderid AS datainstanceid INTO #t1 FROM splitstrings2(@datainstanceids,',')\r\n\r\nSELECT\td.dataid,d.personid,d.datainstanceid,d.datatypecode,d.datavalue,d.datavaluejoinid,\r\n\t\tcli.ItemCaption AS joinedlistitemcaption,\r\n        '' AS joinedfilename --f.filename \r\nFROM\t{0} d LEFT JOIN CustomListItem cli ON d.datatypecode=6 AND cli.CustomListItemid=d.datavaluejoinid --list items\r\n\t\t--LEFT JOIN Files f ON d.datatypecode=3 AND f.fileid=d.datavaluejoinid -- files\r\nWHERE\t{1} AND d.datainstanceid IN (SELECT datainstanceid FROM #t1) \r\nORDER BY d.datainstanceid\r\n\r\nDROP TABLE #t1", "CustomDataPerDate", "personid=@pid AND perdateid=@perdateid");

		// Token: 0x04000438 RID: 1080
		internal const string QD_PER_STUDENT_DATA_ITEM = "DELETE FROM CustomDataPerStudent WHERE personid=@pid AND datainstanceid=@datainstanceid";

		// Token: 0x04000439 RID: 1081
		internal const string QD_PER_SEMESTER_DATA_ITEM = "DELETE FROM CustomDataPerSemester WHERE personid=@pid AND semesterid=@semesterid AND datainstanceid=@datainstanceid";

		// Token: 0x0400043A RID: 1082
		internal const string QD_PER_DATE_DATA_ITEM = "DELETE FROM CustomDataPerDate WHERE personid=@pid AND perdateid=@perdateid AND datainstanceid=@datainstanceid";

		// Token: 0x0400043B RID: 1083
		internal const string QI_PER_STUDENT_DATA_ITEM = "IF EXISTS(SELECT dataid FROM CustomDataPerStudent WHERE personid=@pid AND datainstanceid=@datainstanceid)\r\n    UPDATE CustomDataPerStudent SET datatypecode=@datatypecode,datavalue=@val WHERE personid=@pid AND datainstanceid=@datainstanceid\r\nELSE\r\n    INSERT INTO CustomDataPerStudent (personid,datainstanceid,datatypecode,datavalue) VALUES (@pid,@datainstanceid,@datatypecode,@val)";

		// Token: 0x0400043C RID: 1084
		internal const string QI_PER_SEMESTER_DATA_ITEM = "IF EXISTS(SELECT dataid FROM CustomDataPerSemester WHERE personid=@pid AND semesterid=@semesterid AND datainstanceid=@datainstanceid)\r\n    UPDATE CustomDataPerSemester SET datatypecode=@datatypecode,datavalue=@val WHERE personid=@pid AND semesterid=@semesterid AND datainstanceid=@datainstanceid\r\nELSE\r\n    INSERT INTO CustomDataPerSemester (personid,semesterid,datainstanceid,datatypecode,datavalue) VALUES (@pid,@semesterid,@datainstanceid,@datatypecode,@val)";

		// Token: 0x0400043D RID: 1085
		internal const string QI_PER_DATE_DATA_ITEM = "IF EXISTS(SELECT dataid FROM CustomDataPerDate WHERE personid=@pid AND perdateid=@perdateid AND datainstanceid=@datainstanceid)\r\n    UPDATE CustomDataPerDate SET datatypecode=@datatypecode,datavalue=@val WHERE personid=@pid AND perdateid=@perdateid AND datainstanceid=@datainstanceid\r\nELSE\r\n    INSERT INTO CustomDataPerDate (personid,perdateid,datainstanceid,datatypecode,datavalue) VALUES (@pid,@perdateid,@datainstanceid,@datatypecode,@val)";
	}
}
