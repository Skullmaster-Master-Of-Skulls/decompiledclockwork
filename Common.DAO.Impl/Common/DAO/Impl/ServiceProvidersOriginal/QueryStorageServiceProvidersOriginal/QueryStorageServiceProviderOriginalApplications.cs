using System;

namespace TechnoPro.Common.DAO.Impl.ServiceProvidersOriginal.QueryStorageServiceProvidersOriginal
{
	// Token: 0x02000069 RID: 105
	public class QueryStorageServiceProviderOriginalApplications
	{
		// Token: 0x04000114 RID: 276
		internal const string QS_PROVIDERS_BY_TYPE_AND_DATE = "SELECT serviceproviderid INTO #t1 FROM ActiveServiceProvidersByCourse(@startdate,@enddate,@sptype);\r\n\r\nSELECT  spa.serviceproviderapplicationid,spa.serviceprovidertype\r\n            ,Status=CASE \r\n                WHEN (sp.serviceproviderid IN (SELECT serviceproviderid FROM AssignedServiceProvidersByCourse(@startdate,@enddate,@sptype))) THEN 'Assigned'\r\n                WHEN (sp.serviceproviderid IN (SELECT serviceproviderid FROM #t1)) THEN 'Active'\r\n                ELSE ''\r\n            END\r\n            ,sp.lastname,sp.firstname,sp.student_no\r\n            ,sp.specialization,sp.email,sp.phone1,sp.phone2,sp.phonenote\r\n            ,spa.serviceproviderid,sp.altid,sp.dateentered\r\nFROM        serviceproviderapplications spa LEFT JOIN serviceproviders sp ON sp.serviceproviderid=spa.serviceproviderid \r\nWHERE       spa.serviceprovidertype=@sptype \r\n            AND sp.isactive=1\r\n            AND sp.serviceproviderid=@spid;\r\n\r\nDROP TABLE #t1";
	}
}
