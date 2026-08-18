using System;

namespace TechnoPro.Common.DAO.Impl.Appointments
{
	// Token: 0x02000136 RID: 310
	internal static class QueryStorageShowTimeAs
	{
		// Token: 0x0400055D RID: 1373
		internal const string QS_APPOINTMENT_SHOWTIMEAS_ALL = "SELECT appointmentshowtimeasid,extraiconid,showtimeastitle,showtimeascolour FROM appointmentshowtimeas ORDER BY showtimeastitle";

		// Token: 0x0400055E RID: 1374
		internal const string QS_APPOINTMENT_SHOWTIMEAS_BY_ID = "SELECT appointmentshowtimeasid,extraiconid,showtimeastitle,showtimeascolour FROM appointmentshowtimeas WHERE appointmentshowtimeasid=@id";

		// Token: 0x0400055F RID: 1375
		internal const string QS_APPOINTMENT_SHOWTIMEAS_BY_APPCODE = "SELECT appointmentshowtimeasid,extraiconid,showtimeastitle,showtimeascolour FROM appointmentshowtimeas WHERE extraiconid=@appcode";

		// Token: 0x04000560 RID: 1376
		internal const string QD_APPOINTMENT_SHOWTIMEAS_BY_APPCODE = "DELETE FROM appointmentshowtimeas WHERE extraiconid=@appcode";

		// Token: 0x04000561 RID: 1377
		internal const string QD_APPOINTMENT_SHOWTIMEAS_BY_ID = "DELETE FROM appointmentshowtimeas WHERE appointmentshowtimeasid=@id";

		// Token: 0x04000562 RID: 1378
		internal const string QU_APPOINTMENT_SHOWTIMEAS = "UPDATE appointmentshowtimeas SET showtimeastitle=@title,showtimeascolour=@colour WHERE extraiconid=@appcode";

		// Token: 0x04000563 RID: 1379
		internal const string QI_APPOINTMENT_SHOWTIMEAS = "INSERT INTO appointmentshowtimeas (showtimeastitle,showtimeascolour,extraiconid)\r\nVALUES (@title,@colour,@appcode)\r\nSET @id=SCOPE_IDENTITY()";
	}
}
