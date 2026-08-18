using System;

namespace TechnoPro.Common.DAO.Impl.Veteran
{
	// Token: 0x02000024 RID: 36
	public static class QueryStorageVeteran
	{
		// Token: 0x0400004A RID: 74
		internal const string QS_ChangeInBenefitRequestsByStudentAndDateRange = "DECLARE @sd datetime = DATEADD(D, 0, DATEDIFF(D, 0, @startdate))\r\nDECLARE @ed datetime = DATEADD(D, 1, DATEDIFF(D, 0, @enddate))\r\n\r\nSELECT\tpm.personid,pm.appointmentid,pm.dateentered,pm.[description],\r\n\t\tm.controlid,m.controlvalue,ll.LookupText AS [status]\r\nFROM\tinfopm pm LEFT JOIN maininfopm m ON m.personid=pm.personid AND m.appointmentid=pm.appointmentid AND m.controlid=@cid\r\n\t\tLEFT JOIN LookupLists ll ON ll.lookuplistid=m.controlvalue\r\nWHERE\tpm.personid=@pid\r\n\t\tAND pm.dateentered >= @sd AND pm.dateentered < @ed \r\n\t\tAND pm.screennum=@screennum\r\n\t\tAND (pm.[description] IS NULL OR NOT CAST(pm.[description] AS varchar(max)) = '.DELETED.')\r\nORDER BY pm.dateentered DESC,pm.personid,pm.appointmentid";
	}
}
