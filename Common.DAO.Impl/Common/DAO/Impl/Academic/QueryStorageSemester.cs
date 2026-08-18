using System;

namespace TechnoPro.Common.DAO.Impl.Academic
{
	// Token: 0x02000188 RID: 392
	public static class QueryStorageSemester
	{
		// Token: 0x04000722 RID: 1826
		internal const string QI_SEMESTER = "INSERT INTO semester (semestertitle,startdate,enddate) VALUES (@title,@sd,@ed) \r\nSELECT TOP 1 CAST(SCOPE_IDENTITY() AS int) AS semesterid";

		// Token: 0x04000723 RID: 1827
		internal const string QU_SEMESTER = "UPDATE semester SET semestertitle=@title,startdate=@sd,enddate=@ed WHERE semesterid=@semesterid";

		// Token: 0x04000724 RID: 1828
		internal const string QS_SEMESTER_BY_ID = "SELECT semesterid,semestertitle,startdate,enddate FROM semester WHERE semesterid=@semesterid";

		// Token: 0x04000725 RID: 1829
		internal const string QS_CURRENT_SEMESTER = "SELECT semesterid,semestertitle,startdate,enddate FROM semester WHERE @now >= startdate AND @now <= enddate";

		// Token: 0x04000726 RID: 1830
		internal const string QS_CURRENT_AND_NEXT_1_SEMESTER = "SELECT TOP 2 semesterid,semestertitle,startdate,enddate FROM semester WHERE startdate>=@now ORDER BY startdate";

		// Token: 0x04000727 RID: 1831
		internal const string QD_SEMESTER = "DELETE FROM semester WHERE semesterid=@semesterid";
	}
}
