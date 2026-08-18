using System;

namespace TechnoPro.Common.DAO.Impl.UserSettingsPermissions.QueryStorage
{
	// Token: 0x0200002B RID: 43
	public static class QueryStoragePermissions
	{
		// Token: 0x0400005B RID: 91
		internal const string QS_PERMISSIONS_BY_USER = "SELECT groupid,ordernum INTO #t1 FROM peoplegroups WHERE personid=@pid ORDER BY ordernum;\r\nDECLARE @maxGroupIdOrderNum int\r\nSET @maxGroupIdOrderNum=(SELECT MAX(ordernum) FROM #t1)\r\nIF @maxGroupIdOrderNum IS NULL \r\n    SET @maxGroupIdOrderNum=0;\r\nSET @maxGroupIdOrderNum=@maxGroupIdOrderNum+100\r\n\r\nSELECT  permissionid AS id,CAST(NULL AS int) AS groupid,personid,permissioncode,permissionvalue,CAST(0 AS int) AS ordernum \r\nFROM    permissions \r\nWHERE   personid=@pid\r\n\r\nUNION\r\n\r\nSELECT  p.permissiongroupid AS id,#t1.groupid,CAST(NULL AS int) AS personid,p.permissioncode,p.permissionvalue,#t1.ordernum\r\nFROM    permissionsgroups p LEFT JOIN #t1 ON #t1.groupid=p.groupid\r\nWHERE   #t1.groupid > 0\r\n\r\nUNION   \r\n\r\nSELECT  permissiongroupid AS id,groupid,CAST(NULL AS int) AS personid,permissioncode,permissionvalue,@maxGroupIdOrderNum AS ordernum\r\nFROM    permissionsgroups\r\nWHERE   groupid <= 0\r\n\r\nDROP TABLE #t1";

		// Token: 0x0400005C RID: 92
		internal const string QS_JUST_PERMISSIONS_BY_USER = "SELECT permissionid,personid,permissioncode,permissionvalue FROM permissions WHERE personid=@pid";

		// Token: 0x0400005D RID: 93
		internal const string QS_JUST_PERMISSIONS_BY_GROUP = "SELECT permissiongroupid,groupid,permissioncode,permissionvalue FROM permissionsgroups WHERE groupid=@gid";

		// Token: 0x0400005E RID: 94
		internal const string QI_USER_PERMISSION = "INSERT INTO permissions (personid,permissioncode,permissionvalue) VALUES (@pid,@pc,@val)";

		// Token: 0x0400005F RID: 95
		internal const string QI_GROUP_PERMISSION = "INSERT INTO permissionsgroups (groupid,permissioncode,permissionvalue) VALUES (@gid,@pc,@val)";

		// Token: 0x04000060 RID: 96
		internal const string QD_ALL_USER_PERMISSIONS = "DELETE FROM permissions WHERE personid=@pid";

		// Token: 0x04000061 RID: 97
		internal const string QD_ALL_GROUP_PERMISSIONS = "DELETE FROM permissionsgroups WHERE groupid=@gid";
	}
}
