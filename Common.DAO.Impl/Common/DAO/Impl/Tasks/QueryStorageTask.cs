using System;

namespace TechnoPro.Common.DAO.Impl.Tasks
{
	// Token: 0x0200003A RID: 58
	public class QueryStorageTask
	{
		// Token: 0x04000095 RID: 149
		internal const string QS_TASK_GROUPS = "SELECT\ttg.taskgroupid,tg.personid,p.firstname,p.lastname,p.student_no,\r\n\t\ttg.taskgroupdescription,tg.ordernum,tg.isactive,tg.isprivate AS isprivategroup,tg.parenttaskgroupid,\r\n        tg.ownerpersonid AS groupowner_personid,pgroup.firstName AS groupowner_firstname,pgroup.lastName AS groupowner_lastname,pgroup.student_no AS groupowner_student_no\r\nFROM\tTaskGroups tg LEFT JOIN people p ON p.PersonID=tg.personid\r\n        LEFT JOIN people pgroup ON pgroup.personid=tg.ownerpersonid\r\nWHERE\ttg.isactive=1\r\n        AND \r\n        (\r\n            (@includeprivate=1 AND tg.isprivate=1 AND tg.ownerpersonid=@whoami)\r\n            OR\r\n            (@includeshared=1 AND tg.isprivate=0)\r\n        )\r\nORDER BY tg.ordernum,tg.taskgroupdescription";

		// Token: 0x04000096 RID: 150
		internal const string QS_TASK_NOTES_BY_ID = "SELECT tn.notes AS notes_notes,\r\n\t\ttn.tasknoteid,tn.whoentered AS notes_whoentered_personid,tn.dateentered AS notes_dateentered,pnoteswe.firstName AS notes_whoentered_firstname,pnoteswe.lastName AS notes_whoentered_lastname,pnoteswe.student_no AS notes_whoentered_student_no,\r\n\t\ttn.wholastmodified AS notes_whomodified_personid,tn.datelastmodified AS notes_datelastmodified,pnoteslm.firstname AS notes_whomodified_firstname,pnoteslm.lastName AS notes_whomodified_lastname,pnoteslm.student_no AS notes_whomodified_student_no\r\nFROM    tasknotes tn \r\n        LEFT JOIN people pnoteswe ON pnoteswe.personid=tn.whoentered \r\n\t\tLEFT JOIN people pnoteslm ON pnoteslm.PersonID=tn.wholastmodified \r\nWHERE   tn.taskid=@taskid\r\nORDER BY tn.dateentered DESC";

		// Token: 0x04000097 RID: 151
		internal const string QS_TASK_BY_ID = "SELECT\tt.taskid,t.dateentered,t.personid,powner.firstName,powner.lastName,powner.student_no,\r\n\t\tt.[description],t.isEncrypted,t.dueDate,t.completed,t.iconID,t.orderNum,\r\n\t\tt.reminder,t.taskGroupID,t.progress,t.priority,t.startDate,t.Title,t.OverrideColourArgb,\r\n\t\ttc.taskclientid,tc.personid AS client_personid,tc.notes AS client_notes,\r\n\t\tp.firstName AS client_firstname,p.lastName AS client_lastname,p.student_no AS client_student_no,\r\n        tn.notes AS notes_notes,\r\n\t\ttn.tasknoteid,tn.whoentered AS notes_whoentered_personid,tn.dateentered AS notes_dateentered,pnoteswe.firstName AS notes_whoentered_firstname,pnoteswe.lastName AS notes_whoentered_lastname,pnoteswe.student_no AS notes_whoentered_student_no,\r\n\t\ttn.wholastmodified AS notes_whomodified_personid,tn.datelastmodified AS notes_datelastmodified,pnoteslm.firstname AS notes_whomodified_firstname,pnoteslm.lastName AS notes_whomodified_lastname,pnoteslm.student_no AS notes_whomodified_student_no,\r\n\t\ttg.ownerpersonid AS groupowner_personid,pgroup.firstName AS groupowner_firstname,pgroup.lastName AS groupowner_lastname,pgroup.student_no AS groupowner_student_no,\r\n        t.primarytaskid,t.whoentered AS whoenteredpersonid,pwe.firstname AS whoenteredfirstname,pwe.lastname AS whoenteredlastname,pwe.student_no AS whoenteredstudent_no,\r\n        t.wholastmodified AS whomodifiedpersonid,pwl.firstname AS whomodifiedfirstname,pwl.lastname AS whomodifiedlastname,pwl.student_no AS whomodifiedstudent_no,\r\n        t.datelastmodified,tg.taskgroupdescription,tg.isactive,t.isprivate,tg.isprivate AS isprivategroup,tg.parenttaskgroupid\r\nFROM\tTasks t LEFT JOIN TaskGroups tg ON tg.TaskGroupID=t.taskGroupID \r\n\t\tLEFT JOIN TaskNotes tn ON tn.TaskId=t.TaskID \r\n\t\tLEFT JOIN TaskClients tc ON tc.TaskId=t.TaskID\r\n\t\tLEFT JOIN people p ON p.PersonID=tc.personid \r\n\t\tLEFT JOIN people powner ON powner.personid=t.personID \r\n\t\tLEFT JOIN people pnoteswe ON pnoteswe.personid=tn.whoentered \r\n\t\tLEFT JOIN people pnoteslm ON pnoteslm.PersonID=tn.wholastmodified \r\n\t\tLEFT JOIN people pgroup ON pgroup.PersonID=tg.ownerpersonid\r\n        LEFT JOIN people pwe ON pwe.personid=t.whoentered\r\n        LEFT JOIN people pwl ON pwl.personid=t.wholastmodified\r\nWHERE   t.taskid=@taskid AND t.isactive=1\r\nORDER BY t.TaskID,t.dateentered";

