using System;

namespace TechnoPro.Common.DAO.Impl.UserSettingsPermissions.QueryStorage
{
	// Token: 0x0200002A RID: 42
	public class QueryStorageOldSetting
	{
		// Token: 0x04000053 RID: 83
		internal const string QS_LOAD_PERSON_SETTING = "SELECT\t0 AS settinggroupid,s.settingid,0 AS groupid,s.personid,s.settingcode,s.settingvalue,s.settingstringvalue,0 As ordernum\r\nFROM\tsettings s \r\nWHERE\ts.personid=@pid AND s.settingcode=@settingcode\r\nORDER BY s.settingcode";

		// Token: 0x04000054 RID: 84
		internal const string QS_PERSON_SETTINGS_BY_PERSONID = "SELECT\t0 AS settinggroupid,s.settingid,0 AS groupid,s.personid,s.settingcode,s.settingvalue,s.settingstringvalue,0 As ordernum\r\nFROM\tsettings s \r\nWHERE\ts.personid=@pid\r\nORDER BY s.settingcode";

		// Token: 0x04000055 RID: 85
		internal const string QS_GROUP_SETTINGS_BY_GROUPID = "SELECT\tsg.settinggroupid,0 AS settingid,g.groupid,0 AS personid,sg.settingcode,sg.settingvalue,sg.settingstringvalue,g.ordernum\r\nFROM\tsettingsgroups sg LEFT JOIN groups g ON sg.groupID=g.GroupID \r\nWHERE\tsg.groupid=@gid\r\nORDER BY g.ordernum,sg.settingcode";

		// Token: 0x04000056 RID: 86
		internal const string QS_LOAD_USER_SETTINGS = "IF EXISTS(SELECT * FROM sysobjects WHERE id = object_id(N'[LoadUserSettings]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)\r\n            BEGIN\r\n\t            EXEC LoadUserSettings @pid\r\n            END\r\n            ELSE\r\n            BEGIN\r\n\t            SELECT DISTINCT x.* FROM\r\n\t\t            (\r\n\t\t\t            SELECT\t0 AS settinggroupid,s.settingid,0 AS groupid,s.personid,s.settingcode,s.settingvalue,s.settingstringvalue,0 As ordernum\r\n\t\t\t            FROM\tsettings s \r\n\t\t\t            WHERE\ts.personid=@pid\r\n\r\n\t\t\t            UNION\r\n\r\n\t\t\t            SELECT\tsg.settinggroupid,0 AS settingid,pg.groupid,0 AS personid,sg.settingcode,sg.settingvalue,sg.settingstringvalue,g.ordernum\r\n\t\t\t            FROM\tpeoplegroups pg LEFT JOIN groups g ON g.groupid=pg.groupid\r\n\t\t\t\t\t            LEFT JOIN settingsgroups sg ON sg.groupID=pg.GroupID \r\n\t\t\t            WHERE\tpg.personid=@pid\r\n\r\n\t\t\t            UNION\r\n\r\n\t\t\t            SELECT\tsg.settinggroupid,0 AS settingid,sg.groupid,0 AS personid,sg.settingcode,sg.settingvalue,sg.settingstringvalue,0 AS ordernum\r\n\t\t\t            FROM\tsettingsgroups sg \r\n\t\t\t            WHERE\tsg.groupid=-1 \r\n\t\t            ) x \r\n\t\t            ORDER BY x.personid DESC,x.groupid DESC,x.ordernum\r\n            END";

		// Token: 0x04000057 RID: 87
		internal const string QI_CREATE_OR_UPDATE_PERSON_SETTING = "IF EXISTS(SELECT settingid FROM settings WHERE personid=@pid AND settingcode=@settingcode)\r\nBEGIN\r\n    UPDATE settings SET settingvalue=@settingvalue,settingstringvalue=@settingstringvalue WHERE personid=@pid AND settingcode=@settingcode\r\n    SELECT settingid FROM settings WHERE personid=@pid AND settingcode=@settingcode\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO settings (personid,settingcode,settingvalue,settingstringvalue) VALUES (@pid,@settingcode,@settingvalue,@settingstringvalue);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS settingid;\r\nEND";

		// Token: 0x04000058 RID: 88
		internal const string QI_CREATE_OR_UPDATE_GROUP_SETTING = "IF EXISTS(SELECT settinggroupid FROM settingsgroups WHERE groupid=@gid AND settingcode=@settingcode)\r\nBEGIN\r\n    UPDATE settingsgroups SET settingvalue=@settingvalue,settingstringvalue=@settingstringvalue WHERE groupid=@gid AND settingcode=@settingcode\r\n    SELECT settinggroupid FROM settingsgroups WHERE groupid=@gid AND settingcode=@settingcode\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO settingsgroups (groupid,settingcode,settingvalue,settingstringvalue) VALUES (@gid,@settingcode,@settingvalue,@settingstringvalue);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS settinggroupid;\r\nEND";

		// Token: 0x04000059 RID: 89
		internal const string QD_PERSON_SETTING = "DELETE FROM settings WHERE personid=@pid AND settingcode=@settingcode";

		// Token: 0x0400005A RID: 90
		internal const string QD_GROUP_SETTING = "DELETE FROM settingsgroups WHERE groupid=@gid AND settingcode=@settingcode";
	}
}
