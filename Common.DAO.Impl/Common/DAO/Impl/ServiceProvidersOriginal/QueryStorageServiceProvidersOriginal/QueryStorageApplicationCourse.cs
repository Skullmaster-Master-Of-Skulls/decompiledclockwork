using System;

namespace TechnoPro.Common.DAO.Impl.ServiceProvidersOriginal.QueryStorageServiceProvidersOriginal
{
	// Token: 0x02000064 RID: 100
	public class QueryStorageApplicationCourse
	{
		// Token: 0x04000108 RID: 264
		internal const string QS_COURSES_BY_PROVIDER = "SELECT    spac.serviceproviderapplicationcourseid,spac.serviceprovidertype,spac.lucourseid,spac.registrationstatus,\r\n            luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,lucd.altlookupstring AS subject,\r\n            luc.course,luc.[section],luc.timeofday,luc.campus,luc.coursenote,luc.location,luc.campus,luc.department\r\nFROM        serviceproviderapplications spa LEFT JOIN serviceproviderapplicationcourses spac ON spac.serviceproviderapplicationid=spa.serviceproviderapplicationid\r\n            LEFT JOIN lucourses luc ON luc.lucourseid=spac.lucourseid\r\n            LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\nWHERE       spa.serviceproviderid=@spid AND spac.serviceprovidertype=@sptype\r\n            AND (spac.registrationstatus IS NULL OR NOT spac.registrationstatus=2)\r\n            AND NOT ( ( luc.enddate<@startdate ) OR (luc.startdate > @enddate ) )";
	}
}
