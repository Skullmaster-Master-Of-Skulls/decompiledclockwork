using System;

namespace TechnoPro.Common.DAO.Impl.AppointmentsReminder.QueryStorage
{
	// Token: 0x0200015B RID: 347
	public static class AppointmentsReminderQueryStorage
	{
		// Token: 0x04000609 RID: 1545
		internal const string SQ_GET_PEOPLE_EXCLUSION_LIST = "select PersonID from AppointmentsReminder_PeopleExclusionList";

		// Token: 0x0400060A RID: 1546
		internal const string SQ_GET_GROUP_INCLUSION_LIST = "select GroupID from AppointmentsReminder_GroupInclusionList";

		// Token: 0x0400060B RID: 1547
		internal const string SQ_EXCLUSION_LIST_CONTAINS_PERSON = "select PersonID from [AppointmentsReminder_PeopleExclusionList] where PersonID=@personid";

		// Token: 0x0400060C RID: 1548
		internal const string DQ_DELETE_APP_REMINDER = "update AppointmentsReminder_Notification\r\n            set\t WasDeleted = 1\r\n            where AppointmentID = @appointmentid and PersonID = @personid\r\n";

		// Token: 0x0400060D RID: 1549
		internal const string UQ_UDPATE_APP_REMINDER = "update AppointmentsReminder_Notification\r\n                set\t startDate\t\t\t\t= @startdate\r\n\t            ,endDate\t\t\t\t= @enddate\r\n\t            ,[Subject]\t\t\t\t= @subject\r\n\t            ,NotificationDatetime\t= @notificationdatetime\r\n            where AppointmentID = @appointmentid and PersonID = @personid";

		// Token: 0x0400060E RID: 1550
		internal const string IQ_ADD_APP_REMINDER = "insert into AppointmentsReminder_Notification \r\n\t\t            (AppointmentID, PersonID, startDate, endDate, [Subject], NotificationDatetime)\r\n            values\t(@appointmentid, @personid, @startdate, @enddate, @subject, @notificationdatetime)\r\n            set @appreminderid = SCOPE_IDENTITY()";

		// Token: 0x0400060F RID: 1551
		internal const string IQ_ADD_PEOPLE_TO_EXCLUSION_LIST = "if not exists (select 1 from AppointmentsReminder_PeopleExclusionList where PersonID=@personid)\r\n                begin\r\n                    insert into AppointmentsReminder_PeopleExclusionList (PersonID) values (@personid)\r\n                end";

		// Token: 0x04000610 RID: 1552
		internal const string DQ_REMOVE_PEOPLE_FROM_EXCLUSION_LIST = "delete from AppointmentsReminder_PeopleExclusionList where PersonID=@personid";
	}
}