		// Token: 0x04000098 RID: 152
		internal const string QS_TASKS = "SELECT\tt.taskid,t.dateentered,t.personid,powner.firstName,powner.lastName,powner.student_no,\r\n\t\tt.[description],t.isEncrypted,t.dueDate,t.completed,t.iconID,t.orderNum,\r\n\t\tt.reminder,t.taskGroupID,t.progress,t.priority,t.startDate,t.Title,t.OverrideColourArgb,\r\n\t\ttc.taskclientid,tc.personid AS client_personid,tc.notes AS client_notes,\r\n\t\tp.firstName AS client_firstname,p.lastName AS client_lastname,p.student_no AS client_student_no,\r\n        tn.notes AS notes_notes,\r\n\t\ttn.tasknoteid,tn.whoentered AS notes_whoentered_personid,tn.dateentered AS notes_dateentered,pnoteswe.firstName AS notes_whoentered_firstname,pnoteswe.lastName AS notes_whoentered_lastname,pnoteswe.student_no AS notes_whoentered_student_no,\r\n\t\ttn.wholastmodified AS notes_whomodified_personid,tn.datelastmodified AS notes_datelastmodified,pnoteslm.firstname AS notes_whomodified_firstname,pnoteslm.lastName AS notes_whomodified_lastname,pnoteslm.student_no AS notes_whomodified_student_no,\r\n\t\ttg.ownerpersonid AS groupowner_personid,pgroup.firstName AS groupowner_firstname,pgroup.lastName AS groupowner_lastname,pgroup.student_no AS groupowner_student_no,\r\n        t.primarytaskid,t.whoentered AS whoenteredpersonid,pwe.firstname AS whoenteredfirstname,pwe.lastname AS whoenteredlastname,pwe.student_no AS whoenteredstudent_no,\r\n        t.wholastmodified AS whomodifiedpersonid,pwl.firstname AS whomodifiedfirstname,pwl.lastname AS whomodifiedlastname,pwl.student_no AS whomodifiedstudent_no,\r\n        t.datelastmodified,tg.taskgroupdescription,tg.isactive,t.isprivate,tg.isprivate AS isprivategroup,tg.parenttaskgroupid\r\nFROM\tTasks t LEFT JOIN TaskGroups tg ON tg.TaskGroupID=t.taskGroupID \r\n\t\tLEFT JOIN TaskNotes tn ON @loadnotes=1 AND tn.TaskId=t.TaskID \r\n\t\tLEFT JOIN TaskClients tc ON @loadclients=1 AND tc.TaskId=t.TaskID\r\n\t\tLEFT JOIN people p ON p.PersonID=tc.personid \r\n\t\tLEFT JOIN people powner ON powner.personid=t.personID \r\n\t\tLEFT JOIN people pnoteswe ON pnoteswe.personid=tn.whoentered \r\n\t\tLEFT JOIN people pnoteslm ON pnoteslm.PersonID=tn.wholastmodified \r\n\t\tLEFT JOIN people pgroup ON pgroup.PersonID=tg.ownerpersonid\r\n        LEFT JOIN people pwe ON pwe.personid=t.whoentered\r\n        LEFT JOIN people pwl ON pwl.personid=t.wholastmodified\r\nWHERE\t(\r\n          (@includeprivate=1 AND t.isprivate=1 AND t.personid=@whoami)\r\n          OR (@includeshared=1 AND t.isprivate=0)\r\n          OR (@includeassigned=1 AND t.taskid IN (SELECT taskid FROM taskclients WHERE personid=@whoami))\r\n        )\r\n        AND t.removefromlist=0\r\n        AND t.isactive=1\r\nORDER BY t.TaskID,t.dateentered";

