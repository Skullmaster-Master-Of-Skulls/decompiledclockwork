using System;

namespace TechnoPro.Common.DAO.Impl.People
{
	// Token: 0x02000079 RID: 121
	public class QueryStoragePeople
	{
		// Token: 0x04000133 RID: 307
		internal const string QS_DELETED_USERS = "SELECT\t\tp.personid,p.firstname,p.middlename,p.lastname,p.student_no\r\n            ,pg2.groupid,g.description\r\nFROM        people p LEFT JOIN peoplegroups pg2 ON pg2.personid=p.personid\r\n            LEFT JOIN groups g ON g.groupid=pg2.groupid\r\nWHERE       p.isactive=0\r\n            AND (@gids IS NULL OR @gids='' OR p.personid IN (SELECT personid FROM peoplegroups WHERE groupid IN (SELECT orderid AS groupid FROM splitorderids(@gids,','))))\r\nORDER BY    p.personid";

		// Token: 0x04000134 RID: 308
		internal const string QS_PERSONGROUPID_BY_PERSONID_AND_GROUPID = "SELECT persongroupid FROM peoplegroups WHERE personid=@pid AND groupid=@gid";

		// Token: 0x04000135 RID: 309
		internal const string QS_LAST_PERSON_ADDED = "SELECT MAX(personid) AS personid FROM peoplelastaddition";

		// Token: 0x04000136 RID: 310
		internal const string QS_PIDS_GREATER_THAN = "SELECT personid FROM people WHERE personid>@pid";

		// Token: 0x04000137 RID: 311
		internal const string QS_USER_GROUP_MEMBERSHIPS = "SELECT pg.groupid,g.description \r\nFROM peoplegroups pg LEFT JOIN groups g ON g.groupid=pg.groupid \r\nWHERE pg.personid=@pid";

		// Token: 0x04000138 RID: 312
		internal const string QS_ANY_ACCOUNT_BY_STUDENT_NUMBER = "SELECT    p.personid,p.firstname,p.middlename,p.lastname,p.student_no\r\nFROM        people p\r\nWHERE       p.isactive=1 \r\n            AND p.student_no=@snume";

		// Token: 0x04000139 RID: 313
		internal const string QS_STUDENT_BY_EMAIL = "SELECT    p.personid,p.firstname,p.middlename,p.lastname,p.student_no\r\nFROM        people p\r\nWHERE       p.isactive=1 AND p.personid IN (SELECT personid FROM peoplegroups WHERE groupid=1)\r\n            AND p.personid IN (SELECT personid FROM perstudentdata WHERE controlid=@cid AND (valbytes=@))";

		// Token: 0x0400013A RID: 314
		internal const string QS_PERSON_DATEADDED = "SELECT dateadded FROM people WHERE personid=@pid";

		// Token: 0x0400013B RID: 315
		internal const string QS_STUDENTS_WITH_COMMON_INFO_MY_STUDENTS = "EXEC sp_Students_MyStudents @pid,@sd,@ed,@showappswith,@showadvisorfor,@includecancelled,@includenoshow";

		// Token: 0x0400013C RID: 316
		internal const string QS_COMMON_INFO = "SELECT    c.personid,c.email,c.oktoemail,c.emailisnotencrypted,\r\n            c.assignedcounsellorpid AS advisorpersonid,c.assignedcounsellorfirst AS advisorfirstname,\r\n            c.assignedcounsellorlast AS advisorlastname,'' AS advisorstudent_no,\r\n            c.advisortitle,c.advisoremail,c.advisorphone,c.phone,c.dateofbirth,c.gender\r\nFROM        common c\r\nWHERE       c.personid=@pid";

		// Token: 0x0400013D RID: 317
		internal const string QS_STUDENT_WITH_COMMON_INFO = "SELECT    p.firstname,p.middlename,p.lastname,p.student_no,p.isactive,\r\n            p.personid,c.email,c.oktoemail,c.emailisnotencrypted,\r\n            c.assignedcounsellorpid AS advisorpersonid,c.assignedcounsellorfirst AS advisorfirstname,\r\n            c.assignedcounsellorlast AS advisorlastname,'' AS advisorstudent_no,\r\n            c.advisortitle,c.advisoremail,c.advisorphone,c.phone,c.dateofbirth,c.gender\r\nFROM        people p LEFT JOIN common c ON c.personid=p.personid\r\nWHERE       p.personid=@pid AND p.isactive=1 AND NOT p.personid IS NULL";

