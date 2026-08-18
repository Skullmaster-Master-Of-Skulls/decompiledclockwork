using System;

namespace TechnoPro.Common.DAO.Impl.Cases
{
	// Token: 0x02000118 RID: 280
	public class QueryStorageCases
	{
		// Token: 0x040004A6 RID: 1190
		internal const string QS_CASE_CLIENTS = "SELECT    DISTINCT ipc.personid AS infopcid,att.personid,p.firstname,p.middlename,p.lastname,p.student_no,att.usertype FROM infopc ipc LEFT JOIN infopcpeople att ON att.infopcid=ipc.personid LEFT JOIN people p ON p.personid=att.personid WHERE ipc.personid=@infopcid";

		// Token: 0x040004A7 RID: 1191
		internal const string QS_CASE = "DECLARE @statuscid int\r\nSET @statuscid=(SELECT TOP 1 dsc.controlid FROM DynamicScreenControls dsc LEFT JOIN DynamicControls dc ON dc.ControlID=dsc.controlID WHERE dsc.screenNum=@screennum AND dc.controlcaption LIKE '%status%')\r\n\r\nSELECT DISTINCT ipc.personid AS infopcid,ipc.student_no AS CaseNumber,ipc.dateentered,\r\n        ipc.whoentered AS whoenteredpersonid,p.firstname AS whoenteredfirstname,p.lastname AS whoenteredlastname,p.student_no AS whoenteredstudent_no,\r\n\t\tpcd.valtext AS [status],\r\n        att.personid,att.usertype,\r\n\t\tp2.lastname,p2.firstname,p2.middlename,p2.student_no,ipc.title\r\nFROM    infopc ipc LEFT JOIN people p ON p.personid=ipc.whoentered\r\n        LEFT JOIN pcdata2 pcd ON pcd.infopcid=ipc.personid AND pcd.controlid=@statuscid\r\n        LEFT JOIN infopcpeople att ON att.infopcid=ipc.personid\r\n\t\tLEFT JOIN people p2 ON p2.personid=att.personid\r\nWHERE ipc.personid=@infopcid";

		// Token: 0x040004A8 RID: 1192
		internal const string QS_STUDENTS_CASES = "DECLARE @statuscid int\r\nSET @statuscid=(SELECT TOP 1 dsc.controlid FROM DynamicScreenControls dsc LEFT JOIN DynamicControls dc ON dc.ControlID=dsc.controlID WHERE dsc.screenNum=@screennum AND dc.controlcaption LIKE '%status%')\r\n\r\nSELECT DISTINCT ipc.personid AS infopcid,ipc.student_no AS CaseNumber,ipc.dateentered,\r\n            ipc.whoentered AS whoenteredpersonid,p.firstname AS whoenteredfirstname,p.lastname AS whoenteredlastname,p.student_no AS whoenteredstudent_no,\r\n            pcd.valtext AS [status],ipc.title\r\nFROM    infopc ipc LEFT JOIN people p ON p.personid=ipc.whoentered\r\n        LEFT JOIN pcdata2 pcd ON pcd.infopcid=ipc.personid AND pcd.controlid=@statuscid\r\nWHERE ipc.isactive=1 \r\n      AND ipc.personid IN (SELECT infopcid AS personid FROM infopcpeople WHERE personid=@pid)\r\n      --AND p.isactive=1 \r\n    AND EXISTS(SELECT screendataid FROM screendata WHERE personid=ipc.personid AND screennum=@screennum)\r\nORDER BY ipc.dateentered DESC,ipc.personid";

		// Token: 0x040004A9 RID: 1193
		internal const string QS_BASIC_APPOINTMENTS_BY_CASEID = "SELECT\ta.appointmentid,a.AppTypeID,a.[description] AS apptypedescription,\r\n        at.appointmenttypegroupid,atg.title AS apptypegrouptitle,\r\n\t\ta.appCode,a.startDate,a.endDate,a.[subject],a.location,\r\n\t\ta.cancelled,a.isLocked,a.isHidden,a.groupCode,a.extraattendeescount,\r\n\t\ta.AttendeeID,a.PersonID,a.firstName,a.lastName,a.student_no,a.miscCode,a.noShow,\r\n\t\tat.isCourse,at.isWorkshop,at.defaultColour,\r\n\t\tpg.groupid,ast.appointmentshowtimeasid,ast.showtimeastitle,ast.extraiconid,ast.showtimeascolour\r\nFROM\tapps a LEFT JOIN peoplegroups pg ON pg.personid=a.personid AND pg.groupid<10 \r\n\t\tLEFT JOIN AppointmentTypes at ON at.apptypeid=a.AppTypeID \r\n\t\tLEFT JOIN AppointmentTypeGroups atg ON atg.AppointmentTypeGroupID=at.appointmentTypeGroupID \r\n        LEFT JOIN AppointmentShowTimeAs ast ON ast.extraiconid=a.appcode\r\n WHERE a.caseid=@caseid ORDER BY a.startdate DESC";

		// Token: 0x040004AA RID: 1194
		internal const string QI_ADD_OR_UPDATE_CASE_CLIENT = "IF EXISTS(SELECT infopcid FROM infopcpeople WHERE infopcid=@infopcid AND personid=@pid)\r\n    UPDATE infopcpeople SET usertype=@usertype WHERE infopcid=@infopcid AND personid=@pid\r\nELSE\r\n    INSERT INTO infopcpeople (infopcid,personid,usertype) VALUES (@infopcid,@pid,@usertype)";

		// Token: 0x040004AB RID: 1195
		internal const string QI_CASE = "INSERT INTO infopc (student_no,dateentered,whoentered,description,title,isactive) VALUES (@student_no,getdate(),@whoenteredpid,'',@title,1)\r\nSET @infopcid=(SELECT TOP 1 CAST(SCOPE_IDENTITY() AS int) AS infopcid)";

		// Token: 0x040004AC RID: 1196
		internal const string QU_MERGE_DUPLICATE_CASES_FOR_TWO_STUDENTS = "UPDATE infopcpeople SET personid=@newpid WHERE personid=@oldpid";

		// Token: 0x040004AD RID: 1197
		internal const string QU_CASE_STUDENT_NUMBER = "UPDATE infopc SET student_no=@student_no WHERE personid=@infopcid";

		// Token: 0x040004AE RID: 1198
		internal const string QU_CASE_TITLE = "UPDATE infopc SET title=@title WHERE personid=@infopcid";

		// Token: 0x040004AF RID: 1199
		internal const string QD_CASE_CLIENT = "DELETE FROM infopcpeople WHERE infopcid=@infopcid AND personid=@pid";

		// Token: 0x040004B0 RID: 1200
		internal const string QD_CASE = "UPDATE infopc SET isactive=0 WHERE personid=@infopcid";
	}
}
