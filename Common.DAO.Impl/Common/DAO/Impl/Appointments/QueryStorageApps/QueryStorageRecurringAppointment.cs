using System;

namespace TechnoPro.Common.DAO.Impl.Appointments.QueryStorageApps
{
	// Token: 0x0200013B RID: 315
	public static class QueryStorageRecurringAppointment
	{
		// Token: 0x0400056F RID: 1391
		internal const string QS_APPOINTMENT_EDIT_PERMISSIONS_FOR_RECURRING_SET_AND_SPECIFIC_USER = "EXEC sp_Calendar_AllowedAppointmentsToEditFromRecurringSet @appid,@pid";

		// Token: 0x04000570 RID: 1392
		internal const string QS_RECURRING_APPOINTMENTS_BY_GROUPCODE = "SELECT DISTINCT app.appointmentid FROM appointments app WHERE ";

		// Token: 0x04000571 RID: 1393
		internal const string QU_GROUPCODE = "UPDATE appointments SET groupcode=@groupcode WHERE appointmentid=@appid";
	}
}