		// Token: 0x0400013E RID: 318
		internal const string QS_STUDENTS_WITH_COMMON_INFO = "SELECT    p.firstname,p.middlename,p.lastname,p.student_no,p.isactive,\r\n            p.personid,c.email,c.oktoemail,c.emailisnotencrypted,\r\n            c.assignedcounsellorpid AS advisorpersonid,c.assignedcounsellorfirst AS advisorfirstname,\r\n            c.assignedcounsellorlast AS advisorlastname,'' AS advisorstudent_no,\r\n            c.advisortitle,c.advisoremail,c.advisorphone,c.phone,c.dateofbirth,c.gender\r\nFROM        people p LEFT JOIN common c ON c.personid=p.personid\r\nWHERE       p.isactive=1 AND NOT p.personid IS NULL AND p.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,','))\r\nORDER BY p.personid";

		// Token: 0x0400013F RID: 319
		internal const string QS_ALL_USER_OBJECTS = "IF @loadisactive=0\r\nBEGIN\r\n    SELECT    p.personid,p.firstname,p.middlename,p.lastname,p.student_no\r\n                ,pg.groupid,g.description\r\n    FROM        people p LEFT JOIN peoplegroups pg ON pg.personid=p.personid AND pg.groupid<10\r\n                LEFT JOIN groups g ON g.groupid=pg.groupid\r\n    WHERE       p.isactive=1\r\n    ORDER BY    p.personid\r\nEND\r\nELSE \r\nBEGIN\r\n    SELECT    p.personid,p.firstname,p.middlename,p.lastname,p.student_no\r\n                ,pg.groupid,g.description\r\n    FROM        people p LEFT JOIN peoplegroups pg ON pg.personid=p.personid AND pg.groupid<10\r\n                LEFT JOIN groups g ON g.groupid=pg.groupid\r\n    WHERE       p.isactive=1\r\n    ORDER BY    p.personid\r\nEND ";

		// Token: 0x04000140 RID: 320
		internal const string QS_PEOPLE_BY_GROUPID = "SELECT    pg.personid,p.firstname,p.middlename,p.lastname,p.student_no\r\n            ,pg2.groupid,g.description\r\nFROM        peoplegroups pg LEFT JOIN people p ON p.personid=pg.personid\r\n            LEFT JOIN peoplegroups pg2 ON pg2.personid=pg.personid\r\n            LEFT JOIN groups g ON g.groupid=pg2.groupid\r\nWHERE       pg.groupid=@gid AND p.isactive=1\r\nORDER BY    pg.personid";

		// Token: 0x04000141 RID: 321
		internal const string QS_PERSON_IDS_BY_GROUPS = "SELECT orderid AS groupid INTO #t1 FROM splitorderids(@gids,',');\r\n\r\nSELECT   DISTINCT pg.personid\r\nFROM    peoplegroups pg LEFT JOIN people p ON p.personid=pg.personid\r\nWHERE   pg.groupid IN (SELECT groupid FROM #t1)\r\n        AND p.isactive=1\r\n\r\nDROP TABLE #t1";

		// Token: 0x04000142 RID: 322
		internal const string QS_PEOPLE_BY_GROUPIDS = "SELECT orderid AS groupid INTO #t1 FROM splitorderids(@gids,',');\r\n\r\nSELECT    pg.personid,p.firstname,p.middlename,p.lastname,p.student_no\r\n            ,pg2.groupid,g.description\r\nFROM        peoplegroups pg LEFT JOIN people p ON p.personid=pg.personid\r\n            LEFT JOIN peoplegroups pg2 ON pg2.personid=pg.personid\r\n            LEFT JOIN groups g ON g.groupid=pg2.groupid\r\nWHERE       pg.groupid IN (SELECT groupid FROM #t1)\r\n            AND p.isactive=1\r\nORDER BY    pg.personid;\r\n\r\nDROP TABLE #t1";

		// Token: 0x04000143 RID: 323
		internal const string QS_PEOPLE_BY_GROUPIDS_AND_FILTER_PIDS = "SELECT orderid AS groupid INTO #t1 FROM splitorderids(@gids,',');\r\nSELECT orderid AS personid INTO #t2 FROM splitorderids(@pids,',');\r\n\r\nSELECT    pg.personid,p.firstname,p.middlename,p.lastname,p.student_no\r\n            ,pg2.groupid,g.description\r\nFROM        peoplegroups pg LEFT JOIN people p ON p.personid=pg.personid\r\n            LEFT JOIN peoplegroups pg2 ON pg2.personid=pg.personid\r\n            LEFT JOIN groups g ON g.groupid=pg2.groupid\r\nWHERE       pg.personid IN (SELECT personid FROM #t2) \r\n            AND pg.groupid IN (SELECT groupid FROM #t1)\r\n            AND p.isactive=1\r\nORDER BY    pg.personid;\r\n\r\nDROP TABLE #t1;\r\nDROP TABLE #t2";

		// Token: 0x04000144 RID: 324
		internal const string QS_PERSON_BY_PERSONID = "EXEC LoadPersonByPersonId @pid";

		// Token: 0x04000145 RID: 325
		internal const string QS_PERSONS_BY_PERSONIDS = "EXEC LoadPersonsByPersonIds @pids";

