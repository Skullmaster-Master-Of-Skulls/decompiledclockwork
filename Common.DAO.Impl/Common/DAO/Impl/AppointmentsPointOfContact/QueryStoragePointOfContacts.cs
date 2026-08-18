using System;

namespace TechnoPro.Common.DAO.Impl.AppointmentsPointOfContact
{
	// Token: 0x02000123 RID: 291
	public class QueryStoragePointOfContacts
	{
		// Token: 0x040004E2 RID: 1250
		internal const string QS_POC_DATA_SCREENNUM_BY_APPID = "DECLARE @screennums varchar(8000)\r\nSET @screennums=(SELECT TOP 1 perAppScreenNumsForTabs FROM appointmenttypes WHERE apptypeid IN (SELECT apptypeid FROM appointments WHERE appointmentid=@appid))\r\nIF NOT @screennums IS NULL\r\nBEGIN\r\n    SET @screennum=(SELECT TOP 1 dsc.screennum FROM perappdata2 pad LEFT JOIN dynamicscreencontrols dsc ON dsc.controlid=pad.controlid \r\n                    WHERE pad.appointmentid=@pid AND pad.personid=@pid)\r\nEND\r\nELSE \r\n    SET @screennum=0";

		// Token: 0x040004E3 RID: 1251
		internal const string QI_POC = "INSERT INTO appointments (startdate,enddate,personid,apptypeid,appcode,subject,dateadded,overrideColour) \r\nVALUES (@startdate,@enddate,@whoenteredpid,@apptypeid,@appcode,@subtitle,getdate(),@overrideColour)\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS appointmentid";

		// Token: 0x040004E4 RID: 1252
		internal const string QU_POC = "UPDATE appointments SET startdate=@startdate,enddate=@enddate,apptypeid=@apptypeid,appcode=@appcode,subject=@subtitle\r\nWHERE appointmentid=@appid";
	}
}