		// Token: 0x04000099 RID: 153
		internal const string QS_COMPLETED_TASKS = "SELECT\tt.taskid,t.dateentered,t.personid,powner.firstName,powner.lastName,powner.student_no,\r\n\t\tt.[description],t.isEncrypted,t.dueDate,t.completed,t.iconID,t.orderNum,\r\n\t\tt.reminder,t.taskGroupID,t.progress,t.priority,t.startDate,t.Title,t.OverrideColourArgb,\r\n\t\ttc.taskclientid,tc.personid AS client_personid,tc.notes AS client_notes,\r\n\t\tp.firstName AS client_firstname,p.lastName AS client_lastname,p.student_no AS client_student_no,\r\n        tn.notes AS notes_notes,\r\n\t\ttn.tasknoteid,tn.whoentered AS notes_whoentered_personid,tn.dateentered AS notes_dateentered,pnoteswe.firstName AS notes_whoentered_firstname,pnoteswe.lastName AS notes_whoentered_lastname,pnoteswe.student_no AS notes_whoentered_student_no,\r\n\t\ttn.wholastmodified AS notes_whomodified_personid,tn.datelastmodified AS notes_datelastmodified,pnoteslm.firstname AS notes_whomodified_firstname,pnoteslm.lastName AS notes_whomodified_lastname,pnoteslm.student_no AS notes_whomodified_student_no,\r\n\t\ttg.ownerpersonid AS groupowner_personid,pgroup.firstName AS groupowner_firstname,pgroup.lastName AS groupowner_lastname,pgroup.student_no AS groupowner_student_no,\r\n        t.primarytaskid,t.whoentered AS whoenteredpersonid,pwe.firstname AS whoenteredfirstname,pwe.lastname AS whoenteredlastname,pwe.student_no AS whoenteredstudent_no,\r\n        t.wholastmodified AS whomodifiedpersonid,pwe.firstname AS whomodifiedfirstname,pwe.lastname AS whomodifiedlastname,pwe.student_no AS whomodifiedstudent_no,\r\n        t.datelastmodified,tg.taskgroupdescription,tg.isactive,t.isprivate,tg.isprivate AS isprivategroup,tg.parenttaskgroupid\r\nFROM\tTasks t LEFT JOIN TaskGroups tg ON tg.TaskGroupID=t.taskGroupID \r\n\t\tLEFT JOIN TaskNotes tn ON tn.TaskId=t.TaskID \r\n\t\tLEFT JOIN TaskClients tc ON tc.TaskId=t.TaskID\r\n\t\tLEFT JOIN people p ON p.PersonID=tc.personid \r\n\t\tLEFT JOIN people powner ON powner.personid=t.personID \r\n\t\tLEFT JOIN people pnoteswe ON pnoteswe.personid=tn.whoentered \r\n\t\tLEFT JOIN people pnoteslm ON pnoteslm.PersonID=tn.wholastmodified \r\n\t\tLEFT JOIN people pgroup ON pgroup.PersonID=tg.ownerpersonid \r\n        LEFT JOIN people pwe ON pwe.personid=t.whoentered\r\n        LEFT JOIN people pwl ON pwl.personid=t.wholastmodified\r\nWHERE\t(\r\n          (@includeprivate=1 AND t.isprivate=1 AND t.personid=@whoami)\r\n          OR (@includeshared=1 AND t.isprivate=0)\r\n          OR (@includeassigned=1 AND t.taskid IN (SELECT taskid FROM taskclients WHERE personid=@whoami))\r\n        )\r\n        AND t.completed=1\r\n        AND ((t.dateentered>=@startdate AND t.dateentered<=@enddate)\r\n            OR (NOT t.datelastmodified IS NULL AND t.datelastmodified>=@startdate AND t.datelastmodified<=@enddate)\r\n            OR (NOT t.duedate IS NULL AND t.duedate>=@startdate AND t.duedate<=@enddate)\r\n            )\r\n        AND t.isactive=1\r\nORDER BY t.TaskID,t.dateentered";

