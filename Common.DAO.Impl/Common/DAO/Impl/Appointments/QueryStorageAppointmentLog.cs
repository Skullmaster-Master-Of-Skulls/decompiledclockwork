using System;

namespace TechnoPro.Common.DAO.Impl.Appointments
{
	// Token: 0x0200012F RID: 303
	internal static class QueryStorageAppointmentLog
	{
		// Token: 0x040004FF RID: 1279
		internal const string QI_APPOINTMENT_LOG = "IF EXISTS(SELECT appointmentid FROM appointmentsmodifieddates WHERE appointmentid=@appid AND howmodifiedcode=@howmodifiedcode AND personid=@whomodified AND DATEDIFF(s,datemodified,getdate())<=2)\r\nBEGIN\r\n    UPDATE appointmentsmodifieddates SET \r\n    datemodified=getdate(),changed_cancelled=changed_cancelled | @changed_cancelled,changed_room = changed_room | @changed_room,\r\n    changed_memo=changed_memo | @changed_memo,changed_attendees=changed_attendees | @changed_attendees,changed_icons = changed_icons | @changed_icons\r\n    WHERE appointmentid=@appid AND howmodifiedcode=@howmodifiedcode AND personid=@whomodified AND DATEDIFF(s,datemodified,getdate())<=2\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO appointmentsmodifieddates (appointmentid,datemodified,personid,howmodifiedcode,changed_cancelled,changed_room,changed_memo,changed_attendees,changed_icons)\r\n    VALUES (@appid,getdate(),@whomodified,@howmodifiedcode,@changed_cancelled,@changed_room,@changed_memo,@changed_attendees,@changed_icons)\r\nEND";

		// Token: 0x04000500 RID: 1280
		internal const string QI_LOG_APPOINTMENT_MODIFICATION_PRE_CHANGE_COMMITTED = "DECLARE @pidlist varchar(max)\r\nSELECT @pidlist=COALESCE(@pidlist + ',', '' ) + CAST(personid AS varchar(256))\r\nFROM attendees WHERE appointmentid=@appointmentid\r\n\r\nSET @pidlist= 'pids=' + @pidlist + ';who=' + CAST(@whomodifiedpid AS varchar(256));\r\n\r\nINSERT INTO archive_appointments (auditaction,appointmentid,apptypeid,startdate,enddate,cancelled,dateadded,personid,ishidden,islocked,extraattendeescount,appcode,groupcode,[subject],location,examid,caseid,totalbreakminutes,sittingid,extrainfo)\r\n    SELECT \t'PRE',a.appointmentid,a.apptypeid,a.startdate,a.enddate,a.cancelled,a.dateadded,a.personid,a.ishidden,a.islocked,a.extraattendeescount,a.appcode,a.groupcode,\r\n\t\t    a.[subject],a.location,a.examid,a.caseid,a.totalbreakminutes,a.sittingid,COALESCE(@pidlist,'-') AS extrainfo\r\n    FROM    appointments a\r\n    WHERE a.appointmentid=@appointmentid";

		// Token: 0x04000501 RID: 1281
		internal const string QI_APPOINTMENT_DELETED_LOG = "INSERT INTO appointmentsdeleteddates (appointmentid,datedeleted,personid) \r\nVALUES (@appid,getdate(),@whodeleted)";
	}
}
