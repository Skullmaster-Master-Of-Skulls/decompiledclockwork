using System;

namespace TechnoPro.Common.DAO.Impl.Intake.QueryStorage
{
	// Token: 0x020000C7 RID: 199
	internal static class QueryStorageIntake
	{
		// Token: 0x040002CB RID: 715
		internal const string QI_CREATE_INTAKE_ACCOUNT = "INSERT INTO people_intake (firstname,lastname,middlename,student_no,email,isactive,ip,dateadded,note) VALUES (@fne,@lne,@mne,@sne,@email,1,@ip,getdate(),NULL)\r\nSET @pid=(SELECT TOP 1 CAST(@@identity AS int) AS pid FROM people_intake)";

		// Token: 0x040002CC RID: 716
		internal const string QS_PENDING_INTAKE_ENTRIES = "SELECT\tp.personid,p.firstname,p.middlename,p.lastname,p.student_no,p.email,p.isactive,p.[ip],p.dateadded,\r\n\t\tp.IntakeStatusId,pis.title,pis.[description],pis.backgroundcolor,pis.IsInactive,pis.OrderNum,p.note,\r\n        pp.personid AS existingpid\r\nFROM\tpeople_intake p LEFT JOIN people_intake_status pis ON pis.IntakeStatusId=p.IntakeStatusId\r\n        LEFT JOIN people pp ON pp.student_no=p.student_no\r\nWHERE\tp.isactive=1\r\nORDER BY dateadded DESC";

		// Token: 0x040002CD RID: 717
		internal const string QS_PENDING_INTAKE_QUEUE_ENTRIES = "SELECT\tp.personid,p.firstname,p.middlename,p.lastname,p.student_no,p.email,p.isactive,p.[ip],p.dateadded,\r\n\t\tp.IntakeStatusId,pis.title,pis.[description],pis.backgroundcolor,pis.IsInactive,pis.OrderNum,p.note,\r\n        pp.personid AS existingpid,\r\n        mii.controlvalue AS seldeptid,ll.lookuptext AS seldepttitle\r\nFROM\tpeople_intake p LEFT JOIN people_intake_status pis ON pis.IntakeStatusId=p.IntakeStatusId\r\n        LEFT JOIN people pp ON pp.student_no=p.student_no\r\n        LEFT JOIN maininfointake mii ON mii.personid=p.personid AND mii.controlid=@cid AND @cid>0\r\n        LEFT JOIN lookuplists ll ON ll.lookuplistid=mii.controlvalue\r\nWHERE\tp.isactive=1\r\nORDER BY dateadded DESC";

		// Token: 0x040002CE RID: 718
		internal const string QU_INTAKE_STATUS = "UPDATE people_intake SET IntakeStatusId=@intakeStatusId WHERE personid IN (SELECT orderid AS personid FROM splitorderids(@pids,','))";

		// Token: 0x040002CF RID: 719
		internal const string QU_INTAKE_NOTE = "UPDATE people_intake SET note=@note WHERE personid IN (SELECT orderid AS personid FROM splitorderids(@pids,','))";

		// Token: 0x040002D0 RID: 720
		internal const string QU_MARK_INTAKES_INACTIVE_BY_STUDENTNUMBER = "UPDATE people_intake SET isactive=0 WHERE student_no=@snum";

		// Token: 0x040002D1 RID: 721
		internal const string QU_MARK_INTAKES_INACTIVE_BY_PERSONIDS = "UPDATE people_intake SET isactive=0 WHERE personid IN (SELECT orderid AS personid FROM splitorderids(@pids,','))";

		// Token: 0x040002D2 RID: 722
		internal const string QS_INTAKE_STATUSES = "SELECT IntakeStatusId,Title,[Description],BackgroundColor,OrderNum,IsInactive FROM people_intake_status WHERE isinactive=0 ORDER BY OrderNum,Title";

		// Token: 0x040002D3 RID: 723
		internal const string QS_INTAKE_FORM_DATA = "SELECT personid INTO #tpids FROM people_intake WHERE student_no=@snume\r\nSELECT controlid INTO #tcids FROM dynamicscreencontrols WHERE screennum=@screennum\r\nSELECT #tcids.controlid,#tpids.personid INTO #tpc FROM #tcids LEFT JOIN #tpids ON 1=1\r\n\r\nSELECT\tx.controlid,MAX(x.personid) AS personid \r\nINTO #t1\r\nFROM \r\n(\r\nSELECT\t#tpc.controlid,#tpc.personid\r\nFROM\t#tpc LEFT JOIN maininfoIntake m ON m.controlid=#tpc.controlid AND m.personid=#tpc.personid\r\nWHERE\tNOT m.dataid IS NULL\r\nUNION ALL\r\nSELECT\t#tpc.controlid,#tpc.personid\r\nFROM\t#tpc LEFT JOIN otherinfoIntake m ON m.controlid=#tpc.controlid AND m.personid=#tpc.personid\r\nWHERE\tNOT m.dataid IS NULL\r\nUNION ALL\r\nSELECT\t#tpc.controlid,#tpc.personid\r\nFROM\t#tpc LEFT JOIN datetimeinfoIntake m ON m.controlid=#tpc.controlid AND m.personid=#tpc.personid\r\nWHERE\tNOT m.dataid IS NULL\r\nUNION ALL\r\nSELECT\t#tpc.controlid,#tpc.personid\r\nFROM\t#tpc LEFT JOIN imageinfoIntake m ON m.controlid=#tpc.controlid AND m.personid=#tpc.personid\r\nWHERE\tNOT m.dataid IS NULL\r\n) x GROUP BY x.controlid\r\n\r\nSELECT\t#t1.personid,#t1.controlid,m.*\r\nFROM\t#t1 LEFT JOIN perintakedata2 m ON m.personid=#t1.personid AND m.controlid=#t1.controlid \r\n\t\t\r\n\r\nDROP TABLE #tpids\r\nDROP TABLE #tcids\r\nDROP TABLE #tpc\r\nDROP TABLE #t1";

		// Token: 0x040002D4 RID: 724
		internal const string QS_INTAKE_PERSON_BY_STUDENT_NUMBER = "SELECT personid,firstname,middlename,lastname,student_no,email,dateadded FROM people_intake WHERE student_no=@snume AND isactive=1";

		// Token: 0x040002D5 RID: 725
		internal const string QS_INTAKE_PERSON_IDS_BY_STUDENT_NUMBER = "SELECT personid FROM people_intake WHERE isactive=1 AND student_no=@snume";
	}
}
