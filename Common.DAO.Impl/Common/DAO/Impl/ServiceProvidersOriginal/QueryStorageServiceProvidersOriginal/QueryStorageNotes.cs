using System;

namespace TechnoPro.Common.DAO.Impl.ServiceProvidersOriginal.QueryStorageServiceProvidersOriginal
{
	// Token: 0x02000066 RID: 102
	public static class QueryStorageNotes
	{
		// Token: 0x0400010A RID: 266
		internal const string QS_PROVIDERS_WITH_COURSES_NO_NOTES_NO_EMAIL_NOTICES_BY_DAYCOUNT = "DECLARE @now datetime\r\nSET @now = DATEADD(D, 0, DATEDIFF(D, 0, GETDATE()))\r\n\r\nDECLARE @mindate datetime\r\nSET @mindate=dateadd(day,-@daycount,@now)\r\n\r\nSELECT orderid AS serviceprovidertype INTO #t1 FROM SplitOrderIDs(@sptypes,',')\r\n\r\nSELECT\tDISTINCT spr.ServiceProviderId,spr.serviceproviderlucourseid,\r\n\t\tsp.lastname,sp.firstname,sp.middlename,sp.email,\r\n\t\tlucd.altlookupstring AS subject,luc.course,luc.section,luc.timeofday,\r\n\t\tluc.StartDate,luc.EndDate,\r\n\t\tMAX(nd.datecreated) AS lastuploaddate\r\nFROM\tServiceProviderRequests spr LEFT JOIN lucourses luc ON luc.LUCourseID=spr.serviceproviderlucourseid\r\n\t\tLEFT JOIN lucoursedata lucd ON lucd.luCourseDataID=luc.SubjectID\r\n\t\tLEFT JOIN NotetakerDocument nd ON nd.NotetakerID=ServiceProviderId AND nd.LUCourseId=spr.serviceproviderlucourseid \r\n\t\tLEFT JOIN ServiceProviders sp ON sp.ServiceProviderId=spr.ServiceProviderId\r\nWHERE\tNOT spr.serviceproviderlucourseid IS NULL AND NOT spr.ServiceProviderId IS NULL\r\n\t\tAND spr.serviceprovidertype IN (SELECT serviceprovidertype FROM #t1)\r\n\t\tAND @now >= DATEADD(D, 0, DATEDIFF(D, 0, luc.startdate)) AND @now < DATEADD(D, 1 - @daycount, DATEDIFF(D, 0, luc.enddate))\r\n\t\tAND (nd.dateCreated IS NULL OR nd.dateCreated<@mindate)\r\n\t\tAND NOT EXISTS(SELECT eh.datesent FROM emailhistory eh WHERE eh.[successful]=1 AND eh.datesent>=@mindate AND eh.emailtypecode='MISSNOTES' AND eh.personid=spr.serviceproviderid AND eh.lucourseid=spr.serviceproviderlucourseid)\r\nGROUP BY spr.ServiceProviderId,spr.serviceproviderlucourseid,\r\n\t\tsp.lastname,sp.firstname,sp.middlename,sp.email,\r\n\t\tlucd.altlookupstring,luc.course,luc.section,luc.timeofday,\r\n\t\tluc.startdate,luc.enddate\r\nORDER BY spr.ServiceProviderId,spr.serviceproviderlucourseid\r\n\r\nDROP TABLE #t1";
	}
}
