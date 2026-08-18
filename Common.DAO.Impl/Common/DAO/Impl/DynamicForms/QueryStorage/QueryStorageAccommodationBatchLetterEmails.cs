using System;

namespace TechnoPro.Common.DAO.Impl.DynamicForms.QueryStorage
{
	// Token: 0x020000E5 RID: 229
	public static class QueryStorageAccommodationBatchLetterEmails
	{
		// Token: 0x040003E9 RID: 1001
		internal const string QS_LETTER_SENT_DATES_BY_COURSES = "SELECT DISTINCT personid,lucourseid,max(datesent) AS datesent \r\nFROM AccommodationLettersBatchSent\r\nWHERE personid=@pid AND lucourseid IN (SELECT orderid AS lucourseid FROM splitorderids(@lucids,','))\r\nGROUP BY personid,lucourseid";

		// Token: 0x040003EA RID: 1002
		internal const string QS_STUDENTS_AND_COURSES_POTENTIALLY_REQUIRING_LETTER_SENT = "DECLARE @defaultexpirydate datetime \r\nSET @defaultexpirydate=dateadd(year,1,@now)\r\n\r\nSELECT DISTINCT x.personid,x.lucourseid,MAX(x.maxdatemodified) AS maxdatemodified \r\nINTO #t2\r\nFROM\r\n(\r\n\tSELECT DISTINCT personid,lucourseid,MAX(dateentered) AS maxdatemodified FROM archive_maininfoaccommodationps GROUP BY personid,lucourseid\r\n\tUNION\r\n\tSELECT DISTINCT personid,lucourseid,MAX(dateentered) AS maxdatemodified FROM archive_otherinfoaccommodationps GROUP BY personid,lucourseid\r\n\tUNION\r\n\tSELECT DISTINCT personid,lucourseid,MAX(dateentered) AS maxdatemodified FROM archive_datetimeinfoaccommodationps GROUP BY personid,lucourseid\r\n) x GROUP BY x.personid,x.lucourseid\r\n\r\nSELECT DISTINCT c.personid,c.lucourseid,COALESCE(d.controlvalue,@defaultexpirydate) AS expiry,t2.maxdatemodified,MAX(ab.datesent) AS datelastsent\r\nFROM\tcourses c LEFT JOIN people p ON p.personid=c.personid\r\n\t\tLEFT JOIN DateTimeInfoAccommodationPS d ON d.personid=c.personid AND NOT @expirydatecid IS NULL AND d.controlid=@expirydatecid\r\n\t\tLEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid\r\n\t\tLEFT JOIN #t2 t2 ON t2.personid=c.personid AND t2.lucourseid=dbo.AccommodationsCourseOrTemplate(c.personid,c.lucourseid)\r\n\t\tLEFT JOIN AccommodationLettersBatchSent ab ON ab.personid=c.personid AND ab.lucourseid=c.lucourseid\r\nWHERE\tNOT p.personid IS NULL AND p.isactive=1\r\n\t\tAND (c.registrationstatus is null OR NOT c.registrationstatus=2)\r\n\t\tAND NOT luc.lucourseid IS NULL \r\n\t\tAND luc.enddate>=@now\r\n\t\tAND COALESCE(d.controlvalue,@defaultexpirydate) >= @now\r\nGROUP BY c.personid,c.lucourseid,COALESCE(d.controlvalue,@defaultexpirydate),t2.maxdatemodified\r\nORDER BY c.personid,c.lucourseid,datelastsent DESC\r\n\r\nDROP TABLE #t2";

		// Token: 0x040003EB RID: 1003
		internal const string QI_MARK_LETTER_SENT = "INSERT INTO AccommodationLettersBatchSent (personid,lucourseid,datesent) VALUES (@pid,@lucid,@datesent)";
	}
}