		// Token: 0x04000146 RID: 326
		internal const string QS_PERSON_BY_STUDENT_NO = "EXEC LoadPersonByStudentNumber @sne";

		// Token: 0x04000147 RID: 327
		internal const string QS_STUDENT_ACCOMMODATION_EXPIRY_DATE = "DECLARE @cid int\r\nSET @cid=(SELECT TOP 1 controlid FROM DynamicScreenControls WHERE screennum=4 AND controlID IN (SELECT controlID FROM DynamicControls WHERE ControlCode=6 AND Setting2=1) ORDER BY orderNum)\r\n\r\nSELECT controlvalue FROM DateTimeInfoAccommodationPS WHERE PersonID=@pid AND ControlID=@cid";

		// Token: 0x04000148 RID: 328
		internal const string QS_ALL_GROUPS = "SELECT    groupid,description,isprimary,viewappsvisible,fulldescription,ordernum \r\nFROM        groups\r\nORDER BY description,ordernum";

		// Token: 0x04000149 RID: 329
		internal const string QS_ALL_ROOM_GROUPS = "SELECT groupid INTO #t1 FROM peoplegroups WHERE personid IN (SELECT personid FROM peoplegroups WHERE groupid=3);\r\n\r\nSELECT    groupid,description,isprimary,viewappsvisible,fulldescription,ordernum \r\nFROM        groups\r\nWHERE   groupid IN (SELECT groupid FROM #t1)\r\nORDER BY description,ordernum;\r\n\r\nDROP TABLE #t1";

		// Token: 0x0400014A RID: 330
		internal const string QI_TEMP_STUDENT_NUMBER = "INSERT INTO uniqueids (dateadded) VALUES (getdate())\r\nSET @uniqueid=(SELECT TOP 1 CAST(SCOPE_IDENTITY() AS int) AS uniqueid)\r\n\r\nDELETE FROM uniqueids WHERE uniqueid=@uniqueid";

		// Token: 0x0400014B RID: 331
		internal const string QI_GROUP = "INSERT INTO groups (description,isprimary,viewappsvisible,fulldescription,ordernum)\r\nVALUES (@description,@isprimary,@viewappsvisible,@fulldescription,@ordernum);\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS groupid";

		// Token: 0x0400014C RID: 332
		internal const string QI_USER = "INSERT INTO people (firstname,middlename,lastname,student_no,isactive,dateadded) VALUES (@fne,@mne,@lne,@sne,1,getdate());\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS personid";

		// Token: 0x0400014D RID: 333
		internal const string QI_USER_DATES_ADDED = "INSERT INTO peopledatesadded (personid,dateadded,whoadded ) VALUES (@pid,getdate(),@whoami)";

		// Token: 0x0400014E RID: 334
		internal const string QI_USER_ADD_TO_GROUPS = "INSERT INTO peoplegroups (groupid,personid,isprimarygroup) \r\n    SELECT orderid AS groupid,@pid,\r\n        CASE WHEN ( orderid>=1 AND orderid<=4 ) THEN CAST(1 as bit)\r\n        ELSE CAST(0 AS bit)\r\n        END FROM splitorderids(@gids,',') WHERE NOT EXISTS(SELECT personid FROM peoplegroups WHERE personid=@pid AND groupid=orderid)";

		// Token: 0x0400014F RID: 335
		internal const string QU_GROUP = "UPDATE groups SET description=@description,viewappsvisible=@viewappsvisible,fulldescription=@fulldescription,\r\nordernum=@ordernum WHERE groupid=@gid";

		// Token: 0x04000150 RID: 336
		internal const string QU_USER = "IF EXISTS(SELECT personid FROM people WHERE isactive=1 AND student_no=@sne)\r\n    UPDATE people SET firstname=@fne,middlename=@mne,lastname=@lne WHERE personid=@pid\r\nelse \r\n    UPDATE people SET firstname=@fne,middlename=@mne,lastname=@lne,student_no=@sne WHERE personid=@pid";

		// Token: 0x04000151 RID: 337
		internal const string Qu_REACTIVATE_USER = "UPDATE people SET isactive=1 WHERE personid=@pid";

		// Token: 0x04000152 RID: 338
		internal const string QD_GROUP = "DELETE FROM groups WHERE groupid=@gid";

		// Token: 0x04000153 RID: 339
		internal const string QD_DEACTIVATE_USER = "UPDATE people SET isactive=0 WHERE personid=@pid";

		// Token: 0x04000154 RID: 340
		internal const string QD_REACTIVATE_USER = "UPDATE people SET isactive=1 WHERE personid=@pid";

		// Token: 0x04000155 RID: 341
		internal const string QD_USER_REMOVE_FROM_GROUPS = "DELETE FROM peoplegroups WHERE personid=@pid AND groupid IN (SELECT orderid AS groupid FROM splitorderids(@gids,','))";
	}
}
