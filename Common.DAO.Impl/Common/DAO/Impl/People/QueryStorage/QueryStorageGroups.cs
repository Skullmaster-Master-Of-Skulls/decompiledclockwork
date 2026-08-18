using System;

namespace TechnoPro.Common.DAO.Impl.People.QueryStorage
{
	// Token: 0x0200007B RID: 123
	internal static class QueryStorageGroups
	{
		// Token: 0x0400015E RID: 350
		internal const string QS_GROUP_CONTAINERS = "SELECT 0 AS ordernum,'Main' AS fulldescription\r\nUNION\r\nSELECT DISTINCT 1 AS ordernum,fulldescription FROM groups WHERE NOT fulldescription IS NULL AND NOT fulldescription=''\r\nORDER BY ordernum,fulldescription";

		// Token: 0x0400015F RID: 351
		internal const string QI_GROUP = "IF EXISTS(SELECT groupid FROM groups WHERE description=@grouptitle)\r\n    SET @gid=(SELECT TOP 1 groupid FROM groups WHERE description=@grouptitle)\r\nELSE \r\nBEGIN\r\n    INSERT INTO groups (description,isprimary,viewappsvisible,fulldescription,ordernum) VALUES (@grouptitle,0,0,'',9999)\r\n    SET @gid=(SELECT TOP 1 CAST(SCOPE_IDENTITY() AS int) AS gid)\r\nEND";

		// Token: 0x04000160 RID: 352
		internal const string QI_FIRST_GROUP_OR_CREATE_NEW_BY_TITLE = "SELECT orderid AS [description] INTO #t1 FROM splitstrings2(@grouptitles,',')\r\n\r\nSET @gid = (SELECT TOP 1 groupid FROM groups WHERE [description] IN (SELECT [description] FROM #t1))\r\n\r\nIF (@gid IS NULL OR @gid<1)\r\nBEGIN\r\n    INSERT INTO groups (description,isprimary,viewappsvisible,fulldescription,ordernum) VALUES (@grouptitle,0,0,'',9999)\r\n    SET @gid=(SELECT TOP 1 CAST(SCOPE_IDENTITY() AS int) AS gid)\r\nEND";

		// Token: 0x04000161 RID: 353
		internal const string QS_ALL_GROUPS_FOR_EDIT = "SELECT groupid,description,isprimary,viewappsvisible,fulldescription,ordernum FROM groups ORDER BY ordernum,description";
	}
}