		// Token: 0x0400009A RID: 154
		internal const string QI_TASK_GROUP = "INSERT INTO taskgroups (ownerpersonid,taskgroupdescription,ordernum,isactive,isprivate,parenttaskgroupid) VALUES (@pid,@description,@ordernum,@isactive,@isprivate,@parenttaskgroupid); SELECT CAST(SCOPE_IDENTITY() AS int) AS taskgroupid";

		// Token: 0x0400009B RID: 155
		internal const string QI_TASK_CLIENT = "INSERT INTO taskclients (taskid,personid,notes) VALUES (@taskid,@personid,@notes)";

		// Token: 0x0400009C RID: 156
		internal const string QI_TASK_NOTES = "INSERT INTO tasknotes (taskid,whoentered,wholastmodified,notes) \r\nVALUES (@taskid,@whoentered,@whoentered,@notes); SELECT CAST(SCOPE_IDENTITY() AS int) AS tasknoteid";

		// Token: 0x0400009D RID: 157
		internal const string QI_TASK = "INSERT INTO tasks (description,duedate,iconid,completed,ordernum,priority,progress,taskgroupid,title,whoentered,dateentered,primarytaskid) \r\nVALUES (@description,@duedate,@iconid,@completed,@ordernum,@priority,@progress,@taskgroupid,@title,@whoentered,getdate(),@primarytaskid);\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS taskid";

		// Token: 0x0400009E RID: 158
		internal const string QD_TASK_GROUP = "DELETE FROM taskgroups WHERE taskgroupid=@taskgroupid";

		// Token: 0x0400009F RID: 159
		internal const string QD_TASK = "UPDATE tasks SET isactive=0 WHERE taskid=@taskid";

		// Token: 0x040000A0 RID: 160
		internal const string QD_TASK_CLIENTS = "DELETE FROM taskclients WHERE taskid=@taskid AND NOT taskclientid IN (SELECT orderid AS taskclientid FROM splitorderids(@ids,','))";

		// Token: 0x040000A1 RID: 161
		internal const string QD_TASK_ALL_NOTES = "DELETE FROM tasknotes WHERE taskid=@taskid AND NOT tasknoteid IN (SELECT orderid AS tasknoteid FROM splitorderids(@ids,','))";

		// Token: 0x040000A2 RID: 162
		internal const string QD_TASK_ALL_CLIENTS = "DELETE FROM taskclients WHERE taskid=@taskid";

		// Token: 0x040000A3 RID: 163
		internal const string QD_TASK_NOTES = "DELETE FROM tasknotes WHERE taskid=@taskid AND NOT tasknoteid IN (SELECT orderid AS tasknoteid FROM splitorderids(@ids,','))";

		// Token: 0x040000A4 RID: 164
		internal const string QU_TASK_GROUP = "UPDATE taskgroups SET parenttaskgroupid=@parenttaskgroupid,ownerpersonid=@pid,taskgroupdescription=@description,ordernum=@ordernum,isactive=@isactive,isprivate=@isprivate WHERE taskgroupid=@taskgroupid";

		// Token: 0x040000A5 RID: 165
		internal const string QU_TASK_COMPLETED = "UPDATE tasks SET completed=@iscompleted WHERE taskid=@taskid";

		// Token: 0x040000A6 RID: 166
		internal const string QU_TASK_COMPLETED_AND_PROGRESS = "UPDATE tasks SET completed=@iscompleted,progress=@progress WHERE taskid=@taskid";

		// Token: 0x040000A7 RID: 167
		internal const string QU_TASK_REMOVEFROMLIST = "UPDATE tasks SET removefromlist=@removefromlist WHERE taskid=@taskid";

		// Token: 0x040000A8 RID: 168
		internal const string QU_TASK = "UPDATE tasks SET description=@description,duedate=@duedate,iconid=@iconid,completed=@completed,\r\nordernum=@ordernum,priority=@priority,progress=@progress,taskgroupid=@taskgroupid,title=@title,\r\nwholastmodified=@wholastmodified,datelastmodified=getdate(),primarytaskid=@primarytaskid\r\nWHERE taskid=@taskid";

		// Token: 0x040000A9 RID: 169
		internal const string QU_TASK_NOTES = "UPDATE tasknotes SET wholastmodified=@wholastmodified,datelastmodified=getdate(),notes=@notes WHERE tasknoteid=@tasknoteid";

		// Token: 0x040000AA RID: 170
		internal const string QU_TASK_CLIENT = "UPDATE taskclients SET notes=@notes WHERE taskclientid=@taskclientid";
	}
}
