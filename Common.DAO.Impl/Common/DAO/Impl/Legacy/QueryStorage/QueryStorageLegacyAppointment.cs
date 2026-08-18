using System;

namespace TechnoPro.Common.DAO.Impl.Legacy.QueryStorage
{
	// Token: 0x020000AC RID: 172
	public static class QueryStorageLegacyAppointment
	{
		// Token: 0x04000236 RID: 566
		internal const string QS_APP_MODIFIED_HISTORY = "SELECT 'Created' AS action,app.dateadded AS action_date,'' AS changed_description,'' AS changed_room,'' AS changed_memo,'' AS changed_attendees,'' AS changed_cancelled,'' AS changed_noshow,'' AS changed_course,'' AS changed_other1,'' AS changed_other2,'' AS changed_icons,'' AS changed_datetime,p.firstname,p.lastname FROM appointments app LEFT JOIN people p ON p.personid=app.personid WHERE app.appointmentid=@appid UNION SELECT x.action,m.datemodified AS action_date,m.changed_description,m.changed_room,m.changed_memo,m.changed_attendees,m.changed_cancelled,m.changed_noshow,m.changed_course,m.changed_other1,m.changed_other2,m.changed_icons,m.changed_datetime,p.firstname,p.lastname FROM appointmentsmodifieddates m LEFT JOIN (SELECT 1 AS howmodifiedcode,'Modified' AS action UNION SELECT 2 AS howmodifiedcode,'Deleted' AS action) x ON x.howmodifiedcode=m.howmodifiedcode LEFT JOIN people p ON p.personid=m.personid WHERE m.appointmentid=@appid ORDER BY action_date";
	}
}
