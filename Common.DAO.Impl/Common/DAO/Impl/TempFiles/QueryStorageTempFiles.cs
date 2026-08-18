using System;

namespace TechnoPro.Common.DAO.Impl.TempFiles
{
	// Token: 0x02000038 RID: 56
	public static class QueryStorageTempFiles
	{
		// Token: 0x0400008E RID: 142
		internal const string QS_TEMP_FILE = "SELECT filename,filebytes FROM tempfiles WHERE usagecode=@usagecode AND groupname=@groupname AND tempfileid=@tempfileid";

		// Token: 0x0400008F RID: 143
		internal const string QI_TEMP_FILE = "INSERT INTO TempFiles (UsageCode,GroupName,Filename,FileBytes) VALUES (@usagecode,@groupname,@filename,@filebytes); SET @tempfileid = CAST(SCOPE_IDENTITY() as int)";

		// Token: 0x04000090 RID: 144
		internal const string QI_TEMP_FILE_TO_EXAM_FILE = "DECLARE @t1 table( examfileid int );\r\n\r\nINSERT INTO ExamFiles (examid,[filename],filedata,dateentered,whoentered,[description],visible)\r\n\tOUTPUT\tINSERTED.examfileid\r\n\tINTO @t1\r\n\t\tSELECT\t@examid,[filename],filebytes,getdate() AS dateentered,@whoenteredpid,@description,1 FROM {0}TempFiles WHERE usagecode=@usagecode AND groupname=@groupname\r\n\r\nDECLARE @results varchar(max) = ''\r\nSELECT @results = COALESCE(@results + ',', '' ) + convert(varchar(12),cast(examfileid AS varchar(8000))) FROM @t1\r\n\r\nSET @examfileids=@results";

		// Token: 0x04000091 RID: 145
		internal const string QD_DELETE_OLD_TEMP_FILES = "DELETE FROM TempFiles WHERE dateentered<@mindate";

		// Token: 0x04000092 RID: 146
		internal const string QD_TEMP_FILES = "DELETE FROM TempFiles WHERE usagecode=@usagecode AND groupname=@groupname";

		// Token: 0x04000093 RID: 147
		internal const string QD_TEMP_FILE = "DELETE FROM TempFiles WHERE usagecode=@usagecode AND groupname=@groupname AND tempfileid=@tempfileid";
	}
}
