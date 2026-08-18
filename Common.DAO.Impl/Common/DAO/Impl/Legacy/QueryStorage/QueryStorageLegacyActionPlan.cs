using System;

namespace TechnoPro.Common.DAO.Impl.Legacy.QueryStorage
{
	// Token: 0x020000AB RID: 171
	public static class QueryStorageLegacyActionPlan
	{
		// Token: 0x0400022E RID: 558
		internal const string QI_CREATE_ACTION_PLAN_NOTE = "INSERT INTO studentfiletasks_note (personid,whoadded,wholastmodified,notegroup,notedescription,staffnotes) VALUES (@personid,@whoadded,@wholastmodified,@notegroup,@notedescription,@staffnotes)\r\nSET @noteid=(SELECT TOP 1 CAST(@@identity AS int) AS noteid FROM studentfiletasks_note)";

		// Token: 0x0400022F RID: 559
		internal const string QU_UPDATE_ACTION_PLAN_NOTE = "UPDATE studentfiletasks_note SET datelastmodified=getdate(),wholastmodified=@whoami,notegroup=@notegroup,notedescription=@notedescription,staffnotes=@staffnotes WHERE noteid=@noteid";

		// Token: 0x04000230 RID: 560
		internal const string QD_DELETE_ACTION_PLAN_NOTE = "DELETE FROM studentfiletasks_note WHERE noteid=@noteid";

		// Token: 0x04000231 RID: 561
		internal const string QS_LOAD_NOTES = "SELECT n.noteid,n.personid,n.datelastmodified,n.whoadded,n.wholastmodified,'' AS who,n.notegroup,n.notedescription,n.staffnotes\r\n,p.firstname,p.lastname,n.dateadded\r\nFROM studentfiletasks_note n LEFT JOIN people p ON p.personid=n.wholastmodified\r\nWHERE n.personid=@pid\r\nORDER BY n.datelastmodified";

		// Token: 0x04000232 RID: 562
		internal const string QU_UPDATE_ACTION_PLAN_TASK = "UPDATE studentfiletasks_task SET whoresponsiblecode=@whoresponsiblecode,datelastmodified=getdate(),wholastmodified=@whoami,description=@description,completedid=@completedid,staffnotes=@staffnotes,studentnotes=@studentnotes WHERE taskid=@taskid";

		// Token: 0x04000233 RID: 563
		internal const string QI_CREATE_ACTION_PLAN_TASK = "INSERT INTO studentfiletasks_task (personid,whoresponsiblecode,whoadded,wholastmodified,description,completedid,staffnotes,studentnotes) VALUES (@personid,@whoresponsiblecode,@whoadded,@wholastmodified,@description,@completedid,@staffnotes,@studentnotes)\r\nSET @taskid=(SELECT TOP 1 CAST(@@identity AS int) AS taskid FROM studentfiletasks_task)";

		// Token: 0x04000234 RID: 564
		internal const string QS_DELETE_ACTION_PLAN_TASK = "DELETE FROM studentfiletasks_task WHERE taskid=@taskid";

		// Token: 0x04000235 RID: 565
		internal const string QS_LOAD_TASKS = "SELECT t.taskid,t.whoresponsiblecode,t.datelastmodified,t.whoadded,t.wholastmodified,'' AS who,'' AS Assigned_To,t.[group],t.description,t.completedid,c.title AS completed,c.meanscomplete,t.staffnotes,t.studentnotes,t.ordernum\r\n,p.firstname,p.lastname,t.dateadded,t.personid\r\nFROM studentfiletasks_task t LEFT JOIN studentfiletasks_completed c ON c.completedid=t.completedid\r\nLEFT JOIN people p ON p.personid=t.wholastmodified\r\nWHERE t.personid=@pid\r\nORDER BY t.datelastmodified";
	}
}
