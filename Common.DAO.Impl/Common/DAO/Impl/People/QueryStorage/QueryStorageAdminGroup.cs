using System;

namespace TechnoPro.Common.DAO.Impl.People.QueryStorage
{
	// Token: 0x0200007A RID: 122
	internal static class QueryStorageAdminGroup
	{
		// Token: 0x04000156 RID: 342
		internal const string QS_GROUP_MEMBERS = "SELECT   DISTINCT p.personid,p.Firstname,p.Lastname,p.Student_no,pg.groupid,g.Description AS GroupMemberships \r\nFROM    people p LEFT JOIN peoplegroups pg ON pg.personid=p.personid \r\n        LEFT JOIN groups g ON g.groupid=pg.groupid \r\nWHERE   p.isactive=@personIsActive\r\n        AND (@gids='' \r\n                OR p.personid IN (SELECT personid FROM peoplegroups WHERE groupid IN (SELECT orderid AS groupid FROM splitorderids(@gids,',')))\r\n        ) \r\nORDER BY p.personid,g.description";

		// Token: 0x04000157 RID: 343
		internal const string QI_GROUP = "INSERT INTO Groups ([description],isPrimary,viewAppsVisible,fullDescription,ordernum) \r\nVALUES (@description,0,@visibleincalendar,@fulldescription,@ordernum)\r\nSET @groupid = (SELECT CAST(SCOPE_IDENTITY() AS int) AS groupid)";

		// Token: 0x04000158 RID: 344
		internal const string QU_GROUP_ORDER = "UPDATE Groups SET ordernum=@ordernum WHERE groupid=@groupid";

		// Token: 0x04000159 RID: 345
		internal const string QU_GROUP = "UPDATE Groups SET [description]=@description,fullDescription=@fulldescription,ordernum=@ordernum,viewAppsVisible=@visibleincalendar WHERE groupid=@groupid";

		// Token: 0x0400015A RID: 346
		internal const string QD_GROUP = "DELETE FROM Groups WHERE GroupId=@groupid AND NOT EXISTS(SELECT groupid FROM peoplegroups WHERE groupid=@groupid)";

		// Token: 0x0400015B RID: 347
		internal const string QU_GROUP_CONTAINER_TITLE = "UPDATE Groups SET FullDescription=@newFullDescription WHERE FullDescription=@oldFullDescription";

		// Token: 0x0400015C RID: 348
		internal const string QI_ADD_MEMBERS_TO_GROUP = "DECLARE @isPrimaryGroup bit = CASE WHEN @gid>0 AND @gid<5 THEN CAST(1 AS BIT) ELSE CAST(0 AS bit) END\r\nINSERT INTO peoplegroups(personid,groupid,isprimarygroup)\r\n\tSELECT orderid AS personid,@gid,@isPrimaryGroup FROM SplitOrderIDs(@pids,',') WHERE orderid>0 AND NOT orderid IN (SELECT personid AS orderid FROM peoplegroups WHERE groupid=@gid)";

		// Token: 0x0400015D RID: 349
		internal const string QD_REMOVE_MEMBERS_FROM_GROUP = "DELETE FROM peoplegroups WHERE groupid=@gid AND personid IN (SELECT orderid AS personid FROM splitorderids(@pids,','))";
	}
}
